NoTitle: true
Title: Extending View Support
---

## Extending IViewFor

There will be times where you need to extend the power of ReactiveUI view types to concrete implementations you don't control. The answer then is in extending the `IViewFor` interface so that ReactiveUI will pick up and register it against the `ViewLocator` instance.  Once you implement `IViewFor<T>`, binding methods are now available as extension methods on your class.

Below are a few ReactiveUI base implementations that are provided for various platforms, this is not an extensive list.

- [ReactiveUserControl](https://github.com/reactiveui/ReactiveUI/blob/main/src/ReactiveUI/Platforms/windows-common/ReactiveUserControl.cs) (Windows Platform)
- [ReactiveContentPage](https://github.com/reactiveui/ReactiveUI/blob/main/src/ReactiveUI.XamForms/ReactiveContentPage.cs) (Xamarin Forms)

#### **Note: As of writting this C# does not support inheritance from multiple classes.**

## Xamarin Forms Example

For this example we will use [Rg.Plugins.Popup](https://github.com/rotorgames/Rg.Plugins.Popup) a library that displays popup style modal pages for Xamarin.Forms.  This scenario expects that you are extending for a base implementation.  Another option is to explicitly extend each page from `IViewFor<T>`.  Chose the path that is right for you.

### The implementation

The first thing we need to do is bridge the `PopupPage`, which is a concrete implementation, by extending from `IViewFor`.

``` cs
 public abstract class BasePopupPage<TViewModel> : PopupPage, IViewFor<TViewModel>
        where TViewModel : class
    {
        protected readonly CompositeDisposable SubscriptionDisposables = new CompositeDisposable();

        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create(nameof(ViewModel),
                typeof(TViewModel),
                typeof(IViewFor<TViewModel>),
                (object) null,
                BindingMode.OneWay,
                (BindableProperty.ValidateValueDelegate) null,
                new BindableProperty.BindingPropertyChangedDelegate(OnViewModelChanged),
                (BindableProperty.BindingPropertyChangingDelegate) null,
                (BindableProperty.CoerceValueDelegate) null,
                (BindableProperty.CreateDefaultValueDelegate) null);

        /// <summary>The ViewModel to display</summary>
        public TViewModel ViewModel
        {
            get => (TViewModel) GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, (object) value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ViewModel = BindingContext as TViewModel;
        }

        private static void OnViewModelChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            bindableObject.BindingContext = newValue;
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TViewModel)value;
        }
    }
```

### The usage

From here all we need to inherit from `BasePopupPage`.

``` cs
    public partial class ExtendingPopupPage : BasePopupPage<ExtendingPopupViewModel>
    {
        public ExtendingPopupPage()
        {
            InitializeComponent();
        }
    }
```

### The XAML

In our XAML we need to make sure that we inherit from the same page so that the partial class will resolve correctly.  That should look similar to below.

``` xml
<ui:BasePopupPage
    x:TypeArguments="ui:ExtendingPopupViewModel"
    xmlns="https://xamarin.com/schemas/2014/forms"
    xmlns:x="https://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:pages="clr-namespace:Rg.Plugins.Popup.Pages;assembly=Rg.Plugins.Popup"
    xmlns:ui="clr-namespace:MyApplication;assembly=MyApplication"
    x:Class="MyApplication.ExtendingPopupPage">

</ui:BasePopupPage>
```

Now you should have full access to the `ViewModel` property on the `PopupPage` and be able to use Reactive Bindings and View Activation.
