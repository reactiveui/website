Title: .Net Standard
Order: 10
---


# .Net Standard Library

As of version 8.0 of ReactiveUI, support for .Net Standard will be included. For any version lower than 8.0, you are still able to reference the RxUI nuget pacakge (along with other popular PCL-based packages) with a little modification. Locate the project.json file in your .Net Standard library project, then locate the following:

```
"frameworks": {
    "netstandard1.4": {}
}
```

and append the `imports` section:

```
"frameworks": {
    "netstandard1.4": {
      "imports": "portable-net45+win8+wp8"
    }
  }
```

For a detailed understanding of what `imports` does, read the following [detailed post](https://msdn.microsoft.com/en-us/library/system.windows.input.icommand.aspx).