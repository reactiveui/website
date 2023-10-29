using CP.BuildTools;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using ReactiveUI.Web;
using Nuke.Common.Tools.MSBuild;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    private readonly string reactiveui = nameof(reactiveui);

    private AbsolutePath RxUIAPIDirectory => RootDirectory / reactiveui / "api" / reactiveui;
    private AbsolutePath RxMAPIDirectory => RootDirectory / reactiveui / "api" / "reactivemarbles";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(async () =>
        {
            RxUIAPIDirectory.DeleteDirectory();
            RxMAPIDirectory.DeleteDirectory();
            // Install docfx
            ProcessTasks.StartShell("dotnet tool update -g docfx").AssertZeroExitCode();
            // Install DotNet SDK's
            await this.InstallDotNetSdk("3.1.x", "5.x.x", "6.x.x", "7.x.x");
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            var RxUIProjects = new string[] { reactiveui, "akavache", "fusillade", "punchclock", "splat" };
            // Restore ReactiveUI Projects
            RxUIAPIDirectory.GetSources(reactiveui, RxUIProjects);

            // Build ReactiveUI Projects
            foreach (var project in RxUIProjects)
            {
                try
                {
                    MSBuildTasks.MSBuild(s => s
                        .SetProjectFile(RxUIAPIDirectory / "external" / project / $"{project}-main" / "src")
                        .SetConfiguration(Configuration)
                        .SetRestore(false));
                }
                catch { }
            }

            // Build Reactive Marbles Projects
            RxMAPIDirectory.GetSources("reactivemarbles", "DynamicData");
            try
            {
                MSBuildTasks.MSBuild(s => s
                    .SetProjectFile(RxMAPIDirectory / "external" / "DynamicData" / "DynamicData-main" / "src")
                    .SetConfiguration(Configuration)
                    .SetRestore(false));
            }
            catch { }
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            // Generate Website output
            ProcessTasks.StartShell($"docfx {RootDirectory / reactiveui}\\docfx.json").AssertZeroExitCode();

            // Copy main.css file to output
            (RootDirectory / reactiveui / "public" / "main.css").MoveToDirectory(RootDirectory / reactiveui / "_site" / "public");

            // Publish to netlify via yml
        });
}
