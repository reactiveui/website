Sometimes view model code needs to request a confirmation from the user. For example, before deleting a file or after an error occurs.

Displaying an interactive dialog from the view model in this case may seem quick and easy, but it ties the view model to a particular UI framework and makes the application harder or impossible to test.

Interactions are ReactiveUI's solution to the problem of suspending the view model's execution path until the user has provided some input.

## API Overview

The `Interaction<TInput, TOutput>` class is the foundation of the interaction infrastructure. It glues together collaborating components of the interaction, coordinates interactions, and distributes them to handlers.

Interactions accept an input and produce an output. Views can use the input to handle the interaction, and the view model can process the resulting output. For example, a view model may need to ask the user for confirmation before deleting a file. Using an interaction, the view model can pass the file path as input, and get back a boolean as output indicating whether the user has confirmed that the file can be deleted.

The input and output types of an `Interaction<TInput, TOutput>` are generic type parameters and therefore under the control of the programmer; there are no restrictions as to what you can use as the input or output type.

> **Note:** Some interactions do not require any input. Use `Unit` in that case. `Unit` can also be used as the output type, but this implies that the view model is merely informing the view that something is about to happen; the interaction's output is ignored in the view model.

Interaction handlers receive an `InteractionContext<TInput, TOutput>`. The `Input` property of the interaction context exposes the interaction's input. Handlers can supply an output for the interaction using the `SetOutput` method of the interaction context. 

Here's a typical arrangement of interaction components:

* The **view model** needs to ask a question such as "Is it OK to delete this file?" to the user.
* The **view** asks the user the question, and provides the answer during the interaction.

This scenario is not mandatory, but it is the most common. For example, the view could answer the question on its own without any user intervention. Or the two components could both be view models. ReactiveUI's interactions impose no restrictions on collaborating components.

Assuming the most common scenario, however, a view model must expose an instance of `Interaction<TInput, TOutput>`. The corresponding view then registers a handler by calling one of the `RegisterHandler` methods on the interaction. To start the interaction, the view model calls the `Handle` method on the interaction and passes in a parameter of type `TInput`. When the asynchronous call finally returns, the view model receives a result of type `TOutput`.

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

An `Interaction<TInput, TOutput>` can be shared across multiple components in an application. Error recovery is a common example of this. Many components may raise errors, but you may want to have a single handler. Let's see an example:

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

> **Note:** For the sake of clarity, the example above mixes TPL and Rx code. Production code would normally use just one or the other.

> **Warning:** The observable returned by `Handle` is cold. You must subscribe to it or handlers will never be invoked.

<!-- FIXME: Is this warning clear? Are we subscribing to Handle's observable in the example? -->

## Handler Precedence

`Interaction<TInput, TOutput>` implements a handler chain. Any number of handlers can be registered in the handler chain, and the handler registered last alway has higher precedence than the previous handler. When the `Handle` method is called on an interaction, each handler in the handler chain is given the chance to handle that interaction (normally, by setting an output). However, handlers need not handle the current interaction. Handlers in the handler chain keep being invoked in order of precedence until one of them finally handles the interaction.

<!-- TODO: What happens if no handler handles the interaction? -->

> **Note:** The `Interaction<TInput, TOutput>` class is designed to be extensible. Subclasses can override the default behavior of the `Handle` method. For example, you could write an implementation that tries only the first handler in the handler chain.

The order of precedence in the handler chain makes it possible to define a default handler that can be temporarily overridden. For example, a handler with the lowest priority in the handler chain may provide default error recovery by displaying an interactive dialog to the user. However, a view may be able to recover from an error without user intervention. The view could then register a new handler when it gets activated, and dispose it when it gets deactivated again. The handler registered by the view now has higher priority than the default handler and will get a chance to handle the interaction before the default handler. This approach requires a shared `Interaction<TInput, TOutput>` instance.

## Unhandled Interactions

An interaction is considered to be unhandled if it has no handlers or none of them set a result. In this case, invoking `Handle` will throw an `UnhandledInteractionException<TInput, TOutput>`. This exception has `Interaction` and `Input` properties that provide further details on the error.

## Testing

Interaction logic in view models can be tested by registering a handler for the interaction:

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

If the test exercises a shared interaction, consider disposing the handler registration:

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
