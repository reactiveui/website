---
Order: 7
---
# Android

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/platform-android/platform-android.csproj).

A .NET for Android app builds its screens from `Activity`, `Fragment` and `View` subclasses, plus AndroidX
classes such as `AppCompatActivity`, `RecyclerView` and `ViewPager`. `ReactiveUI.AndroidX` connects those to a
view model. Most of the surface lives in the core `ReactiveUI` package, under the same names. A classic
Activity/Fragment app already needs it. `ReactiveUI.AndroidX` adds the AppCompat and Jetpack-flavored base
classes on top. Together the two packages add:

- base classes for activities, fragments, dialogs and preference screens, so each one is an `IViewFor<TViewModel>`
  with a `ViewModel` property;
- `WireUpControls`, which finds every control in your layout by naming convention instead of a `FindViewById`
  call per field ([Wire up controls](../data-binding/android/wire-up-controls.md) covers it);
- adapters that show a collection of view models in a `RecyclerView` or page through them in a `ViewPager`;
- an activation signal tied to the Android lifecycle, for [`WhenActivated`](../when-activated.md);
- a main-thread sequencer pointed at the app's `Looper`, plus a `With<Platform>()` call for the
  [app builder](../rxappbuilder.md) that registers all of the above;
- a helper that saves state across process death;
- a helper that binds to a service as a stream instead of a `ServiceConnection` callback.

The package also ships as `ReactiveUI.AndroidX.Reactive`, built from the same source for apps that use
System.Reactive. For the packages to reference, see [Installation](../../getting-started/installation/androidx.md).

The example below is a school timetable app. Its main screen lists the week's lessons in a `RecyclerView` and
features one lesson in a compound view. It also shows a running badge and two peeks of the next lessons.
Tapping through reports an absence to a bound service and shows a lesson's detail in a fragment. It then
confirms the absence in a dialog, pages through the weekdays, and opens a notification-settings screen. Running
it on an emulator prints the lines quoted through this page under the `RxDocs` logcat tag.

## Namespace note

`ReactiveUI.AndroidX` mirrors several core `ReactiveUI` names: `ReactiveUI.ReactiveActivity<TViewModel>` sits
next to `ReactiveUI.AndroidX.ReactiveAppCompatActivity<TViewModel>`, and both namespaces declare a
`ControlFetcherMixins`. The example's own namespace, `ReactiveUI.Documentation.PlatformAndroid`, starts with
`ReactiveUI`, so C# finds `AndroidX` as a nested namespace of the enclosing `ReactiveUI` namespace before it
looks anywhere else. That lets every file on this page write `AndroidX.ReactiveAppCompatActivity<TimetableViewModel>`,
`AndroidX.ReactiveFragment<LessonViewModel>` and so on, without spelling out `ReactiveUI.AndroidX.` in full. Do
the same in your own app once its namespace starts with `ReactiveUI`: it disambiguates the AndroidX class from
the core one of the same name, and it marks the AndroidX package at the call site.

## Start ReactiveUI for AndroidX

**1. Register the platform module.** `WithAndroidX` on the [app builder](../rxappbuilder.md) registers
`ReactiveUI.AndroidX.Registrations` and points ReactiveUI's main-thread sequencer at the app's `Looper` through
`HandlerSequencer`. Call this once, in your `Application` subclass's `OnCreate`, before any activity runs.

```csharp
IReactiveUIBuilder builder = RxAppBuilder.CreateReactiveUIBuilder();
_ = builder.WithAndroidX().BuildApp();

TimetableLog.Info($"ReactiveUI is running on {AndroidXReactiveUIBuilderExtensions.AndroidXMainThreadScheduler.GetType().Name}.");
```

```text
ReactiveUI is running on HandlerSequencer.
```

`AndroidXReactiveUIBuilderExtensions.AndroidXMainThreadScheduler` is the sequencer both calls point
`RxSchedulers.MainThreadScheduler` at. `WithAndroidXScheduler` sets only that sequencer, for a host that
registers the rest of the platform module another way, such as a shared multi-platform composition root.

```csharp
IReactiveUIBuilder schedulerOnlyBuilder = RxAppBuilder.CreateReactiveUIBuilder().WithAndroidXScheduler();
TimetableLog.Info($"WithAndroidXScheduler configured a {schedulerOnlyBuilder.GetType().Name} to run on "
    + $"{AndroidXReactiveUIBuilderExtensions.AndroidXMainThreadScheduler.GetType().Name}.");
```

**2. Wire up automatic state suspension.** `AutoSuspendHelper` translates activity lifecycle callbacks into the
signals [suspension](../data-persistence.md) needs, and `BundleSuspensionDriver` saves and loads state from the
activity bundle those callbacks carry. [Save and restore state](#save-and-restore-state-across-process-death)
covers both below.

```csharp
_autoSuspendHelper = new AutoSuspendHelper(this);
RxSuspension.SuspensionHost.CreateNewAppState = static () => new TimetableAppState();

BundleSuspensionDriver driver = new();
RxSuspension.SuspensionHost.SetupDefaultSuspendResume(driver);

TimetableLog.Info("TimetableApplication started; AutoSuspendHelper and BundleSuspensionDriver are wired up.");
```

```text
TimetableApplication started; AutoSuspendHelper and BundleSuspensionDriver are wired up.
```

A reader who stops here can already start ReactiveUI. The rest of this page gives an activity a view model,
wires its controls, and covers the platform's other Android-specific pieces.

## Give an activity a view model

`ReactiveUI.AndroidX.ReactiveAppCompatActivity<TViewModel>` is an `AppCompatActivity` that already implements
`IViewFor<TViewModel>`. `MainActivity`, the timetable's home screen, derives from it and assigns `ViewModel`
after wiring its controls.

```csharp
public sealed class MainActivity : AndroidX.ReactiveAppCompatActivity<TimetableViewModel>
```

```csharp
this.WireUpControls();

PropertyInfo[] wiredMembers = this.GetWireUpMembers(ControlFetcherMixins.ResolveStrategy.Implicit);
TimetableLog.Info($"WireUpControls found {wiredMembers.Length} members to wire.");
```

```text
WireUpControls found 5 members to wire.
```

`WireUpControls` finds every property that is a `View` subtype and assigns it to the like-named resource in the
activity's layout, instead of a `FindViewById` call per field. [Wire up controls](../data-binding/android/wire-up-controls.md)
covers the naming policy and the two other resolve strategies in depth.

`AbsenceActivity`, the screen that records an absence, derives instead from the plain, non-AppCompat
`ReactiveUI.ReactiveActivity<TViewModel>`. Classic Activity support without AppCompat theming stays available
directly under `ReactiveUI`, for a screen that does not need it.

```csharp
public sealed class AbsenceActivity : ReactiveActivity<AbsenceViewModel>
```

`ReactiveUI.AndroidX.ReactiveFragment<TViewModel>`, `ReactiveDialogFragment<TViewModel>`,
`ReactivePreferenceFragment<TViewModel>` and `ReactiveFragmentActivity<TViewModel>` follow the same pattern for
a fragment, a modal dialog, a preference screen and a fragment-hosting activity that does not need full
AppCompat theming. Every one of them, and both `ReactiveActivity<TViewModel>` variants, exposes the same
`ViewModel` property plus the `IViewFor.ViewModel` explicit implementation for a caller that only knows the
view model as `object`.

## Detect activation

Every reactive Android base class on this page raises the same activation signal, through the constructor an
`ActivationForViewFetcher` registers for the type. `Activated` fires when the view appears; `Deactivated` fires
when it leaves. [When activated](../when-activated.md) covers building on top of these with `WhenActivated`.
`MainActivity` and `WeekdayPagerActivity` both log directly from the raw signals; `AbsenceConfirmationDialogFragment`
does the same from its constructor, since a dialog fragment has no `OnCreate` override to hook in `OnCreate`, and
`LessonDetailFragment` does it from `OnCreateView`.

```csharp
_subscriptions.Add(Activated.Subscribe(static _ => TimetableLog.Info("MainActivity activated.")));
_subscriptions.Add(Deactivated.Subscribe(static _ => TimetableLog.Info("MainActivity deactivated.")));
```

```text
MainActivity activated.
```

```csharp
_subscriptions.Add(Activated.Subscribe(static _ => TimetableLog.Info("AbsenceConfirmationDialogFragment activated.")));
_subscriptions.Add(Deactivated.Subscribe(static _ => TimetableLog.Info("AbsenceConfirmationDialogFragment deactivated.")));
```

```text
AbsenceConfirmationDialogFragment activated.
```

Every one of these types also carries the classic reactive object surface. `Changed` and `Changing` observe
property changes, and `PropertyChanged` and `PropertyChanging` are the .NET events behind them.
`ThrownExceptions` reports errors raised inside reactive operators. `SuppressChangeNotifications()` pauses
change notifications until its result is disposed. `AbsenceActivity` subscribes to `ThrownExceptions` and
suppresses notifications while it fills in a freshly-created view model from an intent extra:

```csharp
_ = ThrownExceptions.Subscribe(static error => TimetableLog.Info($"AbsenceActivity reported an exception: {error.Message}"));
```

```csharp
using (SuppressChangeNotifications())
{
    string subject = Intent?.GetStringExtra(SubjectExtra) ?? "Unknown lesson";
    ViewModel = new AbsenceViewModel { Subject = subject };
}
```

## Move between activities and read the result

`StartActivityForResultAsync` replaces the classic `StartActivityForResult` plus an `OnActivityResult` override
with a `Task` your calling code can `await`. It comes in two overloads, one that takes a ready `Intent` and one
that takes the target `Type` and builds the `Intent` itself. `MainActivity` uses both to report an absence, then
awaits the weekday pager the same way:

```csharp
Intent absenceIntent = new(this, typeof(AbsenceActivity));
absenceIntent.PutExtra(AbsenceActivity.SubjectExtra, firstLesson.Subject);
(Android.App.Result Result, Intent? Intent) absenceResult = await StartActivityForResultAsync(absenceIntent, 100);
TimetableLog.Info($"Absence report (by intent) finished with {absenceResult.Result}: "
    + $"{absenceResult.Intent?.GetStringExtra(AbsenceActivity.SubjectExtra)}.");
```

```csharp
LessonViewModel secondLesson = ViewModel.Lessons[1];
(Android.App.Result Result, Intent? Intent) secondAbsenceResult = await StartActivityForResultAsync(typeof(AbsenceActivity), 101);
TimetableLog.Info($"Absence report (by type) finished with {secondAbsenceResult.Result}.");
```

`ActivityResult` is an observable of every result the activity receives, alongside the `Task`-based calls above.
`MainActivity` and `WeekdayPagerActivity` both log from it directly:

```csharp
_subscriptions.Add(ActivityResult.Subscribe(static result =>
    TimetableLog.Info($"MainActivity received an activity result: {result.Result}.")));
```

```text
MainActivity received an activity result: Ok.
```

Only the activity base classes carry `ActivityResult` and `StartActivityForResultAsync`; the fragment and
dialog base classes do not navigate between activities.

## Host a view without an activity or fragment

`LayoutViewHost` inflates a layout into a `View` your code owns directly, without an `Activity` or `Fragment`
around it. `ReactiveViewHost<TViewModel>` adds a `ViewModel` property on top, for a host that follows a view
model. Both classes wire their children by hand, with no reflection, so a host stays safe to trim and to compile
ahead of time. Each has an Unsafe twin that adds the reflection-based auto-wireup constructor instead:

| AOT-safe type | Unsafe twin | What the twin adds |
| --- | --- | --- |
| `LayoutViewHost` | `LayoutViewHostUnsafe` | A constructor that takes `performAutoWireup` and `resolveStrategy`, and wires each property to the control with the matching resource ID. |
| `ReactiveViewHost<TViewModel>` | `ReactiveViewHostUnsafe<TViewModel>` | The same constructor. It also fills `AllPublicProperties` for older code that reads it. |

Auto-wireup finds the host's properties and the app's resource IDs by reflection, which the trimmer cannot follow.
So both twins are marked `[RequiresUnreferencedCode]` and `[RequiresDynamicCode]`. Prefer the `bind` callback shown
next.

`WeekdayViewHost`, one page of the weekday pager, wires its label through the `bind` callback constructor. It
passes `bind: static (host, view) => ((WeekdayViewHost)host)._weekdayLabel = view.FindViewById<TextView>(Resource.Id.weekdayLabel)`
to the base constructor, which runs that callback once the inflated view is assigned and needs no reflection.
The constructor body then subscribes to the host's own `ViewModel`:

```csharp
{
    this.WhenAnyValue(host => host.ViewModel).Subscribe(weekday =>
        _weekdayLabel!.Text = weekday is null ? null : $"{weekday.Name} ({weekday.LessonCount} lessons)");
}
```

`LessonPeekHost`, the one-line lesson preview next to the badge on the main screen, instead reads `View` itself
once the base constructor returns, through the plain 3- and 4-argument constructors. The only difference
between them is `attachToRoot`: the 3-argument form defaults it to `false`, so the caller decides where to add
the inflated view, while the 4-argument form lets the caller inflate straight into the parent. Both bodies read
the label back the same way:

```csharp
{
    _subjectLabel = View!.FindViewById<TextView>(Resource.Id.peekSubjectLabel)!;
    Hook();
}
```

`MainActivity` uses both constructors: it calls `AddView` itself for the second lesson's peek, and lets the third
lesson's peek inflate directly into its row.

```csharp
LessonPeekHost secondLessonPeek = new(this, PeeksRow!) { ViewModel = ViewModel.Lessons[1] };
PeeksRow!.AddView(secondLessonPeek.View);

_ = new LessonPeekHost(this, PeeksRow!, attachToRoot: true) { ViewModel = ViewModel.Lessons[2] };
```

The first lesson's peek, `FirstLessonPeekHost`, derives from `ReactiveViewHostUnsafe<LessonViewModel>` instead. Its
constructor passes `performAutoWireup: true`, so auto-wireup fills its `PeekSubjectLabel` property from the
`peekSubjectLabel` resource ID, with no `FindViewById` call:

```csharp
public sealed class FirstLessonPeekHost : ReactiveViewHostUnsafe<LessonViewModel>
{
    public FirstLessonPeekHost(Context context, ViewGroup parent)
        : base(
            context,
            Resource.Layout.lesson_peek,
            parent,
            attachToRoot: false,
            performAutoWireup: true,
            resolveStrategy: ControlFetcherMixins.ResolveStrategy.Implicit) =>
        this.WhenAnyValue(host => host.ViewModel)
            .Subscribe(lesson => PeekSubjectLabel!.Text = lesson is null ? null : $"First: {lesson.Subject}");

    public TextView? PeekSubjectLabel { get; set; }
}
```

The class carries `[RequiresUnreferencedCode]` and `[RequiresDynamicCode]` itself, so the warning moves to the code
that creates it. `MainActivity` adds it to the row like any other host:

```csharp
// Auto-wireup fills PeekSubjectLabel by name, so this peek's constructor needs no FindViewById call.
FirstLessonPeekHost firstLessonPeek = new(this, PeeksRow!) { ViewModel = ViewModel.Lessons[0] };
PeeksRow!.AddView(firstLessonPeek.View);
TimetableLog.Info($"Auto-wired peek shows: {firstLessonPeek.PeekSubjectLabel?.Text}.");
```

```text
Auto-wired peek shows: First: Mathematics.
```

`LessonCountBadgeHost`, the lesson-count badge, has no view model, since it only renders a count once. It derives
from `LayoutViewHostUnsafe` and passes `attachToRoot: false`, `performAutoWireup: true` and
`resolveStrategy: ControlFetcherMixins.ResolveStrategy.Implicit` to the base constructor, wiring `BadgeText` by name
the same way. Its own body adds nothing further.

`ToView()` returns the host's backing `View`, ready to add to a layout:

```csharp
public View ToBadgeView() => ToView()!;
```

`ViewMixins.GetViewHost` and `GetViewHost<T>` read the host tagged onto a `View` back off it. The weekday pager
tags each page this way as it creates it, then reads the tag straight back to show both overloads:

```csharp
WeekdayViewHost host = new(this, parent);
View view = host.ToView()!;

WeekdayViewHost? typedHost = view.GetViewHost<WeekdayViewHost>();
ILayoutViewHost? untypedHost = view.GetViewHost();
TimetableLog.Info($"Weekday page tagged: typed={typedHost is not null}, untyped={untypedHost is not null}.");
```

## Show a list with RecyclerView

`ReactiveRecyclerViewAdapter<TViewModel, TCollection>` adapts a collection of view models to a `RecyclerView`.
`LessonsRecyclerAdapter` binds the timetable's lessons this way, and only has to implement
`OnCreateViewHolder`:

```csharp
public sealed class LessonsRecyclerAdapter(ObservableCollection<LessonViewModel> lessons) : ReactiveRecyclerViewAdapter<LessonViewModel, ObservableCollection<LessonViewModel>>(lessons)
{
    /// <inheritdoc/>
    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
        ArgumentNullException.ThrowIfNull(parent);

        LayoutInflater inflater = LayoutInflater.From(parent.Context)
            ?? throw new InvalidOperationException("No LayoutInflater is available for this parent.");
        View itemView = inflater.Inflate(Resource.Layout.lesson_item, parent, false)
            ?? throw new InvalidOperationException("Inflating lesson_item produced no view.");

        return new LessonViewHolder(itemView);
    }
}
```

`ReactiveRecyclerViewAdapter<TViewModel>` is the lower-level, single-generic form: it takes an
`IObservable<IReactiveChangeSet<TViewModel>>` directly instead of a collection, for pages built from a query or
a filter rather than a plain list. `LessonViewHolder`, the row this adapter creates, derives from
`ReactiveRecyclerViewViewHolder<TViewModel>`, wires its own labels, and reports selection and attachment. Its
constructor passes `itemView` straight to the base constructor, then does the rest of its work in its body:

```csharp
{
    // Implicit strategy: any writable View-typed property (SubjectLabel, RoomLabel) is wired to the
    // like-named resource in the inflated row layout.
    this.WireUpControls();

    _subscriptions.Add(this.WhenAnyValue(holder => holder.ViewModel).Subscribe(lesson =>
    {
        SubjectLabel!.Text = lesson?.Subject;
        RoomLabel!.Text = lesson is null ? null : $"Room {lesson.Room}";
    }));

    _subscriptions.Add(Selected.Subscribe(static position => TimetableLog.Info($"Lesson row {position} tapped.")));
    _subscriptions.Add(SelectedWithViewModel.Subscribe(static lesson => TimetableLog.Info($"Selected lesson: {lesson?.Subject}.")));
    _subscriptions.Add(LongClicked.Subscribe(static position => TimetableLog.Info($"Lesson row {position} long-clicked.")));
    _subscriptions.Add(LongClickedWithViewModel.Subscribe(static lesson => TimetableLog.Info($"Long-clicked lesson: {lesson?.Subject}.")));
    _subscriptions.Add(Activated.Subscribe(static _ => TimetableLog.Info("Lesson row attached to the window.")));
    _subscriptions.Add(Deactivated.Subscribe(static _ => TimetableLog.Info("Lesson row detached from the window.")));
}
```

`Selected` reports the row's adapter position when it is tapped; `SelectedWithViewModel` reports the view model
it holds at that moment instead. `LongClicked` and `LongClickedWithViewModel` report the same two shapes for a
long press. `AreChangeNotificationsEnabled()`, `View` and `AllPublicProperties` round out the holder's surface:
the first mirrors `SuppressChangeNotifications`, `View` is the inherited `LayoutViewHost.View`, and
`AllPublicProperties` lists the holder's public properties for reflection-based wiring. The holder's
`[DynamicallyAccessedMembers]` annotation tells the trimmer to keep those properties, so it stays safe to trim.

## Page through view models

`ReactiveUI.AndroidX.ReactivePagerAdapter<TViewModel, TCollection>` pages a collection of view models through a
`ViewPager`, one page per view model. `WeekdayPagerActivity` builds one over the timetable's weekday summaries:

```csharp
AndroidX.ReactivePagerAdapter<WeekdayViewModel, ObservableCollection<WeekdayViewModel>> adapter = new(
    weekdays,
    CreatePage,
    static (weekday, _) => TimetableLog.Info($"Weekday page created for {weekday.Name}."));
```

```text
Weekday page created for Monday.
```

`CreatePage` returns one inflated `WeekdayViewHost` per weekday; the pager assigns its `ViewModel` once the page
is placed. `pager.Adapter = adapter` installs it, and `Count` reports how many pages it holds:

```csharp
pager.Adapter = adapter;
pager.PageSelected += (_, e) =>
{
    WeekdayViewModel weekday = weekdays[e.Position];
    ViewModel!.CurrentWeekday = weekday.Name;
    TimetableLog.Info($"Weekday pager moved to {weekday.Name}.");
};

TimetableLog.Info($"Weekday pager adapter reports {adapter.Count} pages.");
```

```text
Weekday pager adapter reports 5 pages.
Weekday pager moved to Tuesday.
```

`ReactivePagerAdapter<TViewModel>` is the single-generic form: it pages any
`IObservable<IReactiveChangeSet<TViewModel>>`, the same change-set stream `ToReactiveChangeSet()` produces from
a collection, rather than requiring the collection itself.

```csharp
using (AndroidX.ReactivePagerAdapter<WeekdayViewModel> fromChangeSet = new(weekdays.ToReactiveChangeSet(), CreatePage))
{
    TimetableLog.Info($"Change-set-backed pager adapter also reports {fromChangeSet.Count} pages.");
}
```

## Show a dialog

`ReactiveUI.AndroidX.ReactiveDialogFragment<TViewModel>` is a `DialogFragment` with a `ViewModel` property.
`AbsenceConfirmationDialogFragment` reads its view model in `OnCreateDialog` to build the alert:

```csharp
public override Dialog OnCreateDialog(Bundle? savedInstanceState)
{
    string subject = ViewModel?.Subject ?? "this lesson";

    return new global::AndroidX.AppCompat.App.AlertDialog.Builder(RequireContext()!)
        .SetTitle("Confirm absence")!
        .SetMessage($"Mark the student absent from {subject}?")!
        .SetPositiveButton("Confirm", (_, _) => TimetableLog.Info($"Absence confirmed for {subject}."))!
        .SetNegativeButton("Cancel", static (_, _) => TimetableLog.Info("Absence confirmation cancelled."))!
        .Create()!;
}
```

`MainActivity` shows it modally by tag, the way any `DialogFragment` is shown, and dismisses it after a short
wait:

```csharp
const string ConfirmationTag = "confirm-absence";
new AbsenceConfirmationDialogFragment { ViewModel = secondLesson }.Show(SupportFragmentManager, ConfirmationTag);
await Task.Delay(TimeSpan.FromMilliseconds(200));
(SupportFragmentManager!.FindFragmentByTag(ConfirmationTag) as AbsenceConfirmationDialogFragment)?.Dismiss();
```

## Read shared preferences

`ReactiveUI.AndroidX.ReactivePreferenceFragment<TViewModel>` is a `PreferenceFragmentCompat` with a `ViewModel`
property. `SettingsPreferenceFragment` assigns its view model and loads the preference screen from a resource,
the same way a plain `PreferenceFragmentCompat` does:

```csharp
public override void OnCreatePreferences(Bundle? savedInstanceState, string? rootKey)
{
    ViewModel = new SettingsViewModel();
    SetPreferencesFromResource(Resource.Xml.settings_preferences, rootKey);
```

`SharedPreferencesExtensions.PreferenceChanged()` turns `ISharedPreferences`'s change listener into a stream of
the changed key, instead of a listener object your code registers and unregisters by hand:

```csharp
ISharedPreferences? sharedPreferences = PreferenceManager?.SharedPreferences;
if (sharedPreferences is null)
{
    return;
}

_preferenceChangedSubscription = sharedPreferences.PreferenceChanged()
    .Subscribe(static key => TimetableLog.Info($"Preference changed: {key}."));
```

`MainActivity` shows the settings screen the same way it showed the lesson detail fragment, by replacing the
fragment in `DetailContainer`:

```csharp
SettingsPreferenceFragment settingsFragment = new();
SupportFragmentManager!.BeginTransaction()!
    .Replace(DetailContainer!.Id, settingsFragment)!
    .CommitNowAllowingStateLoss();
TimetableLog.Info("Showing notification settings.");
```

```text
Showing notification settings.
```

## Bind to a service as a stream

`ContextExtensions.ServiceBound` binds an Android service and exposes its binder as a stream, instead of a
`ServiceConnection` your code implements and unregisters. The generic overload casts the binder to
`TBinder`; the non-generic overload exposes the raw `IBinder`. Both come in a `Bind`-flags form and a form that
defaults to `Bind.None`, which only connects to a service that is already running rather than starting one.

`AbsenceActivity` binds `AttendanceTrackerService` with `Bind.AutoCreate` first, since the service is not
already running and needs to be started:

```csharp
Intent attendanceIntent = new(this, typeof(AttendanceTrackerService));
_serviceSubscriptions.Add(this.ServiceBound<AttendanceTrackerService.AttendanceBinder>(attendanceIntent, Bind.AutoCreate)
    .Subscribe(
        binder =>
        {
            if (binder is null)
            {
                return;
            }

            binder.Service.RecordAbsence(ViewModel!.Subject);
            ViewModel.Reported = true;
            StatusLabel!.Text = $"Reported: {ViewModel.Subject}.";
            AbsenceStatusView!.SetReported(true);
        },
        static error => TimetableLog.Info($"Attendance service binding failed: {error.Message}")));
```

```text
AttendanceTrackerService recorded an absence from Mathematics.
```

Once that bind has had a moment to start the service, the two `Bind.None` overloads can connect to it, because
it is now already running:

```csharp
_serviceSubscriptions.Add(this.ServiceBound<AttendanceTrackerService.AttendanceBinder>(attendanceIntent)
    .Subscribe(static binder => TimetableLog.Info($"Generic (Context,Intent) ServiceBound connected: {binder is not null}.")));
_serviceSubscriptions.Add(this.ServiceBound(attendanceIntent)
    .Subscribe(static binder => TimetableLog.Info($"Non-generic (Context,Intent) ServiceBound connected: {binder is not null}.")));
```

A second, unrelated service, `SchoolBellService`, is bound with an explicit `Bind` flag through the non-generic,
raw-`IBinder` overload:

```csharp
Intent bellIntent = new(this, typeof(SchoolBellService));
_serviceSubscriptions.Add(this.ServiceBound(bellIntent, Bind.AutoCreate)
    .Subscribe(static binder => TimetableLog.Info($"SchoolBellService bound via (Context,Intent,Bind): {binder is not null}.")));
```

Both services never leave the app's own process, so binding to either always succeeds without a system
permission prompt.

## Save and restore state across process death

`AutoSuspendHelper` and `BundleSuspensionDriver` were wired up in [Start ReactiveUI for AndroidX](#start-reactiveui-for-androidx).
`AutoSuspendHelper` watches every activity's lifecycle callbacks and keeps the static `LatestBundle` current, so
`BundleSuspensionDriver` always has the bundle Android most recently handed to `OnSaveInstanceState`. Reading it
tells a cold launch apart from a process recreation:

```csharp
TimetableLog.Info($"AutoSuspendHelper's latest bundle is currently {(AutoSuspendHelper.LatestBundle is null ? "unset" : "set")}.");
```

```text
AutoSuspendHelper's latest bundle is currently unset.
```

`SaveState<T>(T, JsonTypeInfo<T>)` and `LoadState<T>(JsonTypeInfo<T>)` are the trim- and AOT-safe overloads,
through a source-generated `JsonTypeInfo<T>`. `TimetableApplication` builds one for `TimetableAppState` and uses
both, plus `InvalidateState` to clear the saved bundle:

```csharp
_ = driver.SaveState(new TimetableAppState { LastViewedSubject = "Mathematics" }, TimetableAppStateJsonContext.Default.TimetableAppState)
    .Subscribe(static _ => TimetableLog.Info("BundleSuspensionDriver saved the app state."));
_ = driver.LoadState(TimetableAppStateJsonContext.Default.TimetableAppState)
    .Subscribe(
        static state => TimetableLog.Info($"BundleSuspensionDriver loaded state for {state?.LastViewedSubject}."),
        static error => TimetableLog.Info($"BundleSuspensionDriver had nothing to load yet: {error.Message}"));
_ = driver.InvalidateState().Subscribe(static _ => TimetableLog.Info("BundleSuspensionDriver invalidated the saved state."));
```

```text
BundleSuspensionDriver saved the app state.
BundleSuspensionDriver had nothing to load yet: New bundle detected; no persisted state is available.
BundleSuspensionDriver invalidated the saved state.
```

`LoadState` fails here because this call runs on the same cold launch that just saved the state: `AutoSuspendHelper`
hasn't seen an `OnSaveInstanceState` callback yet, so `LatestBundle` is still the one from before `SaveState` ran.
A real app calls `LoadState`/`LoadState<T>` once, after a process recreation, to read back what an earlier process
saved. `LoadState()` and `SaveState<T>(T)` are the untyped overloads: they serialize through reflection instead of
a source-generated `JsonTypeInfo<T>`, so a page project built for trimming and AOT never calls them directly.

`UntimelyDemise` is a signal that fires from an unhandled exception. `SetupDefaultSuspendResume` treats that
signal as a reason to invalidate the saved state rather than trust it on the next launch; `TimetableApplication`
also logs from it directly:

```csharp
_ = AutoSuspendHelper.UntimelyDemise.Subscribe(static _ => TimetableLog.Info("AutoSuspendHelper reported an untimely demise."));
```

Nothing in this app's own scenario throws unhandled, so this line only appears in `adb logcat` if something else
does.

## Read the device orientation

`PlatformOperations` answers the same `GetOrientation` question every platform's `IPlatformOperations` answers.
On Android it reads the default display's current rotation from the display manager and returns the rotation's
name. `Rotation0` is the device's natural orientation, and `Rotation90`, `Rotation180` and `Rotation270` are the
turns away from it:

```csharp
PlatformOperations platformOperations = new();
string? orientation = platformOperations.GetOrientation();
TimetableLog.Info($"Device orientation: {orientation}.");
```

```text
Device orientation: Rotation0.
```

## USB permission requests

`UsbManagerExtensions.PermissionRequested` turns a USB permission dialog into a stream of the granted result,
for either a `UsbDevice` or a `UsbAccessory`, instead of a `BroadcastReceiver` your code registers and
unregisters. It needs a physical or emulated USB device attached, so this example does not exercise it.

## At a glance

| Member | What it does |
| --- | --- |
| `AndroidXReactiveUIBuilderExtensions.WithAndroidX()` | Registers the AndroidX platform module and its main-thread sequencer |
| `AndroidXReactiveUIBuilderExtensions.WithAndroidXScheduler()` | Sets only the main-thread sequencer |
| `AndroidXReactiveUIBuilderExtensions.AndroidXMainThreadScheduler` | The shared `HandlerSequencer` for the app's `Looper` |
| `ReactiveUI.AndroidX.Registrations` | The module `WithAndroidX` loads: core Android platform registrations, plus a `Looper` for the app if needed |
| `ReactiveActivity` / `ReactiveActivity<TViewModel>` | A classic `Activity` that is an `IViewFor<TViewModel>`, without AppCompat |
| `AndroidX.ReactiveAppCompatActivity` / `ReactiveAppCompatActivity<TViewModel>` | An `AppCompatActivity` that is an `IViewFor<TViewModel>` |
| `AndroidX.ReactiveFragmentActivity` / `ReactiveFragmentActivity<TViewModel>` | A fragment-hosting activity that is an `IViewFor<TViewModel>`, without full AppCompat theming |
| `AndroidX.ReactiveFragment` / `ReactiveFragment<TViewModel>` | A `Fragment` that is an `IViewFor<TViewModel>` |
| `AndroidX.ReactiveDialogFragment` / `ReactiveDialogFragment<TViewModel>` | A `DialogFragment` that is an `IViewFor<TViewModel>` |
| `AndroidX.ReactivePreferenceFragment` / `ReactivePreferenceFragment<TViewModel>` | A `PreferenceFragmentCompat` that is an `IViewFor<TViewModel>` |
| `Activated` / `Deactivated` | Fire when the view appears and leaves; feed [`WhenActivated`](../when-activated.md) |
| `Changed` / `Changing` / `PropertyChanged` / `PropertyChanging` | Observe and raise property changes, as on any `ReactiveObject` |
| `ThrownExceptions` | Reports errors raised inside reactive operators |
| `SuppressChangeNotifications()` | Pauses change notifications until the result is disposed |
| `ActivityResult` | An observable of every activity result received |
| `StartActivityForResultAsync(Intent, int)` / `StartActivityForResultAsync(Type, int)` | Starts an activity and awaits its result as a `Task` |
| `ControlFetcherMixins.WireUpControls(...)` (both namespaces) | Finds controls in a layout by naming convention; see [Wire up controls](../data-binding/android/wire-up-controls.md) |
| `ControlFetcherMixins.GetControl(...)` | Fetches one control by resource name without a property |
| `ControlFetcherMixins.GetWireUpMembers(object, ResolveStrategy)` | Lists the properties a resolve strategy would wire |
| `ControlFetcherMixins.GetResourceName(PropertyInfo)` | Reads the resource name a property wires to |
| `ControlFetcherMixins.ResolveStrategy` | `Implicit`, `ExplicitOptIn` and `ExplicitOptOut`: which properties `WireUpControls` wires |
| `WireUpResourceAttribute` | Opts a property in under `ExplicitOptIn`, with an optional resource name override |
| `IgnoreResourceAttribute` | Opts a property out under `ExplicitOptOut` |
| `LayoutViewHost` | Inflates a layout into a `View` your code owns, wiring its children by hand with no reflection |
| `LayoutViewHostUnsafe` | A `LayoutViewHost` whose constructor can wire its children by reflection (auto-wireup) |
| `ReactiveViewHost<TViewModel>` | A `LayoutViewHost` that is also an `IViewFor<TViewModel>` |
| `ReactiveViewHostUnsafe<TViewModel>` | A `ReactiveViewHost<TViewModel>` whose constructor can wire its children by reflection (auto-wireup) |
| `ILayoutViewHost` | The `View`-holding interface both hosts implement |
| `ViewMixins.GetViewHost()` / `GetViewHost<T>()` | Reads the host a `View` was tagged with when it was hosted |
| `AndroidX.ReactiveRecyclerViewAdapter<TViewModel, TCollection>` | Adapts a collection of view models to a `RecyclerView` |
| `AndroidX.ReactiveRecyclerViewAdapter<TViewModel>` | Adapts a change-set stream of view models to a `RecyclerView` |
| `AndroidX.ReactiveRecyclerViewViewHolder<TViewModel>` | A `RecyclerView.ViewHolder` that is an `IViewFor<TViewModel>`, with `Selected`, `SelectedWithViewModel`, `LongClicked` and `LongClickedWithViewModel` |
| `AndroidX.ReactivePagerAdapter<TViewModel, TCollection>` | Pages a collection of view models through a `ViewPager` |
| `AndroidX.ReactivePagerAdapter<TViewModel>` | Pages a change-set stream of view models through a `ViewPager` |
| `ContextExtensions.ServiceBound(...)` / `ServiceBound<TBinder>(...)` | Binds a service and exposes its binder as a stream |
| `AutoSuspendHelper` | Turns activity lifecycle callbacks into suspend/resume signals |
| `AutoSuspendHelper.LatestBundle` | The bundle from the most recent `OnSaveInstanceState`, or `null` on a cold launch |
| `AutoSuspendHelper.UntimelyDemise` | Fires once from an unhandled exception |
| `BundleSuspensionDriver.SaveState<T>(T, JsonTypeInfo<T>)` / `LoadState<T>(JsonTypeInfo<T>)` | Trim- and AOT-safe save and load through a source-generated `JsonTypeInfo<T>` |
| `BundleSuspensionDriver.InvalidateState()` | Clears the saved state |
| `BundleSuspensionDriver.SaveState<T>(T)` / `LoadState()` | The untyped, reflection-based overloads; not callable from a trimmed or AOT page project |
| `PlatformOperations.GetOrientation()` | Returns the display's current rotation, such as `"Rotation0"` |
| `SharedPreferencesExtensions.PreferenceChanged()` | A stream of the key each changed shared preference used |
| `UsbManagerExtensions.PermissionRequested(...)` | A stream of the granted result for a USB device or accessory permission request; needs real or emulated hardware |
