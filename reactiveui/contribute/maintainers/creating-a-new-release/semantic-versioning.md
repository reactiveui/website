# Semantic Versioning

Semantic versioning is all about releases, our continuous integration infrastructure uses [GitVersion](https://gitversion.readthedocs.io) to automatically version our releases [as per the configuration](https://github.com/reactiveui/ReactiveUI/blob/develop/GitVersion.yml).

For maintainer sanity, we version ReactiveUI and package as a pinned group - all packages in a release will always be the same version and only work with that version which makes it impossible for a consumer to run into situations where they use `reactiveui-core` at `7.1.0` but `reactiveui-xamforms` at `7.0.0`. Additionally all assemblies share the same [CommonAssemblyInfo.cs](https://github.com/reactiveui/ReactiveUI/blob/develop/src/CommonAssemblyInfo.cs) which is updated just before compile time by the build infrastructure.

We have three different workflows which control how ReactiveUI is versioned.

## Development Builds

![Development suffix](/semver-develop.png)

Builds from the `develop` branch have a suffix of `alpha` so that they are sorted higher than release builds which provides the team the ability to manually publish development builds to NuGet as pre-releases if they so desire.

GitVersion is configured in [Continuous Deployment mode](https://gitversion.readthedocs.io/en/latest/reference/continuous-deployment/) which automatically increments the version for you.

## Pull Request Builds

![Pull-request suffix](/semver-pull-request-into-develop.png)

Builds from pull-requests have a suffix of `pullrequest$GitHubIssueNumber` and are not automatically published to NuGet or MyGet but the packages are available for download from AppVeyor which allows the team to test the unit of change without merging into `develop`.

## Release Builds

![Release has no suffix](/semver-master.png)

Builds from the `main` branch do not have a suffix and GitVersion is configured in [ContinuousDelivery mode](https://gitversion.readthedocs.io/en/latest/reference/continuous-delivery). If the current commit is tagged, the version in the tag overrides the automatic versioning strategies.

![Building a tagged release](/building-a-tagged-release.png)

## Versioning

```BREAKING.MINOR.PATCH\[-ALPHA-BUILDNUMBER\]```

BREAKING when:

* remove public API surface area
* drop support for a platform
* adopt a newer MAJOR version of an existing dependency 
* disable a compatibility quirk off by default

MINOR when:

* add public API surface area 
* add new behavior
* adopt a newer version of an existing dependency
* introduce a new dependency 
* any other change \(not otherwise captured\)

PATCH when:

* never, unless you _really_ stuff something up.
