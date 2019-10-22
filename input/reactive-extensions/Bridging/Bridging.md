title: Bridging with Existing .NET Events
---
# Bridging with Existing .NET Events

Rx provides factory methods for you to bridge with existing asynchronous sources in .NET so that you can employ the rich composing, filtering and resource management features provided by Rx on any kind of data streams. This topic examines the FromEventPattern operator that allows “importing” a .NET event into Rx as an observable sequence. Every time an event is raised, an OnNext message will be delivered to the observable sequence. You can then manipulate event data just like any other observable sequences.

Rx does not aim at replacing existing asynchronous programming models such as .NET events, the asynchronous pattern or the Task Parallel Library. However, when you attempt to compose events, Rx’s factory methods will provide you the convenience that cannot be found in the current programming model. This is especially true for resource maintenance (e.g., when to unsubscribe) and filtering (e.g., choosing what kind of data to receive). In this topic and the ones that follow, you can examine how these Rx features can assist you in asynchronous programming.

## Converting a .NET event to a Rx Observable Sequence

The following sample creates a simple .NET event handler for the mouse move event, and prints out the mouse’s location in a label on a Windows form.

    using System.Linq;
    using System.Windows.Forms;
    using System.Reactive;
    using System.Reactive.Linq;
    using System;
    using WinForm;
    using System.Reactive.Disposables;
    
    class Program {
     
        static void Main() 
        {
             var lbl = new Label(); 
             var frm = new Form { Controls = { lbl } }; 
             frm.MouseMove += (sender, args) =>
             {
                  lbl.Text = args.Location.ToString();
             };
             Application.Run(frm);
        }; 
    }

To import an event into Rx, you can use the FromEventPattern operator, and provide the EventArgs objects that will be raised by the event being bridged. The FromEventPattern operator works with events that take an object sender and some EventArgs, and uses reflection to find those add/remove methods for you. It then converts the given event into an observable sequence with an EventPattern type that captures both the sender and the event arguments.

For delegates that have one parameter (non-standard events), you can use the FromEvent operator which takes a pair of functions used to attach and detach a handler.

In the following example, we convert the mouse-move event stream of a Windows form into an observable sequence. Every time a mouse-move event is fired, the subscriber will receive an OnNext notification. We can then examine the EventArgs value of such notification and get the location of the mouse-move.

    using System.Linq;
    using System.Windows.Forms;
    using System.Reactive;
    using System.Reactive.Linq;
    using System;
    using WinForm;
    using System.Reactive.Disposables;
    
    class Program {
     
        static void Main() 
        {
             var lbl = new Label(); 
             var frm = new Form { Controls = { lbl } }; 
             IObservable<EventPattern<MouseEventArgs>> move = Observable.FromEventPattern<MouseEventArgs>(frm, "MouseMove");
             move.Subscribe(evt => { 
                                 lbl.Text = evt.EventArgs.Location.ToString(); 
                           }) ;
             Application.Run(frm);
       }; 
    }

Notice that in this sample, `move` becomes an observable sequence in which we can manipulate further. The [Querying Observable Sequences using LINQ Operators](Querying\Querying.md) topic will show you how you can project this sequence into a collection of Points type and filter its content, so that your application will only receive values that satisfy a certain criteria.

Cleaning up of the event handler is taken care of by the IDisposable object returned by the Subscribe method. Calling Dispose (done by reaching the end of the using-block in this example) will release all resources being used by the sequence including the underlying event handler. This essentially takes care of unsubscribing to an event on your behalf.

## See Also

#### Concepts

[Querying Observable Sequences using LINQ Operators](Querying\Querying.md)





# Bridging with Existing Asynchronous Sources

Besides .NET events, other asynchronous data sources exist in the .NET Framework. One of them is the asynchronous method pattern. In this design pattern, two methods are provided. One method (usually named BeginX) is used to start the computation and returns an IAsyncResult handle that’s passed to the second method (usually named EndX), which then retrieves the result of the computation. Completion is usually signaled by implementing an AsyncCallback delegate or polling IAsyncResult.IsCompleted. Code adhere to this pattern is often difficult to read and maintain. In this topic, we will show how to use Rx factory methods to convert such asynchronous data sources to observable sequences.

## Converting Async Patterns to Observable Sequences

Many asynchronous methods in .NET are written with signatures like BeginX and EndX where X is the method name that is being executed asynchronously. BeginX takes arguments to execute the method, an AsyncCallback which is an action that takes an IAsyncResult and returns nothing, and finally an object state. EndX takes the IAsyncResult which is passed in from the AsyncCallback to retrieve the value of the asynchronous call.

The FromAsyncPattern operator of the [Observable](Observable\Observable.md) type wraps the Begin and End methods (which are being passed as parameters to the operator), and returns a function that takes the same parameters as Begin and returns an observable. This observable represents a sequence that publishes a single value, which is the asynchronous result of the call you just specified.

In the following example, we will convert BeginRead and EndRead for a [Stream](https://msdn.microsoft.com/en-us/library/8f86tw9e) object which uses the IAsyncResult pattern to a function that returns an observable sequence. For the generic parameters of the FromAsyncPattern operator, we specify the types of the arguments of **BeginRead** up to the callback. Since the EndRead method returns a value, we append this type as the final generic parameter for FromAsyncPattern. If you hover over `var` for `read`, you will notice that the return value of FromAsyncPattern is a function delegate that has the following signature:  `Func<byte[], int32,int32, IObservable<int32>>`, which means that this function takes 3 parameters (the same ones for BeginRead) and returns an IObservable\<Int32\>. This IObservable contains one value, which is the integer returned by EndRead, and contains the number of bytes read from the stream, between zero (0) and the number of bytes you requested. Since we now get an IObservable instead of an IAsyncResult, we can use all the LINQ operators available to Observables and subscribe, parse or compose it.

    Stream inputStream = Console.OpenStandardInput();
    var read = Observable.FromAsyncPattern<byte[], int, int, int>(inputStream.BeginRead, inputStream.EndRead);
    byte[] someBytes = new byte[10];
    IObservable<int> source = read(someBytes, 0, 10);
    IDisposable subscription = source.Subscribe(
                                x => Console.WriteLine("OnNext: {0}", x),
                                ex => Console.WriteLine("OnError: {0}", ex.Message),
                                () => Console.WriteLine("OnCompleted"));
    Console.ReadKey();


