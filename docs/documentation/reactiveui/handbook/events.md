---
Order: 19
---
# Events

A view raises events: a button is clicked, a key is released, a page loads. To use them in a view model pipeline, you
turn each event into a stream. Then you can filter it, combine it with other streams, and feed it to a command.

## Generate a stream for every event

The `ReactiveUI.Primitives.ObservableEvents` package does this for you. It is a **source generator**: a part of the
compiler that writes extra C# code into your project when it builds. For every public event on a type, it writes a
strongly typed stream, so your IDE suggests the events and the compiler checks their names. Call `Events()` on the
object that raises the event, then pick the event as a property on the result.

[ObservableEvents](../../primitives/observable-events/index.md) is the full walkthrough: installing the package,
what each event's stream sends, static events, and the compiler warning the generator reports when it cannot see an
event.

## Use a generated stream in a view

Because each event is a stream, an operator can filter or combine it the way it would any other stream: buffering a
run of key presses, say, or selecting only the argument a command needs. Subscribe inside
[WhenActivated](when-activated.md) so the subscription starts when the view appears and stops when it is hidden;
otherwise the view or its view model can outlive the other. `InvokeCommand` runs a `ReactiveCommand` each time the
stream emits, which is the usual way to connect a button's click stream to a view model command. See
[Composing events](../../primitives/observable-events/index.md#composing-events) for a worked example, and
[the operator pages](../../primitives/index.md#the-operator-pages) for `Buffer`, `Select`, `Where` and the rest.

## Turn an event into a stream yourself

Without the generator, `Signal` has two methods that do the same job by hand. Each takes a lambda that attaches the
handler and one that removes it.

- `Signal.FromEventPattern` works with events whose delegate takes a sender and an `EventArgs`. It sends an
  `EventPattern<TEventArgs>`, with `Sender` and `EventArgs` properties.
- `Signal.FromEvent` works with any delegate type, including one that carries no `EventArgs` at all.

Neither method looks events up by name, so both work with trimming and Native AOT. See
[From an event](../../primitives/creation-factories.md#from-an-event) for both methods, their overloads, and the
conversion argument a custom delegate type needs.
