---
title: Removing ReactiveCommand abstract base class
category: 
  - Announcement
author: Rodney Littles, II
---

The ReactiveUI team has been urging consumers for some time now to move away from the `ReactiveCommand` abstract class for properties.  There are some slight nuiances with type constraints that can sometimes cause run time bugs.  Because your property can resolve to an abstract base implementation doesn't mean you should define it that way.  We are very adamante about creating a type safe environment where consumers don't have to worry about hidden runtime issues with the framework. [RFC: Remove ReactiveCommand abstract base class](https://github.com/reactiveui/rfcs/issues/19) was raised to address this exact issue.

`ReactiveCommand LoginCommand = ReactiveCommand.Create(ExecuteLogin);` works fine at compile time.  Depending on the type resolution and what your expectation of the `Create` `TParam` and `TReturn` types, this doesn't work as expected.  ReactiveUI bindings can also be affected by types not resolving correctly at run time.  To close the loop on this, and make good on our feedback of "don't do that", we have removed the `ReactiveCommand` abstract base class.  This will enforce our concerns for using `ReactiveCommand<TParam, TResult>` to ensure avoidance of runtime type resolution issues.

This change shouldn't cause any breaking changes to the `ReactiveCommand`, but if you have been using the abstract base class to define properties, moving to the new package, you will have to define the types of your existing commands.  If you previously exteneded the `ReactiveCommand` abstract base class, you should change that to derive from `ReactiveCommandBase<TParam, TResult>` in the future.