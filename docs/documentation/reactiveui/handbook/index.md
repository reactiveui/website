---
Order: 2
---
# ReactiveUI Handbook

The handbook teaches ReactiveUI one topic at a time. Every page starts from a runnable example project in the
[ReactiveUI repository](https://github.com/reactiveui/ReactiveUI/tree/main/src/examples/Documentation). Each code
block on a page is copied from that project, and the text under it shows what the code prints. To see a page's code
run, clone the repository and run its project:

```bash
dotnet run --project src/examples/Documentation/Pages/commands
```

The examples use small apps you already know: a to-do list, a school, a library, a shop, a recipe book and a music
player. The data is in memory, so nothing needs a server.

## Start here

Read these in order. Together they build a complete app.

| Page | What you learn |
| --- | --- |
| [Starting an app](rxappbuilder.md) | Start ReactiveUI with the app builder and choose its services. |
| [Registration](registration.md) | Register your own services, views and view models. |
| [View models](view-models/index.md) | Write a view model the view can bind to. |
| [Commands](commands/index.md) | Run an action from a button, with its state and errors. |
| [Data binding](data-binding/index.md) | Keep a view and its view model in step. |
| [WhenActivated](when-activated.md) | Start work when a view appears and stop it when the view goes away. |
| [Interactions](interactions/index.md) | Let a view model ask the view a question. |
| [Routing](routing.md) | Move between pages and go back. |
| [View location](view-location/index.md) | Find the view for a view model. |

## Topics

| Page | What you learn |
| --- | --- |
| [Collections](collections.md) | React to items added to and removed from a list. |
| [Message bus](message-bus.md) | Send messages between parts of an app that do not know each other. |
| [Scheduling](scheduling.md) | Choose which thread work runs on. |
| [Data persistence](data-persistence.md) | Save and restore the app's state when the operating system pauses or closes it. |
| [Default exception handler](default-exception-handler.md) | Decide what happens to an error nobody handles. |
| [Testing](testing.md) | Test view models without a UI. |
| [Platforms](platforms/index.md) | Use ReactiveUI with WPF, Windows Forms, WinUI, MAUI, Blazor and Android. |
| [Members that need reflection](reflection.md) | Which members do not work with trimming or Native AOT, and what to use instead. |

## Related projects

Each of these libraries has its own repository and its own section.

- [ReactiveUI.Binding](../../binding/index.md) runs ReactiveUI's bindings, property observation and view location.
- [ReactiveUI.SourceGenerators](../../source-generators/index.md) writes reactive properties and commands.
- [ReactiveUI.Validation](../../validation.md) adds validation rules to view models.
- [Sextant](../../sextant.md) navigates by view model.
- [ReactiveUI.Maui.Plugins.Popup](../../maui-plugins-popup.md) adds reactive popup pages to MAUI.
- [Splat](../../splat/index.md) provides [dependency injection](../../splat/dependency-injection/index.md) and
  [logging](../../splat/logging/index.md).
- [ReactiveUI.Primitives](../../primitives/index.md) provides the streams and operators, and
  [ObservableEvents](../../primitives/observable-events/index.md) turns .NET events into streams.
- [Akavache](../../akavache/index.md) caches data and settings on the device.

To move an existing app to this version, see [Upgrading](../upgrading/index.md).
