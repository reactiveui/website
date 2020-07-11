title: Qbservable.Repeat<TSource>(IQbservable<TSource>)
---
# Qbservable.Repeat\<TSource\> Method (IQbservable\<TSource\>)

Repeats the queryable observable sequence indefinitely.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Repeat(Of TSource) ( _
    source As IQbservable(Of TSource) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim returnValue As IQbservable(Of TSource)

returnValue = source.Repeat()
```

```csharp
public static IQbservable<TSource> Repeat<TSource>(
    this IQbservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Repeat(
    IQbservable<TSource>^ source
)
```

```fsharp
static member Repeat : 
        source:IQbservable<'TSource> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The queryable observable sequence to repeat.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
The queryable observable sequence producing the elements of the given sequence repeatedly and sequentially.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Repeat Overload](Repeat/Qbservable.Repeat)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Repeat\<TSource\> Method (IQbservable\<TSource\>, Int32)

Repeats the queryable observable sequence indefinitely.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Repeat(Of TSource) ( _
    source As IQbservable(Of TSource), _
    repeatCount As Integer _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim repeatCount As Integer
Dim returnValue As IQbservable(Of TSource)

returnValue = source.Repeat(repeatCount)
```

```csharp
public static IQbservable<TSource> Repeat<TSource>(
    this IQbservable<TSource> source,
    int repeatCount
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Repeat(
    IQbservable<TSource>^ source, 
    int repeatCount
)
```

```fsharp
static member Repeat : 
        source:IQbservable<'TSource> * 
        repeatCount:int -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The queryable observable sequence to repeat.

- repeatCount  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The number of times to repeat the sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
The queryable observable sequence producing the elements of the given sequence repeatedly.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Repeat Overload](Repeat/Qbservable.Repeat)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
