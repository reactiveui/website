---
NoTitle: true
---
## IViewFor, Activation and Data Binding

In order to use bindings in the View, you must first implement `IViewFor<TViewModel>` on your View. Once you [implement `IViewFor<T>`](extending-iviewfor), binding methods are now available as extension methods on your class, as well as [activation and deactivation](../when-activated) feature for your views and associated view models that implement the `IActivatableViewModel` interface. See [Data Binding](../data-binding) section for details and platform-specific examples.

## View Location

View Location is a feature of ReactiveUI that allows you to associate Views with ViewModels and set them up Automagically.

### ViewModelViewHost

The easiest way to use View Location is via the `ViewModelViewHost` control, which is a View (on Cocoa, a UIView/NSView, and on XAML-based platforms a Control) which has a single `ViewModel` property. When the ViewModel property is set, View Location looks up the associated View and loads it into the container. 

`ViewModelViewHost` is great for lists - so much so, that if you Bind to
`ItemsSource` on XAML-based platforms and don't set a DataTemplate nor the `DisplayMemberPath` property, one gets
configured that just uses `ViewModelViewHost`.

```xml
<ListBox x:Name="ToasterList" />
```

```cs
// Now ListBox automatically gets a DataTemplate
this.OneWayBind(ViewModel, vm => vm.ToasterList, v => v.ToasterList.ItemsSource);
```

**This is not supported by Xamarin.Forms! You have to use a DataTemplate there and specify your view class.**

### Registering new Views

To use View Location, you must first register types, via Splat's Service Location feature.

```cs
Locator.CurrentMutable.Register(() => new ToasterView(), typeof(IViewFor<ToasterViewModel>));
```

View Location internally uses a class called `ViewLocator` which can either be replaced, or the default one used. The `ResolveView` method will return the View associated with a given ViewModel object.

### Using reflection to register views
ReactiveUI has some helper methods that use Reflection to register all the view's that implement the `IViewFor` interface. Be aware that due to the fact it is using Reflection it is slower than manually registering each view by hand.

```cs
Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
```

### Overriding ViewLocator

If you want to override the view locator, then you want to start by creating a class that inherits from `IViewLocator`.

```cs
public class ConventionalViewLocator : IViewLocator
{
    public IViewFor ResolveView<T>(T viewModel, string contract = null) where T : class
    {
        // Find view's by chopping of the 'Model' on the view model name
        // MyApp.ShellViewModel => MyApp.ShellView
        var viewModelName = viewModel.GetType().FullName;
        var viewTypeName = viewModelName.TrimEnd("Model".ToCharArray());

        try
        {
            var viewType = Type.GetType(viewTypeName);
            if (viewType == null)
            {
                this.Log().Error($"Could not find the view {viewTypeName} for view model {viewModelName}.");
                return null;
            }
            return Activator.CreateInstance(viewType) as IViewFor;
        }
        catch (Exception)
        {
            this.Log().Error($"Could not instantiate view {viewTypeName}.");
            throw;
        }
    }
}
```

Then, while bootstrapping your app you'll want to tell ReactiveUI about your new view locator:

```cs
// Make sure Splat and ReactiveUI are already configured in the locator
// so that our override runs last
Locator.CurrentMutable.RegisterLazySingleton(() => new ConventionalViewLocator(), typeof(IViewLocator));
```

### Manually setting the view

Sometimes you need to manually set the view for the items in an `ItemsControl` or one of its subclasses.
The easiest option is to set `DisplayMemberPath`, which will cause ReactiveUI to not assign a value to `ItemTemplate` and will instead show the referenced property as text. A more powerful option is manually setting a value for `ItemTemplate`, which gives you full control over how each item is displayed.

```XML
<!--  ReactiveUI will set ItemTemplate to ViewModelViewHost -->
<ItemsControl />

<!--  ReactiveUI ignores this one because ItemTemplate is already set -->
<ItemsControl> 
    <ItemsControl.ItemTemplate>
        ...
    </ItemsControl.ItemTemplate>
</ItemsControl>

<!--  ReactiveUI ignores this one because DisplayMemberPath is already set -->
<ItemsControl DisplayMemberPath="SomeValue" /> 
```

It's possible you want to use `DisplayMemberPath`, but don't know the value for it at compile time. Trying to bind the property will result in the following exception: "InvalidOperationException: Cannot set both DisplayMemberPath and ItemTemplate". This is because ReactiveUI looks at the control on initialization, does not see any preset value for `DisplayMemberPath` nor `ItemTemplate` and decides to set `ItemTemplate` to `ViewModelViewHost`. Then, when the ViewModel is attached to the View, the binding tries to set the value for `DisplayMemberPath`, and the aforementioned exception occurs. The solution is to set a dummy value for `DisplayMemberPath`, which will be replaced by the binding but will stop ReactiveUI from trying to set `ItemTemplate`.
```XML
<!-- The dummy value will cause ReactiveUI to ignore this control -->
<ItemsControl Name="MyItemsControl" DisplayMemberPath="DummyValue" /> 
```
```cs
//This binding will override the dummy value
this.OneWayBind(ViewModel, vm => vm.MyDisplayMemberPath, v => v.MyItemsControl.DisplayMemberPath);
```
