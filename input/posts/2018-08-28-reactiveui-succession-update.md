NoTitle: true
IsBlog: true
Title: ReactiveUI Succession Update
Tags: Announcement
Author: Rodney Littles, II
Published: 2018-08-28
---

ReactiveUI has released several minor versions since 8.0.  Some of the most recent versions have been released without Geoff having to push code, or prep issues.  This is thanks to members of the core team, and community members who have stepped up to move the project forward.  Glenn Watson has started to play a large role in the maintenance of ReactiveUI.  There have been other's getting involved in enhancing some of the platform logic, and documentation.  Community members have been identifying and resolving issues that help make our framework better.  What does this mean for ReactiveUI succession? It's happening.  What does that mean for you?  It means we are working on ways to improve ReactiveUI and make it easier to consume.  We have several initiatives on the team and there are a few RFC's planned for the 9.0 release that might be of interest.

We are trying to make the contributor experience better.  [RFC 18](https://github.com/reactiveui/rfcs/issues/18) is about enforcing a consistent coding style across the entire code base.  We are going to configure analyzers that will enforce the [Core Fx](https://github.com/dotnet/corefx/blob/master/Documentation/coding-guidelines/coding-style.md#c-coding-style) rules.  This will remove the friction of contributors having to check to see if their changes are compliant with our rules before submitting a Pull Request!  This change will also add xml comments to our public API surface.  Going forward consumers of ReactiveUI will get intellisense comments for the public API inside the IDE.

Another, more significant change is [RFC 10](https://github.com/reactiveui/rfcs/issues/10) which brings the deprecation of `ReactiveList`.  We are removing `ReactiveList` in favor of [Roland Pheasant's Dynamic Data](https://github.com/RolandPheasant/DynamicData).  There have been numerous issues reported around `ReactiveList`, from performance concerns to usability of the feature.  We felt that instead of continuing support our implementation, we should move towards an implementation that is reactive, and built to handle what `ReactiveList` was intended for, and more.  You can read more about [Dynamic Data for ReactiveUI Consumers](https://github.com/RolandPheasant/DynamicData/wiki/Introduction-for-ReactiveUI-users)

There have been a few work streams in the ReactiveUI repository to support this effort.  We took deprecation of `ReactiveList` as an opportunity to incorporate performance benchmarks using [Benchmark DotNet](https://github.com/dotnet/BenchmarkDotNet).  Before changing the internals of ReactiveUI, we felt it best to verify any memory or performance concerns with this change.  Our long term plan is to benchmark as many areas in the framework that make sense so we can look for the best ways to improve performance in the future.


ReactiveUI version 9.0 is coming and with it some improvements.  We aren't done there, what will be the focus of ReactiveUI v10? What do you want to see included? Join us on Slack [ReactiveUI Slack](https://join.slack.com/t/reactivex/shared_invite/zt-lt48skpz-G5WDYOAuzA80_MByZrLT0g), or post an RFC and tell us what would improve your consumer experience!
