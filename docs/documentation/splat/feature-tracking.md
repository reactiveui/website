---
Order: 15
---
# Track a view and its operations

Use view tracking to record when a user opens a screen. Use a feature session to group work started on that screen. A packing desk, for example, can record opening the desk, packing an order and printing its label.

```csharp
var views = new ApplicationInsightsViewTracking(client);
views.OnViewNavigation("Packing desk");
var sessions = new FuncFeatureUsageTrackingManager(
    name => new ApplicationInsightsFeatureUsageTrackingSession(name, client));
using var packing = sessions.GetFeatureUsageTrackingSession("Pack order");
using var label = packing.SubFeature("Print label");
```

Pass the application's configured `TelemetryClient`. The v2 adapter exports its types from `Splat`. The v3 adapter uses `Splat.ApplicationInsightsV3`. The shared manager lives in `Splat.ApplicationPerformanceMonitoring`. Dispose the child before its parent so each session records the end of its own operation.

| Package | What the provider records |
| --- | --- |
| `Splat.ApplicationInsights` | Navigation sends a page view. The label's parent reference identifies the packing session. |
| `Splat.ApplicationInsightsV3` | Navigation sends a custom event called `PageView` with a `Name` property. Feature events can be joined by `Reference` and `ParentReference`. |
| `Splat.Raygun` | Call `OnException(error)` on the active child session to associate a printer error with that operation. |
| `Splat.Exceptionless` | Call `ExceptionlessViewTracking.OnViewNavigation` on an instance configured with your client to record the view. |

The examples accept an existing client, so tests can inspect the recorded data locally. [Application Insights example](https://github.com/reactiveui/splat/blob/main/src/examples/DispatchTelemetry/PackingTelemetry.cs) · [Raygun example](https://github.com/reactiveui/splat/blob/main/src/examples/DispatchTelemetry/RaygunPacking.cs) · [Exceptionless example](https://github.com/reactiveui/splat/blob/main/src/examples/DispatchAdapters/DispatchLogs.cs)
