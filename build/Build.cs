#nullable enable

using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Serilog.Extensions.Logging;
using SourceDocParser;
using SourceDocParser.Model;
using SourceDocParser.NuGet.Infrastructure;
using SourceDocParser.SourceLink;
using SourceDocParser.Zensical;
using SourceDocParser.Zensical.Navigation;
using SourceDocParser.Zensical.Options;

namespace ReactiveUI.Web;

/// <summary>
/// Nuke build entry point. Drives the documentation pipeline: fetches
/// NuGet packages, walks their public API surface into Zensical-flavoured
/// Markdown via SourceDocParser.Zensical, regenerates mkdocs.yml with an
/// explicit nav (Zensical doesn't support awesome-nav-style auto-discovery
/// overrides), then shells out to the zensical CLI in a project-local venv
/// to render the final site.
/// </summary>
internal sealed class Build : NukeBuild
{
    private const string DocsFolder = "docs";
    private const string ApiFolder = "api";
    private const string SiteFolder = "site";
    private const string VenvFolder = ".venv";
    private const string RequirementsFile = "requirements.txt";
    private const string MkdocsBaseFile = "mkdocs.base.yml";
    private const string MkdocsFile = "mkdocs.yml";

    /// <summary>
    /// Out-of-tree cache for the NuGet fetcher (lib/, refs/, cache/, and
    /// the .primary-packages sidecar). Keeping it out of <c>docs/</c>
    /// stops these binary artefacts from getting copied into the
    /// published <c>site/</c>.
    /// </summary>
    private const string ApiCacheFolder = ".api-cache";
    private const int DefaultPort = 8000;

    [Parameter("Port for the preview server (default: 8000)")]
    private readonly int Port = DefaultPort;

    [Parameter("Treat broken source links as a build failure (mkdocs-style strict mode)")]
    private readonly bool FailOnBrokenSourceLinks;

    [Parameter("Pass --strict to zensical (CI gate); fails on any broken link or unresolved cross-ref")]
    private readonly bool Strict;

    private static readonly SerilogLoggerFactory _loggerFactory = new(Serilog.Log.Logger, dispose: false);

    private static ExtractionResult? _lastExtractionResult;

    private static NavigationGraph? _lastNavigationGraph;

    public static int Main() => Execute<Build>(x => x.BuildWebsite);

    private static AbsolutePath WebRootPath => RootDirectory / DocsFolder;

    private static AbsolutePath ApiPath => WebRootPath / ApiFolder;

    /// <summary>
    /// Where <see cref="NuGetAssemblySource"/> writes its lib/ + refs/ +
    /// cache/ trees. Lives outside <c>docs/</c> so the assets don't end
    /// up in the rendered site.
    /// </summary>
    private static AbsolutePath ApiCachePath => RootDirectory / ApiCacheFolder;

    private static AbsolutePath SiteOutputPath => RootDirectory / SiteFolder;

    private static AbsolutePath VenvPath => RootDirectory / VenvFolder;

    private static AbsolutePath ZensicalExecutable => OperatingSystem.IsWindows()
        ? VenvPath / "Scripts" / "zensical.exe"
        : VenvPath / "bin" / "zensical";

    private static AbsolutePath VenvPipExecutable => OperatingSystem.IsWindows()
        ? VenvPath / "Scripts" / "pip.exe"
        : VenvPath / "bin" / "pip";

    private Target Clean => _ => _
        .Before(ExtractMetadata)
        .Executes(() =>
        {
            ApiCachePath.DeleteDirectory();
            foreach (var dir in ApiPath.GlobDirectories("*"))
            {
                dir.DeleteDirectory();
            }
            SiteOutputPath.DeleteDirectory();
        });

    /// <summary>
    /// Walks each NuGet package's public API into Zensical Markdown,
    /// captures the navigation graph the emitter just produced, and
    /// regenerates docs/api/index.md from the discovered namespace dirs.
    /// </summary>
    private Target ExtractMetadata => _ => _
        .Executes(async () =>
        {
            // The source's path receives lib/, refs/, cache/ and the
            // .primary-packages sidecar; the emitter's outputRoot
            // receives the rendered Markdown. Keep them separate so
            // binary artefacts stay out of docs/.
            Directory.CreateDirectory(ApiCachePath);
            var source = new NuGetAssemblySource(RootDirectory, ApiCachePath, _loggerFactory.CreateLogger(nameof(NuGetAssemblySource)));

            // Hide the 13k+ API pages from the Zensical client-side
            // search index. The emitter writes `search.exclude: true`
            // into each type/member page's frontmatter, which keeps
            // the search corpus focused on hand-authored prose under
            // docs/ and saves a large chunk of build memory at index
            // time. Symbol cross-references via UID anchors still work.
            var emitterOptions = ZensicalEmitterOptions.Default with { IncludeInSearch = false };
            var inner = new ZensicalDocumentationEmitter(emitterOptions);
            var emitter = new NavigationCapturingEmitter(inner, emitterOptions);
            _lastExtractionResult = await new MetadataExtractor().RunAsync(source, ApiPath, emitter, _loggerFactory.CreateLogger(nameof(MetadataExtractor)));
            _lastNavigationGraph = emitter.NavigationGraph;
            var nsCount = ApiIndexWriter.Write(ApiPath);
            Serilog.Log.Information("Wrote API index for {Count} namespace(s)", nsCount);
        });

    private Target ValidateSourceLinks => _ => _
        .DependsOn(ExtractMetadata)
        // Pin a deterministic order vs the other ExtractMetadata
        // children. Nuke 10's TargetDefinitionOrderChecker fails the
        // run if siblings sharing a dependency have no relative order;
        // these `.Before()` hints don't run BuildWebsite/Serve (they
        // aren't `.Triggers()`), they just give Nuke a topological sort.
        .Before(BuildWebsite)
        .Before(Serve)
        .Executes(async () =>
        {
            if (_lastExtractionResult is not { SourceLinks: { } links })
            {
                Serilog.Log.Information("No extraction result available for validation.");
                return;
            }

            var broken = await new SourceLinkValidator().ValidateAsync(links, FailOnBrokenSourceLinks, _loggerFactory.CreateLogger(nameof(SourceLinkValidator)));
            if (broken == 0)
            {
                Serilog.Log.Information("Source link validation passed.");
            }
        });

    /// <summary>
    /// Bootstraps the project-local Python venv if missing, then makes
    /// sure requirements.txt is installed.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Performance",
        "CA1822:Mark members as static",
        Justification = "Nuke Target properties are bound to the build instance by convention and must remain instance members.")]
    private Target EnsurePythonEnv => _ => _
        // Soft ordering hint: when more than one BuildWebsite-sibling
        // is invoked, run after WriteMkdocsConfig so Nuke's
        // TargetDefinitionOrderChecker has a total order across
        // ValidateSourceLinks -> WriteMkdocsConfig -> EnsurePythonEnv.
        // Doesn't actually drag in WriteMkdocsConfig (no .Triggers()).
        .After(WriteMkdocsConfig)
        .Executes(() =>
        {
            var freshlyCreated = false;
            if (!File.Exists(ZensicalExecutable))
            {
                Serilog.Log.Information("Bootstrapping Python venv at {VenvPath}", VenvPath);
                var python = OperatingSystem.IsWindows() ? "python" : "python3";
                ProcessTasks.StartProcess(python, $"-m venv \"{VenvPath}\"", workingDirectory: RootDirectory)
                    .AssertZeroExitCode();
                freshlyCreated = true;
            }

            var requirements = RootDirectory / RequirementsFile;
            var stamp = VenvPath / ".requirements.stamp";
            if (freshlyCreated || !File.Exists(stamp) || File.GetLastWriteTimeUtc(requirements) > File.GetLastWriteTimeUtc(stamp))
            {
                // Bring pip itself up to date before installing the
                // requirements. Stops the "A new release of pip is
                // available" notice from showing up in CI logs and
                // ensures the resolver in use is the latest release.
                ProcessTasks.StartProcess(VenvPipExecutable, "install --quiet --upgrade pip", workingDirectory: RootDirectory)
                    .AssertZeroExitCode();
                ProcessTasks.StartProcess(VenvPipExecutable, $"install --quiet --upgrade --requirement \"{requirements}\"", workingDirectory: RootDirectory)
                    .AssertZeroExitCode();
                File.WriteAllText(stamp, DateTime.UtcNow.ToString("o"));
            }
        });

    /// <summary>
    /// Concatenates mkdocs.base.yml with a generated <c>nav:</c> block,
    /// writing the result to mkdocs.yml. The generated block walks the
    /// docs tree to fix top-level ordering, then splices in the API tree
    /// captured from the Zensical emitter under <c>API</c>.
    /// </summary>
    private Target WriteMkdocsConfig => _ => _
        .DependsOn(ExtractMetadata)
        // See EnsurePythonEnv.After(WriteMkdocsConfig) -- this hint
        // closes the order on the other side: ValidateSourceLinks
        // first, then WriteMkdocsConfig, then EnsurePythonEnv.
        .After(ValidateSourceLinks)
        .Executes(() =>
        {
            var basePath = RootDirectory / MkdocsBaseFile;
            var outPath = RootDirectory / MkdocsFile;
            var baseYaml = File.ReadAllText(basePath);
            var nav = NavBuilder.Build(WebRootPath, _lastNavigationGraph);
            var sb = new StringBuilder();
            sb.AppendLine("# DO NOT EDIT. Generated by `nuke WriteMkdocsConfig` from mkdocs.base.yml.");
            sb.Append(baseYaml.TrimEnd());
            sb.AppendLine();
            sb.AppendLine();
            sb.Append(nav);
            File.WriteAllText(outPath, sb.ToString());
            Serilog.Log.Information("Wrote {OutPath}", outPath);
        });

    private Target BuildWebsite => _ => _
        .DependsOn(ExtractMetadata)
        .DependsOn(WriteMkdocsConfig)
        .DependsOn(EnsurePythonEnv)
        .Before(Serve)
        .Produces(SiteOutputPath)
        .Executes(() =>
        {
            // zensical reads the output dir from `site_dir` in the YAML
            // (set in mkdocs.base.yml) -- there is no `--site-dir` flag.
            var strictFlag = Strict ? " --strict" : string.Empty;
            RunZensicalStreamed($"build{strictFlag}");
            Serilog.Log.Information("Site built at {SiteOutputPath}", SiteOutputPath);
        });

    private Target Serve => _ => _
        .DependsOn(ExtractMetadata)
        .DependsOn(WriteMkdocsConfig)
        .DependsOn(EnsurePythonEnv)
        .Executes(() =>
        {
            Serilog.Log.Information("Serving site at http://127.0.0.1:{Port}", Port);
            RunZensicalStreamed($"serve --dev-addr 127.0.0.1:{Port}");
        });

    /// <summary>
    /// Runs zensical with stdout + stderr streamed straight to the
    /// console as each line arrives. Nuke's <see cref="ProcessTasks.StartProcess(string, string, string, IReadOnlyDictionary{string, string}, int?, bool?, bool?, Func{string, string})"/>
    /// routes the child's stdout through Debug, which the default
    /// Information filter swallows -- so a multi-minute zensical run
    /// looks frozen until the process exits. Going direct via
    /// <see cref="System.Diagnostics.Process"/> gives us live output
    /// at the cost of losing the Nuke timer banner, which the wrapping
    /// target already prints.
    /// </summary>
    private static void RunZensicalStreamed(string arguments)
    {
        var psi = new System.Diagnostics.ProcessStartInfo
        {
            FileName = ZensicalExecutable,
            Arguments = arguments,
            WorkingDirectory = RootDirectory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };

        // Python's stdout switches to block-buffering when its stdout
        // isn't a TTY (which it isn't once we redirect it to a Process
        // pipe). Result: zensical's progress output sits in an 8 KB
        // buffer for minutes at a time before flushing on exit, so the
        // build looks frozen even though it's actively rendering.
        // PYTHONUNBUFFERED=1 forces line-buffered output, same effect
        // as `python -u`.
        psi.Environment["PYTHONUNBUFFERED"] = "1";

        using var process = new System.Diagnostics.Process { StartInfo = psi };
        process.OutputDataReceived += (_, e) =>
        {
            if (e.Data is { } line)
            {
                Console.Out.WriteLine(line);
            }
        };
        process.ErrorDataReceived += (_, e) =>
        {
            if (e.Data is { } line)
            {
                Console.Error.WriteLine(line);
            }
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            throw new InvalidOperationException($"zensical {arguments.Split(' ')[0]} exited with code {process.ExitCode}.");
        }
    }
}

