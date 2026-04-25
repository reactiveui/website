#nullable enable

using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using ReactiveUI.Web.NuGetTooling;

namespace ReactiveUI.Web;

/// <summary>
/// Nuke build entry point. Drives the docfx documentation pipeline:
/// fetching NuGet packages, generating a docfx configuration that points
/// at the extracted assemblies, then running docfx itself.
/// </summary>
internal sealed class Build : NukeBuild
{
    /// <summary>
    /// Pinned docfx CLI version. Updated explicitly so doc generation is
    /// reproducible across machines and CI runs.
    /// </summary>
    private const string DocfxVersion = "2.78.5";

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

    /// <summary>Path used for the locally installed docfx tool.</summary>
    private static AbsolutePath LocalToolsPath => RootDirectory / ".nuke" / "tools";

    /// <summary>Path of the dynamically generated docfx configuration file.</summary>
    private static AbsolutePath GeneratedDocfxConfigPath => TemporaryDirectory / "docfx.generated.json";

    /// <summary>
    /// Removes previously-fetched assemblies and the generated docfx
    /// configuration, and ensures the docfx CLI tool is installed at the
    /// pinned version. Always runs before <see cref="FetchPackages"/>.
    /// </summary>
    private Target Clean => _ => _
        .Before(FetchPackages)
        .Executes(() =>
        {
            ApiLibDirectory.DeleteDirectory();
            ApiRefsDirectory.DeleteDirectory();
            GeneratedDocfxConfigPath.DeleteFile();
            EnsureDocfxTool();
        });

    /// <summary>
    /// Fetches every package described in <c>nuget-packages.json</c> and
    /// extracts their assemblies into <see cref="ApiPath"/>.
    /// </summary>
    private Target FetchPackages => _ => _
        .DependsOn(Clean)
        .Executes(async () =>
        {
            await NuGetFetcher.FetchPackagesAsync(RootDirectory, ApiPath);
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
            var generatedDocfxConfigPath = DocfxConfigWriter.Write(RootDirectory, GeneratedDocfxConfigPath);
            ProcessTasks.StartProcess("dotnet", $"\"{ResolveDocfxDllPath()}\" \"{generatedDocfxConfigPath}\"", workingDirectory: RootDirectory)
                .AssertZeroExitCode();
            Log.Info("Web Site build complete");
        });

    /// <summary>
    /// Builds the website and then serves it locally on <see cref="Port"/>
    /// via docfx's built-in preview server.
    /// </summary>
    private Target Serve => _ => _
        .DependsOn(BuildWebsite)
        .Executes(() =>
        {
            Log.Info($"Serving website at http://localhost:{Port}");
            ProcessTasks.StartProcess("dotnet", $"\"{ResolveDocfxDllPath()}\" serve \"{SiteOutputPath}\" -p {Port}", workingDirectory: RootDirectory)
                .AssertZeroExitCode();
        });

    /// <summary>
    /// Installs or updates the pinned docfx CLI under <see
    /// cref="LocalToolsPath"/>. Tries <c>tool update</c> first and falls
    /// back to <c>tool install</c> when the tool is not yet present.
    /// </summary>
    private static void EnsureDocfxTool()
    {
        try
        {
            ProcessTasks.StartProcess("dotnet", $"tool update docfx --tool-path \"{LocalToolsPath}\" --version {DocfxVersion}", workingDirectory: RootDirectory)
                .AssertZeroExitCode();
        }
        catch
        {
            ProcessTasks.StartProcess("dotnet", $"tool install docfx --tool-path \"{LocalToolsPath}\" --version {DocfxVersion}", workingDirectory: RootDirectory)
                .AssertZeroExitCode();
        }
    }

    /// <summary>
    /// Locates the <c>docfx.dll</c> shipped under <see
    /// cref="LocalToolsPath"/> for the pinned version. Probes successive
    /// TFM-specific install layouts because the docfx tool's assets folder
    /// changes between releases.
    /// </summary>
    /// <returns>Absolute path to the <c>docfx.dll</c>.</returns>
    /// <exception cref="FileNotFoundException">Thrown when no candidate path exists on disk.</exception>
    private static AbsolutePath ResolveDocfxDllPath()
    {
        AbsolutePath[] candidates =
        [
            LocalToolsPath / ".store" / "docfx" / DocfxVersion / "docfx" / DocfxVersion / "tools" / "net10.0" / "any" / "docfx.dll",
            LocalToolsPath / ".store" / "docfx" / DocfxVersion / "docfx" / DocfxVersion / "tools" / "net9.0" / "any" / "docfx.dll",
            LocalToolsPath / ".store" / "docfx" / DocfxVersion / "docfx" / DocfxVersion / "tools" / "net8.0" / "any" / "docfx.dll",
        ];

        var existingPath = candidates.FirstOrDefault(path => File.Exists(path));
        if (existingPath == null)
        {
            throw new FileNotFoundException($"Could not locate docfx.dll under {LocalToolsPath}");
        }

        return existingPath;
    }
}
