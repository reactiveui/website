title: Observer.Synchronize<T>(IObserver<T>, Object)
---
# Observer.Synchronize\<T\> Method (IObserver\<T\>, Object)

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Synchronize(Of T) ( _
    observer As IObserver(Of T), _
    gate As Object _
) As IObserver(Of T)
```

```vb
'Usage
Dim observer As IObserver(Of T)
Dim gate As Object
Dim returnValue As IObserver(Of T)

returnValue = Observer.Synchronize(observer, _
    gate)
```

```csharp
public static IObserver<T> Synchronize<T>(
    IObserver<T> observer,
    Object gate
)
```

```c++
public:
generic<typename T>
static IObserver<T>^ Synchronize(
    IObserver<T>^ observer, 
    Object^ gate
)
```

```fsharp
static member Synchronize : 
        observer:IObserver<'T> * 
        gate:Object -> IObserver<'T> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

#### Parameters

- observer  
  Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<T\>

- gate  
  Type: [System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)

#### Return Value

Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<T\>

## See Also

#### Reference

[Observer Class](Observer\Observer.md)

[Synchronize Overload](Synchronize\Observer.Synchronize.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)

# Observer.Synchronize\<T\> Method (IObserver\<T\>)

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Synchronize(Of T) ( _
    observer As IObserver(Of T) _
) As IObserver(Of T)
```

```vb
'Usage
Dim observer As IObserver(Of T)
Dim returnValue As IObserver(Of T)

returnValue = Observer.Synchronize(observer)
```

```csharp
public static IObserver<T> Synchronize<T>(
    IObserver<T> observer
)
```

```c++
public:
generic<typename T>
static IObserver<T>^ Synchronize(
    IObserver<T>^ observer
)
```

```fsharp
static member Synchronize : 
        observer:IObserver<'T> -> IObserver<'T> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

#### Parameters

- observer  
  Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<T\>

#### Return Value

Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<T\>

## See Also

#### Reference

[Observer Class](Observer\Observer.md)

[Synchronize Overload](Synchronize\Observer.Synchronize.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)
