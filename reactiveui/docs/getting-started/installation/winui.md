---
Title: WinUI
---

## Package Installation

If you want to get started with ReactiveUI for WinUI, you will need to install some packages and follow some steps. 

#### Here is a brief introduction:

#### Install packages: 

You will need to install the ReactiveUI.WinUI package, which contains the core functionality and base classes for WinUI applications.
You will also need to install the ReactiveMarbles.ObservableEvents.SourceGenerator package, which enables automatic property change notifications.
You can use the NuGet Package Manager or the Package Manager Console to install these packages.

#### Create a view model: 

A view model is a class that represents the state and logic of your user interface. 
You can use the ReactiveObject base class to create a view model that supports reactive properties and commands. 
You can also use the ReactiveValidationObject base class to create a view model that supports validation rules and error messages.

#### Create a view: 

A view is a class that defines the appearance and layout of your user interface. 
You can use the IViewFor<TViewModel> interface to create a view that is associated with a specific view model type. 
You can also use the ReactiveUserControl<TViewModel> or ReactiveWindow<TViewModel> base classes to create a view that inherits from the WinUI UserControl or Window classes.
Bind the view and the view model: Binding is the process of connecting the properties and commands of your view model to the controls and events of your view. 
You can use the Bind, OneWayBind, and BindCommand extension methods to create bindings in code. 
You can also use the WhenActivated method to manage the lifetime of your bindings and subscriptions.

#### Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.WinUI (application)
- MyCoolApp.UnitTests (tests)
```

* Install [ReactiveUI](https://www.nuget.org/packages/ReactiveUI) into your netstandard libraries and tests.
* Install [ReactiveUI.WinUI](https://www.nuget.org/packages/ReactiveUI.WinUI) into your application and tests.
* Install [ReactiveMarbles.ObservableEvents.SourceGenerator](https://www.nuget.org/packages/ReactiveMarbles.ObservableEvents.SourceGenerator) into your application.
* Install [ReactiveUI.Testing](https://www.nuget.org/packages/ReactiveUI.Testing) into your tests.

