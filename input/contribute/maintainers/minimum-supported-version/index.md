Be extremely conversative about bumping the minimum supported version. Please ensure that you aren't breaking the ecosystem, there's a migration path and no rift is created (python 2 vs python 3) that people can fall into.

With that said however this is an open-source project and typically folks who are stuck on older versions work at enterprise companies. Maintainers have zero obligations to requests from this demographic - our software is made available in binary and source form on a AS-IS basis. Often we will accomodate such requests to keep a platform around longer if the request comes from a maintainer or a respected community member who is actively engaged helping make ReactiveUI better.

The two rules of thumb maintainers should use are as follows: 

1) What is the min version of a platform shipped by Visual Studio for Windows?
2) When a new project is created by the File -> New project wizard what version is in the csproj?


