NoTitle: true
---
A value converter should implement the `IBindingTypeConverter` interface. See an example of a globally registered converter type that converts between Boolean and XAML Visibility: [BooleanToVisibilityTypeConverter.cs](https://github.com/reactiveui/ReactiveUI/blob/main/src/ReactiveUI.Uwp/Common/BooleanToVisibilityTypeConverter.cs)

## How GetAffinityForObjects works

Return 0 if you can't convert an object and return anything else if it is possible. The largest value wins. 

```csharp
public int GetAffinityForObjects(Type fromType, Type toType)
{
    if (fromType == typeof(string))
    {
        return 100; // any number other than 0 signifies conversion is possible.
    }
    return 0;
}
```

## How TryConvert works

TryConvert function takes four arguments. The first one is value we are trying to convert, the second one is type to which we are trying to convert that value. `conversionHint` is like a converter parameter, and `result` is where we need to store the conversion result. TryConvert function should return `true` if conversion succeeds and `false` if conversion fails.

```csharp
public bool TryConvert(object from, Type toType, object conversionHint, out object result)
{
    try
    {
        result = !string.IsNullOrWhiteSpace((string)from);
    }
    catch (Exception ex)
    {
        this.Log().WarnException("Couldn't convert object to type: " + toType, ex);
        result = null;
        return false;
    }

    return true;
}
```

## Registration

If you'd like to use a custom converter globally, you need to register it using Splat Locator.

```csharp
Locator.CurrentMutable.RegisterConstant(
    new MyCoolTypeConverter(), 
    typeof(IBindingTypeConverter)
);
```

## Usage

For globally registered converters, usage is **automatic**. When a binding is created, the converter(s) with the highest affinity are chosen from those registered with Splat. Optionally, you can provide a specific converter to override what the default would have been for either VM to View or View to VM. In this case you don't need to register a converter:

```csharp
this.Bind(ViewModel, 
    viewModel => viewModel.ViewModelProperty, 
    view => view.Control.Property,
    vmToViewConverterOverride: new CustomTypeConverter());
```

It's important to specify `vmToViewConverterOverride` to be sure not to accidentally pass your converter to the `conversionHint` parameter.
    
## Inline Binding Converters

You can supply inline function methods to Bind. This allows you to quickly supply a conversion method. This avoids having to supply a IBindingTypeConverter for one off cases. 

```csharp            
this.WhenActivated(disposables =>
{
    this.Bind(this.ViewModel, 
        viewModel => viewModel.DateTime, 
        view => view.DateTextBox.Text, 
        this.ViewModelToViewConverterFunc, 
        this.ViewToViewModelConverterFunc)
        .DisposeWith(disposables);
});
                    
private string ViewModelToViewConverterFunc(DateTime dateTime)
{
    return dateTime.ToString("O"); // return ISO 8601 Date ime
}

private DateTime ViewToViewModelConverterFunc(string value)
{
    DateTime.TryParse(value, out DateTime returnValue);
    return returnValue;
}            
```
