> One thing that motivates me to write my own instead of using the legion of others, is that most loggers give zero thought to perf concerns on mobile devices - they're all written for servers, so none of them think about CPU perf or allocations. The best imho is Serilog, but it allocates way too much stuff imho to be usable on mobile — Anaïs Betts (2014) [issue 46#issuecomment-56550457](https://github.com/reactiveui/splat/issues/46#issuecomment-56550457)

# Logging

ReactiveUI has an underlying framework called Splat which provides a logging interface which can be used to debug
your applications as well as ReactiveUI itself. You may ask yourself,
"Seriously, another logging interface \ framework?". The reason RxUI does this itself is
for portability - none of the common popular logging frameworks support all of
the platforms that ReactiveUI supports, and many are server-oriented
frameworks ill-suited for simple mobile app logging.

Splat has taken the approach of allowing the developer to decide the target logging framework, whilst the interface aims to keep ReactiveUI and any other users of Splat as agnostic as possible. Logging frameworks have evolved since 2014 but there is still the challenge of performance and flexibility. There has also been a move within Splat to offer better integration into other frameworks so you don't have to make accomodations for Splat or vice versa.

## Getting started

By default Splat uses a NullLogger (i.e. it doesn't log). To set up logging:

1. Register an implementation of `ILogger` using Service Location.
1. In the class in which you want to log stuff, "implement" the `IEnableLogger`
   interface (this is a tag interface, no implementation actually needed).
1. Call the `Log` method to write log entries:

### Available logging adapters

Splat has support for the following logging frameworks:

| Target | Package | NuGet |
|---------|-------|------|
| Console | [Splat][SplatNuGet] | [![SplatBadge]][SplatNuGet] |
| Debug | [Splat][SplatNuGet] | [![SplatBadge]][SplatNuGet] |
| Log4Net | [Splat.Log4Net][SplatLog4NetNuGet] | [![SplatLog4NetBadge]][SplatLog4NetNuGet]  |
| Microsoft Extensions Logging | [Splat.Microsoft.Extensions.Logging][SplatMicrosoftExtensionsLoggingNuGet] | [![SplatMicrosoftExtensionsLoggingBadge]][SplatMicrosoftExtensionsLoggingNuGet] |
| NLog | [Splat.NLog][SplatNLogNuGet] | [![SplatNLogBadge]][SplatNLogNuGet] |
| Serilog | [Splat.Serilog][SplatSerilogNuGet] | [![SplatSerilogBadge]][SplatSerilogNuGet] |

[SplatNuGet]: https://www.nuget.org/packages/Splat/
[SplatBadge]: https://img.shields.io/nuget/v/Splat.svg
[SplatLog4NetNuGet]: https://www.nuget.org/packages/Splat.Log4Net/
[SplatLog4NetBadge]: https://img.shields.io/nuget/v/Splat.Log4Net.svg
[SplatMicrosoftExtensionsLoggingNuGet]: https://www.nuget.org/packages/Splat.Microsoft.Extensions.Logging/
[SplatMicrosoftExtensionsLoggingBadge]: https://img.shields.io/nuget/v/Splat.Microsoft.Extensions.Logging.svg
[SplatNLogNuGet]: https://www.nuget.org/packages/Splat.NLog/
[SplatNLogBadge]: https://img.shields.io/nuget/v/Splat.NLog.svg
[SplatSerilogNuGet]: https://www.nuget.org/packages/Splat.Serilog/
[SplatSerilogBadge]: https://img.shields.io/nuget/v/Splat.Serilog.svg

#### Console

This is a simple built in logger, Console logging is not available on every platform and typically carries a performance overhead. It is recommended NOT to use this logger unless you have a specific need for Console output and don't want to use a larger logging framework.

```cs
// I only want to hear about errors
var logger = new ConsoleLogger() { Level = LogLevel.Error };
Locator.CurrentMutable.RegisterConstant(logger, typeof(ILogger));
```

#### Debug

This is a simple built in logger, useful during early development, but you may want to consider using a fully fledged logging package below and using the debug target of that.

```cs
// I only want to hear about errors
var logger = new DebugLogger() { Level = LogLevel.Error };
Locator.CurrentMutable.RegisterConstant(logger, typeof(ILogger));
```

#### Log4Net

First configure Log4Net. For guidance see [configuration](https://logging.apache.org/log4net/release/manual/configuration.html)

```cs
using Splat.Log4Net;

// then in your service locator initialisation
Locator.CurrentMutable.UseLog4NetWithWrappingFullLogger();
```

#### Microsoft.Extensions.Logging

First configure Microsoft.Extensions.Logging. For guidance see [logging](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/)

```cs
using Splat.Microsoft.Extensions.Logging;

// note: this is different from the other adapter extension methods
//       as it needs knowledge of the logger factory
//       also the "container" is how you configured the Microsoft.Logging.Extensions
var loggerFactory = container.Resolve<ILoggerFactory>();
// in theory it could also be
// var loggerFactory = new LoggerFactory();


/// then in your service locator initialisation
Locator.CurrentMutable.UseMicrosoftExtensionsLoggingWithWrappingFullLogger(loggerFactory);
```

#### NLog

First configure NLog. For guidance see [tutorial](https://github.com/nlog/nlog/wiki/Tutorial) and [configuration file](https://github.com/nlog/nlog/wiki/Configuration-file)

```cs
using Splat.NLog;

//  then in your service locator initialisation
Locator.CurrentMutable.UseNLogWithWrappingFullLogger();
```

#### Serilog

First configure Serilog. For guidance see [configuration basics](https://github.com/serilog/serilog/wiki/Configuration-Basics)

```cs
using Splat.Serilog;

// Then in your service locator initialisation
Locator.CurrentMutable.UseSerilogFullLogger()
```

#### Using a different logging framework

Splat has tried to cater for offering sufficient scope for logging frameworks. If you need another framework or platform consider the whether your desired target platform is available as an output to one of the other logging frameworks already covered? Using an existing output for an existing framework will make your life easier. For example OpenTelemetry is supported across all the supported frameworks.

If we are missing a target (as times change) then feel free to engage the team at the [Splat Issues Page on Github][SplatIssuesPageOnGithub].

If you want to write your own logging integration the [source code of an existing projects such as the NLog adapter is a good reference][NLogSplatSourceCodeOnGithub]. There are a few interfaces you can use depending on the level of control you need for your implementation.

[SplatIssuesPageOnGithub]: https://github.com/reactiveui/splat/issues
[NLogSplatSourceCodeOnGithub]: https://github.com/reactiveui/splat/tree/main/src/Splat.NLog

There are 2 logging interfaces to consider: ILogger and IFullLogger. The full logger is the full detailed interface for logging, ILogger is the minimal interface where you can use the default implementation for sending in the logging requests, and need a minimal implementation to get the detail out into the target platform. In most scenarios you should be able to use WrappingFullLogger and implement a smaller subset of ILogger which you can pass in. Your decision will be based on the flexibility and performance of the logging framework you're trying to use. For example if your logging framework supports a simple Write method that takes a log level you may want to use ILogger with the WrappingFullLogger. However if the target framework has specific methods per log level, or has both offerings but is more performant with the direct calls you may wish to implement IFullLogger.

## Logging via this.Log() and IEnableLogger

ReactiveUI's logger works a bit differently than other frameworks - its
design is inspired by Rails 'logger'. To use it, make your class implement the
`IEnableLogger` interface:

```cs
public class MyClass : IEnableLogger
{
    // IEnableLogger doesn't actually require anything of us
}
```

Now, you can call the `Log` method on your class. Because of how extension
methods work, you must prepend `this` to it:

```cs
this.Log().Info("Downloaded {0} tweets", tweets.Count);
```

There are **five** levels of logging, `Debug`, `Info`, `Warn`, `Error`, and
`Fatal`. Additionally, there are special methods to log exceptions - for
example, `this.Log().InfoException(ex, "Failed to post the message")`. This 
trick doesn't work for static methods though, you have to settle for an
alternate method, `LogHost.Default.Info(...)`.

`this` is used to set the class name for a logger. `this.Log()` call doesn't always 
create a new instance of a logger, instead, it uses a cached version if possible.

## Debugging Observables

ReactiveUI has several helpers for debugging IObservables. The most
straightforward one is `Log`, which logs events that happen to an Observable:

```cs
// Note: Since Log acts like another Rx operator like Select or Where,
// it won't do anything by itself unless someone Subscribes to it.
this.WhenAnyValue(x => x.Name)
    .SelectMany(async x => GoogleForTheName(x))
    .Log(this, "Result of Search")
    .Subscribe();
```

Another useful method to debug Observables is `LoggedCatch`. This method works
identically to Rx's `Catch` operator, except that it also logs the exception
to the Logger. For example:

```cs
var userAvatar = await FetchUserAvatar()
    .LoggedCatch(this, Observable.Return(default(Avatar)));
```

## Static Logging

For static methods, `LogHost.Default` can be used as the object to write a log
entry for. The Static logger uses a different interface from the main logger to allow capture of additional
caller context as it doesn't have the details of the class instance etc. when compared to the normal logger.
To get the benefit of these you don't need to do much as they are optional parameters at the end of the methods
that are utilised by the compiler\framework. Currently we only capture [CallerMemberName](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.callermembernameattribute).
