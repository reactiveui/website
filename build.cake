#tool "nuget:https://api.nuget.org/v3/index.json?package=Wyam&version=1.0.0"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Cake.Git&version=0.16.0"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Cake.Wyam&version=1.0.0"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Octokit&version=0.27.0"

using Octokit;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define variables
var isRunningOnAppVeyor = AppVeyor.IsRunningOnAppVeyor;
var isPullRequest       = AppVeyor.Environment.PullRequest.IsPullRequest;
var netlifyToken        = EnvironmentVariable("NETLIFY_TOKEN");
var currentBranch       = isRunningOnAppVeyor ? BuildSystem.AppVeyor.Environment.Repository.Branch : GitBranchCurrent("./").FriendlyName;

// Define directories.
var dependenciesDir     = Directory("./dependencies");
var outputPath          = MakeAbsolute(Directory("./output"));
var sourceDir           = dependenciesDir + Directory("reactiveui");

// Variables


//////////////////////////////////////////////////////////////////////
// SETUP
//////////////////////////////////////////////////////////////////////


Setup(ctx =>
{
    // Executed BEFORE the first task.
    Information("Building branch {0}...", currentBranch);
});


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    if(DirectoryExists(dependenciesDir))
    {
        CleanDirectory(dependenciesDir);
        DeleteDirectory(dependenciesDir, true);
    }

    CreateDirectory(dependenciesDir);
});


Task("GetSource")
    .IsDependentOn("Clean")
    .Does(() =>
    {
	    FilePath sourceZip = DownloadFile("https://codeload.github.com/reactiveui/ReactiveUI/zip/master");
        Unzip(sourceZip, dependenciesDir);
        
        // Need to rename the container directory in the zip file to something consistent
        var containerDir = GetDirectories(dependenciesDir.Path.FullPath + "/*").First(x => x.GetDirectoryName().StartsWith("ReactiveUI"));
        MoveDirectory(containerDir, sourceDir);
    });

Task("Build")
    .IsDependentOn("GetArtifacts")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            Recipe = "Docs",
            Theme = "Samson",
            UpdatePackages = true
        });
    });

Task("Preview")
    .IsDependentOn("GetArtifacts")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            Recipe = "Docs",
            Theme = "Samson",
            UpdatePackages = false,
            Preview = true,
            Watch = true
        });
    });

// Assumes Wyam source is local and at ../Wyam
Task("Debug")
    .Does(() =>
    {
        StartProcess("../Wyam/src/clients/Wyam/bin/Debug/wyam.exe",
            "-a \"../Wyam/src/**/bin/Debug/*.dll\" -r \"docs -i\" -t \"../Wyam/themes/Docs/Samson\" -p --attach");
    });

Task("Deploy")
    .WithCriteria(isRunningOnAppVeyor)
    .WithCriteria(!isPullRequest)
    .IsDependentOn("Build")
    .Does(() =>
    {
        Zip("./output", "output.zip", "./output/**/*");
        StartProcess("curl", "--header \"Content-Type: application/zip\" --header \"Authorization: Bearer " + netlifyToken + "\" --data-binary \"@output.zip\" --url https://api.netlify.com/api/v1/sites/reactiveui.netlify.com/deploys");
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");

Task("GetArtifacts")
    .IsDependentOn("GetSource");

Task("AppVeyor")
    .IsDependentOn(isPullRequest ? "Build" : "Deploy");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

if (!StringComparer.OrdinalIgnoreCase.Equals(target, "Deploy"))
{
    RunTarget(target);
}
