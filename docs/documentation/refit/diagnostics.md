---
Order: 7
---
# Refit diagnostics

Refit checks your API interfaces while your project builds. A diagnostic is a warning or error that the
compiler shows with an ID, such as `RF006`. Each one points at a declaration that will fail, or that will not
work the way you expect, when the app runs. This page lists every diagnostic that Refit reports and how to fix it.

Most IDs start with `RF`. The Refit analyzer and the Refit source generator report them. The generator is the
part of Refit that writes your client code during the build. The `REFIT001` error comes from the Refit
package's build step instead.

## Fix a diagnostic

**1. Find the ID.** Build the project. The build output and your editor show the ID, the file and the line.
Most diagnostics sit on the declaration to change, such as a parameter or the text of a route.

**2. Look up the ID.** Find it in [the diagnostics table](#all-diagnostics). The table gives the meaning and the fix.
The message itself names the method and, often, the parameter.

**3. Change the declaration and build again.** Two diagnostics offer a code fix in the editor.
`RF003` offers `Use forward slashes in Refit route`. `RF005` offers `Use IDictionary<string, string> for HeaderCollection`.

Take this method. The route has a `{id}` placeholder, but no parameter supplies it:

```csharp
[Get("/users/{id}")]
Task<User> GetUser(int userId);
```

The build reports `RF015` on the `{id}` text:

```text
Route placeholder '{id}' in method IUsersApi.GetUser matches no parameter, so the request throws
ArgumentException unless RefitSettings.AllowUnmatchedRouteParameters is enabled. Add a parameter named 'id'
or use [AliasAs("id")] on an existing one.
```

Rename the parameter to `id`, or keep the name and add `[AliasAs("id")]`:

```csharp
[Get("/users/{id}")]
Task<User> GetUser([AliasAs("id")] int userId);
```

## All diagnostics

| ID | Severity | Meaning | Fix |
| --- | --- | --- | --- |
| `REFIT001` | Error | The compiler is older than Roslyn 4.8, which Refit's source generator needs. Without this check, the build would show many unrelated "type or namespace not found" errors. | Use Visual Studio 2022 17.8 or later, or the .NET 8.0.100 SDK or later. Or set `DisableRefitSourceGenerator` to `true` and use the `Refit.Reflection` package. |
| `RF001` | Warning | A method on a Refit interface has no Refit HTTP method attribute, or its path argument is not a string literal. Refit also checks methods that the interface inherits. | Add an attribute such as `[Get("/path")]` with a literal path, or remove the method from the Refit interface and its base interfaces. |
| `RF002` | Error | The generator cannot find Refit in the project. | Add a reference to the `Refit` package. |
| `RF003` | Warning | A route contains a backslash. | Use `/` in Refit routes. |
| `RF004` | Warning | A method has more than one `CancellationToken` parameter. The warning sits on the second one. | Keep one `CancellationToken` parameter. |
| `RF005` | Warning | A `[HeaderCollection]` parameter is not `IDictionary<string, string>`. | Declare the parameter as `IDictionary<string, string>`. |
| `RF006` | Warning | Refit cannot generate the method's request, so the method needs the reflection request builder. The message gives the reason, the fix and whether `Refit.Reflection` can run it. | Follow the fix in the message. [Find methods that fall back to reflection](aot.md#find-methods-that-fall-back-to-reflection) lists every reason. |
| `RF007` | Error | A method uses `[Paged]`, `[QueryName]`, `[Encoded]` or `[QueryConverter]`, which only generated requests honor, but Refit cannot generate its request. | Change the method so Refit can generate it, or remove the attribute. |
| `RF008` | Warning | A method has more than one `[HeaderCollection]` parameter. | Keep one. |
| `RF009` | Warning | A method has more than one `[Authorize]` parameter. | Keep one. |
| `RF011` | Warning | A method has more than one `[Body]` parameter. | Keep one. Send the other values as query, header or path values. |
| `RF012` | Warning | A `[Multipart]` method also has a `[Body]` parameter. | Remove `[Body]`. Every parameter of a multipart method becomes a part. |
| `RF013` | Error | A paged method is not set up in a way the generator can build. The message gives the reason. | See [build-time checks for paging](results/pagination.md#build-time-checks). |
| `RF014` | Error | A `JsonTypeInfo<T>` parameter cannot supply metadata for the request. The message gives the reason. | See [build-time checks for JSON metadata](serialization/json.md#build-time-checks). |
| `RF015` | Warning | A route placeholder matches no parameter. | Add a parameter with the placeholder's name, or put `[AliasAs]` with that name on an existing parameter. |

## RF006 and the strict build

`RF006` is a warning by default, because a project can choose to use `Refit.Reflection`.
A project that publishes with Native AOT, or registers only generated clients, should make it an error.
Set `<RefitRequireGeneratedRequests>true</RefitRequireGeneratedRequests>` in the project file.
To cover only some folders, set `dotnet_diagnostic.RF006.severity = error` in an `.editorconfig` file instead.
[Fail the build on a fallback](aot.md#fail-the-build-on-a-fallback) shows both.

## RF015 in detail

A placeholder is the `{name}` text in a route. Refit fills it from the method parameter with the same name.
`RF015` warns when no parameter has that name.

The message suggests the parameter name to use:

| Placeholder | Suggested name |
| --- | --- |
| `{id}` | `id` |
| `{**path}` | `path` |
| `{order.Id}` | `order` |

A `{**path}` placeholder keeps the slashes inside its value. A `{order.Id}` placeholder reads the `Id`
property of the `order` parameter. [Routes](requests/routes.md#fill-a-placeholder) covers both.

When the app runs, the request throws `ArgumentException`. Set `RefitSettings.AllowUnmatchedRouteParameters`
to `true` to keep the placeholder in the URL instead. That suits code that rewrites the URL later, such as a
`DelegatingHandler`. [Settings](clients/settings.md#check-buffering-and-unresolved-routes) shows the setting.
When you mean to leave a placeholder unmatched, suppress `RF015` on that method:

```csharp
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Refit",
    "RF015",
    Justification = "The sample demonstrates RefitSettings.AllowUnmatchedRouteParameters, which leaves {tenant} for later rewriting.")]
[Get("/policy/{tenant}")]
Task<HttpRequestMessage> UnmatchedAsync();
```

`RF015` checks only methods whose requests Refit generates. It does not run when generated request building
is off. A method that falls back to the reflection request builder gets `RF006` instead.

## REFIT001 in detail

NuGet skips an analyzer that is newer than your compiler, and it does not say so.
You would see no generated clients and many unrelated "type or namespace not found" errors.
`REFIT001` stops the build first and names the cause.

The full message reads:

```text
Refit's source generator requires Roslyn 4.8 or later (Visual Studio 2022 17.8+, or .NET SDK 8.0.100+), but
this project is building with Roslyn <version>. Upgrade the build tools, or set DisableRefitSourceGenerator to
true and use the Refit.Reflection package, which builds requests at runtime instead.
```

Refit skips the check when `DisableRefitSourceGenerator` is `true`, when the project is not C#, or when the
build does not report a Roslyn compiler version.
