using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using ReactiveUI.Web;
using Nuke.Common.Tools.MSBuild;
using System.IO;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    private static readonly string reactiveui = nameof(reactiveui);

    [Solution(GenerateProjects = true)] private readonly Solution Solution;

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    private readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    private AbsolutePath RxUIAPIDirectory => RootDirectory / reactiveui / "api" / reactiveui;
    private AbsolutePath RxSrcIDirectory => RootDirectory / reactiveui / "api" / "src";

    private Project PackageProject;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            RxUIAPIDirectory.DeleteDirectory();
            RxSrcIDirectory.CreateOrCleanDirectory();

            // Install docfx
            ProcessTasks.StartShell("dotnet tool update -g docfx").AssertZeroExitCode();
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            // Restore ReactiveUI Projects
            RxUIAPIDirectory.GetSources(reactiveui, reactiveui);

            // Restore Reactive Projects from Nuget
            PackageProject = Solution.GetProject("NugetPackageExtractionForDocs");
            DotNetRestore(s => s.SetProjectFile(PackageProject));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            try
            {
                var dirRx = RxUIAPIDirectory / "external" / reactiveui / $"{reactiveui}-main" / "src";
                File.Copy(RootDirectory / "global.json", dirRx / "global.json", true);
                MSBuildTasks.MSBuild(s => s
                    .SetProjectFile(dirRx / $"{reactiveui}.sln")
                    .SetConfiguration(Configuration)
                    .SetRestore(true));
            }
            catch { }

            MSBuildTasks.MSBuild(s => s
                .SetProjectFile(PackageProject)
                .SetConfiguration(Configuration)
                .SetRestore(true));
        });
}
