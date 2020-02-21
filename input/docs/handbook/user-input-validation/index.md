ReactiveUI itself offers a few powerful features allowing you to validate user input on fly. With [WhenAnyValue](/docs/handbook/when-any/), you can listen to view model property changes and control [ReactiveCommand](/docs/handbook/commands/) executability. When reactive command's `CanExecute` observable returns false, the control to which you bind that command stays disabled. The simplest validator looks as follows:

```cs
// Declare name validator as IObservable<bool> which emitts a new value when name changes.
var nameValid = this.WhenAnyValue(x => x.Name, name => !string.IsNullOrWhiteSpace(name));

// The reactive command will stay disabled while name is invalid.
var saveName = ReactiveCommand.CreateFromTask(_ => Save(this.Name), canExecute: nameValid);
```

This definitely works for simple scenarios, but for larger forms and more complex validations you definitely need to give [ReactiveUI.Validation](https://github.com/reactiveui/reactiveUI.validation/) package a try.

# ReactiveUI.Validation

This is the primary way we make validations. The package contains validation helpers for ReactiveUI-based solutions, functioning in a reactive way. [ReactiveUI.Validation sources](https://github.com/reactiveui/ReactiveUI.Validation) are available on GitHub. The package supports all platforms, including .NET Framework, .NET Standard, MonoAndroid, Tizen, UAP, Xamarin.iOS, Xamarin.Mac, Xamarin.TVOS.

| Platform          | ReactiveUI Package                  | NuGet                |
| ----------------- | ----------------------------------- | -------------------- |
| Any               | [ReactiveUI.Validation][CoreDoc]    | [![CoreBadge]][Core] |

[Core]: https://www.nuget.org/packages/ReactiveUI.Validation/
[CoreBadge]: https://img.shields.io/nuget/v/ReactiveUI.Validation.svg
[CoreDoc]: https://reactiveui.net/docs/handbook/user-input-validation/

## Getting Started

1. Decorate existing ViewModel with `IValidatableViewModel` (`ISupportsValidation` for version 1.3 and lower), which has a single member, `ValidationContext`. The `ValidationContext` contains all of the functionality surrounding the validation of the view model. Most access to the specification of validation rules is performed through extension methods on the `IValidatableViewModel` interface. Then, add validation to the view model.

```cs
public class SampleViewModel : ReactiveObject, IValidatableViewModel
{    
    // Initialize validation context that manages reactive validations.
    public ValidationContext ValidationContext { get; } = new ValidationContext();
    
    // Declare a separate validator for complex rule.
    public ValidationHelper ComplexRule { get; }
    
    // Declare a separate validator for age rule.
    public ValidationHelper AgeRule { get; }
    
    [Reactive] public int Age { get; set; }

    [Reactive] public string Name { get; set; }
    
    public ReactiveCommand<Unit, Unit> Save { get; }
    
    public SampleViewModel()
    {
        // Name must be at least 3 chars. The selector is the property 
        // name and the line below is a single property validator.
        this.ValidationRule(
            viewModel => viewModel.Name,
            name => !string.IsNullOrWhiteSpace(name),
            "You must specify a valid name");

        // Age must be between 13 and 100, message includes the silly 
        // length being passed in, stored in a property of the ViewModel.
        AgeRule = this.ValidationRule(
            viewModel => viewModel.Age,
            age => age >= 13 && age <= 100,
            age => $"{age} is a silly age");

        var nameAndAgeValid = this
            .WhenAnyValue(x => x.Age, x => x.Name, (age, name) => new { Age = age, Name = name })
            .Select(x => x.Age > 10 && !string.IsNullOrEmpty(x.Name));

        // Create a rule from an IObservable.
        ComplexRule = this.ValidationRule(
            _ => nameAndAgeValid,
            (vm, state) => !state ? "That's a ridiculous name / age combination" : string.Empty);
            
        // IsValid extension method returns true when all validations succeed.
        var canSave = this.IsValid();
        
        // Save command is only active when all validators are valid.
        Save = ReactiveCommand.CreateFromTask(async unit => { }, canSave);
    }
}
```

> **Note** We use the `[Reactive]` attribute from [ReactiveUI.Fody](https://www.nuget.org/packages/ReactiveUI.Fody/) package in the example above. ReactiveUI.Fody attributes is a great replacement for `INotifyPropertyChanged` boilerplate code, see [Boilerplate Code](/docs/handbook/view-models/boilerplate-code) section for more info.  

2. Add validation presentation to our example Android view. Actually, ReactiveUI.Validation operates the same on all platforms ReactiveUI supports, so the principles described below apply to any platform.

```cs
public class MainActivity : ReactiveAppCompatActivity<SampleViewModel>
{
    public EditText nameEdit { get; set; }
    public EditText ageEdit { get; set; }

    public TextView nameValidation { get; set; }
    public TextView ageValidation { get; set; }
    public TextView validationSummary { get; set; }

    public Button myButton { get; set; }
    public TextInputLayout til { get; set; }
    public TextInputEditText tiet { get; set; }

    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);

        // Set our View from the "main" layout resource.
        SetContentView(Resource.Layout.Main);
        WireUpControls();

        this.BindCommand(ViewModel, vm => vm.Save, v => v.myButton);
        this.Bind(ViewModel, vm => vm.Name, v => v.nameEdit.Text);
        this.Bind(ViewModel, vm => vm.Age, v => v.ageEdit.Text);

        // Bind any validations which reference the name property to the text of the nameValidation control.
        this.BindValidation(ViewModel, vm => vm.Name, v => v.nameValidation.Text);

        // Bind the validation specified by the AgeRule to the text of the ageValidation control.
        this.BindValidation(ViewModel, vm => vm.AgeRule, v => v.ageValidation.Text);

        // bind the summary validation text to the validationSummary control.
        this.BindValidation(ViewModel, v => v.validationSummary.Text);

        // bind to an Android TextInputLayout control, utilising the Error property.
        this.BindValidation(ViewModel, vm => vm.ComplexRule, til);
    }
}
```

## INotifyDataErrorInfo Support 

For those platforms which support the `INotifyDataErrorInfo` interface (including WPF, Avalonia, Xamarin.Forms), ReactiveUI.Validation provides [a helper base class](https://github.com/reactiveui/ReactiveUI.Validation/blob/master/src/ReactiveUI.Validation/Helpers/ReactiveValidationObject.cs) named `ReactiveValidationObject<TViewModel>`. The helper class implements both the `IValidatableViewModel` interface and the `INotifyDataErrorInfo` interface. It listens to any changes in the `ValidationContext` and invokes `INotifyDataErrorInfo` events. 

```cs
public class SampleViewModel : ReactiveValidationObject<SampleViewModel>
{
    [Reactive]
    public string Name { get; set; } = string.Empty;

    public SampleViewModel()
    {
        this.ValidationRule(
            x => x.Name, 
            name => !string.IsNullOrWhiteSpace(name),
            "Name shouldn't be empty.");
    }
}
```

# Alternatives

You can also use Xamarin validations as explained in the [David Britch's article](https://devblogs.microsoft.com/xamarin/validation-xamarin-forms-enterprise-apps/). Other great tools for user input validation are [FluentValidation](https://github.com/JeremySkinner/FluentValidation) and [Sprache](https://github.com/sprache/Sprache).
