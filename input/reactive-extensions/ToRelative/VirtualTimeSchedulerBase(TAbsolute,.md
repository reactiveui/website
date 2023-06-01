title: VirtualTimeSchedulerBase<TAbsolute, TRelative>.ToRelative()
---
# VirtualTimeSchedulerBase\<TAbsolute, TRelative\>.ToRelative Method

Converts the TimeSpan value to a relative time value.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Protected MustOverride Function ToRelative ( _
    timeSpan As TimeSpan _
) As TRelative
```

```vb
'Usage
Dim timeSpan As TimeSpan
Dim returnValue As TRelative

returnValue = Me.ToRelative(timeSpan)
```

```csharp
protected abstract TRelative ToRelative(
    TimeSpan timeSpan
)
```

```c++
protected:
virtual TRelative ToRelative(
    TimeSpan timeSpan
) abstract
```

```fsharp
abstract ToRelative : 
        timeSpan:TimeSpan -> 'TRelative 
```

```javascript
protected abstract function ToRelative(
    timeSpan : TimeSpan
) : TRelative
```

#### Parameters

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The TimeSpan value to convert.

#### Return Value

Type: [TRelative](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)  
The corresponding relative time value.

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)
