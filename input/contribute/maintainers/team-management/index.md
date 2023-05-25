## Team Management

When a pull-request is created GitHub evaulates which files have been changed [as per these rules](https://github.com/reactiveui/ReactiveUI/blob/main/.github/CODEOWNERS
). For more information about the `CODEOWNERS` convention [refer to this page on GitHub](https://help.github.com/articles/about-codeowners/) or refer to the sample below

```
# A CODEOWNERS file uses a pattern that follows the same rules used in gitignore files.
# The pattern is followed by one or more GitHub usernames or team names using the
# standard @username or @org/team-name format. You can also refer to a user by an
# email address that has been added to their GitHub account, for example user@example.com

.github/*                                  @reactiveui/maintainers
```

ReactiveUI heavily uses GitHub teams to split the workload into approachable chunks. If someone wishes to help with reviewing pull-requests then ask which areas they would be comfortable in helping out with and add them to the appropriate team. 

When adding new teams please add the team as a sub-team of the [maintainers team](https://github.com/orgs/reactiveui/teams/maintainers) otherwise permissions won't flow down correctly. For more information see stream at 1h mark at https://twitter.com/GeoffreyHuntley/status/1026413749819691009

Unfortunately people outside of the organization cannot view teams until they are part of a team. This is a GitHub limitation and we can't fix it. The solution is to add them to a team, nuture the relationship and help them go through the motions of becoming a regular contributor or maintainer.

