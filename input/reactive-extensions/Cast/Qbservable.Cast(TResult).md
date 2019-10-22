title: Qbservable.Cast<TResult>()
---
# Qbservable.Cast\<TResult\> Method

Converts the elements of an observable sequence to the specified type.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Cast(Of TResult) ( _
    source As IQbservable(Of Object) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of Object)
Dim returnValue As IQbservable(Of TResult)

returnValue = source.Cast()
```

```csharp
public static IQbservable<TResult> Cast<TResult>(
    this IQbservable<Object> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static IQbservable<TResult>^ Cast(
    IQbservable<Object^>^ source
)
```

```fsharp
static member Cast : 
        source:IQbservable<Object> -> IQbservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)\>  
  The source sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>  
An observable sequence that contains each element of the source sequence converted to the specified type.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








