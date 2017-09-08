---
title: The public API surface of ReactiveUI is now protected by API approval tests
category: Announcement
author: Geoffrey Huntley
---

When reviewing pull requests the maintainers used to manually eyeball the changes proposed in a pull-request to determine if there was a change to the public API surface. We now have new tests, one per platform that trip when the public API surface changes for that platform.

![](https://cloud.githubusercontent.com/assets/127353/22617686/3a113e4c-eb1f-11e6-9416-19c39085ef61.png)

These tests will provide contributors to ReactiveUI instant feedback (by failing) [if the public API surface changes](https://github.com/reactiveui/ReactiveUI/pull/1463). Right now only Blend, WPF, Winforms, NET452, Testing are covered but in the future, once our CI practices evolve this will grow to include iOS, Mac, UWP and Android. One of the super-cool things about doing this is it provides a versioned changelog of when [the public contracts change](https://github.com/Particular/NServiceBus/blame/develop/src/NServiceBus.Core.Tests/API/APIApprovals.ApproveNServiceBus.approved.txt).

When the tests run, a file called `ApiApprovalTests.{TestName}.received.txt` is generated and compared to the "approved" file (`ApiApprovalTests.{TestName}.approved.txt`). If a change is found the test will fail. If the change is desired, replace the old "approved" file with the new "received" file and commit this and the test will pass. Pretty cool huh?
