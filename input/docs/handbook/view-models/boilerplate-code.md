If you are tired of writing boilerplate code for property change notifications, you can try either <a href="https://github.com/Fody/PropertyChanged">PropertyChanged.Fody</a> or <a href="https://www.nuget.org/packages/ReactiveUI.Fody/">ReactiveUI.Fody</a>. These libraries are both based on <a href="https://github.com/Fody/">Fody</a> - an extensible tool for weaving .NET assemblies, and they'll inject `INotifyPropertyChanged` code into properties at compile time for you. We recommend using <a href="https://www.nuget.org/packages/ReactiveUI.Fody/">ReactiveUI.Fody</a> package that also handles `ObservaleAsProperyHelper` properties.

# Read-write properties
Typically properties are declared like this:

```cs
private string name;

public string Name 
{
    get { return name; }
    set { this.RaiseAndSetIfChanged(ref name, value); }
}
```

With ReactiveUI.Fody, you don't have to write boilerplate code for getters and setters of read-write properties - the package will do it automagically for you at compile time.

```cs
[Reactive]public string Name { get; set; }
```

All you have to do is annotate the property with the `[Reactive]` attribute.

# ObservableAsPropertyHelper properties

Similarly, to declare output properties, the code looks like this:

```cs
ObservableAsPropertyHelper<string> firstName;

public string FirstName 
{
    get { return firstName.Value; }
}
```

Then the helper is initialized with a call to `ToProperty`:

```cs
...
.ToProperty(this, x => x.FirstName, out firstName);
```

With ReactiveUI.Fody, you can simply declare a read-only property using the `[ObservableAsProperty]` attribute:

```cs
public string FullName { [ObservableAsProperty]get; }
```
    
The field will be generated and the property implemented at compile time.

Because there is no field for you to pass to `.ToProperty`, you should use the `.ToPropertyEx` extension method provided by this library:

```cs
...
.ToPropertyEx(this, x => x.FullName);
```

This extension will assign the auto-generated field for you rather than relying on the `out` parameter.