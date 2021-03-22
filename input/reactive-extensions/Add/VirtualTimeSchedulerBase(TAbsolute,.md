title: VirtualTimeSchedulerBase<TAbsolute, TRelative>.Add()
---
# VirtualTimeSchedulerBase\<TAbsolute, TRelative\>.Add Method

Adds a relative time to an absolute time value.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Protected MustOverride Function Add ( _
    absolute As TAbsolute, _
    relative As TRelative _
) As TAbsolute
```

```vb
'Usage
Dim absolute As TAbsolute
Dim relative As TRelative
Dim returnValue As TAbsolute

returnValue = Me.Add(absolute, relative)
```

```csharp
protected abstract TAbsolute Add(
    TAbsolute absolute,
    TRelative relative
)
```

```c++
protected:
virtual TAbsolute Add(
    TAbsolute absolute, 
    TRelative relative
) abstract
```

```fsharp
abstract Add : 
        absolute:'TAbsolute * 
        relative:'TRelative -> 'TAbsolute 
```

```javascript
protected abstract function Add(
    absolute : TAbsolute, 
    relative : TRelative
) : TAbsolute
```

#### Parameters

- absolute  
  Type: [TAbsolute](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)  
  The absolute time value.

- relative  
  Type: [TRelative](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)  
  The relative time value to add.

#### Return Value

Type: [TAbsolute](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)  
The resulting absolute time sum value.

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)
