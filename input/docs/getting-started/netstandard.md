Title: .NET Standard
Order: 10
---


# .NET Standard Library

As of version 8.0 of ReactiveUI, support for [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) will be included. For any version lower than 8.0, you are still able to reference the RxUI nuget package (along with other popular PCL-based packages) with a little modification.

## Projects using Project.json:

Locate the project.json file in your netstandard library project, then locate the following:

```json
"frameworks": {
    "netstandard1.4": {}
}
```

and append the `imports` section:

```json
"frameworks": {
    "netstandard1.4": {
      "imports": "portable-net45+win8+wp8"
    }
  }
```

For a detailed understanding of what `imports` does, read the following [detailed post](https://msdn.microsoft.com/en-us/library/system.windows.input.icommand.aspx).

## Projects using PackageReference:

Edit your .Net Standard project file and locate the following:

```xml
<PropertyGroup>
	<TargetFramework>netstandard1.4</TargetFramework>
</PropertyGroup>
```

  then change it as such:

```xml
<PropertyGroup>
	<TargetFramework>netstandard1.4</TargetFramework>
	<PackageTargetFallback>portable-net45+win8+wpa81+wp8</PackageTargetFallback>
</PropertyGroup>
```
