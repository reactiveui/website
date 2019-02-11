Title: Compiling
---

0. ReactiveUI will not build correctly on Visual Studio for Mac as of 22/08/2017 building for multiple TFM's is not implemented nor working in Visual Studio for Mac. Only netstandard is compiled.

0. You'll need a windows machine w/VS2017 with UWP+Windows10 SDK's installed. We are able to compile iOS and Mac platforms without being connected to a Mac via reference assemblies. It's magic. You do not need a mac.

1. Clone ReactiveUI using Git, you won't be able to compile from the archive as the build script uses GitVersion to automatically determine semver.

2. Run `./build.cmd` from a Windows machine. Make sure you have the UWP and Windows 10 SDK's installed. They are checkboxes in the Visual Studio installer.

3. If you want to hack away in visual studio, either run `./build.cmd` first or unload the `ReactiveUI-events` packages.

TODO:

Provide overview of what each project does; document any magic tricks, etc.

