Approval tests are run to make sure that changes to the public API surface are known about.
Currently, this covers the Blend, Forms, Testing and .Net 45 API surfaces.

These are included in `ReactiveUI.Tests\API\APIApprovals.cs`.

When run, a file called `APIApprovals.{TestName}.received.txt` is generated and compared to the "approved" file 
(`APIApprovals.{TestName}.approved.txt`). If a change is found the test will fail.

If the change is desired, replace the old "approved" file with the new "received" file and commit this and the test should pass.
