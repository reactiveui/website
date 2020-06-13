#module nuget:?package=Cake.DotNetTool.Module&version=0.3.0
#tool "dotnet:?package=Wyam.Tool&version=2.2.8"
#addin "nuget:?package=Cake.Git&version=0.21.0"
#addin "nuget:?package=Octokit&version=0.32.0"

using Octokit;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var dependenciesDir     = Directory("./dependencies");
var outputPath          = MakeAbsolute(Directory("./output"));
var sourceDir           = dependenciesDir + Directory("reactiveui");


//////////////////////////////////////////////////////////////////////
// SETUP
//////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
    // Executed BEFORE the first task.
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
        DeleteDirectory(dependenciesDir, new DeleteDirectorySettings { Recursive = true, Force = true });
    }

    CreateDirectory(dependenciesDir);
});


Task("GetSource")
    .IsDependentOn("Clean")
    .Does(() =>
    {
	    FilePath sourceZip = DownloadFile("https://codeload.github.com/reactiveui/ReactiveUI/zip/main");
        Unzip(sourceZip, dependenciesDir);
        
        // Need to rename the container directory in the zip file to something consistent
        var containerDir = GetDirectories(dependenciesDir.Path.FullPath + "/*").First(x => x.GetDirectoryName().StartsWith("ReactiveUI"));
        MoveDirectory(containerDir, sourceDir);
    });

Task("Build")
    .IsDependentOn("GetArtifacts")
    .Does(() =>
    {
        StartProcess(Context.Tools.Resolve("Wyam*"), new ProcessSettings {
                    Arguments = new ProcessArgumentBuilder()
                        .Append("build")
                        .AppendSwitch("--recipe", "Docs")
                        .AppendSwitch("--theme", "Samson")
                        .Append("-l")
                    });
            
        Zip("./output", "output.zip", "./output/**/*");
    });

Task("Preview")
    .IsDependentOn("GetArtifacts")
    .Does(() =>
    {
        StartProcess(Context.Tools.Resolve("Wyam*"), new ProcessSettings {
                    Arguments = new ProcessArgumentBuilder()
                        .Append("build")
                        .AppendSwitch("--recipe", "Docs")
                        .AppendSwitch("--theme", "Samson")
                        .Append("-l")
                        .Append("--preview")
                        .Append("--watch")
                    });
    });

// Assumes Wyam source is local and at ../../WyamIO/Wyam
Task("Debug")
    .Does(() =>
    {
        var wyamFolder = MakeAbsolute(Directory("../../wyamio/Wyam")).ToString();
        var wyamExecutable = wyamFolder + "/src/clients/Wyam/bin/Debug/netcoreapp2.1/Wyam.dll";
        var wyamIntegrationFolder = wyamFolder + "/tests/integration/Wyam.Examples.Tests";
        var wyamIntegrationBinFolder = wyamIntegrationFolder + "/bin/Debug/netcoreapp2.1";
        var wyamProject = wyamIntegrationFolder + "/Wyam.Examples.Tests.csproj";


        Information($"Building project {wyamProject}");
        DotNetCoreBuild(wyamProject);        
        Information($"Running WYAM at {wyamExecutable}");
        DotNetCoreExecute(wyamExecutable,
            $"-a \"{wyamIntegrationBinFolder}/**/*.dll\" -r \"docs -i\" -t \"{wyamFolder}/themes/Docs/Samson\" -p");
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");

Task("GetArtifacts")
    .IsDependentOn("GetSource");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

if (!StringComparer.OrdinalIgnoreCase.Equals(target, "Deploy"))
{
    RunTarget(target);
}
