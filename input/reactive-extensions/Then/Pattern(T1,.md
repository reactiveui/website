title: Pattern<T1, T2, T3, T4, T5>.Then<TResult>()
---
# Pattern\<T1, T2, T3, T4, T5\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, T3, T4, T5, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, T3, T4, T5, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, T3, T4, T5, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, T3, T4, T5, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), [T3](Pattern\Pattern(T1,.md), [T4](Pattern\Pattern(T1,.md), [T5](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2, T3, T4, T5\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)

# Pattern\<T1, T2, T3, T4, T5, T6, T7, T8\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), [T3](Pattern\Pattern(T1,.md), [T4](Pattern\Pattern(T1,.md), [T5](Pattern\Pattern(T1,.md), [T6](Pattern\Pattern(T1,.md), [T7](Pattern\Pattern(T1,.md), [T8](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2, T3, T4, T5, T6, T7, T8\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)

# Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402862)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), [T3](Pattern\Pattern(T1,.md), [T4](Pattern\Pattern(T1,.md), [T5](Pattern\Pattern(T1,.md), [T6](Pattern\Pattern(T1,.md), [T7](Pattern\Pattern(T1,.md), [T8](Pattern\Pattern(T1,.md), [T9](Pattern\Pattern(T1,.md), [T10](Pattern\Pattern(T1,.md), [T11](Pattern\Pattern(T1,.md), [T12](Pattern\Pattern(T1,.md), [T13](Pattern\Pattern(T1,.md), [T14](Pattern\Pattern(T1,.md), [T15](Pattern\Pattern(T1,.md), [T16](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)

# Pattern\<T1, T2\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)

# Pattern\<T1, T2, T3\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, T3, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, T3, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, T3, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, T3, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'T3, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), [T3](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2, T3\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)

# Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), [T3](Pattern\Pattern(T1,.md), [T4](Pattern\Pattern(T1,.md), [T5](Pattern\Pattern(T1,.md), [T6](Pattern\Pattern(T1,.md), [T7](Pattern\Pattern(T1,.md), [T8](Pattern\Pattern(T1,.md), [T9](Pattern\Pattern(T1,.md), [T10](Pattern\Pattern(T1,.md), [T11](Pattern\Pattern(T1,.md), [T12](Pattern\Pattern(T1,.md), [T13](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)

# Pattern\<T1, T2, T3, T4, T5, T6\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, T3, T4, T5, T6, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, T3, T4, T5, T6, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, T3, T4, T5, T6, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, T3, T4, T5, T6, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), [T3](Pattern\Pattern(T1,.md), [T4](Pattern\Pattern(T1,.md), [T5](Pattern\Pattern(T1,.md), [T6](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2, T3, T4, T5, T6\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)

# Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), [T3](Pattern\Pattern(T1,.md), [T4](Pattern\Pattern(T1,.md), [T5](Pattern\Pattern(T1,.md), [T6](Pattern\Pattern(T1,.md), [T7](Pattern\Pattern(T1,.md), [T8](Pattern\Pattern(T1,.md), [T9](Pattern\Pattern(T1,.md), [T10](Pattern\Pattern(T1,.md), [T11](Pattern\Pattern(T1,.md), [T12](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)

# Pattern\<T1, T2, T3, T4, T5, T6, T7\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, T3, T4, T5, T6, T7, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, T3, T4, T5, T6, T7, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, T3, T4, T5, T6, T7, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), [T3](Pattern\Pattern(T1,.md), [T4](Pattern\Pattern(T1,.md), [T5](Pattern\Pattern(T1,.md), [T6](Pattern\Pattern(T1,.md), [T7](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2, T3, T4, T5, T6, T7\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)

# Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), [T3](Pattern\Pattern(T1,.md), [T4](Pattern\Pattern(T1,.md), [T5](Pattern\Pattern(T1,.md), [T6](Pattern\Pattern(T1,.md), [T7](Pattern\Pattern(T1,.md), [T8](Pattern\Pattern(T1,.md), [T9](Pattern\Pattern(T1,.md), [T10](Pattern\Pattern(T1,.md), [T11](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)

# Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402868)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), [T3](Pattern\Pattern(T1,.md), [T4](Pattern\Pattern(T1,.md), [T5](Pattern\Pattern(T1,.md), [T6](Pattern\Pattern(T1,.md), [T7](Pattern\Pattern(T1,.md), [T8](Pattern\Pattern(T1,.md), [T9](Pattern\Pattern(T1,.md), [T10](Pattern\Pattern(T1,.md), [T11](Pattern\Pattern(T1,.md), [T12](Pattern\Pattern(T1,.md), [T13](Pattern\Pattern(T1,.md), [T14](Pattern\Pattern(T1,.md), [T15](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)

# Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), [T3](Pattern\Pattern(T1,.md), [T4](Pattern\Pattern(T1,.md), [T5](Pattern\Pattern(T1,.md), [T6](Pattern\Pattern(T1,.md), [T7](Pattern\Pattern(T1,.md), [T8](Pattern\Pattern(T1,.md), [T9](Pattern\Pattern(T1,.md), [T10](Pattern\Pattern(T1,.md), [T11](Pattern\Pattern(T1,.md), [T12](Pattern\Pattern(T1,.md), [T13](Pattern\Pattern(T1,.md), [T14](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)

# Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), [T3](Pattern\Pattern(T1,.md), [T4](Pattern\Pattern(T1,.md), [T5](Pattern\Pattern(T1,.md), [T6](Pattern\Pattern(T1,.md), [T7](Pattern\Pattern(T1,.md), [T8](Pattern\Pattern(T1,.md), [T9](Pattern\Pattern(T1,.md), [T10](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)

# Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), [T3](Pattern\Pattern(T1,.md), [T4](Pattern\Pattern(T1,.md), [T5](Pattern\Pattern(T1,.md), [T6](Pattern\Pattern(T1,.md), [T7](Pattern\Pattern(T1,.md), [T8](Pattern\Pattern(T1,.md), [T9](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)

# Pattern\<T1, T2, T3, T4\>.Then\<TResult\> Method

Matches when all observable sequences have an available value and projects the values.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins\System.Reactive.Joins.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Then(Of TResult) ( _
    selector As Func(Of T1, T2, T3, T4, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim instance As Pattern
Dim selector As Func(Of T1, T2, T3, T4, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = instance.Then(selector)
```

```csharp
public Plan<TResult> Then<TResult>(
    Func<T1, T2, T3, T4, TResult> selector
)
```

```c++
public:
generic<typename TResult>
Plan<TResult>^ Then(
    Func<T1, T2, T3, T4, TResult>^ selector
)
```

```fsharp
member Then : 
        selector:Func<'T1, 'T2, 'T3, 'T4, 'TResult> -> Plan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of the return value of the selector function.

#### Parameters

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<[T1](Pattern\Pattern(T1,.md), [T2](Pattern\Pattern(T1,.md), [T3](Pattern\Pattern(T1,.md), [T4](Pattern\Pattern(T1,.md), TResult\>  
  The function that projects the result to the next observer.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The execution plan for join patterns.

## See Also

#### Reference

[Pattern\<T1, T2, T3, T4\> Class](Pattern\Pattern(T1,.md)

[System.Reactive.Joins Namespace](System.Reactive.Joins\System.Reactive.Joins.md)
