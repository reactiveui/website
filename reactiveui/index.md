---
Title: An advanced, composable, reactive model-view-viewmodel framework
_disableTocFilter: true
_layout: landing
---

<div class="container">
        <div class="row branding">
            <div class="span6 col-md-6">
                <div class="row">
                    <div class="col-md-4">
                      <img class="img-responsive branding-image" alt="ReactiveUI" src="images/logo.png" />
                    </div>
                    <div class="col-md-8">
                    	<h1 class="branding-title">ReactiveUI</h1>
                        <h3>
                        	An advanced, composable, functional reactive model-view-viewmodel framework for all .NET platforms!
                    	</h3>
                        <a class="branding-button" href="~/docs/getting-started/index.md">
                            🛠️ Get Started
                        </a> &nbsp;
                        <a class="branding-button secondary-button" href="https://github.com/reactiveui/reactiveui">
                    		⭐ Star on GitHub
                        </a>
                    </div>
                </div>
            </div>
            <div class="span6 col-md-6 hidden-sm hidden-xs">
                <br class="visible-xs visible-sm">
                <div class="code-sample">
                 	<pre class="branding-code">
<a class="text-danger" href="~/docs/handbook/view-models/index.md">this</a>.<a class="text-info" href="~/docs/handbook/when-any.md">WhenAnyValue</a>(x => x.SearchQuery)
    .<a class="text-info" href="https://reactivex.io/documentation/operators/debounce.html">Throttle</a>(<span class="text-danger">TimeSpan</span>.<span class="text-info">FromSeconds</span>(<span class="text-danger">0.8</span>), <span class="text-danger">RxApp</span>.<a class="text-info" href="~/docs/handbook/scheduling.md">TaskpoolScheduler</a>)
    .<a class="text-info" href="https://reactivex.io/documentation/operators/map.html">Select</a>(query => query?.<span class="text-info">Trim</span>())
    .<a class="text-info" href="https://reactivex.io/documentation/operators/distinct.html">DistinctUntilChanged</a>()
    .<a class="text-info" href="https://reactivex.io/documentation/operators/filter.html">Where</a>(query => !<span class="text-danger">string</span>.<span class="text-info">IsNullOrWhiteSpace</span>(query))
    .<a class="text-info" href="https://reactivex.io/documentation/operators/observeon.html">ObserveOn</a>(<span class="text-danger">RxApp</span>.<a class="text-info" href="~/docs/handbook/scheduling.md">MainThreadScheduler</a>)
    .<a class="text-info" href="~/docs/handbook/commands/index.md">InvokeCommand</a>(ExecuteSearch);</pre>
                </div>
            </div>
        </div>
    </div>
<div class="container" style="margin-top: 30px">
    <div class="row text-center">
        <div class="span6 col-md-4">
            <h3 class="branding-subheader">Declarative</h3>
            <p>Describe what you want, not how to do it &amp; rejoice in the increased readability of your code. Code is communication between people, that also happens to run on a computer. If you optimise for reading by humans, then over a long time your project will end up better. </p>
        </div>
        <div class="span6 col-md-4">
            <h3 class="branding-subheader">Composable</h3>
            <p>Create re-usable chunks of functionality that can be seamlessly integrated into your reactive pipelines. These chunks might be widely applicable, or specific to your application. Regardless, you have the power to write and <a href="docs/handbook/testing.md")">test code</a> once, and leverage it many times over.</p>
        </div>
        <div class="span6 col-md-4">
            <h3 class="branding-subheader">Cross-platform</h3>
            <p>Any device, any platform. Share business logic between your mobile and desktop applications. ReactiveUI has <a href="~/docs/getting-started/installation/index.md">first class support</a> for MAUI, Windows Presentation Foundation (WPF), Windows Forms, UNO Platform, Xamarin Forms, Xamarin.iOS, Xamarin.Android, Xamarin.Mac, &amp; Tizen.</p>
        </div>
    </div>
</div>
<div class="container">
    <div class="row" style="margin-top: 30px;">
        <div class="span6 col-md-6">
            <h3 class="branding-subheader">Scalable & Testable</h3>
            <p>ReactiveUI <a href="https://ericsink.com/entries/dont_use_rxui.html">copes gracefully as your application gets more complicated</a> because of the System.Reactive base  <a href="http://introtorx.com/Content/v1.0.10621.0/01_WhyRx.html#WhyRx" target="_blank">(See the Introduction to Rx to learn more)</a> on which ReactiveUI is built upon. These building blocks are particularly adept at expressing the relationship between a group of things that are changing. ReactiveUI is essentially a collection of extension methods that make expressing intention more convenient when implementing user interfaces.
            </p>
            <p>Waiting 3 seconds after receiving user input before instigating a request? Don't write a test that waits for 3 seconds - control time! Fast-forward 2.9 seconds, assert the request hasn't been sent, fast-forward another 0.1 seconds, and assert that it has. The functional idioms inherent in reactive programming lead to many other benefits including greater ease in testing your code &amp; you will be able to assert the correctness of features and aspects of your application that you thought were impossible to test.</p>
        </div>
        <div class="span6 col-md-6">
            <h3 class="branding-subheader">Open-source</h3>
            <p>ReactiveUI is developed under an <a href="https://github.com/reactiveui/ReactiveUI/blob/main/LICENSE" target="_blank">OSI-approved open source license</a>, making it freely usable and distributable, even for commercial use. We ❤ the people who are involved in this project, and we’d love <a href="~/Contribute/index.md">to have you on board</a>, especially if you are just getting started or have never contributed to open-source before.</p>
            <p>ReactiveUI is a <a href="https://dotnetfoundation.org/" target="_blank">.NET Foundation</a> project. Other projects that are associated with the foundation include the .NET Compiler Platform ("Roslyn") as well as the ASP.NET family of projects, .NET Core, MAUI, Avalonia &amp; Xamarin Forms.</p>
        </div>
    </div>
</div>
