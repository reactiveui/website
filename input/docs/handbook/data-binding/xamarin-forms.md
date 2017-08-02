In the app.xaml file register your viewmodels (If you want the viewmodel based navigation)
```    
this.Router = new RoutingState();
Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
Locator.CurrentMutable.Register(() => new LoginPage(), typeof(IViewFor<LoginViewModel>));
Locator.CurrentMutable.Register(() => new RegisterPage(), typeof(IViewFor<RegisterViewModel>));
```
Include the splat package for Locator (Service locator) class (https://github.com/paulcbetts/splat). To start the app and navigate to the default page login page in our case use the below code
```
this.Router.Navigate.Execute(new HomeViewModel());
MainPage = new RoutedViewHost();
```
You can implement logic here checking if the user has a valid token and may be navigate to the home page bypassing the login page. It could be a check with in the akavache storage for the presense of a token.

Below is the sample loginviewmodel

```
public LoginViewModel(IScreen screen = null)
{
    HostScreen = screen ?? Locator.Current.GetService<IScreen>();
    var canLogin = (this).WhenAnyValue(
      x => x.Email,
      x => x.Password,
      (em, pa) => 
      {
          LoginError = "";
          return !string.IsNullOrWhiteSpace(em) &&
          !string.IsNullOrWhiteSpace(pa);
      });

    this.WhenAnyValue(x => x.Email).Subscribe(n => user.username = n);
    this.WhenAnyValue(x => x.Password).Subscribe(p => user.password = p);

    Register = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.Navigate.Execute(new RegSelectionViewModel()));

    Login = ReactiveCommand.CreateFromTask<object, LoginResponse>(
        async _ => 
        {
          return await App.userManager.Login(user);
        }, canLogin);

    Login.IsExecuting.ToProperty(this, x => x.IsLoading, out _isLoading);

    this.WhenAnyObservable(x => x.Login).Subscribe(response => 
    {
            Debug.WriteLine(response.access_token);
            LoginError = response.error_description;

            if(response.access_token != null) 
            {
                    Debug.WriteLine("From Blob--> " + x);
                    App.userManager.setToken(response.access_token);
                    HostScreen.Router.NavigateAndReset.Execute(new HomeViewModel()).Subscribe();
            }
    });
}

```

In the above view model `canLogin` will listen to changes in the email and password fields and can enable the login command if the fields are not empty. One can even do a regex based email validation. One thing would be to bind this command to a login button inside your view like below
```
this.BindCommand(ViewModel, vm => vm.Login, v => v.loginButton) ;
```