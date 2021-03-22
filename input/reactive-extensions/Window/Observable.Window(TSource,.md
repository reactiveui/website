title: Observable.Window<TSource, TWindowClosing>(IObservable<TSource>, Func<IObservable<TWindowClosing>>)
---
# Observable.Window\<TSource, TWindowClosing\> Method (IObservable\<TSource\>, Func\<IObservable\<TWindowClosing\>\>)

Projects each element of an observable sequence into consecutive non-overlapping windows.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Window(Of TSource, TWindowClosing) ( _
    source As IObservable(Of TSource), _
    windowClosingSelector As Func(Of IObservable(Of TWindowClosing)) _
) As IObservable(Of IObservable(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim windowClosingSelector As Func(Of IObservable(Of TWindowClosing))
Dim returnValue As IObservable(Of IObservable(Of TSource))

returnValue = source.Window(windowClosingSelector)
```

```csharp
public static IObservable<IObservable<TSource>> Window<TSource, TWindowClosing>(
    this IObservable<TSource> source,
    Func<IObservable<TWindowClosing>> windowClosingSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TWindowClosing>
static IObservable<IObservable<TSource>^>^ Window(
    IObservable<TSource>^ source, 
    Func<IObservable<TWindowClosing>^>^ windowClosingSelector
)
```

```fsharp
static member Window : 
        source:IObservable<'TSource> * 
        windowClosingSelector:Func<IObservable<'TWindowClosing>> -> IObservable<IObservable<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TWindowClosing  
  The type of window closing.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to produce windows over.

- windowClosingSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TWindowClosing\>\>  
  A function invoked to define the boundaries of the produced windows.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
An observable sequence of windows.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

The Window operator breaks up an observable sequence into consecutive non-overlapping windows. The end of the current window and start of the next window is controlled by an observable sequence which is the result of the **windowClosingSelect** function which is passed as an input parameter to the operator. The operator could be used to group a set of events into a window. For example, states of a transaction could be the main sequence being observed. Those states could include: Preparing, Prepared, Active, and Committed/Aborted. The main sequence could include all of those states are they occur in that order. The windowClosingSelect function could return an observable sequence that only produces a value on the Committed or Abort states. This would close the window that represented transaction events for a particular transaction.

## Examples

The following simple example breaks up a sequence of integers into consecutive non-overlapping windows. The end of the current window and start of the next window is controlled by an observable sequence of integers produced by the Interval operator every six seconds. Since the main observable sequence is producing an item every seconds, each window will have six items in it. The example code writes each window of integers to the console window along with a timestamp that shows a new window is opened every six seconds.

    using System;
    using System.Reactive.Linq;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //*********************************************************************************************//
          //*** The mainSequence produces a new long integer from the Interval operator every sec but ***//
          //*** this sequence is broken up by the Window operator into subsets like a windowed        ***//
          //*** view of the sequence. The time when each window stops and the next window starts is   ***//
          //*** controlled by the IObservable<TWindowClosing> named seqWindowControl. It is returned  ***//
          //*** by the lambda expression which is passed to the Window operator. In this case it      ***//
          //**  returns another IObservable<long> generated by the Interval operator. So whenever     ***//
          //*** seqWindowControl produces a item, the current window into the mainSequence stops and  ***//
          //*** a new window starts.                                                                  ***//
          //*********************************************************************************************//
    
          var mainSequence = Observable.Interval(TimeSpan.FromSeconds(1));
    
          var seqWindowed = mainSequence.Window(() => 
          {
            var seqWindowControl = Observable.Interval(TimeSpan.FromSeconds(6));
            return seqWindowControl;
          });
    
    
          //*********************************************************************************************//
          //*** A subscription to seqWindowed will provide a new IObservable<long> every 6 secs.      ***//
          //***                                                                                       ***//
          //*** Create a subscription to each window into the main sequence and list the values along ***//
          //*** with the time the window was opened and the previous window was closed.               ***//
          //*********************************************************************************************//
          
          seqWindowed.Subscribe(seqWindow => 
          {
            Console.WriteLine("\nA new window into the main sequence has opened: {0}\n",DateTime.Now.ToString());
            seqWindow.Subscribe(x =>
            {
              Console.WriteLine("Integer : {0}", x);
            });
          });
    
          Console.ReadLine();
        }
      }
    }

The following output was generated by the example code.

    A new window into the main sequence has opened: 6/1/2011 8:48:43 PM
    
    Integer : 0
    Integer : 1
    Integer : 2
    Integer : 3
    Integer : 4
    Integer : 5
    
    A new window into the main sequence has opened: 6/1/2011 8:48:49 PM
    
    Integer : 6
    Integer : 7
    Integer : 8
    Integer : 9
    Integer : 10
    Integer : 11
    
    A new window into the main sequence has opened: 6/1/2011 8:48:55 PM
    
    Integer : 12
    Integer : 13
    Integer : 14
    Integer : 15
    Integer : 16
    Integer : 17
    
    A new window into the main sequence has opened: 6/1/2011 8:49:02 PM
    
    Integer : 18
    Integer : 19
    Integer : 20
    Integer : 21
    Integer : 22
    Integer : 23

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Window Overload](Window/Observable.Window)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Window\<TSource, TWindowOpening, TWindowClosing\> Method (IObservable\<TSource\>, IObservable\<TWindowOpening\>, Func\<TWindowOpening, IObservable\<TWindowClosing\>\>)

Projects each element of an observable sequence into zero or more windows.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Window(Of TSource, TWindowOpening, TWindowClosing) ( _
    source As IObservable(Of TSource), _
    windowOpenings As IObservable(Of TWindowOpening), _
    windowClosingSelector As Func(Of TWindowOpening, IObservable(Of TWindowClosing)) _
) As IObservable(Of IObservable(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim windowOpenings As IObservable(Of TWindowOpening)
Dim windowClosingSelector As Func(Of TWindowOpening, IObservable(Of TWindowClosing))
Dim returnValue As IObservable(Of IObservable(Of TSource))

returnValue = source.Window(windowOpenings, _
    windowClosingSelector)
```

```csharp
public static IObservable<IObservable<TSource>> Window<TSource, TWindowOpening, TWindowClosing>(
    this IObservable<TSource> source,
    IObservable<TWindowOpening> windowOpenings,
    Func<TWindowOpening, IObservable<TWindowClosing>> windowClosingSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TWindowOpening, typename TWindowClosing>
static IObservable<IObservable<TSource>^>^ Window(
    IObservable<TSource>^ source, 
    IObservable<TWindowOpening>^ windowOpenings, 
    Func<TWindowOpening, IObservable<TWindowClosing>^>^ windowClosingSelector
)
```

```fsharp
static member Window : 
        source:IObservable<'TSource> * 
        windowOpenings:IObservable<'TWindowOpening> * 
        windowClosingSelector:Func<'TWindowOpening, IObservable<'TWindowClosing>> -> IObservable<IObservable<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TWindowOpening  
  The type of window opening.

- TWindowClosing  
  The type of window closing.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to produce windows over.

- windowOpenings  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TWindowOpening\>  
  The observable sequence whose elements denote the creation of new windows.

- windowClosingSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TWindowOpening, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TWindowClosing\>\>  
  A function invoked to define the closing of each produced window.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
An observable sequence of windows.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Window Overload](Window/Observable.Window)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
