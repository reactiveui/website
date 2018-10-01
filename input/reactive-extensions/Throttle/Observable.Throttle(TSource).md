# Observable.Throttle\<TSource\> Method (IObservable\<TSource\>, TimeSpan, IScheduler)

Ignores the values from an observable sequence which are followed by another value before due time with the specified source, dueTime and scheduler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Throttle(Of TSource) ( _
    source As IObservable(Of TSource), _
    dueTime As TimeSpan, _
    scheduler As IScheduler _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim dueTime As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TSource)

returnValue = source.Throttle(dueTime, _
    scheduler)
```

```csharp
public static IObservable<TSource> Throttle<TSource>(
    this IObservable<TSource> source,
    TimeSpan dueTime,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Throttle(
    IObservable<TSource>^ source, 
    TimeSpan dueTime, 
    IScheduler^ scheduler
)
```

```fsharp
static member Throttle : 
        source:IObservable<'TSource> * 
        dueTime:TimeSpan * 
        scheduler:IScheduler -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to throttle.

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The duration of the throttle period for each value.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run the throttle timers on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The values from an observable sequence which are followed by another value before due time.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

The Throttle operator buffers an item from the sequence and wait for the time span specified by the dueTime parameter to expire. If another item is produced from the sequence before the time span expires, then that item replaces the old item in the buffer and the wait starts over. If the due time does expire before another item is produced in the sequence, them that item is observed through any subscriptions to the sequence.

## Examples

This example demonstrates using the throttle operator to guarantee that items are observed at an interval no faster than two seconds. This is demonstrated by using the EndlessBarrageOfEmails method to continuously yield emails that are produced as items in an observable sequence. The email items in the sequence occur at random intervals within three seconds from each other. Only the items that occur with no item following it for the two second time span are observed from the sequence.

    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Reactive.Linq;
    using System.Reactive.Concurrency;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //*****************************************************************************************************//
          //*** Create an observable sequence from the enumerator which is yielding random emails within      ***//
          //*** 3 sec. continuously.  The enumeration of the enumerator will be scheduled to run on a thread  ***//
          //*** in the .NET thread pool so the main thread will not be blocked.                               ***//
          //*****************************************************************************************************//
    
          var obs = EndlessBarrageOfEmails().ToObservable(Scheduler.ThreadPool);
    
    
          //****************************************************************************************************//
          //*** Use the throttle operator to ONLY deliver an item that occurs with a 2 second interval       ***//
          //*** between it and the next item in the sequence. The throttle buffer will hold an item from the ***//
          //*** sequence waiting for the 2 second timespan to pass. If a new item is produced before the     ***//
          //*** time span expires, that new item will replace the old item in the buffer and the wait starts ***//
          //*** over. If the time span does expire before a new item is produced, then the item in the       ***//
          //*** buffer will be observed through any subscriptions on the sequence.                           ***//
          //***                                                                                              ***//
          //*** To be clear, an item is not guarnteed to be returned every 2 seconds. The use of throttle    ***//
          //*** here does guarntee that the subscriber will observe an item no faster than every 2 sec.      ***//
          //***                                                                                              ***//
          //*** Since a new email is generated at a random time within 3 seconds, the items which are        ***//
          //*** generated with 2 seconds of silence following them will also be random.                      ***//
          //***                                                                                              ***//
          //*** The timers associated with the 2 second time span are run on the .NET thread pool.           ***//
          //****************************************************************************************************//
    
          var obsThrottled = obs.Throttle(TimeSpan.FromSeconds(2), Scheduler.ThreadPool);
    
    
          //***********************************************************************************************//
          //*** Write each observed email to the console window. Also write a current timestamp to get  ***//
          //*** an idea of the time which has passed since the last item was observed. Notice, the time ***//
          //*** will not be less than 2 seconds but, will frequently exceed 2 sec.                      ***//
          //***********************************************************************************************//
    
          obsThrottled.Subscribe(i => Console.WriteLine("{0}\nTime Received {1}\n", i, DateTime.Now.ToString()));
    
    
          //*********************************************************************************************//
          //*** Main thread waiting on the user's ENTER key press.                                    ***//
          //*********************************************************************************************//
    
          Console.WriteLine("\nPress ENTER to exit...\n");
          Console.ReadLine();
        }
    
    
    
    
        //*********************************************************************************************//
        //***                                                                                       ***//
        //*** This method will continually yield a random email at a random interval within 3 sec.  ***//
        //***                                                                                       ***//
        //*********************************************************************************************//
        static IEnumerable<string> EndlessBarrageOfEmails()
        {
          Random random = new Random();
    
          //***************************************************************//
          //*** For this example we are using this fixed list of emails ***//
          //***************************************************************//
          List<string> emails = new List<string> { "Email Msg from John ", 
                                                   "Email Msg from Bill ", 
                                                   "Email Msg from Marcy ", 
                                                   "Email Msg from Wes "};
    
          //***********************************************************************************//
          //*** Yield an email from the list continually at a random interval within 3 sec. ***//
          //***********************************************************************************//
          while (true)
          {
            yield return emails[random.Next(emails.Count)];
            Thread.Sleep(random.Next(3000));
          }
        }
      }
    }

The following output was generated from the example code.

    Press ENTER to exit...
    
    Email Msg from Wes
    Time Received 6/5/2011 11:54:05 PM
    
    Email Msg from Marcy
    Time Received 6/5/2011 11:54:08 PM
    
    Email Msg from Bill
    Time Received 6/5/2011 11:54:12 PM
    
    Email Msg from Bill
    Time Received 6/5/2011 11:54:15 PM
    
    Email Msg from John
    Time Received 6/5/2011 11:54:33 PM
    
    Email Msg from Wes
    Time Received 6/5/2011 11:54:35 PM
    
    Email Msg from Marcy
    Time Received 6/5/2011 11:54:38 PM
    
    Email Msg from Bill
    Time Received 6/5/2011 11:54:43 PM

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Throttle Overload](Throttle\Observable.Throttle.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Throttle\<TSource\> Method (IObservable\<TSource\>, TimeSpan)

Ignores the values from an observable sequence which are followed by another value before due time with the specified source and dueTime.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Throttle(Of TSource) ( _
    source As IObservable(Of TSource), _
    dueTime As TimeSpan _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim dueTime As TimeSpan
Dim returnValue As IObservable(Of TSource)

returnValue = source.Throttle(dueTime)
```

```csharp
public static IObservable<TSource> Throttle<TSource>(
    this IObservable<TSource> source,
    TimeSpan dueTime
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Throttle(
    IObservable<TSource>^ source, 
    TimeSpan dueTime
)
```

```fsharp
static member Throttle : 
        source:IObservable<'TSource> * 
        dueTime:TimeSpan -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to throttle.

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The duration of the throttle period for each value.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The values from an observable sequence which are followed by another value before due time.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Throttle Overload](Throttle\Observable.Throttle.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
