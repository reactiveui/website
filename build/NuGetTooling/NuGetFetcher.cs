#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Nuke.Common.IO;
using Polly;
using Polly.Retry;

namespace ReactiveUI.Web.NuGetTooling;

/// <summary>
/// Fetches NuGet packages defined by <c>nuget-packages.json</c>, extracts
/// their managed assemblies into TFM-bucketed directories under
/// <c>api/lib</c>, and stages reference assemblies under <c>api/refs</c>
/// so the docfx assembly resolver can find them. Designed to be invoked
/// from a Nuke build target as <see cref="FetchPackagesAsync"/>.
/// </summary>
internal static class NuGetFetcher
{
    /// <summary>
    /// Maximum number of NuGet packages downloaded in parallel. Kept small
    /// to stay friendly to the NuGet feed and avoid rate-limit responses.
    /// </summary>
    private const int MaxParallelDownloads = 3;

    /// <summary>
    /// Number of retry attempts the Polly policy makes per HTTP call before
    /// surfacing the failure.
    /// </summary>
    private const int RetryAttempts = 6;

    /// <summary>
    /// Base URI of the NuGet v3 service index used for endpoint discovery.
    /// </summary>
    private static readonly Uri ServiceIndexUri = new("https://api.nuget.org/v3/index.json");

    /// <summary>
    /// Base URI of the NuGet v3 flat-container endpoint used for version
    /// resolution and <c>.nupkg</c> downloads.
    /// </summary>
    private static readonly Uri FlatContainerUri = new("https://api.nuget.org/v3-flatcontainer/");

    /// <summary>
    /// The NuGet search service interface implemented by the endpoint we
    /// consult. Pinned to 3.5.0 because it returns owner-filtered results.
    /// </summary>
    private const string SearchQueryServiceType = "SearchQueryService/3.5.0";

    /// <summary>
    /// Reads <c>nuget-packages.json</c> and orchestrates the full fetch:
    /// reference packages first, then owner-discovered + additional
    /// packages in parallel. Finally, copies reference assemblies into each
    /// <c>lib/</c> TFM directory so docfx's <c>UniversalAssemblyResolver</c>
    /// finds them via the assembly-local search before falling back to the
    /// references section.
    /// </summary>
    /// <param name="rootDirectory">Repository root containing <c>nuget-packages.json</c>.</param>
    /// <param name="apiPath">Destination root, typically <c>reactiveui/api</c>.</param>
    public static async Task FetchPackagesAsync(AbsolutePath rootDirectory, AbsolutePath apiPath)
    {
        var manifestPath = rootDirectory / "nuget-packages.json";
        var json = await File.ReadAllTextAsync(manifestPath);
        var config = JsonSerializer.Deserialize(json, PackageConfigContext.Default.PackageConfig)
            ?? throw new InvalidOperationException("Failed to deserialize nuget-packages.json");

        var libDir = apiPath / "lib";
        var cacheDir = apiPath / "cache";
        var refsDir = apiPath / "refs";

        libDir.CreateDirectory();
        cacheDir.CreateDirectory();
        refsDir.CreateDirectory();

        if (config.ReferencePackages.Length > 0)
        {
            await FetchReferencePackagesAsync(config.ReferencePackages, refsDir, cacheDir);
        }

        var discoveredIds = await DiscoverAllPackagesAsync(config);

        foreach (var additional in config.AdditionalPackages)
        {
            if (!discoveredIds.Any(d => string.Equals(d.Id, additional.Id, StringComparison.OrdinalIgnoreCase)))
            {
                discoveredIds.Add((additional.Id, additional.Version));
            }
        }

        var excludeSet = new HashSet<string>(config.ExcludePackages, StringComparer.OrdinalIgnoreCase);
        discoveredIds.RemoveAll(d =>
            excludeSet.Contains(d.Id)
            || config.ExcludePackagePrefixes.Any(prefix =>
                d.Id.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)));

        (string Id, string? Version, string? Tfm)[] allPackages =
        [
            .. discoveredIds.Select(d =>
            {
                config.TfmOverrides.TryGetValue(d.Id, out var tfm);
                return (d.Id, d.Version, Tfm: tfm);
            }),
        ];

        Log.Info($"Fetching {allPackages.Length} packages...");
        await FetchGroupAsync(libDir, cacheDir, allPackages, config.TfmPreference);

        CopyRefsIntoLibDirs(libDir, refsDir);
    }

    /// <summary>
    /// Constructs the standard exponential-backoff retry policy used for
    /// every HTTP call in this fetcher. Catches <see
    /// cref="HttpRequestException"/> and waits exponentially longer between
    /// attempts (0.2s, 0.4s, 0.8s, ...).
    /// </summary>
    /// <returns>A configured <see cref="AsyncRetryPolicy"/>.</returns>
    private static AsyncRetryPolicy CreateRetryPolicy() =>
        Policy.Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                RetryAttempts,
                attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2, attempt)));

    /// <summary>
    /// Copies extracted reference assemblies from <c>refs/</c> into each
    /// <c>lib/</c> TFM directory using <see
    /// cref="TfmResolver.FindBestRefsTfm"/> to pair them. Files that
    /// already exist in the destination are not overwritten so package
    /// assemblies always win over reference shims of the same name.
    /// </summary>
    /// <param name="libDir">Root of the per-TFM lib directories.</param>
    /// <param name="refsDir">Root of the per-TFM refs directories.</param>
    private static void CopyRefsIntoLibDirs(AbsolutePath libDir, AbsolutePath refsDir)
    {
        if (!Directory.Exists(libDir) || !Directory.Exists(refsDir))
        {
            return;
        }

        List<string> refsTfms = [.. Directory.GetDirectories(refsDir).Select(d => Path.GetFileName(d)!)];

        foreach (var libTfmDir in Directory.GetDirectories(libDir))
        {
            var libTfm = Path.GetFileName(libTfmDir)!;
            var bestRef = TfmResolver.FindBestRefsTfm(libTfm, refsTfms);
            if (bestRef == null)
            {
                continue;
            }

            var refDir = Path.Combine(refsDir, bestRef);
            var count = 0;
            foreach (var refDll in Directory.GetFiles(refDir, "*.dll"))
            {
                var destPath = Path.Combine(libTfmDir, Path.GetFileName(refDll));
                if (!File.Exists(destPath))
                {
                    File.Copy(refDll, destPath);
                    count++;
                }
            }

            if (count > 0)
            {
                Log.Info($"Copied {count} reference assemblies into lib/{libTfm} (from refs/{bestRef})");
            }
        }
    }

    /// <summary>
    /// Discovers every package owned by any of the configured NuGet owner
    /// accounts via the search service. Returns the list of (id, version)
    /// tuples; versions are <see langword="null"/> at this stage and
    /// resolved later per-package.
    /// </summary>
    /// <param name="config">The parsed package configuration.</param>
    /// <returns>The discovered package identifiers paired with a null version.</returns>
    private static async Task<List<(string Id, string? Version)>> DiscoverAllPackagesAsync(PackageConfig config)
    {
        using var client = new HttpClient();
        var retryPolicy = CreateRetryPolicy();

        var searchEndpoint = await ResolveSearchEndpointAsync(client, retryPolicy);
        Log.Info($"Using NuGet search endpoint: {searchEndpoint}");

        var allIds = new List<(string Id, string? Version)>();
        foreach (var owner in config.NugetPackageOwners)
        {
            var ids = await DiscoverPackagesByOwnerAsync(client, retryPolicy, searchEndpoint, owner);
            Log.Info($"Discovered {ids.Count} packages for owner '{owner}'");
            allIds.AddRange(ids.Select(id => (id, (string?)null)));
        }

        return allIds;
    }

    /// <summary>
    /// Fetches reference-only packages and extracts their reference
    /// assemblies into per-TFM directories under <c>refs/</c>. Failures on
    /// individual packages are logged and skipped so one bad package does
    /// not abort the whole run.
    /// </summary>
    /// <param name="packages">Reference packages to fetch.</param>
    /// <param name="refsDir">Output root for extracted reference assemblies.</param>
    /// <param name="cacheDir">Cache directory for the downloaded <c>.nupkg</c> files.</param>
    private static async Task FetchReferencePackagesAsync(
        ReferencePackage[] packages,
        AbsolutePath refsDir,
        AbsolutePath cacheDir)
    {
        using var client = new HttpClient();
        var retryPolicy = CreateRetryPolicy();

        foreach (var pkg in packages)
        {
            try
            {
                var idLower = pkg.Id.ToLowerInvariant();

                var version = pkg.Version;
                if (version == null)
                {
                    Log.Info($"Resolving latest version for reference package {pkg.Id}...");
                    version = await ResolveLatestStableVersionAsync(client, retryPolicy, idLower);
                    if (version == null)
                    {
                        Log.Error($"Could not resolve version for reference package {pkg.Id}, skipping");
                        continue;
                    }
                }

                Log.Info($"Using reference package {pkg.Id} v{version}");

                var versionLower = version.ToLowerInvariant();
                var nupkgPath = cacheDir / $"{idLower}.{versionLower}.nupkg";
                if (!File.Exists(nupkgPath))
                {
                    Log.Info($"Downloading {pkg.Id} v{version}...");
                    await DownloadNupkgAsync(client, retryPolicy, idLower, versionLower, nupkgPath);
                }
                else
                {
                    Log.Info($"Using cached {pkg.Id} v{version}");
                }

                var tfmRefsDir = refsDir / pkg.TargetTfm;
                tfmRefsDir.CreateDirectory();
                ExtractReferenceAssemblies(nupkgPath, tfmRefsDir, pkg.Id, pkg.PathPrefix);
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to fetch reference package {pkg.Id}: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Returns <see langword="true"/> when the supplied stream contains a
    /// pure managed (IL-only) assembly. Filters out native and mixed-mode
    /// DLLs that Roslyn cannot consume as references (for example
    /// <c>System.EnterpriseServices.Wrapper.dll</c>).
    /// </summary>
    /// <param name="stream">Stream positioned at the start of a candidate PE file.</param>
    /// <returns><see langword="true"/> if the assembly is managed and IL-only.</returns>
    private static bool IsManagedAssembly(Stream stream)
    {
        try
        {
            using var peReader = new PEReader(stream, PEStreamOptions.LeaveOpen);
            return peReader.HasMetadata
                && peReader.PEHeaders.CorHeader != null
                && peReader.PEHeaders.CorHeader.Flags.HasFlag(CorFlags.ILOnly);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Extracts every managed <c>.dll</c> under the requested path prefix
    /// (typically <c>ref/</c>) of the supplied <c>.nupkg</c> into the
    /// destination directory. Native and mixed-mode DLLs are filtered out
    /// using <see cref="IsManagedAssembly"/>.
    /// </summary>
    /// <param name="nupkgPath">Path to the <c>.nupkg</c> on disk.</param>
    /// <param name="tfmRefsDir">Destination directory for the extracted assemblies.</param>
    /// <param name="packageId">Package identifier, for logging only.</param>
    /// <param name="pathPrefix">Path prefix inside the archive to extract from.</param>
    private static void ExtractReferenceAssemblies(
        AbsolutePath nupkgPath,
        AbsolutePath tfmRefsDir,
        string packageId,
        string pathPrefix)
    {
        using var archive = ZipFile.OpenRead(nupkgPath);
        var prefix = pathPrefix.TrimEnd('/') + "/";
        var count = 0;

        foreach (var entry in archive.Entries)
        {
            if (!entry.FullName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (string.IsNullOrEmpty(entry.Name))
            {
                continue;
            }

            var ext = Path.GetExtension(entry.Name).ToLowerInvariant();
            if (ext is not ".dll")
            {
                continue;
            }

            using var entryStream = entry.Open();
            using var memStream = new MemoryStream();
            entryStream.CopyTo(memStream);

            memStream.Position = 0;
            if (!IsManagedAssembly(memStream))
            {
                Log.Info($"  Skipping native DLL: {entry.Name}");
                continue;
            }

            memStream.Position = 0;
            var destPath = Path.Combine(tfmRefsDir, entry.Name);
            using var fileStream = new FileStream(destPath, FileMode.Create);
            memStream.CopyTo(fileStream);
            count++;
        }

        Log.Info($"Extracted {count} reference assemblies from {packageId} ({pathPrefix})");
    }

    /// <summary>
    /// Reads the NuGet v3 service index and returns the URI of the search
    /// service registered for <see cref="SearchQueryServiceType"/>. Throws
    /// when the service index does not advertise that type.
    /// </summary>
    /// <param name="client">HTTP client used for the request.</param>
    /// <param name="retryPolicy">Retry policy applied to the request.</param>
    /// <returns>The resolved search endpoint URI.</returns>
    private static async Task<Uri> ResolveSearchEndpointAsync(
        HttpClient client,
        AsyncRetryPolicy retryPolicy)
    {
        Uri? endpoint = null;

        await retryPolicy.ExecuteAsync(async () =>
        {
            var response = await client.GetAsync(ServiceIndexUri);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            foreach (var resource in doc.RootElement.GetProperty("resources").EnumerateArray())
            {
                var type = resource.GetProperty("@type").GetString();
                if (type == SearchQueryServiceType)
                {
                    var id = resource.GetProperty("@id").GetString();
                    if (id != null)
                    {
                        endpoint = new Uri(id);
                    }

                    break;
                }
            }
        });

        return endpoint ?? throw new InvalidOperationException(
            $"Could not find {SearchQueryServiceType} in NuGet service index");
    }

    /// <summary>
    /// Pages through the NuGet search service to enumerate every package
    /// (including unlisted) owned by the supplied owner. Deprecated
    /// packages are skipped so they don't appear in the documentation.
    /// </summary>
    /// <param name="client">HTTP client used for the requests.</param>
    /// <param name="retryPolicy">Retry policy applied to each request.</param>
    /// <param name="searchEndpoint">The resolved search service endpoint.</param>
    /// <param name="owner">The NuGet owner account to query.</param>
    /// <returns>Discovered package identifiers (deduplication is handled by the caller).</returns>
    private static async Task<List<string>> DiscoverPackagesByOwnerAsync(
        HttpClient client,
        AsyncRetryPolicy retryPolicy,
        Uri searchEndpoint,
        string owner)
    {
        var packageIds = new List<string>();
        var skip = 0;
        const int take = 100;

        while (true)
        {
            var queryBuilder = new UriBuilder(searchEndpoint)
            {
                Query = $"q=owner:{Uri.EscapeDataString(owner)}&take={take}&skip={skip}&semVerLevel=2.0.0",
            };
            var url = queryBuilder.Uri;
            int totalHits = 0;

            await retryPolicy.ExecuteAsync(async () =>
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                using var stream = await response.Content.ReadAsStreamAsync();
                using var doc = await JsonDocument.ParseAsync(stream);

                totalHits = doc.RootElement.GetProperty("totalHits").GetInt32();

                foreach (var result in doc.RootElement.GetProperty("data").EnumerateArray())
                {
                    if (result.TryGetProperty("deprecation", out _))
                    {
                        continue;
                    }

                    var id = result.GetProperty("id").GetString();
                    if (id != null)
                    {
                        packageIds.Add(id);
                    }
                }
            });

            skip += take;
            if (skip >= totalHits)
            {
                break;
            }
        }

        return packageIds;
    }

    /// <summary>
    /// Downloads and extracts every package in <paramref name="packages"/>
    /// in parallel up to <see cref="MaxParallelDownloads"/>, reusing
    /// cached <c>.nupkg</c> files when they already exist on disk. Each
    /// package's failures are isolated so a single bad package does not
    /// abort the whole run.
    /// </summary>
    /// <param name="libDir">Root of the per-TFM lib output directories.</param>
    /// <param name="cacheDir">Cache directory for downloaded <c>.nupkg</c> files.</param>
    /// <param name="packages">Tuples of package identifier, optional pinned version, and optional TFM override.</param>
    /// <param name="tfmPreference">The global TFM preference list.</param>
    private static async Task FetchGroupAsync(
        AbsolutePath libDir,
        AbsolutePath cacheDir,
        (string Id, string? Version, string? Tfm)[] packages,
        string[] tfmPreference)
    {
        using var client = new HttpClient();
        using var semaphore = new SemaphoreSlim(MaxParallelDownloads, MaxParallelDownloads);
        var retryPolicy = CreateRetryPolicy();

        var tasks = packages.Select(async pkg =>
        {
            await semaphore.WaitAsync();
            try
            {
                var id = pkg.Id;
                var idLower = id.ToLowerInvariant();

                var version = pkg.Version;
                if (version == null)
                {
                    Log.Info($"Resolving latest version for {id}...");
                    version = await ResolveLatestStableVersionAsync(client, retryPolicy, idLower);
                    if (version == null)
                    {
                        Log.Error($"Could not resolve version for {id}");
                        return;
                    }
                }

                Log.Info($"Using {id} v{version}");

                var versionLower = version.ToLowerInvariant();
                var nupkgPath = cacheDir / $"{idLower}.{versionLower}.nupkg";
                if (!File.Exists(nupkgPath))
                {
                    Log.Info($"Downloading {id} v{version}...");
                    await DownloadNupkgAsync(client, retryPolicy, idLower, versionLower, nupkgPath);
                }
                else
                {
                    Log.Info($"Using cached {id} v{version}");
                }

                ExtractAssemblies(nupkgPath, libDir, id, pkg.Tfm, tfmPreference);
                Log.Info($"Extracted {id} v{version}");
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to process {pkg.Id}: {ex.Message}");
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(tasks);
    }

    /// <summary>
    /// Resolves the latest stable (non-prerelease) version of the named
    /// package from the NuGet flat-container index.
    /// </summary>
    /// <param name="client">HTTP client used for the request.</param>
    /// <param name="retryPolicy">Retry policy applied to the request.</param>
    /// <param name="idLower">Lowercased package identifier.</param>
    /// <returns>The resolved version string, or <see langword="null"/> when the package has no stable releases.</returns>
    private static async Task<string?> ResolveLatestStableVersionAsync(
        HttpClient client,
        AsyncRetryPolicy retryPolicy,
        string idLower)
    {
        string? result = null;
        var url = new Uri(FlatContainerUri, $"{idLower}/index.json");

        await retryPolicy.ExecuteAsync(async () =>
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            string[] versions =
            [
                .. doc.RootElement.GetProperty("versions")
                    .EnumerateArray()
                    .Select(v => v.GetString()!)
                    .Where(v => !v.Contains('-')),
            ];

            result = versions.LastOrDefault();
        });

        return result;
    }

    /// <summary>
    /// Downloads a specific package version's <c>.nupkg</c> to the
    /// supplied path on disk.
    /// </summary>
    /// <param name="client">HTTP client used for the request.</param>
    /// <param name="retryPolicy">Retry policy applied to the request.</param>
    /// <param name="idLower">Lowercased package identifier.</param>
    /// <param name="versionLower">Lowercased package version.</param>
    /// <param name="outputPath">Destination file path.</param>
    private static async Task DownloadNupkgAsync(
        HttpClient client,
        AsyncRetryPolicy retryPolicy,
        string idLower,
        string versionLower,
        AbsolutePath outputPath)
    {
        var url = new Uri(FlatContainerUri, $"{idLower}/{versionLower}/{idLower}.{versionLower}.nupkg");

        await retryPolicy.ExecuteAsync(async () =>
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            using var contentStream = await response.Content.ReadAsStreamAsync();
            using var fileStream = new FileStream(outputPath, FileMode.Create);
            await contentStream.CopyToAsync(fileStream);
        });
    }

    /// <summary>
    /// Selects the best TFM in the supplied <c>.nupkg</c>'s <c>lib/</c>
    /// folder using <see cref="TfmResolver.SelectTfm"/>, then extracts the
    /// matching <c>.dll</c> and <c>.xml</c> files into <c>libDir</c>'s
    /// per-TFM subdirectory.
    /// </summary>
    /// <param name="nupkgPath">Path to the <c>.nupkg</c> on disk.</param>
    /// <param name="libDir">Root of the per-TFM lib output directories.</param>
    /// <param name="packageId">Package identifier, for logging.</param>
    /// <param name="tfmOverride">Optional per-package TFM override.</param>
    /// <param name="tfmPreference">The global TFM preference list.</param>
    private static void ExtractAssemblies(
        AbsolutePath nupkgPath,
        AbsolutePath libDir,
        string packageId,
        string? tfmOverride,
        string[] tfmPreference)
    {
        using var archive = ZipFile.OpenRead(nupkgPath);

        var libEntries = archive.Entries
            .Where(e => e.FullName.StartsWith("lib/", StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrEmpty(e.Name))
            .GroupBy(e =>
            {
                var parts = e.FullName.Split('/');
                return parts.Length >= 2 ? parts[1] : string.Empty;
            })
            .Where(g => !string.IsNullOrEmpty(g.Key))
            .ToDictionary(g => g.Key, g => g.ToArray(), StringComparer.OrdinalIgnoreCase);

        if (libEntries.Count == 0)
        {
            Log.Info($"No lib/ entries found in {packageId}, skipping (source generator or meta-package)");
            return;
        }

        var selectedTfm = TfmResolver.SelectTfm(libEntries.Keys, tfmOverride, tfmPreference);
        if (selectedTfm == null)
        {
            Log.Error($"No matching TFM for {packageId}. Available: {string.Join(", ", libEntries.Keys)}");
            return;
        }

        Log.Info($"  {packageId}: selected TFM '{selectedTfm}'");

        var tfmLibDir = Path.Combine(libDir, selectedTfm);
        Directory.CreateDirectory(tfmLibDir);

        foreach (var entry in libEntries[selectedTfm])
        {
            var ext = Path.GetExtension(entry.Name).ToLowerInvariant();
            if (ext is ".dll" or ".xml")
            {
                var destPath = Path.Combine(tfmLibDir, entry.Name);
                entry.ExtractToFile(destPath, overwrite: true);
            }
        }
    }
}
