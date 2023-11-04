
## UI Thread and Schedulers
Always make sure to update the UI on the `RxApp.MainThreadScheduler` to ensure UI  changes happen on the UI thread. In practice, this typically means making sure to update view models on the main thread scheduler.

## Do
```csharp
FetchStuffAsync()
  .ObserveOn(RxApp.MainThreadScheduler)
  .Subscribe(x => this.SomeViewModelProperty = x);
```

## Better
Even better, pass the scheduler to the asynchronous operation - this is often
necessary for more complex tasks.

```csharp
FetchStuffAsync(RxApp.MainThreadScheduler)
  .Subscribe(x => this.SomeViewModelProperty = x);
```

## Don't
```csharp
FetchStuffAsync()
  .Subscribe(x => this.SomeViewModelProperty = x);
```

