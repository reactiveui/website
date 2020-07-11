title: Observable.When<TResult>(array<Plan<TResult>[])
---
# Observable.When\<TResult\> Method (array\<Plan\<TResult\>\[\])

Joins together the results from several patterns.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function When(Of TResult) ( _
    ParamArray plans As Plan(Of TResult)() _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim plans As Plan(Of TResult)()
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.When(plans)
```

```csharp
public static IObservable<TResult> When<TResult>(
    params Plan<TResult>[] plans
)
```

```c++
public:
generic<typename TResult>
static IObservable<TResult>^ When(
    ... array<Plan<TResult>^>^ plans
)
```

```fsharp
static member When : 
        plans:Plan<'TResult>[] -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- plans  
  Type: array\<[System.Reactive.Joins.Plan](Plan/Plan(TResult))\<TResult\>\[\]  
  The joining patterns.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
The results from several patterns.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[When Overload](When/Observable.When)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.When\<TResult\> Method (IEnumerable\<Plan\<TResult\>\>)

Joins together the results from several patterns.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function When(Of TResult) ( _
    plans As IEnumerable(Of Plan(Of TResult)) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim plans As IEnumerable(Of Plan(Of TResult))
Dim returnValue As IObservable(Of TResult)

returnValue = plans.When()
```

```csharp
public static IObservable<TResult> When<TResult>(
    this IEnumerable<Plan<TResult>> plans
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static IObservable<TResult>^ When(
    IEnumerable<Plan<TResult>^>^ plans
)
```

```fsharp
static member When : 
        plans:IEnumerable<Plan<'TResult>> -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- plans  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[Plan](Plan/Plan(TResult))\<TResult\>\>  
  The joining patterns.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
The results from several patterns.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[Plan](Plan/Plan(TResult))\<TResult\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[When Overload](When/Observable.When)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
