using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using ReactiveUI.Web;
using System;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.BuildWebsite);

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
