> One thing that motivates me to write my own instead of using the legion of others, is that most loggers give zero thought to perf concerns on mobile devices - they're all written for servers, so none of them think about CPU perf or allocations. The best imho is Serilog, but it allocates way too much stuff imho to be usable on mobile — Paul Betts https://github.com/paulcbetts/splat/issues/46#issuecomment-56550457

# Logging

ReactiveUI comes with its own logging framework which can be used to debug
your applications as well as ReactiveUI itself. You may ask yourself,
"Seriously, another logging framework?". The reason RxUI does this itself is
for portability - none of the common popular logging frameworks support all of
the platforms that ReactiveUI supports, and many are server-oriented
frameworks and ill-suited for simple mobile app logging.

### this.Log() and IEnableLogger

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

### Debugging Observables

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

### Configuring the logger

To configure the logger, register an implementation of `ILogger` class (there are several built-in ones, such as `DebugLogger`). Here's an example where we use a built-in logger, but a custom log level:

```cs
// I only want to hear about errors
var logger = new DebugLogger() { Level = LogLevel.Error };
Locator.CurrentMutable.RegisterConstant(logger, typeof(ILogger));
```

If you really need to control how things are logged, you can implement
`IFullLogger`, which will allow you to control every logging overload.
