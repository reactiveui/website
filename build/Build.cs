using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using ReactiveUI.Web;
using Nuke.Common.Tools.MSBuild;
using System.IO;
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
    private static readonly string[] RxUIProjects = new string[] { reactiveui, akavache, fusillade, punchclock, splat, "ReactiveUI.Validation" };

    private AbsolutePath RxUIAPIDirectory => RootDirectory / reactiveui / "api" / reactiveui;
    private AbsolutePath RxMAPIDirectory => RootDirectory / reactiveui / "api" / reactivemarbles;


    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            RxUIAPIDirectory.DeleteDirectory();
            RxMAPIDirectory.DeleteDirectory();
            // Install docfx
            ProcessTasks.StartShell("dotnet tool update -g docfx").AssertZeroExitCode();
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            // Restore ReactiveUI Projects
            RxUIAPIDirectory.GetSources(RootDirectory, reactiveui, RxUIProjects);

            // Restore Reactive Marbles Projects
            RxMAPIDirectory.GetSources(RootDirectory, reactivemarbles, DynamicData);
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            foreach (var project in RxUIProjects)
            {
                try
                {
                    var dirRx = RxUIAPIDirectory / "external" / project / $"{project}-main" / "src";
                    File.Copy(RootDirectory / "global.json", dirRx / "global.json", true);
                    MSBuildTasks.MSBuild(s => s
                        .SetProjectFile(dirRx / $"{project}.sln")
                        .SetConfiguration(Configuration)
                        .SetRestore(true));
                    SourceFetcher.LogInfo($"{project} build complete");
                }
                catch (Exception ex)
                {
                    SourceFetcher.LogRepositoryError(reactiveui, project, ex.ToString());
                }
            }

            try
            {
                var dirDd = RxMAPIDirectory / "external" / DynamicData / $"{DynamicData}-main" / "src";
                File.Copy(RootDirectory / "global.json", dirDd / "global.json", true);
                MSBuildTasks.MSBuild(s => s
                    .SetProjectFile(dirDd / $"{DynamicData}.sln")
                    .SetConfiguration(Configuration)
                    .SetRestore(true));
                SourceFetcher.LogInfo($"{DynamicData} build complete");
            }
            catch (Exception ex)
            {
                SourceFetcher.LogRepositoryError(reactivemarbles, DynamicData, ex.ToString());
            }
        });

    Target BuildWebsite => _ => _
    .Produces(RootDirectory / reactiveui / "_site")
    .Executes(() =>
    {
        try
        {
            ProcessTasks.StartShell("docfx reactiveui/docfx.json").AssertZeroExitCode();
            SourceFetcher.LogInfo("Web Site build complete");
        }
        catch (Exception ex)
        {
            SourceFetcher.LogError(ex.ToString());
        }
    });
}
