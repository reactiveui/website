Since Xamarin Android doesn't provide some method to automatically generate properties for subviews in your layout, ReactiveUI provides a helper method to do this for you.
Every `Activity`, `Fragment`, `View` or class implementing `ILayoutViewHost` can call the extensions method `WireUpControls` with one of three ways to resolve properties to their respective element in the layout.

It works in a similar fashion to [Butterknife](https://jakewharton.github.io/butterknife/) for Android.

## Naming policies

When calling `WireUpControls` from your application, a dictionary is built of all resource ID names used in your layouts, converted to lowercase, mapped to the actual resource ID. Essentially it's based on the contents of the `Resource.designer.cs` file.

Because the dictionary is indexed on the lowercase variant of the name, you can't use the same resouce name twice in your layouts with different casings. Android is case-sensitive when generating resource ID's, thus making it impossbile to map this to a single resource ID for use with `WireUpControls`.

The reason this is mapping is made, is because you would often use camelCase names in your layout files with a lower case first letter. While in your classes, properties should use an upper case first letter.

So while you can use the same resource name multiple times in a single or multiple layout files like you normally can, they would have to use the exact same casing.

## Resolving strategies

You can choose from three different `ResolveStrategy` options when calling `WireUpControls`, of which the default implicit variant will work in most cases, but for specialized cases we also provide two explicit strategies.

### Implicit

This is the default. `WireUpControls` tries to resolve every property that is a subclass of `View` in your class to a resource ID in your layout, regardless of visibility. Since your view should mainly be responsible for data-binding and not much else, this shouldn't impose a lot of problems. Mind you that properties of type `View` will be ignored since this causes propblems with default properties already present in activities for example.

### Explicit opt-in

In this case, you'll need to decorate properties you want to have wired up with the `WireUpResource` attribute. Every other property will be ignored by `WireUpControls`. No type-checking is imposed.

### Explicit opt-out

In this case, every property will be mapped to a resource ID in your layout file, unless it's decorated with the `IgnoreResource` attribute. This applies to all properties with a type of `View`, and subclasses thereof.

## Examples

For this layout:

```xml
<?xml version="1.0" encoding="utf-8"?>
  <LinearLayout xmlns:android="https://schemas.android.com/apk/res/android"
    android:orientation="vertical"
    android:layout_width="fill_parent"
    android:layout_height="fill_parent">
  <EditText
    android:id="@+id/TheEditText"
    android:layout_width="match_parent"
    android:layout_height="match_parent" />
  <TextView
    android:id="@+id/TheTextView"
    android:layout_width="match_parent"
    android:layout_height="match_parent" />
</LinearLayout>
```

### `ControlFetcherMixin.ResolveStrategy.Implicit`

```csharp
[Activity (Label = "WireUpControls-Sample", MainLauncher = true)]
public class TestActivity : ReactiveActivity<TheViewModel> 
{
    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);

        SetContentView(Resource.Layout.Main);

        this.WireUpControls(ControlFetcherMixin.ResolveStrategy.Implicit);

        ViewModel = new TheViewModel();

        this.Bind(this.ViewModel, x => x.TheText, x => x.TheEditText.Text);
        this.OneWayBind(this.ViewModel, x => x.TheText, x => x.TheTextView.Text);
    }

    public EditText TheEditText { get; private set; }

    public TextView TheTextView { get; private set; }
}
```

### `ControlFetcherMixin.ResolveStrategy.ExplicitOptIn`

```csharp
[Activity (Label = "WireUpControls-Sample", MainLauncher = true)]
public class TestActivity : ReactiveActivity<TheViewModel>
{
    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);

        SetContentView(Resource.Layout.Main);

        this.WireUpControls(ControlFetcherMixin.ResolveStrategy.ExplicitOptIn);

        ViewModel = new TheViewModel();

        this.Bind(this.ViewModel, x => x.TheText, x => x.TheEditText.Text);
        this.OneWayBind(this.ViewModel, x => x.TheText, x => x.TheTextView.Text);
    }

    [WireUpResource]
    public EditText TheEditText { get; private set; }

    [WireUpResource("TheTextView")
    public TextView AwesomeTextView { get; private set; }
}
```

### `ControlFetcherMixin.ResolveStrategy.ExplicitOptOut`

```csharp
[Activity (Label = "WireUpControls-Sample", MainLauncher = true)]
public class TestActivity : ReactiveActivity<TheViewModel>
{
    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);

        SetContentView(Resource.Layout.Main);

        this.WireUpControls(ControlFetcherMixin.ResolveStrategy.ExplicitOptOut);

        ViewModel = new TheViewModel();

        this.Bind(this.ViewModel, x => x.TheText, x => x.TheEditText.Text);
        this.OneWayBind(this.ViewModel, x => x.TheText, x => x.TheTextView.Text);
    }

    public EditText TheEditText { get; private set; }

    public TextView TheTextView { get; private set; }

    [IgnoreResource]
    public View SomeTempView { get; private set; }
}
```
