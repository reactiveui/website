
## Enable Framework Logging

Debug information is written by the framework to Splat. By default, [splat ships with a null logger as "Debug.WriteLine" is stripped by the compiler when Splat is packaged](https://github.com/reactiveui/splat/issues/46). Wire in an implementation of `ILogger` such as the one below to see these messages:

```csharp

    public class LoggingService : ILogger
    {
        public LogLevel Level { get; set; }

        public void Write([Splat.Localizable(false)] string message, LogLevel logLevel)
        {
            if (logLevel >= Level)
                System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
```

Then at your composition root, register your implementation

```csharp
public void ConfigureLogging()
{
#if DEBUG
    Locator.CurrentMutable.RegisterConstant(new LoggingService { Level = LogLevel.Debug }, typeof(ILogger));
#endif
}
```
