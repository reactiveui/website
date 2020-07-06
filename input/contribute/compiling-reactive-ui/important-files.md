
# appveyor.yml
* https://github.com/reactiveui/ReactiveUI/blob/develop/appveyor.yml
* Schema is at https://www.appveyor.com/docs/appveyor-yml/
* Different configuration for `main` and `develop` branches - https://www.appveyor.com/docs/branches/
* Secrets are encrypted and are not available for pull-request builds - https://www.appveyor.com/docs/build-configuration/#secure-variables
* When `develop` branch builds are pushed to MyGet, when `main` builds goto NuGet once they have been tagged.

# directory.build.props
* https://github.com/reactiveui/ReactiveUI/blob/develop/src/Directory.build.props
* Used to define common properties (Copyright, PackageLicenseUrl, PackageTags) used in packaging projects.

# directory.build.targets

* https://github.com/reactiveui/ReactiveUI/blob/develop/src/Directory.build.targets
* Used to define common properties used in projects (compiler symbols)

# build.cmd
* https://github.com/reactiveui/ReactiveUI/blob/develop/build.cmd
* Updates nuget, installs cake and xunit
* Executes build.cake
* Invoked via appveyor.yml - https://github.com/reactiveui/ReactiveUI/blob/72b4921d0b60d55b795474c2f7a03918a85fb150/appveyor.yml#L24

# build.cake
* https://github.com/reactiveui/ReactiveUI/blob/develop/build.cake
* Tightly coupled to AppVeyor & GitHub as it does more than just build - full release pipeline - see https://reactiveui.net/contribute/maintainers/creating-a-new-release/
* Don't use if you want to run your own pipeline - see https://reactiveui.net/docs/getting-started/compiling/setting-up-ci for manual instructions.

# editor.config
* http://kent-boogaart.com/blog/editorconfig-reference-for-c-developers

# eventgenerator.binlog
* https://reactiveui.net/docs/getting-started/compiling/troubleshooting#binary-logging

# gitreleasemanager.yml
* This is how our release notes are automatically generated (look at build.cake)
* See https://github.com/GitTools/GitReleaseManager
* See https://reactiveui.net/contribute/maintainers/creating-a-new-release/workflow
* Upstream defects -> https://github.com/GitTools/GitReleaseManager/issues/90

# gitversion.yml
* This is how we automatically version our software (look at build.cake)
* See https://github.com/GitTools/GitVersion
* See https://reactiveui.net/contribute/maintainers/creating-a-new-release/semantic-versioning

# reactiveui.binlog
* https://reactiveui.net/docs/getting-started/compiling/troubleshooting#binary-logging

# signpackages.ps1
Identity signs our NuGet packages, it means consumers can validate that they are using official packages.

# clean-merged-branches

# issue_template.md

# pull_request_template.md

# code_of_conduct.md
* https://reactiveui.net/contribute/maintainers/accountability-and-expectations/
* https://reactiveui.net/code-of-conduct

# license

* https://reactiveui.net/license
