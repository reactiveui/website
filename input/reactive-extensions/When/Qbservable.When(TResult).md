title: Qbservable.When<TResult>(IQbservableProvider, IEnumerable<QueryablePlan<TResult>>)
---
# Qbservable.When\<TResult\> Method (IQbservableProvider, IEnumerable\<QueryablePlan\<TResult\>\>)

Joins together the results from several patterns.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function When(Of TResult) ( _
    provider As IQbservableProvider, _
    plans As IEnumerable(Of QueryablePlan(Of TResult)) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim plans As IEnumerable(Of QueryablePlan(Of TResult))
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.When(plans)
```

```csharp
public static IQbservable<TResult> When<TResult>(
    this IQbservableProvider provider,
    IEnumerable<QueryablePlan<TResult>> plans
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static IQbservable<TResult>^ When(
    IQbservableProvider^ provider, 
    IEnumerable<QueryablePlan<TResult>^>^ plans
)
```

```fsharp
static member When : 
        provider:IQbservableProvider * 
        plans:IEnumerable<QueryablePlan<'TResult>> -> IQbservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- plans  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>\>  
  The joining patterns.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
The results from several patterns.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[When Overload](When/Qbservable.When)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.When\<TResult\> Method (IQbservableProvider, array\<QueryablePlan\<TResult\>\[\])

Joins together the results from several patterns.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function When(Of TResult) ( _
    provider As IQbservableProvider, _
    ParamArray plans As QueryablePlan(Of TResult)() _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim plans As QueryablePlan(Of TResult)()
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.When(plans)
```

```csharp
public static IQbservable<TResult> When<TResult>(
    this IQbservableProvider provider,
    params QueryablePlan<TResult>[] plans
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static IQbservable<TResult>^ When(
    IQbservableProvider^ provider, 
    ... array<QueryablePlan<TResult>^>^ plans
)
```

```fsharp
static member When : 
        provider:IQbservableProvider * 
        plans:QueryablePlan<'TResult>[] -> IQbservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- plans  
  Type: array\<[System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>\[\]  
  The joining patterns.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
The results from several patterns.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[When Overload](When/Qbservable.When)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
