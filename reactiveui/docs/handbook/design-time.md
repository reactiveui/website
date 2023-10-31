---
NoTitle: true
---
## ReactiveUI Bindings

ReactiveUI offers a better design-time data system for solutions that use [ReactiveUI type-safe bindings](~/docs/handbook/data-binding/index.md). `this.Bind` methods family overwrite whatever has been put into XAML. If your XAML markup looks like this:

```xml
<TextBlock 
    x:Name="ExampleTextBlock" 
    Text="This is design time text" />
```

You'll see `This is design time text` string in design time, that will be overridden by the code where binding magic happens at run time:

```cs
this.WhenActivated(disposable => 
{
    // ReactiveUI will bind ExampleString ViewModel property 
    // to ExampleTextBlock.Text property (defined above)
    this.Bind(ViewModel, 
        viewModel => viewModel.ExampleString, 
        view => view.ExampleTextBlock.Text)
        .DisposeWith(disposable);
});
```

## Regular Bindings

If you use regular bindings, or type-safe `{x:Bind }` markup extension available on UWP, then you can import the `d` directive and set design-time `DataContext`. For the ease of use, you can extract an interface from your ViewModel and create two implementations for it, one implementation would display design-time data only, and another one would be your actual view model. See an example.

**Views.AboutView.xaml**

```xml
<Page x:Class="MyCoolApp.UWP.Views.AboutView"
      xmlns:d="https://schemas.microsoft.com/expression/blend/2008"
      xmlns:designTime="using:MyCoolApp.UWP.DesignTime"
      d:DataContext="{d:DesignInstance designTime:DesignTimeAboutViewModel,
                                                  IsDesignTimeCreatable=True}"
      d:DesignHeight="600"
      d:DesignWidth="600">
  <!-- Page Content -->
</Page>
```

**Interfaces.IAboutViewModel.cs**

```cs
public interface IAboutViewModel : INotifyPropertyChanged
{
    IEnumerable<AboutSection> AboutSections { get; set; }

    ReactiveCommand<AboutFeed> RefreshCommand { get; set; }
}
```

**DesignTime.DesignTimeAboutViewModel.cs**

```cs
public class DesignTimeAboutViewModel : IAboutViewModel
{
  public DesignTimeAboutViewModel()
  {
      AboutSections = new List<AboutSectionViewModel>
      {
          new AboutSection {Title = "Title 1", Body = "Lorum Ipsum"},
          new AboutSection {Title = "Title 2", Body = "Lorum Ipsum"},
          new AboutSection {Title = "Title 3", Body = "Lorum Ipsum"}
      });
      RefreshCommand = ReactiveCommand.CreateFromTask(o => Task.FromResult(new AboutFeed()));
  }
  
  /* Properties and commands are deliberately omitted */
}
```

**ViewModels.AboutViewModel.cs**

```cs
public class AboutViewModel : ReactiveObject, IAboutViewModel
{
  /* Actual interface implementation */
}
```

    
