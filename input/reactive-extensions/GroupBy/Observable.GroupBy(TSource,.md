# Observable.GroupBy\<TSource, TKey, TElement\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>, Func\<TSource, TElement\>, IEqualityComparer\<TKey\>)

Groups the elements of an observable sequence according to a specified key selector function and comparer and selects the resulting elements by using a specified function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupBy(Of TSource, TKey, TElement) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey), _
    elementSelector As Func(Of TSource, TElement), _
    comparer As IEqualityComparer(Of TKey) _
) As IObservable(Of IGroupedObservable(Of TKey, TElement))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim elementSelector As Func(Of TSource, TElement)
Dim comparer As IEqualityComparer(Of TKey)
Dim returnValue As IObservable(Of IGroupedObservable(Of TKey, TElement))

returnValue = source.GroupBy(keySelector, _
    elementSelector, comparer)
```

```csharp
public static IObservable<IGroupedObservable<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector,
    Func<TSource, TElement> elementSelector,
    IEqualityComparer<TKey> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey, typename TElement>
static IObservable<IGroupedObservable<TKey, TElement>^>^ GroupBy(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector, 
    Func<TSource, TElement>^ elementSelector, 
    IEqualityComparer<TKey>^ comparer
)
```

```fsharp
static member GroupBy : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> * 
        elementSelector:Func<'TSource, 'TElement> * 
        comparer:IEqualityComparer<'TKey> -> IObservable<IGroupedObservable<'TKey, 'TElement>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TKey  
  The type key.

- TElement  
  The type element.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence whose elements to group.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  A function to extract the key for each element.

- elementSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TElement\>  
  A function to map each source element to an element in an observable group.

- comparer  
  Type: [System.Collections.Generic.IEqualityComparer](https://msdn.microsoft.com/en-us/library/ms132151)\<TKey\>  
  An equality comparer to compare keys with.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IGroupedObservable](IGroupedObservable\IGroupedObservable(TKey,.md)\<TKey, TElement\>\>  
A sequence of observable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[GroupBy Overload](GroupBy\Observable.GroupBy.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.GroupBy\<TSource, TKey\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>)

Groups the elements of an observable sequence according to a specified key selector function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupBy(Of TSource, TKey) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey) _
) As IObservable(Of IGroupedObservable(Of TKey, TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim returnValue As IObservable(Of IGroupedObservable(Of TKey, TSource))

returnValue = source.GroupBy(keySelector)
```

```csharp
public static IObservable<IGroupedObservable<TKey, TSource>> GroupBy<TSource, TKey>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey>
static IObservable<IGroupedObservable<TKey, TSource>^>^ GroupBy(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector
)
```

```fsharp
static member GroupBy : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> -> IObservable<IGroupedObservable<'TKey, 'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TKey  
  The type key.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence whose elements to group.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  A function to extract the key for each element.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IGroupedObservable](IGroupedObservable\IGroupedObservable(TKey,.md)\<TKey, TSource\>\>  
A sequence of observable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[GroupBy Overload](GroupBy\Observable.GroupBy.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.GroupBy\<TSource, TKey\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>, IEqualityComparer\<TKey\>)

Groups the elements of an observable sequence according to a specified key selector function and comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupBy(Of TSource, TKey) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey), _
    comparer As IEqualityComparer(Of TKey) _
) As IObservable(Of IGroupedObservable(Of TKey, TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim comparer As IEqualityComparer(Of TKey)
Dim returnValue As IObservable(Of IGroupedObservable(Of TKey, TSource))

returnValue = source.GroupBy(keySelector, _
    comparer)
```

```csharp
public static IObservable<IGroupedObservable<TKey, TSource>> GroupBy<TSource, TKey>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector,
    IEqualityComparer<TKey> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey>
static IObservable<IGroupedObservable<TKey, TSource>^>^ GroupBy(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector, 
    IEqualityComparer<TKey>^ comparer
)
```

```fsharp
static member GroupBy : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> * 
        comparer:IEqualityComparer<'TKey> -> IObservable<IGroupedObservable<'TKey, 'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TKey  
  The type key.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence whose elements to group.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  A function to extract the key for each element.

- comparer  
  Type: [System.Collections.Generic.IEqualityComparer](https://msdn.microsoft.com/en-us/library/ms132151)\<TKey\>  
  An equality comparer to compare keys with.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IGroupedObservable](IGroupedObservable\IGroupedObservable(TKey,.md)\<TKey, TSource\>\>  
A sequence of observable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[GroupBy Overload](GroupBy\Observable.GroupBy.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.GroupBy\<TSource, TKey, TElement\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>, Func\<TSource, TElement\>)

Groups the elements of an observable sequence and selects the resulting elements by using a specified function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupBy(Of TSource, TKey, TElement) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey), _
    elementSelector As Func(Of TSource, TElement) _
) As IObservable(Of IGroupedObservable(Of TKey, TElement))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim elementSelector As Func(Of TSource, TElement)
Dim returnValue As IObservable(Of IGroupedObservable(Of TKey, TElement))

returnValue = source.GroupBy(keySelector, _
    elementSelector)
```

```csharp
public static IObservable<IGroupedObservable<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector,
    Func<TSource, TElement> elementSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey, typename TElement>
static IObservable<IGroupedObservable<TKey, TElement>^>^ GroupBy(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector, 
    Func<TSource, TElement>^ elementSelector
)
```

```fsharp
static member GroupBy : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> * 
        elementSelector:Func<'TSource, 'TElement> -> IObservable<IGroupedObservable<'TKey, 'TElement>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TKey  
  The type key.

- TElement  
  The type element.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence whose elements to group.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  A function to extract the key for each element.

- elementSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TElement\>  
  A function to map each source element to an element in an observable group.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IGroupedObservable](IGroupedObservable\IGroupedObservable(TKey,.md)\<TKey, TElement\>\>  
A sequence of observable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

The GroupBy operator is used to group a source sequence of items into groups based on a key value. Each key value is the result of the keySelector function and can be derived from each item in the source sequence. The result of the GroupBy operator is a sequence of grouped items represented by a sequence of [IGroupedObservable\<TKey, TElement\>](IGroupedObservable\IGroupedObservable(TKey,.md). IGroupedObservable exposes a key property which identifies the grouped sequence. The actual items in the resulting grouped sequences are controlled by the result of the elementSelector function applied to each source item.

## Examples

This example code generates the sequence of event log records which have occurred within the last six hours. The GroupBy operator is then used with a LINQ statement to group event records which are errors based on the LevelDisplayName property evaluating to “Error” or “Critical”. If this is true, the result of the key selector function is true, resulting in the event record being grouped in the error group. The result of the GroupBy operator is an IGroupedObservable\<Boolean, EventRecord\>. This sequence of IGroupedObservable is subscribed to access the grouped error event log entries. The code examines the sequence of groups to find the group where the key value is true. The key value of true identifies the error group. That error group is then subscribed to and the error event log entries are written to the console window.

    using System;
    using System.Reactive.Linq;
    using System.Reactive.Concurrency;
    using System.Diagnostics.Eventing.Reader;
    using System.IO;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //************************************************************************************************//
          //*** Generate a sequence of event log entries that have been written to the system event log  ***//
          //*** within the last 6 hours.                                                                 ***//
          //************************************************************************************************//
    
          const int eventLogTimeSpan = 6;
          EventLogReader sysEventLog = new EventLogReader("System");
          
    
          //****************************************************************************************************//
          //*** Start with the last entry in the event log.                                                  ***//
          //*** Stop on an event generated at a time more than the EventLogTimeSpan (6 hrs in this example). ***//
          //*** Each iteration function will step back one entry.                                            ***//
          //*** The resultSelector function will just select each entry for inclusion in the sequence.       ***//
          //*** The ThreadPool schedule schedules the processing of the event log on a thread pool thread    ***//
          //****************************************************************************************************//
    
          sysEventLog.Seek(SeekOrigin.End,0);
          EventRecord lastEntry = sysEventLog.ReadEvent();
          var eventLogEntrySeq = Observable.Generate(lastEntry,                                                      
                                                     x => (DateTime.Now - x.TimeCreated) < TimeSpan.FromHours(eventLogTimeSpan),  
                                                     x => {sysEventLog.Seek(x.Bookmark,-1); return sysEventLog.ReadEvent();},                          
                                                     x => x,                                                         
                                                     Scheduler.ThreadPool);                                          
    
    
          //************************************************************************************************************//
          //*** Use the GroupBy operator with LINQ to group the sequence into entries that are errors (key=true) and ***//
          //*** those that are not errors (key=false).                                                               ***//
          //************************************************************************************************************//
    
          var eventLogGroupedSeq =
            from entry in eventLogEntrySeq
            group entry by (entry.LevelDisplayName == "Error") || (entry.LevelDisplayName == "Critical") into groupedEntries
            select groupedEntries;
    
    
          //***************************************************************************************//
          //*** eventLogGroupedSeq is a IGroupedObservable<Boolean, EventRecord>. Subscribing   ***//
          //*** will return a sequence of the groups. For this example, we only want the group  ***//
          //*** where the key is true indicating the Error entries group. So we then subscribe  ***//
          //*** to that grouped sequence to write the error entries that occurred within the    ***//
          //*** last 6 hours.                                                                   ***//
          //***************************************************************************************//
    
          eventLogGroupedSeq.Subscribe(groupedSeq =>
          {
            if (groupedSeq.Key == true)
            {
              groupedSeq.Subscribe(evtEntry => Console.WriteLine("ID : {0}\n" +
                                                                 "Type : {1}\n" + 
                                                                 "Source: {2}\n" + 
                                                                 "Time Generated: {3}\n" + 
                                                                 "Message: {4}\n",
                                                                 evtEntry.Id,
                                                                 evtEntry.LevelDisplayName,
                                                                 evtEntry.ProviderName,
                                                                 evtEntry.TimeCreated.ToString(),
                                                                 evtEntry.FormatDescription()));
            }  
          });
    
          Console.WriteLine("\nDisplaying error entries from the system event log\n" +
                            "that occurred within the last {0} hours...\n\n" +
                            "Press ENTER to exit...\n",eventLogTimeSpan);
          Console.ReadLine();
        }
      }
    }

The following output was generated with the example code.

    Displaying error entries from the system event log
    that occurred within the last 6 hours...
    
    Press ENTER to exit...
    
    ID : 34001
    Type : Error
    Source: Microsoft-Windows-SharedAccess_NAT
    Time Generated: 5/31/2011 4:39:18 AM
    Message: The ICS_IPV6 failed to configure IPv6 stack.
    
    ID : 34001
    Type : Error
    Source: Microsoft-Windows-SharedAccess_NAT
    Time Generated: 5/31/2011 4:01:36 AM
    Message: The ICS_IPV6 failed to configure IPv6 stack.
    
    ID : 34001
    Type : Error
    Source: Microsoft-Windows-SharedAccess_NAT
    Time Generated: 5/31/2011 3:49:29 AM
    Message: The ICS_IPV6 failed to configure IPv6 stack.
    
    ID : 34001
    Type : Error
    Source: Microsoft-Windows-SharedAccess_NAT
    Time Generated: 5/31/2011 3:11:47 AM
    Message: The ICS_IPV6 failed to configure IPv6 stack.
    
    ID : 34001
    Type : Error
    Source: Microsoft-Windows-SharedAccess_NAT
    Time Generated: 5/31/2011 2:59:40 AM
    Message: The ICS_IPV6 failed to configure IPv6 stack.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[GroupBy Overload](GroupBy\Observable.GroupBy.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)










