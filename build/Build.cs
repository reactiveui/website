using System;
using System.Linq;
using CP.BuildTools;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;

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

    Target Clean => _ => _
        .Before(Restore)
        .Executes(async () =>
        {
            // Install docfx
            ProcessTasks.StartShell("dotnet tool update -g docfx").AssertZeroExitCode();
            // Install DotNet SDK's
            await this.InstallDotNetSdk("3.1.x", "5.x.x", "6.x.x", "7.x.x");
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            // TODO: Restore ReactiveUI Projects
            
            // TODO: Build ReactiveUI Projects
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            // TODO: Generate ReactiveUI API Docs

            // Generate Website output
            ProcessTasks.StartShell($"docfx {RootDirectory}\\reactiveui\\docfx.json").AssertZeroExitCode();

            // TODO: Publish to netlify
        });

}
