# Observable.Where\<TSource\> Method (IObservable\<TSource\>, Func\<TSource, Int32, Boolean\>)

Filters the elements of an observable sequence based on a predicate by incorporating the element's index.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Where(Of TSource) ( _
    source As IObservable(Of TSource), _
    predicate As Func(Of TSource, Integer, Boolean) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim predicate As Func(Of TSource, Integer, Boolean)
Dim returnValue As IObservable(Of TSource)

returnValue = source.Where(predicate)
```

```csharp
public static IObservable<TSource> Where<TSource>(
    this IObservable<TSource> source,
    Func<TSource, int, bool> predicate
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Where(
    IObservable<TSource>^ source, 
    Func<TSource, int, bool>^ predicate
)
```

```fsharp
static member Where : 
        source:IObservable<'TSource> * 
        predicate:Func<'TSource, int, bool> -> IObservable<'TSource> 
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
  An observable sequence whose elements to filter.

- predicate  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TSource, [Int32](https://msdn.microsoft.com/en-us/library/td2s409d), [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
  A function to test each source element for a condition; the second parameter of the function represents the index of the source element.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence that contains elements from the input sequence that satisfy the condition.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Where Overload](Where\Observable.Where.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Where\<TSource\> Method (IObservable\<TSource\>, Func\<TSource, Boolean\>)

Filters the elements of an observable sequence based on a predicate.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Where(Of TSource) ( _
    source As IObservable(Of TSource), _
    predicate As Func(Of TSource, Boolean) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim predicate As Func(Of TSource, Boolean)
Dim returnValue As IObservable(Of TSource)

returnValue = source.Where(predicate)
```

```csharp
public static IObservable<TSource> Where<TSource>(
    this IObservable<TSource> source,
    Func<TSource, bool> predicate
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Where(
    IObservable<TSource>^ source, 
    Func<TSource, bool>^ predicate
)
```

```fsharp
static member Where : 
        source:IObservable<'TSource> * 
        predicate:Func<'TSource, bool> -> IObservable<'TSource> 
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
  An observable sequence whose elements to filter.

- predicate  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
  A function to test each source element for a condition.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence that contains elements from the input sequence that satisfy the condition.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

The Where operator allows you to define a predicate function to test each item in a sequence. Each item from the source sequence which causes the predicate function to return false is filtered from the resulting sequence.

## Examples

The following example uses the Where operator with a Language Integerated Query (LINQ) to filter the integer sequence so that only even integers are produced for the sequence.

    using System;
    using System.Reactive.Linq;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //*********************************************************************************************//
          //*** The mainSequence produces a new long integer from the Interval operator every sec.    ***//
          //*********************************************************************************************//
    
          var mainSequence = Observable.Interval(TimeSpan.FromSeconds(1));
    
    
          //*********************************************************************************************//
          //*** This LINQ statement uses the Where operator to filter the integers in the sequence so ***//
          //*** that only the even integers are returned.                                             ***//
          //***                                                                                       ***//
          //*** you could also call the method explicitly. For example...                             ***//
          //***                                                                                       ***//
          //*** var seqWhereEven = mainSequence.Where(x => x % 2 == 0);                               ***//
          //***                                                                                       ***//
          //*********************************************************************************************//
    
          var seqWhereEven = from i in mainSequence
                             where i % 2 == 0 
                             select i;
    
    
          //*********************************************************************************************//
          //*** Create a subscription and write each of the even integers to the console window.      ***//
          //*********************************************************************************************//
    
          seqWhereEven.Subscribe(x => Console.WriteLine(x)); 
          Console.ReadLine();
        }
      }
    }

The following output was generated by the example code.

    0
    2
    4
    6
    8
    10
    12
    14
    16

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Where Overload](Where\Observable.Where.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
