NoTitle: true
---
The default behaviour of ReactiveUI is to crash the application with whenever an object that has a ThrownExceptions property doesn't have a subscription. 

You can override this behaviour or hook your debugger or analytics client by connecting an observable to [RxApp.DefaultExceptionHandler](https://reactiveui.net/api/reactiveui/rxapp/ce529741):

```csharp
public class MyCoolObservableExceptionHandler : IObserver<Exception>
{
    public void OnNext(Exception value)
    {
        if (Debugger.IsAttached) Debugger.Break();

        Analytics.Current.TrackEvent("MyRxHandler", new Dictionary<string, string>()
                                                {
                                                    {"Type", value.GetType().ToString()},
                                                    {"Message", value.Message},
                                                });

        RxApp.MainThreadScheduler.Schedule(() => { throw value; }) ;
    }

    public void OnError(Exception error)
    {
        if (Debugger.IsAttached) Debugger.Break();

        Analytics.Current.TrackEvent("MyRxHandler Error", new Dictionary<string, string>()
        {
            {"Type", error.GetType().ToString()},
            {"Message", error.Message},
        });

        RxApp.MainThreadScheduler.Schedule(() => { throw error; });
    }

    public void OnCompleted()
    {
        if (Debugger.IsAttached) Debugger.Break();
        RxApp.MainThreadScheduler.Schedule(() => { throw new NotImplementedException(); });
    }
}
```
