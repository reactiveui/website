
Before you submit your pull request, please:

* If you are considering submitting a pull-request that is more than a simple fix, open a discussion on GitHub first with your proposal.
* Search [GitHub](https://github.com/reactiveui/reactiveui/pulls) for an open or closed Pull Request that relates to your submission. 

Make your changes in a new git branch:

```shell
git checkout -b my-fix-branch master
```

* Follow our [Coding Style](code-style.md).
* Create your patch, **including appropriate test cases and documentation**.
* Run the test suite.
* Commit your changes using a descriptive commit message that follows our [commit message conventions](commit-message-convention.md).

```shell
git commit -a
```

Note: the optional commit `-a` command line option will automatically "add" and "rm" edited files.

* Build your changes locally to ensure all the tests pass.

* Push your branch to GitHub:

```shell
git push origin my-fix-branch
```

In GitHub, send a pull request to `reactiveui:develop`.

If we suggest changes, then:

* Make the required updates.
* Re-run the test suite to ensure tests are still passing.
* Commit your changes to your branch (e.g. `my-fix-branch`).
* Push the changes to your GitHub repository (this will update your Pull Request).

Finally, nerd snipe the appropriate team to review the pull-request.

![](/en/images/contributing/nerd-snipe-the-appropriate-review-team.png)

That's it! Thank you for your contribution!