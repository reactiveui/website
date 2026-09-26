---
Order: 4
---
# Migration Guide: Xamarin to .NET MAUI

Xamarin.Forms support ended in May 2024. .NET MAUI is its successor: one project instead of one per platform, and
current .NET releases instead of .NET Standard. Microsoft's own
[Xamarin.Forms to MAUI migration guide](https://learn.microsoft.com/dotnet/maui/migration/) and the
[.NET Upgrade Assistant](https://dotnet.microsoft.com/platform/upgrade-assistant) cover the general project,
XAML and resource changes. This guide covers only the ReactiveUI-specific part: the package, the app's start-up
code, the view base classes and navigation.

## Prerequisites

- Visual Studio 2022 17.8 or later, or VS Code with the .NET MAUI extension.
- The .NET 8.0 SDK or later.
- The MAUI workload: `dotnet workload install maui`.

## Update the package reference

**Before**

```xml
<PackageReference Include="Xamarin.Forms" />
<PackageReference Include="Xamarin.Essentials" />
<PackageReference Include="ReactiveUI.XamForms" />
```

**After**

```xml
<PackageReference Include="Microsoft.Maui.Controls" Version="*" />
<PackageReference Include="ReactiveUI.Maui" Version="*" />
```

`ReactiveUI.Maui` brings `ReactiveUI` and MAUI's `ReactiveUI.Binding` platform package with it, so `WhenAnyValue`,
`Bind` and `ToProperty` are ready to use with no extra package.
[Migration Guide: Bindings on ReactiveUI.Binding](reactiveui-binding-migration.md) covers that package. Reference
`ReactiveUI.Maui.Reactive` instead when your app uses System.Reactive rather than ReactiveUI.Primitives.

## Start the app

Xamarin.Forms configured ReactiveUI in the `App` constructor. MAUI configures it in `MauiProgram.CreateMauiApp`,
through `MauiAppBuilder.UseReactiveUI`.

**Before**

```csharp
public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        Locator.CurrentMutable.RegisterLazySingleton<IScreen>(() => new MainViewModel());
        Locator.CurrentMutable.Register<IViewFor<MainViewModel>>(() => new MainPage());

        MainPage = new NavigationPage(new MainPage());
    }
}
```

**After**

```csharp
MauiAppBuilder mauiBuilder = MauiApp.CreateBuilder();

_ = mauiBuilder.UseReactiveUI(builder =>
{
    _ = builder.WithMauiConverters();
});
```

`UseReactiveUI` takes either a `Action<IReactiveUIBuilder>`, as above, or a MAUI `IDispatcher` and configures the
builder for you:

```csharp
MauiAppBuilder mauiBuilder = MauiApp.CreateBuilder();

MauiAppBuilder configured = mauiBuilder.UseReactiveUI(new ImmediateDispatcher());

Console.WriteLine(ReferenceEquals(mauiBuilder, configured));

// Output:
// True
```

A real app's delegate calls `WithMaui()`, which registers MAUI's converters, its platform module and its
scheduler in one call, then adds its own views, view models and services:

```csharp
IReactiveUIBuilder builder = RxAppBuilder.CreateReactiveUIBuilder()
    .WithMaui(new ImmediateDispatcher());

Console.WriteLine(builder is not null);

// Output:
// True
```

[Migration Guide: RxAppBuilder](rxappbuilder-migration.md) covers `WithRegistration`, `RegisterView` and the
rest of the builder's registration calls, and [RxAppBuilder](../handbook/rxappbuilder.md) is the full reference.
An `App.xaml.cs` no longer registers anything; it only sets `MainPage` or, on Shell apps, `AppShell`.

## Move the views

`ReactiveUI.Maui` replaces `ReactiveUI.XamForms`. Its base classes pair a `Microsoft.Maui.Controls` type with
`IViewFor<T>`, and every one exposes the same `ViewModel` property kept in sync with `BindingContext`:

| MAUI control | ReactiveUI base class |
|---|---|
| `ContentPage` | `ReactiveContentPage<TViewModel>` |
| `NavigationPage` | `ReactiveNavigationPage<TViewModel>` |
| `FlyoutPage` | `ReactiveFlyoutPage<TViewModel>`, or `ReactiveMasterDetailPage<TViewModel>` under its old name |
| `CarouselView` | `ReactiveCarouselView<TViewModel>` |
| `ContentView` | `ReactiveContentView<TViewModel>` |
| `Window` | `ReactiveWindow<TViewModel>` |
| `TitleBar` | `ReactiveTitleBar<TViewModel>` |
| `Shell` | `ReactiveShell<TViewModel>` |
| `Page` (older, superseded by `ReactiveContentPage<TViewModel>`) | `ReactivePage<TViewModel>` |

```csharp
RecipeListPage page = new();
RecipeListViewModel first = new(new RecipeBookScreen());
RecipeListViewModel second = new(new RecipeBookScreen());

page.ViewModel = first;
Console.WriteLine(ReferenceEquals(page.BindingContext, first));

page.BindingContext = second;
Console.WriteLine(ReferenceEquals(page.ViewModel, second));

Console.WriteLine(ReactiveContentPage<RecipeListViewModel>.ViewModelProperty.PropertyName);

// Output:
// True
// True
// ViewModel
```

The class declares `IViewFor<T>`, so ReactiveUI.Binding's view locator finds it at compile time; a MAUI app needs
no `RegisterViewsForViewModels` or `RegisterView<TView, TViewModel>()` call for a page like this one:

```csharp
public sealed class RecipeListPage : ReactiveContentPage<RecipeListViewModel>;
```

[Migration Guide: Bindings on ReactiveUI.Binding](reactiveui-binding-migration.md#view-location) covers that
generated lookup. Register a view in the service locator only when it needs constructor arguments, or when two
views serve the same view model under different contracts.

## View models change the least

A view model that derives from `ReactiveObject` needs no change: `RaiseAndSetIfChanged`, `WhenAnyValue`,
`ToProperty` and `ReactiveCommand` all work the same way on MAUI as they did on Xamarin.Forms.

```csharp
public sealed class Student : ReactiveObject
{
    private readonly List<int> _grades = [];

    public string Name
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = string.Empty;
```

```csharp
public RecipeListViewModel(IScreen hostScreen)
{
    HostScreen = hostScreen;
    Open = ReactiveCommand.CreateFromObservable<RecipeItem, IRoutableViewModel>(
        recipe => HostScreen.Router.Navigate.Execute(new RecipeDetailViewModel(HostScreen, recipe)));
}
```

`ReactiveUI.SourceGenerators` also still works, unchanged from Xamarin.Forms: its `[Reactive]` and
`[ReactiveCommand]` attributes write the property and command bodies for you.
[Source generators](../../source-generators/index.md) covers that package.

## Move navigation onto RoutingState

Xamarin.Forms code that called `Navigation.PushAsync`, `PopAsync` or `PushModalAsync` moves onto ReactiveUI's own
router: a `RoutingState` that holds a stack of view models, and a `RoutedViewHost` that keeps a MAUI
`NavigationPage` in step with it. Both types ship in `ReactiveUI` itself; no extra package is needed.

**Before**

```csharp
await Navigation.PushAsync(new DetailsPage());
await Navigation.PopAsync();
```

**After**

A page implements `IRoutableViewModel` and navigates through its `HostScreen`'s router:

```csharp
public RecipeListViewModel(IScreen hostScreen)
{
    HostScreen = hostScreen;
    Open = ReactiveCommand.CreateFromObservable<RecipeItem, IRoutableViewModel>(
        recipe => HostScreen.Router.Navigate.Execute(new RecipeDetailViewModel(HostScreen, recipe)));
}
```

`RoutedViewHost` needs a registered `IScreen` before it can find the router, and its `SyncAsync()` pushes the
router's stack onto the host's own `Navigation.NavigationStack`:

```csharp
RecipeBookScreen screen = new();
AppLocator.CurrentMutable.RegisterConstant<IScreen>(screen);
AppLocator.CurrentMutable.Register<IViewFor<RecipeListViewModel>>(static () => new RecipeListPage());

RecipeRoutedViewHost host = new();
screen.Router.NavigationStack.Add(new RecipeListViewModel(screen));

await host.SyncAsync();

Console.WriteLine(host.Navigation.NavigationStack.Count);
Console.WriteLine(host.Navigation.NavigationStack[0].GetType().Name);

// Output:
// 1
// RecipeListPage
```

[Routing](../handbook/routing.md) covers `RoutingState`, `NavigationStack`, `Navigate` and `NavigateBack` in
full, from a platform-independent example. Keep navigation on `RoutingState` rather than mixing it with MAUI
Shell's own `Routing.RegisterRoute` and `GoToAsync`; the two stacks do not know about each other.

## Replace MessagingCenter

Xamarin.Forms's `MessagingCenter` is gone in MAUI. ReactiveUI's `MessageBus` sends a typed message to every
subscriber, and works the same way it did on Xamarin.Forms:

**Before**

```csharp
MessagingCenter.Send(this, "Message", data);
MessagingCenter.Subscribe<T>(this, "Message", callback);
```

**After**

```csharp
IMessageBus original = MessageBus.Current;
MessageBus.Current = new MessageBus();

using IDisposable subscription = MessageBus.Current.Listen<Announcement>().Subscribe(static announcement => Console.WriteLine(announcement.Text));
MessageBus.Current.SendMessage(new Announcement("Assembly starts at 9am"));

MessageBus.Current = original;

// Output:
// Assembly starts at 9am
```

[Message bus](../handbook/message-bus.md) covers scoped instances, contracts and the delivery scheduler.

## Dependency injection replaces DependencyService

`DependencyService.Get<T>()` is gone from MAUI. Register a service in `MauiProgram` with
`builder.Services.AddSingleton<IMyService, MyService>()` and receive it through a constructor parameter, the
same as any other ASP.NET-style dependency injection. ReactiveUI's own `AppLocator` keeps working alongside it;
`RxAppBuilder` registers into whichever resolver you point it at, including one backed by
`Microsoft.Extensions.DependencyInjection`. [RxAppBuilder](../handbook/rxappbuilder.md#build-the-whole-app)
covers registration lifetimes.

## Testing needs no change

A view model test that builds a view model directly and drives its commands and properties keeps working
unchanged, since it never touched Xamarin.Forms in the first place. [Testing](../handbook/testing.md) covers
`TestScheduler` and the builder-based test fixture pattern for anything that reads `RxSchedulers` or `AppLocator`.

## Where to go next

- [Xamarin.Forms to MAUI migration](https://learn.microsoft.com/dotnet/maui/migration/)
- [RxAppBuilder](../handbook/rxappbuilder.md)
- [Routing](../handbook/routing.md)
- [Message bus](../handbook/message-bus.md)
