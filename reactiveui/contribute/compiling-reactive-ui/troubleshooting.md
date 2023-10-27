---
NoTitle: true
Title: Troubleshooting
---

## Binary Logging
ReactiveUI [uses the binary logger feature](https://github.com/reactiveui/ReactiveUI/blob/72b4921d0b60d55b795474c2f7a03918a85fb150/build.cake#L214) which was made available from msbuild v15.3 onwards to output all build events to a structured/binary log file.

![](~/Images/msbuild-binlog-cli.png)

You'll find `reactiveui.binlog` and `eventgenerator.binlog` in the project directory after your first compile. Install the [MSBuild Structured Log Viewer](https://msbuildlog.com/) and double-click on the `.binlog` to for a more productive troubleshooting experience. Please attach your `.binlog` when opening GitHub issues related to compilation.

![](~/Images/structured-log-viewer.png)

## Binary log advantages

* Completeness - records the exact events in order they happened during the build. You can replay a binary log into other loggers to reconstruct full text logs. Pass the .binlog file to MSBuild.exe instead of a project/solution to replay the log as if the original build was happening.

* Verbosity - binary logs capture more information than even the most diagnostic-level text logs. The binary logger turns on various MSBuild switches to output more information, such as task inputs and target outputs.

* Faster builds - less overhead than a text log, can make builds significantly faster if other logs are disabled. They can be reconstructed later anyway if needed.

* Smaller disk size - a binary log is much smaller in size than a corresponding diagnostic-level text log (10-20x or more). This can result in significant storage savings on CI servers.

* Easier to read/analyze - text logs (especially from multi-processor builds) can become unwieldy, intertwined and hard to read. The viewer allows to collapse unneeded sections, as well as intelligent search.

* Programmatic access - All information is easily accessible programmatically for tools such as analyzers (whereas you need to parse text logs yourself and it's hard without a clearly defined structure) A .NET API is available to load and query binary logs.

[Read more about the new MSBuild binary log format (*.binlog)](https://github.com/Microsoft/msbuild/wiki/Binary-Log)
