---
NoTitle: true
---
.NET for Android has no built-in way to generate a property for each control in a layout. `WireUpControls` fills
that gap. Call it once, and it finds every eligible property on your `Activity`, `Fragment`, `View` or
`ILayoutViewHost`. It assigns each one the control with the matching resource ID, instead of a `FindViewById`
call per field. It works the way [Butterknife](https://jakewharton.github.io/butterknife/) does for Java Android
code.
The [school timetable app](../../platforms/android.md) uses it throughout; the examples below are all from that
app.

## Naming policy

`WireUpControls` builds a dictionary of every resource ID name in your layouts, lowercased, mapped to the actual
resource ID; the mapping mirrors the generated `Resource.designer.cs` file. Because the lookup is
case-insensitive, you cannot reuse the same resource name with two different casings across your layouts.
Android generates a distinct ID for each casing, so there is no single ID `WireUpControls` could map both to.
Layout files commonly use a lower-case-first name such as `subjectLabel`, while a C# property uses an
upper-case-first name such as `SubjectLabel`; the lowercased lookup is why that difference does not need a
`[WireUpResource]` override.

## Resolve strategies

Three `ControlFetcherMixins.ResolveStrategy` values choose which properties `WireUpControls` wires.

| Strategy | Wires |
| --- | --- |
| `Implicit` | Every writable property whose type is a `View` subclass, or that carries `[WireUpResource]` |
| `ExplicitOptIn` | Only properties that carry `[WireUpResource]` |
| `ExplicitOptOut` | Every writable `View`-typed property except one that carries `[IgnoreResource]` |

### Implicit

`Implicit` is the default: call `WireUpControls()` with no argument, and it wires every `View`-typed writable
property regardless of visibility. `MainActivity`'s home screen uses it for three properties, all resolved by
their like-named resources in `activity_main.xml`:

```csharp
this.WireUpControls();

PropertyInfo[] wiredMembers = this.GetWireUpMembers(ControlFetcherMixins.ResolveStrategy.Implicit);
TimetableLog.Info($"WireUpControls found {wiredMembers.Length} members to wire.");
foreach (PropertyInfo member in wiredMembers)
{
    TimetableLog.Info($"  {member.Name} -> resource '{member.GetResourceName()}'.");
}
```

```text
WireUpControls found 3 members to wire.
```

`GetWireUpMembers` returns the properties a strategy would wire without wiring them, and `GetResourceName`
reads the resource name one property resolves to; both are useful for a diagnostic like the one above. The
activity's layout, `activity_main.xml`, declares the three resources those properties resolve to:

```xml
<?xml version="1.0" encoding="utf-8"?>
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
    android:orientation="vertical"
    android:layout_width="match_parent"
    android:layout_height="match_parent">
  <ReactiveUI.Documentation.PlatformAndroid.LessonCardView
      android:id="@+id/featuredLessonCard"
      android:layout_width="match_parent"
      android:layout_height="wrap_content"
      android:padding="8dp" />
  <FrameLayout
      android:id="@+id/badgeContainer"
      android:layout_width="match_parent"
      android:layout_height="wrap_content"
      android:padding="8dp" />
  <LinearLayout
      android:id="@+id/peeksRow"
      android:orientation="horizontal"
      android:layout_width="match_parent"
      android:layout_height="wrap_content" />
  <androidx.recyclerview.widget.RecyclerView
      android:id="@+id/lessonsRecyclerView"
      android:layout_width="match_parent"
      android:layout_height="0dp"
      android:layout_weight="1" />
  <FrameLayout
      android:id="@+id/detailContainer"
      android:layout_width="match_parent"
      android:layout_height="wrap_content" />
</LinearLayout>
```

`LessonsRecyclerView`, `BadgeContainer` and `DetailContainer` are the three writable `View`-typed properties
`MainActivity` declares; `FeaturedLessonCard` and `PeeksRow` are assigned separately in code, so `Implicit`
wires only the properties the activity actually declares as `View` subtypes. `LessonCardView`, a compound view
inflated into the top of that layout, wires its own two labels the same way, from inside its own constructor:

```csharp
{
    Orientation = Orientation.Vertical;
    LayoutInflater.From(context)!.Inflate(Resource.Layout.lesson_card, this, true);

    // Implicit strategy: SubjectLabel and RoomLabel are wired to the like-named ids merged into this view.
    this.WireUpControls();
}
```

```xml
<?xml version="1.0" encoding="utf-8"?>
<merge xmlns:android="http://schemas.android.com/apk/res/android">
  <TextView
      android:id="@+id/cardSubjectLabel"
      android:layout_width="match_parent"
      android:layout_height="wrap_content"
      android:textStyle="bold" />
  <TextView
      android:id="@+id/cardRoomLabel"
      android:layout_width="match_parent"
      android:layout_height="wrap_content" />
</merge>
```

`LessonViewHolder`, one row of the lessons `RecyclerView`, wires `SubjectLabel` and `RoomLabel` the same way
against the inflated row view, from its constructor body:

```csharp
{
    // Implicit strategy: any writable View-typed property (SubjectLabel, RoomLabel) is wired to the
    // like-named resource in the inflated row layout.
    this.WireUpControls();
```

### Explicit opt-in

Under `ExplicitOptIn`, only a property that carries `[WireUpResource]` is wired; every other property is
ignored, with no type check. `[WireUpResource]` takes an optional resource name, for a property whose name does
not match its resource. `AbsenceStatusView`, a compound view, opts in its one label from its constructor body:

```csharp
{
    LayoutInflater.From(context)!.Inflate(Resource.Layout.absence_status, this, true);

    // ExplicitOptIn: only StatusText, which carries [WireUpResource], is wired.
    this.WireUpControls(ControlFetcherMixins.ResolveStrategy.ExplicitOptIn);
}
```

```csharp
[WireUpResource("statusText")]
public TextView? StatusText { get; set; }
```

`LessonDetailFragment` opts in two labels whose names differ from their resources, so each one names its
resource explicitly:

```csharp
[WireUpResource("detailSubject")]
public TextView? SubjectLabel { get; set; }

[WireUpResource("detailRoom")]
public TextView? RoomLabel { get; set; }
```

A fragment wires against its inflated view rather than itself, so it calls the two-argument overload. This
excerpt also shows the overload's non-generic-caller form, called directly on `AndroidX.ControlFetcherMixins`
instead of as an extension method:

```csharp
AndroidX.ControlFetcherMixins.WireUpControls(this, view, ControlFetcherMixins.ResolveStrategy.ExplicitOptIn);
```

The fragment's layout carries a third control, `detailIcon`, that no property wires. `GetControl` fetches it
directly instead, resolving it against a `View` and the fragment's assembly:

```csharp
View? icon = view.GetControl(GetType().Assembly, "detailIcon");
TimetableLog.Info(icon is null ? "detailIcon was not found." : "detailIcon resolved directly with GetControl.");
```

```xml
<?xml version="1.0" encoding="utf-8"?>
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
    android:orientation="vertical"
    android:layout_width="match_parent"
    android:layout_height="wrap_content"
    android:padding="16dp">
  <TextView
      android:id="@+id/detailSubject"
      android:layout_width="match_parent"
      android:layout_height="wrap_content"
      android:textStyle="bold" />
  <TextView
      android:id="@+id/detailRoom"
      android:layout_width="match_parent"
      android:layout_height="wrap_content" />
  <ImageView
      android:id="@+id/detailIcon"
      android:layout_width="24dp"
      android:layout_height="24dp"
      android:contentDescription="@null" />
</LinearLayout>
```

### Explicit opt-out

Under `ExplicitOptOut`, every writable `View`-typed property is wired unless it carries `[IgnoreResource]`.
`AbsenceActivity` uses it, and excludes a label its layout has no resource for:

```csharp
// ExplicitOptOut: every View-typed property is wired except those carrying [IgnoreResource].
this.WireUpControls(ControlFetcherMixins.ResolveStrategy.ExplicitOptOut);

// A control fetched directly, without a matching property, to show GetControl used on its own.
View? extraLabel = this.GetControl("extraLabel");
TimetableLog.Info(extraLabel is null ? "extraLabel was not found." : "extraLabel resolved directly with GetControl.");
```

```csharp
[IgnoreResource]
public TextView? UnusedLabel { get; set; }
```

Without `[IgnoreResource]`, `ExplicitOptOut` would try to wire `UnusedLabel` to a resource named `unusedlabel`
and throw a `MissingFieldException`, since `activity_absence.xml` declares no such resource. `GetControl` above
fetches `extraLabel` directly on the activity, the same way `LessonDetailFragment` fetched `detailIcon` on a
`View`; called with no argument, it resolves the resource named after the calling member instead.

```xml
<?xml version="1.0" encoding="utf-8"?>
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
    android:orientation="vertical"
    android:layout_width="match_parent"
    android:layout_height="match_parent"
    android:padding="16dp">
  <TextView
      android:id="@+id/statusLabel"
      android:layout_width="match_parent"
      android:layout_height="wrap_content" />
  <TextView
      android:id="@+id/extraLabel"
      android:layout_width="match_parent"
      android:layout_height="wrap_content" />
  <ReactiveUI.Documentation.PlatformAndroid.AbsenceStatusView
      android:id="@+id/absenceStatusView"
      android:layout_width="match_parent"
      android:layout_height="wrap_content" />
</LinearLayout>
```

## Wiring without reflection

`LessonCountBadgeHost`, a host with no view model, wires its one label through the auto-wireup constructor of
`LayoutViewHostUnsafe` instead of a call to `WireUpControls` in its own body. Its constructor passes
`attachToRoot: false`, `performAutoWireup: true` and `resolveStrategy: ControlFetcherMixins.ResolveStrategy.Implicit`
to the base constructor, which performs the same `Implicit` wire-up once inflation finishes; the constructor's
own body adds nothing further. `ReactiveViewHostUnsafe<TViewModel>` offers the same constructor for a host with a view
model. The reflection-free `LayoutViewHost` and `ReactiveViewHost<TViewModel>` have no such constructor.

A host that wires its child without reflection, and stays trim- and AOT-safe, uses the `bind`-callback
constructor instead and calls `FindViewById` itself. `WeekdayViewHost`, one page of the weekday pager, passes
`bind: static (host, view) => ((WeekdayViewHost)host)._weekdayLabel = view.FindViewById<TextView>(Resource.Id.weekdayLabel)`
to the base constructor this way.

Every `WireUpControls` overload, and `GetControl`, carries `RequiresUnreferencedCode` and `RequiresDynamicCode`:
both reflect over your assembly's generated resource type to resolve a name to an integer ID. The `bind`-callback
constructor above needs neither. `MissingFieldException` is what any `WireUpControls` overload throws when a
property cannot be resolved to a matching resource, whichever strategy is in effect.

See [Android](../../platforms/android.md) for the rest of what `ReactiveUI.AndroidX` adds: activation, lists,
paging, dialogs, preferences and service binding.
