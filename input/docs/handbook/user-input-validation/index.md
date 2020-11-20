ReactiveUI itself offers a few powerful features allowing you to validate user input on fly. With [WhenAnyValue](/docs/handbook/when-any/), you can listen to view model property changes and control [ReactiveCommand](/docs/handbook/commands/) executability. When reactive command's `CanExecute` observable returns false, the control to which you bind that command stays disabled. The simplest validator looks as follows:

```cs
// Declare name validator as IObservable<bool> which emitts a new value when name changes.
var nameValid = this.WhenAnyValue(x => x.Name, name => !string.IsNullOrWhiteSpace(name));

// The reactive command will stay disabled while name is invalid.
var saveName = ReactiveCommand.CreateFromTask(_ => Save(this.Name), canExecute: nameValid);
```

This definitely works for simple scenarios, but for larger forms and more complex validations you definitely need to give [ReactiveUI.Validation](https://github.com/reactiveui/reactiveUI.validation/) package a try.

# ReactiveUI.Validation

This is the primary way we make validations. The package contains validation helpers for ReactiveUI-based solutions, functioning in a reactive way. [ReactiveUI.Validation sources](https://github.com/reactiveui/ReactiveUI.Validation) are available on GitHub. The package supports all platforms, including .NET Framework, .NET Standard, MonoAndroid, Tizen, UAP, Xamarin.iOS, Xamarin.Mac, Xamarin.TVOS. Install the following package into you class library and into a platform-specific project.

| Platform           | ReactiveUI Package                               | NuGet                |
| ------------------ | ------------------------------------------------ | -------------------- |
| Any Platform       | [ReactiveUI.Validation][CoreDoc]                 | [![CoreBadge]][Core] |
| AndroidX (Xamarin) | [ReactiveUI.Validation.AndroidX][DroDoc]         | [![DroXBadge]][DroX] |
| Xamarin.Android    | [ReactiveUI.Validation.AndroidSupport][DroDoc]   | [![DroBadge]][Dro]   |

[Core]: https://www.nuget.org/packages/ReactiveUI.Validation/
[CoreBadge]: https://img.shields.io/nuget/v/ReactiveUI.Validation.svg
[CoreDoc]: https://reactiveui.net/docs/handbook/user-input-validation/

[Dro]: https://www.nuget.org/packages/ReactiveUI.Validation.AndroidSupport/
[DroBadge]: https://img.shields.io/nuget/v/ReactiveUI.Validation.AndroidSupport.svg
[DroDoc]: https://github.com/reactiveui/reactiveui.validation#example-with-android-extensions
[DroX]: https://www.nuget.org/packages/ReactiveUI.Validation.AndroidX/
[DroXBadge]: https://img.shields.io/nuget/v/ReactiveUI.Validation.AndroidX.svg

## Getting Started

1. Decorate existing ViewModel with `IValidatableViewModel`, which has a single member, `ValidationContext`. The ValidationContext contains all of the functionality surrounding the validation of the ViewModel. Most access to the specification of validation rules is performed through extension methods on the `IValidatableViewModel` interface. Then, add validation to the ViewModel.

```csharp
public class SampleViewModel : ReactiveObject, IValidatableViewModel
{
    public SampleViewModel()
    {
        // Creates the validation for the Name property.
        this.ValidationRule(
            viewModel => viewModel.Name,
            name => !string.IsNullOrWhiteSpace(name),
            "You must specify a valid name");
    }

    public ValidationContext ValidationContext { get; } = new ValidationContext();

    private string _name;
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
}
```

For more complex validation scenarios there are several more overloads of the `ValidationRule` extension method that accept observables. These allow validation to occur asynchronously, and allows complex chains of observables to be combined to produce validation results. The simplest accepts an `IObservable<bool>` where the observed boolean indicates whether the `ValidationRule` is valid or not. The overload accepts a message which is used when the observable produces a `false` (_invalid_) result.

```csharp
IObservable<bool> passwordsObservable =
    this.WhenAnyValue(
        x => x.Password,
        x => x.ConfirmPassword,
        (password, confirmation) => password == confirmation);

this.ValidationRule(
    vm => vm.ConfirmPassword,
    passwordsObservable,
    "Passwords must match.");
```

Any existing observables can be used to drive a `ValidationRule` using the extension method overload that accepts an arbitrary `IObservable<TState>` streams of events. The overload accepts a custom validation function that is supplied with the latest `TState`, and a custom error message function, responsible for formatting the latest `TState` object. The syntax for this looks as follows:

```csharp
// IObservable<{ Password, Confirmation }>
var passwordsObservable =
    this.WhenAnyValue(
        x => x.Password,
        x => x.ConfirmPassword,
        (password, confirmation) =>
            new { Password = password, Confirmation = confirmation });

this.ValidationRule(
    vm => vm.ConfirmPassword,
    passwordsObservable,
    state => state.Password == state.Confirmation,
    state => $"Passwords must match: {state.Password} != {state.Confirmation}");
```
> **Note** The function to extract a message (`messageFunc`) is only invoked if the function to establish validity (`isValidFunc`) returns `false`, otherwise the message is set to `string.Empty`.

Finally, you can directly supply an observable that streams any object (or struct) that implements `IValidationState`; or you can use the `ValidationState` base class which already implements the interface.  As the resulting object is stored directly against the context without further transformation, this can be the most performant approach:
```csharp
IObservable<IValidationState> usernameNotEmpty =
    this.WhenAnyValue(x => x.UserName)
        .Select(name => string.IsNullOrEmpty(name) 
            ? new ValidationState(false, "The username must not be empty")
            : ValidationState.Valid);

this.ValidationRule(vm => vm.UserName, usernameNotEmpty);
```

> **Note** As a valid `ValidationState` does not really require a message, there is a singleton `ValidationState.Valid` property that you are encouraged to use to indicate a valid state whenever possible, to reduce memory allocations.

2. Add validation presentation to the View.

```csharp
public class SampleView : ReactiveContentPage<SampleViewModel>
{
    public SampleView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel, vm => vm.Name, view => view.Name.Text)
                .DisposeWith(disposables);

            // Bind any validations that reference the Name property 
            // to the text of the NameError UI control.
            this.BindValidation(ViewModel, vm => vm.Name, view => view.NameError.Text)
                .DisposeWith(disposables);

            // Bind any validations attached to this particular view model
            // to the text of the FormErrors UI control.
            this.BindValidation(ViewModel, view => view.FormErrors.Text)
                .DisposeWith(disposables);
        });
    }
}
```

> **Note** `Name` is an `<Entry />`, `NameError` is a `<Label />`, and `FormErrors` is a `<Label />` as well. All these controls are from the [Xamarin.Forms](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/) library.

## Extended ReactiveUI.Validation Usage

[ReactiveUI.Validation](https://github.com/reactiveui/ReactiveUI.Validation/) also supports `INotifyDataErrorInfo` validations used by XAML platforms, including WPF and Avalonia; binding to `TextInputLayout` controls from Xamarin.Android and AndroidX, custom formatters that could be used for localization. Head over to [ReactiveUI.Validation GitHub page](https://github.com/reactiveui/ReactiveUI.Validation/) to learn more about extended usage of this ReactiveUI library:

- [Example with Android Extensions](https://github.com/reactiveui/ReactiveUI.Validation/#example-with-android-extensions)
- [INotifyDataErrorInfo Support](https://github.com/reactiveui/ReactiveUI.Validation/#inotifydataerrorinfo-support)
- [Custom Formatters](https://github.com/reactiveui/ReactiveUI.Validation/#custom-formatters)

# Alternatives

You can also use Xamarin validations as explained in the [David Britch's article](https://devblogs.microsoft.com/xamarin/validation-xamarin-forms-enterprise-apps/). Other great tools for user input validation are [FluentValidation](https://github.com/JeremySkinner/FluentValidation) and [Sprache](https://github.com/sprache/Sprache).
