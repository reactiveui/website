NoTitle: true
---
# Use descriptive variables in your `WhenAny`
In situations where you are detecting changes in multiple expressions, ensure you name the variables in the `selector` 

## Do
```csharp
public class MyViewModel : ReactiveObject
{
  public MyViewModel()
  {
    this.WhenAny(
        x => x.SomeProperty.IsEnabled,
        x => x.SomeProperty.IsLoading,
        (isEnabled, isLoading) => isEnabled.Value && isLoading.Value)
        .Subscribe(_ => DoSomething());
  }
  
  // other code here
}
```

## Don't
```csharp
public class MyViewModel : ReactiveObject
{
  public MyViewModel()
  {
    this.WhenAny(
        x => x.SomeProperty.IsEnabled,
        x => x.SomeProperty.IsLoading,
        (x, y) => x.Value && y.Value)
        .Subscribe(_ => DoSomething());
  }
  
  // other code here
}
```

## Why?
This helps greatly with the readability of complex expressions, particularly when working with boolean values.
