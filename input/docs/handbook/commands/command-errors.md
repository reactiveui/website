## Command Errors

If the logic you provide to a `ReactiveCommand` can fail in expected ways, you need a means of dealing with those failures.

For command execution, the pipeline you get back from `Execute` will tick any errors that occur in your execution logic. However, the subscription to this observable is often instigated by the binding infrastructure. As such, it's likely that you cannot even get a hold of the observable to observe any errors.

To address this dilemma, `ReactiveCommand` includes a `ThrownExceptions` observable (of type `IObservable<Exception>`). Any errors that occur in your execution logic will *also* tick through this observable. If you haven't subscribed to it, ReactiveUI will bring down your application. This forces you towards a pit of error-handling success.

It can be tempting to *always* add a subscription to `ThrownExceptions`, even if the only recourse is to just log the problem. However, it is advisable to treat this like any other exception handling and only handle problems you can redress. If, for example, your command merely updates a property in your view model and it should never fail, any subscription to `ThrownExceptions` will serve only to obscure implementation problems. That said, be aware of the potential for intermittent problems, such as network and I/O errors. As always, a strong suite of tests will help you identify where a subscription to `ThrownExceptions` makes sense.

> **Note** Your `canExecute` pipeline also has the potential to produce an error. Such cases are almost certainly a programmer error because you never want your `canExecute` pipeline to end in error. Even so, these errors will also tick through `ThrownExceptions`.
