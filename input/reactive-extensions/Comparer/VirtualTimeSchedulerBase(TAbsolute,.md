# VirtualTimeSchedulerBase\<TAbsolute, TRelative\>.Comparer Property

Gets the comparer used to compare absolute time values.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Protected Property Comparer As IComparer(Of TAbsolute)
    Get
    Private Set
```

```vb
'Usage
Dim value As IComparer(Of TAbsolute)

value = Me.Comparer
```

```csharp
protected IComparer<TAbsolute> Comparer { get; private set; }
```

```c++
protected:
property IComparer<TAbsolute>^ Comparer {
    IComparer<TAbsolute>^ get ();
    private: void set (IComparer<TAbsolute>^ value);
}
```

```fsharp
member Comparer : IComparer<'TAbsolute> with get, private set
```

```jscript
function get Comparer () : IComparer<TAbsolute>
private function set Comparer (value : IComparer<TAbsolute>)
```

#### Property Value

Type: [System.Collections.Generic.IComparer](https://msdn.microsoft.com/en-us/library/8ehhxeaf)\<[TAbsolute](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md)\>  
The comparer object that can be used to compare absolute time values.

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)





