title: Observable.Generate<TState, TResult>(TState, Func<TState, Boolean>, Func<TState, TState>, Func<TState, TResult>, Func<TState, DateTimeOffset>, IScheduler)
---
# Observable.Generate\<TState, TResult\> Method (TState, Func\<TState, Boolean\>, Func\<TState, TState\>, Func\<TState, TResult\>, Func\<TState, DateTimeOffset\>, IScheduler)

Generates an observable sequence by iterating a state from an initial state until the condition fails.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Generate(Of TState, TResult) ( _
    initialState As TState, _
    condition As Func(Of TState, Boolean), _
    iterate As Func(Of TState, TState), _
    resultSelector As Func(Of TState, TResult), _
    timeSelector As Func(Of TState, DateTimeOffset), _
    scheduler As IScheduler _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim initialState As TState
Dim condition As Func(Of TState, Boolean)
Dim iterate As Func(Of TState, TState)
Dim resultSelector As Func(Of TState, TResult)
Dim timeSelector As Func(Of TState, DateTimeOffset)
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Generate(initialState, _
    condition, iterate, resultSelector, _
    timeSelector, scheduler)
```

```csharp
public static IObservable<TResult> Generate<TState, TResult>(
    TState initialState,
    Func<TState, bool> condition,
    Func<TState, TState> iterate,
    Func<TState, TResult> resultSelector,
    Func<TState, DateTimeOffset> timeSelector,
    IScheduler scheduler
)
```

```c++
public:
generic<typename TState, typename TResult>
static IObservable<TResult>^ Generate(
    TState initialState, 
    Func<TState, bool>^ condition, 
    Func<TState, TState>^ iterate, 
    Func<TState, TResult>^ resultSelector, 
    Func<TState, DateTimeOffset>^ timeSelector, 
    IScheduler^ scheduler
)
```

```fsharp
static member Generate : 
        initialState:'TState * 
        condition:Func<'TState, bool> * 
        iterate:Func<'TState, 'TState> * 
        resultSelector:Func<'TState, 'TResult> * 
        timeSelector:Func<'TState, DateTimeOffset> * 
        scheduler:IScheduler -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The type of state.

- TResult  
  The type of result.

#### Parameters

- initialState  
  Type: TState  
  The initial state.

- condition  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
  The condition to terminate generation.

- iterate  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TState\>  
  The iteration step function.

- resultSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TResult\>  
  The selector function for results produced in the sequence.

- timeSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)\>  
  The time selector function to control the speed of values being produced each iteration.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler on which to run the generator loop.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
The generated sequence.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Generate Overload](Generate/Observable.Generate)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Generate\<TState, TResult\> Method (TState, Func\<TState, Boolean\>, Func\<TState, TState\>, Func\<TState, TResult\>)

Generates an observable sequence by iterating a state from an initial state until the condition fails.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Generate(Of TState, TResult) ( _
    initialState As TState, _
    condition As Func(Of TState, Boolean), _
    iterate As Func(Of TState, TState), _
    resultSelector As Func(Of TState, TResult) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim initialState As TState
Dim condition As Func(Of TState, Boolean)
Dim iterate As Func(Of TState, TState)
Dim resultSelector As Func(Of TState, TResult)
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Generate(initialState, _
    condition, iterate, resultSelector)
```

```csharp
public static IObservable<TResult> Generate<TState, TResult>(
    TState initialState,
    Func<TState, bool> condition,
    Func<TState, TState> iterate,
    Func<TState, TResult> resultSelector
)
```

```c++
public:
generic<typename TState, typename TResult>
static IObservable<TResult>^ Generate(
    TState initialState, 
    Func<TState, bool>^ condition, 
    Func<TState, TState>^ iterate, 
    Func<TState, TResult>^ resultSelector
)
```

```fsharp
static member Generate : 
        initialState:'TState * 
        condition:Func<'TState, bool> * 
        iterate:Func<'TState, 'TState> * 
        resultSelector:Func<'TState, 'TResult> -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The type of state.

- TResult  
  The type of result.

#### Parameters

- initialState  
  Type: TState  
  The initial state.

- condition  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
  The condition to terminate generation.

- iterate  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TState\>  
  The iteration step function.

- resultSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TResult\>  
  The selector function for results produced in the sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
The generated sequence.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Generate Overload](Generate/Observable.Generate)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Generate\<TState, TResult\> Method (TState, Func\<TState, Boolean\>, Func\<TState, TState\>, Func\<TState, TResult\>, Func\<TState, DateTimeOffset\>)

Generates an observable sequence by iterating a state from an initial state until the condition fails.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Generate(Of TState, TResult) ( _
    initialState As TState, _
    condition As Func(Of TState, Boolean), _
    iterate As Func(Of TState, TState), _
    resultSelector As Func(Of TState, TResult), _
    timeSelector As Func(Of TState, DateTimeOffset) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim initialState As TState
Dim condition As Func(Of TState, Boolean)
Dim iterate As Func(Of TState, TState)
Dim resultSelector As Func(Of TState, TResult)
Dim timeSelector As Func(Of TState, DateTimeOffset)
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Generate(initialState, _
    condition, iterate, resultSelector, _
    timeSelector)
```

```csharp
public static IObservable<TResult> Generate<TState, TResult>(
    TState initialState,
    Func<TState, bool> condition,
    Func<TState, TState> iterate,
    Func<TState, TResult> resultSelector,
    Func<TState, DateTimeOffset> timeSelector
)
```

```c++
public:
generic<typename TState, typename TResult>
static IObservable<TResult>^ Generate(
    TState initialState, 
    Func<TState, bool>^ condition, 
    Func<TState, TState>^ iterate, 
    Func<TState, TResult>^ resultSelector, 
    Func<TState, DateTimeOffset>^ timeSelector
)
```

```fsharp
static member Generate : 
        initialState:'TState * 
        condition:Func<'TState, bool> * 
        iterate:Func<'TState, 'TState> * 
        resultSelector:Func<'TState, 'TResult> * 
        timeSelector:Func<'TState, DateTimeOffset> -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The type of state.

- TResult  
  The type of result.

#### Parameters

- initialState  
  Type: TState  
  The initial state.

- condition  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
  The condition to terminate generation.

- iterate  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TState\>  
  The iteration step function.

- resultSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TResult\>  
  The selector function for results produced in the sequence.

- timeSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)\>  
  The time selector function to control the speed of values being produced each iteration.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
The generated sequence.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Generate Overload](Generate/Observable.Generate)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Generate\<TState, TResult\> Method (TState, Func\<TState, Boolean\>, Func\<TState, TState\>, Func\<TState, TResult\>, Func\<TState, TimeSpan\>)

Generates an observable sequence by iterating a state from an initial state until the condition fails.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Generate(Of TState, TResult) ( _
    initialState As TState, _
    condition As Func(Of TState, Boolean), _
    iterate As Func(Of TState, TState), _
    resultSelector As Func(Of TState, TResult), _
    timeSelector As Func(Of TState, TimeSpan) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim initialState As TState
Dim condition As Func(Of TState, Boolean)
Dim iterate As Func(Of TState, TState)
Dim resultSelector As Func(Of TState, TResult)
Dim timeSelector As Func(Of TState, TimeSpan)
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Generate(initialState, _
    condition, iterate, resultSelector, _
    timeSelector)
```

```csharp
public static IObservable<TResult> Generate<TState, TResult>(
    TState initialState,
    Func<TState, bool> condition,
    Func<TState, TState> iterate,
    Func<TState, TResult> resultSelector,
    Func<TState, TimeSpan> timeSelector
)
```

```c++
public:
generic<typename TState, typename TResult>
static IObservable<TResult>^ Generate(
    TState initialState, 
    Func<TState, bool>^ condition, 
    Func<TState, TState>^ iterate, 
    Func<TState, TResult>^ resultSelector, 
    Func<TState, TimeSpan>^ timeSelector
)
```

```fsharp
static member Generate : 
        initialState:'TState * 
        condition:Func<'TState, bool> * 
        iterate:Func<'TState, 'TState> * 
        resultSelector:Func<'TState, 'TResult> * 
        timeSelector:Func<'TState, TimeSpan> -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The type of state.

- TResult  
  The type of result.

#### Parameters

- initialState  
  Type: TState  
  The initial state.

- condition  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
  The condition to terminate generation.

- iterate  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TState\>  
  The iteration step function.

- resultSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TResult\>  
  The selector function for results produced in the sequence.

- timeSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)\>  
  The time selector function to control the speed of values being produced each iteration.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
The generated sequence.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Generate Overload](Generate/Observable.Generate)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Generate\<TState, TResult\> Method (TState, Func\<TState, Boolean\>, Func\<TState, TState\>, Func\<TState, TResult\>, IScheduler)

Generates an observable sequence by iterating a state from an initial state until the condition fails.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Generate(Of TState, TResult) ( _
    initialState As TState, _
    condition As Func(Of TState, Boolean), _
    iterate As Func(Of TState, TState), _
    resultSelector As Func(Of TState, TResult), _
    scheduler As IScheduler _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim initialState As TState
Dim condition As Func(Of TState, Boolean)
Dim iterate As Func(Of TState, TState)
Dim resultSelector As Func(Of TState, TResult)
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Generate(initialState, _
    condition, iterate, resultSelector, _
    scheduler)
```

```csharp
public static IObservable<TResult> Generate<TState, TResult>(
    TState initialState,
    Func<TState, bool> condition,
    Func<TState, TState> iterate,
    Func<TState, TResult> resultSelector,
    IScheduler scheduler
)
```

```c++
public:
generic<typename TState, typename TResult>
static IObservable<TResult>^ Generate(
    TState initialState, 
    Func<TState, bool>^ condition, 
    Func<TState, TState>^ iterate, 
    Func<TState, TResult>^ resultSelector, 
    IScheduler^ scheduler
)
```

```fsharp
static member Generate : 
        initialState:'TState * 
        condition:Func<'TState, bool> * 
        iterate:Func<'TState, 'TState> * 
        resultSelector:Func<'TState, 'TResult> * 
        scheduler:IScheduler -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The type of state.

- TResult  
  The type of result.

#### Parameters

- initialState  
  Type: TState  
  The initial state.

- condition  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
  The condition to terminate generation.

- iterate  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TState\>  
  The iteration step function.

- resultSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TResult\>  
  The selector function for results produced in the sequence.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler on which to run the generator loop.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
The generated sequence.

## Remarks

The Generate operator generates a sequence of the type TState by applying the iterate function to initialState until the condition function returns false for the current state. The resultSelector function is run for each state to generate each item in the resulting sequence.

## Examples

This code example uses the Generate operator to generate a sequence of the integers that are perfect squares less than 1000.

    using System;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //*********************************************************************************************//
          //*** Generate a sequence of integers which are the perfect squares that are less than 100. ***//
          //*********************************************************************************************//
    
          var obs = Observable.Generate(1,                      // Initial state value
                                        x => x * x < 1000,      // The termination condition. Terminate generation when false (the integer squared is not less than 1000).
                                        x => x + 1,             // Iteration step function updates the state and returns the new state. In this case state is incremented by 1.
                                        x => x * x,             // Selector function determines the next resulting value in the sequence. The state of type in is squared.
                                        Scheduler.ThreadPool);  // The ThreadPool scheduler runs the generation on a thread pool thread instead of the main thread.
    
          using (IDisposable handle = obs.Subscribe(x => Console.WriteLine(x)))
          {
            Console.WriteLine("Press ENTER to exit...\n");
            Console.ReadLine();
          }
        }
      }
    }

The following output demonstrates running the example code.

    Press ENTER to exit...
    
    1
    4
    9
    16
    25
    36
    49
    64
    81
    100
    121
    144
    169
    196
    225
    256
    289
    324
    361
    400
    441
    484
    529
    576
    625
    676
    729
    784
    841
    900
    961

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Generate Overload](Generate/Observable.Generate)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)










# Observable.Generate\<TState, TResult\> Method (TState, Func\<TState, Boolean\>, Func\<TState, TState\>, Func\<TState, TResult\>, Func\<TState, TimeSpan\>, IScheduler)

Generates an observable sequence by iterating a state from an initial state until the condition fails.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Generate(Of TState, TResult) ( _
    initialState As TState, _
    condition As Func(Of TState, Boolean), _
    iterate As Func(Of TState, TState), _
    resultSelector As Func(Of TState, TResult), _
    timeSelector As Func(Of TState, TimeSpan), _
    scheduler As IScheduler _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim initialState As TState
Dim condition As Func(Of TState, Boolean)
Dim iterate As Func(Of TState, TState)
Dim resultSelector As Func(Of TState, TResult)
Dim timeSelector As Func(Of TState, TimeSpan)
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Generate(initialState, _
    condition, iterate, resultSelector, _
    timeSelector, scheduler)
```

```csharp
public static IObservable<TResult> Generate<TState, TResult>(
    TState initialState,
    Func<TState, bool> condition,
    Func<TState, TState> iterate,
    Func<TState, TResult> resultSelector,
    Func<TState, TimeSpan> timeSelector,
    IScheduler scheduler
)
```

```c++
public:
generic<typename TState, typename TResult>
static IObservable<TResult>^ Generate(
    TState initialState, 
    Func<TState, bool>^ condition, 
    Func<TState, TState>^ iterate, 
    Func<TState, TResult>^ resultSelector, 
    Func<TState, TimeSpan>^ timeSelector, 
    IScheduler^ scheduler
)
```

```fsharp
static member Generate : 
        initialState:'TState * 
        condition:Func<'TState, bool> * 
        iterate:Func<'TState, 'TState> * 
        resultSelector:Func<'TState, 'TResult> * 
        timeSelector:Func<'TState, TimeSpan> * 
        scheduler:IScheduler -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The type of state.

- TResult  
  The type of result.

#### Parameters

- initialState  
  Type: TState  
  The initial state.

- condition  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
  The condition to terminate generation.

- iterate  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TState\>  
  The iteration step function.

- resultSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TResult\>  
  The selector function for results produced in the sequence.

- timeSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)\>  
  The time selector function to control the speed of values being produced each iteration.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler on which to run the generator loop.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
The generated sequence.

## Remarks

The Generate operator generates a sequence of the type TState by applying the iterate function to initialState until the condition function returns false for the current state. The resultSelector function is run for each state to generate each item in the resulting sequence.

## Examples

This code example uses the Generate operator to generate a sequence of the integers that are perfect squares less than 1000.

    using System;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //*********************************************************************************************//
          //*** Generate a sequence of integers which are the perfect squares that are less than 100. ***//
          //*********************************************************************************************//
    
          var obs = Observable.Generate(1,                             // Initial state value. Starting with 1.
                                        x => x * x < 1000,             // Terminate generation when false (the integer squared is not less than 1000).
                                        x => x + 1,                    // Iteration step function updates the state returning the new state. In this case state is incremented by 1. 
                                        x => x * x,                    // Selector function determines the next resulting value in the sequence. The state of type in is squared.
                                        x => TimeSpan.FromSeconds(1),  // Each item in the sequence delayed by 1 sec.
                                        Scheduler.ThreadPool);         // The ThreadPool scheduler runs the generation on a thread pool thread instead of the main thread.
    
          using (IDisposable handle = obs.Subscribe(x => Console.WriteLine(x)))
          {
            Console.WriteLine("Press ENTER to exit...\n");
            Console.ReadLine();
          }
        }
      }
    }

The following output was generated with the code example.

    Press ENTER to exit...
    
    1
    4
    9
    16
    25
    36
    49
    64
    81
    100
    121
    144
    169
    196
    225
    256
    289
    324
    361
    400
    441
    484
    529
    576
    625
    676
    729
    784
    841
    900
    961

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Generate Overload](Generate/Observable.Generate)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









