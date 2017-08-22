Each time a commit is merged into the develop branch a continious integration job is kicked off and if tests pass a new set of packages is pushed to the ReactiveUI MyGet.org feed. You can use this feed to access hotfixes before they are released and to help the maintainers with QA duties.


1. Visit https://www.visualstudio.com/en-us/docs/package/nuget/consume
2. Configure in the following address `https://www.myget.org/F/reactiveui/api/v3/index.json`
