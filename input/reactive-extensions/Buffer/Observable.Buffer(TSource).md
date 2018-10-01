# Observable.Buffer\<TSource\> Method (IObservable\<TSource\>, TimeSpan, TimeSpan, IScheduler)

Indicates each element of an observable sequence into zero or more buffers which are produced based on timing information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IObservable(Of TSource), _
    timeSpan As TimeSpan, _
    timeShift As TimeSpan, _
    scheduler As IScheduler _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim timeSpan As TimeSpan
Dim timeShift As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.Buffer(timeSpan, _
    timeShift, scheduler)
```

```csharp
public static IObservable<IList<TSource>> Buffer<TSource>(
    this IObservable<TSource> source,
    TimeSpan timeSpan,
    TimeSpan timeShift,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<IList<TSource>^>^ Buffer(
    IObservable<TSource>^ source, 
    TimeSpan timeSpan, 
    TimeSpan timeShift, 
    IScheduler^ scheduler
)
```

```fsharp
static member Buffer : 
        source:IObservable<'TSource> * 
        timeSpan:TimeSpan * 
        timeShift:TimeSpan * 
        scheduler:IScheduler -> IObservable<IList<'TSource>> 
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
  The source sequence to produce buffers over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The length of each buffer.

- timeShift  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The interval between creation of consecutive buffers.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run buffering timers on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The buffered observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

This operator creates a buffer that will hold all items that occur during the duration of the timeSpan parameter. This allows an application to buffer items to be delivered in batches. The timeShift parameter indicates how often subscription handlers should be executed for the items in the buffer which results in pushing the items to the subscribers. The scheduler parameter controls which thread the timers for the buffer will be created on.

## Examples

The example code generates an endless sequence of emails from an IEnumerable that randomly yields an email within three seconds. The emails are timestamped using the IObservable.TimeStamp operator. Then they are buffered into a buffer that holds all emails that occur within a ten second time span. A subscription to the buffered sequence is created. Finally, each group of emails is then written to the console window along with the corresponding timestamp generated for the email.

    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Reactive.Linq;
    using System.Reactive;
    
    namespace Example
    {
    
      class Program
      {
        static void Main()
        {
          //************************************************************************************************************************//
          //*** By generating an observable sequence from the enumerator, we can use Rx to push the emails to an email buffer    ***//
          //*** and have the buffer dumped at an interval we choose. This simulates how often email is checked for new messages. ***//
          //************************************************************************************************************************//
          IObservable<string> myInbox = EndlessBarrageOfEmails().ToObservable();
    
    
          //************************************************************************************************************************//
          //*** We can use the Timestamp operator to additionally timestamp each email in the sequence when it is received.      ***//
          //************************************************************************************************************************//
          IObservable<Timestamped<string>> myInboxTimestamped = myInbox.Timestamp();
    
    
          //******************************************************************************************************************************//
          //*** The timer controls the frequency of emails delivered from the email buffer. This timer will be on another thread since ***//
          //*** the main thread will be blocked waiting on a key press.                                                                ***//
          //******************************************************************************************************************************//
          System.Reactive.Concurrency.IScheduler scheduleOnNewThread = System.Reactive.Concurrency.Scheduler.NewThread;
    
    
          //***************************************************************************************************************************//
          //*** Create a buffer with Rx that will hold all emails received within 10 secs and execute subscription handlers for the ***//
          //*** buffer every 10 secs.                                                                                               ***//
          //*** Schedule the timers associated with emptying the buffer to be created on the new thread.                            ***//
          //***************************************************************************************************************************//
          IObservable<IList<Timestamped<string>>> newMail = myInboxTimestamped.Buffer(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10), 
                                                                                      scheduleOnNewThread);
    
    
          //******************************************************//
          //*** Activate the subscription on a separate thread ***//
          //******************************************************//
          IDisposable handle = newMail.SubscribeOn(scheduleOnNewThread).Subscribe(emailList =>
          {
            Console.WriteLine("\nYou've got mail!  {0} messages.\n", emailList.Count);
            foreach (Timestamped<string> email in emailList)
            {
              Console.WriteLine("Message   : {0}\nTimestamp : {1}\n", email.Value, email.Timestamp.ToString());
            }
          });
    
          Console.ReadLine();
          handle.Dispose();
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

Here is example output from the example code.

    You've got mail!  6 messages.
    
    Message   : Email Msg from John
    Timestamp : 5/16/2011 3:45:09 PM -04:00
    
    Message   : Email Msg from Wes
    Timestamp : 5/16/2011 3:45:12 PM -04:00
    
    Message   : Email Msg from Marcy
    Timestamp : 5/16/2011 3:45:13 PM -04:00
    
    Message   : Email Msg from Bill
    Timestamp : 5/16/2011 3:45:13 PM -04:00
    
    Message   : Email Msg from Marcy
    Timestamp : 5/16/2011 3:45:13 PM -04:00
    
    Message   : Email Msg from Marcy
    Timestamp : 5/16/2011 3:45:15 PM -04:00
    
    
    You've got mail!  7 messages.
    
    Message   : Email Msg from Marcy
    Timestamp : 5/16/2011 3:45:17 PM -04:00
    
    Message   : Email Msg from Bill
    Timestamp : 5/16/2011 3:45:18 PM -04:00
    
    Message   : Email Msg from Wes
    Timestamp : 5/16/2011 3:45:19 PM -04:00
    
    Message   : Email Msg from Bill
    Timestamp : 5/16/2011 3:45:21 PM -04:00
    
    Message   : Email Msg from Bill
    Timestamp : 5/16/2011 3:45:24 PM -04:00
    
    Message   : Email Msg from Bill
    Timestamp : 5/16/2011 3:45:26 PM -04:00
    
    Message   : Email Msg from Marcy
    Timestamp : 5/16/2011 3:45:26 PM -04:00

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Buffer Overload](Buffer\Observable.Buffer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)











# Observable.Buffer\<TSource\> Method (IObservable\<TSource\>, TimeSpan, Int32, IScheduler)

Indicates each element of an observable sequence into a buffer that’s sent out when either it’s full or a given amount of time has elapsed.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IObservable(Of TSource), _
    timeSpan As TimeSpan, _
    count As Integer, _
    scheduler As IScheduler _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim timeSpan As TimeSpan
Dim count As Integer
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.Buffer(timeSpan, _
    count, scheduler)
```

```csharp
public static IObservable<IList<TSource>> Buffer<TSource>(
    this IObservable<TSource> source,
    TimeSpan timeSpan,
    int count,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<IList<TSource>^>^ Buffer(
    IObservable<TSource>^ source, 
    TimeSpan timeSpan, 
    int count, 
    IScheduler^ scheduler
)
```

```fsharp
static member Buffer : 
        source:IObservable<'TSource> * 
        timeSpan:TimeSpan * 
        count:int * 
        scheduler:IScheduler -> IObservable<IList<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to produce buffers over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The maximum time length of a buffer.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum element count of a buffer.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run buffering timers on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Buffer Overload](Buffer\Observable.Buffer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Buffer\<TSource\> Method (IObservable\<TSource\>, TimeSpan)

Indicates each element of an observable sequence into consecutive non-overlapping buffers which are produced based on timing information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IObservable(Of TSource), _
    timeSpan As TimeSpan _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim timeSpan As TimeSpan
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.Buffer(timeSpan)
```

```csharp
public static IObservable<IList<TSource>> Buffer<TSource>(
    this IObservable<TSource> source,
    TimeSpan timeSpan
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<IList<TSource>^>^ Buffer(
    IObservable<TSource>^ source, 
    TimeSpan timeSpan
)
```

```fsharp
static member Buffer : 
        source:IObservable<'TSource> * 
        timeSpan:TimeSpan -> IObservable<IList<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to produce buffers over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The length of each buffers.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Buffer Overload](Buffer\Observable.Buffer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Buffer\<TSource\> Method (IObservable\<TSource\>, TimeSpan, Int32)

Indicates each element of an observable sequence into a buffer that’s sent out when either it’s full or a given amount of time has elapsed.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IObservable(Of TSource), _
    timeSpan As TimeSpan, _
    count As Integer _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim timeSpan As TimeSpan
Dim count As Integer
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.Buffer(timeSpan, _
    count)
```

```csharp
public static IObservable<IList<TSource>> Buffer<TSource>(
    this IObservable<TSource> source,
    TimeSpan timeSpan,
    int count
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<IList<TSource>^>^ Buffer(
    IObservable<TSource>^ source, 
    TimeSpan timeSpan, 
    int count
)
```

```fsharp
static member Buffer : 
        source:IObservable<'TSource> * 
        timeSpan:TimeSpan * 
        count:int -> IObservable<IList<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to produce buffers over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The maximum time length of a window.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum element count of a window.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Buffer Overload](Buffer\Observable.Buffer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Buffer\<TSource\> Method (IObservable\<TSource\>, Int32, Int32)

Indicates each element of an observable sequence into zero or more buffers which are produced based on element count information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IObservable(Of TSource), _
    count As Integer, _
    skip As Integer _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim count As Integer
Dim skip As Integer
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.Buffer(count, _
    skip)
```

```csharp
public static IObservable<IList<TSource>> Buffer<TSource>(
    this IObservable<TSource> source,
    int count,
    int skip
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<IList<TSource>^>^ Buffer(
    IObservable<TSource>^ source, 
    int count, 
    int skip
)
```

```fsharp
static member Buffer : 
        source:IObservable<'TSource> * 
        count:int * 
        skip:int -> IObservable<IList<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to produce buffers over.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The length of each buffer.

- skip  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The number of elements to skip between creation of consecutive buffers.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
An observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Buffer Overload](Buffer\Observable.Buffer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Buffer\<TSource\> Method (IObservable\<TSource\>, Int32)

Indicates each element of an observable sequence into consecutive non-overlapping buffers which are produced based on element count information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IObservable(Of TSource), _
    count As Integer _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim count As Integer
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.Buffer(count)
```

```csharp
public static IObservable<IList<TSource>> Buffer<TSource>(
    this IObservable<TSource> source,
    int count
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<IList<TSource>^>^ Buffer(
    IObservable<TSource>^ source, 
    int count
)
```

```fsharp
static member Buffer : 
        source:IObservable<'TSource> * 
        count:int -> IObservable<IList<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to produce buffers over.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The length of each buffer.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
An observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Buffer Overload](Buffer\Observable.Buffer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Buffer\<TSource\> Method (IObservable\<TSource\>, TimeSpan, IScheduler)

Indicates each element of an observable sequence into consecutive non-overlapping buffers which are produced based on timing information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IObservable(Of TSource), _
    timeSpan As TimeSpan, _
    scheduler As IScheduler _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim timeSpan As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.Buffer(timeSpan, _
    scheduler)
```

```csharp
public static IObservable<IList<TSource>> Buffer<TSource>(
    this IObservable<TSource> source,
    TimeSpan timeSpan,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<IList<TSource>^>^ Buffer(
    IObservable<TSource>^ source, 
    TimeSpan timeSpan, 
    IScheduler^ scheduler
)
```

```fsharp
static member Buffer : 
        source:IObservable<'TSource> * 
        timeSpan:TimeSpan * 
        scheduler:IScheduler -> IObservable<IList<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to produce buffers over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The length of each buffer.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run buffering timers on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Buffer Overload](Buffer\Observable.Buffer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Buffer\<TSource\> Method (IObservable\<TSource\>, TimeSpan, TimeSpan)

Indicates each element of an observable sequence into zero or more buffers which are produced based on timing information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IObservable(Of TSource), _
    timeSpan As TimeSpan, _
    timeShift As TimeSpan _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim timeSpan As TimeSpan
Dim timeShift As TimeSpan
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.Buffer(timeSpan, _
    timeShift)
```

```csharp
public static IObservable<IList<TSource>> Buffer<TSource>(
    this IObservable<TSource> source,
    TimeSpan timeSpan,
    TimeSpan timeShift
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<IList<TSource>^>^ Buffer(
    IObservable<TSource>^ source, 
    TimeSpan timeSpan, 
    TimeSpan timeShift
)
```

```fsharp
static member Buffer : 
        source:IObservable<'TSource> * 
        timeSpan:TimeSpan * 
        timeShift:TimeSpan -> IObservable<IList<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to produce buffers over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The length of each buffer.

- timeShift  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The interval between creation of consecutive buffers.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Buffer Overload](Buffer\Observable.Buffer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








