# Commands

Commands encapsulate logic to be executed in response to some user action. Examples of actions are clicking a **Save** menu item, tapping a phone icon, or stretching an image. Respectively, the associated logic might be to save outstanding changes, instigate a phone call, or zoom into the image.

Commands may or may not be executable in a given situation. For example, the command backing the **Save** menu item might be unavailable if there are no unsaved changes.

In MVVM, the view model does not concern itself with *how* a user executes a particular command - that decision is left to the view. But typically it's best if the execution logic resides in the view model so that changes to the view model's state can be encapsulated. Commands facilitate this separation between the *how* and the *what*.

If you've done any UI development in .NET, you're likely familiar with [the `ICommand` interface](https://msdn.microsoft.com/en-us/library/system.windows.input.icommand.aspx). It is an abstraction that can be used for exactly the purposes described. Its `CanExecute` method can be used to determine whether a command can be executed. If so, one can then invoke the `Execute` method. The `CanExecuteChanged` event can be used by interested parties to know when the command's executability has changed. For example, a `Button` bound to a particular `ICommand` would want to update its enabled state based on the executability of the command.

But the `ICommand` interface isn't an ideal abstraction. It fails to elegantly accommodate long-running commands, such as those that perform I/O. Moreover, its interface is imperative, not reactive. This makes it far less amenable to a reactive code base.
