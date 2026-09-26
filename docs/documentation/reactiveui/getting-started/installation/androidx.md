---
Order: 9
---
# AndroidX (.NET for Android)

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/platform-android/platform-android.csproj).

AndroidX support targets a native .NET for Android application. If you want a cross-platform UI stack instead,
[.NET MAUI](maui.md) also has full ReactiveUI support.

Add `ReactiveUI.AndroidX` to your .NET for Android application project. It brings `ReactiveUI` and, through it,
[ReactiveUI.Binding](../../../binding/index.md).

```bash
dotnet add package ReactiveUI.AndroidX
```

`ReactiveUI.AndroidX` also ships as `ReactiveUI.AndroidX.Reactive`, built from the same source, for an app that
uses System.Reactive.

## Configure ReactiveUI at startup

Call `WithAndroidX` on the app builder once, in your `Application`'s `OnCreate`. `WithAndroidXScheduler` sets
only the main-thread sequencer, for a host that already registers platform services elsewhere.

```csharp
IReactiveUIBuilder builder = RxAppBuilder.CreateReactiveUIBuilder();
_ = builder.WithAndroidX().BuildApp();
```

`WithAndroidX` registers AndroidX's activation fetcher and its main-thread sequencer, backed by the main looper.
[RxAppBuilder](../../handbook/rxappbuilder.md) covers the builder itself.

## Bind an activity to its view model

`ReactiveAppCompatActivity<TViewModel>` gives an `Activity` a `ViewModel` property and runs `WhenActivated` for
you. `WireUpControls` wires every writable `View`-typed property to the like-named resource in the activity's
layout, so you skip `FindViewById` calls.

```csharp
public sealed class MainActivity : AndroidX.ReactiveAppCompatActivity<TimetableViewModel>
{
```

```csharp
    protected override async void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity_main);

        // Implicit strategy: every writable View-typed property (LessonsRecyclerView, BadgeContainer,
        // DetailContainer) is wired to the like-named resource in activity_main.xml.
        this.WireUpControls();
```

## Show a collection in a RecyclerView

`ReactiveRecyclerViewAdapter<TViewModel, TCollection>` binds an `ObservableCollection<TViewModel>` to a
`RecyclerView`, adding, removing and replacing rows as the collection changes. `ReactiveRecyclerViewViewHolder<TViewModel>`
is the view holder each row uses; it has a `ViewModel` property and its own `WireUpControls`.

```csharp
public sealed class LessonsRecyclerAdapter(ObservableCollection<LessonViewModel> lessons) : ReactiveRecyclerViewAdapter<LessonViewModel, ObservableCollection<LessonViewModel>>(lessons)
{
```

```csharp
        // Implicit strategy: any writable View-typed property (SubjectLabel, RoomLabel) is wired to the
        // like-named resource in the inflated row layout.
        this.WireUpControls();
```

A view holder's `Selected` and `SelectedWithViewModel` streams deliver when its row is tapped, and `Activated`
and `Deactivated` deliver as the row attaches to and detaches from the window.

[Android](../../handbook/platforms/android.md) covers the rest of the package: activities, fragments, lists, view
hosts, service binding and saving state. [Routing](../../handbook/routing.md) covers navigating with a router, and
[RxAppBuilder](../../handbook/rxappbuilder.md) covers the builder in depth. There is no `ReactiveUI.Validation` code in this installation's example project; see
[Validation](../../../validation.md) for validation rules on a view model. The [compelling example](../compelling-example.md)
walks through a first view model and view on a platform-neutral console app.
