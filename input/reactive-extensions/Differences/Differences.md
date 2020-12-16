title: Differences Between Versions of Rx
---
# Differences Between Versions of Rx

The following topic describes the various platforms for which you can develop solutions using Reactive Extensions.

To get the latest release of Rx, as well as learn about its prerequisites, please visit the [Rx MSDN Developer Center](https://msdn.microsoft.com/en-us/data/gg577610).

## .NET Framework

The core Rx interfaces, IObservable\<T\> and IObserver\<T\>, ship as part of .NET Framework 4. If you are running on .NET Framework 3.5 SP1, or if you want to take advantage of the LINQ operators implemented in [Observable](Observable/Observable) type, as well as many other features such as schedulers, you can download the Rx assemblies in the [Rx MSDN Developer Center](https://msdn.microsoft.com/en-us/data/gg577610).

## Silverlight

Silverlight disallows you from making cross-threading calls, thus you cannot use a background thread to update the UI. Instead of writing verbose code using the Dispatcher.BeginInvoke call to explicitly execute code on the main UI thread, you can use the factory Observable.Start method provided by the Rx assemblies to invoke an action asynchronously. Cross-threading is taken care of transparently by Rx under the hood.

You can also use the various Observable operator overloads that take in a Scheduler, and specify the [DispatcherScheduler](DispatcherScheduler/DispatcherScheduler) to be used.

## Javascript

Rx for Javascript (RxJS) allows you to use LINQ operators in JavaScript. It provides easy-to-use conversions from existing DOM, XmlHttpRequest (AJAX), and jQuery events to push-based observable collections, allowing users to seamlessly integrate Rx into their existing JavaScript-based websites.

RxJS brings similar capabilities to client script and integrates with jQuery events (Rx.Observable.FromJQueryEvent). It also supports Script\#.

## Windows Phone

Windows Phone 7 ships with a version of the Reactive Extensions baked into the ROM of the device. For more information, see [Reactive Extensions for .NET Overview for Windows Phone](https://msdn.microsoft.com/en-us/library/ff431792(vs.92).aspx). Documentation for this version of the Reactive Extensions can be found in Windows Phone API library at [Microsoft.Phone.Reactive Namespace](https://msdn.microsoft.com/en-us/library/ff707857(v=vs.92).aspx).

The [Rx MSDN Developer Center](https://msdn.microsoft.com/en-us/data/gg577610) also contains an updated version of Rx for WP7, which has new definitions in the System.Reactive.Linq namespace. Note that the new APIs will not clash with the library built in to the phone (nor do they replace the version in the ROM). For more information on the differences of these 2 versions, see [this Rx team blog post](https://blogs.msdn.com/b/rxteam/archive/2010/10/28/rx-for-windows-phone-7.aspx).





