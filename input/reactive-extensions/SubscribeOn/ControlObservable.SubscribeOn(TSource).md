# ControlObservable.SubscribeOn\<TSource\> Method

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Windows.Forms (in System.Reactive.Windows.Forms.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SubscribeOn(Of TSource) ( _
    source As IObservable(Of TSource), _
    control As Control _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim control As Control
Dim returnValue As IObservable(Of TSource)

returnValue = source.SubscribeOn(control)
```

```csharp
public static IObservable<TSource> SubscribeOn<TSource>(
    this IObservable<TSource> source,
    Control control
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ SubscribeOn(
    IObservable<TSource>^ source, 
    Control^ control
)
```

```fsharp
static member SubscribeOn : 
        source:IObservable<'TSource> * 
        control:Control -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>

- control  
  Type: [System.Windows.Forms.Control](https://msdn.microsoft.com/en-us/library/36cd312w)

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[ControlObservable Class](ControlObservable\ControlObservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
