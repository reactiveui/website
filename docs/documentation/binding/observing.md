---
Order: 2
---
# Observing

[Run the complete page example](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/observing/observing.csproj).

A screen shows values that live on an object or deep inside one: the title of a to-do item, the login of an issue's assignee, the balance of the selected account. When one of those values changes, the screen has to change with it, and you do not want to poll for it.

Observing solves this. You write a lambda that names a property or a chain of properties, and the library gives you a stream of values. A stream is an `IObservable<T>`: a source of values that arrive over time. You subscribe to the stream to receive its values. A subscription is an `IDisposable`, so you dispose it to stop receiving values. The [first page](index.md) walks through a first binding.

An operator is a method that takes a stream and returns another stream, such as `Where` or `Select`. A notification is the way an object announces that a property changed, such as the `PropertyChanged` event of `INotifyPropertyChanged`. [Mechanisms](mechanisms.md) lists the notifications the library understands. A property path is a lambda that names one property or a chain of properties, such as `x => x.SelectedIssue!.Assignee!.Login`.

The source generator reads each property path while your project builds and writes the observation code for it, so nothing looks up a property by name at run time. If the generator cannot read a call, [Unsafe twins and the runtime fallback](unsafe.md) covers the `Unsafe` overload that finds properties by reflection.

## Observe one property

**1. Call `WhenChanged` on the object.** The lambda `x => x.Title` names the property. The object must be a reference type that announces changes, for example through `PropertyChanged`.

**2. Subscribe.** The stream delivers the current title as soon as you subscribe. `using` disposes the subscription at the end of the method. `CreateRegistrationTask` is a helper in the example that returns the sample to-do item.

**3. Change the property.** The subscriber receives the new title right after the setter runs. The example below performs all three steps and prints every title the subscriber receives.

```csharp
var item = CreateRegistrationTask();

using var subscription = item.WhenChanged(x => x.Title).Subscribe(Console.WriteLine);

item.Title = RenamedTitle;
```

```text
Renew car registration
Renew car registration online
```

The stream delivered two values: the title when you subscribed, and the title after the change. `WhenChanged` delivers a value only when the property changes to a different value. Setting a property to the value it holds delivers nothing.

## Choose an API

Six APIs observe properties. They differ in when they deliver and in what they deliver.

| API | Most properties | Delivers |
| --- | --- | --- |
| `WhenChanged` | sixteen | The value, a `PropertyValues` for several properties, or the result of a selector |
| `WhenChanging` | sixteen | The same as `WhenChanged`, just before each change |
| `WhenAnyValue` | sixteen | The same as `WhenChanged` |
| `WhenAny` | twelve | The result of a selector that receives one observed change per property |
| `WhenAnyObservable` | twelve | The values of the streams that the properties hold |
| `WhenAnyDynamic` | twelve | The result of a selector that receives one observed change per chain |

`WhenChanged`, `WhenChanging` and `WhenAnyValue` take one to sixteen properties. `WhenAny`, `WhenAnyObservable` and `WhenAnyDynamic` take one to twelve. A selector is a lambda that turns the observed values into one result. Every API except `WhenAnyObservable` delivers once when you subscribe and again after each change.

Follow these habits with every API. Mark a lambda `static` when it captures nothing, so the compiler reuses one delegate. Name an unused selector parameter `_`. Dispose every subscription. Write the property path inline: the generator reads a lambda written at the call site, and a path held in a variable is a call it cannot read.

A property path is a lambda that C# turns into an expression tree, and C# does not allow the null-propagating operator `?.` in such a lambda. Use a trailing `!` on a nullable link instead, as in `x => x.Assignee!`. The `!` only satisfies the compiler, the generator reads through it, and it changes nothing at run time. The generator does not read through parentheses, so write `x => x.A.B` and not `x => (x.A).B`.

## Observe several properties

Two or more properties deliver a `PropertyValues`. It holds one member per property, named `Property1`, `Property2` and so on, in the order of the lambdas. [Read several values at once](#read-several-values-at-once) covers it in detail.

```csharp
var item = CreateRegistrationTask();

using var subscription = item.WhenChanged(x => x.Title, x => x.IsDone).Subscribe(static values => Console.WriteLine(values.Property2));

item.IsDone = true;
```

```text
False
True
```

Add a selector as the last argument to turn the values into one result. The selector receives the values directly, in the order of the lambdas.

```csharp
var item = CreateRegistrationTask();

using var subscription = item
    .WhenChanged(x => x.Title, x => x.IsDone, static (title, isDone) => (isDone ? DoneMark : OpenMark) + title)
    .Subscribe(Console.WriteLine);

item.IsDone = true;
```

```text
[ ] Renew car registration
[x] Renew car registration
```

## Observe before a change

`WhenChanging` delivers the values just before a change replaces them. It delivers the current values when you subscribe, and again before each change with the values the change is about to replace. The object must raise `INotifyPropertyChanging.PropertyChanging`. The example type `ChangingObject` is a base class whose setters raise `PropertyChanging` before they store a value and `PropertyChanged` after it. The analyzer reports RXUIBIND004 when a type cannot raise `PropertyChanging`. See [Setup](setup.md) for the analyzers.

The example prints the title again after the setter runs, so you can compare the value the subscriber saw with the value the property holds.

```csharp
var item = CreateRegistrationTask();

using var subscription = item.WhenChanging(x => x.Title).Subscribe(Console.WriteLine);

item.Title = RenamedTitle;

Console.WriteLine(item.Title);
```

```text
Renew car registration
Renew car registration
Renew car registration online
```

The first line is the value at subscription. The second is the value before the change. The third is the value after the change.

`WhenChanging` takes several properties in the same way as `WhenChanged`. Each emission holds the values before the change.

```csharp
var item = CreateRegistrationTask();

using var subscription = item.WhenChanging(x => x.Title, x => x.IsDone).Subscribe(static values => Console.WriteLine(values.Property2));

item.IsDone = true;

Console.WriteLine(item.IsDone);
```

```text
False
False
True
```

A selector turns the values before the change into one result. This one prints the status line the item had before its done flag changed.

```csharp
var item = CreateRegistrationTask();

using var subscription = item
    .WhenChanging(x => x.Title, x => x.IsDone, static (title, isDone) => $"{title} ({(isDone ? DoneText : OpenText)})")
    .Subscribe(Console.WriteLine);

item.IsDone = true;
```

```text
Renew car registration (open)
Renew car registration (open)
```

## Observe with WhenAnyValue

`WhenAnyValue` takes the same lambdas as `WhenChanged` and delivers the same values after each change. The selector receives the values directly, and several properties deliver a `PropertyValues`.

```csharp
var item = CreateRegistrationTask();

using var subscription = item.WhenAnyValue(x => x.Title, static title => title.Length).Subscribe(Console.WriteLine);

item.Title = RenamedTitle;
```

```text
22
29
```

## Follow a path of properties

A property path can pass through several objects. The board below has a selected issue, the issue has an assignee, and the assignee has a login. The observation follows every link. It delivers no value until every link has an object, and then it delivers the current login.

```csharp
var board = await OpenWebshopBoardAsync();
var checkoutBug = FindIssue(board, CheckoutBugNumber);

List<string> logins = [];

using (board.WhenChanged(x => x.SelectedIssue!.Assignee!.Login).Subscribe(logins.Add))
{
    // Nothing is selected yet, so the path has no value to read.
    Console.WriteLine(logins.Count);

    board.SelectedIssue = checkoutBug;

    checkoutBug.Assignee!.Login = TomasLogin;
}

Console.WriteLine(string.Join(", ", logins));
```

```text
0
priya-nair, tomas-berg
```

**The observation moves with the objects.** When an object in the middle of the path is replaced, the observation attaches to the new object. It lets go of the old one.

```csharp
var board = await OpenWebshopBoardAsync();
var checkoutBug = FindIssue(board, CheckoutBugNumber);
var previousAssignee = checkoutBug.Assignee!;

board.SelectedIssue = checkoutBug;

List<string> logins = [];

using (board.WhenChanged(x => x.SelectedIssue!.Assignee!.Login).Subscribe(logins.Add))
{
    User newAssignee = new() { Login = TomasLogin };

    checkoutBug.Assignee = newAssignee;

    // The previous assignee no longer belongs to the path, so its changes are not observed.
    previousAssignee.Login = MariaLogin;

    newAssignee.Login = MariaLogin;
}

Console.WriteLine(string.Join(", ", logins));
```

```text
priya-nair, tomas-berg, maria-santos
```

**Only a changed final value is delivered.** A change elsewhere on the path, or a change to the same value, delivers nothing.

```csharp
var board = await OpenWebshopBoardAsync();
var checkoutBug = FindIssue(board, CheckoutBugNumber);

board.SelectedIssue = checkoutBug;

List<string> logins = [];

using (board.WhenChanged(x => x.SelectedIssue!.Assignee!.Login).Subscribe(logins.Add))
{
    // The same login again.
    checkoutBug.Assignee!.Login = PriyaLogin;

    // A different property of the assignee.
    checkoutBug.Assignee.DisplayName = "Priya N.";

    // A different object with the same login.
    checkoutBug.Assignee = new User { Login = PriyaLogin };

    // A property of the issue that is not on the path.
    checkoutBug.Title = "Checkout button fails on Safari";

    checkoutBug.Assignee.Login = TomasLogin;
}

Console.WriteLine(string.Join(", ", logins));
```

```text
priya-nair, tomas-berg
```

**A null in the middle delivers nothing.** When an object in the middle of the path is null, there is no final value to report. The observation waits for the next object.

```csharp
var board = await OpenWebshopBoardAsync();
var giftCards = FindIssue(board, GiftCardNumber);

List<string> logins = [];

using (board.WhenChanged(x => x.SelectedIssue!.Assignee!.Login).Subscribe(logins.Add))
{
    // The issue has no assignee, so there is no login to report.
    board.SelectedIssue = giftCards;

    giftCards.Assignee = new User { Login = TomasLogin };

    // Removing the assignee does not emit a null login either.
    giftCards.Assignee = null;

    giftCards.Assignee = new User { Login = MariaLogin };

    board.SelectedIssue = null;
}

Console.WriteLine(string.Join(", ", logins));
```

```text
tomas-berg, maria-santos
```

**A null final value is delivered.** When the path ends at the property that is null, every object on the path exists, so the null is a real value. It delivers nothing when no issue is selected.

```csharp
var board = await OpenWebshopBoardAsync();
var giftCards = FindIssue(board, GiftCardNumber);
User tomas = new() { Login = TomasLogin };

// The trailing ! only satisfies the compiler: the emitted values include null.
List<string> assignees = [];

using (board.WhenChanged(x => x.SelectedIssue!.Assignee!).Subscribe(assignee => assignees.Add(assignee?.Login ?? Unassigned)))
{
    board.SelectedIssue = giftCards;

    giftCards.Assignee = tomas;

    giftCards.Assignee = null;

    // With nothing selected there is no issue to read the assignee from.
    board.SelectedIssue = null;
}

Console.WriteLine(string.Join(", ", assignees));
```

```text
unassigned, tomas-berg, unassigned
```

**The null rules in one place.** A null link and a null value are different cases:

- **A null link before the last property.** The observation holds no value for the path. It drops its subscription on the detached objects and waits for a new object. The subscriber receives nothing, not a null.
- **A null last property.** Every object on the path exists, so the null is a real value and the subscriber receives it. Declare the subscriber's parameter as nullable, and use `!` on the lambda only to satisfy the compiler.

**Every object on the path must announce changes.** A storage object in the example raises no notification. The path `x.SelectedObject!.Key` would stop following at that object, and the analyzer reports RXUIBIND010. The generated observation reads such a link once and never again, and it writes no message. It delivers the value it read and then nothing, and it never completes, so the subscription stays open. End the path at the selection and read the key when a value arrives. A change to the key alone is not reported, and selecting the object again reads the key it has then.

```csharp
var browser = await OpenMediaBucketAsync();
var launchBanner = browser.Objects[LaunchBannerIndex];
var teamOffsite = browser.Objects[TeamOffsiteIndex];

List<string> keys = [];

browser.SelectedObject = launchBanner;

using (browser.WhenChanged(x => x.SelectedObject!).Subscribe(selected => keys.Add(selected.Key)))
{
    // A plain setter raises nothing, so the subscriber is not told.
    launchBanner.Key = RenamedLaunchBannerKey;

    browser.SelectedObject = teamOffsite;

    // Selecting the object again reads the key it has now.
    browser.SelectedObject = launchBanner;
}

Console.WriteLine(string.Join(", ", keys));
```

```text
photos/2026/launch-banner.png, photos/2026/team-offsite.jpg, photos/2026/launch-banner-v2.png
```

A path can also start on a plain property and reach a property of a property. This path reads the money available in the account a draft transfer pays from.

```csharp
var transfer = await OpenTransferScreenAsync();
var everyday = transfer.Accounts[EverydayAccountIndex];
var savings = transfer.Accounts[SavingsAccountIndex];

List<decimal> available = [];

using (transfer.WhenChanged(x => x.Draft.Source!.AvailableBalance).Subscribe(available.Add))
{
    transfer.Draft.Source = everyday;

    everyday.Balance = EverydayBalanceAfterRent;

    transfer.Draft.Source = savings;

    // The everyday account is no longer the source, so its balance is no longer observed.
    everyday.Balance = EverydayBalanceAfterBills;

    savings.Balance = SavingsBalanceAfterWithdrawal;

    Console.WriteLine(string.Join(", ", available));
}
```

```text
2950.75, 1750.75, 15230.00, 15000
```

## Read the sender with WhenAny

`WhenAny` hands its selector an observed change for each property instead of the bare value. An observed change carries the object that changed (`Sender`), the property value (`Value`) and the property expression (`Expression`). `WhenAny` takes one to twelve properties and always takes a selector. An observation that the generator wrote has no expression tree to report, so `Expression` is `null`.

```csharp
var item = new TodoItem { Id = 1, Title = ReviewPrTitle };

using var subscription = item.WhenAny(x => x.Title, static change => change)
    .Subscribe(static change => Console.WriteLine($"Item {change.Sender.Id} is now '{change.Value}'"));

item.Title = RenamedTitle;
```

```text
Item 1 is now 'Review PR'
Item 1 is now 'Review PR 204'
```

The next example prints whether the expression is `null`.

```csharp
var item = new TodoItem { Id = 1, Title = ReviewPrTitle };

using var subscription = item.WhenAny(x => x.Title, static change => change)
    .Subscribe(static change => Console.WriteLine(change.Expression is null));
```

```text
True
```

A selector can also read the value alone. This one turns the title into its length, as a character counter does.

```csharp
var item = CreateRegistrationTask();

using var subscription = item.WhenAny(x => x.Title, static title => title.Value.Length).Subscribe(Console.WriteLine);

item.Title = RenamedTitle;
```

```text
22
29
```

With several properties, the selector receives one observed change per property, in order. Read `Value` from each one.

```csharp
var item = CreateRegistrationTask();

using var subscription = item
    .WhenAny(x => x.Title, x => x.IsDone, x => x.Priority, static (_, isDone, priority) => isDone.Value ? TodoPriority.Low : priority.Value)
    .Subscribe(static priority => Console.WriteLine(priority));

item.Priority = TodoPriority.High;
item.IsDone = true;
```

```text
Normal
High
Low
```

**A selector that throws ends the stream with an error.** `Catch` can replace the error with a value, but the stream ends after it, so later changes are never delivered.

```csharp
var server = InMemoryGitHubServer.CreateSeeded();
IssueSearchViewModel viewModel = new(server, Webshop) { SearchTerm = FirstIssueNumber };

using var subscription = viewModel
    .WhenAny(x => x.SearchTerm, static term => int.Parse(term.Value, CultureInfo.InvariantCulture))
    .Catch<int, FormatException>(static _ => Signal.Return(NotANumber))
    .Subscribe(Console.WriteLine);

viewModel.SearchTerm = SecondIssueNumber;
viewModel.SearchTerm = "abc";

// The pipeline has ended, so this number is never delivered.
viewModel.SearchTerm = ThirdIssueNumber;
```

```text
100
101
-1
```

Handle the failure inside the selector when the stream has to keep going.

```csharp
var server = InMemoryGitHubServer.CreateSeeded();
IssueSearchViewModel viewModel = new(server, Webshop) { SearchTerm = FirstIssueNumber };

using var subscription = viewModel
    .WhenAny(x => x.SearchTerm, static term => int.TryParse(term.Value, CultureInfo.InvariantCulture, out var number) ? number : NotANumber)
    .Subscribe(Console.WriteLine);

viewModel.SearchTerm = SecondIssueNumber;
viewModel.SearchTerm = "abc";
viewModel.SearchTerm = ThirdIssueNumber;
```

```text
100
101
-1
102
```

## Work with an observed change

`ObservedChange<TSender, TValue>` is the class behind `IObservedChange<TSender, TValue>`. The generator creates one for each notification, and you can create one yourself with a sender, an expression and a value. The expression may be `null`.

```csharp
var item = new TodoItem { Id = 1, Title = ReviewPrTitle };
Expression<Func<TodoItem, string>> expression = x => x.Title;

var change = new ObservedChange<TodoItem, string>(item, expression.Body, item.Title);

Console.WriteLine(change.Sender.Title);
Console.WriteLine(change.Expression);
Console.WriteLine(change.Value);
```

```text
Review PR
x.Title
Review PR
```

Pass `null` for the expression when the observation needs no expression tree.

```csharp
var item = new TodoItem { Id = 1, Title = ReviewPrTitle };

var change = new ObservedChange<TodoItem, string>(item, null, item.Title);

Console.WriteLine(change.Sender.Title);
Console.WriteLine(change.Value);
Console.WriteLine(change.Expression is null);
```

```text
Review PR
Review PR
True
```

The helpers below read the expression, so they need one. They live in the `ReactiveUI.Binding.ObservableForProperty` namespace. Each reads properties by reflection and carries `RequiresUnreferencedCode`, so none is safe to trim. Read `change.Value` when you can.

**`GetPropertyName` names the property.** It joins every step of the path with dots.

```csharp
var issue = new Issue { Number = CheckoutNumber, Title = CheckoutTitle, Assignee = new User { Login = "priya-nair" } };
Expression<Func<Issue, string>> title = x => x.Title;
Expression<Func<Issue, string>> assigneeLogin = x => x.Assignee!.Login;

ObservedChange<Issue, string> titleChange = new(issue, title.Body, issue.Title);
ObservedChange<Issue, string> loginChange = new(issue, assigneeLogin.Body, issue.Assignee!.Login);

Console.WriteLine(titleChange.GetPropertyName());
Console.WriteLine(loginChange.GetPropertyName());
```

```text
Title
Assignee.Login
```

**`GetValue` reads the value.** It returns the value the change carries. When the change carries the default value, it reads the value from the sender through the expression.

```csharp
var item = new TodoItem { Id = 1, Title = ReviewPrTitle };
Expression<Func<TodoItem, string>> expression = x => x.Title;

ObservedChange<TodoItem, string> carried = new(item, expression.Body, RenamedTitle);
ObservedChange<TodoItem, string> empty = new(item, expression.Body, default!);

Console.WriteLine(carried.GetValue());
Console.WriteLine(empty.GetValue());
```

```text
Review PR 204
Review PR
```

When an object in the middle of the path is null, `GetValue` throws an `InvalidOperationException` and `GetValueOrDefault` returns the default.

```csharp
var issue = new Issue { Number = CheckoutNumber, Title = CheckoutTitle, Assignee = null };
Expression<Func<Issue, string>> assigneeLogin = x => x.Assignee!.Login;

ObservedChange<Issue, string> change = new(issue, assigneeLogin.Body, default!);

Console.WriteLine(change.GetValueOrDefault() is null);

try
{
    Console.WriteLine(change.GetValue());
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}
```

```text
True
One of the properties in the expression 'Assignee.Login' was null
```

**`Value()` turns a stream of changes into a stream of values.** It applies the same read to each change. 

```csharp
var item = new TodoItem { Id = 1, Title = ReviewPrTitle };

using var subscription = item.WhenAny(x => x.Title, static change => change)
    .Value()
    .Subscribe(Console.WriteLine);

item.Title = RenamedTitle;
```

```text
Review PR
Review PR 204
```

## Read several values at once

An emission with two to sixteen properties is a `PropertyValues<T1, ..., T16>` with members `Property1` to `Property16`. It is a read-only record struct, so it supports deconstruction, value equality, `with` copies and a readable `ToString`.

The examples read the first emission with `FirstAsync()`, which returns a `Task` that completes with the first value. Because the stream delivers the current values on subscription, that value is the current state.

**Read the members and deconstruct.** Deconstruction returns one variable per member.

```csharp
var item = CreateRegistrationTask();

var values = await item.WhenChanged(x => x.Title, x => x.IsDone, x => x.Priority).FirstAsync();

Console.WriteLine(values.Property1);
Console.WriteLine(values.Property2);
Console.WriteLine(values.Property3);

var (title, isDone, priority) = values;

Console.WriteLine(values.Equals(new(title, isDone, priority)));
```

```text
Renew car registration
False
Normal
True
```

**Read in a subscription.** A subscriber reads the members it needs from each emission.

```csharp
var item = CreateRegistrationTask();
List<string> lines = [];

using (item
    .WhenChanged(x => x.Title, x => x.IsDone, x => x.Priority)
    .Subscribe(values => lines.Add($"{values.Property1} ({values.Property3}, done: {values.Property2})")))
{
    item.Priority = TodoPriority.High;
}

Console.WriteLine(string.Join(", ", lines));
```

```text
Renew car registration (Normal, done: False), Renew car registration (High, done: False)
```

**Compare emissions.** Two emissions are equal when every member is equal, and equal emissions have the same hash code.

```csharp
var first = CreateRegistrationTask();
var second = CreateRegistrationTask();

var left = await first.WhenChanged(x => x.Title, x => x.IsDone).FirstAsync();
var right = await second.WhenChanged(x => x.Title, x => x.IsDone).FirstAsync();
var boxed = (object)right;

Console.WriteLine(left == right);
Console.WriteLine(left != right);
Console.WriteLine(left.Equals(right));
Console.WriteLine(left.Equals(boxed));
Console.WriteLine(left.GetHashCode() == right.GetHashCode());

second.IsDone = true;
var finished = await second.WhenChanged(x => x.Title, x => x.IsDone).FirstAsync();

Console.WriteLine(left == finished);
```

```text
True
False
True
True
True
False
```

**Copy with one member replaced.** `with` leaves the original unchanged.

```csharp
var item = CreateRegistrationTask();

var original = await item.WhenChanged(x => x.Title, x => x.IsDone).FirstAsync();
var finished = original with { Property2 = true };

Console.WriteLine(original.Property2);
Console.WriteLine(finished.Property2);
```

```text
False
True
```

**Print an emission.** `ToString` names each member.

```csharp
var item = CreateRegistrationTask();

var values = await item.WhenChanged(x => x.Title, x => x.IsDone).FirstAsync();

Console.WriteLine($"Emission: {values}.");
```

```text
Emission: PropertyValues { Property1 = Renew car registration, Property2 = False }.
```

An emission holds up to sixteen properties and follows the same pattern at every size: `Property1` to `Property16`, deconstruction into one variable each, and equality over every member. `PropertyValuesExamples` in the example project has one method for each size from two to sixteen.

`PropertyValuesComparisonExamples` compares and prints emissions of each size. The first prints an emission of two properties.

```csharp
var form = CreateTransferForm();

var values = await form.WhenChanged(x => x.Amount, x => x.Reference).FirstAsync();

var text = values.ToString();

Console.WriteLine($"Emission: {text}.");
```

```text
Emission: PropertyValues { Property1 = 250, Property2 = Rent March }.
```

Each larger size copies the emission, changes `Property1`, and checks equality, the hash code and the text. This proves that an emission behaves the same at every size. The first example below uses three properties.

```csharp
var form = CreateTransferForm();

var values = await form
    .WhenChanged(
        x => x.Amount,
        x => x.Reference,
        x => x.SourceId)
    .FirstAsync();
var copy = values with { };
var edited = values with { Property1 = default };

Console.WriteLine(values == copy);
Console.WriteLine(values != edited);
Console.WriteLine(values.Equals((object)copy));
Console.WriteLine(values.GetHashCode() == copy.GetHashCode());
Console.WriteLine(values.ToString().Contains("Property3 = "));
```

```text
True
True
True
True
True
```

The larger sizes run the same five checks on more properties, up to sixteen.

## Follow a property that holds a stream

Some properties hold a stream. The issue feed in the example has one property for each kind of activity, and every property is replaced when the user selects another repository. `WhenAnyObservable` subscribes to the stream a property holds. When the property changes, it drops the old stream and follows the new one.

```csharp
using RepositoryChannels webshop = new(_webshop);
RepositoryFeedViewModel feed = new();
feed.Follow(webshop);

using var subscription = feed.WhenAnyObservable(x => x.IssueOpened).Subscribe(Console.WriteLine);

webshop.IssueOpened.OnNext(IssueOpenedEvent);
```

```text
#101 opened: Checkout button unresponsive
```

Selecting another repository replaces the stream. The first repository loses its subscriber, and the second gains one.

```csharp
using RepositoryChannels webshop = new(_webshop);
using RepositoryChannels mobileApp = new(_mobileApp);
RepositoryFeedViewModel feed = new();
feed.Follow(webshop);

using var subscription = feed.WhenAnyObservable(x => x.IssueOpened).Subscribe(Console.WriteLine);

Console.WriteLine(webshop.IssueOpened.HasObservers);

feed.Follow(mobileApp);

Console.WriteLine(webshop.IssueOpened.HasObservers);
Console.WriteLine(mobileApp.IssueOpened.HasObservers);

webshop.IssueOpened.OnNext(StaleEvent);
mobileApp.IssueOpened.OnNext(MobileAppEvent);
```

```text
True
False
True
#7 opened: App crashes on start
```

A property that holds no stream delivers nothing and keeps no subscription.

```csharp
using RepositoryChannels webshop = new(_webshop);
RepositoryFeedViewModel feed = new();
feed.Follow(webshop);

using var subscription = feed.WhenAnyObservable(x => x.IssueOpened).Subscribe(Console.WriteLine);

feed.Unfollow();

Console.WriteLine(webshop.IssueOpened.HasObservers);

webshop.IssueOpened.OnNext(StaleEvent);

feed.Follow(webshop);
webshop.IssueOpened.OnNext(IssueOpenedEvent);
```

```text
False
#101 opened: Checkout button unresponsive
```

**Name several properties to merge their streams.** `WhenAnyObservable` takes one to twelve properties that hold streams of the same element type. The result delivers the values of all of them in the order they arrive.

```csharp
using RepositoryChannels webshop = new(_webshop);
RepositoryFeedViewModel feed = new();
feed.Follow(webshop);

using var subscription = feed
    .WhenAnyObservable(x => x.IssueOpened, x => x.IssueClosed)
    .Subscribe(Console.WriteLine);

webshop.IssueClosed.OnNext(IssueClosedEvent);
webshop.IssueOpened.OnNext(IssueOpenedEvent);
```

```text
#98 closed: Cart total rounds down
#101 opened: Checkout button unresponsive
```

Only the streams you name receive a subscriber. The repository in the example has more streams than the two named here.

```csharp
using RepositoryChannels webshop = new(_webshop);
RepositoryFeedViewModel feed = new();
feed.Follow(webshop);

using var subscription = feed.WhenAnyObservable(x => x.IssueOpened, x => x.ReleasePublished).Subscribe(Console.WriteLine);

Console.WriteLine(webshop.SubscribedStreamCount);
```

```text
2
```

The twelve-stream form merges the issue, comment, milestone, pull request, review and release streams in one call. `MergeThreeStreams` to `MergeTwelveStreams` in the example project add one property at a time.

**Add a selector to combine the latest values.** With a selector as the last argument, the result delivers the selector's result each time one stream delivers. It starts once every stream has delivered a value. `PublishNotice` sends one announcement on every stream, so each stream has a value before the closing event arrives.

```csharp
using RepositoryChannels webshop = new(_webshop);
RepositoryFeedViewModel feed = new();
feed.Follow(webshop);

using var subscription = feed
    .WhenAnyObservable(x => x.IssueOpened, x => x.IssueClosed, static (first, last) => $"{first} / {last}")
    .Subscribe(Console.WriteLine);

webshop.PublishNotice(MaintenanceNotice);
webshop.IssueClosed.OnNext(IssueClosedEvent);
```

```text
Maintenance starts at 22:00 UTC / Maintenance starts at 22:00 UTC
Maintenance starts at 22:00 UTC / #98 closed: Cart total rounds down
```

## Shape a stream of property changes with Primitives operators

The streams that observation returns are ordinary `IObservable<T>` streams, so any operator works on them. The operators come from [ReactiveUI.Primitives](../primitives/index.md), the package that `ReactiveUI.Binding` builds on. Add `using ReactiveUI.Primitives;` to use them.

Primitives gives some operators two names. The examples use `Throttle`, `Sample`, `Scan` and `Window`. [Time operators](../primitives/time.md) and [aggregation](../primitives/aggregation.md) list both names of each.

Two ideas recur in the examples:

- **The first value is the current value.** `WhenChanged` delivers the current value when you subscribe. `Skip(1)` drops it when you want changes only.
- **A time operator takes a sequencer.** A sequencer decides when and where work runs. The examples pass a `VirtualClock`, a sequencer that moves only when you call `AdvanceBy`, so each example runs at once and prints the same output every time. Leave the argument out and the operator measures time on a thread pool timer, and delivers on that timer's thread. `WitnessOn` below moves values to a sequencer such as the UI thread. [Scheduling](../primitives/scheduling.md) covers sequencers.

To share one subscription among several subscribers, or to keep the latest value for a subscriber that joins late, use the sharing operators in [Sharing](../primitives/sharing.md). To defer building a stream until someone subscribes, use `Lazy` in [Creation factories](../primitives/creation-factories.md).

### Build a search box

A search box is the classic shaped stream. The user types, and a search should run once for each settled term. The view model in the example chains five operators on the term.

- `Throttle` delivers a term only after the user stops typing for the quiet period.
- `Select` trims each term.
- `DistinctUntilChanged` drops a term that equals the term before it.
- `Where` drops a blank term.
- `SwitchMap` starts a search for each term and drops the search that is waiting when a newer term arrives. A slow response to an older term cannot overwrite the results of a newer one.

The method below is the whole pipeline. It returns the subscription, so the caller can dispose it to stop the searching.

```csharp
public IDisposable StartSearching(ISequencer sequencer, TimeSpan quietPeriod) =>
    this.WhenAnyValue(x => x.SearchTerm)
        .Throttle(quietPeriod, sequencer)
        .Select(static term => term.Trim())
        .DistinctUntilChanged()
        .Where(static term => term.Length > 0)
        .SwitchMap(BeginSearch)
        .Subscribe(EndSearch);
```

The first three operators on their own show what the pipeline does to typed text. Padding and blank input start no search, and the same term twice starts one search.

```csharp
const string PaddedCheckout = "  checkout ";

var server = InMemoryGitHubServer.CreateSeeded();
IssueSearchViewModel viewModel = new(server, Webshop);

using var subscription = viewModel.WhenChanged(x => x.SearchTerm)
    .Select(static term => term.Trim())
    .DistinctUntilChanged()
    .Where(static term => term.Length > 0)
    .Subscribe(Console.WriteLine);

viewModel.SearchTerm = PaddedCheckout;
viewModel.SearchTerm = CheckoutTerm;
viewModel.SearchTerm = "   ";
viewModel.SearchTerm = GiftTerm;
```

```text
checkout
gift
```

`Throttle` needs time to pass. This example uses a `VirtualClock`, so every key press restarts the wait and only a pause delivers a term.

```csharp
VirtualClock clock = new();
var server = InMemoryGitHubServer.CreateSeeded();
IssueSearchViewModel viewModel = new(server, Webshop);

using var subscription = viewModel.WhenAnyValue(x => x.SearchTerm)
    .Throttle(_quietPeriod, clock)
    .Subscribe(Console.WriteLine);

viewModel.SearchTerm = "c";
clock.AdvanceBy(_keyGap);
viewModel.SearchTerm = "cr";
clock.AdvanceBy(_keyGap);
viewModel.SearchTerm = CraTerm;

// Every key press restarts the wait, so nothing has settled yet.
clock.AdvanceBy(_keyGap);
Console.WriteLine("Still typing");

clock.AdvanceBy(_quietPeriod);

viewModel.SearchTerm = CrashTerm;
clock.AdvanceBy(_quietPeriod);
```

```text
Still typing
cra
crash
```

The whole view model searches once for each settled term and shows the matches. The request counter shows that a repeated term sends no request. The example uses the real clock, so it waits a short time for each search to finish.

```csharp
var server = await CreateSignedInServerAsync();
IssueSearchViewModel viewModel = new(server, Webshop);

using var subscription = viewModel.StartSearching(Sequencer.Default, _quietPeriod);

var quotaAtStart = server.RateLimitRemaining;

viewModel.SearchTerm = "  checkout ";
await Task.Delay(_settle);

Console.WriteLine(string.Join(", ", TitlesOf(viewModel.Results)));
Console.WriteLine(viewModel.IsSearching);
Console.WriteLine(quotaAtStart - server.RateLimitRemaining);

// The same term without its padding trims to the term already searched, so no request goes out.
viewModel.SearchTerm = CheckoutTerm;
await Task.Delay(_settle);
Console.WriteLine(quotaAtStart - server.RateLimitRemaining);

viewModel.SearchTerm = "GIFT";
await Task.Delay(_settle);

Console.WriteLine(string.Join(", ", TitlesOf(viewModel.Results)));
Console.WriteLine(quotaAtStart - server.RateLimitRemaining);
```

```text
Checkout button unresponsive on Safari
False
1
1
Add gift-card support
2
```

`SwitchMap` protects the results from a slow response. The first search below takes longer than the second, and its response is dropped when it arrives.

```csharp
var server = await CreateSignedInServerAsync();
IssueSearchViewModel viewModel = new(server, Webshop);

using var subscription = viewModel.StartSearching(Sequencer.Default, _quietPeriod);

// The first response takes longer than the second.
server.Latency = _slowResponse;
viewModel.SearchTerm = CheckoutTerm;
await Task.Delay(_settle);

server.Latency = TimeSpan.Zero;
viewModel.SearchTerm = GiftTerm;
await Task.Delay(_settle);

Console.WriteLine(string.Join(", ", TitlesOf(viewModel.Results)));
Console.WriteLine(viewModel.IsSearching);

// The response for "checkout" arrives now and is dropped.
await Task.Delay(_slowResponse);

Console.WriteLine(string.Join(", ", TitlesOf(viewModel.Results)));
```

```text
Add gift-card support
False
Add gift-card support
```

The same pause works on a control. This example copies the amount box into the draft once the customer stops typing, without a two-way binding.

```csharp
VirtualClock clock = new();
TransferViewModel screen = new(new InMemoryBankingBackend());
TransferView view = new() { ViewModel = screen };

using var subscription = view.AmountTextBox.WhenChanged(x => x.Text)
    .Where(static text => !string.IsNullOrEmpty(text))
    .Throttle(_quietPeriod, clock)
    .Subscribe(text => screen.Draft.Amount = decimal.Parse(text, CultureInfo.InvariantCulture));

view.AmountTextBox.Text = "1";
clock.AdvanceBy(_keyGap);
view.AmountTextBox.Text = "12";
clock.AdvanceBy(_keyGap);
view.AmountTextBox.Text = "125";

Console.WriteLine(screen.Draft.Amount);

clock.AdvanceBy(_quietPeriod);

Console.WriteLine(screen.Draft.Amount);
```

```text
0
125
```

`BindSearchStateToView` in the example project binds the busy flag and the results to a view with the APIs that [Bindings](bindings.md) covers.

### Slow a fast stream

`Throttle` waits for a quiet moment. It delivers a value only after no newer value has arrived for the period you give, so every edit restarts the wait. The notes below save only after the user stops typing for 300 ms.

```csharp
VirtualClock clock = new();
TodoItem item = new();

using var subscription = item.WhenChanged(x => x.Notes)
    .Skip(1)
    .Throttle(_quietPeriod, clock)
    .Subscribe(static notes => Console.WriteLine($"Saved: {notes}"));

item.Notes = FirstNote;
clock.AdvanceBy(_keyGap);
item.Notes = SecondNote;
clock.AdvanceBy(_keyGap);
item.Notes = ThirdNote;
clock.AdvanceBy(_quietPeriod);
```

```text
Saved: Bring the plates, the receipt and a pen
```

`Sample` delivers the newest value on a fixed beat instead. Use it for a slider that redraws a preview while the user drags. A quiet period sends nothing.

```csharp
VirtualClock clock = new();
TransferDraft draft = new();

using var subscription = draft.WhenChanged(x => x.Amount)
    .Skip(1)
    .Sample(_sampleInterval, clock)
    .Subscribe(static amount => Console.WriteLine($"Preview {amount}"));

draft.Amount = FirstAmount;
draft.Amount = SecondAmount;
clock.AdvanceBy(_sampleInterval);
draft.Amount = ThirdAmount;
draft.Amount = LastAmount;
clock.AdvanceBy(_sampleInterval);
clock.AdvanceBy(_sampleInterval);
```

```text
Preview 20
Preview 40
```

### Collect changes into lists with Buffer

`Buffer` gathers values into lists. Give it a count, and it delivers a list each time that many values have arrived. The fourth reference below waits for a list that is not full.

```csharp
TransferDraft draft = new();

using var subscription = draft.WhenChanged(x => x.Reference)
    .Skip(1)
    .Buffer(EditsPerList)
    .Subscribe(static edits => Console.WriteLine(string.Join(Separator, edits)));

draft.Reference = FirstReference;
draft.Reference = SecondReference;
draft.Reference = ThirdReference;
draft.Reference = FourthReference;
```

```text
Rent | Rent March | Rent March 2026
```

A second count is the step: it starts a new list after that many values, so lists overlap. With a step of one, each list holds the last three edits.

```csharp
TransferDraft draft = new();

using var subscription = draft.WhenChanged(x => x.Reference)
    .Skip(1)
    .Buffer(EditsPerList, EditsBetweenLists)
    .Subscribe(static edits => Console.WriteLine(string.Join(Separator, edits)));

draft.Reference = FirstReference;
draft.Reference = SecondReference;
draft.Reference = ThirdReference;
draft.Reference = FourthReference;
```

```text
Rent | Rent March | Rent March 2026
Rent March | Rent March 2026 | Rent April 2026
```

A time period delivers one list for each period. The sequencer measures the period, so the example moves the clock to end each list.

```csharp
VirtualClock clock = new();
TransferDraft draft = new();

using var subscription = draft.WhenChanged(x => x.Reference)
    .Skip(1)
    .Buffer(_bufferLength, clock)
    .Subscribe(static edits => Console.WriteLine($"Batch of {edits.Count}: {string.Join(Separator, edits)}"));

draft.Reference = FirstReference;
draft.Reference = SecondReference;
clock.AdvanceBy(_bufferLength);
draft.Reference = ThirdReference;
clock.AdvanceBy(_bufferLength);
```

```text
Batch of 2: Rent | Rent March
Batch of 1: Rent March 2026
```

### Cut a stream into slices

`Slice` cuts a stream into slices. Each slice is itself a stream that delivers the values of that slice. `Window` is the same operator under another name. Because a slice is a stream, you decide what to do with it: `ToList` collects it into a list, and `Count` counts it. `SelectMany` flattens the result back into one stream.

A slice can end on a count, on time, on another stream, or on a mix of them.

| Call | The slice ends |
| --- | --- |
| `Slice(count)` | After that many values |
| `Slice(count, skip)` | After `count` values; a new slice starts every `skip` values, so slices overlap or leave gaps |
| `Slice(timeSpan, sequencer)` | After the time span |
| `Slice(timeSpan, timeShift, sequencer)` | After the time span; a new slice starts every `timeShift` |
| `Slice(timeSpan, count, sequencer)` | After the time span or the count, whichever comes first |
| `Slice(boundaries)` | Each time the boundary stream delivers |
| `Slice(closingSelector)` | When the stream that the method returns delivers |
| `Slice(openings, closingSelector)` | A slice opens for each opening value and ends with the stream the method returns for it |

The examples below show each form in turn.

**Slice by count.** Three edits make a slice. The fourth edit waits in a slice that is not full, so it is not delivered.

```csharp
TodoItem item = new() { Title = DraftTitle };

using var subscription = item.WhenChanged(x => x.Title)
    .Skip(1)
    .Slice(EditsPerSlice)
    .SelectMany(static slice => slice.ToList())
    .Subscribe(static edits => Console.WriteLine(string.Join(Separator, edits)));

item.Title = FirstTitle;
item.Title = SecondTitle;
item.Title = ThirdTitle;
item.Title = FourthTitle;
```

```text
Renew, Renew car, Renew car registration
```

**Slice by count with a step.** Slices of two that start after every edit pair each edit with the one before it. This example keeps the current value, so the first pair holds the title the task started with.

```csharp
TodoItem item = new() { Title = DraftTitle };

using var subscription = item.WhenChanged(x => x.Title)
    .Slice(EditsPerPair, EditsBetweenSlices)
    .SelectMany(static slice => slice.ToList())
    .Subscribe(static pair => Console.WriteLine(string.Join(" -> ", pair)));

item.Title = FirstTitle;
item.Title = SecondTitle;
item.Title = ThirdTitle;
```

```text
Draft -> Renew
Renew -> Renew car
Renew car -> Renew car registration
```

**Slice by time.** A time span ends each slice after that long. The sequencer measures the span, so the example moves the clock to end each slice.

```csharp
VirtualClock clock = new();
TransferDraft draft = new();

using var subscription = draft.WhenChanged(x => x.Amount)
    .Skip(1)
    .Slice(_sliceLength, clock)
    .SelectMany(static slice => slice.ToList())
    .Subscribe(static amounts => Console.WriteLine(string.Join(Separator, amounts)));

draft.Amount = FirstAmount;
clock.AdvanceBy(_editGap);
draft.Amount = SecondAmount;
clock.AdvanceBy(_sliceLength);
draft.Amount = ThirdAmount;
clock.AdvanceBy(_sliceLength);
```

```text
10, 20
30
```

**Slice by time or count.** A slice ends after the time span or after the count, whichever comes first. The first slice below fills up after three edits, and the fourth edit waits for the time span.

```csharp
VirtualClock clock = new();
TransferDraft draft = new();

using var subscription = draft.WhenChanged(x => x.Amount)
    .Skip(1)
    .Slice(_sliceLength, EditsPerSlice, clock)
    .SelectMany(static slice => slice.ToList())
    .Subscribe(static amounts => Console.WriteLine(string.Join(Separator, amounts)));

draft.Amount = FirstAmount;
draft.Amount = SecondAmount;
draft.Amount = ThirdAmount;
draft.Amount = FirstAmount + ThirdAmount;
clock.AdvanceBy(_sliceLength);
```

```text
10, 20, 30
40
```

**Slice with overlapping time.** A second time span sets how often a new slice starts. Each slice lasts the first span, so slices overlap and an edit can land in two of them.

```csharp
VirtualClock clock = new();
TransferDraft draft = new();

using var subscription = draft.WhenChanged(x => x.Amount)
    .Skip(1)
    .Slice(_sliceLength, _sliceShift, clock)
    .SelectMany(static slice => slice.ToList())
    .Subscribe(static amounts => Console.WriteLine(string.Join(Separator, amounts)));

clock.AdvanceBy(_editGap);
draft.Amount = FirstAmount;
clock.AdvanceBy(_editGap);
draft.Amount = SecondAmount;
clock.AdvanceBy(_sliceLength);
```

```text
10, 20
20
```

**Slice on another stream.** Give `Slice` a stream of boundaries. Each time it delivers, the current slice ends and the next one starts. `Signal.Every` delivers on a fixed period, like an autosave timer.

```csharp
VirtualClock clock = new();
TodoItem item = new();

using var subscription = item.WhenChanged(x => x.Notes)
    .Skip(1)
    .Slice(Signal.Every(_autosaveInterval, clock))
    .SelectMany(static slice => slice.ToList())
    .Subscribe(static notes => Console.WriteLine(string.Join(Separator, notes)));

item.Notes = "Bring the old plates";
item.Notes = "Bring the old plates and the receipt";
clock.AdvanceBy(_autosaveInterval);
item.Notes = "Bring the plates, the receipt and a pen";
clock.AdvanceBy(_autosaveInterval);
```

```text
Bring the old plates, Bring the old plates and the receipt
Bring the plates, the receipt and a pen
```

**Slice until a stream fires.** Give `Slice` a method that returns the closing stream. It ends each slice with the stream it returns and starts the next slice. Here a tick of the checkbox ends a slice.

```csharp
TodoItem item = new() { Title = DraftTitle };

using var subscription = item.WhenChanged(x => x.Title)
    .Skip(1)
    .Slice(() => item.WhenChanged(x => x.IsDone).Skip(1))
    .SelectMany(static slice => slice.ToList())
    .Subscribe(static titles => Console.WriteLine(string.Join(Separator, titles)));

item.Title = FirstTitle;
item.Title = SecondTitle;
item.IsDone = true;
item.Title = ThirdTitle;
item.IsDone = false;
```

```text
Renew, Renew car
Renew car registration
```

**Slice while a condition holds.** Give `Slice` an opening stream and a method that returns the closing stream for each opening. Edits outside a slice are dropped. Here the slice opens when the task becomes urgent and closes when it stops being urgent.

```csharp
TodoItem item = new() { Title = DraftTitle };
var becameUrgent = item.WhenChanged(x => x.Priority).Where(static priority => priority == TodoPriority.High);

using var subscription = item.WhenChanged(x => x.Title)
    .Skip(1)
    .Slice(becameUrgent, _ => item.WhenChanged(x => x.Priority).Where(static priority => priority != TodoPriority.High))
    .SelectMany(static slice => slice.ToList())
    .Subscribe(static titles => Console.WriteLine(string.Join(Separator, titles)));

item.Title = FirstTitle;
item.Priority = TodoPriority.High;
item.Title = SecondTitle;
item.Title = ThirdTitle;
item.Priority = TodoPriority.Normal;
item.Title = FourthTitle;
```

```text
Renew car, Renew car registration
```

### Count changes in a window

`Window` takes the same arguments as `Slice`. A time window opens even when nothing happens in it, so a quiet window counts zero.

```csharp
VirtualClock clock = new();
TodoItem item = new();

using var subscription = item.WhenChanged(x => x.Notes)
    .Skip(1)
    .Window(_windowLength, clock)
    .SelectMany(static window => window.Count())
    .Subscribe(static count => Console.WriteLine($"Edits in the window: {count}"));

item.Notes = FirstNote;
item.Notes = SecondNote;
item.Notes = ThirdNote;
clock.AdvanceBy(_windowLength);
item.Notes = FirstNote;
clock.AdvanceBy(_windowLength);
clock.AdvanceBy(_windowLength);
```

```text
Edits in the window: 3
Edits in the window: 1
Edits in the window: 0
```

A method that returns a closing stream ends each window in the same way. Here a change of the checkbox ends the window, and the example counts the edits made before it.

```csharp
TodoItem item = new();

using var subscription = item.WhenChanged(x => x.Notes)
    .Skip(1)
    .Window(() => item.WhenChanged(x => x.IsDone).Skip(1))
    .SelectMany(static window => window.Count())
    .Subscribe(static count => Console.WriteLine($"Edits before the checkbox changed: {count}"));

item.Notes = FirstNote;
item.Notes = SecondNote;
item.IsDone = true;
item.Notes = ThirdNote;
item.IsDone = false;
```

```text
Edits before the checkbox changed: 2
Edits before the checkbox changed: 1
```

### Keep a running value with Scan

`Scan` folds each value into a running value and delivers the running value after every value. The first argument is the starting value, and the second is a lambda that combines the running value with the next one.

```csharp
TodoItem item = new();

using var subscription = item.WhenChanged(x => x.Notes)
    .Skip(1)
    .Scan(0, static (count, _) => count + 1)
    .Subscribe(static count => Console.WriteLine($"Edits: {count}"));

item.Notes = FirstNote;
item.Notes = SecondNote;
item.Notes = ThirdNote;
```

```text
Edits: 1
Edits: 2
Edits: 3
```

The next example keeps the highest amount the customer has typed. This stream keeps its current value, so the first delivery is the starting value.

```csharp
TransferDraft draft = new();

using var subscription = draft.WhenChanged(x => x.Amount)
    .Scan(0M, Math.Max)
    .Subscribe(static highest => Console.WriteLine($"Highest: {highest}"));

draft.Amount = FirstAmount;
draft.Amount = SecondAmount;
draft.Amount = ThirdAmount;
```

```text
Highest: 0
Highest: 250
Highest: 250
Highest: 400
```

### Group changes by key

`GroupBy` splits a stream into one stream for each key. A group arrives when its first value arrives, and it keeps delivering the values with that key. `ObserveEdits` in the example merges the title edits and the notes edits into one stream of field and value pairs. The example below counts the edits of each field with `Scan`, one group per field.

```csharp
TodoItem item = new();

using var subscription = ObserveEdits(item)
    .GroupBy(static edit => edit.Field, static edit => edit.Value)
    .SelectMany(static group => group.Scan(0, static (count, _) => count + 1).Select(count => $"{group.Key} edit {count}"))
    .Subscribe(Console.WriteLine);

item.Title = FirstTitle;
item.Notes = FirstNote;
item.Title = SecondTitle;
item.Notes = SecondNote;
```

```text
Title edit 1
Notes edit 1
Title edit 2
Notes edit 2
```

A key comparer decides which values share a group, and an initial capacity sizes the table of groups. Here the comparer ignores case, so "Car" and "car" are one tag. The group keeps the spelling of its first value.

```csharp
TodoItem item = new();

using var subscription = item.WhenChanged(x => x.Tags)
    .Skip(1)
    .SelectMany(static tags => tags)
    .GroupBy(static tag => tag, ExpectedTagCount, StringComparer.OrdinalIgnoreCase)
    .SelectMany(static group => group.Scan(0, static (count, _) => count + 1).Select(count => $"{group.Key}: {count}"))
    .Subscribe(Console.WriteLine);

item.Tags = ["Car", "Errands"];
item.Tags = ["car", "admin"];
```

```text
Car: 1
Errands: 1
Car: 2
admin: 1
```

`GroupByUntil` also ends each group. It takes a method that returns the closing stream for a group, and the next value with that key opens a new group. Here a group ends when its field has stayed quiet for 300 ms, and the example reports what was typed in it.

```csharp
VirtualClock clock = new();
TodoItem item = new();

using var subscription = ObserveEdits(item)
    .GroupByUntil(static edit => edit.Field, static edit => edit.Value, group => group.Throttle(_quietPeriod, clock))
    .SelectMany(static group => group.ToList().Select(values => $"{group.Key}: {string.Join(Separator, values)}"))
    .Subscribe(Console.WriteLine);

item.Title = FirstTitle;
clock.AdvanceBy(_editGap);
item.Notes = FirstNote;
clock.AdvanceBy(_editGap);
item.Title = SecondTitle;
clock.AdvanceBy(_quietPeriod);
item.Notes = SecondNote;
clock.AdvanceBy(_quietPeriod);
```

```text
Notes: Bring the old plates
Title: Renew, Renew car registration
Notes: Bring the plates and the receipt
```

### Deliver on a sequencer

`WitnessOn` delivers each value on a sequencer that you name, in order. The example uses a `VirtualClock` as the UI thread. The three amounts wait in its queue, and `Start` runs the queue, so all three previews print after the typing.

```csharp
VirtualClock uiThread = new();
TransferDraft draft = new();

using var subscription = draft.WhenChanged(x => x.Amount)
    .Skip(1)
    .WitnessOn(uiThread)
    .Subscribe(static amount => Console.WriteLine($"Preview {amount}"));

draft.Amount = FirstAmount;
draft.Amount = SecondAmount;
draft.Amount = ThirdAmount;
Console.WriteLine("Typed three amounts");
uiThread.Start();
```

```text
Typed three amounts
Preview 10
Preview 20
Preview 30
```

`WitnessLatestOn` delivers on the sequencer and keeps only the newest value that is waiting. Use it when the consumer needs the latest state and not every step, such as a preview that redraws. The same three amounts produce one preview.

```csharp
VirtualClock uiThread = new();
TransferDraft draft = new();

using var subscription = draft.WhenChanged(x => x.Amount)
    .Skip(1)
    .WitnessLatestOn(uiThread)
    .Subscribe(static amount => Console.WriteLine($"Preview {amount}"));

draft.Amount = FirstAmount;
draft.Amount = SecondAmount;
draft.Amount = ThirdAmount;
Console.WriteLine("Typed three amounts");
uiThread.Start();
```

```text
Typed three amounts
Preview 30
```

Bindings use the same rule for view writes: only the latest value waits. [Threading and platforms](threading.md) explains it.

## Observe an expression built at run time

The APIs above read property paths that you write in your code. Sometimes a program builds the path itself, for example from a column name the user picks. Then the path is an `Expression` value made at run time, and the generator has nothing to read. The three APIs in this section take such expressions.

They read the chain by reflection and carry `RequiresUnreferencedCode`, so none is safe to trim. They use the observation providers registered in the service locator, so register the providers first. [Setup](setup.md) covers the builder, and [Mechanisms](mechanisms.md) covers providers. Prefer `WhenChanged` whenever the path is known when you write the code.

```csharp
IReactiveUIBindingBuilder builder = RxBindingBuilder.CreateReactiveUIBindingBuilder();

_ = builder.WithCoreServices().BuildApp();
```

### WhenAnyDynamic

`WhenAnyDynamic` takes one to twelve chains and a selector that receives an observed change for each chain. It works like `WhenAny` for chains built at run time. A final `bool` argument sets `isDistinct`. It is `true` by default, so a chain reports only when its value changes. Pass `false` to report every notification.

The first example observes the amount of a transfer form. The chain is built with `Expression.Property`, so the compiler never sees a lambda, and the selector turns each observed change into text.

```csharp
var form = CreateTransferForm();
var root = Expression.Parameter(typeof(TransferForm), "x");

using (form
    .WhenAnyDynamic(
        Expression.Property(root, nameof(TransferForm.Amount)),
        static amount => $"{amount.Value}")
    .Subscribe(Console.WriteLine))
{
    form.Amount = RevisedAmount;
}
```

```text
250
300
```

The second example observes two chains, the amount and the reference, and passes `false` for `isDistinct`. The selector receives one observed change for each chain and joins their values.

```csharp
var form = CreateTransferForm();
var root = Expression.Parameter(typeof(TransferForm), "x");

using (form
    .WhenAnyDynamic(
        Expression.Property(root, nameof(TransferForm.Amount)),
        Expression.Property(root, nameof(TransferForm.Reference)),
        static (amount, reference) => $"{amount.Value} {reference.Value}",
        false)
    .Subscribe(Console.WriteLine))
{
    form.Amount = RevisedAmount;
}
```

```text
250 Rent March
300 Rent March
```

### ObservableForProperty

`ObservableForProperty` observes one property and delivers an observed change for each change. Pass a lambda, or pass the property name as a string. Your editor does not list it, so type the call yourself. If no registered provider can observe the property, it throws an `InvalidOperationException`.

Three arguments control the observation. `SubscribeToExpressionChain`, described below, adds a fourth. Each short overload leaves out the arguments it fixes at the default.

| Argument | Meaning | Default |
| --- | --- | --- |
| `beforeChange` | Deliver just before the property changes | `false` |
| `skipInitial` | Skip the current value that arrives on subscription | `true` |
| `isDistinct` | Drop a value equal to the one before it | `true` |
| `suppressWarnings` | Observe a property that cannot notify without a warning; only `SubscribeToExpressionChain` takes it | `false` |

The default skips the current value, so only changes arrive. The example below observes the amount by lambda and prints only the value after the change.

```csharp
TransferForm form = new() { Amount = RentAmount };

using (form.ObservableForProperty(x => x.Amount).Subscribe(static change => Console.WriteLine(change.Value)))
{
    form.Amount = RevisedAmount;
}
```

```text
300
```

Pass `true` for all three arguments to receive the value before each change.

```csharp
TransferForm form = new() { Amount = RentAmount };

using (form.ObservableForProperty(x => x.Amount, true, true, true).Subscribe(static change => Console.WriteLine(change.Value)))
{
    form.Amount = RevisedAmount;
}
```

```text
250
```

By name, write both type arguments and pass the property name. This call observes the reference.

```csharp
TransferForm form = new() { Reference = RentReference };

using (form.ObservableForProperty<TransferForm, string>(nameof(TransferForm.Reference)).Subscribe(static change => Console.WriteLine(change.Value)))
{
    form.Reference = RevisedReference;
}
```

```text
Rent March, paid early
```

A second argument of `false` keeps the current value, so the subscriber receives the reference at once and again after the change.

```csharp
TransferForm form = new() { Reference = RentReference };

using (form.ObservableForProperty<TransferForm, string>(nameof(TransferForm.Reference), false).Subscribe(static change => Console.WriteLine(change.Value)))
{
    form.Reference = RevisedReference;
}
```

```text
Rent March
Rent March, paid early
```

The long form by name takes all three arguments too. This call asks for the reference before it changes, and it skips the current value, so it prints the value the change replaces.

```csharp
TransferForm form = new() { Reference = RentReference };

using (form.ObservableForProperty<TransferForm, string>(nameof(TransferForm.Reference), true, true, true).Subscribe(static change => Console.WriteLine(change.Value)))
{
    form.Reference = RevisedReference;
}
```

```text
Rent March
```

### SubscribeToExpressionChain

`SubscribeToExpressionChain` observes a chain that you built as an expression. It takes an `Expression` and the same arguments as `ObservableForProperty`, and its longest overload adds `suppressWarnings`, which turns off the warning the engine writes when it cannot observe a link. It delivers the value at the end of the chain and follows each link as the objects on the chain change.

```csharp
TransferForm form = new() { Amount = RentAmount };
Expression<Func<TransferForm, decimal>> amount = x => x.Amount;

using (form.SubscribeToExpressionChain<TransferForm, decimal>(amount.Body, false).Subscribe(static change => Console.WriteLine(change.Value)))
{
    form.Amount = RevisedAmount;
}
```

```text
250
300
```

Pass `true` for `beforeChange`, `skipInitial` and `isDistinct` to receive the value before each change.

```csharp
TransferForm form = new() { Amount = RentAmount };
Expression<Func<TransferForm, decimal>> amount = x => x.Amount;

using (form.SubscribeToExpressionChain<TransferForm, decimal>(amount.Body, true, true, true).Subscribe(static change => Console.WriteLine(change.Value)))
{
    form.Amount = RevisedAmount;
}
```

```text
250
```

The five-argument overload adds `suppressWarnings`. Pass `true` to observe a link that cannot notify without a warning.

```csharp
TransferForm form = new() { Amount = RentAmount };
Expression<Func<TransferForm, decimal>> amount = x => x.Amount;

using (form.SubscribeToExpressionChain<TransferForm, decimal>(amount.Body, false, true, true, true).Subscribe(static change => Console.WriteLine(change.Value)))
{
    form.Amount = RevisedAmount;
}
```

```text
300
```

A link that is a plain class has no notification of its own. The fallback provider observes it: it delivers the current value once and then nothing, and the subscription stays open. It writes a debug message the first time it observes each type and property, and `suppressWarnings` set to `true` skips that message. A chain over such a link can deliver a value twice although `isDistinct` is `true`.

The run-time engine follows the same null rules as generated code. It delivers nothing while an object before the last property is null, and it delivers a null last property as a value. If a value cannot be cast to the value type you asked for, the stream ends with an `InvalidCastException`.

## Small value types

Two record structs carry the settings and results of observation. Both are read-only, so two values are equal when every member is equal.

`ExpressionChainParameters<TSender>` bundles what the run-time engine needs to observe one chain: the object, the chain as an expression, its links and the four options. Its text names each member, like `PropertyValues`.

```csharp
TransferForm form = new() { Amount = RentAmount };
Expression<Func<TransferForm, decimal>> amount = x => x.Amount;
Expression[] links = [amount.Body];
ExpressionChainParameters<TransferForm> parameters = new(form, amount.Body, links, false, true, true, false);

var (source, expression, chain, beforeChange, skipInitial, isDistinct, suppressWarnings) = parameters;

Console.WriteLine(source?.Amount);
Console.WriteLine(expression);
Console.WriteLine(chain.Length);
Console.WriteLine(beforeChange);
Console.WriteLine(skipInitial);
Console.WriteLine(isDistinct);
Console.WriteLine(suppressWarnings);
```

```text
250
x.Amount
1
False
True
True
False
```

Two bundles are equal when every member is equal, and equal bundles have the same hash code.

```csharp
TransferForm form = new() { Amount = RentAmount };
Expression<Func<TransferForm, decimal>> amount = x => x.Amount;
Expression[] links = [amount.Body];
ExpressionChainParameters<TransferForm> after = new(form, amount.Body, links, false, true, true, false);
ExpressionChainParameters<TransferForm> sameAfter = new(form, amount.Body, links, false, true, true, false);
ExpressionChainParameters<TransferForm> before = new(form, amount.Body, links, true, true, true, false);

Console.WriteLine(after == sameAfter);
Console.WriteLine(after != before);
Console.WriteLine(after.Equals(sameAfter));
Console.WriteLine(after.Equals((object)before));
Console.WriteLine(after.GetHashCode() == sameAfter.GetHashCode());
```

```text
True
True
True
False
True
```

The text of a bundle names each member. The example splits the text at each comma to print one member per line.

```csharp
TransferForm form = new() { Amount = RentAmount };
Expression<Func<TransferForm, decimal>> amount = x => x.Amount;
Expression[] links = [amount.Body];
ExpressionChainParameters<TransferForm> parameters = new(form, amount.Body, links, false, true, true, false);

var text = parameters.ToString();

foreach (var member in text.TrimEnd(' ', '}').Split(", "))
{
    Console.WriteLine(member);
}
```

```text
ExpressionChainParameters { Source = ReactiveUI.Binding.Documentation.Observing.TransferForm
Expression = x.Amount
Links = System.Linq.Expressions.Expression[]
BeforeChange = False
SkipInitial = True
IsDistinct = True
SuppressWarnings = False
```

`BindingChange` is the value a binding reports each time it writes. It holds the value written and whether the write went from the view model to the view. [Bindings](bindings.md) shows where a binding reports it. Two changes are equal when both members are equal. The example below compares three changes, and only the change that went the other way differs.

```csharp
BindingChange fromViewModel = new(RenamedTitle, true);
BindingChange sameChange = new(RenamedTitle, true);
BindingChange fromView = new(RenamedTitle, false);

Console.WriteLine(fromViewModel == sameChange);
Console.WriteLine(fromViewModel != fromView);
Console.WriteLine(fromViewModel.Equals(sameChange));
Console.WriteLine(fromViewModel.Equals((object)fromView));
Console.WriteLine(fromViewModel.GetHashCode() == sameChange.GetHashCode());
```

```text
True
True
True
False
True
```

The text of a change names each member. The example prints one change so you can see the member names.

```csharp
BindingChange change = new(RenamedTitle, true);

var text = change.ToString();

Console.WriteLine($"Change: {text}.");
```

```text
Change: BindingChange { Value = Renew car registration online, FromViewModel = True }.
```

## Where to go next

- [Bindings](bindings.md) copies observed values into views.
- [Mechanisms](mechanisms.md) explains how a type announces a change and how the generator picks a way to listen.
- [Threading and platforms](threading.md) explains where observed values are delivered.
- [Unsafe twins and the runtime fallback](unsafe.md) covers calls the generator cannot read.
- [API reference](api.md) lists every public type and member.

## API reference

The [full API reference](api.md) lists every overload. Each row below covers one member, and a member with many overloads by property count appears once with its largest form.

| Member | What it does | Type and values | Notes |
| --- | --- | --- | --- |
| [`WhenChanged`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.WhenChanged.WideArity.cs) | Observes properties and delivers their values on subscription and after each change that alters them. | Extension method on a reference type. One to sixteen properties. Returns `IObservable<T1>` for one property and `IObservable<PropertyValues<...>>` for two or more. A selector as the last argument returns `IObservable<TReturn>`. | The stub throws `InvalidOperationException` when no generated dispatch matches the call site. See [Unsafe twins](unsafe.md). |
| [`WhenChanging`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.WhenChanging.WideArity.cs) | Observes properties and delivers their values on subscription and just before each change. | Same shapes as `WhenChanged`: one to sixteen properties, with or without a selector. | The object must raise `INotifyPropertyChanging.PropertyChanging`. The analyzer reports RXUIBIND004 when it cannot. |
| [`WhenAnyValue`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.WhenAnyValue.WideArity.cs) | Observes properties and delivers their values on subscription and after each change that alters them. | Same shapes as `WhenChanged`: one to sixteen properties, with or without a selector. | Delivers the same values as `WhenChanged`. |
| [`WhenAny`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.WhenAny.WideArity.cs) | Observes properties and delivers the result of a selector that receives one observed change per property. | One to twelve properties. The selector is required. Returns `IObservable<TRet>`. | A change from generated code has a null `Expression`. |
| [`WhenAnyDynamic`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.WhenAnyDynamic.WideArity.cs) | Observes property chains that a program built as `Expression` values, and delivers the selector's result. | One to twelve `Expression?` chains and a selector. The optional `isDistinct` argument is `true` by default. | Reads by reflection and carries `RequiresUnreferencedCode`. Not safe to trim. |
| [`WhenAnyObservable`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.WhenAnyObservable.WideArity.cs) | Observes properties that hold `IObservable<T>` and delivers the values of the streams they hold. | One to twelve properties. Streams of one element type merge. A selector as the last argument combines the latest value of each. | A property that holds null delivers nothing. When a property changes, the old stream is dropped and the new one is followed. A selector result starts once every stream has delivered. |
| [`PropertyValues<T1, ..., T16>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Observables/Values/PropertyValues%7BT1,%20T2,%20T3,%20T4,%20T5,%20T6,%20T7,%20T8,%20T9,%20T10,%20T11,%20T12,%20T13,%20T14,%20T15,%20T16%7D.cs) | Holds the values of several observed properties as one emission. | Read-only record struct with members `Property1` to `Property16`. Two to sixteen type arguments. | Supports deconstruction, value equality, `with` copies and a `ToString` that names each member. |
| [`IObservedChange<TSender, TValue>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IObservedChange.cs) | Describes one property change. | Read-only members `Sender` (`TSender`), `Expression` (`Expression?`) and `Value` (`TValue`). | `Value` is the default when a source raises a notification without reading the property. |
| [`ObservedChange<TSender, TValue>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/ObservedChange.cs) | The class behind `IObservedChange`. | Constructor takes `sender`, `expression` (`Expression?`) and `value`. | Pass a null expression when no expression tree is needed. |
| [`GetPropertyName`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/ObservedChangedMixins.cs) | Returns the dotted path of the property a change describes, such as `A.B[0].C`. | Extension on `IObservedChange<TSender, TValue>`. Returns `string`. | Throws `ArgumentNullException` when the change or its expression is null. |
| [`GetValue`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/ObservedChangedMixins.cs) | Returns the value a change carries, or reads it from the sender when the carried value is the default. | Extension on `IObservedChange<TSender, TValue>`. Returns `TValue`. | Throws `InvalidOperationException` when an object in the middle of the path is null. Carries `RequiresUnreferencedCode`. |
| [`GetValueOrDefault`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/ObservedChangedMixins.cs) | Returns what `GetValue` returns, or the default when an object in the middle of the path is null. | Extension on `IObservedChange<TSender, TValue>`. Returns `TValue?`. | Carries `RequiresUnreferencedCode`. |
| [`Value()`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/ObservedChangedMixins.cs) | Turns a stream of observed changes into a stream of current values. | Extension on `IObservable<IObservedChange<TSender, TValue>>`. Returns `IObservable<TValue>`. | A change whose expression cannot be followed ends the stream with `InvalidOperationException`. Carries `RequiresUnreferencedCode`. |
| [`ObservableForProperty`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/ReactiveNotifyPropertyChangedMixins.cs) | Observes one property by lambda or by name through the highest-affinity provider registered. | Optional `beforeChange`, `skipInitial` and `isDistinct` arguments. The short forms use `false`, `true` and `true`. | Throws `ArgumentNullException` for a null object, lambda or name, and `InvalidOperationException` when no provider bids. Hidden from IntelliSense. Carries `RequiresUnreferencedCode`. |
| [`SubscribeToExpressionChain`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/ReactiveNotifyPropertyChangedMixins.cs) | Observes a property chain built as an `Expression`, and follows each link as objects on the chain change. | Optional `beforeChange`, `skipInitial`, `isDistinct` and `suppressWarnings` arguments. The defaults are `false`, `true`, `true` and `false`. | Delivers nothing while an object before the last property is null. A value that cannot be cast to `TValue` ends the stream with `InvalidCastException`. |
| [`ExpressionChainParameters<TSender>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/ExpressionChainParameters.cs) | Bundles what the run-time engine needs to observe one chain. | Read-only record struct: `Source`, `Expression`, `Links`, `BeforeChange`, `SkipInitial`, `IsDistinct`, `SuppressWarnings`. | Hidden from IntelliSense. Two bundles are equal when every member is equal. |
| [`ExpressionChainSink<TSender, TValue>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/ExpressionChainSink.cs) | The observable behind a chain: it delivers the value of the last link each time any link changes. | Sealed class implementing `IObservable<IObservedChange<TSender, TValue>>`. | Returned by `SubscribeToExpressionChain` and by the expression forms of `ObservableForProperty`. |
| [`ObservableForPropertySink<TSender, TValue>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/ObservableForPropertySink.cs) | The observable behind one property: it delivers the value on subscription, unless skipped, and on each notification. | Sealed class implementing `IObservable<IObservedChange<TSender, TValue>>`. | Returned by the by-name forms of `ObservableForProperty`. It reads the value on every notification and ignores the notification's own value. A read that throws reaches the observer as an error. |
| [`ExpressionMixins`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/ExpressionMixins.cs) | Takes property-access expression trees apart. | Extension members on `Expression`: `GetExpressionChain`, `GetMemberInfo`, `GetParent`, `GetArgumentsArray`. | `GetParent` returns null for a static member. `GetArgumentsArray` returns null when the expression is not an index expression. |
| [`Reflection`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/Reflection.cs) | Reads and writes members along a property expression chain by reflection. | Static methods: `ExpressionToPropertyNames`, `Rewrite`, value fetchers and setters, and `TryGet` and `TrySet` chain methods. | The `OrThrow` forms throw when a member is not a field or property. |
