ShowInSidebar: false
---
title: ReactiveUI v8.0.0 released
category: Release Notes
author: Geoffrey Huntley
---

Hello ;-)

We never want to do a release this big ever again. A lot of time and effort went into this release from dedicated folks in the community.

One of the biggest factors in this release was that Microsoft changed their toolchain three times over and ReactiveUI was in the centre of that change. Dear Microsoft, never again please. We're happy with the end result however please take a peek out how the source code of ReactiveUI is now structured on the fileystem and how we multi-target multiple platforms using single projects and `Directory.build.prop`. Life for maintainers that target multiple platforms with dotnet is much better now than ever before. We were able to eliminate so much msbuild technical debt.

Big thank-you to Olly Levett who did the bulk of the conversion. Mate - you are like Mike Rowe - you did the dirty job and if you take a peek at Prism, MvvmCross and other upcoming projects you'll see that they followed your lead. Be proud of what you have accomplished.

<div class="youtube-video-container"><iframe src="https://www.youtube.com/embed/jBGBS3ylFhs" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>

Thank-you to Oren Novotony for your guidance, Immo Landwerth for your educational guidance on netstandard and David Kean who helped immensely with troubleshooting Visual Studio 2017 stability problems we encountered way back in the early days during this time of epic change. Rob Relyea please keep the communication channels open with the community because how we release software will be changing within the next month or so to a continuous delivery model. The ReactiveUI community has grown, we have new maintainers and our testing automation has matured to a point where will be releasing faster from this point onwards because we can. Consumers of ReactiveUI will no longer need to use our MyGet feed.

ICYMI - At MVP Summit, the community met with Microsoft and with their support and encouragement we forked control of System.Reactive. Head on over to [this blog post](https://reactiveui.net/blog/2018/04/the-future-of-system-reactive) for specifics as to what this means for the future of Reactive Programming on .NET.

You may or may not know this but ReactiveUI is maintained by unpaid volunteers. We put up a big marketing front but at its core is a couple of passionate folks. Open-source as it is today is [unbalanced and broken](https://www.youtube.com/watch?v=VS6IpvTWwkQ). If you’re relying on open-source software for your business and you haven’t secured your supply chain, you’re negligent. For the longest time the software industry has talked about open-source software in the terms of user freedom, but we have never stopped to think about the cost. The cost is too high. Lana Montgomery, thank-you for sponsoring Geoff's attendance to LinuxConfAu where it was confirmed that this is indeed a cross ecosystem and industry wide concern.

<div class="youtube-video-container"><iframe src="https://www.youtube.com/embed/0t85TyH-h04" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>

It has been amazing and humbling experience seeing [other maintainers](https://twitter.com/brianlagunas/status/990962346087477249) of dotnet projects also come to the same realisation as us. Due to their observations they are now implementing measures to [restore the imbalance of open-source software](https://nadiaeghbal.com/oss). For any maintainers still on the fence about this topic please open a conversation with us on our slack channel and we'll share with you ideas and guide you through your setup. ReactiveUI is now a vibrant community of developers from 22 timezones that supports the projects they depend on. Reactive programming in .NET is now [financially subsidised to the tune of $7,580USD per year by community crowdsourcing and corporate sponsorship](https://reactiveui.net/support). We are on track to exceed $10,000USD by the end of 2018.

<blockquote class="twitter-tweet" data-lang="en"><p lang="en" dir="ltr">❤️ Thank-you for your support. What you have authored below is the essence of opensource. We do this stuff for others like yourself, we trade favours, we find things that annoys us &amp; we fix it. Helping out transforms the relationship dynamic. If you have an idea, dig in and do it. <a href="https://t.co/uMPSqg9ex3">https://t.co/uMPSqg9ex3</a></p>&mdash; 𝓖𝓮𝓸𝓯𝓯𝓻𝓮𝔂 𝓗𝓾𝓷𝓽𝓵𝓮𝔂 (@GeoffreyHuntley) <a href="https://twitter.com/GeoffreyHuntley/status/991517552734957569?ref_src=twsrc%5Etfw">May 2, 2018</a></blockquote>

Thank-you to our backers who are making sustainable open-source a reality:

<a href="https://opencollective.com/reactiveui/sponsor/0/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/0/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/1/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/1/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/2/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/2/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/3/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/3/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/4/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/4/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/5/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/5/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/6/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/6/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/7/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/7/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/8/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/8/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/9/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/9/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/10/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/10/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/11/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/11/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/12/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/12/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/13/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/13/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/14/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/14/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/15/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/15/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/16/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/16/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/17/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/17/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/18/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/18/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/19/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/19/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/20/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/20/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/21/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/21/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/22/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/22/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/23/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/23/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/24/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/24/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/25/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/25/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/26/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/26/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/27/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/27/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/28/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/28/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/sponsor/29/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/sponsor/29/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/0/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/0/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/1/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/1/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/2/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/2/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/3/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/3/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/4/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/4/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/5/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/5/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/6/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/6/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/7/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/7/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/8/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/8/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/9/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/9/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/10/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/10/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/11/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/11/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/12/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/12/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/13/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/13/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/14/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/14/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/15/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/15/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/16/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/16/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/17/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/17/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/18/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/18/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/19/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/19/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/20/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/20/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/21/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/21/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/22/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/22/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/23/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/23/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/24/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/24/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/25/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/25/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/26/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/26/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/27/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/27/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/28/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/28/avatar.svg"></a> <a href="https://opencollective.com/reactiveui/backer/29/website" target="_blank"><img alt="img" src="https://opencollective.com/reactiveui/backer/29/avatar.svg"></a>

Thank-you to every single person who shipped a pull-request into this release. 

- Alexey Zimarev
- Benjamin Tam
- Damien Doumer Tohin
- David Nelson
- Dominik Mydlil
- Geoffrey Huntley
- Giusepe Casagrande
- Glenn Watson
- Grzegorz Kotfis
- Jeremy Koritzinsky
- Jon Stødle
- Kent Boogaart
- Martin Björkström
- Olly Levett
- Oren Novotny
- Robert
- Rodney Littles II
- Shane Neuville
- Stefan Moonen
- Taylor Buchanan
- Tim Jones

If you can't (or won't) help out financially, then please donate your time. Join the conversation over at https://reactiveui.net/slack and ask how you can help. Thanks for any support you can offer if you decide to - Geoffrey Huntley

# New Features

We kept getting reports from folks who want to contribute to the project but their employer has a restrictive list of pre-approved licenses and MSPL isn't on that list.  So what if we enabled those to help us? It would be much easier than making them go through internal corporate bs. So that's what we have done - ReactiveUI is now available under the MIT licence. This means we are now fully compatible with the GPL and are in alignment with .NET Core, CoreCLR, CoreFX, Roslyn and Xamarin Forms.

This is the first release of [ReactiveUI since joining the .NET foundation](https://www.dotnetfoundation.org/blog/2017/08/08/welcome-reactiveui-to-the-net-foundation). It contains [security features that allow you to verify the authenticity of assemblies provided by us](https://reactiveui.net/blog/2017/09/assemblies-are-now-code-signed-with-the-dotnet-foundation-certificate). 

This is a housekeeping release to ensure that ReactiveUI is in a good position going forward. We have removed a massive amount of maintainer technical debt. The public API surface of ReactiveUI has not regressed in any way and we added automatic tests that validate this as part of the release. We will be adding additional features such as [automated on-device, end-to-end integration tests](https://github.com/reactiveui/ReactiveUI/pull/1598) in the coming months to make the source more accessible and safer for folks to contribute to the project. 

We have added netcoreapp20 as a supported platform. You can use ReactiveUI on the server-side. Maintainers are super excited about the future of WebAssembly and it allows you to unit test ReactiveUI using dotnet core unit test libraries.

We also fixed that niggling problem that prevented folks from writing unit tests for ReactiveUI applications using Visual Studio for Mac.

# Upgrading
This is a big release so it's up to you to decide when it is best for you to upgrade. We don't recommend holding off on upgrading because of changes in the .NET toolchain. It is now impossible to compile previous releases below this release. Know that the maintainers and community of ReactiveUI have been running 183 editions of alpha releases in production over the last eight months. Yesterday we went through and manually tested every platform and are only weeks away from adding full end-to-end regression tests. To us, it's risker to hold off upgrading - we recommend that if you haven't started your netstandard upgrade spike that you do it so now. The longer you hold off on the upgrade the more pain you will experience consuming opensource in the post netstandard world.

When you install ReactiveUI we will bring in the correct version of System.Reactive (currently 3.11) as part of installation as a transitive reference. You'll need to manually remove/upgrade all references to the Reactive Extensions that are below 3.11 that aren't netstandard. Do not attempt to do binding redirects from 2.x to 3.x series. It won't work. It's not worth it. You'll end up with nothing but being unhappy and have a wasted afternoon.

ReactiveUI is now netstandard20 compatible. We have dropped support for portable class libraries. You may be able to install netstandard20 into your portable class library but both Microsoft and the maintainers of ReactiveUI strongly recommend against doing this as you'll be fighting an uphill battle. The correct path to take is File -> New -> Netstandard library and drag your code into the new project. Then install the netstandard compatible versions of any dependencies that you have.

The first thing you'll notice after creating your netstandard20 class libraries is that the name of the ReactiveUI packages on NuGet have subtly changed. Instead of using `-` we now use `.` in our package names. We did this because it allows the maintainers to reserve the namespace of ReactiveUI on NuGet and provide you guarantees that if it's in the `ReactiveUI.*` namespace then it is an official package that was produced by us. Look for the Blue Tick (tm). The [documentation has been updated with instructions](https://reactiveui.net/docs/getting-started/installation/nuget-packages/) of which packages you should install on each platform.

If you target the WPF platform you now need to install the WPF specific package otherwise schedulers won't be wired up and you'll be greeted with an appropriate exception. The documentation has been updated. We had to make this change prevent Visual Studio for Mac from exploding when it encountered an automatically imported reference to Windows.Presentation which prevented these folks from being able to unit test their applications with Visual Studio for Mac.

The .NET toolchain has evolved quite a bit over the last year which provided an excellent opportunity to remove legacy platforms such as Silverlight and Windows Phone 8.x. 

ReactiveUI now uses monoandroid80 as the minsdk but you'll be able to target earlier versions of android by specifying the [targetsdk compilation option](https://stackoverflow.com/a/27629181).

The minimum version of .NET framework is now net461 which means you'll need to adjust your unit test projects and WinForms/WPF applications. We chose net461 as it's in alignment with Microsoft's official support policy and net461 is now the default option provided in Visual Studio 2017 when scaffolding File -> New Project.

The minimum version of UWP is now 10.0.16299 aka Fall Creators Update. The maintainers picked this as it felt like a reasonable baseline to us and it helps reduce the amount of SDK's one needs to have installed to be contribute to ReactiveUI. If you need to target anything earlier than this then you will need to compile from source. It's as simple as Right Click -> Change Target -> Compile.

The minimum version of Xamarin forms is now 2.5.1.444934. We picked this because it was the latest version available on NuGet when we were publishing the release. If you need to target an older version then you will need to compile from source.

# Detailed release notes


As part of this release we had [138 commits](https://github.com/reactiveui/reactiveui/compare/7.4.0...8.0.0) which resulted in [113 issues](https://github.com/reactiveui/ReactiveUI/issues?milestone=4&state=closed) being closed.


__All Platforms__

- [__#1530__](https://github.com/reactiveui/ReactiveUI/pull/1530) housekeeping:  use new nuget package names to enable nuget namespace reservation
- [__#1522__](https://github.com/reactiveui/ReactiveUI/pull/1522) feature: nameof() overload for ObservableAsPropertyHelper mixin
- [__#1498__](https://github.com/reactiveui/ReactiveUI/issues/1498) ReactiveCommand in current 8.0 alpha concurrency problem
- [__#1476__](https://github.com/reactiveui/ReactiveUI/issues/1476) InvokeCommand not executed in v8 where v7.4 would
- [__#1472__](https://github.com/reactiveui/ReactiveUI/pull/1472) test: use new threads explicitly in message bus test
- [__#1461__](https://github.com/reactiveui/ReactiveUI/pull/1461) feature: add approval tests
- [__#1456__](https://github.com/reactiveui/ReactiveUI/pull/1456) fix: added netstandard 1.1 build and added clamped system.reactive references
- [__#1444__](https://github.com/reactiveui/ReactiveUI/pull/1444) feature: add code snippets for VS, VS4Mac and Rider
- [__#1441__](https://github.com/reactiveui/ReactiveUI/pull/1441) core: make ViewContractAttribute.Contract public
- [__#1440__](https://github.com/reactiveui/ReactiveUI/issues/1440) Make ViewContractAttribute.Contract public
- [__#1439__](https://github.com/reactiveui/ReactiveUI/pull/1439) chore: remove legacy code
- [__#1426__](https://github.com/reactiveui/ReactiveUI/pull/1426) housekeeping: added identity signing to NuGet packages
- [__#1425__](https://github.com/reactiveui/ReactiveUI/pull/1425) chore: remove SILVERLIGHT #ifdefs
- [__#1423__](https://github.com/reactiveui/ReactiveUI/pull/1423) chore: cull Splat methods that were never removed
- [__#1416__](https://github.com/reactiveui/ReactiveUI/pull/1416) chore: use pre-release visual studio 2017 sdk to work around tooling issues
- [__#1394__](https://github.com/reactiveui/ReactiveUI/pull/1394) chore: tidy up comments in ReactiveNotifyPropertyChangedMixins
- [__#1392__](https://github.com/reactiveui/ReactiveUI/pull/1392) test: add additional unit test to cover Activation returning IDisposable
- [__#1391__](https://github.com/reactiveui/ReactiveUI/pull/1391) chore: update comments in ReactiveCommand
- [__#1390__](https://github.com/reactiveui/ReactiveUI/pull/1390) chore: update logging mixing XML comments
- [__#1389__](https://github.com/reactiveui/ReactiveUI/pull/1389) Tidy up comments in legacy ReactiveCommand
- [__#1367__](https://github.com/reactiveui/ReactiveUI/pull/1367) fix: pin child packages of our main project be locked to the version that's being built
- [__#1355__](https://github.com/reactiveui/ReactiveUI/pull/1355) chore: split platform registrations into platform specific files
- [__#1327__](https://github.com/reactiveui/ReactiveUI/pull/1327) First pass at updating to VS2017 csproj file
- [__#1304__](https://github.com/reactiveui/ReactiveUI/issues/1304) [Bug] CS1701 / CS1702 System.Runtime reference mismatch
- [__#1190__](https://github.com/reactiveui/ReactiveUI/issues/1190) .NETStandard support?
- [__#1139__](https://github.com/reactiveui/ReactiveUI/issues/1139) Reference Rx 3.0.0 NuGet packages
- [__#780__](https://github.com/reactiveui/ReactiveUI/issues/780) Running Unit Tests under Xamarin Studio without Xam.Mac

__Events Generator__

- [__#1529__](https://github.com/reactiveui/ReactiveUI/pull/1529) fix: this.Events() was not available for consumption due to assembly name conflicts
- [__#1526__](https://github.com/reactiveui/ReactiveUI/issues/1526) reactiveui-events not working for platform projects possibly related to PackageReference style projects
- [__#1514__](https://github.com/reactiveui/ReactiveUI/pull/1514) feat: propagate obsolete attributes to generated observables
- [__#1495__](https://github.com/reactiveui/ReactiveUI/issues/1495) reactive-ui events doesn't have the WPF target anymore in 8.0 alpha 89
- [__#1371__](https://github.com/reactiveui/ReactiveUI/pull/1371) housekeeping: only include events source if present
- [__#1359__](https://github.com/reactiveui/ReactiveUI/pull/1359) fix: split xamarin forms events BACK into seperate package to prevent future targeting conflicts

__Windows Forms__

- [__#1462__](https://github.com/reactiveui/ReactiveUI/pull/1462) chore: fix xml doc on ReactiveDerivedBindingListMixins

__Universal Windows Platform__

- [__#1452__](https://github.com/reactiveui/ReactiveUI/pull/1452) fix: set UWP min version to 10.0.10586.0

__Windows Presentation Foundation__

- [__#1556__](https://github.com/reactiveui/ReactiveUI/pull/1556) fix: include xml docs in WPF package
- [__#1504__](https://github.com/reactiveui/ReactiveUI/issues/1504) reactiveui-wpf.csproj triggers defect/crash in visual studio for windows when opening reactiveui.sln
- [__#1450__](https://github.com/reactiveui/ReactiveUI/pull/1450) fix: min platform should be net452; was net452 before migration to new csproj format
- [__#1433__](https://github.com/reactiveui/ReactiveUI/pull/1433) fix: remove duplicate ComponentModelTypeConverter
- [__#1427__](https://github.com/reactiveui/ReactiveUI/pull/1427) fix: include theme in Reactive.Wpf
- [__#1424__](https://github.com/reactiveui/ReactiveUI/issues/1424) The themes directory hasn't been included in `reactiveui-wpf` so the templates don't resolve
- [__#1406__](https://github.com/reactiveui/ReactiveUI/pull/1406) feature: move wpf out of main package to fix unit testing story on mono and unblock other ui frameworks

__Xamarin Android__

- [__#1589__](https://github.com/reactiveui/ReactiveUI/pull/1589) fix: runtime API check for the Android time picker (#1233)
- [__#1519__](https://github.com/reactiveui/ReactiveUI/pull/1519) feature: target monoandroid80 instead of monoandroid70 and upgraded to 26.1.0.1 support lib
- [__#1517__](https://github.com/reactiveui/ReactiveUI/issues/1517) VS2017 15.4 issues
- [__#1496__](https://github.com/reactiveui/ReactiveUI/pull/1496) feature: added ctors for JNI ownership transfer in ReactiveAppCompatActivity

__Xamarin Forms__

- [__#1493__](https://github.com/reactiveui/ReactiveUI/pull/1493) feature: bumped to xamarin forms 2.4

__Xamarin iOS__

- [__#1485__](https://github.com/reactiveui/ReactiveUI/issues/1485) [iOS] Circular dependency detected 'reactiveui 8.0.0-alpha0089 => System.Reactive 3.1.1 => System.Reactive.PlatformServices 4.0.0-preview00001 => System.Reactive 3.1.1'.
- [__#1437__](https://github.com/reactiveui/ReactiveUI/pull/1437) Changed to add pending changes all at the same starting index when using RemoveRange
- [__#1405__](https://github.com/reactiveui/ReactiveUI/pull/1405) Raise ViewWillAppear for child even if parent is already visible
- [__#1303__](https://github.com/reactiveui/ReactiveUI/pull/1303) Removed all obsolete UNIFIED symbol checks

__Housekeeping__

- [__#1606__](https://github.com/reactiveui/ReactiveUI/pull/1606) housekeeping: fix configuration for tizen
- [__#1594__](https://github.com/reactiveui/ReactiveUI/pull/1594) chore: use same vs2017 appveyor image for master and develop
- [__#1591__](https://github.com/reactiveui/ReactiveUI/pull/1591) docs: add details about developer environment set up
- [__#1587__](https://github.com/reactiveui/ReactiveUI/pull/1587) househeeping: resolve broken CI infrastructure

- [__#1579__](https://github.com/reactiveui/ReactiveUI/pull/1579) fix: use netstandard 2.0 compatible version of splat
- [__#1568__](https://github.com/reactiveui/ReactiveUI/pull/1568) fix ReactiveUI nuget dependency not being pinned correctly
- [__#1563__](https://github.com/reactiveui/ReactiveUI/pull/1563) Add .idea to gitignore to simplify use of JetBrains Rider
- [__#1554__](https://github.com/reactiveui/ReactiveUI/pull/1554) chore: fix build
- [__#1535__](https://github.com/reactiveui/ReactiveUI/issues/1535) build.cake fails if the pwd contains spaces
- [__#1532__](https://github.com/reactiveui/ReactiveUI/pull/1532) housekeeping: compile csproj with multiple target frameworks in parallel
- [__#1524__](https://github.com/reactiveui/ReactiveUI/pull/1524) housekeeping: remove fully qualified test names in test explorer (#1328)
- [__#1518__](https://github.com/reactiveui/ReactiveUI/pull/1518) housekeeping: github has spoken this is the new convention
- [__#1515__](https://github.com/reactiveui/ReactiveUI/issues/1515) Add git hash to nuget package / assembly info
- [__#1511__](https://github.com/reactiveui/ReactiveUI/pull/1511) housekeeping: net45 legacy project with CPS workaround
- [__#1500__](https://github.com/reactiveui/ReactiveUI/pull/1500) housekeeping: the f in wpf is foundation, not framework
- [__#1497__](https://github.com/reactiveui/ReactiveUI/pull/1497) housekeeping: fixed broken link to stackoverflow
- [__#1490__](https://github.com/reactiveui/ReactiveUI/pull/1490) housekeeping: upload msbuild binlogs even when the build fails
- [__#1489__](https://github.com/reactiveui/ReactiveUI/pull/1489) housekeeping: remove && exit 0
- [__#1483__](https://github.com/reactiveui/ReactiveUI/pull/1483) housekeeping: specify cake version in bootstrap script
- [__#1482__](https://github.com/reactiveui/ReactiveUI/pull/1482) housekeeping: pinned Cake 0.21.0 and tools/addins to current version
- [__#1479__](https://github.com/reactiveui/ReactiveUI/pull/1479) housekeeping: move the code of conduct to the website
- [__#1478__](https://github.com/reactiveui/ReactiveUI/pull/1478) housekeeping: cleanup of readme, contributing guide, pull-request template, coc, license
- [__#1475__](https://github.com/reactiveui/ReactiveUI/pull/1475) housekeeping: copy API approval results to appveyor so maintainers don't need to re-run builds
- [__#1474__](https://github.com/reactiveui/ReactiveUI/issues/1474) Cake.PinNuGetDepenencies needs upgrading to Cake 0.22 soon or our builds will break
- [__#1471__](https://github.com/reactiveui/ReactiveUI/issues/1471) Build status badge is out of date
- [__#1470__](https://github.com/reactiveui/ReactiveUI/pull/1470) Remove build.sh
- [__#1469__](https://github.com/reactiveui/ReactiveUI/pull/1469) Update CONTRIBUTING.md to reference 'develop' branch instead of 'master'
- [__#1468__](https://github.com/reactiveui/ReactiveUI/pull/1468) housekeeping: don't autoclose dx issues or cx issues
- [__#1458__](https://github.com/reactiveui/ReactiveUI/pull/1458) housekeeping: improve stability of CI when re-running a failed build
- [__#1454__](https://github.com/reactiveui/ReactiveUI/pull/1454) housekeeping: add license to nuget package
- [__#1449__](https://github.com/reactiveui/ReactiveUI/pull/1449) housekeeping: SignPackages throws an exception when it fails
- [__#1448__](https://github.com/reactiveui/ReactiveUI/pull/1448) housekeeping: ¯\_(ツ)_/¯ and proposal labels won't be autoclosed
- [__#1446__](https://github.com/reactiveui/ReactiveUI/pull/1446) housekeeping: upgrade MSBuild.Sdk.Extras
- [__#1443__](https://github.com/reactiveui/ReactiveUI/issues/1443) Set TargetPlatformMinVer
- [__#1442__](https://github.com/reactiveui/ReactiveUI/pull/1442) housekeeping: update to MSBuild.Sdk.Extras 1.0.9
- [__#1436__](https://github.com/reactiveui/ReactiveUI/pull/1436) housekeeping: merged probot message into single line

- [__#1435__](https://github.com/reactiveui/ReactiveUI/pull/1435) housekeeping: automatically close stale github issues in a friendly way
- [__#1432__](https://github.com/reactiveui/ReactiveUI/pull/1432) housekeeping update ISSUE_TEMPLATE to match website
- [__#1431__](https://github.com/reactiveui/ReactiveUI/issues/1431) SignPackages does not throw exception when it fails
- [__#1430__](https://github.com/reactiveui/ReactiveUI/pull/1430) housekeeping: ignore cla github labels otherwise release will break
- [__#1420__](https://github.com/reactiveui/ReactiveUI/issues/1420) MSB4041: The default XML namespace of the project must be the MSBuild XML namespace
- [__#1419__](https://github.com/reactiveui/ReactiveUI/pull/1419) housekeeping: use .NET foundation appveyor infrastructure
- [__#1414__](https://github.com/reactiveui/ReactiveUI/pull/1414) housekeeping: nuget packaging :lipstick:
- [__#1410__](https://github.com/reactiveui/ReactiveUI/pull/1410) housekeeping: upgraded eventbuilder to vs2017 project format
- [__#1408__](https://github.com/reactiveui/ReactiveUI/pull/1408) housekeeping: added .net foundation license headers
- [__#1407__](https://github.com/reactiveui/ReactiveUI/pull/1407) housekeeping: bumped next version to be v8
- [__#1395__](https://github.com/reactiveui/ReactiveUI/pull/1395) chore: use editorconfig in place of rebracer
- [__#1373__](https://github.com/reactiveui/ReactiveUI/pull/1373) chore: remove CommonAssemblyInfo from git
- [__#1363__](https://github.com/reactiveui/ReactiveUI/pull/1363) chore: don't publish ReactiveUI-Core as package no longer exists
- [__#1358__](https://github.com/reactiveui/ReactiveUI/pull/1358) housekeeping: github labels updated to rename uwp & wpf, drop windows phone/store, add tizen
- [__#1357__](https://github.com/reactiveui/ReactiveUI/pull/1357) housekeeping: retrieve System.Windows.Interactivity from NuGet so contributors don't need to install the SDK
- [__#1328__](https://github.com/reactiveui/ReactiveUI/issues/1328) Remove fully qualified test names in test explorer
- [__#1232__](https://github.com/reactiveui/ReactiveUI/issues/1232) Using RxUI with Rx v3.x possible?
- [__#1122__](https://github.com/reactiveui/ReactiveUI/issues/1122) reactiveui-testing v7 does not install into Profile111 (netstandard1.1)

__Documentation__

- [__#1562__](https://github.com/reactiveui/ReactiveUI/pull/1562) Fix default values for snippet variables in Rider
- [__#1506__](https://github.com/reactiveui/ReactiveUI/pull/1506) docs: upgraded version of xam forms and changed ondestroy to use flush instead of shutdown
- [__#1502__](https://github.com/reactiveui/ReactiveUI/pull/1502) housekeeping: let's keep cinephile as an xamarin.forms app
- [__#1459__](https://github.com/reactiveui/ReactiveUI/pull/1459) documentation: added cinephile application
- [__#1457__](https://github.com/reactiveui/ReactiveUI/pull/1457) documentation: added baseline for xamarin.forms sample application
- [__#1415__](https://github.com/reactiveui/ReactiveUI/pull/1415) Improve wording on PR template
- [__#1412__](https://github.com/reactiveui/ReactiveUI/pull/1412) chore: tweaking comments in TestUtils
- [__#1204__](https://github.com/reactiveui/ReactiveUI/pull/1204) docs: email Geoff Huntley, not Anaïs Betts for slack invitation

# Where to get it

You can download this release from [nuget.org](https://www.nuget.org/packages/reactiveui/8.0.0)
