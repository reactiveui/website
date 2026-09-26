---
NoTitle: true
Order: 7
---
.NET for Android gives a view no built-in way to raise a change notification, so a binding has nothing to
subscribe to unless the view raises one itself. To bind a view to a view model, implement `IViewFor<TViewModel>`
on the view. ReactiveUI's Android base classes already do this: `ReactiveActivity<TViewModel>` and
`ReactiveFragment<TViewModel>` in the core `ReactiveUI` package, and their AppCompat and Jetpack equivalents,
`ReactiveUI.AndroidX.ReactiveAppCompatActivity<TViewModel>`, `ReactiveUI.AndroidX.ReactiveFragment<TViewModel>`
and the rest, in `ReactiveUI.AndroidX`. Derive from one of them and the `ViewModel` property, and the change
notifications it raises, come for free.

The school timetable app on [Android](../../platforms/android.md) shows both families side by side. Its main
screen derives from the AndroidX base class:

```csharp
public sealed class MainActivity : AndroidX.ReactiveAppCompatActivity<TimetableViewModel>
```

Its absence screen derives from the plain, non-AppCompat base class instead, because it predates AndroidX:

```csharp
public sealed class AbsenceActivity : ReactiveActivity<AbsenceViewModel>
```

Both give you a `ViewModel` property and the `IViewFor.ViewModel` explicit implementation a caller uses when it
only knows the view model as `object`. Once a view has one, it can read `ViewModel` the way any other property
observation does, with `WhenAnyValue`. The lesson detail fragment reads its `ViewModel` this way and updates two
labels whenever it changes, instead of a two-way binding:

```csharp
this.WhenAnyValue(fragment => fragment.ViewModel).Subscribe(lesson =>
{
    SubjectLabel!.Text = lesson?.Subject;
    RoomLabel!.Text = lesson is null ? null : $"Room {lesson.Room}";
});
```

`SubjectLabel` and `RoomLabel` above are wired to their layout resources by [WireUpControls](wire-up-controls.md),
so the fragment never calls `FindViewById` itself.

See [Android](../../platforms/android.md) for the complete app: activation, lists, paging, dialogs, preferences,
service binding, and the rest of what `ReactiveUI.AndroidX` adds on top of `IViewFor<TViewModel>`.
