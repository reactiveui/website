title: VirtualTimeSchedulerBase<TAbsolute, TRelative>.ToDateTimeOffset()
---
# VirtualTimeSchedulerBase\<TAbsolute, TRelative\>.ToDateTimeOffset Method

Converts the absolute time value to a DateTimeOffset value.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Protected MustOverride Function ToDateTimeOffset ( _
    absolute As TAbsolute _
) As DateTimeOffset
```

```vb
'Usage
Dim absolute As TAbsolute
Dim returnValue As DateTimeOffset

returnValue = Me.ToDateTimeOffset(absolute)
```

```csharp
protected abstract DateTimeOffset ToDateTimeOffset(
    TAbsolute absolute
)
```

```c++
protected:
virtual DateTimeOffset ToDateTimeOffset(
    TAbsolute absolute
) abstract
```

```fsharp
abstract ToDateTimeOffset : 
        absolute:'TAbsolute -> DateTimeOffset 
```

```jscript
protected abstract function ToDateTimeOffset(
    absolute : TAbsolute
) : DateTimeOffset
```

#### Parameters

- absolute  
  Type: [TAbsolute](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md)  
  The absolute time value to convert.

#### Return Value

Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
The corresponding DateTimeOffset value.

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)
