IsBlog: true
IsPost: true
Title: ReactiveUI v7.0.0 released
Tags: Release Notes
Lead: Geoffrey Huntley
---

_Oh, hai!_

![](https://media.giphy.com/media/nrYqOTU8hoAeY/giphy.gif)

Wow! It's been a while, but we have a juicy new release for you. We had to take some time to heal internally, to train and mentor the future generations of maintainers which are going to carry us forward.  Additionally the .NET ecosystem changed underneath us, and we didn't have continuous integration setup. Let this be a lesson to anyone considering operating an open-source project that targets nine platforms. Have a roadmap and succession plan in place and never allow long-running branches, ever. Implement continuous integration and mature your release processes to the point where cutting a new release is as simple as pressing a button.

**So what is ReactiveUI?**

ReactiveUI is inspired by functional reactive programming and is the father of the ReactiveCocoa (Cocoa/Swift) framework. Internally we debate whether we are or are not a framework, as at its core the project is essentially a bunch of extension methods for the Reactive Extensions.

ReactiveUI was started seven years ago and is now old enough to attend grade school but unlike a teenager is extremely stable. ReactiveUI has matured over the years into a solid and fine choice for building your next application. Additionally, because the implementation is unopinionated migration from another framework to ReactiveUI is incredibly easy.. You can slide it in on a single ViewModel and then migrate as you become more comfortable. Avoid those costly rewrites.

When reading the code, you'll find that ReactiveUI is rather unopinionated in the implementation, but we have always held some beliefs which have been the basis and foundation of the project.

We believe that code is communication between people, that also happens to run on a computer. If you optimise for humans, then over a long time your project will end up better. Software should be understandable by other people; that is super important. 

We believe that only the power of the Reactive Extensions allows you to express the idea around a feature in one readable place. 

![Even Miguel de Icaza agrees](https://i.imgur.com/mCYZ2M4.png)

Think about your typical user interface? It's a mutable bag of sadness with code all over the place. Instead of telling a computer how to do its job, why not define what the computers job is and get out of its way? If that sounds odd, let us re-introduce you to Microsoft Excel. 

Instead of doing the typical ViewModel isLoading = true/false mutable dance by toggling it on and off in different branches of your code. Why not express the condition in a single place using a Microsoft Excel expression - =SUM(A1: B2)?

**Still not convinced?**
- [Why You Should Be Building Better Mobile Apps with Reactive Programming](https://www.youtube.com/watch?v=DYEbUF4xs1Q) – Michael Stonis (Xamarin Univeristy Trainer/Eightbot)
- [Awaiting for Rx: A Play in Four Acts](https://www.youtube.com/watch?v=5DZ8nC0ENdg) - Anaïs Betts (Slack/GitHub) 
- [FRP In Practice: Taking a look at ReactiveUI/ReactiveCocoa](https://www.youtube.com/watch?v=1XNATGjqM6U) by Anaïs Betts (Slack/GitHub)

Maybe you need to watch this insanely smart, and eccentric guy in a tie-dye t-shirt do maths on a whiteboard:

[![](https://i.imgur.com/P1Bt3iw.png)](https://channel9.msdn.com/Shows/Going+Deep/E2E-Erik-Meijer-and-Wes-Dyer-Reactive-Framework-Rx-Under-the-Hood-1-of-2)

Async/await is the zombie plague. Liberate your codebase today.

ReactiveUI is used in production at GitHub, Slack, Microsoft and is supported by consultants from different companies from all around the world. It's used at our clients, we can't name names specifically, but we encourage our community to [showcase where and how they have used ReactiveUI in their applications](https://github.com/reactiveui/ReactiveUI/issues/979), some members have even gone [as far as open-sourcing their app and sharing their entire codebase](https://github.com/github/VisualStudio/tree/master/src). You are of course under no obligation to share these insights (or code) with us but it is greatly appreciated by the project maintainers, and you'll usually get a retweet out of it.

**Where are the examples?**

We are working on it; this release was for us. Next release is for you.  For now:
- Read https://docs.reactiveui.net/en/fundamentals/history.html
- [Watch the guest lecture over at Xamarin Univeristy](https://university.xamarin.com/guestlectures/introduction-to-reactiveui-for-xamarin)
- [Watch this god damn amazing video series by Kent Boogaart (one of the core maintainers)](https://github.com/kentcb/WorkoutWotch)
- [Play with the Xamarin University lecture source code. (Xamarin Forms)](https://github.com/TheEightBot/Reactive-Examples)
- [Play with the GitHub for Windows Visual Studio source code. (WPF)](https://github.com/github/visualstudio)

**Rx is hard**

No, it's not. Learning Rx is one of the best things you can do to improve yourself as a software engineer. Unit testing was hard, so was dependency injection at first. The principals you learn during your journey will forever change you and best of all the knowledge is implementation and language agnostic. We have designed ReactiveUI so you can slowly transition from an async/await codebase at a pace that feels comfortable to you.

<!--excerpt-->

# Highlights

## ReactiveCommand is Better

`ReactiveCommand` is completely rewritten again (sorry).
- interfaces are gone. Any use of `IReactiveCommand` should be replaced with `ReactiveCommand`, possibly with type information (see below).
- static creation methods have changed:
  - execution logic is _always_ required when calling `CreateXxx` methods, including synchronous commands (i.e. those created with `Create`). So rather than calling `Create` and then subscribing, you call `Create` and pass in your execution logic right then and there.
  - for consistency, the execution logic is always provided as the first parameter. Other parameters (`canExecute`, `scheduler`) are optional.
  - `CreateAsyncObservable` is now called `CreateFromObservable`.
  - `CreateAsyncTask` is now called `CreateFromTask`.
- parameter types are formalized by `TParam` in `ReactiveCommand<TParam, TResult>`.
  - if your command takes a parameter, you no longer take an `object` and cast it. Instead, you explicitly specify the parameter type when creating the command (of course, you can still choose `object` as your `TParam` if that makes sense, perhaps as an intermediary migration step).
- `ICommand` is now implemented explicitly. As a result:
  - the `Execute` exposed by `ReactiveCommand` is reactive (it returns `IObservable<TResult>`). It is therefore lazy and won't do anything unless something subscribes to it.
  - `CanExecuteObservable` is now called `CanExecute`.
- observables such as `CanExecute` and `IsExecuting` are now behavioral. That is, they will always provide the current value to new subscribers.
- `RoutingState` has been updated to use the new implementation. Consequently, any use of its commands will be affected per the above.
- the `ToCommand` extension method has been removed. This was a simple convenience to take an `IObservable<bool>` and use it as the `canExecute` pipeline for a new command. If you're using `ToCommand`, you can just replace it with a call to one of the creation methods on `ReactiveCommand`.

**Old:**

``` cs
var canExecute = ...;
var someCommand = ReactiveCommand.Create(canExecute);
someCommand.Subscribe(x => /* execution logic */);

var someAsyncCommand1 = ReactiveCommand.CreateAsyncObservable(canExecute, someObservableMethod);
var someAsyncCommand2 = ReactiveCommand.CreateAsyncTask(canExecute, someTaskMethod);

someCommand.Execute();
```

**New:**

``` cs
var canExecute = ...;
var someCommand = ReactiveCommand.Create(() => /* execution logic */);

var someAsyncCommand1 = ReactiveCommand.CreateFromObservable(someObservableMethod, canExecute);
var someAsyncCommand2 = ReactiveCommand.CreateFromTask(someTaskMethod, canExecute);

someCommand.Execute().Subscribe();
```

For more details, please see the [extensive documentation](https://docs.reactiveui.net/en/user-guide/commands/index.html) on this topic.

> **Note** To enable you to ease into the migration, all previous types are available under the `ReactiveUI.Legacy` namespace. Note, however, that there is no legacy version of `RoutingState`, so any code you have that interacts with its command may require minor updates.

## Interactions are New and Exciting

`UserError` has been generalized and re-imagined. We call it interactions, and we think you'll like it. We did this in part because people were feeling icky using `UserError` for non-error scenarios. Basically, we realized that people need a general mechanism via which a view model can ask a question, and wait for the answer. It doesn't have to be an error - we're not that pessimistic! You could be asking to confirm a file deletion, or maybe how the weather is out there in the analog world.

Migrating from `UserError` to the interactions infrastructure is not really a case of one-for-one substitution. But here are some tips to get you started:
- read through [the documentation](https://docs.reactiveui.net/en/user-guide/interactions/index.html) first.
- decide whether you need shared interactions and, if so, define them in an appropriate place for your application (often just a static class).
- for any non-shared interactions, have your view model create an instance of the interaction and expose it via a property.
- typically you want the corresponding view to handle interactions by calling one of the `RegisterHandler` methods on the interaction exposed by the view model.
- the view model can call `Handle` on the interaction, passing in an input value.
- Recovery commands are no longer a built-in thing. If you need such a mechanism for your interactions, you are encouraged to write an appropriate class and use it as the input for your interaction.

> **Note** To enable you to ease into the migration, all previous types are available under the `ReactiveUI.Legacy` namespace.

## ToProperty is now eager by default

In previous ReactiveUI versions, `ToProperty` was lazy. That is, it would have no effect unless something was "pulling" on the target property. This was for performance reasons, as you may have properties that are expensive to resolve, but only used in specific scenarios.

Whilst this was good for performance, it was often confusing and contrary to expectations. Therefore, `ToProperty` is no longer lazy - it immediately subscribes to ensure the property's value reflects the given observable pipeline. However, the original lazy behavior can be obtained by passing in `true` to the `deferSubscription` parameter.

## Automation galore

A mountain of effort has gone into automating ReactiveUI's build and deployment infrastructure (thanks @ghuntley!). Using tools such as Cake, AppVeyor, and gitversion, we now have a very compelling automation story. This is no mean feat, especially considering the vast array of platforms on which ReactiveUI runs, and must therefore be built.

The end goal here is to get you, the community, new versions of ReactiveUI more rapidly and seamlessly.

# Details

As part of this release we had [113 issues](https://github.com/reactiveui/ReactiveUI/issues?milestone=2&state=closed) closed.

**All Platforms**
- [**#1189**](https://github.com/reactiveui/ReactiveUI/pull/1189) Add `WhenAnyObservable` overloads that combine latest via a given selector
- [**#1161**](https://github.com/reactiveui/ReactiveUI/pull/1161) Remove `BindCommand` implementation taking `Func<TParam>`
- [**#1158**](https://github.com/reactiveui/ReactiveUI/pull/1158) Throw `UnhandledErrorException` when a thrown exception goes unhandled (rather than just `Exception`)
- [**#1157**](https://github.com/reactiveui/ReactiveUI/pull/1157) Fix `InvokeCommand` to respect the command's execution window
- [**#1091**](https://github.com/reactiveui/ReactiveUI/pull/1091) Add `WhenActivated` overload that takes a `CompositeDisposable`, as well as a `DisposeWith` extension method
- [**#971**](https://github.com/reactiveui/ReactiveUI/pull/971) [**#1035**](https://github.com/reactiveui/ReactiveUI/pull/1035) [**#1054**](https://github.com/reactiveui/ReactiveUI/pull/1054) [**#1085**](https://github.com/reactiveui/ReactiveUI/pull/1085) [**#1088**](https://github.com/reactiveui/ReactiveUI/pull/1088) Overhaul reactive commands
- [**#1084**](https://github.com/reactiveui/ReactiveUI/pull/1084) Enhance `ToProperty` to work with indexers
- [**#1082**](https://github.com/reactiveui/ReactiveUI/pull/1082) Add scheduling support to `RoutingState`
- [**#1079**](https://github.com/reactiveui/ReactiveUI/pull/1079) Add `Activated` and `Deactivated` observables to `ViewModelActivator`
- [**#1061**](https://github.com/reactiveui/ReactiveUI/pull/1061) `ToProperty` is no longer lazy by default
- [**#964**](https://github.com/reactiveui/ReactiveUI/pull/964) [**#1055**](https://github.com/reactiveui/ReactiveUI/pull/1055) [**#1181**](https://github.com/reactiveui/ReactiveUI/pull/1181) Add interactions feature
- [**#1052**](https://github.com/reactiveui/ReactiveUI/pull/1052) Add `Bind` overloads that convert via `Func`s
- [**#1028**](https://github.com/reactiveui/ReactiveUI/pull/1028) Improve `CreateCollection` overloads
- [**#1021**](https://github.com/reactiveui/ReactiveUI/pull/1021) Generalize auto-persistence helpers to work against `IReactiveCollection<T>`
- [**#1018**](https://github.com/reactiveui/ReactiveUI/pull/1018) `BindTo` now throws a helpful exception when attempting to bind to `null`
- [**#1015**](https://github.com/reactiveui/ReactiveUI/pull/1015) Enhance the default view locator to resolve views when the VM is abstracted as an interface
- [**#1013**](https://github.com/reactiveui/ReactiveUI/pull/1013) Add auto-registration feature for views within a given assembly
- [**#1012**](https://github.com/reactiveui/ReactiveUI/pull/1012) Add scheduler parameter to `CreateCollection`
- [**#977**](https://github.com/reactiveui/ReactiveUI/pull/977) Add `WhenNavigatedToObservable` method, telling you whenever a given view model is on top of the navigation stack
- [**#962**](https://github.com/reactiveui/ReactiveUI/pull/962) Fix a race condition in `ObservableAsPropertyHelper`
- [**#938**](https://github.com/reactiveui/ReactiveUI/pull/938) Don't automatically create a `DataTemplate` if the `DisplayMemberPath` of an `ItemsControl` is set
- [**#936**](https://github.com/reactiveui/ReactiveUI/pull/936) Ensure `ObservableAsPropertyHelper` provides the initial value immediately
- [**#926**](https://github.com/reactiveui/ReactiveUI/pull/926) Remove potentially expensive hash calculation from `ReactiveList`
- [**#914**](https://github.com/reactiveui/ReactiveUI/pull/914) Remove `ViewContractAttribute`
- [**#913**](https://github.com/reactiveui/ReactiveUI/pull/913) Remove `fallbackValue` from binding methods
- [**#875**](https://github.com/reactiveui/ReactiveUI/pull/875) Relax generic constraints on `ToProperty` from `ReactiveObject` to `IReactiveObject`
- [**#943**](https://github.com/reactiveui/ReactiveUI/pull/943) Add `ReactiveUserControl` to relevant XAML platforms
- [**#935**](https://github.com/reactiveui/ReactiveUI/pull/935) Add `XmlnsDefinitionAttribute` for those XAML platforms that support it
- [**#879**](https://github.com/reactiveui/ReactiveUI/pull/879) Add `ViewContract` property to `ViewModelViewHost` for XAML platforms
- [**#1149**](https://github.com/reactiveui/ReactiveUI/pull/1149) Set nuspec dependency to == (exact) version as that is being built
- [**#1142**](https://github.com/reactiveui/ReactiveUI/pull/1142) Fix Splat dependency
- [**#1110**](https://github.com/reactiveui/ReactiveUI/pull/1110) Fix `WaitForScheduler` exception handling

**Events Generator**
- [**#1151**](https://github.com/reactiveui/ReactiveUI/pull/1151) reactiveui-events now targets UWP 10.0.14393.0 (aka anniversary edition)
- [**#1056**](https://github.com/reactiveui/ReactiveUI/pull/1056) eventbuilder.exe is now xplat and always uses the latest SDK's
- [**#1187**](https://github.com/reactiveui/ReactiveUI/pull/1187) Ignore CS[1591,0618,0105,0672] because that code is generated

**Windows Phone, Store and Universal**
- [**#1192**](https://github.com/reactiveui/ReactiveUI/pull/1192) Fixed ReactiveUI to install into a library targeting WPA81 (Profile111)
- [**#1174**](https://github.com/reactiveui/ReactiveUI/pull/1174) Retired Windows Phone Silverlight
- [**#1099**](https://github.com/reactiveui/ReactiveUI/pull/1099) Changed `LaunchActivatedEventArgs` to `IActivatedEventArgs`
- [**#1045**](https://github.com/reactiveui/ReactiveUI/pull/1045) Add Universal Windows Platform (UWP) support
- [**#940**](https://github.com/reactiveui/ReactiveUI/pull/940) Update platform UAP 10069 -> UWP 10240, add Events and Testing projects

**Xamarin Android**
- [**#1186**](https://github.com/reactiveui/ReactiveUI/pull/1186) Android timepicker now uses `Hour`/`Minute` instead of `CurrentHour`/`CurrentMinute`
- [**#1144**](https://github.com/reactiveui/ReactiveUI/pull/1144) Small bug fixes to `ReactiveRecyclerViewAdapter` and `ViewHolder`
- [**#1105**](https://github.com/reactiveui/ReactiveUI/pull/1105) Add `ReactiveAppCompatActivity`
- [**#1095**](https://github.com/reactiveui/ReactiveUI/pull/1095) Add `AsObservable` when `ISubject` is returned as `IObservable`
- [**#1067**](https://github.com/reactiveui/ReactiveUI/pull/1067) Add Android reactive preferences
- [**#1059**](https://github.com/reactiveui/ReactiveUI/pull/1059) Add Rx implementation of the Android shared preferences changed
- [**#1058**](https://github.com/reactiveui/ReactiveUI/pull/1058) Add Rx implementation of the Android USB permission request
- [**#1057**](https://github.com/reactiveui/ReactiveUI/pull/1057) Add Rx implementation of the Android service bind
- [**#1044**](https://github.com/reactiveui/ReactiveUI/pull/1044) Resolve an issue building android support project
- [**#985**](https://github.com/reactiveui/ReactiveUI/pull/985) Add `serializer.Serialize(st, state);` into `SaveState`
- [**#982**](https://github.com/reactiveui/ReactiveUI/pull/982) Add a basic reactive adapter for the Android `RecyclerView`
- [**#939**](https://github.com/reactiveui/ReactiveUI/pull/939) Add a reactive version of the `Android.Support.V4.DialogFragment` class
- [**#885**](https://github.com/reactiveui/ReactiveUI/pull/885) Add a `ControlFetcherMixin` for the Android Support library

**Xamarin Forms**
- [**#1160**](https://github.com/reactiveui/ReactiveUI/pull/1160) Use non-generic `BindableProperty.Create`, since the generic overloads are deprecated
- [**#952**](https://github.com/reactiveui/ReactiveUI/pull/952) [**#1115**](https://github.com/reactiveui/ReactiveUI/pull/1115) Add support for view activation
- [**#942**](https://github.com/reactiveui/ReactiveUI/pull/942) [**#1074**](https://github.com/reactiveui/ReactiveUI/pull/1074) [**#1071**](https://github.com/reactiveui/ReactiveUI/pull/1071) [**#986**](https://github.com/reactiveui/ReactiveUI/pull/986) Lots of routing fixes and improvements
- [**#1032**](https://github.com/reactiveui/ReactiveUI/pull/1032) Add reactive pages
- [**#921**](https://github.com/reactiveui/ReactiveUI/pull/921) Add reactive cells
- [**#953**](https://github.com/reactiveui/ReactiveUI/pull/953) Upgrade Xamarin Forms dependency
- [**#951**](https://github.com/reactiveui/ReactiveUI/pull/951) Throw a useful exception from `ViewModelViewHost` if the view is of the wrong type
- [**#950**](https://github.com/reactiveui/ReactiveUI/pull/950) Use `ContentView` as base class for `ViewModelViewHost`
- [**#919**](https://github.com/reactiveui/ReactiveUI/pull/919) Fix `ViewModelViewHost` bug where collection is modified whilst being enumerated
- [**#861**](https://github.com/reactiveui/ReactiveUI/pull/861) Retarget ReactiveUI.Xamforms to Profile259
- [**#1153**](https://github.com/reactiveui/ReactiveUI/pull/1153) Update reactiveui-xamforms nuspec dependency from v2.1.0.6529 to v2.3.1.114
- [**#1152**](https://github.com/reactiveui/ReactiveUI/pull/1152) Upgrade reactiveui-core for Xamarin.Forms from v2.2.0.31 to v2.3.1.114
- [**#1150**](https://github.com/reactiveui/ReactiveUI/pull/1150) Upgrade reactiveui-events for Xamarin.Forms from v2.2.0.31 to v2.3.1.114
- [**#1196**](https://github.com/reactiveui/ReactiveUI/pull/1196) Resolve a crash when using `ViewModelViewHost` with Xamarin Forms

**Xamarin iOS**
- [**#1182**](https://github.com/reactiveui/ReactiveUI/pull/1182) Use `ViewWillAppear` for iOS activation (not `ViewDidAppear`)
- [**#1176**](https://github.com/reactiveui/ReactiveUI/pull/1176) Migrate reactiveui-testing from Monotouch to Xamarin.iOS
- [**#1087**](https://github.com/reactiveui/ReactiveUI/pull/1087) Make `ReactiveNavigationController` activatable
- [**#1086**](https://github.com/reactiveui/ReactiveUI/pull/1086) Fix various `RoutedViewHost` issues
- [**#927**](https://github.com/reactiveui/ReactiveUI/pull/927) Rewrite `CommonReactiveSource` to be reactive and fix numerous bugs
- [**#917**](https://github.com/reactiveui/ReactiveUI/pull/917) Add `ReactiveSplitViewController`
- [**#874**](https://github.com/reactiveui/ReactiveUI/pull/874) Make iOS `ViewModelViewHost` an actual view controller
- [**#873**](https://github.com/reactiveui/ReactiveUI/pull/873) Fix a `CommonReactiveSource` race condition by ensuring that batched changes result in `BeginUpdates` call ASAP
- [**#1177**](https://github.com/reactiveui/ReactiveUI/pull/1177) Fix code generation of events with iOS 10

**Xamarin Mac**
- [**#1185**](https://github.com/reactiveui/ReactiveUI/pull/1185) Use Xamarin.Mac framework for reactiveui/reactiveui-testing/microsoft-reactive-testing
- [**#1175**](https://github.com/reactiveui/ReactiveUI/pull/1175) Xamarin.Mac (unified) now compiles on windows
- [**#1022**](https://github.com/reactiveui/ReactiveUI/pull/1022) Add `ViewAttributes` to Xamarin.Mac projects
- [**#916**](https://github.com/reactiveui/ReactiveUI/pull/916) Fix `ViewModelViewHost` on Mac
- [**#887**](https://github.com/reactiveui/ReactiveUI/pull/887) Changed output to Xamarin.Mac20 to reflect reality and added .nuspec

**Housekeeping**
- [**#1188**](https://github.com/reactiveui/ReactiveUI/pull/1188) Upload .dll artefacts to AppVeyor on successful build
- [**#1179**](https://github.com/reactiveui/ReactiveUI/pull/1179) Run unit tests as part of continuous integration pipeline
- [**#1178**](https://github.com/reactiveui/ReactiveUI/pull/1178) Resolved an issue with continuous integration pipeline
- [**#1148**](https://github.com/reactiveui/ReactiveUI/pull/1148) Bootstrapper now downloads/installs Android API level 24
- [**#1140**](https://github.com/reactiveui/ReactiveUI/pull/1140) Add GitHub issue/PR templates
- [**#1128**](https://github.com/reactiveui/ReactiveUI/pull/1128) Enable strict mode (fail the build on any powershell error)
- [**#1092**](https://github.com/reactiveui/ReactiveUI/pull/1092) Continuous integration and pre-release builds for v7
- [**#1010**](https://github.com/reactiveui/ReactiveUI/pull/1010) Compiler warnings for events disabled to reduce CI log spam

**Documentation**
- [**#1003**](https://github.com/reactiveui/ReactiveUI/pull/1003) Initial revamp of README.md
- [**#898**](https://github.com/reactiveui/ReactiveUI/pull/898) Update compelling example in README.md for v6.x

### Where to get it

You can download this release from [nuget.org](https://www.nuget.org/packages/reactiveui/7.0.0)
