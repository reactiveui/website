title: QueryablePattern<T1, T2, T3, T4, T5, T6>.Then<TResult>()
---
# QueryablePattern\<T1, T2, T3, T4, T5, T6\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, T3, T4, T5, T6, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), [T3](QueryablePattern/QueryablePattern(T1,), [T4](QueryablePattern/QueryablePattern(T1,), [T5](QueryablePattern/QueryablePattern(T1,), [T6](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join queryable patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2, T3, T4, T5, T6\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)

# QueryablePattern\<T1, T2, T3\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, T3, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, T3, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, T3, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, T3, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'T3, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), [T3](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join queryable patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2, T3\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)

# QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), [T3](QueryablePattern/QueryablePattern(T1,), [T4](QueryablePattern/QueryablePattern(T1,), [T5](QueryablePattern/QueryablePattern(T1,), [T6](QueryablePattern/QueryablePattern(T1,), [T7](QueryablePattern/QueryablePattern(T1,), [T8](QueryablePattern/QueryablePattern(T1,), [T9](QueryablePattern/QueryablePattern(T1,), [T10](QueryablePattern/QueryablePattern(T1,), [T11](QueryablePattern/QueryablePattern(T1,), [T12](QueryablePattern/QueryablePattern(T1,), [T13](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join queryable patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)

# QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402868)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), [T3](QueryablePattern/QueryablePattern(T1,), [T4](QueryablePattern/QueryablePattern(T1,), [T5](QueryablePattern/QueryablePattern(T1,), [T6](QueryablePattern/QueryablePattern(T1,), [T7](QueryablePattern/QueryablePattern(T1,), [T8](QueryablePattern/QueryablePattern(T1,), [T9](QueryablePattern/QueryablePattern(T1,), [T10](QueryablePattern/QueryablePattern(T1,), [T11](QueryablePattern/QueryablePattern(T1,), [T12](QueryablePattern/QueryablePattern(T1,), [T13](QueryablePattern/QueryablePattern(T1,), [T14](QueryablePattern/QueryablePattern(T1,), [T15](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join queryable patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)

# QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), [T3](QueryablePattern/QueryablePattern(T1,), [T4](QueryablePattern/QueryablePattern(T1,), [T5](QueryablePattern/QueryablePattern(T1,), [T6](QueryablePattern/QueryablePattern(T1,), [T7](QueryablePattern/QueryablePattern(T1,), [T8](QueryablePattern/QueryablePattern(T1,), [T9](QueryablePattern/QueryablePattern(T1,), [T10](QueryablePattern/QueryablePattern(T1,), [T11](QueryablePattern/QueryablePattern(T1,), [T12](QueryablePattern/QueryablePattern(T1,), [T13](QueryablePattern/QueryablePattern(T1,), [T14](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join queryable patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)

# QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), [T3](QueryablePattern/QueryablePattern(T1,), [T4](QueryablePattern/QueryablePattern(T1,), [T5](QueryablePattern/QueryablePattern(T1,), [T6](QueryablePattern/QueryablePattern(T1,), [T7](QueryablePattern/QueryablePattern(T1,), [T8](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join queryable patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)

# QueryablePattern\<T1, T2, T3, T4\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, T3, T4, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, T3, T4, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, T3, T4, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, T3, T4, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'T3, 'T4, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), [T3](QueryablePattern/QueryablePattern(T1,), [T4](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join queryable patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2, T3, T4\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)

# QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402862)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), [T3](QueryablePattern/QueryablePattern(T1,), [T4](QueryablePattern/QueryablePattern(T1,), [T5](QueryablePattern/QueryablePattern(T1,), [T6](QueryablePattern/QueryablePattern(T1,), [T7](QueryablePattern/QueryablePattern(T1,), [T8](QueryablePattern/QueryablePattern(T1,), [T9](QueryablePattern/QueryablePattern(T1,), [T10](QueryablePattern/QueryablePattern(T1,), [T11](QueryablePattern/QueryablePattern(T1,), [T12](QueryablePattern/QueryablePattern(T1,), [T13](QueryablePattern/QueryablePattern(T1,), [T14](QueryablePattern/QueryablePattern(T1,), [T15](QueryablePattern/QueryablePattern(T1,), [T16](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join queryable patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)

# QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), [T3](QueryablePattern/QueryablePattern(T1,), [T4](QueryablePattern/QueryablePattern(T1,), [T5](QueryablePattern/QueryablePattern(T1,), [T6](QueryablePattern/QueryablePattern(T1,), [T7](QueryablePattern/QueryablePattern(T1,), [T8](QueryablePattern/QueryablePattern(T1,), [T9](QueryablePattern/QueryablePattern(T1,), [T10](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join queryable patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)

# QueryablePattern\<T1, T2\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join queryable patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)

# QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), [T3](QueryablePattern/QueryablePattern(T1,), [T4](QueryablePattern/QueryablePattern(T1,), [T5](QueryablePattern/QueryablePattern(T1,), [T6](QueryablePattern/QueryablePattern(T1,), [T7](QueryablePattern/QueryablePattern(T1,), [T8](QueryablePattern/QueryablePattern(T1,), [T9](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)

# QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function…

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), [T3](QueryablePattern/QueryablePattern(T1,), [T4](QueryablePattern/QueryablePattern(T1,), [T5](QueryablePattern/QueryablePattern(T1,), [T6](QueryablePattern/QueryablePattern(T1,), [T7](QueryablePattern/QueryablePattern(T1,), [T8](QueryablePattern/QueryablePattern(T1,), [T9](QueryablePattern/QueryablePattern(T1,), [T10](QueryablePattern/QueryablePattern(T1,), [T11](QueryablePattern/QueryablePattern(T1,), [T12](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join queryable patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)

# QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), [T3](QueryablePattern/QueryablePattern(T1,), [T4](QueryablePattern/QueryablePattern(T1,), [T5](QueryablePattern/QueryablePattern(T1,), [T6](QueryablePattern/QueryablePattern(T1,), [T7](QueryablePattern/QueryablePattern(T1,), [T8](QueryablePattern/QueryablePattern(T1,), [T9](QueryablePattern/QueryablePattern(T1,), [T10](QueryablePattern/QueryablePattern(T1,), [T11](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join queryable patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)

# QueryablePattern\<T1, T2, T3, T4, T5\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, T3, T4, T5, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, T3, T4, T5, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, T3, T4, T5, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, T3, T4, T5, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), [T3](QueryablePattern/QueryablePattern(T1,), [T4](QueryablePattern/QueryablePattern(T1,), [T5](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join queryable patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2, T3, T4, T5\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)

# QueryablePattern\<T1, T2, T3, T4, T5, T6, T7\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim instance As QueryablePattern
Dim selector As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public QueryablePlan<TResult> Then<TResult>(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> selector
)
```

```c++
public:
generic<typename TResult>
QueryablePlan<TResult>^ Then(
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>^>^ selector
)
```

```fsharp
member Then : 
        selector:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'TResult>> -> QueryablePlan<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<[T1](QueryablePattern/QueryablePattern(T1,), [T2](QueryablePattern/QueryablePattern(T1,), [T3](QueryablePattern/QueryablePattern(T1,), [T4](QueryablePattern/QueryablePattern(T1,), [T5](QueryablePattern/QueryablePattern(T1,), [T6](QueryablePattern/QueryablePattern(T1,), [T7](QueryablePattern/QueryablePattern(T1,), TResult\>\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The execution plan for join queryable patterns.

## See Also

#### Reference

[QueryablePattern\<T1, T2, T3, T4, T5, T6, T7\> Class](QueryablePattern/QueryablePattern(T1,)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)
