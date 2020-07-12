---
title: ReactiveUI On The Web with Blazor
category: 
  - Article
author: Rich Bryant
---

<img src="https://avatars0.githubusercontent.com/u/2327577?s=200&v=4" align="right" style="height:8em"/>

# ReactiveUI On The Web With Blazor!  
## It really works, honest.  
  
I hope by now people are starting to learn about the existence of the [ReactiveUI.Blazor](https://www.nuget.org/packages/ReactiveUI.Blazor/) package.  Blazor means "writing Single Page Apps like Angular or Vue in C#" and it has all the potential in the world.  And ReactiveUI supports it.  The official [samples are here](https://github.com/reactiveui/ReactiveUI.Samples/tree/main/blazor) but I don't feel that they really sell the reason _why_ you'd want to do web work with ReactiveUI, and I should know, I wrote them.
  
For me, the big seller is that you've already got a WPF or Xamarin or WinForms app using ReactiveUI and you want a web version.  In such a case, you can rewrite everything from scratch _or_ you can use Blazor and keep the logic in your ViewModels absolutely unchanged.  Sounds nice in theory but the web is a tricky thing, so does it actually work?  I decided to try it.  
  
### The ReactiveUI Compelling Example  
  
The ReactiveUI website Getting Started section links a [Compelling Example](https://www.reactiveui.net/docs/getting-started/compelling-example), a simple Nuget browser that you can build very quickly with copypasta.  

<img src="https://i.imgur.com/GoczlPS.png" align="center"/>

As you can see, it has text input, it does pipelines, it has data, it has views and viewmodels.... Why not, I thought? What are the steps?  
  
1. Follow the instructions Rich, you idiot

Yeah, that makes sense.  Step 1 - build the Compelling Example just like the instructions say except that I used WPF in .NET Core 3.1 which worked flawlessly and I didn't have to change anything.  I mostly did this because Blazor is a .NET Core app and I wanted to avoid any hassles later.  The screenshot above is my working WPF project.

2. Add a Blazor project.  

I used Blazor AspNetCore Hosted for reasons which I hope will become clear later and I moved the actual "get the data" stuff to a service inside the Server project.  I also created a ViewModels project (a .NET Standard 2.1 class library) because I don't want to copy my ViewModels, I want them shared.  
  
Here's the project structure :  
  
<img src="https://i.imgur.com/BCf0xvv.png" align="center"/>  
  
Yes, I could have put the viewmodels in CompellingExample.Blazor.Shared but they contain no Blazor code and apply to the original WPF project too, so I didn't.  You can throw eggs me at later.  Let's move on.  
  
Because I needed an HttpClient calling an MVC WebApi, I put Refit on both the Blazor Client and the CompellingExample - they have to be the same, right?  I won't go into the specifics because this isn't about Refit, I could have done it any number of ways, but I like Refit.  The code's in a [GitHub repo](https://github.com/richbryant/ReactiveUI.CompellingExample) if you want to check it for errors.  
  
This did require some wrangling of Splat, shown here in the App.xaml.cs of the WPF project:  

    public partial class App
    {
        public App()
        {
            Locator.CurrentMutable.Register(() => new MainWindow(), typeof(IViewFor<AppViewModel>));
            Locator.CurrentMutable.Register(() => new NugetDetailsView(), typeof(IViewFor<NugetDetailsViewModel>));
            
            Locator.CurrentMutable.RegisterLazySingleton(() =>
                RestService.For<INugetService>("https://localhost:44394/api"), typeof(INugetService));
            
        }
    }

3. Wrangle Splat into Blazor

Here's my Program.cs.  As you can see, I'm using [Blazorise](https://blazorise.com) mostly because I suck at CSS and styling things, and I think `<div>` is ugly.  You don't have to, obviously.  
  
     public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            
            builder.Services
                .AddBlazorise( options =>
                {
                    options.ChangeTextOnKeyPress = false;
                } )
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            builder.Services.UseMicrosoftDependencyResolver(); //Splat config
            var resolver = Locator.CurrentMutable;
            resolver.InitializeSplat();
            resolver.InitializeReactiveUI();

            Locator.CurrentMutable.Register(() => new IndexView(), typeof(IViewFor<AppViewModel //Splat!
            Locator.CurrentMutable.Register(() => new NugetDetailsView(), typeof(IViewFor<NugetDetailsViewModel>)); //Splat!

            Locator.CurrentMutable.RegisterLazySingleton(() =>
                RestService.For<INugetService>("https://localhost:44394/api", new RefitSettings{ ContentSerializer = new JsonContentSerializer()}), typeof(INugetService)); //Annoying bit of Splat that I kicked myself for forgetting!

            builder.RootComponents.Add<App>("app");
            var host = builder.Build();

            host.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    
    
Other than that, you don't need much.  
  
4.  Create your views!  
  
Here's the IndexView.razor  
  
    @page "/"
    @inherits ReactiveComponentBase<AppViewModel>

    <Container class="mainContent">
        <header>
            <Addons>
                <Addon AddonType="AddonType.Body">
                    <TextEdit @bind-Text="ViewModel.SearchTerm" Placeholder="Search nuget.org..." />
                </Addon>
                <Addon AddonType="AddonType.End">
                    <Button Color="Color.Secondary" Clicked="@SearchTextChanged">Search</Button>
                </Addon>
            </Addons>
        </header>
        <main>
        @if (ShowResults)
        {
            <Table>
                @foreach (var result in ViewModel.SearchResults)
                {
                    <NugetDetailsView NugetViewModel="@result"/>
                }
            </Table>
        }
        </main>
    </Container>
    
and here's its codebehind.  
  
    public partial class IndexView 
    {
        private bool _showResults;
        public bool ShowResults
        {
            get => _showResults;
            set
            {
                _showResults = value;
                StateHasChanged();
            } 
        }

        public IndexView()
        {
            ViewModel = new AppViewModel();

            this.WhenActivated(disposableRegistration =>
            {
                this.OneWayBind(ViewModel, 
                        viewModel => viewModel.IsAvailable, 
                        view => view.ShowResults)
                    .DisposeWith(disposableRegistration); 
                
            });
        }

        private void SearchTextChanged()
        {
            //This is really just here for sanity chacking and to make the textbox lose focus.
            Console.WriteLine(ViewModel.SearchTerm);
            Console.WriteLine($"SearchResults is {ViewModel.SearchResults.Count()}");
        }
    }

and now the NugetDetailsView  
  
    @inherits ReactiveComponentBase<NugetDetailsViewModel>
    <TableRow>
        <TableRowCell>
            <Figure Size="FigureSize.Is64x64">
                <FigureImage Source="@ViewModel.IconUrl.ToString()" AlternateText="@ViewModel.Title"/>
            </Figure>
        </TableRowCell>
        <TableRowCell Class="boxContent">
            <header>
                <h3>@ViewModel.Title</h3>
            </header>
            <main>@ViewModel.Description</main>
            <footer>
                <Blazorise.Link To="@ViewModel.ProjectUrl.ToString()">
                    open
                </Blazorise.Link>
            </footer>
        </TableRowCell>
    </TableRow>

and its codebehind:  
  
    public partial class NugetDetailsView
    {
        [Parameter] 
        public NugetDetailsViewModel NugetViewModel { get; set; }

        protected override Task OnParametersSetAsync()
        {
            ViewModel = NugetViewModel;
            return base.OnParametersSetAsync();
        }
    }

Basic stuff.

You could run this now and provided you've got your controllers wired up properly, it'll work.  But I didn't much like it.  I'll show you why.  
  
### Fugly Code  
  
Have a look at this controller.  Yuck .  
  
    [Route("api/[controller]")]
    [ApiController]
    public class NugetController : ControllerBase
    {
        private readonly INugetService _nugetService;

        public NugetController(INugetService nugetService)
        {
            _nugetService = nugetService;
        }

        [HttpGet("{term}")]
        public async Task<IActionResult> GetNugetResults(string term)
        {
            try
            {
                var result = await _nugetService.GetNugetPackages(term);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

It might just be me, but I find Try/Catch personally offensive, especially in an async method.  That **had** to go.  And it wasn't just that, either.  Take a look at the code to actually get package details, straight from the original CompellingExample.  
  
    public class NugetService : INugetService
    {
        public async Task<IEnumerable<NugetPackageDto>> GetNugetPackages(string term)
        {
            var providers = new List<Lazy<INuGetResourceProvider>>();
            providers.AddRange(Repository.Provider.GetCoreV3());
            var packageSource = new PackageSource("https://api.nuget.org/v3/index.json");
            var source = new SourceRepository(packageSource, providers);

            var filter = new SearchFilter(false);
            var resource = await source.GetResourceAsync<PackageSearchResource>().ConfigureAwait(false);
            var metadata = await resource.SearchAsync(term, filter, 0, 10, null, new CancellationToken())
                .ConfigureAwait(false);
            return metadata.Select(x => new NugetPackageDto(x));
        }
    }
    
### Beautiful Functional C#

Wow.  Imagine having to unit test that.  Horrible.  Imagine _debugging_ it.  No, it wouldn't do.  Luckily, I have another tool in my toolbox especially for horrible things like these.  It's called Functional Programming and ReactiveUI is pretty religious about it, except here for some reason.  I don't know why.  So I added a few static methods.    
  
        public static SourceRepository NuGetLocalRepository()
            => new SourceRepository(new PackageSource("https://api.nuget.org/v3/index.json"),
                new List<Lazy<INuGetResourceProvider>>(Repository.Provider.GetCoreV3()));

        public static IEnumerable<NugetPackageDto> AsDtos(this IEnumerable<IPackageSearchMetadata> list)
            => list.Select(x => x.AsDto());

        public static NugetPackageDto AsDto(this IPackageSearchMetadata nuget)
            => new NugetPackageDto(nuget);

        public static async Task<PackageSearchResource> GetResource(this SourceRepository repo)
            => await repo.GetResourceAsync<PackageSearchResource>().ConfigureAwait(false);

        public static async Task<IEnumerable<IPackageSearchMetadata>> GetMetadata(PackageSearchResource source, string term)
            => await source.SearchAsync(term, new SearchFilter(false), 0, 10, null, new CancellationToken());

each one atomic, each one easy to test.  And I added also my second favourite library ever, [Language-Ext](https://github.com/louthy/language-ext) to get some nice helper methods and structs.  
  
And here's that `GetNugetPackages` method now.  
  
    public async Task<IEnumerable<NugetPackageDto>> GetNugetPackages(string term) =>
            await NugetFunctions.NuGetLocalRepository()
                .GetResource()
                .Bind(x => NugetFunctions.GetMetadata(x, term))
                .Map(x => x.AsDtos());

Oh look, a Functional pipeline!  Tell me you don't prefer that.

But I still had that nasty TryCatch in the controller.  Luckily for me, along with fun things like `Option<T>` which protects you from `null` and `Either<L,R>` which takes away 99% of all `if...then` statements, Language-Ext also has a `Try<T>` which performs a Try and returns either your value or a safely wrapped exception that you can deal with you choose.  And there's a `TryAsync` variant especially for asynchronous methods.  
  
So I did this.  
  
    public TryAsync<IEnumerable<NugetPackageDto>> TryGetNugetPackagesAsync(string term)
            => TryAsync(GetNugetPackages(term));

And I still didn't want an exception in my controller method, so I put in some MVC helper statics, [which you can find here if you care](https://github.com/richbryant/ReactiveUI.CompellingExample/blob/main/CompellingExample.Blazor/Server/Extensions/ExtensionsForMvc.cs).  And that left my controller as this  
  
    [Route("api/[controller]")]
    [ApiController]
    public class NugetController : ControllerBase
    {
        private readonly INugetService _nugetService;

        public NugetController(INugetService nugetService) => _nugetService = nugetService;


        [HttpGet("{term}")]
        public async Task<IActionResult> GetNugetResults(string term) =>
            await _nugetService.TryGetNugetPackagesAsync(term)
                .ToActionResult();
    }
    
Not only easy but also easy to throw in a few Functional C# improvements at the same time.  
  
I hope you find this both useful, and a compelling example.  
  
[All code is here](https://github.com/richbryant/ReactiveUI.CompellingExample)