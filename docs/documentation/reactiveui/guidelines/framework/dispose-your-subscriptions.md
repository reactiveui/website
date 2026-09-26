# Dispose your subscriptions

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/guidelines/guidelines.csproj).

A subscription is an `IDisposable`: while it is alive, its stream keeps a path from whatever it watches to your
subscriber. A subscription you never keep a reference to cannot be disposed, so it keeps that path open, and its
subscriber, for as long as the object it watches is alive. `WhenActivated` and `DisposeWith` tie a subscription to
a view or view model's activation, so it closes when the screen does.

## Tie a subscription to activation

**1. Wrap the subscription in `WhenActivated`.** The example below subscribes to a student's grade inside it.

**2. Dispose the activation with `DisposeWith`.** Activating the object below prints the grade at once, the way a
label bound to it would refresh.

```csharp
public PreferGradeAnnouncer(Student student) =>
    this.WhenActivated(disposables =>
        student.WhenAnyValue(x => x.Grade)
            .Subscribe(grade => Console.WriteLine($"Prefer: {student.Name}'s grade is now {grade}"))
            .DisposeWith(disposables));
```

Activating and then deactivating stops the subscription:

```csharp
Student student = new("Grace", "Robotics Club") { Grade = 88 };
using PreferGradeAnnouncer announcer = new(student);
IDisposable activation = announcer.Activator.Activate(); // prints the current grade immediately: 88

student.Grade = 95;
activation.Dispose(); // stops the subscription WhenActivated set up
student.Grade = 99; // not announced
```

```text
Prefer: Grace's grade is now 88
Prefer: Grace's grade is now 95
```

Deactivating disposes everything `WhenActivated`'s lambda added to `disposables`, so the grade set after that,
`99`, is never announced. [When activated](../../handbook/when-activated.md) covers `IActivatableViewModel` and
`ViewModelActivator`, the type `Activator` returns.

## What a leaked subscription does

The type below subscribes in its constructor and keeps no reference to the subscription. Its `Dispose` method has
nothing to call.

```csharp
public AvoidGradeAnnouncer(Student student) =>
    student.WhenAnyValue(x => x.Grade).Subscribe(grade => Console.WriteLine($"Avoid: {student.Name}'s grade is now {grade}"));
```

```csharp
Student student = new("Grace", "Robotics Club") { Grade = 88 };
AvoidGradeAnnouncer announcer = new(student); // prints the current grade immediately: 88

student.Grade = 95;
announcer.Dispose(); // there is nothing here to stop
student.Grade = 99; // still announced: the subscription leaked
```

```text
Avoid: Grace's grade is now 88
Avoid: Grace's grade is now 95
Avoid: Grace's grade is now 99
```

Calling `Dispose()` on `announcer` does nothing to the subscription, because nothing kept a reference to it. The
grade set afterward, `99`, is still announced: the subscription, and the announcer with it, stays alive for as
long as `student` does.

## Not every subscription needs disposing

A view model that subscribes to its own property does not need to dispose that subscription. The subscription is
the view model holding a reference to itself, the same way a component that both raises and handles its own event
needs no unsubscribe.

```csharp
Student student = new("Grace", "Robotics Club");
List<int> grades = [];

// No DisposeWith: student's own PropertyChanged event holds this subscription, and dies with student itself.
student.WhenAnyValue(x => x.Grade).Subscribe(grades.Add);

student.Grade = 88;
student.Grade = 95;

Console.WriteLine(string.Join(", ", grades));
```

```text
0, 88, 95
```

`student` cannot be collected while its own `PropertyChanged` event still references the subscriber, so the
subscription and `student` live and die together. [Dispose every subscription](../../../primitives/best-practices.md)
covers the general rule for a subscription that is not self-referential.
