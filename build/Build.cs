using CP.BuildTools;
using Nuke.Common.Git;
using Nuke.Common.Tools.NerdbankGitVersioning;

[GitHubActions(
    "continuous",
    GitHubActionsImage.WindowsLatest,
    Lfs = true,
    FetchDepth = 0,
    On = new[] { GitHubActionsTrigger.Push },
    InvokedTargets = new[] { nameof(Pack) },
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

    public static int Main() => Execute<Build>(x => x.Pack);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    private readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [GitRepository]
    private readonly GitRepository Repository;

    [Solution(GenerateProjects = true)]
    private readonly Solution Solution;

    [NerdbankGitVersioning]
    private readonly NerdbankGitVersioning NerdbankVersioning;

    private static AbsolutePath OutputDirectory => RootDirectory / "output";

    private AbsolutePath ThemeDirectory => RootDirectory / "theme";

    ////private static AbsolutePath SourceDirectory => RootDirectory;

    Target Print => _ => _
        .Before(Clean)
        .Executes(() =>
        {
            Log.Information("Branch = {Branch}", Repository.Branch);
            Log.Information("Commit = {Commit}", Repository.Commit);
            Log.Information("GitVersion = {Value}", NerdbankVersioning.SimpleVersion);
        });

    Target Clean => _ => _
        .DependsOn(Print)
        .Before(Restore)
        .Executes(() =>
        {
            // throws=> UnauthorizedAccessException: Access to the path 'Azure.Core.dll' is denied.
            ////SourceDirectory.GlobDirectories("*/bin", "*/obj").DeleteDirectories();
            ThemeDirectory.DeleteDirectory();
            OutputDirectory.DeleteDirectory();
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(async () =>
        {
            // TODO: Replace this with Nuke's inbuilt mechanism for installing SDK's
            // I have not been able to locate any documentation nor examples on how to do this,
            // except for the installation of the Build Project specified Target SDK via the specified value in global.json.
            await this.InstallDotNetSdk("3.1.x", "7.x.x");

            // TODO: Confirm that we need to checkout the existing branch, this should be done by Nuke and always results in Nothing to checkout.
            ////Git($"checkout {Repository.Branch}");
            Git($"clone -s -n https://github.com/glennawatson/Docable5.git {ThemeDirectory.ToString("dn")}");
            Git($"checkout", ThemeDirectory.ToString("dn"));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() => DotNetBuild(_ => _
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)));

    Target Pack => _ => _
        .DependsOn(Compile) // TODO: Confirm that we want to artifact each induvidual folder in the output dir. - .GlobDirectories("**/*").Select(x => x.ToString()).ToArray()
        .Produces(OutputDirectory)
        .Executes(() => DotNetRun(_ => _
                .SetProjectFile(Solution.AllProjects.First(x => x.Name.Contains("ReactiveUI.Web")))
                .SetConfiguration(Configuration)));
}
