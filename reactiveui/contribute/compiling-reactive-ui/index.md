---
NoTitle: true
Title: Compiling
---

0. ReactiveUI will not build correctly on Visual Studio for Mac as of 22/08/2017 building for multiple TFM's is not implemented nor working in Visual Studio for Mac. Only netstandard is compiled.

1. You'll need a windows machine w/VS2022 with UWP+Windows10 SDK's installed. We are able to compile iOS and Mac platforms without being connected to a Mac via reference assemblies. It's magic. You do not need a mac.

2. Clone ReactiveUI using Git, you won't be able to compile from the archive as the build script uses GitVersion to automatically determine semver.

3. ReactiveUI and other projects within the group make use of msbuild.sdk.extras. Each project repository contains a global.json that denotes the version of the .Net Core SDK being used. If you do not have the correct version you will see a solution with a bunch of unloaded projects. When you attempt to load a project you will get a message akin to:

   ```
   Unable to locate the .NET Core SDK. Check that it is installed and that the version specified in global.json (if any) matches the installed version.

   D:\github\reactiveui\splat\src\Splat\Splat.csproj : error  : The expression "[System.IO.Path]::GetDirectoryName('')" cannot be evaluated. The path is not of a legal form.  C:\Users\username\.nuget\packages\msbuild.sdk.extras\2.0.29\Sdk\Sdk.props
   ```

   To get the correct version (or later) please visit https://dotnet.microsoft.com/download/dotnet-core


## Introduction

For you to contribute to the ReactiveUI framework you need to have an understanding of the ReactiveUI framework.
This document will give you an overview of the ReactiveUI framework elements.

ReactiveUI is a composable, cross-platform model-view-viewmodel framework for all .NET platforms that is inspired by functional reactive programming which is a paradigm that allows you to abstract mutable state away from your user interfaces and express the idea around a feature in one readable place and improve the testability of your application.

## The Main Features of ReactiveUI

* **Reactive programming** - ReactiveUI is a reactive programming framework which means you are able to easily express complex architectures in a composable, testable way.
* **MVVM** - ReactiveUI is a MVVM framework which means it provides a way to structure your application in a way that is easy to reason about and test.
* **Cross-platform** - ReactiveUI is a cross-platform framework which means you can share your business logic across all .NET platforms.
* **Extensible** - ReactiveUI is an extensible framework which means you can extend the framework to fit your needs.
* **Testable** - ReactiveUI is a testable framework which means you can test your application in a deterministic way.
* **ReactiveUI is a community project** - ReactiveUI is a community project which means it is developed by the community for the community.
* **ReactiveUI is open source** - ReactiveUI is open source which means you can dig into the source code and learn from it.
* **ReactiveUI is used by many companies** - ReactiveUI is used by many companies which means it is a framework that is used in production and is battle tested.

## The Main Elements of ReactiveUI

* **ReactiveObject** - ReactiveObject is the base class for objects that notify when properties change.
* **ReactiveCommand** - ReactiveCommand is a command that notifies when it is executing and has completed.
* **WhenAnyValue** - WhenAnyValue is a method that allows you to observe when a property changes.
* **ObservableAsPropertyHelper** - ObservableAsPropertyHelper is a class that allows you to create a read-only property from an observable.

ReactiveUI has Splat as a dependency which is a portable library for logging, error reporting, and a service locator.

ReactiveUI has Akavache as a dependency which is an asynchronous, persistent key-value store created for writing desktop and mobile applications in C#, based on SQLite3. Akavache is great for both storing important data (i.e. user settings) as well as cached local data that expires.

ReactiveUI has DynamicData as a dependency which is a portable class library which brings the power of Reactive Extensions (Rx) to collections. Rx is extremely powerful but out of the box provides nothing to assist with managing collections, nor does it solve the problem of turning asynchronous data into collections. This library fills that void, and initialises the Reactive Extensions suite of collections.

The combination of ReactiveUI, Splat, Akavache, and DynamicData is called the ReactiveUI framework.

In order to maintain a high level of quality, ReactiveUI has a set of unit tests that are run on every commit via CI. ReactiveUI also has a set of integration tests that are run on every commit using CI.

All code changes are reviewed by the ReactiveUI team via pull requests. The ReactiveUI team consists of the core team and the community. The core team consists of the maintainers of the ReactiveUI framework. The community consists of the contributors to the ReactiveUI framework.

Any code changes must take into account the other projects and platforms in the ReactiveUI framework and how they are affected by the code changes.

Any code changes must take into account the existing functionality and how it affects the operation of current releases in the ReactiveUI framework and how they are affected by the code changes.
