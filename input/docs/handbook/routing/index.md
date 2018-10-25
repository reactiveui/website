https://stackoverflow.com/questions/26898381/reactiveui-view-viewmodel-injection-and-di-in-general

View-First or ViewModel-First?

Whether you can use VM-based routing (i.e. RoutedViewHost, IScreen, RoutingState, and friends) in ReactiveUI depends on the platform you're on:

* WPF, Xamarin Forms: Absolutely
* WP8, WinRT: You can make it work, you lose some transitions and niceties
* Android, iOS Native: Very difficult to make work

# XAML-based Platforms

In the App.xaml file register your viewmodels (If you want the viewmodel based navigation)
```    
this.Router = new RoutingState();
Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
Locator.CurrentMutable.Register(() => new LoginPage(), typeof(IViewFor<LoginViewModel>));
Locator.CurrentMutable.Register(() => new RegisterPage(), typeof(IViewFor<RegisterViewModel>));
```
Add `using Splat;` directive to use the `Splat.Locator` class. To start the app and navigate to the default page login page in our case use the below code
```
this.Router.Navigate.Execute(new HomeViewModel());
MainPage = new RoutedViewHost();
```
You can implement logic here checking if the user has a valid token and may be navigate to the home page bypassing the login page. It could be a check with in the akavache storage for the presense of a token. Below is the sample loginviewmodel
```
public LoginViewModel(IScreen screen = null)
{
    HostScreen = screen ?? Locator.Current.GetService<IScreen>();
    var canLogin = this.WhenAnyValue(x => x.Email, x => x.Password, (email, password) => 
    {
        LoginError = "";
        return !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password);
    });

    this.WhenAnyValue(x => x.Email).Subscribe(n => user.username = n);
    this.WhenAnyValue(x => x.Password).Subscribe(p => user.password = p);

    Register = ReactiveCommand.CreateFromObservable(
        () => HostScreen.Router.Navigate.Execute(new RegSelectionViewModel())
    );

    Login = ReactiveCommand.CreateFromTask<object, LoginResponse>(
        async _ => await App.userManager.Login(user), canLogin
    );

    Login.IsExecuting.ToProperty(this, x => x.IsLoading, out _isLoading);

    this.WhenAnyObservable(x => x.Login).Subscribe(response => 
    {
        Debug.WriteLine(response.AccessToken);
        LoginError = response.ErrorDescription;

        if (response.AccessToken != null) 
        {
            Debug.WriteLine("From Blob--> " + x);
            App.userManager.setToken(response.AccessToken);
            HostScreen.Router.NavigateAndReset.Execute(new HomeViewModel()).Subscribe();
        }
    });
}

```

In the above view model `canLogin` will listen to changes in the email and password fields and can enable the login command if the fields are not empty. One can even do a regex based email validation. One thing would be to bind this command to a login button inside your view like below:
```
this.BindCommand(ViewModel, vm => vm.Login, v => v.loginButton) ;
```

# Wizard Navigation

```
phil.cleveland [8:16 AM] 
@moswald: I see how that could work.  I give up the default binding goodness of the command

phil.cleveland [8:18 AM]
@michaelteper: Not sure I follow where you are indicating to put those subjects

phil.cleveland [8:19 AM]
My original design had each page impl the Next and Back and also the xaml for the buttons.  I didn't like it because each page then had to know about all the other pages it could possibly go to or go back to.  So I moved all the logic to the shell, but that forces me to recreate the command each time a page changes. (Which I think is legit, but doesn't work)

moswald [8:20 AM] 
well, my solution is a little bit wrong, combine it with @michaelteper's

moswald [8:20 AM]
pass two `Subject<bool>`s into your page VMs

moswald [8:20 AM]
those subjects are your `CanExecute`s

moswald [8:21 AM]
that way you keep your `ReactiveCommand` binding goodness

phil.cleveland [8:21 AM] 
I see. So set up the cmd with those and then the pages OnNext to define the enabled

phil.cleveland [8:21 AM]
Ok.  I like that

phil.cleveland [8:22 AM]
Thanks :simple_smile:

michaelteper [8:23 AM] 
```var canGoBack = new Subject<bool>();
var canGoNext = new Subject<bool>();
BackButton = ReactiveCommand.Create(canGoBack);
NextButton = ReactiveCommand.Create(canGoNext);
this.WhenAnyValue(x => x.Router.CurrentViewModel)
                .Subscribe(cvm =>
                {
                    var page = Router.GetCurrentViewModel() as IWizardPage;
                    canGoBack.OnNext(page.CanMoveBack);
                    canGoNext.OnNext(page.CanMoveNext);

          ...
```
