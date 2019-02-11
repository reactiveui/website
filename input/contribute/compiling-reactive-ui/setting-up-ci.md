Title: Setting up CI
---

* Use a windows build agent w/VS2017 with UWP+Windows 10 SDK's installed (iOS and Mac targets can be compiled without a connected OSX machine as we use those platforms reference assemblies during compilation)
* Perform NuGet restore of `src/EventBuilder.sln`
* Perform NuGet restore of `src/ReactiveUI.sln`
* Using the EventBuilder console application, generate `Events_$platform.cs` into the appropriate folders - see https://github.com/reactiveui/ReactiveUI/blob/72b4921d0b60d55b795474c2f7a03918a85fb150/build.cake#L196-L201
* Run `msbuild` (min ver 15.3) with the targets `restore;build;pack` in a `Release` configuration - see https://github.com/reactiveui/ReactiveUI/blob/72b4921d0b60d55b795474c2f7a03918a85fb150/build.cake#L212
* Version numbers of the assemblies and NuGet packages are specified via https://github.com/reactiveui/ReactiveUI/blob/72b4921d0b60d55b795474c2f7a03918a85fb150/build.cake#L222
* Validate the build by running XUnit2 against `"./src/ReactiveUI.Tests/bin/**/*.Tests.dll"`

