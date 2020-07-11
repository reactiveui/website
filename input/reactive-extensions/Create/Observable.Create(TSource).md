title: Observable.Create<TSource>(Func<IObserver<TSource>, Action>)
---
# Observable.Create\<TSource\> Method (Func\<IObserver\<TSource\>, Action\>)

Creates an observable sequence from a specified subscribe method implementation with a specified subscribe.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Create(Of TSource) ( _
    subscribe As Func(Of IObserver(Of TSource), Action) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim subscribe As Func(Of IObserver(Of TSource), Action)
Dim returnValue As IObservable(Of TSource)

returnValue = Observable.Create(subscribe)
```

```csharp
public static IObservable<TSource> Create<TSource>(
    Func<IObserver<TSource>, Action> subscribe
)
```

```c++
public:
generic<typename TSource>
static IObservable<TSource>^ Create(
    Func<IObserver<TSource>^, Action^>^ subscribe
)
```

```fsharp
static member Create : 
        subscribe:Func<IObserver<'TSource>, Action> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- subscribe  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<TSource\>, [Action](https://msdn.microsoft.com/en-us/library/Bb534741)\>  
  The implementation of the resulting observable sequence's subscribe method, returning an action delegate that will be wrapped in an IDisposable.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence with the specified implementation for the subscribe method.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Create Overload](Create/Observable.Create)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Create\<TSource\> Method (Func\<IObserver\<TSource\>, IDisposable\>)

Creates an observable sequence from a subscribe method implementation.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Create(Of TSource) ( _
    subscribe As Func(Of IObserver(Of TSource), IDisposable) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim subscribe As Func(Of IObserver(Of TSource), IDisposable)
Dim returnValue As IObservable(Of TSource)

returnValue = Observable.Create(subscribe)
```

```csharp
public static IObservable<TSource> Create<TSource>(
    Func<IObserver<TSource>, IDisposable> subscribe
)
```

```c++
public:
generic<typename TSource>
static IObservable<TSource>^ Create(
    Func<IObserver<TSource>^, IDisposable^>^ subscribe
)
```

```fsharp
static member Create : 
        subscribe:Func<IObserver<'TSource>, IDisposable> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- subscribe  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<TSource\>, [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\>  
  The implementation of the resulting observable sequence's subscribe method.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence with the specified implementation for the subscribe method.

## Remarks

The Create operator allows you to create your own custom sequences without fully implementing the IObservable\<T\> interface for your sequence. Using this operator you just implement a subscribe function which takes an IObserver\<T\> where T is your type and returns the IDisposable interface that is used to cancel the subscription by calling IDisposable::Dispose(). The use of this operator is preferred over manual implementation of the IObservable\<T\> interface.

## Examples

This example simulates a hypothetical ticket system where an observable sequence of tickets is provided without fully implementing IObservable\<Ticket\>. The TicketFactory class implements it’s own subscribe method called TicketSubscribe. This method is passed as the **subscribe** parameter to Create operator. TicketSubscribe creates a continuous sequence of tickets on another thread until the subscription is canceled by calling the Dispose method on the IDisposable interface returned from TicketSubscribe. An IObserver\<Ticket\> is passed to TicketSubscribe. Each ticket in the sequence is delivered by calling IObserver\<Ticket\>.OnNext(). The observer action executed for the subscription displays each ticket to the console window.

    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Reactive.Linq;
    
    namespace Example
    {
    
      class Program
      {
    
        static void Main()
        {
          IObservable<Ticket> ticketObservable = Observable.Create((Func<IObserver<Ticket>, IDisposable>)TicketFactory.TicketSubscribe);
    
          //*********************************************************************************//
          //*** This is a sequence of tickets. Display each ticket in the console window. ***//
          //*********************************************************************************//
          using(IDisposable handle = ticketObservable.Subscribe(ticket => Console.WriteLine(ticket.ToString())))
          {
            Console.WriteLine("\nPress ENTER to unsubscribe...\n");
            Console.ReadLine();
          }
        }
      }
    
    
      //***************************************************************************************************//
      //***                                                                                             ***//
      //*** The Ticket class just represents a hypothetical ticket composed of an ID and a timestamp.   ***//
      //***                                                                                             ***//
      //***************************************************************************************************//
      class Ticket
      {
        private readonly string ticketID;
        private readonly DateTime timeStamp;
    
        public Ticket(string tid)
        {
          ticketID = tid;
          timeStamp = DateTime.Now;
        }
    
        public override string ToString()
        {
          return String.Format("Ticket ID : {0}\nTimestamp : {1}\n", ticketID, timeStamp.ToString());
        }
      }
    
    
      //***************************************************************************************************//
      //***                                                                                             ***//
      //*** The TicketFactory class generates a new sequence of tickets on a separate thread. The       ***//
      //*** generation of the sequence is initiated by the TicketSubscribe method which will be passed  ***//
      //*** to Observable.Create().                                                                     ***//
      //***                                                                                             ***//
      //*** TicketSubscribe() provides the IDisposable interface used to dispose of the subscription    ***//
      //*** stopping ticket generation resources.                                                       ***//
      //***                                                                                             ***//
      //***************************************************************************************************//
      public class TicketFactory : IDisposable
      {
        private bool bGenerate = true;
    
    
        internal TicketFactory(object ticketObserver)
        {
          //************************************************************************//
          //*** The sequence generator for tickets will be run on another thread ***//
          //************************************************************************//
          Task.Factory.StartNew(new Action<object>(TicketGenerator), ticketObserver);
        }
    
    
        //**************************************************************************************************//
        //*** Dispose frees the ticket generating resources by allowing the TicketGenerator to complete. ***//
        //**************************************************************************************************//
        public void Dispose()
        {
          bGenerate = false;
        }
    
    
        //*****************************************************************************************************************//
        //*** TicketGenerator generates a new ticket every 3 sec and calls the observer's OnNext handler to deliver it. ***//
        //*****************************************************************************************************************//
        private void TicketGenerator(object observer)
        {
          IObserver<Ticket> ticketObserver = (IObserver<Ticket>)observer;
    
          //***********************************************************************************************//
          //*** Generate a new ticket every 3 sec and call the observer's OnNext handler to deliver it. ***//
          //***********************************************************************************************//
          Ticket t;
    
          while (bGenerate)
          {
            t = new Ticket(Guid.NewGuid().ToString());
            ticketObserver.OnNext(t);
            Thread.Sleep(3000);
          }
        }
    
    
    
        //********************************************************************************************************************************//
        //*** TicketSubscribe starts the flow of tickets for the ticket sequence when a subscription is created. It is passed to       ***//
        //*** Observable.Create() as the subscribe parameter. Observable.Create() returns the IObservable<Ticket> that is used to      ***//
        //*** create subscriptions by calling the IObservable<Ticket>.Subscribe() method.                                              ***//
        //***                                                                                                                          ***//
        //*** The IDisposable interface returned by TicketSubscribe is returned from the IObservable<Ticket>.Subscribe() call. Calling ***//
        //*** Dispose cancels the subscription freeing ticket generating resources.                                                    ***//
        //********************************************************************************************************************************//
        public static IDisposable TicketSubscribe(object ticketObserver)
        {
          TicketFactory tf = new TicketFactory(ticketObserver);
    
          return tf;
        }
      }
    }

The following is example output from the example code. Press enter to cancel the registration.

    Press ENTER to unsubscribe...
    
    Ticket ID : a5715731-b9ba-4992-af00-d5030956cfc4
    Timestamp : 5/18/2011 6:48:50 AM
    
    Ticket ID : d9797b2b-a356-4928-bfce-797a1637b11d
    Timestamp : 5/18/2011 6:48:53 AM
    
    Ticket ID : bb01dc7d-1ed5-4ba0-9ce0-6029187792be
    Timestamp : 5/18/2011 6:48:56 AM
    
    Ticket ID : 0d3c95de-392f-4ed3-bbda-fed2c6bc7287
    Timestamp : 5/18/2011 6:48:59 AM
    
    Ticket ID : 4d19f79e-6d4f-4fec-83a8-9644a1d4e759
    Timestamp : 5/18/2011 6:49:05 AM

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Create Overload](Create/Observable.Create)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









