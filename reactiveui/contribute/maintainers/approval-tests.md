---
NoTitle: true
---
## Approval Tests
Approval tests are run to make sure that changes to the public API surface are known about.
Currently, this covers the Blend, Forms, Testing and .Net 462 and .Net 6 API surfaces.
These are included in `ReactiveUI.Tests\API\ApiApprovalTests.cs`.

When run, a file called `ApiApprovalTests.{TestName}.received.txt` is generated and compared to the "approved" file 
(`ApiApprovalTests.{TestName}.approved.txt`). If a change is found the test will fail.

If the change is desired, replace the old "approved" file with the new "received" file and commit this and the test should pass. This
