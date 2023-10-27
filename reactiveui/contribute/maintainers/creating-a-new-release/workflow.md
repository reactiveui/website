---
NoTitle: true
---

## Development

Once pull-requests have been merged into `main` a new release is automatically generated and published to Nuget. There is nothing you need to do as the package identifier and library version is [automatically incremented](semantic-versioning.md) based upon our [GitVersion configuration](https://github.com/reactiveui/ReactiveUI/blob/develop/GitVersion.yml).

## Production

By design, no single person can release a new version of ReactiveUI without approval from [one of the other contributors](https://github.com/orgs/reactiveui/teams/contributors). The process is kicked off by one of the contributors opening up a pull-request from `develop` to `main`

![](~/images/create-a-pull-request-from-develop-to-master.png)

At this stage the contributor that is proposing a new release must perform a few administrative tasks. It's the responsibility of the release approver to verify that these tasks have been performed correctly. Both parties should [jump into the ReactiveUI video chat room](https://appear.in/reactiveui) before proceeding any further.

![](~/images/pull-request-review-required.png)

Edit the `vNext` milestone and change it to the version number of the next release. If this release contains a [breaking change](semantic-versioning.md) then increase the `BREAKING` version by one and reset the `MINOR` back to zero and keep the `PATCH` at zero. Otherwise just bump the `MINOR` version by one and keep the `PATCH` at zero.

![](~/images/click-edit-vnext-milestone-button.png)

If process for [merging individual pull-requests](~/contribute/maintainers/merging-pull-requests/index.md) was followed perfectly then you won't need to do much here but in both parties need to verify that all issues that have been assigned to a milestone:

* Has a prefix which classifies the type of change and clearly explained what was changed because our release notes are automatically generated from this information.
* Have _one_ label assigned \(no, more until GitReleaseManager has been PR'ed to support multiple labels\) to the GitHub issue which classifies the scope of change. GitReleaseManager will fail to automatically generate the release notes and the draft release in GitHub if any issue has no labels. If this happens, resolve this in GitHub and then re-run the merge commit build in AppVeyor.

![](~/images/ensure-all-issues-assigned-to-a-milestone-are-labeled.png)

Once the release has been approved, you'll need to merge your code.

Verify again that the GitHub milestone has been renamed from **vNext** to the appropriate version, as that will be the version of the software release.

When you merge, be sure to include a message that will cause the semver to be bumped appropriately. One of:

* +semver: breaking
* +semver: feature

![](~/images/merge-commit.png)

Merging this pull-request into `main` will kick off a new build on AppVeyor but the build pipeline is configured not to publish packages to NuGet.org unless the release has been tagged.

![](~/images/commits-to-master-do-not-automatically-publish-to-nuget.png)

Once this build completes, a new draft release with automatically generated release notes will be available for you to polish and add any flair as needed.

![](~/images/edit-release-notes.png)

Pressing the publish release button will stamp the repository with a git tag which the release version, overriding any automatic versioning strategies and trigger a new build which will be automatically published to NuGet.org.

![](~/images/stamp-repository-and-publish-release.png)

![](~/images/pull-request-into-master-then-publish-tag-to-release.png)

![](~/images/tagged-releases-automatically-publish-to-nuget.png)

One final step remains, create a new `vNext` milestone.

![](~/images/create-new-vnext-milestone.png)

