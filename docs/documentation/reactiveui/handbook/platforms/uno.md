---
Order: 9
---
# Uno Platform

Use `ReactiveUI.Uno` to connect a Uno window to ReactiveUI. SQLite Studio shows how to open a database, edit a query and save the editor text between launches. For an app that uses System.Reactive, the IoT Dashboard shows the same window setup with `ReactiveUI.Uno.Reactive`. [SQLite Studio](https://github.com/reactiveui/ReactiveUI.Uno/tree/main/src/Examples/ReactiveUI.Uno.SQLiteStudio) · [IoT Dashboard](https://github.com/reactiveui/ReactiveUI.Uno/tree/main/src/Examples/ReactiveUI.Uno.Reactive.IoTDashboard)

## Connect the window

Install the package for your app. In `OnLaunched`, create the main window and pass it to `WithUno` before assigning the initial view. Keep the window in a field so you can reactivate it on later launches. The builder registers the UI scheduler for this window, view activation, dependency-property observation and a driver for saved application state. [SQLite Studio startup](https://github.com/reactiveui/ReactiveUI.Uno/blob/main/src/Examples/ReactiveUI.Uno.SQLiteStudio/App.xaml.cs)

```csharp
_ = RxAppBuilder.CreateReactiveUIBuilder()
    .WithUno(window)
    .BuildApp();

window.Content = new MainView { ViewModel = viewModel };
window.Activate();
```

## Save and restore an editor draft

In SQLite Studio, enter a query and choose **Save draft** from the command bar menu. Change the text, then choose **Restore draft**. The saved text replaces the editor contents. **Forget draft** removes it from storage. [Query commands](https://github.com/reactiveui/ReactiveUI.Uno/blob/main/src/Examples/ReactiveUI.Uno.SQLiteStudio/Presentation/MainViewModel.cs)

Give `WinRTAppDataDriver` a generated JSON contract. The example uses the contract for a string. For a state object with several fields, declare that type on your serializer context and pass its generated property instead.

```csharp
using System.Text.Json.Serialization;
using ReactiveUI.Primitives.Signals;
using ReactiveUI.Uno;

[JsonSerializable(typeof(string))]
internal sealed partial class QueryDraftJsonContext : JsonSerializerContext;
```

```csharp
var driver = new WinRTAppDataDriver();
var contract = QueryDraftJsonContext.Default.String;
await Signal.ToTask(driver.SaveState(queryText, contract), cancellationToken);
var draft = await Signal.ToTask(driver.LoadState(contract), cancellationToken);
```

The driver stores JSON in `appData.json` in the app's roaming folder. Handle `FileNotFoundException` on a first launch or after `InvalidateState()`. For an app that must read an older DataContract XML file, `ReactiveUI.Uno.Unsafe` and `ReactiveUI.Uno.Unsafe.Reactive` supply `WinRTAppDataDriverUnsafe`. [Storage driver](https://github.com/reactiveui/ReactiveUI.Uno/blob/main/src/ReactiveUI.Uno/WinRTAppDataDriver.cs) · [XML compatibility driver](https://github.com/reactiveui/ReactiveUI.Uno/blob/main/src/ReactiveUI.Uno.Unsafe/WinRTAppDataDriverUnsafe.cs)
