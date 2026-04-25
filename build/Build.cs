#nullable enable

using Microsoft.Extensions.Logging;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Serilog.Extensions.Logging;
using SourceDocParser;
using SourceDocParser.Docfx;
using SourceDocParser.NuGet;
using SourceDocParser.SourceLink;
using SourceDocParser.Zensical;

namespace ReactiveUI.Web;

/// <summary>
/// Nuke build entry point. Drives the docfx documentation pipeline:
/// fetching NuGet packages, generating a docfx configuration that points
/// at the extracted assemblies, then running docfx itself. The docfx CLI
/// is restored from the local tool manifest at <c>.config/dotnet-tools.json</c>
/// so its version is tracked there (and bumped automatically by Renovate).
/// </summary>
internal sealed class Build : NukeBuild
{
    /// <summary>
    /// Top-level docs directory name in the repository (also the docfx
    /// project root). Held as a const so path expressions read clearly.
    /// </summary>
    private const string ReactiveUiFolder = "reactiveui";

    /// <summary>
    /// Sub-folder under <see cref="ReactiveUiFolder"/> where the generated
    /// API content lives.
    /// </summary>
    private const string ApiFolder = "api";

    /// <summary>
    /// Default port the local preview server listens on. Overridable from
    /// the command line via <c>--port</c>.
    /// </summary>
    private const int DefaultPort = 8080;

    /// <summary>
    /// Port for the preview server. Sourced from the <c>--port</c>
    /// command-line argument; defaults to <see cref="DefaultPort"/>.
    /// </summary>
    [Parameter("Port for the preview server (default: 8080)")]
    private readonly int Port = DefaultPort;

    /// <summary>
    /// Bridges the Serilog logger Nuke configures into an
    /// <see cref="ILoggerFactory"/> so the parser libraries can log
    /// through their own <c>Microsoft.Extensions.Logging</c> abstraction.
    /// </summary>
    private static readonly SerilogLoggerFactory _loggerFactory = new(Serilog.Log.Logger, dispose: false);

    /// <summary>
    /// Entry point invoked by the Nuke bootstrap script. Selects <see
    /// cref="BuildWebsite"/> as the default target.
    /// </summary>
    /// <returns>Process exit code.</returns>
    public static int Main() => Execute<Build>(x => x.BuildWebsite);

    /// <summary>Path to the docfx project root inside the repository.</summary>
    private static AbsolutePath WebRootPath => RootDirectory / ReactiveUiFolder;

    /// <summary>Path to the generated API content folder.</summary>
    private static AbsolutePath ApiPath => WebRootPath / ApiFolder;

    /// <summary>Path to the per-TFM extracted package assemblies.</summary>
    private static AbsolutePath ApiLibDirectory => ApiPath / "lib";

    /// <summary>Path to the per-TFM extracted reference assemblies.</summary>
    private static AbsolutePath ApiRefsDirectory => ApiPath / "refs";

    /// <summary>Path docfx writes the rendered website to.</summary>
    private static AbsolutePath SiteOutputPath => WebRootPath / "_site";


    /// <summary>
    /// Path of the dynamically generated docfx configuration file.
    /// Written into the docfx project root so docfx resolves the
    /// relative <c>api/</c>, <c>articles/</c>, ... paths correctly.
    /// Regenerated on every run and gitignored.
    /// </summary>
    private static AbsolutePath GeneratedDocfxConfigPath => WebRootPath / "docfx.json";

    /// <summary>
    /// Removes previously-fetched assemblies and the generated docfx
    /// configuration, and restores the locally pinned docfx CLI from the
    /// tool manifest. Always runs before <see cref="FetchPackages"/>.
    /// </summary>
    private Target Clean => _ => _
        .Before(FetchPackages)
        .Executes(() =>
        {
            ApiLibDirectory.DeleteDirectory();
            ApiRefsDirectory.DeleteDirectory();
            GeneratedDocfxConfigPath.DeleteFile();
            RestoreDocfxTool();
        });

    /// <summary>
    /// Fetches every package described in <c>nuget-packages.json</c> and
    /// extracts their assemblies into <see cref="ApiPath"/>.
    /// </summary>
    private Target FetchPackages => _ => _
        .DependsOn(Clean)
        .Executes(async () =>
        {
            await new NuGetFetcher().FetchPackagesAsync(RootDirectory, ApiPath, _loggerFactory.CreateLogger(nameof(NuGetFetcher)));
        });

    /// <summary>
    /// In-memory handoff between <see cref="ExtractMetadata"/> and
    /// <see cref="ValidateSourceLinks"/>. Stashes the most recent
    /// extraction result on the build instance so the validator can
    /// pick up the (uid, url) pairs without re-walking symbols or
    /// reading a manifest file from disk. Cleared (default) until
    /// ExtractMetadata has run in the current process.
    /// </summary>
    private static ExtractionResult? _lastExtractionResult;

    /// <summary>
    /// Loads each package assembly into a Roslyn Compilation, walks
    /// its public surface into an ApiCatalog, and emits one Markdown
    /// page per documented type under build/_intermediate/api-md/.
    /// Stashes the resulting source-link list on the build instance
    /// for an optional follow-up validation target.
    /// </summary>
    private Target ExtractMetadata => _ => _
        .DependsOn(FetchPackages)
        .Executes(async () =>
        {
            var markdownOutput = RootDirectory / "build" / "_intermediate" / "api-md";
            var source = new NuGetAssemblySource(RootDirectory, ApiPath, _loggerFactory.CreateLogger(nameof(NuGetAssemblySource)));
            var emitter = new ZensicalDocumentationEmitter();
            _lastExtractionResult = await new MetadataExtractor().RunAsync(source, markdownOutput, emitter, _loggerFactory.CreateLogger(nameof(MetadataExtractor)));
        });

    /// <summary>
    /// Strict-mode flag for <see cref="ValidateSourceLinks"/>. When
    /// true, the target throws (non-zero exit) on any broken URL —
    /// matches mkdocs's <c>strict</c> behaviour for CI gates.
    /// Default false so day-to-day runs report and continue.
    /// </summary>
    [Parameter("Treat broken source links as a build failure (mkdocs-style strict mode)")]
    private readonly bool FailOnBrokenSourceLinks;

    /// <summary>
    /// Optional post-extraction sweep that HEAD-checks every unique
    /// source URL the converter generated. Reports broken links per
    /// URL with the symbols that referenced them. Rate-limited via
    /// Polly v8 + System.Threading.RateLimiting so we stay friendly
    /// to GitHub and other source hosts.
    ///
    /// In-memory handoff: depends on <see cref="ExtractMetadata"/>,
    /// reads the stashed result from this build instance — no
    /// manifest file on disk. Run on demand, never as part of the
    /// default build chain.
    /// </summary>
    private Target ValidateSourceLinks => _ => _
        .DependsOn(ExtractMetadata)
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
    /// Generates the docfx configuration based on the discovered
    /// assemblies and runs docfx to produce the final website output.
    /// </summary>
    private Target BuildWebsite => _ => _
        .DependsOn(FetchPackages)
        .Produces(SiteOutputPath)
        .Executes(() =>
        {
            var generatedDocfxConfigPath = DocfxConfigWriter.Write(ApiPath, GeneratedDocfxConfigPath, _loggerFactory.CreateLogger(nameof(DocfxConfigWriter)));
            ProcessTasks.StartProcess("dotnet", $"docfx \"{generatedDocfxConfigPath}\"", workingDirectory: RootDirectory)
                .AssertZeroExitCode();
            Serilog.Log.Information("Web Site build complete");
        });

    /// <summary>
    /// Builds the website and then serves it locally on <see cref="Port"/>
    /// via docfx's built-in preview server.
    /// </summary>
    private Target Serve => _ => _
        .DependsOn(BuildWebsite)
        .Executes(() =>
        {
            Serilog.Log.Information("Serving website at http://localhost:{Port}", Port);
            ProcessTasks.StartProcess("dotnet", $"docfx serve \"{SiteOutputPath}\" -p {Port}", workingDirectory: RootDirectory)
                .AssertZeroExitCode();
        });

    /// <summary>
    /// Restores the local dotnet tool manifest at
    /// <c>.config/dotnet-tools.json</c>, which pins the docfx CLI version
    /// used by <see cref="BuildWebsite"/> and <see cref="Serve"/>. The
    /// manifest is the single source of truth for the docfx version and
    /// is updated by Renovate.
    /// </summary>
    private static void RestoreDocfxTool() =>
        ProcessTasks.StartProcess("dotnet", "tool restore", workingDirectory: RootDirectory)
            .AssertZeroExitCode();
}
