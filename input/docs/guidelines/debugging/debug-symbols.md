# Debugging Symbols

We use [GitLink](https://github.com/Gittools/GitLink) instead of shipping symbols so that you don't have to specify custom symbol servers (such as symbolsource.org).

![We use GitLink](/images/getting-started/git-link.gif)

The only requirement is to ensure the check the `Enable source server support` option in Visual Studio as shown below:

![Configure Visual Studio as follows](/contribute/maintainers/gitlink-visualstudio-enablesourceserversupport.png)

Refer to [this guide if you experience problems](https://github.com/GitTools/GitLink#troubleshooting).