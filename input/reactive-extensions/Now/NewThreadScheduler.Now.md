title: NewThreadScheduler.Now Property
---
# NewThreadScheduler.Now Property

Gets the scheduler's notion of current time.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public ReadOnly Property Now As DateTimeOffset
    Get
```

```vb
'Usage
Dim instance As NewThreadScheduler
Dim value As DateTimeOffset

value = instance.Now
```

```csharp
public DateTimeOffset Now { get; }
```

```c++
public:
virtual property DateTimeOffset Now {
    DateTimeOffset get () sealed;
}
```

```fsharp
abstract Now : DateTimeOffset
override Now : DateTimeOffset
```

```javascript
final function get Now () : DateTimeOffset
```

#### Property Value

Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
The current time.

#### Implements

[IScheduler.Now](Now/IScheduler.Now)

## See Also

#### Reference

[NewThreadScheduler Class](NewThreadScheduler/NewThreadScheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)






