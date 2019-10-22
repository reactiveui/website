title: Observable.GroupByUntil<TSource, TKey, TDuration>(IObservable<TSource>, Func<TSource, TKey>, Func<IGroupedObservable<TKey, TSource>, IObservable<TDuration>>, IEqualityComparer<TKey>)
---
# Observable.GroupByUntil\<TSource, TKey, TDuration\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>, Func\<IGroupedObservable\<TKey, TSource\>, IObservable\<TDuration\>\>, IEqualityComparer\<TKey\>)

Groups the elements of an observable sequence according to a specified key selector function and comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupByUntil(Of TSource, TKey, TDuration) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey), _
    durationSelector As Func(Of IGroupedObservable(Of TKey, TSource), IObservable(Of TDuration)), _
    comparer As IEqualityComparer(Of TKey) _
) As IObservable(Of IGroupedObservable(Of TKey, TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim durationSelector As Func(Of IGroupedObservable(Of TKey, TSource), IObservable(Of TDuration))
Dim comparer As IEqualityComparer(Of TKey)
Dim returnValue As IObservable(Of IGroupedObservable(Of TKey, TSource))

returnValue = source.GroupByUntil(keySelector, _
    durationSelector, comparer)
```

```csharp
public static IObservable<IGroupedObservable<TKey, TSource>> GroupByUntil<TSource, TKey, TDuration>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector,
    Func<IGroupedObservable<TKey, TSource>, IObservable<TDuration>> durationSelector,
    IEqualityComparer<TKey> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey, typename TDuration>
static IObservable<IGroupedObservable<TKey, TSource>^>^ GroupByUntil(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector, 
    Func<IGroupedObservable<TKey, TSource>^, IObservable<TDuration>^>^ durationSelector, 
    IEqualityComparer<TKey>^ comparer
)
```

```fsharp
static member GroupByUntil : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> * 
        durationSelector:Func<IGroupedObservable<'TKey, 'TSource>, IObservable<'TDuration>> * 
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

- TDuration  
  The type duration.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence whose elements to group.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  A function to extract the key for each element.

- durationSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IGroupedObservable](IGroupedObservable\IGroupedObservable(TKey,.md)\<TKey, TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TDuration\>\>  
  A function to signal the expiration of a group.

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

[GroupByUntil Overload](GroupByUntil\Observable.GroupByUntil.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.GroupByUntil\<TSource, TKey, TElement, TDuration\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>, Func\<TSource, TElement\>, Func\<IGroupedObservable\<TKey, TElement\>, IObservable\<TDuration\>\>)

Groups the elements of an observable sequence according to a specified key selector function and selects the resulting elements by using a specified function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupByUntil(Of TSource, TKey, TElement, TDuration) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey), _
    elementSelector As Func(Of TSource, TElement), _
    durationSelector As Func(Of IGroupedObservable(Of TKey, TElement), IObservable(Of TDuration)) _
) As IObservable(Of IGroupedObservable(Of TKey, TElement))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim elementSelector As Func(Of TSource, TElement)
Dim durationSelector As Func(Of IGroupedObservable(Of TKey, TElement), IObservable(Of TDuration))
Dim returnValue As IObservable(Of IGroupedObservable(Of TKey, TElement))

returnValue = source.GroupByUntil(keySelector, _
    elementSelector, durationSelector)
```

```csharp
public static IObservable<IGroupedObservable<TKey, TElement>> GroupByUntil<TSource, TKey, TElement, TDuration>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector,
    Func<TSource, TElement> elementSelector,
    Func<IGroupedObservable<TKey, TElement>, IObservable<TDuration>> durationSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey, typename TElement, typename TDuration>
static IObservable<IGroupedObservable<TKey, TElement>^>^ GroupByUntil(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector, 
    Func<TSource, TElement>^ elementSelector, 
    Func<IGroupedObservable<TKey, TElement>^, IObservable<TDuration>^>^ durationSelector
)
```

```fsharp
static member GroupByUntil : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> * 
        elementSelector:Func<'TSource, 'TElement> * 
        durationSelector:Func<IGroupedObservable<'TKey, 'TElement>, IObservable<'TDuration>> -> IObservable<IGroupedObservable<'TKey, 'TElement>> 
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

- TDuration  
  The type duration.

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

- durationSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IGroupedObservable](IGroupedObservable\IGroupedObservable(TKey,.md)\<TKey, TElement\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TDuration\>\>  
  A function to signal the expiration of a group.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IGroupedObservable](IGroupedObservable\IGroupedObservable(TKey,.md)\<TKey, TElement\>\>  
A sequence of observable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

The GroupByUntil operator separates an observable sequence into observable grouped sequences which have an expiration time span set by the durationSelector function.

## Examples

This simple example demonstrates using the GroupByUntil operator to group a continuous sequence of random integers into groups that expire after ten seconds. Pressing ENTER will exit the example code.

    using System;
    using System.Reactive.Linq;
    using System.Reactive.Concurrency;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //*****************************************************************************************//
          //*** Generate a sequence of random integers less than 100 every seconds continuously.  ***//
          //*****************************************************************************************//
    
          Random rand = new Random();
    
          var obs = Observable.Generate(rand.Next(100),                      // Initial value
                                        x => true,                           // The termination condition. Never terminate.
                                        x => rand.Next(100),                 // Iteration step function 
                                        x => x,                              // Selector function 
                                        x => TimeSpan.FromMilliseconds(500), // timeSelector Delay function
                                        Scheduler.ThreadPool);               // Schedule on a .NET threadpool thread 
    
    
          //*************************************************************************************//
          //*** Generate a groups of the random integers in the sequence that are in the 80s. ***//
          //*** Each grouping has an expiration set to expire in 10 seconds. This is set by   ***//
          //*** the durationSelector function which returns an observable sequence of time    ***//
          //*** spans set to 10 seconds.                                                      ***//
          //*************************************************************************************//
    
          int groupExpirationSec = 10;
          var obsEighties = obs.GroupByUntil(x => (x > 79) && (x < 90),
                                             x => x,
                                             x => Observable.Timer(TimeSpan.FromSeconds(groupExpirationSec)));
    
    
          //*************************************************************************************//
          //*** Subscribe to the grouped sequences. Each grouped sequence will expire after   ***//
          //*** 10 seconds by completing the sequence. Display timings with the sequence so   ***//
          //*** this is evident.                                                              ***//
          //*************************************************************************************//
    
          obsEighties.Subscribe(groupedObs => 
          {
    
            if (groupedObs.Key == true) // True for eighties group
            {
              Console.WriteLine("\nNew eighties group\nThis group should expire at {0}\n",
                                (DateTime.Now + TimeSpan.FromSeconds(groupExpirationSec)).ToLongTimeString());
    
              groupedObs.Subscribe(x => Console.WriteLine(x),
                                   () => Console.WriteLine("\nGrouped sequence completed or expired. {0}\n",
                                                           DateTime.Now.ToLongTimeString()));
            }
          });
    
          Console.ReadLine();
        }
      }
    }

The following output was generated with the example code.

    New eighties group
    This group should expire at 5:10:22 PM
    
    86
    88
    81
    81
    89
    
    Grouped sequence completed or expired. 5:10:22 PM
    
    
    New eighties group
    This group should expire at 5:10:33 PM
    
    80
    88
    80
    88
    
    Grouped sequence completed or expired. 5:10:33 PM
    
    
    New eighties group
    This group should expire at 5:10:50 PM
    
    81
    83
    83
    82
    81
    
    Grouped sequence completed or expired. 5:10:50 PM
    
    
    New eighties group
    This group should expire at 5:11:01 PM
    
    85
    86
    88
    
    Grouped sequence completed or expired. 5:11:01 PM

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[GroupByUntil Overload](GroupByUntil\Observable.GroupByUntil.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)











# Observable.GroupByUntil\<TSource, TKey, TElement, TDuration\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>, Func\<TSource, TElement\>, Func\<IGroupedObservable\<TKey, TElement\>, IObservable\<TDuration\>\>, IEqualityComparer\<TKey\>)

Groups the elements of an observable sequence according to a specified key selector function and comparer and selects the resulting elements by using a specified function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupByUntil(Of TSource, TKey, TElement, TDuration) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey), _
    elementSelector As Func(Of TSource, TElement), _
    durationSelector As Func(Of IGroupedObservable(Of TKey, TElement), IObservable(Of TDuration)), _
    comparer As IEqualityComparer(Of TKey) _
) As IObservable(Of IGroupedObservable(Of TKey, TElement))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim elementSelector As Func(Of TSource, TElement)
Dim durationSelector As Func(Of IGroupedObservable(Of TKey, TElement), IObservable(Of TDuration))
Dim comparer As IEqualityComparer(Of TKey)
Dim returnValue As IObservable(Of IGroupedObservable(Of TKey, TElement))

returnValue = source.GroupByUntil(keySelector, _
    elementSelector, durationSelector, _
    comparer)
```

```csharp
public static IObservable<IGroupedObservable<TKey, TElement>> GroupByUntil<TSource, TKey, TElement, TDuration>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector,
    Func<TSource, TElement> elementSelector,
    Func<IGroupedObservable<TKey, TElement>, IObservable<TDuration>> durationSelector,
    IEqualityComparer<TKey> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey, typename TElement, typename TDuration>
static IObservable<IGroupedObservable<TKey, TElement>^>^ GroupByUntil(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector, 
    Func<TSource, TElement>^ elementSelector, 
    Func<IGroupedObservable<TKey, TElement>^, IObservable<TDuration>^>^ durationSelector, 
    IEqualityComparer<TKey>^ comparer
)
```

```fsharp
static member GroupByUntil : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> * 
        elementSelector:Func<'TSource, 'TElement> * 
        durationSelector:Func<IGroupedObservable<'TKey, 'TElement>, IObservable<'TDuration>> * 
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

- TDuration  
  The type duration.

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

- durationSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IGroupedObservable](IGroupedObservable\IGroupedObservable(TKey,.md)\<TKey, TElement\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TDuration\>\>  
  A function to signal the expiration of a group.

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

[GroupByUntil Overload](GroupByUntil\Observable.GroupByUntil.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.GroupByUntil\<TSource, TKey, TDuration\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>, Func\<IGroupedObservable\<TKey, TSource\>, IObservable\<TDuration\>\>)

Groups the elements of an observable sequence according to a specified key selector function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupByUntil(Of TSource, TKey, TDuration) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey), _
    durationSelector As Func(Of IGroupedObservable(Of TKey, TSource), IObservable(Of TDuration)) _
) As IObservable(Of IGroupedObservable(Of TKey, TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim durationSelector As Func(Of IGroupedObservable(Of TKey, TSource), IObservable(Of TDuration))
Dim returnValue As IObservable(Of IGroupedObservable(Of TKey, TSource))

returnValue = source.GroupByUntil(keySelector, _
    durationSelector)
```

```csharp
public static IObservable<IGroupedObservable<TKey, TSource>> GroupByUntil<TSource, TKey, TDuration>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector,
    Func<IGroupedObservable<TKey, TSource>, IObservable<TDuration>> durationSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey, typename TDuration>
static IObservable<IGroupedObservable<TKey, TSource>^>^ GroupByUntil(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector, 
    Func<IGroupedObservable<TKey, TSource>^, IObservable<TDuration>^>^ durationSelector
)
```

```fsharp
static member GroupByUntil : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> * 
        durationSelector:Func<IGroupedObservable<'TKey, 'TSource>, IObservable<'TDuration>> -> IObservable<IGroupedObservable<'TKey, 'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TKey  
  The type key.

- TDuration  
  The type duration.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence whose elements to group.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  A function to extract the key for each element.

- durationSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IGroupedObservable](IGroupedObservable\IGroupedObservable(TKey,.md)\<TKey, TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TDuration\>\>  
  A function to signal the expiration of a group.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IGroupedObservable](IGroupedObservable\IGroupedObservable(TKey,.md)\<TKey, TSource\>\>  
A sequence of observable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[GroupByUntil Overload](GroupByUntil\Observable.GroupByUntil.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








