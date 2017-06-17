#tool "nuget:https://api.nuget.org/v3/index.json?package=Wyam"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Cake.Wyam"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var archiveDir = Directory("./archive");
var wwwrootDir = Directory("./src/reactiveui.net/wwwroot");

var dashboardUrl = "https://github.com/reactiveui/dashboard/archive/master.zip";
var dashboardDir = wwwrootDir + Directory("dashboard");

var wyamInputDir = Directory("./src/wyam");
var wyamOutputDir = Directory("./output");

var sourceCodeUrl = "https://github.com/reactiveui/ReactiveUI/archive/develop.zip";
var sourceCodeDir = Directory("./src/reactiveui");

var apiDir = wwwrootDir + Directory("api");
var apiAssetsDir = wwwrootDir + Directory("assets");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() => 
    {
        Action<string> delete = (string directory) =>
        {
            if(DirectoryExists(directory))
            {
                DeleteDirectory(directory, true);
            }  
        };

        delete(archiveDir);
        delete(wyamOutputDir);
        delete(dashboardDir);
        delete(sourceCodeDir);
        delete(apiDir);
        delete(apiAssetsDir);
    });

Task("GetDashboard")
    .IsDependentOn("Clean")
    .Does(() =>
    {
	    FilePath dashboardZip = DownloadFile(dashboardUrl);
        Unzip(dashboardZip, archiveDir);
        
        // Need to rename the container directory in the zip file to something consistent
        var containerDir = GetDirectories(archiveDir.Path.FullPath + "/*").First(x => x.GetDirectoryName().StartsWith("dashboard"));
        MoveDirectory(containerDir, dashboardDir);
    });


Task("GetSource")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        if(DirectoryExists(sourceCodeDir))
        {
            DeleteDirectory(sourceCodeDir, true);
        }    

	    FilePath sourceZip = DownloadFile(sourceCodeUrl);
        Unzip(sourceZip, archiveDir);
        
        // Need to rename the container directory in the zip file to something consistent
        var containerDir = GetDirectories(archiveDir.Path.FullPath + "/*").First(x => x.GetDirectoryName().StartsWith("ReactiveUI"));
        MoveDirectory(containerDir, sourceCodeDir);
    });

Task("BuildWyam")
    .IsDependentOn("Clean")
    .IsDependentOn("GetSource")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            InputPath = wyamInputDir
            OutputPath = wyamOutputDir
        });        
    });


Task("CopyApiDocs")
    .IsDependentOn("Clean")
    .IsDependentOn("BuildWyam")
    .Does(() =>
    {
        CopyDirectory(wyamOutputDir + Directory("api"), apiDir);
        CopyDirectory(wyamOutputDir + Directory("assets"), apiAssetsDir);
    });

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("GetDashboard")
    .IsDependentOn("CopyApiDocs")
    .Does(() =>
    {
        StartProcess("dotnet", "restore");
        StartProcess("dotnet", "publish");
    });
    
//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");
    
//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
