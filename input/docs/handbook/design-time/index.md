# Design Time

## Chatlogs

      RxUI has a better (IMO) system for design-time data
      `this.Bind` and family overwrite whatever has been put into the Xaml
      so your Xaml can look like this, and will be overridden by the following code: (edited)

      ```<TextBlock x:Name="SomeField" 
                        Text="-this is design time-" />

      this.Bind(ViewModel, vm => vm.Something, v => v.SomeField.Text);
      ```

      now, if you're on UWP, `{x:Bind}` is superior to both

## Reference

https://github.com/shiftkey/octohipster/blob/reactive-ui/OctoHipster/ViewModels/Design/DesignShellViewModel.cs


https://github.com/shiftkey/octohipster/blob/reactive-ui/OctoHipster/Views/ShellView.xaml#L6-L15


Views/AboutView.xaml:


    <Page
        x:Class="MyCoolApp.UWP.Views.AboutView"

        xmlns:designTime="using:MyCoolApp.UWP.DesignTime"
        d:DataContext="{d:DesignInstance designTime:DesignTimeAboutViewModel,
                                                  IsDesignTimeCreatable=True}"
        d:DesignHeight="600"
        d:DesignWidth="600"



DesignTime/DesignTimeAboutViewModel.cs:

    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using MyCoolApp.Core.ViewModels;
    using ReactiveUI;
    using MyCoolApp.Core.ServiceModel;

    namespace MyCoolApp.UWP.DesignTime
    {
        public class DesignTimeAboutViewModel : AboutViewModel
        {
            public DesignTimeAboutViewModel()
            {
                AboutSections = new ObservableCollection<AboutSectionViewModel>(new Collection<AboutSectionViewModel>
                {
                    new AboutSectionViewModel {Title = "Title 1", Body = "Lorum Ipsum"},
                    new AboutSectionViewModel {Title = "Title 2", Body = "Lorum Ipsum"},
                    new AboutSectionViewModel {Title = "Title 3", Body = "Lorum Ipsum"}
                });

                RefreshCommand = ReactiveCommand.CreateAsyncTask(o => Task.FromResult(new AboutFeed()));
            }
        }
    }
    
  ViewModels/IAboutViewModel.cs:

    public interface IAboutViewModel : IRoutableViewModel
    {
        ObservableCollection<AboutSectionViewModel> AboutSections { get; set; }

        ReactiveCommand<AboutFeed> RefreshCommand { get; set; }
    }

ViewModels/IAboutSectionViewModel.cs:

    public interface IAboutSectionViewModel
    {
        string Title { get; set; }
        string Body { get; set; }
    }
