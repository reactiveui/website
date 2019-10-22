title: ITestableObservable<T>.Subscriptions Property
---
# ITestableObservable\<T\>.Subscriptions Property

Gets the subscriptions to the observable.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
ReadOnly Property Subscriptions As IList(Of Subscription)
    Get
```

```vb
'Usage
Dim instance As ITestableObservable
Dim value As IList(Of Subscription)

value = instance.Subscriptions
```

```csharp
IList<Subscription> Subscriptions { get; }
```

```c++
property IList<Subscription>^ Subscriptions {
    IList<Subscription>^ get ();
}
```

```fsharp
abstract Subscriptions : IList<Subscription>
```

```jscript
function get Subscriptions () : IList<Subscription>
```

#### Property Value

Type: [System.Collections.Generic.IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<[Subscription](Subscription\Subscription.md)\>

## See Also

#### Reference

[ITestableObservable\<T\> Interface](ITestableObservable\ITestableObservable(T).md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)
