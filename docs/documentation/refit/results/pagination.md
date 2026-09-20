---
Order: 6
---
# Pagination

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/results-paging/results-paging.csproj).

A service that holds a long list rarely returns it in one reply. It returns a *page*: some of the
items, plus a way to ask for the next page. That way is a *token*, such as a cursor, an offset or a
page number, or it is a link to the next page. To read the whole list, you send a request, read the
items, read the token, and send the next request until the service stops naming one.

Refit runs that loop for you. Declare a method that returns `PagedEnumerable<TPage, TItem>`, and read
the items with `await foreach`. `TPage` is the page type your service returns, and `TItem` is the type
of each item on it. Your page type stays your own class. Refit does not add a page model of its own.

This page starts with one listing from Amazon S3. It then shows how the same two attributes cover the
other common paging styles, and how to write the loop yourself when they do not.

## Read every item

**1. Declare the method.** Return `PagedEnumerable<TPage, TItem>`. Add `[Paged]` to say where the next
token is on a page, and add `[PageToken]` to the parameter that carries the token in a request.

```csharp
internal interface IS3Api
{
    [Get("/{bucket}?list-type=2")]
    [Paged(Next = nameof(ListBucketResult.NextContinuationToken))]
    PagedEnumerable<ListBucketResult, S3Object> ListObjects(
        string bucket,
        string? prefix,
        [AliasAs("max-keys")] int maxKeys,
        [PageToken] [AliasAs("continuation-token")] string? continuationToken = null);
}
```

The token parameter binds like any other parameter. Here `[AliasAs]` names the query key that S3 expects.
The argument you pass is the token of the first page, so `null` starts at the beginning and a saved
token resumes a listing.

`ListBucketResult` is the class S3 returns, and `NextContinuationToken` is a property on it.
Refit turns that name into a plain property access when your project builds. It does not use
reflection, and a misspelled name is a build error.

**2. Read the items.** The returned sequence is an `IAsyncEnumerable<S3Object>`.

```csharp
await foreach (S3Object item in api.ListObjects("photos", "2026/", 3))
{
    Console.WriteLine(item.Key);
}
```

Calling the method sends nothing. The first request goes out when the loop starts. Each later request
uses the token that the previous page named. The loop ends when a page names no token.

**3. Read the pages instead.** Call `AsPages()` when you need more than the items, such as a count or a
flag that the service puts on each page. You get your own `ListBucketResult` objects.

```csharp
await foreach (ListBucketResult page in api.ListObjects("photos", "2026/", 3).AsPages())
{
    Console.WriteLine($"{page.KeyCount} keys, truncated: {page.IsTruncated}");
}
```

## What the sequence does for you

- **It is lazy.** Nothing is sent until you enumerate it, and every enumeration starts again from the
  first page.
- **Each page is a new request.** Refit builds the path, query, headers and body from your arguments
  every time, so a request is never sent twice.
- **It stops when you stop.** Leave the loop early, or cancel, and Refit requests no further page.
- **It cancels in flight.** The token you pass to `WithCancellation` reaches the request that is
  running, not only the gaps between pages.
- **It disposes each page.** A page is disposed when you move on to the next one. Do not keep a page
  after that.
- **It ends on a missing page.** A page that is `null` ends the sequence.
- **It ends on an empty token.** A `null` or empty string ends the sequence. Azure Blob Storage sends
  an empty `NextMarker` on the last page, and an empty string would otherwise request page one again.

## Tell Refit where the next page is

`[Paged]` has one setting for each place a token can be. Name exactly one of `Next`, `NextHeader` or
`Total`. A method that follows links names `Next` or `NextHeader` and states an origin policy.

| Setting | Meaning |
| --- | --- |
| `Items` | The member of the page that holds the items. Omit it when the page has exactly one sequence of `TItem`. |
| `Next` | The member of the page that holds the token or the link. It must be nullable, so the last page can end the sequence. |
| `NextHeader` | The response header that holds the token or the link. The page type must be `ApiResponse<T>` or `IApiResponse`. |
| `Total` | The member of the page that holds the total item count, for offset paging. The token must be an `int`. |
| `Origins`, `SameOrigin`, `AnyOrigin` | Which origins a link may point at. |

A name can reach through nested members with dots, such as `Content.Documents`. That is how you
reach into an `ApiResponse<T>`, and how you name a token that a page nests inside another member, such
as `ResponseMetadata.NextCursor`. Use the C# member names, not the JSON names.

### A token in the body

S3, Google Cloud Storage and Azure Blob Storage each return the next token in the reply body.
The token goes back to the service in a query parameter.

```csharp
[Get("/storage/v1/b/{bucket}/o")]
[Paged(Next = nameof(GcsObjectList.NextPageToken))]
PagedEnumerable<GcsObjectList, GcsObject> ListObjects(string bucket, string? prefix, int maxResults, [PageToken] string? pageToken = null);
```

Azure names its items inside a `Blobs` element, so the method names both `Items` and `Next`:

```csharp
[Get("/{container}?restype=container&comp=list")]
[Paged(Items = "Blobs.Items", Next = nameof(EnumerationResults.NextMarker))]
PagedEnumerable<EnumerationResults, Blob> ListBlobs(string container, string? prefix, [AliasAs("maxresults")] int maxResults, [PageToken] string? marker = null);
```

### A token in a header

Azure Cosmos DB returns its continuation in the `x-ms-continuation` response header and expects it back
in a request header. Return `ApiResponse<TPage>` so the page exposes its headers, and set
`NextHeader`. The token parameter is a request header:

```csharp
[Post("/dbs/{database}/colls/{collection}/docs")]
[Headers("x-ms-documentdb-isquery: true")]
[Paged(NextHeader = "x-ms-continuation")]
PagedEnumerable<ApiResponse<DocumentFeed>, CosmosDocument> Query(
    string database,
    string collection,
    [Body] CosmosQuery query,
    [Header("x-ms-max-item-count")] int maxItemCount,
    [PageToken] [Header("x-ms-continuation")] string? continuation = null);
```

The query in `query` goes out with every page. Only the continuation changes.

### An offset

Jira and many other services address a page by the index of its first item, and report the total.
Give `[PageToken]` to an `int` parameter and name the total. Refit advances the offset by the number
of items on each page and stops when it reaches the total.

```csharp
[Get("/rest/api/3/search")]
[Paged(Items = nameof(JiraSearchResult.Issues), Total = nameof(JiraSearchResult.Total))]
PagedEnumerable<JiraSearchResult, JiraIssue> Search(string jql, int maxResults, [PageToken] int startAt = 0);
```

A service that names the next offset itself uses `Next` instead of `Total`. Name a member of type
`int?`, and `null` ends the sequence.

### Links

Microsoft Graph and GitHub return the address of the next page. A method that follows links has no
`[PageToken]` parameter. Refit builds the first request from your arguments. It sends every later
request to the link, keeping your method, headers and body.

```csharp
[Get("/v1.0/users")]
[Paged(Next = nameof(GraphUserPage.NextLink), SameOrigin = true)]
PagedEnumerable<GraphUserPage, GraphUser> ListUsers([AliasAs("$top")] int top);
```

GitHub sends the link in a `Link` header, on a reply that is a bare JSON array. `NextHeader = "Link"`
reads the entry whose relation is `next`. The page is an `ApiResponse<List<GitHubRepo>>`, and Refit
finds the items in its content.

```csharp
[Get("/orgs/{org}/repos")]
[Headers("User-Agent: refit-paging-sample", "Accept: application/vnd.github+json")]
[Paged(NextHeader = "Link", Origins = ["https://api.github.com"])]
PagedEnumerable<ApiResponse<List<GitHubRepo>>, GitHubRepo> ListRepositories(string org, [AliasAs("per_page")] int perPage);
```

A link can be relative. Refit resolves it against the address of the request that returned it.

### Choose which origins a link may name

A link comes from the server, and your client sends its credentials with every request. An *origin* is
the scheme, host and port of an address. If a reply could name any origin, a compromised or careless
service could send your `Authorization` header to another host. So a method that follows links must
say which origins are allowed. Name exactly one:

| Setting | Allows |
| --- | --- |
| `SameOrigin = true` | Only the origin of the client's `BaseAddress`. |
| `Origins = [...]` | The origins you list. Each must be an absolute `http` or `https` address. |
| `AnyOrigin = true` | Any `http` or `https` origin. Use it only for a service you trust completely. |

Refit does not send a link it refuses. The sequence throws `InvalidOperationException` once you have
finished with the page that carried the link. A link that contains user information, such as
`https://user:secret@host/`, is always refused.

## Services this covers

The example project runs each of these against a local stand-in that returns the real wire format,
including the XML of S3 and Azure and their opaque tokens.

| Service | How it pages | What you write |
| --- | --- | --- |
| Amazon S3 ListObjectsV2 | `continuation-token` query, `NextContinuationToken` in XML | `Next`, and `[PageToken]` with `[AliasAs]` |
| Azure Blob Storage | `marker` query, `NextMarker` in XML, empty on the last page | `Items` and `Next`, and `[PageToken]` |
| Azure Cosmos DB | `x-ms-continuation` header in both directions | `NextHeader`, and `[PageToken]` with `[Header]` |
| Microsoft Graph | Absolute `@odata.nextLink` in JSON | `Next` and `SameOrigin` |
| GitHub | `Link` header on a JSON array | `NextHeader = "Link"` and `Origins` |
| Google Cloud Storage | `pageToken` query, `nextPageToken` in JSON | `Next`, and `[PageToken]` |
| Jira | `startAt` offset and `total` in JSON | `Total`, and `[PageToken]` on an `int` |
| Amazon DynamoDB | `ExclusiveStartKey` inside the request body | [Write the loop yourself](#write-the-loop-yourself) |

## Write the loop yourself

`[Paged]` reads a member of the page and puts the token into one parameter. Some services need more.
DynamoDB, for example, keeps its token inside the request body, next to other fields. For those, write
an ordinary Refit method that returns one page, and wrap it with `PagedEnumerable`.

```csharp
internal interface IDynamoDbApi
{
    [Post("/")]
    [Headers("X-Amz-Target: DynamoDB_20120810.Scan")]
    Task<DynamoScanResponse> Scan([Body] DynamoScanRequest request, CancellationToken cancellationToken);
}
```

`PagedEnumerable.Create` takes a method that fetches a page for a token, a selector for the items,
and a selector for the continuation. Refit calls the fetch method once for each page and passes it the
cancellation token of the enumeration.

```csharp
PagedEnumerable<DynamoScanResponse, Dictionary<string, DynamoAttributeValue>> orders = PagedEnumerable.Create(
    (Dictionary<string, DynamoAttributeValue>? startKey, CancellationToken cancellationToken) =>
        api.Scan(new() { TableName = "Orders", Limit = 3, ExclusiveStartKey = startKey }, cancellationToken),
    static page => page.Items,
    static (page, _) => PageContinuation.To(page.LastEvaluatedKey));
```

The selectors capture nothing, so mark them `static`. `PageContinuation.To` ends the sequence when it
gets `null` or an empty string.

Three shorter factories cover the usual shapes. Each takes the same fetch method and item selector:

| Factory | Use it when |
| --- | --- |
| `PagedEnumerable.FromCursor` | The next page is named by a string cursor. |
| `PagedEnumerable.FromOffset` | The next page starts at an `int` offset. |
| `PagedEnumerable.FromLinks` | The reply names the next page with a link. Pass a `NextLinkOriginPolicy` first. |

## Stop early, limit and overlap

- **Stop early.** Leave the `await foreach` loop, or dispose the enumerator. Refit fetches no more.
- **Limit the pages.** `WithMaxPages(2)` returns a new sequence that fetches at most two pages, however
  many the service holds.
- **Overlap the requests.** `WithPrefetch()` asks for the next page while you work through the current
  one. It reads at most one page ahead. If you stop, Refit cancels that request and disposes its page.
- **Cancel.** Pass a token with `WithCancellation`. It cancels the request that is running and stops
  the loop.
- **Use observables.** `ToObservable()` gives an `IObservable<TItem>`, and `ToPageObservable()` gives
  the pages. Both are *cold*: nothing is requested until you subscribe, and each subscription starts
  again from the first page. Disposing a subscription cancels the request in flight. A page is disposed
  after `OnNext` returns, so an observer must not keep it.

```csharp
await foreach (S3Object item in listing.WithMaxPages(2))
{
    Console.WriteLine(item.Key);
}
```

## Errors

An unsuccessful reply on any page throws its `ApiException`, so a failure never reads as the end of the
list. Refit sends no request after it. If the code that reads the token throws, you still receive the
items of that page first, and then the exception.

## Build-time checks

The generator checks each `[Paged]` method when your project builds. A method that cannot be generated
reports `RF013` and names the reason. These are the reasons:

| Reason | What to change |
| --- | --- |
| No `[Paged]` attribute | Add `[Paged]`, or return `Task<TPage>`. |
| `[Paged]` on a method that does not return `PagedEnumerable<TPage, TItem>` | Return `PagedEnumerable<TPage, TItem>`, or remove the attribute. |
| Generic method, or a `CancellationToken` parameter | Remove it. The token you pass to the enumeration serves every page. |
| More than one `[PageToken]` parameter | Mark only one. |
| Items missing, ambiguous or not a sequence of `TItem` | Name the member with `Items`. |
| More than one of `Next`, `NextHeader` and `Total`, or none | Name exactly one. |
| `Next` is not nullable, or its type differs from the token | Use a nullable member of the token's type. |
| A header is named on a page that is not `ApiResponse<T>` or `IApiResponse` | Return `ApiResponse<TPage>`. |
| `Total` with a token that is not an `int`, or a total that is not an `int` or a `long` | Use an `int` offset and a numeric total. |
| A link method without exactly one origin setting, or an origin that is not `http` or `https` | Name exactly one origin setting. |

Paged methods need generated request building, like `[QueryName]` and `[Encoded]`. When the request of
a paged method cannot be generated inline, the generator reports `RF007` for `[Paged]`.
The reflection request builder rejects them.

## Reference

Source: [PagedAttribute.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/PagedAttribute.cs),
[PageTokenAttribute.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/PageTokenAttribute.cs),
[PagedEnumerable.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/PagedEnumerable.cs),
[NextLinkOriginPolicy.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/NextLinkOriginPolicy.cs)
and the generator in [Parser.Paging.cs](https://github.com/reactiveui/refit/blob/main/src/InterfaceStubGenerator.Shared/Parser.Paging.cs).

| Member | Description |
| --- | --- |
| `PagedEnumerable<TPage, TItem>` | A lazy sequence of items that also exposes its pages. It implements `IAsyncEnumerable<TItem>`. |
| `AsPages()` | Returns an `IAsyncEnumerable<TPage>` of the pages as the service returned them. |
| `WithMaxPages(int pages)` | Returns a sequence that fetches at most `pages` pages. Throws `ArgumentOutOfRangeException` unless `pages` is positive. |
| `WithPrefetch()` | Returns a sequence that requests the next page while you read the current one. |
| `ToObservable()` / `ToPageObservable()` | Return cold observables of the items or the pages. |
| `PagedEnumerable.Create(...)` | Wraps a method that fetches one page, from selectors for the items and the continuation. |
| `FromCursor`, `FromOffset`, `FromLinks` | Shorter forms of `Create` for cursors, offsets and links. |
| `PageContinuation.To(token)` | Continues with `token`. It ends the sequence for `null` or an empty string. |
| `NextLinkOriginPolicy.Allow(origins)` / `Unrestricted` | Decide which links `FromLinks` may follow. |
| `[Paged]`, `[PageToken]` | Attributes that make the generator emit the paging loop. |
| `IApiResponse.GetLink(relation)` | Reads the link with a relation, such as `next`, from a `Link` response header. |
