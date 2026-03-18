using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using ReactiveUI.Web;
using System;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.BuildWebsite);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    private static readonly string reactiveui = nameof(reactiveui);
    private static readonly string api = nameof(api);
    private AbsolutePath WebRootPath => RootDirectory / reactiveui;
    private AbsolutePath ApiPath => WebRootPath / api;
    private AbsolutePath ApiLibDirectory => ApiPath / "lib";
    private AbsolutePath ApiRefsDirectory => ApiPath / "refs";
    private AbsolutePath ApiCacheDirectory => ApiPath / "cache";
    private AbsolutePath DocfxConfigPath => WebRootPath / "docfx.json";
    private AbsolutePath SiteOutputPath => WebRootPath / "_site";

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
            NuGetFetcher.FetchPackages(RootDirectory, ApiPath);
        });

    Target BuildWebsite => _ => _
        .DependsOn(FetchPackages)
        .Produces(SiteOutputPath)
        .Executes(() =>
        {
            try
            {
                NuGetFetcher.PatchDocfxJson(RootDirectory);
                ProcessTasks.StartProcess("docfx", $"\"{DocfxConfigPath}\"", workingDirectory: RootDirectory)
                    .AssertZeroExitCode();
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
            ProcessTasks.StartProcess("docfx", $"serve \"{SiteOutputPath}\" -p {Port}", workingDirectory: RootDirectory)
                .AssertZeroExitCode();
        });
}
