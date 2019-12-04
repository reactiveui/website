# Asynchronous Commands

Prefer using async `ReactiveCommand` over the more basic `ReactiveCommand` for all but the most simple tasks. In ReactiveUI, you should never put Interesting™ code inside the Subscribe block - Subscribe is solely to log the result of operations, or to wire up properties to other properties.

## Do

```csharp
// In XAML
<Button Command="{Binding Delete}" .../>

public class RepositoryViewModel : ReactiveObject
{
  public RepositoryViewModel() 
  {
    Delete = ReactiveCommand.CreateFromObservable(DeleteImpl);
    Delete.IsExecuting.ToProperty(this, x => x.IsDeleting, out _isDeleting);
    Delete.ThrownExceptions.Subscribe(ex => this.Log().ErrorException("Something went wrong", ex));
  }

  public ReactiveCommand<Unit, Unit> Delete { get; private set; }

  readonly ObservableAsPropertyHelper<bool> _isDeleting;
  public bool IsDeleting { get { return _isDeleting.Value; } }

  public IObservable<Unit> DeleteImpl()
  {
    return Observable.Start(() => /* ... */);
  }
}
```

## Don't

```csharp
// In XAML
<Button Command="{Binding Delete}" .../>

public class RepositoryViewModel : ReactiveObject
{
  public RepositoryViewModel() 
  {
    Delete = ReactiveCommand.Create();
    // This will block the UI thread while DeleteImpl runs
    Delete.Subscribe(async _ => await DeleteImpl());
    // These will not do what you expect
    Delete.IsExecuting.ToProperty(this, x => x.IsDeleting, out _isDeleting);
    Delete.ThrownExceptions.Subscribe(ex => this.Log().ErrorException("Something went wrong", ex));
  }

  public ReactiveCommand<Unit, Unit> Delete { get; private set; }

  readonly ObservableAsPropertyHelper<bool> _isDeleting;
  public bool IsDeleting { get { return _isDeleting.Value; } }

  public IObservable<Unit> DeleteImpl()
  {
    return Observable.Start(() => /* ... */);
  }
}
```

## Why?

A lot of the power of `ReactiveCommand` comes from the async version. In the basic version the following features do not function as expected:

* `IsExecuting` observable will not report on your asynchronous method when it is inside the `Subscribe`
* `ThrownExceptions` will not catch anything.
* `CanExecute` is not affected if the command is currently executing, leading to the possibilty of multiple execution at the same time.



