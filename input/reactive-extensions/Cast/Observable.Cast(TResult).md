title: Observable.Cast<TResult>()
---
# Observable.Cast\<TResult\> Method

Converts the elements of an observable sequence to the specified type.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Cast(Of TResult) ( _
    source As IObservable(Of Object) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim source As IObservable(Of Object)
Dim returnValue As IObservable(Of TResult)

returnValue = source.Cast()
```

```csharp
public static IObservable<TResult> Cast<TResult>(
    this IObservable<Object> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static IObservable<TResult>^ Cast(
    IObservable<Object^>^ source
)
```

```fsharp
static member Cast : 
        source:IObservable<Object> -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)\>  
  The source sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence that contains each element of the source sequence converted to the specified type.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








