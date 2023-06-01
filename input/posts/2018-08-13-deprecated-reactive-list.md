NoTitle: true
IsBlog: true
Title: Deprecating ReactiveList
Tags: Announcement
Author: Rodney Littles, II
Published: 2018-08-13
---

## ReactiveUI is deprecating `ReactiveList`

With the coming 8.6.1 release, `ReactiveList` will be marked as deprecated.  ReactiveList is being replaced by Roland Pheasants [Dynamic Data](https://github.com/RolandPheasant/DynamicData
)

## What does this mean for me?

We hope, nothing.  Other than no longer supporting the public API of ReactiveList, the end user shouldn't experience any change.  There will be some internal clean up of the code base that will be changed over to use Dynamic Data as opposed to Reactive Lists.  There are several reasons why ReactiveUI has made this decision for the platform.  These reasons have been outlined and can be discussed in further detail on the [RFC](https://github.com/reactiveui/rfcs/issues/10) or the [GitHub Issue #1372](https://github.com/reactiveui/ReactiveUI/issues/1372).

## Next steps

The work will begin in the next week to push a package with `ReactiveList` marked as deprecated.  Shortly after that package is released, work will begin to move ReactiveUI towards Dynamic Data.  ReactiveList isn't going away, we are just going to hide it from the public interface and put it in the legacy space.  

Dynamic Data's creator Roland Pheasant has provided an [Introduction for ReactiveUI Users](https://github.com/RolandPheasant/DynamicData/wiki/Introduction-for-ReactiveUI-users) as a starting point for ReactiveUI consumers.

If you're willing to test the pre-release version of this change, or you have concerns that this change will break your app join us over at [ReactiveUI Slack](https://join.slack.com/t/reactivex/shared_invite/zt-lt48skpz-G5WDYOAuzA80_MByZrLT0g), or comment on the RFC or Issue linked above.
