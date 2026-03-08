#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Nuke.Common.IO;
using Polly;

namespace ReactiveUI.Web;

internal static class NuGetFetcher
{
    private static readonly object _lockConsoleObject = new();

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private sealed class PackageConfig
    {
        public string[] NugetPackageOwners { get; set; } = [];
        public string[] TfmPreference { get; set; } = [];
        public AdditionalPackage[] AdditionalPackages { get; set; } = [];
        public string[] ExcludePackages { get; set; } = [];
        public ReferencePackage[] ReferencePackages { get; set; } = [];
        public Dictionary<string, string> TfmOverrides { get; set; } = new();
    }

    private sealed class AdditionalPackage
    {
        public string Id { get; set; } = "";
        public string? Version { get; set; }
    }

    private sealed class ReferencePackage
    {
        public string Id { get; set; } = "";
        public string? Version { get; set; }
        public string TargetTfm { get; set; } = "";
        public string PathPrefix { get; set; } = "ref";
    }

    public static void FetchPackages(AbsolutePath rootDirectory)
    {
        var manifestPath = rootDirectory / "nuget-packages.json";
        var json = File.ReadAllText(manifestPath);
        var config = JsonSerializer.Deserialize<PackageConfig>(json, _jsonOptions)
            ?? throw new InvalidOperationException("Failed to deserialize nuget-packages.json");

        // DLLs go into per-TFM subdirectories under lib/ for docfx to resolve cross-package references
        var libDir = rootDirectory / "reactiveui" / "api" / "lib";
        var cacheDir = rootDirectory / "reactiveui" / "api" / "cache";
        var refsDir = rootDirectory / "reactiveui" / "api" / "refs";

        libDir.CreateDirectory();
        cacheDir.CreateDirectory();
        refsDir.CreateDirectory();

        // Fetch reference assembly packages (support libraries for docfx resolution)
        if (config.ReferencePackages.Length > 0)
            FetchReferencePackages(config.ReferencePackages, refsDir, cacheDir);

        // Discover packages from NuGet owners
        var discoveredIds = DiscoverAllPackages(config);

        // Merge additional packages
        foreach (var additional in config.AdditionalPackages)
        {
            if (!discoveredIds.Any(d => string.Equals(d.Id, additional.Id, StringComparison.OrdinalIgnoreCase)))
            {
                discoveredIds.Add((additional.Id, additional.Version));
            }
        }

        // Apply excludes
        var excludeSet = new HashSet<string>(config.ExcludePackages, StringComparer.OrdinalIgnoreCase);
        discoveredIds.RemoveAll(d => excludeSet.Contains(d.Id));

        // Build final package list with TFM overrides
        var allPackages = discoveredIds.Select(d =>
        {
            config.TfmOverrides.TryGetValue(d.Id, out var tfm);
            return (d.Id, d.Version, Tfm: tfm);
        }).ToArray();

        LogInfo($"Fetching {allPackages.Length} packages...");
        FetchGroup(libDir, cacheDir, allPackages, config.TfmPreference);

        // Copy reference assemblies into each lib/ TFM directory so docfx's
        // UniversalAssemblyResolver can find them (it searches the assembly's
        // own directory first, before the references section which is unreliable)
        CopyRefsIntoLibDirs(libDir, refsDir);
    }

    private static void CopyRefsIntoLibDirs(AbsolutePath libDir, AbsolutePath refsDir)
    {
        if (!Directory.Exists(libDir) || !Directory.Exists(refsDir))
            return;

        var refsTfms = Directory.GetDirectories(refsDir)
            .Select(d => Path.GetFileName(d)!)
            .ToList();

        foreach (var libTfmDir in Directory.GetDirectories(libDir))
        {
            var libTfm = Path.GetFileName(libTfmDir)!;
            var bestRef = FindBestRefsTfm(libTfm, refsTfms);
            if (bestRef == null) continue;

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
                LogInfo($"Copied {count} reference assemblies into lib/{libTfm} (from refs/{bestRef})");
        }
    }

    private static List<(string Id, string? Version)> DiscoverAllPackages(PackageConfig config)
    {
        using var client = new HttpClient();
        var retryPolicy = Policy.Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                6,
                attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2, attempt)));

        var searchEndpoint = ResolveSearchEndpoint(client, retryPolicy).GetAwaiter().GetResult();
        LogInfo($"Using NuGet search endpoint: {searchEndpoint}");

        var allIds = new List<(string Id, string? Version)>();

        foreach (var owner in config.NugetPackageOwners)
        {
            var ids = DiscoverPackagesByOwner(client, retryPolicy, searchEndpoint, owner)
                .GetAwaiter().GetResult();
            LogInfo($"Discovered {ids.Count} packages for owner '{owner}'");
            allIds.AddRange(ids.Select(id => (id, (string?)null)));
        }

        return allIds;
    }

    private static void FetchReferencePackages(
        ReferencePackage[] packages,
        AbsolutePath refsDir,
        AbsolutePath cacheDir)
    {
        using var client = new HttpClient();
        var retryPolicy = Policy.Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                6,
                attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2, attempt)));

        foreach (var pkg in packages)
        {
            try
            {
                var idLower = pkg.Id.ToLowerInvariant();

                var version = pkg.Version;
                if (version == null)
                {
                    LogInfo($"Resolving latest version for reference package {pkg.Id}...");
                    version = ResolveLatestStableVersion(client, retryPolicy, idLower)
                        .GetAwaiter().GetResult();
                    if (version == null)
                    {
                        LogError($"Could not resolve version for reference package {pkg.Id}, skipping");
                        continue;
                    }
                }

                LogInfo($"Using reference package {pkg.Id} v{version}");

                var versionLower = version.ToLowerInvariant();
                var nupkgPath = cacheDir / $"{idLower}.{versionLower}.nupkg";
                if (!File.Exists(nupkgPath))
                {
                    LogInfo($"Downloading {pkg.Id} v{version}...");
                    DownloadNupkg(client, retryPolicy, idLower, versionLower, nupkgPath)
                        .GetAwaiter().GetResult();
                }
                else
                {
                    LogInfo($"Using cached {pkg.Id} v{version}");
                }

                var tfmRefsDir = refsDir / pkg.TargetTfm;
                tfmRefsDir.CreateDirectory();
                ExtractReferenceAssemblies(nupkgPath, tfmRefsDir, pkg.Id, pkg.PathPrefix);
            }
            catch (Exception ex)
            {
                LogError($"Failed to fetch reference package {pkg.Id}: {ex.Message}");
            }
        }
    }

    private static bool IsManagedAssembly(Stream stream)
    {
        try
        {
            using var peReader = new PEReader(stream, PEStreamOptions.LeaveOpen);
            // HasMetadata passes mixed-mode assemblies (native+managed) like
            // System.EnterpriseServices.Wrapper.dll — checking ILOnly ensures we
            // only extract pure managed assemblies that Roslyn can reference
            return peReader.HasMetadata
                && peReader.PEHeaders.CorHeader != null
                && peReader.PEHeaders.CorHeader.Flags.HasFlag(CorFlags.ILOnly);
        }
        catch
        {
            return false;
        }
    }

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
                continue;
            if (string.IsNullOrEmpty(entry.Name))
                continue;

            var ext = Path.GetExtension(entry.Name).ToLowerInvariant();
            if (ext is ".dll")
            {
                using var entryStream = entry.Open();
                using var memStream = new MemoryStream();
                entryStream.CopyTo(memStream);

                // Skip native DLLs — only extract managed .NET assemblies
                memStream.Position = 0;
                if (!IsManagedAssembly(memStream))
                {
                    LogInfo($"  Skipping native DLL: {entry.Name}");
                    continue;
                }

                memStream.Position = 0;
                var destPath = Path.Combine(tfmRefsDir, entry.Name);
                using var fileStream = new FileStream(destPath, FileMode.Create);
                memStream.CopyTo(fileStream);
                count++;
            }
        }

        LogInfo($"Extracted {count} reference assemblies from {packageId} ({pathPrefix})");
    }

    private static async Task<string> ResolveSearchEndpoint(
        HttpClient client,
        IAsyncPolicy retryPolicy)
    {
        string? endpoint = null;

        await retryPolicy.ExecuteAsync(async () =>
        {
            var response = await client.GetAsync("https://api.nuget.org/v3/index.json");
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            foreach (var resource in doc.RootElement.GetProperty("resources").EnumerateArray())
            {
                var type = resource.GetProperty("@type").GetString();
                if (type == "SearchQueryService/3.5.0")
                {
                    endpoint = resource.GetProperty("@id").GetString();
                    break;
                }
            }
        });

        return endpoint ?? throw new InvalidOperationException(
            "Could not find SearchQueryService/3.5.0 in NuGet service index");
    }

    private static async Task<List<string>> DiscoverPackagesByOwner(
        HttpClient client,
        IAsyncPolicy retryPolicy,
        string searchEndpoint,
        string owner)
    {
        var packageIds = new List<string>();
        var skip = 0;
        const int take = 100;

        while (true)
        {
            var url = $"{searchEndpoint}?q=owner:{owner}&take={take}&skip={skip}&semVerLevel=2.0.0";
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
                    // Skip deprecated packages
                    if (result.TryGetProperty("deprecation", out _))
                        continue;

                    var id = result.GetProperty("id").GetString();
                    if (id != null)
                        packageIds.Add(id);
                }
            });

            skip += take;
            if (skip >= totalHits)
                break;
        }

        return packageIds;
    }

    private static void FetchGroup(
        AbsolutePath libDir,
        AbsolutePath cacheDir,
        (string Id, string? Version, string? Tfm)[] packages,
        string[] tfmPreference)
    {
        using var client = new HttpClient();
        using var semaphore = new SemaphoreSlim(3, 3);

        var retryPolicy = Policy.Handle<HttpRequestException>()
            .WaitAndRetryAsync(
                6,
                attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2, attempt)));

        Task.WaitAll(packages.Select(pkg => Task.Run(async () =>
        {
            await semaphore.WaitAsync();
            try
            {
                var id = pkg.Id;
                var idLower = id.ToLowerInvariant();

                // Resolve version
                var version = pkg.Version;
                if (version == null)
                {
                    LogInfo($"Resolving latest version for {id}...");
                    version = await ResolveLatestStableVersion(client, retryPolicy, idLower);
                    if (version == null)
                    {
                        LogError($"Could not resolve version for {id}");
                        return;
                    }
                }

                LogInfo($"Using {id} v{version}");

                // Download .nupkg (with cache)
                var versionLower = version.ToLowerInvariant();
                var nupkgPath = cacheDir / $"{idLower}.{versionLower}.nupkg";
                if (!File.Exists(nupkgPath))
                {
                    LogInfo($"Downloading {id} v{version}...");
                    await DownloadNupkg(client, retryPolicy, idLower, versionLower, nupkgPath);
                }
                else
                {
                    LogInfo($"Using cached {id} v{version}");
                }

                // Extract DLL + XML
                ExtractAssemblies(nupkgPath, libDir, id, pkg.Tfm, tfmPreference);
                LogInfo($"Extracted {id} v{version}");
            }
            catch (Exception ex)
            {
                LogError($"Failed to process {pkg.Id}: {ex.Message}");
            }
            finally
            {
                semaphore.Release();
            }
        })).ToArray());
    }

    private static async Task<string?> ResolveLatestStableVersion(
        HttpClient client,
        IAsyncPolicy retryPolicy,
        string idLower)
    {
        string? result = null;
        var url = $"https://api.nuget.org/v3-flatcontainer/{idLower}/index.json";

        await retryPolicy.ExecuteAsync(async () =>
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            var versions = doc.RootElement.GetProperty("versions")
                .EnumerateArray()
                .Select(v => v.GetString()!)
                .Where(v => !v.Contains('-')) // Exclude prerelease
                .ToArray();

            result = versions.LastOrDefault(); // Versions are sorted ascending by the API
        });

        return result;
    }

    private static async Task DownloadNupkg(
        HttpClient client,
        IAsyncPolicy retryPolicy,
        string idLower,
        string versionLower,
        AbsolutePath outputPath)
    {
        var url = $"https://api.nuget.org/v3-flatcontainer/{idLower}/{versionLower}/{idLower}.{versionLower}.nupkg";

        await retryPolicy.ExecuteAsync(async () =>
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            using var contentStream = await response.Content.ReadAsStreamAsync();
            using var fileStream = new FileStream(outputPath, FileMode.Create);
            await contentStream.CopyToAsync(fileStream);
        });
    }

    private static void ExtractAssemblies(
        AbsolutePath nupkgPath,
        AbsolutePath libDir,
        string packageId,
        string? tfmOverride,
        string[] tfmPreference)
    {
        using var archive = ZipFile.OpenRead(nupkgPath);

        // Find all lib/ entries grouped by TFM
        var libEntries = archive.Entries
            .Where(e => e.FullName.StartsWith("lib/", StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrEmpty(e.Name))
            .GroupBy(e =>
            {
                var parts = e.FullName.Split('/');
                return parts.Length >= 2 ? parts[1] : "";
            })
            .Where(g => g.Key != "")
            .ToDictionary(g => g.Key, g => g.ToArray(), StringComparer.OrdinalIgnoreCase);

        if (libEntries.Count == 0)
        {
            LogInfo($"No lib/ entries found in {packageId}, skipping (source generator or meta-package)");
            return;
        }

        // Select TFM
        var selectedTfm = SelectTfm(libEntries.Keys, tfmOverride, tfmPreference);
        if (selectedTfm == null)
        {
            LogError($"No matching TFM for {packageId}. Available: {string.Join(", ", libEntries.Keys)}");
            return;
        }

        LogInfo($"  {packageId}: selected TFM '{selectedTfm}'");

        // Extract DLL and XML files into per-TFM subdirectory
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

    private static string? SelectTfm(
        ICollection<string> availableTfms,
        string? tfmOverride,
        string[] tfmPreference)
    {
        // 1. Per-package override — exact match
        if (tfmOverride != null)
        {
            var exact = availableTfms.FirstOrDefault(t =>
                string.Equals(t, tfmOverride, StringComparison.OrdinalIgnoreCase));
            if (exact != null) return exact;

            // Per-package override — prefix match (e.g. "net10.0-android" matches "net10.0-android35.0")
            var prefix = availableTfms.FirstOrDefault(t =>
                t.StartsWith(tfmOverride, StringComparison.OrdinalIgnoreCase));
            if (prefix != null) return prefix;
        }

        // 2. Global preference — exact match
        foreach (var pref in tfmPreference)
        {
            var exact = availableTfms.FirstOrDefault(t =>
                string.Equals(t, pref, StringComparison.OrdinalIgnoreCase));
            if (exact != null) return exact;
        }

        // 3. Global preference — prefix match
        foreach (var pref in tfmPreference)
        {
            var prefix = availableTfms.FirstOrDefault(t =>
                t.StartsWith(pref, StringComparison.OrdinalIgnoreCase));
            if (prefix != null) return prefix;
        }

        // 4. Fallback: first available
        return availableTfms.FirstOrDefault();
    }

    internal static void LogInfo(string message)
    {
        lock (_lockConsoleObject)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[INFO] ");
            Console.ResetColor();
            Console.WriteLine(message);
        }
    }

    internal static void LogError(string message)
    {
        lock (_lockConsoleObject)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR] ");
            Console.ResetColor();
            Console.WriteLine(message);
        }
    }

    /// <summary>
    /// Dynamically patches docfx.json metadata entries based on discovered lib/ and refs/ TFM subdirectories.
    /// Groups TFMs into core (non-platform) and platform-specific sections.
    /// </summary>
    public static void PatchDocfxJson(AbsolutePath rootDirectory)
    {
        var apiDir = rootDirectory / "reactiveui" / "api";
        var libDir = apiDir / "lib";
        var refsDir = apiDir / "refs";
        var docfxPath = rootDirectory / "reactiveui" / "docfx.json";

        if (!Directory.Exists(libDir))
        {
            LogInfo("No lib/ directory found, skipping docfx.json patching");
            return;
        }

        // Find lib/ TFMs that contain DLLs
        var libTfms = Directory.GetDirectories(libDir)
            .Where(d => Directory.GetFiles(d, "*.dll").Length > 0)
            .Select(d => Path.GetFileName(d)!)
            .OrderBy(t => t)
            .ToList();

        if (libTfms.Count == 0)
        {
            LogInfo("No TFM directories with DLLs found in lib/, skipping docfx.json patching");
            return;
        }

        // Find available refs/ TFMs
        var refsTfms = Directory.Exists(refsDir)
            ? Directory.GetDirectories(refsDir)
                .Where(d => Directory.GetFiles(d, "*.dll").Length > 0)
                .Select(d => Path.GetFileName(d)!)
                .ToList()
            : new List<string>();

        LogInfo($"Discovered lib/ TFMs: {string.Join(", ", libTfms)}");
        LogInfo($"Discovered refs/ TFMs: {string.Join(", ", refsTfms)}");

        // Read existing docfx.json
        var docfxJson = File.ReadAllText(docfxPath);
        using var doc = JsonDocument.Parse(docfxJson);
        var root = doc.RootElement;

        // Extract shared metadata settings from the first existing metadata entry
        var existingMetadata = root.GetProperty("metadata")[0];
        var sharedSettings = new Dictionary<string, JsonElement>();
        foreach (var prop in existingMetadata.EnumerateObject())
        {
            if (prop.Name is not "src" and not "references" and not "dest")
                sharedSettings[prop.Name] = prop.Value;
        }

        // Build one metadata entry per lib TFM — each TFM gets its own compilation
        // with only its matching refs to avoid type system conflicts between
        // .NET Framework (mscorlib) and modern .NET (System.Runtime)
        var metadataEntries = new List<object>();
        var platformLabels = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var tfm in libTfms)
        {
            var bestRef = FindBestRefsTfm(tfm, refsTfms);
            if (bestRef == null)
            {
                LogInfo($"No matching refs for lib/{tfm}, skipping metadata entry");
                continue;
            }

            var platformLabel = GetPlatformLabel(tfm);
            var dest = platformLabel != null ? $"api-{platformLabel}" : "api";
            if (platformLabel != null)
                platformLabels.Add(platformLabel);

            // Only list package DLLs in src.files (not ref DLLs that were copied in
            // for resolver support). Ref DLLs are in the same dir but docfx should
            // only generate docs for the actual packages.
            var refDir = Path.Combine(refsDir, bestRef);
            var refDllNames = Directory.Exists(refDir)
                ? new HashSet<string>(
                    Directory.GetFiles(refDir, "*.dll").Select(f => Path.GetFileName(f)),
                    StringComparer.OrdinalIgnoreCase)
                : new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var libTfmDir = Path.Combine(libDir, tfm);
            var packageDlls = Directory.GetFiles(libTfmDir, "*.dll")
                .Select(f => Path.GetFileName(f))
                .Where(f => !refDllNames.Contains(f!))
                .OrderBy(f => f)
                .ToArray();

            if (packageDlls.Length == 0)
            {
                LogInfo($"No package DLLs in lib/{tfm} (only refs), skipping");
                continue;
            }

            // No references section — ref DLLs are co-located in the lib/ dir
            // so docfx's UniversalAssemblyResolver finds them automatically
            var srcs = new[] { new { src = $"api/lib/{tfm}", files = packageDlls } };

            metadataEntries.Add(BuildMetadataEntry(srcs, Array.Empty<object>(), dest, sharedSettings));
            LogInfo($"Metadata entry: lib/{tfm} ({packageDlls.Length} DLLs, refs from {bestRef}) -> {dest}");
        }

        // Build new docfx.json
        var buildSection = root.GetProperty("build");
        var newDocfx = new Dictionary<string, object>
        {
            ["metadata"] = metadataEntries,
            ["build"] = PatchBuildSection(buildSection, platformLabels.ToList())
        };

        var writeOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var newJson = JsonSerializer.Serialize(newDocfx, writeOptions);
        File.WriteAllText(docfxPath, newJson);
        LogInfo($"Patched docfx.json with {metadataEntries.Count} metadata entries");
    }

    private static string? GetPlatformLabel(string tfm)
    {
        // Modern platform TFMs (net10.0-android, net10.0-ios, etc.)
        if (tfm.Contains("-android", StringComparison.OrdinalIgnoreCase)) return "android";
        if (tfm.Contains("-ios", StringComparison.OrdinalIgnoreCase)) return "ios";
        if (tfm.Contains("-maccatalyst", StringComparison.OrdinalIgnoreCase)) return "maccatalyst";
        if (tfm.Contains("-windows", StringComparison.OrdinalIgnoreCase)) return "windows";
        // Legacy platform TFMs
        if (tfm.StartsWith("monoandroid", StringComparison.OrdinalIgnoreCase)) return "android";
        if (tfm.StartsWith("xamarinios", StringComparison.OrdinalIgnoreCase)) return "ios";
        if (tfm.StartsWith("xamarinmac", StringComparison.OrdinalIgnoreCase)) return "maccatalyst";
        if (tfm.StartsWith("uap", StringComparison.OrdinalIgnoreCase)) return "windows";
        return null;
    }

    private static string? FindBestRefsTfm(string libTfm, List<string> refsTfms)
    {
        // Strip platform suffix for matching (e.g. net10.0-android -> net10.0)
        var baseTfm = libTfm.Contains('-') ? libTfm.Substring(0, libTfm.IndexOf('-')) : libTfm;

        // Exact match first
        var exact = refsTfms.FirstOrDefault(r =>
            string.Equals(r, baseTfm, StringComparison.OrdinalIgnoreCase));
        if (exact != null) return exact;

        // Prefix match (e.g. net48 matches net481 refs, net10.0 matches net10.0 refs)
        var prefix = refsTfms.FirstOrDefault(r =>
            baseTfm.StartsWith(r, StringComparison.OrdinalIgnoreCase));
        if (prefix != null) return prefix;

        // For netstandard, use highest modern .NET refs
        if (baseTfm.StartsWith("netstandard", StringComparison.OrdinalIgnoreCase))
        {
            return refsTfms
                .Where(r => r.StartsWith("net", StringComparison.OrdinalIgnoreCase)
                    && !r.StartsWith("net4", StringComparison.OrdinalIgnoreCase)
                    && !r.StartsWith("netstandard", StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(r => r)
                .FirstOrDefault();
        }

        // No match — legacy TFMs (monoandroid, uap) with no available refs
        return null;
    }

    private static Dictionary<string, object> BuildMetadataEntry(
        object[] srcs,
        object[] refs,
        string dest,
        Dictionary<string, JsonElement> sharedSettings)
    {
        var entry = new Dictionary<string, object>
        {
            ["src"] = srcs
        };

        if (refs.Length > 0)
            entry["references"] = refs;

        entry["dest"] = dest;

        foreach (var (key, value) in sharedSettings)
            entry[key] = value;

        return entry;
    }

    private static Dictionary<string, object> PatchBuildSection(
        JsonElement buildSection,
        List<string> platformLabels)
    {
        var result = new Dictionary<string, object>();

        foreach (var prop in buildSection.EnumerateObject())
        {
            if (prop.Name == "content")
            {
                var contentItems = new List<object>();

                // Add existing content items, filtering out previously-injected platform entries
                foreach (var item in prop.Value.EnumerateArray())
                {
                    var isInjected = false;
                    if (item.TryGetProperty("files", out var files) && files.GetArrayLength() > 0)
                    {
                        var firstFile = files[0].GetString() ?? "";
                        if (firstFile.StartsWith("api-", StringComparison.Ordinal)
                            && !firstFile.StartsWith("api/", StringComparison.Ordinal))
                            isInjected = true;
                    }
                    if (!isInjected)
                        contentItems.Add(item);
                }

                // Add platform API content entries
                foreach (var label in platformLabels.OrderBy(l => l))
                {
                    contentItems.Add(new Dictionary<string, object>
                    {
                        ["files"] = new[] { $"api-{label}/**.yml", $"api-{label}/index.md" }
                    });
                }

                result[prop.Name] = contentItems;
            }
            else
            {
                result[prop.Name] = prop.Value;
            }
        }

        return result;
    }
}
