---
Order: 6
---
# Telemetry

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/clients-telemetry/clients-telemetry.csproj).

When a call is slow or fails in production, you want to find it among thousands of others. *Telemetry* is the data your
app records for that: *traces* show each request as a *span* with a start, an end and some labels, and *metrics* count
and time requests in groups.

`HttpClient` already records a span and a duration metric for every request. Refit adds nothing on top. It does store
two labels on each request it builds: the interface method name and the unfilled route template, such as
`/people/{id}`. This page shows how to put those labels on the data `HttpClient` records, using OpenTelemetry.

Install `OpenTelemetry` and `OpenTelemetry.Instrumentation.Http`, plus an exporter for your monitoring system. The
samples use `OpenTelemetry.Exporter.InMemory` so they can check what was recorded. These packages work with Native AOT.

## Why use the route template

A label with many different values is called *high cardinality*. Each distinct value makes a separate group, so a
metric labelled with the full URL creates one group per person: `/people/1`, `/people/2` and so on. Your monitoring
system slows down and the groups say little.

The route template has one value per API method. `/people/{id}` groups every person's request together. Refit stores it
under `HttpRequestMessageOptions.RelativePathTemplate`, and the method name under `HttpRequestMessageOptions.MethodName`.
See [request context](../requests/request-context.md#refits-option-keys).

Leave full URLs, bodies, tokens and other user values out of labels.

## Read Refit's labels

A small helper reads both labels from a request. It returns null for a request that Refit did not build.

```csharp
internal static string? MethodName(HttpRequestMessage request) =>
    request.Options.TryGetValue(new HttpRequestOptionsKey<string>(HttpRequestMessageOptions.MethodName), out string? name) ? name : null;

internal static string? RouteTemplate(HttpRequestMessage request) =>
    request.Options.TryGetValue(new HttpRequestOptionsKey<string>(HttpRequestMessageOptions.RelativePathTemplate), out string? route) ? route : null;
```

## Label request spans

The problem: every span from your client is named `GET`. You cannot tell which API method it belongs to.

**1. Enrich HttpClient's span.** `EnrichWithHttpRequestMessage` runs when `HttpClient` starts its span, and it receives
the request. Name the span after the route and add the method name.

```csharp
internal static void EnrichSpans(HttpClientTraceInstrumentationOptions options) =>
    options.EnrichWithHttpRequestMessage = static (activity, request) =>
    {
        if (RefitRequestLabels.RouteTemplate(request) is { } route)
        {
            activity.DisplayName = $"{request.Method} {route}"; // "GET /people/{id}", not one name per person
            activity.SetTag("url.template", route);
        }

        if (RefitRequestLabels.MethodName(request) is { } method)
        {
            activity.SetTag("refit.method", method);
        }
    };
```

**2. Turn on tracing.** Add the HTTP client instrumentation with that method, and your exporter.

```csharp
using TracerProvider tracing = Sdk.CreateTracerProviderBuilder()
    .AddHttpClientInstrumentation(EnrichSpans)
    .AddInMemoryExporter(spans)
    .Build();
```

**3. Call the client as usual.** The span is `HttpClient`'s own span. Refit does not add a second one, so you never
see the same request twice.

```csharp
Activity span = spans[0]; // HttpClient's own span: Refit adds no second one
Console.WriteLine(span.DisplayName); // GET /people/{id}
Console.WriteLine(span.GetTagItem("refit.method")); // GetPersonAsync
```

In an app that uses `Microsoft.Extensions.Hosting`, call `AddOpenTelemetry().WithTracing(...)` on the services
instead of creating the provider yourself. The builder methods are the same.

## Label request-duration metrics

The problem: the `http.client.request.duration` metric groups requests by host and status. You want the time for each
API method.

**1. Add a handler that adds the tags.** `HttpMetricsEnrichmentContext.AddCallback` is part of .NET 8 and later. The
callback runs when `HttpClient` records the duration of that request.

```csharp
internal sealed class RefitMetricsTagsHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpMetricsEnrichmentContext.AddCallback(request, static context =>
        {
            if (RefitRequestLabels.RouteTemplate(context.Request) is { } route)
            {
                context.AddCustomTag("url.template", route);
            }

            if (RefitRequestLabels.MethodName(context.Request) is { } method)
            {
                context.AddCustomTag("refit.method", method);
            }
        });

        return base.SendAsync(request, cancellationToken);
    }
}
```

**2. Register it on the client.** Mark the factory lambda `static`, because it captures nothing.

```csharp
services.AddRefitGeneratedClient<ITelemetryPeopleApi>(SampleJsonContext.Default)
    .ConfigureHttpClient(static client => client.BaseAddress = new Uri("https://people.example"))
    .AddHttpMessageHandler(static () => new RefitMetricsTagsHandler());
```

**3. Turn on metrics.** `AddHttpClientInstrumentation` on the meter builder collects `HttpClient`'s metrics.

```csharp
using MeterProvider meters = Sdk.CreateMeterProviderBuilder()
    .AddHttpClientInstrumentation()
    .AddInMemoryExporter(metrics)
    .Build();
```

Two calls for different people now land in one group:

```csharp
Console.WriteLine(tags["url.template"]); // /people/{id}
Console.WriteLine(byRoute.GetHistogramCount()); // 2
```

## When the data is missing

`HttpClient` records spans and metrics inside its own socket handler. A replacement primary handler, such as the
`StubHttp` handler in `Refit.Testing`, skips that code, so tests that use it record neither. The complete sample gives
`SocketsHttpHandler` an in-memory connection through `ConnectCallback`, so it records real data without a network.

A request that Refit did not build, such as one you send with `HttpClient` directly, has no Refit labels. The helper
returns null and the samples skip the tags.

## API reference

| API | Package | Description |
| --- | --- | --- |
| `HttpRequestMessageOptions.MethodName` | `Refit` | Option key for the declared interface method name. |
| `HttpRequestMessageOptions.RelativePathTemplate` | `Refit` | Option key for the unfilled route template. |
| `HttpClientTraceInstrumentationOptions.EnrichWithHttpRequestMessage` | `OpenTelemetry.Instrumentation.Http` | Runs when `HttpClient` starts a span, with the span and the request. |
| `HttpMetricsEnrichmentContext.AddCallback(HttpRequestMessage, Action<HttpMetricsEnrichmentContext>)` | .NET 8 and later | Adds tags to the request's `http.client.request.duration` measurement. |

Source: [`Telemetry.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Telemetry/Telemetry.cs),
[`RefitMetricsTagsHandler.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Telemetry/RefitMetricsTagsHandler.cs)
and [`RefitRequestLabels.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Telemetry/RefitRequestLabels.cs).
