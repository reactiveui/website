---
Order: 13
---
# Message Bus

Like many other MVVM frameworks, ReactiveUI includes an implementation of the
message bus pattern. This allows you to send and recieve messages between
different parts of the code without them directly accessing each other.

One unique property of the default MessageBus (`MessageBus.Current`) in
ReactiveUI is that it schedules messages via the UI thread. This means that
messages sent from background threads will automatically arrive on the main
thread. The MessageBus is also useful for marshaling messages between
different layers of the code (usually sending messages from View to ViewModel)

While this class is provided because it is sometimes necessary, the MessageBus
should be used only as **a last resort**. The MessageBus is effectively a
*global variable*, which means it is subject to memory and event leaks, and
furthermore, the detached nature of MessageBus means that it's a `goto` whose
destination is invisible. It also encourages bad design as many people will
directly proxy View events to the ViewModel layer, which makes them not
particularly ViewModelly.

## The Basics

MessageBus is quite straightforward. First, set up a listener:

```cs
// Listen for anyone sending instances of the KeyUpEventArgs class. Since
// MessageBus simply returns an IObservable, it can be combined or used in
// many different ways
MessageBus.Current.Listen<KeyUpEventArgs>()
    .Where(e => e.KeyCode == KeyCode.Up)
    .Subscribe(x => Console.WriteLine("Up Pressed!"));
```

Now, connect an IObservable to the bus via `RegisterMessageSource`:

```cs
MessageBus.Current.RegisterMessageSource(RootVisual.Events().KeyUp);
```

Or, if you're feeling very imperative and not very Functional:

```cs
MessageBus.Current.SendMessage(new KeyUpEventArgs());
```

## Ways to avoid using MessageBus

Unlike other MVVM frameworks, there are often more correct ways to solve
problems, given a bit of ingenuity. `WhenAny` and `WhenAnyObservable` can
often be used to describe how to reach into objects, even if these objects are
changing over time. This is often most useful in the View:

```cs
public LoginView()
{
    // As soon as the CredentialsAreValid turns to 'true', set the focus
    // to the Ok button.
    this.WhenAny(x => x.ViewModel.CredentialsAreValid, x => x.Value)
        .Where(x => x != false)
        .Subscribe(_ => OkButton.SetFocus());
}
```

Consider another scenario, a ViewModel of open documents containing a list of
Document ViewModels - each Document containing a `Close` command. Many
traditional implementations of MVVM would struggle with implementing this
command, either keeping a reference to the list, or via the MessageBus.

Instead, stream operators solve it with no message bus at all:

```cs
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ReactiveUI;
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;

public class DocumentViewModel : ReactiveObject
{
    public DocumentViewModel(string name)
    {
        Name = name;

        // Note that we don't actually *subscribe* to Close here or implement
        // anything in DocumentViewModel, because Closing is a responsibility
        // of the document list.
        Close = ReactiveCommand.Create(() => { });
    }

    public string Name { get; }

    public ReactiveCommand<RxVoid, RxVoid> Close { get; }
}

public class MainViewModel : ReactiveObject
{
    public ObservableCollection<DocumentViewModel> OpenDocuments { get; } = [];

    public MainViewModel()
    {
        // Whenever the list of documents changes, build a new stream that
        // sends a document when it asks to close, then switch to that stream.
        // When a document arrives, remove it from the list.
        Signal.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                handler => OpenDocuments.CollectionChanged += handler,
                handler => OpenDocuments.CollectionChanged -= handler)
            .Select(_ => WhenAnyDocumentClosed())
            .SwitchTo()
            .Subscribe(document => OpenDocuments.Remove(document));
    }

    // Turn each document's Close command into a stream that sends the
    // document, then blend them all into one stream.
    private IObservable<DocumentViewModel> WhenAnyDocumentClosed() =>
        OpenDocuments
            .Select(document => document.Close.Select(_ => document))
            .ToList()
            .Blend();
}
```

`SwitchTo` follows only the newest stream, so the documents closed are always
the ones in the list right now. `Blend` sends a value from any of the streams
it joins. See [transformation](../primitives/transformation.md) and
[combination](../primitives/combination.md).

```csharp
var main = new MainViewModel();
var first = new DocumentViewModel("first");
var second = new DocumentViewModel("second");

main.OpenDocuments.Add(first);
main.OpenDocuments.Add(second);

first.Close.Execute().Subscribe();
Console.WriteLine(string.Join(", ", main.OpenDocuments.Select(d => d.Name)));
```

Output:

```text
second
```
