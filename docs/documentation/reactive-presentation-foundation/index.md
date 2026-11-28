---
Order: 25
---
# Reactive Presentation Foundation

Reactive Presentation Foundation (RPF) lets you run supported WPF views and controls on a portable desktop backend. To try your first window, open the Operations Dashboard. It contains working screens for data binding, commands, layout and document editing. [Dashboard source and launch guide](https://github.com/reactiveui/ReactivePresentationFoundation/tree/main/samples/OperationsDashboard)

## Run the dashboard

From the RPF checkout, build its SDK and runtime packages together. The sample resolves both from the local package feed. Use the repository's .NET 11 SDK and install the .NET 10 runtime for the XAML compiler. On Linux, start from a Wayland session. [Setup and platform prerequisites](https://github.com/reactiveui/ReactivePresentationFoundation/blob/main/docs/consumer/getting-started.md)

```sh
dotnet run tools/Build.cs -- pack
dotnet run --project samples/OperationsDashboard/OperationsDashboard.App -f net11.0 -p:RPFBackend=Portable
```

| Open in the dashboard | What to try |
| --- | --- |
| [Compiled bindings](https://github.com/reactiveui/ReactivePresentationFoundation/tree/main/samples/OperationsDashboard/CompiledBindings) | Edit a name, move focus and compare it with the editor that commits each keystroke. Replace the address to see a nested path update. This screen uses the portable backend. |
| [Collection views](https://github.com/reactiveui/ReactivePresentationFoundation/tree/main/samples/OperationsDashboard/CollectionViews) | Group work items by owner, move the current selection, then edit and cancel an item. |
| [Routed commands](https://github.com/reactiveui/ReactivePresentationFoundation/tree/main/samples/OperationsDashboard/RoutedCommand) | Change focus between command targets and watch which target handles the command. |
| [Rich report editor](https://github.com/reactiveui/ReactivePresentationFoundation/blob/main/samples/OperationsDashboard/OperationsDashboard.App/RichTextWindow.cs) | Save a snapshot, edit the report and load the snapshot. Choose **Inspect all formats** to see which representations the editor can read back. |

## Try an existing WPF project

Once your project can resolve the local packages, change its project SDK to `RPF.Sdk` and keep its `UseWPF` setting. The SDK-switch example supplies the before and after project files. On Windows, a Windows-targeted project selects WPF by default. Use `RPFBackend=Portable` to try the same sources with RPF. Check the status of the controls and operating systems your app needs before choosing a backend. [SDK-switch example](https://github.com/reactiveui/ReactivePresentationFoundation/tree/main/validation/WpfSdkSwitch) · [Platform support](https://github.com/reactiveui/ReactivePresentationFoundation/blob/main/docs/consumer/platforms-and-targeting.md)
