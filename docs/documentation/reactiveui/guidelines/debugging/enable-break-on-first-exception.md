# Break on exceptions in Visual Studio for Mac

Reactive debugging tip: break as soon as an exception is thrown, not once it becomes unhandled.

In the **Breakpoints** pad, click **New Exception Catchpoint** and enter `System.Exception` as the exception to
break on.

An exception inside a subscription's callback ends that subscription: the stream calls `OnError` instead of
`OnNext` or `OnCompleted`, and stops delivering values. A catchpoint on `System.Exception` stops the debugger at
the line that threw, before the stream unwinds it into `OnError`, so you can inspect the state that caused it.
