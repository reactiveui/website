using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using ReactiveUI.Web;
using System;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    private static readonly string reactiveui = nameof(reactiveui);
    private static readonly string akavache = nameof(akavache);
    private static readonly string fusillade = nameof(fusillade);
    private static readonly string punchclock = nameof(punchclock);
    private static readonly string splat = nameof(splat);
    private static readonly string DynamicData = nameof(DynamicData);
    private static readonly string reactivemarbles = nameof(reactivemarbles);
    private static readonly string extensions = nameof(extensions);
    private static readonly string[] RxUIProjects = [akavache, fusillade, punchclock, splat, extensions]; ////, reactiveui, "ReactiveUI.Validation", "ReactiveUI.Avalonia", "Maui.Plugins.Popup"];

    private AbsolutePath RxUIAPIDirectory => RootDirectory / reactiveui / "api" / reactiveui;
    private AbsolutePath RxMAPIDirectory => RootDirectory / reactiveui / "api" / reactivemarbles;

    private AbsolutePath ApiLibDirectory => RootDirectory / "reactiveui" / "api" / "lib";
    private AbsolutePath ApiRefsDirectory => RootDirectory / "reactiveui" / "api" / "refs";
    private AbsolutePath ApiCacheDirectory => RootDirectory / "reactiveui" / "api" / "cache";

    Target Clean => _ => _
        .Before(FetchPackages)
        .Executes(() =>
        {
            ApiLibDirectory.DeleteDirectory();
            ApiRefsDirectory.DeleteDirectory();
            // Install docfx
            ProcessTasks.StartShell("dotnet tool update -g docfx").AssertZeroExitCode();
        });

    Target FetchPackages => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            NuGetFetcher.FetchPackages(RootDirectory);
        });

    Target BuildWebsite => _ => _
        .DependsOn(FetchPackages)
        .Produces(RootDirectory / "reactiveui" / "_site")
        .Executes(() =>
        {
            try
            {
                NuGetFetcher.PatchDocfxJson(RootDirectory);
                ProcessTasks.StartShell("docfx reactiveui/docfx.json").AssertZeroExitCode();
                NuGetFetcher.LogInfo("Web Site build complete");
            }
            catch (Exception ex)
            {
                NuGetFetcher.LogError(ex.ToString());
            }
        });

    [Parameter("Port for the preview server (default: 8080)")]
    readonly int Port = 8080;

    Target Serve => _ => _
        .DependsOn(BuildWebsite)
        .Executes(() =>
        {
            NuGetFetcher.LogInfo($"Serving website at http://localhost:{Port}");
            ProcessTasks.StartShell($"docfx serve reactiveui/_site -p {Port}").AssertZeroExitCode();
        });
}
