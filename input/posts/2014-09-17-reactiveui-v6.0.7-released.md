NoTitle: true
IsBlog: true
Title: ReactiveUI v6.0.7 released
Tags: Release Notes
Author: Anaïs Betts
Published: 2014-09-17
---

## [What's New](https://github.com/reactiveui/ReactiveUI/compare/6.0.6...6.0.7)

## Android Scheduler improvements

On Android, an improved Scheduler based on RxJava is now automatically configured in your application, that no longer requires setup in OnCreate. Using the main thread scheduler should now work even in contexts such as bound services or other non-Activity scenarios. (#717). 

This PR also fixes an issue where scheduling from non-UI threads in Xamarin Forms-based apps could incorrectly throw an exception. 

## Documentation Galore

This release marks the first release whose source code contains our first steps toward comprehensive documentation. This has been a multiple-month effort from several people, such as @rikbosch, @niik, @dchaib, and @npnelson. Documentation will now be stored [in the docs folder](https://github.com/reactiveui/ReactiveUI/tree/main/docs) and will be kept up-to-date as components change.

Right now while many of the docs are stubs, the "basics" folder is completely written, it's highly recommended to check out the documents in this folder for more information.

## WinForms Improvements

ReactiveUI now supports controls which derive from Component, such as ToolStripButton. Thanks to @vanderkleij for the patches (#720, #721)

## Other
- Add a Reactive class for UITabBarController (#723, thanks @tberman)
- Allow Commands to be used with Menu Items (#722, thanks @mteper)
- IsExecuting is now false as soon as the result from ExecuteAsync returns if awaited. (#714, thanks @Haacked)
