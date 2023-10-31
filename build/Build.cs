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

    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    private readonly string reactiveui = nameof(reactiveui);

    private AbsolutePath RxUIAPIDirectory => RootDirectory / reactiveui / "api" / reactiveui;
    private AbsolutePath RxMAPIDirectory => RootDirectory / reactiveui / "api" / "reactivemarbles";

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
            var RxUIProjects = new string[] { reactiveui, "akavache", "fusillade", "punchclock", "splat" };
            // Restore ReactiveUI Projects
            RxUIAPIDirectory.GetSources(reactiveui, RxUIProjects);

            // Restore Reactive Marbles Projects
            RxMAPIDirectory.GetSources("reactivemarbles", "DynamicData");
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            try
            {
                MSBuildTasks.MSBuild(s => s
                    .SetProjectFile(RxUIAPIDirectory / "external" / reactiveui / $"{reactiveui}-main" / "src" / $"{reactiveui}.sln")
                    .SetConfiguration(Configuration)
                    .SetRestore(false));
            }
            catch { }
        });
}
