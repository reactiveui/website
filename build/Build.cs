using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.MSBuild;
using ReactiveUI.Web;
using System;
using System.IO;
using System.Linq;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

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
    private static readonly string[] RxUIProjects = [reactiveui, akavache, fusillade, punchclock, splat, "ReactiveUI.Validation", "ReactiveUI.Avalonia", extensions, "Maui.Plugins.Popup"];

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
                    var solutionFile = dirRx / $"{project}.sln";

                    // Find any .sln or .slnx file as a fallback
                    var solutionFileSearch = Directory.EnumerateFiles(dirRx, "*.sln*").FirstOrDefault();
                    if (File.Exists(solutionFile) == false)
                    {
                        solutionFile += "x";
                        if (File.Exists(solutionFile) == false)
                        {
                            if (File.Exists(solutionFileSearch))
                            {
                                solutionFile = solutionFileSearch;
                            }
                            else
                            {
                                SourceFetcher.LogRepositoryError(reactiveui, project, $"Solution file not found: {dirRx / $"{project}.sln(x)"}");
                                continue;
                            }
                        }
                    }

                    SourceFetcher.LogInfo($"Restoring {solutionFile}...");
                    DotNetRestore(s => s.SetProjectFile(solutionFile));
                    SourceFetcher.LogInfo($"Building {solutionFile}...");
                    DotNetBuild(s => s
                        .SetProjectFile(solutionFile)
                        .SetConfiguration(Configuration)
                        .EnableNoRestore());
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
                var solutionFile = dirDd / $"{DynamicData}.sln";
                if (File.Exists(solutionFile) == false)
                {
                    solutionFile += "x";
                    if (File.Exists(solutionFile) == false)
                    {
                        SourceFetcher.LogRepositoryError(reactivemarbles, DynamicData, $"Solution file not found: {dirDd / $"{DynamicData}.sln(x)"}");
                        return;
                    }
                }

                SourceFetcher.LogInfo($"Building {solutionFile}...");
                MSBuildTasks.MSBuild(s => s
                    .SetProjectFile(solutionFile)
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
