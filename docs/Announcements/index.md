---
hide:
  - toc
---

# Announcements

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2021-01-04-reactiveui-association.md">ReactiveUI Association, Inc.</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2021-01-04">January 4, 2021</time><span class="md-post__author"> — Rodney Littles, II</span>
  </div>
  <p class="md-post__excerpt">Open Source Sustainability has become a big topic in the .NET community.  With several Open Source projects having issues with long term sustainability, the ReactiveUI team has been identifying ways to address the long term sustainability of our open-source software.  Our main goal is to make sure that as .NET grows and evolves that Reactive Programming and ReactiveUI evolve and grow with it.  We have started to talk about strategies to ensure that ReactiveUI has maintenance and growth for the future.</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2019-01-20-reactiveui-rebranding.md">ReactiveUI Rebranding</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2019-01-20">January 20, 2019</time><span class="md-post__author"> — Artyom Gorchakov</span>
  </div>
  <p class="md-post__excerpt">ReactiveUI was created by Anaïs Betts whilst working in Office Labs at Microsoft, and released in early 2011. Since that time, ReactiveUI has become a mature framework [trusted by Slack, GitHub, Amazon, Elastic and Microsoft](https://github.com/reactiveui/ReactiveUI/issues/979#issuecomment-196735701), has become a member of the [Dot Net Foundation](https://www.dotnetfoundation.org/), and actively maintained by the open-source community on GitHub. The work we do is sponsored by [our wonderful Backers on Reactive Marbles](https://github.com/sponsors/reactivemarbles).</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2018-11-07-reactive-command-abstract.md">Removing ReactiveCommand abstract base class</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2018-11-07">November 7, 2018</time><span class="md-post__author"> — Rodney Littles, II</span>
  </div>
  <p class="md-post__excerpt">The ReactiveUI team has been urging consumers for some time now to move away from the `ReactiveCommand` abstract class for properties.  There are some slight nuiances with type constraints that can sometimes cause run time bugs.  Because your property can resolve to an abstract base implementation doesn't mean you should define it that way.  We are very adamante about creating a type safe environment where consumers don't have to worry about hidden runtime issues with the framework. [RFC: Remove ReactiveCommand abstract base class](https://github.com/reactiveui/rfcs/issues/19) was raised to address this exact issue.</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2018-08-28-reactiveui-succession-update.md">ReactiveUI Succession Update</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2018-08-28">August 28, 2018</time><span class="md-post__author"> — Rodney Littles, II</span>
  </div>
  <p class="md-post__excerpt">ReactiveUI has released several minor versions since 8.0.  Some of the most recent versions have been released without Geoff having to push code, or prep issues.  This is thanks to members of the core team, and community members who have stepped up to move the project forward.  Glenn Watson has started to play a large role in the maintenance of ReactiveUI.  There have been other's getting involved in enhancing some of the platform logic, and documentation.  Community members have been identifying and resolving issues that help make our framework better.  What does this mean for ReactiveUI succession? It's happening.  What does that mean for you?  It means we are working on ways to improve ReactiveUI and make it easier to consume.  We have several initiatives on the team and there are a few RFC's planned for the 9.0 release that might be of interest.</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2018-08-13-deprecated-reactive-list.md">Deprecating ReactiveList</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2018-08-13">August 13, 2018</time><span class="md-post__author"> — Rodney Littles, II</span>
  </div>
  <p class="md-post__excerpt">With the coming 8.6.1 release, `ReactiveList` will be marked as deprecated.  ReactiveList is being replaced by Roland Pheasants [Dynamic Data](https://github.com/reactivemarbles/DynamicData)</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2018-05-15-memory-leak-detection.md">Memory Leak Detection</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2018-05-15">May 15, 2018</time><span class="md-post__author"> — Geoffrey Huntley</span>
  </div>
  <p class="md-post__excerpt">Moments ago we merged [a contribution](https://github.com/reactiveui/ReactiveUI/pull/1527) by [Grzegorz Kotfis](https://twitter.com/gkotfis) that adds another layer of protection before the project moves towards [automated continuous delivery](https://reactiveui.net/blog/2018/05/moving-towards-vsts-and-continuous-deployment). The pull-request added a new project called `ReactiveUI.LeakTests` which allows the maintainers to specify tests that can determine memory leaks by checking memory usage for objects of a particular type, or tests that track memory traffic and fail in case the traffic exceeds some threshold.</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2018-05-14-reactiveui-succession.md">ReactiveUI Succession</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2018-05-14">May 14, 2018</time><span class="md-post__author"> — Rodney Littles, II</span>
  </div>
  <p class="md-post__excerpt">OSS project maintainers have a serious load on their shoulders.  They are responsible for authoring and maintaining tools that enable developers to deliver business value faster and easier.  Gone are the days where a developer has to start every project by defining their own IO, data structures and sub routines.  Modern developers rely on frameworks, third party libraries and other niceties that use to take hundreds of developer man hours before writing any business logic.  Being a maintainer, and focusing that level of effort is a labour of love, but a labour not the less.</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2018-05-07-dotnet-core-3-and-reactiveui.md">.NET Core 3 and ReactiveUI</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2018-05-07">May 7, 2018</time><span class="md-post__author"> — Geoffrey Huntley</span>
  </div>
  <p class="md-post__excerpt">At Microsoft Build Live today, Microsoft [shared a first look at their plans](</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2018-05-04-delisting-of-versions-before-8-0-0-from-nuget.md">Delisting of versions before 8.0.0 from NuGet</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2018-05-04">May 4, 2018</time><span class="md-post__author"> — Geoffrey Huntley</span>
  </div>
  <p class="md-post__excerpt">As part of the v8.0.0 release, ReactiveUI changed the field separator used in our package names from dashes to periods which is the standard convention used in dotnet. It was a breaking change, and our documentation was updated as part of the release to provide you [with instructions of which package is needed](https://reactiveui.net/docs/getting-started/installation/nuget-packages/) for every platform.</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2018-05-03-system-reactive-has-a-new-home-on-github.md">System.Reactive has a new home on GitHub</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2018-05-03">May 3, 2018</time><span class="md-post__author"> — Geoffrey Huntley</span>
  </div>
  <p class="md-post__excerpt">At MVP Summit, [the community met with Microsoft](https://github.com/Reactive-Extensions/Rx.NET/issues/466#issuecomment-370496523) and with their support and encouragement the community forked control of System.Reactive. Over the last couple weeks some of the smartest brains from multiple ecosystems have united in [our Slack instance](https://join.slack.com/t/reactivex/shared_invite/zt-lt48skpz-G5WDYOAuzA80_MByZrLT0g) to plan for the future.</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2018-05-02-moving-towards-vsts-and-continuous-deployment.md">Moving towards VSTS and continuous deployment</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2018-05-02">May 2, 2018</time><span class="md-post__author"> — Geoffrey Huntley</span>
  </div>
  <p class="md-post__excerpt">Howdy folks,</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2018-04-30-the-future-of-system-reactive.md">The future of System.Reactive</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2018-04-30">April 30, 2018</time><span class="md-post__author"> — Geoffrey Huntley</span>
  </div>
  <p class="md-post__excerpt">The last official release of System.Reactive by Microsoft was back in 2014. On the 16th of June 2016 Oren Novotny (with the help of Bart De Smet)  [moved Rx/Ix into the .NET Foundation](https://www.dotnetfoundation.org/blog/2016/06/16/rx-net-welcome).</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2018-04-22-you-i-and-reactiveui.md">You, I, and ReactiveUI</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2018-04-22">April 22, 2018</time><span class="md-post__author"> — Kent Boogaart</span>
  </div>
  <p class="md-post__excerpt">I'm elated to finally announce the launch of my book, _You, I, and ReactiveUI_. This has been a labor of love, and I'm sure you'll be as happy as I am with the result. You can find out more at [https://kent-boogaart.com/you-i-and-reactiveui/](https://kent-boogaart.com/you-i-and-reactiveui/).</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2017-09-13-run-a-meetup-are-you-looking-for-presenters.md">Run a meetup? Are you looking for presenters?</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2017-09-13">September 13, 2017</time><span class="md-post__author"> — Geoffrey Huntley</span>
  </div>
  <p class="md-post__excerpt">Announcing something pretty special today; I'm not sure if anyone has done this before so here we go.</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2017-09-07-the-public-api-surface-of-reactiveui-is-now-protected-by-approval-tests.md">The public API surface of ReactiveUI is now protected by API approval tests</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2017-09-07">September 7, 2017</time><span class="md-post__author"> — Geoffrey Huntley</span>
  </div>
  <p class="md-post__excerpt">When reviewing pull requests the maintainers used to manually eyeball the changes proposed in a pull-request to determine if there was a change to the public API surface. We now have new tests, one per platform that trip when the public API surface changes for that platform.</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2017-09-05-assemblies-are-now-code-signed-with-the-dotnet-foundation-certificate.md">Assemblies are now code signed with the .NET Foundation  certificate</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2017-09-05">September 5, 2017</time><span class="md-post__author"> — Geoffrey Huntley</span>
  </div>
  <p class="md-post__excerpt">Thanks to a legendary contribution from Oren Novotny, all ReactiveUI assemblies from `v8.0.0-alpha73` and onwards are code signed with the .NET Foundation certificate. Identity signing of the assemblies means that you can [verify the authenticity of the binaries](../docs/security/index.md)  used in your application and ensure they have not been tampered with as only assemblies compiled via the .NET Foundation CI infrastructure are signed with this certificate.</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

<article class="md-post">
  <header class="md-post__header">
    <h3 class="md-post__title"><a href="2017-08-25-automatic-closure-of-stale-github-issues.md">Automatic closure of stale GitHub issues</a></h3>
  </header>
  <div class="md-post__meta">
    <time datetime="2017-08-25">August 25, 2017</time><span class="md-post__author"> — Geoffrey Huntley</span>
  </div>
  <p class="md-post__excerpt">One of the perks of being a maintainer of an open-source project is you get to be part of the fantastic initiative by GitHub - [maintainers.github.com](https://maintainers.github.com) where some of the brightest minds in open-source leadership from across disciplines/ecosystems share their insights on how to run projects, productively at scale towards successful outcomes.</p>
  <div class="md-post__tags"><span class="md-tag">Announcement</span></div>
</article>

