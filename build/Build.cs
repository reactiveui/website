using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.NerdbankGitVersioning;

[GitHubActions(
    "continuous",
    GitHubActionsImage.WindowsLatest,
    Lfs = true,
    On = new[] { GitHubActionsTrigger.Push },
    InvokedTargets = new[] { nameof(Compile) },
    CacheKeyFiles = new[] { "**/global.json", "**/*.csproj" },
    CacheIncludePatterns = new[] { ".nuke/temp", "~/.nuget/packages" },
    CacheExcludePatterns = new string[0])]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Pack);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [CI]
    private static GitHubActions GitHubActions => GitHubActions.Instance;

    private static AbsolutePath OutputDirectory => RootDirectory / "output";

    private AbsolutePath WebOutputDirectory => RootDirectory / "web";

    private AbsolutePath ThemeDirectory => WebOutputDirectory / "theme";

    private static AbsolutePath SourceDirectory => RootDirectory;

    private static AbsolutePath SolutionFile => RootDirectory / "ReactiveUI.Web.sln";

    [NerdbankGitVersioning]
    readonly NerdbankGitVersioning NerdbankVersioning;

    Target Print => _ => _
        .DependsOn(InstallDependencies)
        .Before(Clean)
        .Executes(() =>
        {
            Log.Information("Branch = {Branch}", GitHubActions?.Ref);
            Log.Information("Commit = {Commit}", GitHubActions?.Sha);
            Log.Information("GitVersion = {Value}", NerdbankVersioning.SimpleVersion);
        });

    Target InstallDependencies => _ => _
        .Executes(() =>
        {
            NerdbankGitVersioningInstall();
            NerdbankGitVersioningCloud();            
        });

    Target Clean => _ => _
        .DependsOn(Print)
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("*/bin", "*/obj").DeleteDirectories();
            ThemeDirectory.DeleteDirectory();
            OutputDirectory.CreateOrCleanDirectory();
        });

    Target Restore => _ => _
        .DependsOn(InstallDependencies)
        .Executes(() =>
        {
            Git($"checkout {GitHubActions.Ref} {WebOutputDirectory.ToString("d")}");
            Git($"checkout https://github.com/glennawatson/Docable5.git {ThemeDirectory.ToString("d")}");
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(_ => _
                .SetProjectFile(SolutionFile)
                .SetConfiguration(Configuration));
        });

    Target Pack => _ => _
        .DependsOn(Compile)
        .Produces(OutputDirectory.GlobDirectories("**/*").Select(x => x.ToString()).ToArray())
        .Executes(() => { /* Implementation */ });
}
