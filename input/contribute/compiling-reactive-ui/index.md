Title: Compiling
---

0. ReactiveUI will not build correctly on Visual Studio for Mac as of 22/08/2017 building for multiple TFM's is not implemented nor working in Visual Studio for Mac. Only netstandard is compiled.

1. You'll need a windows machine w/VS2017 with UWP+Windows10 SDK's installed. We are able to compile iOS and Mac platforms without being connected to a Mac via reference assemblies. It's magic. You do not need a mac.

2. Clone ReactiveUI using Git, you won't be able to compile from the archive as the build script uses GitVersion to automatically determine semver.

3. ReactiveUI and other projects within the group make use of msbuild.sdk.extras. Each project repository contains a global.json that denotes the version of the .Net Core SDK being used. If you do not have the correct version you will see a solution with a bunch of unloaded projects. When you attempt to load a project you will get a message akin to:

   ```
   Unable to locate the .NET Core SDK. Check that it is installed and that the version specified in global.json (if any) matches the installed version.

   D:\github\reactiveui\splat\src\Splat\Splat.csproj : error  : The expression "[System.IO.Path]::GetDirectoryName('')" cannot be evaluated. The path is not of a legal form.  C:\Users\username\.nuget\packages\msbuild.sdk.extras\2.0.29\Sdk\Sdk.props
   ```

   To get the correct version (or later) please visit https://dotnet.microsoft.com/download/dotnet-core


4. Run `./build.cmd` from a Windows machine. Make sure you have the UWP and Windows 10 SDK's installed. They are checkboxes in the Visual Studio installer.

5. If you want to hack away in visual studio, either run `./build.cmd` first or unload the `ReactiveUI-events` packages.

TODO:

Provide overview of what each project does; document any magic tricks, etc.

