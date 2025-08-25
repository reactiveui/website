# Troubleshooting a failed release

## Version wasn't bumped when merging from develop into main

You'll need to do a pull-request similar to this [https://github.com/reactiveui/ReactiveUI/pull/1226](https://github.com/reactiveui/ReactiveUI/pull/1226)

![](/contribute/maintainers/release-failed-because-gitreleasemanager-could-not-find-the-milestone.png)

Change to a clean copy of `main`

```shell
git clean -fdx
git reset --hard
git checkout main
git pull
```

Create a new branch

```shell
git branch bump-release-version
```

If you want to bump the `BREAKING` create an empty commit in the branch with the following commit message

```
git commit --allow-empty -m "fix: the previous commit didn't bump with +semver: breaking"
```

If you want to bump the `FEATURE` create an empty commit in the branch with the following commit message

```
git commit --allow-empty -m "fix: the previous commit didn't bump with +semver: feature"
```

Push your branch

```shell
git push origin bump-release-version
```

Open a pull-request to `main` and once the release has been approved, you'll need to switch your merge mode to `Create a merge commit` aka `Merge pull request` by using the little arrow on the right hand side.

![](/contribute/maintainers/merge-commit-option.png)

Do not customize the merge commit message or more specifically, do not bump the semver in the merge commit message.

## Release failed because of labeling issue

Visit the issue, resolve the problem and then visit AppVeyor and click "Rebuild Commit". It is safe to do this multiple times because building `main` does not automatically release unless the commit has been tagged.

![](/contribute/maintainers/release-failed-because-of-labeling-issue.png)

