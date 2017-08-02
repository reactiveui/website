# Windows Universal

# Quirks

See https://github.com/reactiveui/ReactiveUI/issues/1221

# Bindings

There are three core ways to do bindings:

* XAML `{Binding blah}` uses reflection based binding. It's fragile, magic strings, dymanic typed.
* XAML `{x:Bind blah}` is strongly typed and does not use reflection.
* RXUI whenactivated, uses reflection but your bindings are more cross-platform. If you keep your property names consistent between iOS Android and WPF/UWP then you'll be able to copy and pasta.

    moswald [1:58 AM] 
    @onovotny: when doing your bindings, do they look like this?
    `<TextBlock Text="{x:Bind ViewModel.SomeProperty}" />`

    ​[1:58] 
    the `ViewModel.` part looks weird

    onovotny [1:59 AM] 
    yes

    ​[1:59] 
    because the datacontext of x:Bind is always the page

    ​[1:59] 
    for perf reasons

    ​[1:59] 
    and type safety

    ​[2:00] 
    DataContext is of type Object

    ​[2:00] 
    so you can't get type safety by using it

    ​[2:00] 
    most pages that I have will have a this.DataContextChanged += {ViewModel = DataContext
    as MyViewModel} in the ctor

    ​[2:01] 
    so that way when the datacontext changes it sets the strongly-typed ViewModel property

    ​[2:01] 
    I'd think that `IViewFor<T>` would do good things for this

    ​[2:02] 
    is `CommandParameter` supported better here?

    ​[2:02] 
    I really missed that with the RxUI 6 commands

    moswald [2:03 AM] 
    not sure yet

    ​[2:03] 
    just now trying x:Bind with RxUI for the first time

    onovotny [2:03 AM] 
    I know that ICommand uses them as Object but I'd be ok with an exception if the types mismatch

    moswald [2:03 AM] 
    I'll look into it(edited)

    onovotny [2:03 AM] 
    thx

    ​[2:04] 
    for `CommandParameter` it'd be great if it worked for regular binding and
    x:Bind (shouldn't be any different code-wise)

    moswald [2:14 AM] 
    ugh. have you used an `IViewFor` like this before?

    ​[2:15] 
    because the bindings aren't working for me, and intellisense is saying "ambiguous reference" since there are two `ViewModel` properties

    ​[2:19] 
    no, nvm, that's not it

    ​[2:19] 
    intellisense didn't like it, but that wasn't my problem. just a regular typo :simple_smile:

    ​[2:23] 
    @onovotny: oh even better: `CommandParameter` isn't just supported, it's now type-safe(edited)

    ​[2:24] 
    and if specified, either with the RxCommand indicating it needs a parameter, ​_or_​ if `CommandParameter` is
    supplied but the RxCommand ​_doesn't_​ take a parameter, it will be a compile-time error

    onovotny [2:25 AM] 
    Cool. That'd be a good documentation sample

    ​[2:26] 
    Showing how a mistyped param is a compile time error



