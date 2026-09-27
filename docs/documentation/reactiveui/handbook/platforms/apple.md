---
Order: 8
---
# Apple platforms

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/platform-apple/platform-apple.csproj).

iOS, Mac Catalyst, tvOS and macOS views come from UIKit or AppKit. Neither toolkit gives a view a built-in way to
raise a change notification, and neither has anything like a router or an activation lifecycle. The core
`ReactiveUI` package supplies all three. It builds for the `ios`, `maccatalyst`, `macos` and `tvos` target
frameworks, so there is no separate Apple package to install beyond `ReactiveUI` itself. It adds:

- base classes for view controllers, views, controls, image views and (on UIKit) navigation, tab bar and page
  containers, so each one is an `IViewFor<TViewModel>` with a `ViewModel` property and change notifications;
- `RoutedViewHost`, which follows a router's navigation stack on iOS, Mac Catalyst and tvOS, and
  `ViewModelViewHost`, which shows whatever view model you assign it, on every Apple platform including macOS;
- `AutoSuspendHelper<T>`, which turns app life-cycle callbacks into the [suspension](../data-persistence.md)
  signals a suspension driver needs, and `AppSupportJsonSuspensionDriver`, which saves and loads state under the
  platform's Application Support directory;
- `PlatformOperations`, which answers `IPlatformOperations.GetOrientation()` the way every platform does.

This page's example is a library app: a catalog of books, a loan flow that picks a member, and a members tab.
iOS and macOS share the same view models and the same `Book`, `Member`, `LoanViewModel` and `BookCatalogViewModel`
types; only the views differ, because UIKit and AppKit are different toolkits. On iPhone the root is a tab bar
with the catalog, members and a cover-paging tab. On iPad and on macOS the root is a split view: a catalog master
column and a `ViewModelViewHost` detail column that shows whichever book is selected.

## Start ReactiveUI

**1. Register the platform module.** `WithPlatformModule<PlatformRegistrations>()` on the
[app builder](../rxappbuilder.md) registers `IPlatformOperations`, `ISuspensionDriver` and the platform's
main-thread sequencer. Call it once, before any view appears: in `FinishedLaunching` on iOS, in
`DidFinishLaunching` on macOS.

```csharp
ReactiveUIBuilder builder = RxAppBuilder.CreateReactiveUIBuilder();
_ = builder.WithPlatformModule<PlatformRegistrations>().BuildApp();
```

Apple ships no `With<Platform>()` convenience method of its own; `PlatformRegistrations` is the same
`IWantsToRegisterStuff` module every platform package supplies, and `WithPlatformModule<T>()` is the general way
to load one.

**2. Wire up automatic state suspension.** `AutoSuspendHelper<T>` needs the app delegate as its type parameter,
so it can verify the delegate overrides every life-cycle method it depends on. Forward each override to the
matching helper method.

```csharp
_autoSuspendHelper = new AutoSuspendHelper<AppDelegate>(this);
_autoSuspendHelper.FinishedLaunching(application, launchOptions!);
Console.WriteLine($"FinishedLaunching captured {_autoSuspendHelper.LaunchOptions?.Count ?? 0} launch option(s).");
```

```text
FinishedLaunching captured 0 launch option(s).
```

```csharp
public override void OnActivated(UIApplication application) =>
    _autoSuspendHelper?.OnActivated(application);

public override void DidEnterBackground(UIApplication application) =>
    _autoSuspendHelper?.DidEnterBackground(application);
```

On macOS the helper watches a different set of notifications, because `NSApplicationDelegate` has no
`FinishedLaunching`/`OnActivated`/`DidEnterBackground` trio. It forwards `DidFinishLaunching`, `DidBecomeActive`,
`DidResignActive`, `DidHide` and `ApplicationShouldTerminate` instead:

```csharp
_autoSuspendHelper = new AutoSuspendHelper<AppDelegate>(this);
_autoSuspendHelper.DidFinishLaunching(notification);
```

```csharp
public override void DidBecomeActive(NSNotification notification) =>
    _autoSuspendHelper?.DidBecomeActive(notification);

public override void DidResignActive(NSNotification notification) =>
    _autoSuspendHelper?.DidResignActive(notification);

public override void DidHide(NSNotification notification) =>
    _autoSuspendHelper?.DidHide(notification);

public override NSApplicationTerminateReply ApplicationShouldTerminate(NSApplication sender) =>
    _autoSuspendHelper?.ApplicationShouldTerminate(sender) ?? NSApplicationTerminateReply.Now;
```

`ApplicationShouldTerminate` returns `NSApplicationTerminateReply.Later` and only lets AppKit quit once
`ShouldPersistState` subscribers finish, so a quit never races a save.

The helper itself is `IDisposable`. Dispose it from the app delegate's own `Dispose(bool)` override, the same
override `NSObject`/`UIApplicationDelegate` subclasses already carry:

```csharp
protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        _autoSuspendHelper?.Dispose();
    }

    base.Dispose(disposing);
}
```

**3. Point the suspension host at a driver.** `AppSupportJsonSuspensionDriver` saves and loads state under
`~/Library/Application Support/<bundle id>/<subdirectory>/state.dat`. Its parameterless constructor uses a
`Data` subdirectory; the one-argument constructor names your own.

```csharp
RxSuspension.SuspensionHost.CreateNewAppState = static () => new LibraryAppState();

AppSupportJsonSuspensionDriver driver = new("Library");
RxSuspension.SuspensionHost.SetupDefaultSuspendResume(driver);

_ = driver.SaveState(new LibraryAppState { LastViewedBook = "Pride and Prejudice" }, LibraryAppStateJsonContext.Default.LibraryAppState)
    .Subscribe(static _ => Console.WriteLine("AppSupportJsonSuspensionDriver saved the app state."));
_ = driver.LoadState(LibraryAppStateJsonContext.Default.LibraryAppState)
    .Subscribe(
        static state => Console.WriteLine($"AppSupportJsonSuspensionDriver loaded state for {state?.LastViewedBook}."),
        static error => Console.WriteLine($"AppSupportJsonSuspensionDriver had nothing to load yet: {error.Message}."));
_ = driver.InvalidateState().Subscribe(static _ => Console.WriteLine("AppSupportJsonSuspensionDriver invalidated the saved state."));
```

`SaveState<T>(T, JsonTypeInfo<T>)` and `LoadState<T>(JsonTypeInfo<T>)` take a source-generated
`JsonTypeInfo<T>`, so serializing never needs reflection. `LibraryAppStateJsonContext` is a one-line
`JsonSerializerContext` for `LibraryAppState`:

```csharp
[JsonSerializable(typeof(LibraryAppState))]
public sealed partial class LibraryAppStateJsonContext : JsonSerializerContext;
```

`LoadState()` and `SaveState<T>(T)` are the same two operations without the type info, serializing through
reflection instead. They carry `[RequiresUnreferencedCode]` and `[RequiresDynamicCode]`, so a page built for
trimming and AOT calls only the typed overloads above.

A reader who stops here can already start ReactiveUI and persist state on both platforms. The rest of this page
gives a view controller a view model, wires up navigation, and covers the platform's other pieces.

## Give a view controller a view model

`ReactiveViewController<TViewModel>` wraps `UIViewController` on UIKit and `NSViewController` on AppKit with a
`ViewModel` property and its change notifications. Build the view in code inside `ViewDidLoad` (`LoadView` on
macOS, where `View` has no default), then bind inside `WhenActivated` so every binding tears down when the page
leaves and starts again if it returns.

```csharp
    internal UIButton LoanButton { get; } = UIButton.FromType(UIButtonType.System);

    internal LoanBoardView LoanBoard { get; } = new(CGRect.Empty);

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        Title = "Catalog";
```

```csharp
        _ = this.WhenActivated(d =>
        {
            foreach (Book book in ViewModel!.Books)
            {
                // The frame constructor pre-sizes the row; Auto Layout resizes it once ViewModel bindings run.
                BookRowView row = new(CGRect.Empty) { ViewModel = book };
                UITapGestureRecognizer tap = new(() => ViewModel!.SelectedBook = book);
                row.AddGestureRecognizer(tap);
                _rows.AddArrangedSubview(row);
            }

            d(this.BindCommand(ViewModel, static vm => vm.OpenLoan, static v => v.LoanButton));
        });
    }
```

`LoanBoard` is a compact "on loan now" list docked below the button; [Tables and collections](#tables-and-collections)
covers what it is and how it gets its rows.

`LoanButton` is `internal`, not `private`: the `this.BindCommand(...)` call above compiles to a generated,
reflection-free dispatch, and the generator can only observe a `public` or `internal` member. A `private` target
fails the build with `RXUIBIND003`. `LoanViewController`, the page `RoutedViewHost` pushes next, follows the same
shape and adds a one-way bind with a converter:

```csharp
d(this.OneWayBind(
        ViewModel,
        static vm => vm.SelectedMember,
        static v => v.SelectedMemberLabel.Text,
        static member => member is null ? "(no member selected)" : $"To: {member.Name}"));
d(this.BindCommand(ViewModel, static vm => vm.ConfirmLoan, static v => v.ConfirmButton));
d(this.BindCommand(ViewModel, static vm => vm.Cancel, static v => v.CancelButton));
```

Every `ReactiveViewController<TViewModel>` also frees its own controls. Override `Dispose(bool)`, the same
`NSObject` override the base class itself overrides, and dispose what you built:

```csharp
protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        _rows.Dispose();
        LoanButton.Dispose();
        LoanBoard.Dispose();
    }

    base.Dispose(disposing);
}
```

`ViewDidLoad`/`LoadView` run once, before the page ever appears; UIKit and AppKit call `ViewWillAppear` and
`ViewDidDisappear` (`ViewWillAppear()`/`ViewDidDisappear()`, no argument, on macOS) every time the page appears
and leaves. `ReactiveViewController<TViewModel>` overrides both to raise `Activated` and `Deactivated`, which
`WhenActivated` needs internally; application code never calls either override directly.

## Views, controls and image views

`ReactiveView<TViewModel>`, `ReactiveControl<TViewModel>` and `ReactiveImageView<TViewModel>` give the same
`ViewModel` property to a plain `UIView`/`NSView`, a `UIControl`/`NSControl`, and a `UIImageView`/`NSImageView`.
`BookRowView` is a `ReactiveView<Book>` that shows one catalog row; a real catalog would use a reactive table
source instead of a stack of rows, which is a different chunk of this API covered under
[Tables and collections](#tables-and-collections) below.

```csharp
    internal UILabel TitleLabel { get; } = new() { Font = UIFont.PreferredHeadline! };

    internal UILabel StatusLabel { get; } = new() { Font = UIFont.PreferredSubheadline!, TextColor = UIColor.SecondaryLabel };
```

```csharp
        _ = this.WhenActivated(d =>
        {
            d(this.OneWayBind(ViewModel, static vm => vm.Title, static v => v.TitleLabel.Text));
            d(this.OneWayBind(
                ViewModel,
                static vm => vm.IsOnLoan,
                static v => v.StatusLabel.Text,
                static onLoan => onLoan ? "On loan" : "Available"));
            d(Activated.Subscribe(static _ => Console.WriteLine("BookRowView activated.")));
            d(Deactivated.Subscribe(static _ => Console.WriteLine("BookRowView deactivated.")));
        });
```

Setting `ViewModel` happens before a view or control ever gains a superview, never after. `Activated` fires from
`WillMoveToSuperview`/`ViewWillMoveToSuperview` the moment a view or control gains one, so a `BookDetailViewController`
that hosts a cover and a rating control builds each one fully, `ViewModel` included, before adding it to its
stack:

```csharp
BookCoverImageView cover = new() { ViewModel = ViewModel, TranslatesAutoresizingMaskIntoConstraints = false };
```

```csharp
StarRatingControl rating = new() { ViewModel = ViewModel };
_layout.AddArrangedSubview(rating);
```

`StarRatingControl` is a `ReactiveControl<Book>` wrapping a `UIStepper`/`NSStepper`, since neither toolkit has a
built-in star control. It shows the full `ReactiveControl<TViewModel>` surface in one place: the classic
`PropertyChanged`/`PropertyChanging` events, the `Changed`/`Changing` observables, `ThrownExceptions`,
`SuppressChangeNotifications()`, and a two-way `Bind`:

```csharp
public event EventHandler? StarsChanged;

public int Stars
{
    get => (int)_stepper.Value;
    set => _stepper.Value = value;
}
```

```csharp
_stepper.ValueChanged += (_, _) => StarsChanged?.Invoke(this, EventArgs.Empty);

// A rating of 0 never fires a change notification on its own, so start silent rather than log a phantom change.
using (SuppressChangeNotifications())
{
    Stars = 0;
}

PropertyChanging += static (_, e) => Console.WriteLine($"StarRatingControl.{e.PropertyName} is changing.");
PropertyChanged += (_, e) => Console.WriteLine($"StarRatingControl.{e.PropertyName} changed to {Stars}.");

_ = this.WhenActivated(d =>
{
    d(this.Bind(ViewModel, static vm => vm.Rating, static v => v.Stars));
    d(Changed.Subscribe(static change => Console.WriteLine($"StarRatingControl.{change.PropertyName} changed (Changed stream).")));
    d(Changing.Subscribe(static change => Console.WriteLine($"StarRatingControl.{change.PropertyName} is changing (Changing stream).")));
    d(ThrownExceptions.Subscribe(static error => Console.WriteLine($"StarRatingControl threw: {error.Message}")));
    d(Activated.Subscribe(static _ => Console.WriteLine("StarRatingControl activated.")));
    d(Deactivated.Subscribe(static _ => Console.WriteLine("StarRatingControl deactivated.")));
});
```

`this.Bind(ViewModel, vm => vm.Rating, v => v.Stars)` works two-way because the binding layer finds a
`StarsChanged` event for the `Stars` property by name. Name a control's own change event `<Property>Changed` to
opt into the same convention.

`BookCoverImageView` is a `ReactiveImageView<Book>`. This page has no bundled cover art, so it sets a blank
placeholder image once and binds the accessibility label to the book's title instead of the image itself:

```csharp
public sealed class BookCoverImageView : ReactiveImageView<Book>
{
    public BookCoverImageView()
    {
        Image ??= new UIImage();

        _ = this.WhenActivated(d =>
        {
            d(this.OneWayBind(ViewModel, static vm => vm.Title, static v => v.AccessibilityLabel));
            d(ThrownExceptions.Subscribe(static error => Console.WriteLine($"BookCoverImageView threw: {error.Message}")));
        });
    }
}
```

## Navigate with RoutedViewHost

`RoutedViewHost` is a `ReactiveNavigationController` that pushes a view for whichever view model a
`RoutingState` router navigates to, and pops when the router navigates back. It exists on iOS, Mac Catalyst and
tvOS, because it wraps `UINavigationController`, which AppKit has no equivalent of. Give it the router and a
view locator, then navigate:

```csharp
RoutedViewHost catalogHost = new()
{
    Router = shell.Router,
    ViewLocator = catalogViewLocator,
};
catalogHost.TabBarItem = new UITabBarItem("Catalog", null, 0);
_ = shell.Router.Navigate.Execute(catalog).Subscribe();
```

`catalogViewLocator` is a `DefaultViewLocator` with a `Map` entry per view model, resolved without reflection:

```csharp
DefaultViewLocator catalogViewLocator = new();
catalogViewLocator.Map<BookCatalogViewModel, BookListViewController>();
catalogViewLocator.Map<LoanViewModel, LoanViewController>();
```

`RoutedViewHost`'s own constructor calls `WhenActivated` internally to track the router; its `PushViewController`
and `PopViewController` overrides run when the router's stack grows or shrinks, never when application code
calls them directly. `RoutedViewHostUnsafe`, the twin that also resolves a view registered only with the service
locator, carries `[RequiresDynamicCode]`; this page is built for trimming and AOT, so it never calls that twin,
the same way [data binding on Apple platforms](../data-binding/ios.md#which-views-the-hosts-find) recommends.

For a screen that is not driven by a router at all, `ReactiveNavigationController<TViewModel>` wraps a root view
controller directly. `MembersNavigationController` sets its own `ViewModel` once, for the tab bar to read:

```csharp
public sealed class MembersNavigationController : ReactiveNavigationController<MembersViewModel>
{
    public MembersNavigationController(MembersViewModel viewModel)
        : base(new MembersPlaceholderViewController { ViewModel = viewModel }) =>
        ViewModel = viewModel;
}
```

## Show any view model with ViewModelViewHost

`ViewModelViewHost` shows whatever view model you assign it, resolving a view through its own `ViewLocator`.
Unlike `RoutedViewHost`, it exists on macOS too, since it only needs `NSViewController`, not a navigation
controller. The library's iPad and macOS root is a split view whose detail column is a `ViewModelViewHost` that
tracks the catalog's selection:

```csharp
DefaultViewLocator detailLocator = new();
detailLocator.Map<Book, BookDetailViewController>();
_detailHost = new ViewModelViewHost
{
    DefaultContent = new UIViewController(),
    ViewLocator = detailLocator,
};
```

```csharp
_ = this.WhenActivated(d =>
    d(catalog.WhenAnyValue(static vm => vm.SelectedBook)
        .WhereNotNull()
        .Subscribe(book => _detailHost.ViewModel = book)));
```

`DefaultContent` shows while `ViewModel` is `null`, before anything is selected. `ViewModelViewHostUnsafe`
carries the same `[RequiresDynamicCode]` restriction as `RoutedViewHostUnsafe`, for the same reason.

## Page and split across screens

`ReactiveTabBarController<TViewModel>` and `ReactivePageViewController<TViewModel>` wrap
`UITabBarController` and `UIPageViewController`; both exist only on iOS, Mac Catalyst and tvOS.
`ReactiveSplitViewController<TViewModel>` wraps `UISplitViewController` on those three and `NSSplitViewController`
on macOS, so the library's split-view root is the one container this page shares across every Apple platform.

`LibraryTabBarController` is the iPhone root: a `RoutedViewHost` catalog tab, a `MembersNavigationController`
tab, a `BookCoverPagerViewController` tab, and a `BookShelfViewController` tab.

```csharp
public sealed class LibraryTabBarController : ReactiveTabBarController<LibraryShellViewModel>
{
    public LibraryTabBarController(LibraryShellViewModel shell, IViewLocator catalogViewLocator, BookCatalogViewModel catalog, MembersViewModel members)
    {
        ViewModel = shell;
```

```csharp
        BookShelfViewController shelf = new()
        {
            ViewModel = catalog,
            TabBarItem = new UITabBarItem("Shelf", null, 3),
        };

        ViewControllers = [catalogHost, membersNav, coverPager, shelf];

        _ = this.WhenActivated(d =>
        {
            d(Activated.Subscribe(static _ => Console.WriteLine("LibraryTabBarController activated.")));
            d(Deactivated.Subscribe(static _ => Console.WriteLine("LibraryTabBarController deactivated.")));
        });
    }
}
```

`BookCoverPagerViewController` pages through a `BookDetailViewController` per book. Give the base constructor a
transition style and orientation, implement `IUIPageViewControllerDataSource`, and set the first page once
`ViewModel` is known:

```csharp
public sealed class BookCoverPagerViewController : ReactivePageViewController<BookCatalogViewModel>, IUIPageViewControllerDataSource
{
    public BookCoverPagerViewController()
        : base(UIPageViewControllerTransitionStyle.Scroll, UIPageViewControllerNavigationOrientation.Horizontal)
    {
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        DataSource = this;

        _ = this.WhenActivated((Action<IDisposable> onDispose) =>
        {
            _ = onDispose;
            if (ViewModel!.Books.Count == 0)
            {
                return;
            }

            SetViewControllers([CreatePage(ViewModel.Books[0])], UIPageViewControllerNavigationDirection.Forward, false, null);
        });
    }
```

`GetPreviousViewController` and `GetNextViewController` are the data source methods `UIPageViewController` calls
as the reader swipes; the app never calls either directly. Both create a fresh `BookDetailViewController` with
its `ViewModel` already set, the same rule every view and control on this page follows.

`LibrarySplitViewController` shares its shape across iOS and macOS: a master column, a `ViewModelViewHost`
detail column, and a subscription that feeds the detail host from the master's selection. Only the master's
own type (`ReactiveViewController<BookCatalogViewModel>`) and how a split item is added
(`ViewControllers = [master, _detailHost]` on iOS, `AddSplitViewItem(NSSplitViewItem.FromViewController(...))`
on macOS) differ between the two.

```csharp
    public LibrarySplitViewController(LibraryShellViewModel shell, BookCatalogViewModel catalog)
    {
        ViewModel = shell;
        PreferredDisplayMode = UISplitViewControllerDisplayMode.OneBesideSecondary;
```

## The macOS window

`ReactiveWindowController` wraps `NSWindowController` with a `ViewModel` property, since AppKit has nothing like
a `UITableViewController` in this surface and needs its own window-level base class. `MainWindowController`
builds the window in `CreateWindow`, which its constructor passes straight to the base constructor, then builds
the split view once `WindowDidLoad` runs:

```csharp
public sealed class MainWindowController : ReactiveWindowController
{
    public MainWindowController()
        : base(CreateWindow())
    {
    }

    public override void WindowDidLoad()
    {
        base.WindowDidLoad();
```

```csharp
        Window!.ContentViewController = new LibrarySplitViewController(shell, catalog);

        // ReactiveWindowController does not implement IActivatableView, unlike the view and controller types, so
        // it subscribes to Activated/Deactivated directly rather than through WhenActivated.
        _ = Activated.Subscribe(static _ => Console.WriteLine("MainWindowController activated."));
        _ = Deactivated.Subscribe(static _ => Console.WriteLine("MainWindowController deactivated."));
    }
```

Every other Reactive base class on this page implements the interface `WhenActivated` needs and can use it
directly. `ReactiveWindowController` does not, so it subscribes to `Activated`/`Deactivated` itself instead of
wrapping the subscriptions in `WhenActivated`. Both still fire the same way, from `WindowDidLoad` and from
AppKit's `NSWindow.WillCloseNotification`.

## Read the device orientation

`PlatformOperations.GetOrientation()` answers the same question every platform's `IPlatformOperations` answers.
On UIKit it reads `UIDevice.CurrentDevice.Orientation`; AppKit has no concept of device orientation, so it always
returns `null` there.

```csharp
PlatformOperations platformOperations = new();
string? orientation = platformOperations.GetOrientation();
Console.WriteLine($"Device orientation: {orientation}.");
```

```text
Device orientation: Portrait.
```

## Suspension and life cycle, side by side

```mermaid
%%{init: {"theme": "base", "themeVariables": {"fontFamily": "Roboto, Helvetica, Arial, sans-serif", "fontSize": "15px", "primaryColor": "#DCE9FF", "primaryBorderColor": "#6C8EC4", "primaryTextColor": "#0B2447", "secondaryColor": "#E3F2E8", "secondaryBorderColor": "#7FA88C", "secondaryTextColor": "#12301C", "tertiaryColor": "#F3E5F5", "tertiaryBorderColor": "#A98BB0", "tertiaryTextColor": "#2E1437", "lineColor": "#7B8699", "textColor": "#1B1F27", "noteBkgColor": "#FFF4D6", "noteBorderColor": "#C9A94F", "noteTextColor": "#3A2A00", "actorBkg": "#DCE9FF", "actorBorder": "#6C8EC4", "actorTextColor": "#0B2447", "signalColor": "#7B8699", "signalTextColor": "#1B1F27", "labelBoxBkgColor": "#F1F3F8", "labelBoxBorderColor": "#A7AEBB", "edgeLabelBackground": "#F7F9FC", "clusterBkg": "#F7F9FC", "clusterBorder": "#C9D1DE"}}}%%
flowchart LR
    classDef view fill:#DCE9FF,stroke:#6C8EC4,color:#0B2447
    classDef vm fill:#E3F2E8,stroke:#7FA88C,color:#12301C
    classDef model fill:#F3E5F5,stroke:#A98BB0,color:#2E1437
    classDef neutral fill:#F1F3F8,stroke:#A7AEBB,color:#1B1F27
    Launch(["FinishedLaunching /\nDidFinishLaunching"]):::view -- "IsResuming" --> Host(["SuspensionHost"]):::vm
    Background(["DidEnterBackground /\nDidResignActive"]):::view -- "ShouldPersistState" --> Host
    Foreground(["OnActivated /\nDidBecomeActive"]):::view -- "IsUnpausing" --> Host
    Crash(["Unhandled exception"]):::neutral -- "ShouldInvalidateState" --> Host
    Host -- "SaveState / LoadState" --> Driver(["AppSupportJsonSuspensionDriver"]):::model
```

`AutoSuspendHelper<T>` turns four kinds of app life-cycle callback into the four signals `SuspensionHost`
exposes; `SetupDefaultSuspendResume` connects `ShouldPersistState` and `ShouldInvalidateState` to the driver's
`SaveState` and `InvalidateState`, and a resume connects `IsResuming` to `LoadState`.

Each `ReactiveUI.*` package also ships as `ReactiveUI.*.Reactive`, built from the same source for apps that use
System.Reactive instead of the primitives this page's samples use.

## Tables and collections

`ReactiveTableViewController<TViewModel>`, `ReactiveCollectionViewController<TViewModel>` and the plain
`ReactiveTableView<TViewModel>`/`ReactiveCollectionView<TViewModel>` views wrap `UITableView` and `UICollectionView`
with a reactive source that keeps rows in sync with a collection. `Members` and `Books` are both
`ObservableCollection<T>`, ReactiveUI's collection change-set support: because each implements
`INotifyCollectionChanged`, a bound source adds and removes rows as the collection changes, with no extra code.
These types exist only on UIKit (iOS, Mac Catalyst, tvOS); AppKit has no table or collection source.

**1. Pair the collection with a cell and a header.** `TableSectionInformation<TSource, TCell>` names the collection,
the cell type's reuse key, a row height, and an optional `Action<TCell>` that runs once a cell is dequeued.
`TableSectionInformation<TSource>` is the base type its `Header`, `Footer`, `Collection`, `CellKeySelector`,
`InitializeCellAction` and `SizeHint` live on, and the type `ReactiveTableViewSource<TSource>.Data` returns. A
`TableSectionHeader` names a section from a `string`, or builds its own view from a `Func<UIView>` and a height:

```csharp
TableSectionInformation<Member, MemberCell> section = new(
    ViewModel!.Members,
    MemberCell.Key,
    sizeHint: 56F,
    static cell => Console.WriteLine($"Initializing a {cell.GetType().Name}."))
{
    Header = new TableSectionHeader("Members"),
    Footer = new TableSectionHeader(
        static () => new UILabel
        {
            Text = "Tap a member to see their card.",
            TextAlignment = UITextAlignment.Center,
            Font = UIFont.PreferredFootnote!,
            TextColor = UIColor.SecondaryLabel,
        },
        24F),
};
IReadOnlyList<TableSectionInformation<Member, MemberCell>> sections = [section];
```

The family's constructors differ only in a fixed `NSString` reuse key versus a `Func<object?, NSString>` chosen per
item, and whether the `Action<TCell>` is supplied; this table always shows one cell, so it never needs the per-item
selector. `CollectionViewSectionInformation<TSource>` and `CollectionViewSectionInformation<TSource, TCell>` are the
same two types for a `UICollectionView`.

**2. Bind the sections to the table view.** `ReactiveTableViewSourceExtensions.BindTo` builds a
`ReactiveTableViewSource<TSource>`, sets it as the table view's `Source`, and keeps `Data` current whenever the
sections observable emits again. The overload that takes a list of sections does not register the cell class, so
`ViewDidLoad` calls `TableView.RegisterClassForCellReuse(typeof(MemberCell), MemberCell.Key)` once itself, before the
table ever asks for a cell:

```csharp
d(Signal.Emit(sections).BindTo(TableView, source =>
{
    Source = source;
    return source.ElementSelected.Subscribe(static item => Console.WriteLine($"Selected member {((Member)item!).Name}."));
}));
```

`initSource` runs once, right after the source is built and before `Data` is set; it is the place to keep a reference
to the source and subscribe to `ElementSelected`, the stream of tapped rows. `InsertRowsAnimation`, `DeleteRowsAnimation`,
`ReloadRowsAnimation`, `InsertSectionsAnimation`, `DeleteSectionsAnimation` and `ReloadSectionsAnimation` keep their
default `UITableViewRowAnimation.Automatic` here, since this table's `Data` is set once and never changes again.
`Source.Data[0]` then reads back the same
`Header`, `SizeHint`, `Collection` and `InitializeCellAction` the constructor above set, this time through the
one-argument `TableSectionInformation<TSource>` the `Data` list holds.

**3. Write the cell.** `ReactiveTableViewCell<TViewModel>` is an `IViewFor<TViewModel>` `UITableViewCell`. UIKit
dequeues one through the `(IntPtr)` constructor a class registration needs, never any other. `MemberCell` also shows
the six classic `IReactiveObject` members every reactive Apple type carries: since `Member` is an immutable record
rather than a `ReactiveObject`, the label follows `ViewModel` itself changing instead of a `OneWayBind` on one of its
properties:

```csharp
        // The classic events fire for any property change; a cell can use them without going through Changed/Changing.
        PropertyChanged += static (_, e) => Console.WriteLine($"MemberCell.{e.PropertyName} changed (classic event).");
        PropertyChanging += static (_, e) => Console.WriteLine($"MemberCell.{e.PropertyName} changing (classic event).");

        _ = this.WhenActivated(d =>
        {
            // Member is an immutable record, not a ReactiveObject, so the label follows ViewModel itself changing
            // (which the cell base class does raise) rather than a OneWayBind on one of its properties.
            d(this.WhenAnyValue(static v => v.ViewModel).Subscribe(member => NameLabel.Text = member?.Name));
            d(Changing.Subscribe(static _ => Console.WriteLine("MemberCell changing.")));
            d(Changed.Subscribe(static _ => Console.WriteLine("MemberCell changed.")));
            d(ThrownExceptions.Subscribe(static error => Console.WriteLine($"MemberCell binding failed: {error.Message}")));
            d(Activated.Subscribe(static _ => Console.WriteLine("MemberCell activated.")));
            d(Deactivated.Subscribe(static _ => Console.WriteLine("MemberCell deactivated.")));
        });
```

`PrepareForReuse` clears that binding target inside a `SuppressChangeNotifications()` scope, so the reset itself never
looks like a change to anything observing the cell.

**4. Host the same collection in a plain view.** `ReactiveTableView<TViewModel>` and `ReactiveCollectionView<TViewModel>`
give a `UITableView`/`UICollectionView` an `IViewFor<TViewModel>` `ViewModel` property with no controller of its own.
The members page uses one as a header above its table: a horizontal strip of chips, bound with the simpler overload
that takes a collection directly and registers its own cell:

```csharp
public sealed class MemberChipStripView : ReactiveCollectionView<MembersViewModel>
{
    public MemberChipStripView(CGRect frame, UICollectionViewLayout layout)
        : base(frame, layout)
    {
        BackgroundColor = UIColor.SystemBackground;

        _ = this.WhenActivated(d =>
            d(Signal.Emit<INotifyCollectionChanged>(ViewModel!.Members).BindTo<Member, MemberChipCell>(this)));
    }
}
```

`BookListViewController` does the same with a `ReactiveTableView<BookCatalogViewModel>` further down its stack,
listing the books currently on loan.

**5. Give a grid a section header.** `ReactiveCollectionViewController<TViewModel>` wraps `UICollectionView` the way
`ReactiveTableViewController<TViewModel>` wraps `UITableView`, but its source, `ReactiveCollectionViewSource<TSource>`,
has no header hook of its own, unlike the table source's `Header`/`Footer`. A grid that needs a header subclasses the
source and overrides `GetViewForSupplementaryElement`, the UIKit method that returns a header or footer view, which
`BindTo` would otherwise leave unimplemented. It returns a `ReactiveCollectionReusableView<TViewModel>`, which
`BookShelfViewController` dequeues by class the same way it dequeues a cell. `ViewDidLoad` registers both the cell
and the header, since the sections overload of `BindTo` registers neither, then builds the section and constructs the
source directly instead of calling `BindTo`, since the header needs the subclass above:

```csharp
public override UICollectionReusableView GetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
{
    ShelfHeaderView header = (ShelfHeaderView)collectionView.DequeueReusableSupplementaryView(UICollectionElementKindSection.Header, HeaderKey, indexPath);
    header.ViewModel = _catalog;
    return header;
}
```

```csharp
            CollectionViewSectionInformation<Book, BookCoverCell> section = new(
                ViewModel!.Books,
                static _ => CoverCellKey,
                static cell => Console.WriteLine($"Initializing a {cell.GetType().Name}."));
            IReadOnlyList<CollectionViewSectionInformation<Book, BookCoverCell>> sections = [section];

            BookShelfCollectionViewSource source = new(CollectionView!, ViewModel);
            source.Data = sections;
            CollectionView!.Source = source;
```

`source.Data[0]` reads back through the one-argument `CollectionViewSectionInformation<TSource>`, the same way the
table section does. A `ReactiveCollectionReusableView<TViewModel>` reacts to `ViewModel` directly rather than through
`WhenActivated`, because UIKit sets it right after dequeuing the view and before adding it to the hierarchy:

```csharp
        _ = this.WhenAnyValue(static v => v.ViewModel)
            .WhereNotNull()
            .Subscribe(vm => CountLabel.Text = $"{vm.Books.Count} book(s)");
```

**6. Normalize a batch of index changes.** `Update`, `UpdateType` and `IndexNormalizer` are the infrastructure
`ReactiveTableViewSource<TSource>` and `ReactiveCollectionViewSource<TSource>` use internally to turn a burst of adds
and deletes on a collection into the ordered, de-duplicated batch UIKit's own batch-update APIs require. `Update` has
no public constructor; only `Update.CreateAdd(int)`, `Update.CreateDelete(int)` and `Update.Create(UpdateType, int)`
build one, and `IndexNormalizer.Normalize(IEnumerable<Update>)` is the one method that consumes them. Application code
never calls it: a source's own `Data` setter and `INotifyCollectionChanged` handler already normalize every batch
before applying it to the table or collection view.

## At a glance

| Member | What it does |
| --- | --- |
| `RxAppBuilder.CreateReactiveUIBuilder()` / `WithPlatformModule<PlatformRegistrations>()` | Builds and registers ReactiveUI's Apple platform module |
| `PlatformRegistrations` | The module `WithPlatformModule` loads: `IPlatformOperations`, `ISuspensionDriver` and the main-thread sequencer |
| `AutoSuspendHelper<T>` | Turns app life-cycle callbacks into suspend/resume signals; `T` is the app delegate type |
| `AutoSuspendHelper<T>.FinishedLaunching` / `OnActivated` / `DidEnterBackground` [ios] | The three UIKit callbacks to forward |
| `AutoSuspendHelper<T>.DidFinishLaunching` / `DidBecomeActive` / `DidResignActive` / `DidHide` / `ApplicationShouldTerminate` [macos] | The five AppKit callbacks to forward |
| `AutoSuspendHelper<T>.LaunchOptions` [ios] | The most recent launch options, as a string dictionary |
| `AutoSuspendHelper<T>.Dispose()` | Unsubscribes from `AppDomain.UnhandledException` and disposes the helper's signals |
| `AppSupportJsonSuspensionDriver` | Saves and loads state under Application Support |
| `AppSupportJsonSuspensionDriver.SaveState<T>(T, JsonTypeInfo<T>)` / `LoadState<T>(JsonTypeInfo<T>)` | Trim- and AOT-safe save and load through a source-generated `JsonTypeInfo<T>` |
| `AppSupportJsonSuspensionDriver.InvalidateState()` | Deletes the saved state file |
| `AppSupportJsonSuspensionDriver.SaveState<T>(T)` / `LoadState()` | The untyped, reflection-based overloads; not callable from a trimmed or AOT page |
| `PlatformOperations.GetOrientation()` | The device's current rotation on UIKit, or `null` on AppKit |
| `ReactiveViewController<TViewModel>` | A `UIViewController`/`NSViewController` that is an `IViewFor<TViewModel>` |
| `ReactiveView<TViewModel>` | A `UIView`/`NSView` that is an `IViewFor<TViewModel>` |
| `ReactiveControl<TViewModel>` | A `UIControl`/`NSControl` that is an `IViewFor<TViewModel>` |
| `ReactiveImageView<TViewModel>` | A `UIImageView`/`NSImageView` that is an `IViewFor<TViewModel>` |
| `ReactiveNavigationController<TViewModel>` [ios] | A `UINavigationController` that is an `IViewFor<TViewModel>` |
| `ReactiveTabBarController<TViewModel>` [ios] | A `UITabBarController` that is an `IViewFor<TViewModel>` |
| `ReactivePageViewController<TViewModel>` [ios] | A `UIPageViewController` that is an `IViewFor<TViewModel>` |
| `ReactiveSplitViewController<TViewModel>` | A `UISplitViewController`/`NSSplitViewController` that is an `IViewFor<TViewModel>` |
| `ReactiveWindowController` [macos] | An `NSWindowController` that is an `IViewFor<TViewModel>`-less `ReactiveObject`, with `WindowDidLoad` |
| `Activated` / `Deactivated` | Fire when a view, control or controller appears and leaves; feed [`WhenActivated`](../when-activated.md) |
| `Changed` / `Changing` / `PropertyChanged` / `PropertyChanging` | Observe and raise property changes, as on any `ReactiveObject` |
| `ThrownExceptions` | Reports errors raised inside reactive operators |
| `SuppressChangeNotifications()` | Pauses change notifications until the result is disposed |
| `RoutedViewHost` [ios] | Follows a `RoutingState`'s navigation stack, pushing and popping as it changes |
| `RoutedViewHost.Router` / `ViewLocator` / `ViewContractObservable` | The router to follow, the locator to resolve views with, and an optional contract stream |
| `RoutedViewHostUnsafe` [ios] | The `RoutedViewHost` twin that also resolves a service-locator-only view; carries `[RequiresDynamicCode]` |
| `ViewModelViewHost` | Shows whichever view model is assigned to it, resolved through its own `ViewLocator` |
| `ViewModelViewHost.ViewModel` / `ViewLocator` / `DefaultContent` / `ViewContract` / `ViewContractObservable` | The view model to show, the locator, the placeholder shown when `ViewModel` is `null`, and an optional contract |
| `ViewModelViewHostUnsafe` | The `ViewModelViewHost` twin that also resolves a service-locator-only view; carries `[RequiresDynamicCode]` |
| `ReactiveTableViewController<TViewModel>` / `ReactiveCollectionViewController<TViewModel>` [ios] | A `UITableViewController`/`UICollectionViewController` that is an `IViewFor<TViewModel>` |
| `ReactiveTableView<TViewModel>` / `ReactiveCollectionView<TViewModel>` [ios] | A `UITableView`/`UICollectionView` that is an `IViewFor<TViewModel>`, usable with no controller of its own |
| `ReactiveTableViewCell<TViewModel>` / `ReactiveCollectionViewCell<TViewModel>` [ios] | A `UITableViewCell`/`UICollectionViewCell` that is an `IViewFor<TViewModel>` |
| `ReactiveCollectionReusableView<TViewModel>` [ios] | A `UICollectionReusableView` that is an `IViewFor<TViewModel>`; used for a grid's section header or footer |
| `ReactiveTableViewSource<TSource>` / `ReactiveCollectionViewSource<TSource>` [ios] | Drives a table or collection view from a `Data` list of sections; the collection source has no header hook, so subclass it to add one |
| `ReactiveTableViewSource<TSource>.Data` / `ElementSelected` / `*RowsAnimation` / `*SectionsAnimation` | The bound sections, the stream of tapped rows, and the `UITableViewRowAnimation` each kind of update uses |
| `ReactiveCollectionViewSource<TSource>.Data` / `ElementSelected` | The bound sections, and the stream of tapped cells |
| `ReactiveTableViewSourceExtensions.BindTo` / `ReactiveCollectionViewSourceExtensions.BindTo` [ios] | Builds a source from a collection or a list of sections, and sets it on the table or collection view |
| `TableSectionInformation<TSource>` / `TableSectionInformation<TSource, TCell>` [ios] | A table section: its collection, cell reuse key, size hint, header and footer |
| `CollectionViewSectionInformation<TSource>` / `CollectionViewSectionInformation<TSource, TCell>` [ios] | The same section shape for a `UICollectionView`, with no header of its own |
| `TableSectionHeader` [ios] | A section header or footer: a `string` title, or a `Func<UIView>` and a height |
| `Update` / `UpdateType` / `IndexNormalizer` | The batching infrastructure the sources use internally to normalize adds and deletes for UIKit |
