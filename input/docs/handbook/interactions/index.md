Sometimes view model code needs to request a confirmation from the user. For example, before deleting a file or after an error occurs.

Displaying an interactive dialog from the view model is an easy solution, but it ties the view model to a particular UI framework and makes the application harder or impossible to test.

Interactions are ReactiveUI's solution to the problem of suspending the view model's execution path until the user has provided some input.

## API Overview

The `Interaction<TInput, TOutput>` class is the foundation of the interaction infrastructure. It glues together collaborating components of the interaction, and coordinates and distributes interactions to handlers.

Interactions accept an input and produce an output. Views can use the input when handling the interaction. The view model gets back the output from the interaction. For example, a view model may need to ask the user for confirmation before deleting a file. Using an interaction, it could pass the file path as the input, and get back a boolean as output indicating whether the file can be deleted.

The input and output types of an `Interaction<TInput, TOutput>` are generic type parameters and therefore under the control of the programmer; there aren't any restrictions as to what you can use as input or output types.

> **Note** There may be times you don't particularly care about the input type. In such cases, you can just use `Unit`. You can also use `Unit` as your output type, though this implies that your view model is not using the interaction to make a decision. Instead, it is merely informing the view that something is about to happen.

Interaction handlers receive an `InteractionContext<TInput, TOutput>`. The interaction context exposes the input for the interaction via the `Input` property. In addition, it provides a means for handlers to supply the interaction's output by calling the `SetOutput` method.

A typical arrangement of interaction components - one that has been assumed until now - is:

* **View Model**: wants to know the answer to a question, such as "is it OK to delete this file?"
* **View**: asks the user the question, and supplies the answer during the interaction

Whilst this configuration is the most common, it is by no means required. You could, for example, have the view answer the question itself without user intervention. Or perhaps both components are view models. The interactions infrastructure provided by ReactiveUI does not place any restrictions on collaborating components.

Assuming the common configuration, a view model would create and expose an instance of `Interaction<TInput, TOutput>`. The corresponding view would register a handler against this interaction by calling one of the `RegisterHandler` methods on it. To instigate an interaction, the view model would pass in an instance of `TInput` to the `Handle` method. It would then asynchronously receive a result of type `TOutput`.

## An Example

```cs
public class ViewModel : ReactiveObject
{
    private readonly Interaction<string, bool> confirm;
    
    public ViewModel()
    {
        this.confirm = new Interaction<string, bool>();
    }
    
    public Interaction<string, bool> Confirm => this.confirm;
    
    public async Task DeleteFileAsync()
    {
        var fileName = ...;
        
        // this will throw an exception if nothing handles the interaction
        var delete = await this.confirm.Handle(fileName);
        
        if (delete)
        {
            // delete the file
        }
    }
}

public class View
{
    public View()
    {
        this.WhenActivated(
            d =>
            {
                d(this
                    .ViewModel
                    .Confirm
                    .RegisterHandler(
                        async interaction =>
                        {
                            var deleteIt = await this.DisplayAlert(
                                "Confirm Delete",
                                $"Are you sure you want to delete '{interaction.Input}'?",
                                "YES",
                                "NO");
                                
                            interaction.SetOutput(deleteIt);
                        }));
            });
    }
}
```

You can also create an `Interaction<TInput, TOutput>` that is shared across multiple components in your application. A common example of this is in error recovery. Many components may want to raise errors, but we may want only one common handler. Here's an example of how you can achieve this:

```cs
public enum ErrorRecoveryOption
{
    Retry,
    Abort
}

public static class Interactions
{
    public static readonly Interaction<Exception, ErrorRecoveryOption> Errors = new Interaction<Exception, ErrorRecoveryOption>();
}

public class SomeViewModel : ReactiveObject
{
    public async Task SomeMethodAsync()
    {
        while (true)
        {
            Exception failure = null;
            
            try
            {
                DoSomethingThatMightFail();
            }
            catch (Exception ex)
            {
                failure = ex;
            }
            
            if (failure == null)
            {
                break;
            }
            
            // this will throw if nothing handles the interaction
            var recovery = await Interactions.Errors.Handle(failure);
            
            if (recovery == ErrorRecoveryOption.Abort)
            {
                break;
            }
        }
    }
}

public class RootView
{
    public RootView()
    {
        Interactions.Errors.RegisterHandler(
            async interaction =>
            {
                var action = await this.DisplayAlert(
                    "Error",
                    "Something bad has happened. What do you want to do?",
                    "RETRY",
                    "ABORT");

                interaction.SetOutput(action ? ErrorRecoveryOption.Retry : ErrorRecoveryOption.Abort);
            });
    }
}
```

> **Note** For the sake of clarity, the example code here mixes TPL and Rx code. Production code would normally stick with one or the other.

> **Warning** The observable returned by `Handle` is cold. You must subscribe to it for handlers to be invoked.

## Handler Precedence

`Interaction<TInput, TOutput>` implements a handler chain. Any number of handlers can be registered, and later registrations are deemed of higher priority than earlier registrations. When an interaction is instigated with the `Handle` method, each handler is given the _opportunity_ to handle that interaction (i.e. set an output). The handler is under no obligation to actually handle the interaction. If a handler chooses _not_ to set an output, the next handler in the chain is invoked.

> **Note** The `Interaction<TInput, TOutput>` class is designed to be extensible. Subclasses can change the behavior of `Handle` such that it does not exhibit the behavior described above. For example, you could write an implementation that tries only the first handler in the list.

This chain of precedence makes it possible to define a default handler, and then temporarily override that handler. For example, a root level handler may provide default error recovery behavior. But a specific view in the application may know how to recover from a certain error without prompting the user. It could register a handler whilst it's activated, then dispose of that registration when it deactivates. Obviously such an approach requires a shared interaction instance.

## Unhandled Interactions

An interaction is considered to be unhandled if it has no handlers or none of them set a result. In this case, invoking `Handle` will throw an `UnhandledInteractionException<TInput, TOutput>`. This exception has an `Interaction` and an `Input` property that provide further details on the error.

## Testing

You can easily test interaction logic in view models by registering a handler for the interaction:

```cs
[Fact]
public async Task interaction_test()
{
    var fixture = new ViewModel();
    fixture
        .Confirm
        .RegisterHandler(interaction => interaction.SetOutput(true));
        
    await fixture.DeleteFileAsync();
    
    Assert.True(/* file was deleted */);
}
```

If your test is hooking into a shared interaction, you probably want to dispose of the registration before your test returns:

```cs
[Fact]
public async Task interaction_test()
{
    var fixture = new SomeViewModel();
    
    using (Interactions.Error.RegisterHandler(interaction => interaction.SetOutput(ErrorRecoveryOption.Abort)))
    {
        fixture.SomeMethodAsync();
        
        // assert abort here
    }
}
```
