# Common Patterns

This example from UserError also illustrates the canonical usage of
CreateAsyncTask:

```cs

// When LoadTweetsCommand is invoked, LoadTweets will be run in the
// background, the result will be Observed on the Main thread, and
// ToProperty will then store it in an Output Property
LoadTweetsCommand = ReactiveCommand.CreateAsyncTask(() => LoadTweets())

LoadTweetsCommand.ToProperty(this, x => x.TheTweets, out theTweets);

var errorMessage = "The Tweets could not be loaded";
var errorResolution = "Check your Internet connection";

// Any exceptions thrown by LoadTweets will end up being
// sent through ThrownExceptions
LoadTweetsCommand.ThrownExceptions
    .Select(ex => new UserError(errorMessage, errorResolution))
    .Subscribe(x => UserError.Throw(x));
```
