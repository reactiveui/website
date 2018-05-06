---
title: The future of System.Reactive
category:
  - Announcement
  - System.Reactive
author: Geoffrey Huntley
---

The last official release of System.Reactive by Microsoft was back in 2014. On the 16th of June 2016 Oren Novotny (with the help of Bart De Smet)  [moved Rx/Ix into the .NET Foundation](https://www.dotnetfoundation.org/blog/2016/06/16/rx-net-welcome).

At MVP Summit, [the community met with Bart](https://github.com/Reactive-Extensions/Rx.NET/issues/466#issuecomment-370496523) and with his support+encouragement we forked control of the project. Forking allows Bart to focus on working on the successor of System.Reactive - [Reactor](https://vimeo.com/132192255) - which is kind of like Microsoft Orleans and Service Fabric. We hope this also creates capacity so Bart can work towards releasing Reactor as open-source and building an open-source community around it.

One of the most frustrating aspects of the backstory of System.Reactive on .NET is that it has been kicked around various factions of Redmond over the years, never owned by a product group or transitioned into a supported product by devdiv. The community would love to see System.Reactive transitioned into an officially supported product that is fully documented and made more accessible by the cloud developer advocate team.

It has always perplexed the reactive community as to why System.Reactive hasn't been invested into when [the financial sector](https://channel9.msdn.com/Events/TechEd/Australia/Tech-Ed-Australia-2011/ARC-MID206) is [built](https://github.com/AdaptiveConsulting/ReactiveTrader) upon [this technology](https://github.com/RolandPheasant/DynamicData). They use ReactiveUI + System.Reactive to [build their desktop applications](https://github.com/RolandPheasant/TailBlazer). Customers use System.Reactive as their [backbone for their event processing backends](https://medium.com/netflix-techblog/reactive-programming-at-netflix-b944d49874d2). Investing in Rx/Ix is an easy way and fast way to light up even more Azure consumption. 

<pre>¯\_(ツ)_/¯</pre>

Okay, so enough backstory - over the last week many folks in the community responded to [the call to action on Twitter](https://twitter.com/GeoffreyHuntley/status/986163246724861952) and here's what we think the next steps should be:

- [Finish the pull-request that merges assemblies and ship System.Reactive 4.0.](https://github.com/Reactive-Extensions/Rx.NET/pull/418)
- [Move the GitHub repository to exist under dotnet/reactive](https://github.com/Reactive-Extensions/Rx.NET/issues/466)
- [Add approval tests to the public API surface](https://github.com/Reactive-Extensions/Rx.NET/issues/479)
- [Increase test coverage and amount of platforms used as validated against by the continuous integration.](https://github.com/Reactive-Extensions/Rx.NET/issues/478)
- Review outstanding [issues](https://github.com/Reactive-Extensions/Rx.NET/issues) and [pull-requests](https://github.com/Reactive-Extensions/Rx.NET/pulls).
- [Solicit help from the community, the creators of these techniques and the innovators/maintainers from other ecosystems to help define the future and help make it happen](https://twitter.com/GeoffreyHuntley/status/986163246724861952).

You may notice a central theme here - make it more accessible and safer for folks to contribute to the project. We would love to do a performance pass but not before these basics are crossed off. *I'd like to take this time to point out that System.Reactive is still a solid and fine way to build your applications, now and into the future. It's mature, stable but it could be better. So the community is going to make it so.*

We are centralising everything to-do with System.Reactive on the ReactiveUI brand as it has the most vibrant, active community and the maintainers have the most skin the game. Please join the conversation over at https://reactiveui.net/slack in #rxnet and please consider [financially supporting the community](https://reactiveui.net/support) or If you can't (or won't) help out financially, then please donate your time.
