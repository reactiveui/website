---
Order: 12
---
# ReactiveUI.Binding API reference

Find types, overloads, parameters, and return values in one place. The tables are grouped by category and topic. Follow a topic link for its walkthrough, examples, and detailed behavior. Use your browser's find command to look up a type or method name.

- [Observing](#observing)
- [Properties](#properties)
- [Bindings](#bindings)
- [Converters](#converters)
- [Mechanisms](#mechanisms)
- [Views](#views)
- [Threading](#threading)
- [Setup](#setup)
- [Unsafe and runtime](#unsafe-and-runtime)
- [Platforms](#platforms)

## Observing

### WhenChanged

[Full description and examples](observing.md).

Types: `ReactiveUI.Binding.ReactiveUIBindingExtensions`.

#### `ReactiveUIBindingExtensions`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class ReactiveUIBindingExtensions` | Extension methods for binding commands from a view model to controls on a view. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `WhenChanged<TObj, T1>(TObj objectToMonitor, Expression<Func<TObj, T1>> property1, string property1Expression = "", string callerFilePath = "", int callerLineNumber = 0)` | Observes a property and emits its current value when subscribed, then its new value after each change that alters it. | `TObj : class`: The type of the object to monitor for property changes; `T1`: The type of the first observed property value; `TObj` `objectToMonitor`: The object instance to observe for property changes; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property1Expression`: The caller argument expression for `property1`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IObservable<T1>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits the current value on subscription and the new value after each change that alters it. |
| `WhenChanged<TObj, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TObj objectToMonitor, Expression<Func<TObj, T1>> property1, Expression<Func<TObj, T2>> property2, Expression<Func<TObj, T3>> property3, Expression<Func<TObj, T4>> property4, Expression<Func<TObj, T5>> property5, Expression<Func<TObj, T6>> property6, Expression<Func<TObj, T7>> property7, Expression<Func<TObj, T8>> property8, Expression<Func<TObj, T9>> property9, Expression<Func<TObj, T10>> property10, Expression<Func<TObj, T11>> property11, Expression<Func<TObj, T12>> property12, Expression<Func<TObj, T13>> property13, Expression<Func<TObj, T14>> property14, Expression<Func<TObj, T15>> property15, Expression<Func<TObj, T16>> property16, string property1Expression = "", string property2Expression = "", string property3Expression = "", string property4Expression = "", string property5Expression = "", string property6Expression = "", string property7Expression = "", string property8Expression = "", string property9Expression = "", string property10Expression = "", string property11Expression = "", string property12Expression = "", string property13Expression = "", string property14Expression = "", string property15Expression = "", string property16Expression = "", string callerFilePath = "", int callerLineNumber = 0)` | Observes 16 properties and emits a PropertyValues of their current values when subscribed and whenever any of them changes. Available for 2 to 16 repeated arguments; the largest form is listed. | `TObj : class`: The type of the object to monitor for property changes; `T1`: The type of the first observed property value; `T2`: The type of the second observed property value; `T3`: The type of the third observed property value; `T4`: The type of the fourth observed property value; `T5`: The type of the fifth observed property value; `T6`: The type of the sixth observed property value; `T7`: The type of the seventh observed property value; `T8`: The type of the eighth observed property value; `T9`: The type of the ninth observed property value; `T10`: The type of the tenth observed property value; `T11`: The type of the eleventh observed property value; `T12`: The type of the twelfth observed property value; `T13`: The type of the thirteenth observed property value; `T14`: The type of the fourteenth observed property value; `T15`: The type of the fifteenth observed property value; `T16`: The type of the sixteenth observed property value; `TObj` `objectToMonitor`: The object instance to observe for property changes; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`Expression<Func<TObj, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: An expression that selects the second property to observe; [`Expression<Func<TObj, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: An expression that selects the third property to observe; [`Expression<Func<TObj, T4>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property4`: An expression that selects the fourth property to observe; [`Expression<Func<TObj, T5>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property5`: An expression that selects the fifth property to observe; [`Expression<Func<TObj, T6>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property6`: An expression that selects the sixth property to observe; [`Expression<Func<TObj, T7>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property7`: An expression that selects the seventh property to observe; [`Expression<Func<TObj, T8>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property8`: An expression that selects the eighth property to observe; [`Expression<Func<TObj, T9>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property9`: An expression that selects the ninth property to observe; [`Expression<Func<TObj, T10>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property10`: An expression that selects the tenth property to observe; [`Expression<Func<TObj, T11>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property11`: An expression that selects the eleventh property to observe; [`Expression<Func<TObj, T12>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property12`: An expression that selects the twelfth property to observe; [`Expression<Func<TObj, T13>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property13`: An expression that selects the thirteenth property to observe; [`Expression<Func<TObj, T14>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property14`: An expression that selects the fourteenth property to observe; [`Expression<Func<TObj, T15>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property15`: An expression that selects the fifteenth property to observe; [`Expression<Func<TObj, T16>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property16`: An expression that selects the sixteenth property to observe; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property1Expression`: The caller argument expression for `property1`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property2Expression`: The caller argument expression for `property2`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property3Expression`: The caller argument expression for `property3`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property4Expression`: The caller argument expression for `property4`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property5Expression`: The caller argument expression for `property5`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property6Expression`: The caller argument expression for `property6`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property7Expression`: The caller argument expression for `property7`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property8Expression`: The caller argument expression for `property8`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property9Expression`: The caller argument expression for `property9`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property10Expression`: The caller argument expression for `property10`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property11Expression`: The caller argument expression for `property11`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property12Expression`: The caller argument expression for `property12`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property13Expression`: The caller argument expression for `property13`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property14Expression`: The caller argument expression for `property14`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property15Expression`: The caller argument expression for `property15`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property16Expression`: The caller argument expression for `property16`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IObservable<PropertyValues<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits a PropertyValues holding the latest value of each property, on subscription and whenever any of them changes. |
| `WhenChanged<TObj, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>(TObj objectToMonitor, Expression<Func<TObj, T1>> property1, Expression<Func<TObj, T2>> property2, Expression<Func<TObj, T3>> property3, Expression<Func<TObj, T4>> property4, Expression<Func<TObj, T5>> property5, Expression<Func<TObj, T6>> property6, Expression<Func<TObj, T7>> property7, Expression<Func<TObj, T8>> property8, Expression<Func<TObj, T9>> property9, Expression<Func<TObj, T10>> property10, Expression<Func<TObj, T11>> property11, Expression<Func<TObj, T12>> property12, Expression<Func<TObj, T13>> property13, Expression<Func<TObj, T14>> property14, Expression<Func<TObj, T15>> property15, Expression<Func<TObj, T16>> property16, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> conversionFunc, string property1Expression = "", string property2Expression = "", string property3Expression = "", string property4Expression = "", string property5Expression = "", string property6Expression = "", string property7Expression = "", string property8Expression = "", string property9Expression = "", string property10Expression = "", string property11Expression = "", string property12Expression = "", string property13Expression = "", string property14Expression = "", string property15Expression = "", string property16Expression = "", string callerFilePath = "", int callerLineNumber = 0)` | Observes 16 properties and emits the result of applying a conversion function to their current values when subscribed and whenever any of them changes. Available for 2 to 16 repeated arguments; the largest form is listed. | `TObj : class`: The type of the object to monitor for property changes; `T1`: The type of the first observed property value; `T2`: The type of the second observed property value; `T3`: The type of the third observed property value; `T4`: The type of the fourth observed property value; `T5`: The type of the fifth observed property value; `T6`: The type of the sixth observed property value; `T7`: The type of the seventh observed property value; `T8`: The type of the eighth observed property value; `T9`: The type of the ninth observed property value; `T10`: The type of the tenth observed property value; `T11`: The type of the eleventh observed property value; `T12`: The type of the twelfth observed property value; `T13`: The type of the thirteenth observed property value; `T14`: The type of the fourteenth observed property value; `T15`: The type of the fifteenth observed property value; `T16`: The type of the sixteenth observed property value; `TReturn`: The return type of the conversion function; `TObj` `objectToMonitor`: The object instance to observe for property changes; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`Expression<Func<TObj, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: An expression that selects the second property to observe; [`Expression<Func<TObj, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: An expression that selects the third property to observe; [`Expression<Func<TObj, T4>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property4`: An expression that selects the fourth property to observe; [`Expression<Func<TObj, T5>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property5`: An expression that selects the fifth property to observe; [`Expression<Func<TObj, T6>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property6`: An expression that selects the sixth property to observe; [`Expression<Func<TObj, T7>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property7`: An expression that selects the seventh property to observe; [`Expression<Func<TObj, T8>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property8`: An expression that selects the eighth property to observe; [`Expression<Func<TObj, T9>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property9`: An expression that selects the ninth property to observe; [`Expression<Func<TObj, T10>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property10`: An expression that selects the tenth property to observe; [`Expression<Func<TObj, T11>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property11`: An expression that selects the eleventh property to observe; [`Expression<Func<TObj, T12>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property12`: An expression that selects the twelfth property to observe; [`Expression<Func<TObj, T13>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property13`: An expression that selects the thirteenth property to observe; [`Expression<Func<TObj, T14>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property14`: An expression that selects the fourteenth property to observe; [`Expression<Func<TObj, T15>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property15`: An expression that selects the fifteenth property to observe; [`Expression<Func<TObj, T16>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property16`: An expression that selects the sixteenth property to observe; [`Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>`](https://learn.microsoft.com/dotnet/api/system.func-17) `conversionFunc`: A function that converts the observed property values to the return type; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property1Expression`: The caller argument expression for `property1`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property2Expression`: The caller argument expression for `property2`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property3Expression`: The caller argument expression for `property3`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property4Expression`: The caller argument expression for `property4`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property5Expression`: The caller argument expression for `property5`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property6Expression`: The caller argument expression for `property6`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property7Expression`: The caller argument expression for `property7`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property8Expression`: The caller argument expression for `property8`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property9Expression`: The caller argument expression for `property9`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property10Expression`: The caller argument expression for `property10`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property11Expression`: The caller argument expression for `property11`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property12Expression`: The caller argument expression for `property12`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property13Expression`: The caller argument expression for `property13`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property14Expression`: The caller argument expression for `property14`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property15Expression`: The caller argument expression for `property15`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property16Expression`: The caller argument expression for `property16`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IObservable<TReturn>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits the conversion result on subscription and whenever any of the observed properties changes. |

### WhenChanging

[Full description and examples](observing.md).

Types: `ReactiveUI.Binding.ReactiveUIBindingExtensions`.

#### `ReactiveUIBindingExtensions`

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `WhenChanging<TObj, T1>(TObj objectToMonitor, Expression<Func<TObj, T1>> property1, string property1Expression = "", string callerFilePath = "", int callerLineNumber = 0)` | Observes a property and emits its current value when subscribed, then the value it holds just before each change. | `TObj : class`: The type of the object to monitor for property changes; `T1`: The type of the first observed property value; `TObj` `objectToMonitor`: The object instance to observe for property changes; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property1Expression`: The caller argument expression for `property1`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IObservable<T1>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits the current value on subscription and the value held before each change. |
| `WhenChanging<TObj, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TObj objectToMonitor, Expression<Func<TObj, T1>> property1, Expression<Func<TObj, T2>> property2, Expression<Func<TObj, T3>> property3, Expression<Func<TObj, T4>> property4, Expression<Func<TObj, T5>> property5, Expression<Func<TObj, T6>> property6, Expression<Func<TObj, T7>> property7, Expression<Func<TObj, T8>> property8, Expression<Func<TObj, T9>> property9, Expression<Func<TObj, T10>> property10, Expression<Func<TObj, T11>> property11, Expression<Func<TObj, T12>> property12, Expression<Func<TObj, T13>> property13, Expression<Func<TObj, T14>> property14, Expression<Func<TObj, T15>> property15, Expression<Func<TObj, T16>> property16, string property1Expression = "", string property2Expression = "", string property3Expression = "", string property4Expression = "", string property5Expression = "", string property6Expression = "", string property7Expression = "", string property8Expression = "", string property9Expression = "", string property10Expression = "", string property11Expression = "", string property12Expression = "", string property13Expression = "", string property14Expression = "", string property15Expression = "", string property16Expression = "", string callerFilePath = "", int callerLineNumber = 0)` | Observes 16 properties and emits a PropertyValues of their values when subscribed and each time one of them is about to change. Available for 2 to 16 repeated arguments; the largest form is listed. | `TObj : class`: The type of the object to monitor for property changes; `T1`: The type of the first observed property value; `T2`: The type of the second observed property value; `T3`: The type of the third observed property value; `T4`: The type of the fourth observed property value; `T5`: The type of the fifth observed property value; `T6`: The type of the sixth observed property value; `T7`: The type of the seventh observed property value; `T8`: The type of the eighth observed property value; `T9`: The type of the ninth observed property value; `T10`: The type of the tenth observed property value; `T11`: The type of the eleventh observed property value; `T12`: The type of the twelfth observed property value; `T13`: The type of the thirteenth observed property value; `T14`: The type of the fourteenth observed property value; `T15`: The type of the fifteenth observed property value; `T16`: The type of the sixteenth observed property value; `TObj` `objectToMonitor`: The object instance to observe for property changes; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`Expression<Func<TObj, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: An expression that selects the second property to observe; [`Expression<Func<TObj, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: An expression that selects the third property to observe; [`Expression<Func<TObj, T4>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property4`: An expression that selects the fourth property to observe; [`Expression<Func<TObj, T5>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property5`: An expression that selects the fifth property to observe; [`Expression<Func<TObj, T6>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property6`: An expression that selects the sixth property to observe; [`Expression<Func<TObj, T7>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property7`: An expression that selects the seventh property to observe; [`Expression<Func<TObj, T8>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property8`: An expression that selects the eighth property to observe; [`Expression<Func<TObj, T9>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property9`: An expression that selects the ninth property to observe; [`Expression<Func<TObj, T10>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property10`: An expression that selects the tenth property to observe; [`Expression<Func<TObj, T11>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property11`: An expression that selects the eleventh property to observe; [`Expression<Func<TObj, T12>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property12`: An expression that selects the twelfth property to observe; [`Expression<Func<TObj, T13>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property13`: An expression that selects the thirteenth property to observe; [`Expression<Func<TObj, T14>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property14`: An expression that selects the fourteenth property to observe; [`Expression<Func<TObj, T15>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property15`: An expression that selects the fifteenth property to observe; [`Expression<Func<TObj, T16>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property16`: An expression that selects the sixteenth property to observe; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property1Expression`: The caller argument expression for `property1`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property2Expression`: The caller argument expression for `property2`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property3Expression`: The caller argument expression for `property3`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property4Expression`: The caller argument expression for `property4`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property5Expression`: The caller argument expression for `property5`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property6Expression`: The caller argument expression for `property6`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property7Expression`: The caller argument expression for `property7`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property8Expression`: The caller argument expression for `property8`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property9Expression`: The caller argument expression for `property9`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property10Expression`: The caller argument expression for `property10`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property11Expression`: The caller argument expression for `property11`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property12Expression`: The caller argument expression for `property12`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property13Expression`: The caller argument expression for `property13`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property14Expression`: The caller argument expression for `property14`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property15Expression`: The caller argument expression for `property15`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property16Expression`: The caller argument expression for `property16`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IObservable<PropertyValues<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits a PropertyValues holding the latest value of each property, on subscription and each time one of them is about to change. |
| `WhenChanging<TObj, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>(TObj objectToMonitor, Expression<Func<TObj, T1>> property1, Expression<Func<TObj, T2>> property2, Expression<Func<TObj, T3>> property3, Expression<Func<TObj, T4>> property4, Expression<Func<TObj, T5>> property5, Expression<Func<TObj, T6>> property6, Expression<Func<TObj, T7>> property7, Expression<Func<TObj, T8>> property8, Expression<Func<TObj, T9>> property9, Expression<Func<TObj, T10>> property10, Expression<Func<TObj, T11>> property11, Expression<Func<TObj, T12>> property12, Expression<Func<TObj, T13>> property13, Expression<Func<TObj, T14>> property14, Expression<Func<TObj, T15>> property15, Expression<Func<TObj, T16>> property16, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> conversionFunc, string property1Expression = "", string property2Expression = "", string property3Expression = "", string property4Expression = "", string property5Expression = "", string property6Expression = "", string property7Expression = "", string property8Expression = "", string property9Expression = "", string property10Expression = "", string property11Expression = "", string property12Expression = "", string property13Expression = "", string property14Expression = "", string property15Expression = "", string property16Expression = "", string callerFilePath = "", int callerLineNumber = 0)` | Observes 16 properties and emits the result of applying a conversion function to their values when subscribed and each time one of them is about to change. Available for 2 to 16 repeated arguments; the largest form is listed. | `TObj : class`: The type of the object to monitor for property changes; `T1`: The type of the first observed property value; `T2`: The type of the second observed property value; `T3`: The type of the third observed property value; `T4`: The type of the fourth observed property value; `T5`: The type of the fifth observed property value; `T6`: The type of the sixth observed property value; `T7`: The type of the seventh observed property value; `T8`: The type of the eighth observed property value; `T9`: The type of the ninth observed property value; `T10`: The type of the tenth observed property value; `T11`: The type of the eleventh observed property value; `T12`: The type of the twelfth observed property value; `T13`: The type of the thirteenth observed property value; `T14`: The type of the fourteenth observed property value; `T15`: The type of the fifteenth observed property value; `T16`: The type of the sixteenth observed property value; `TReturn`: The return type of the conversion function; `TObj` `objectToMonitor`: The object instance to observe for property changes; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`Expression<Func<TObj, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: An expression that selects the second property to observe; [`Expression<Func<TObj, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: An expression that selects the third property to observe; [`Expression<Func<TObj, T4>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property4`: An expression that selects the fourth property to observe; [`Expression<Func<TObj, T5>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property5`: An expression that selects the fifth property to observe; [`Expression<Func<TObj, T6>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property6`: An expression that selects the sixth property to observe; [`Expression<Func<TObj, T7>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property7`: An expression that selects the seventh property to observe; [`Expression<Func<TObj, T8>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property8`: An expression that selects the eighth property to observe; [`Expression<Func<TObj, T9>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property9`: An expression that selects the ninth property to observe; [`Expression<Func<TObj, T10>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property10`: An expression that selects the tenth property to observe; [`Expression<Func<TObj, T11>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property11`: An expression that selects the eleventh property to observe; [`Expression<Func<TObj, T12>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property12`: An expression that selects the twelfth property to observe; [`Expression<Func<TObj, T13>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property13`: An expression that selects the thirteenth property to observe; [`Expression<Func<TObj, T14>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property14`: An expression that selects the fourteenth property to observe; [`Expression<Func<TObj, T15>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property15`: An expression that selects the fifteenth property to observe; [`Expression<Func<TObj, T16>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property16`: An expression that selects the sixteenth property to observe; [`Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>`](https://learn.microsoft.com/dotnet/api/system.func-17) `conversionFunc`: A function that converts the observed property values to the return type; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property1Expression`: The caller argument expression for `property1`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property2Expression`: The caller argument expression for `property2`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property3Expression`: The caller argument expression for `property3`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property4Expression`: The caller argument expression for `property4`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property5Expression`: The caller argument expression for `property5`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property6Expression`: The caller argument expression for `property6`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property7Expression`: The caller argument expression for `property7`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property8Expression`: The caller argument expression for `property8`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property9Expression`: The caller argument expression for `property9`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property10Expression`: The caller argument expression for `property10`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property11Expression`: The caller argument expression for `property11`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property12Expression`: The caller argument expression for `property12`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property13Expression`: The caller argument expression for `property13`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property14Expression`: The caller argument expression for `property14`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property15Expression`: The caller argument expression for `property15`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property16Expression`: The caller argument expression for `property16`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IObservable<TReturn>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits the conversion result on subscription and each time one of the observed properties is about to change. |

### WhenAnyValue

[Full description and examples](observing.md).

Types: `ReactiveUI.Binding.ReactiveUIBindingExtensions`.

#### `ReactiveUIBindingExtensions`

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `WhenAnyValue<TSender, T1>(TSender sender, Expression<Func<TSender, T1>> property1, string property1Expression = "", string callerFilePath = "", int callerLineNumber = 0)` | Observes a property and emits its current value when subscribed, then its new value after each change that alters it. This is a ReactiveUI compatibility shim. | `TSender : class`: The type of the sender to monitor for property changes; `T1`: The type of the first observed property value; `TSender` `sender`: The sender instance to observe for property changes; [`Expression<Func<TSender, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property1Expression`: The caller argument expression for `property1`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IObservable<T1>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits the current value on subscription and the new value after each change that alters it. |
| `WhenAnyValue<TSender, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TSender sender, Expression<Func<TSender, T1>> property1, Expression<Func<TSender, T2>> property2, Expression<Func<TSender, T3>> property3, Expression<Func<TSender, T4>> property4, Expression<Func<TSender, T5>> property5, Expression<Func<TSender, T6>> property6, Expression<Func<TSender, T7>> property7, Expression<Func<TSender, T8>> property8, Expression<Func<TSender, T9>> property9, Expression<Func<TSender, T10>> property10, Expression<Func<TSender, T11>> property11, Expression<Func<TSender, T12>> property12, Expression<Func<TSender, T13>> property13, Expression<Func<TSender, T14>> property14, Expression<Func<TSender, T15>> property15, Expression<Func<TSender, T16>> property16, string property1Expression = "", string property2Expression = "", string property3Expression = "", string property4Expression = "", string property5Expression = "", string property6Expression = "", string property7Expression = "", string property8Expression = "", string property9Expression = "", string property10Expression = "", string property11Expression = "", string property12Expression = "", string property13Expression = "", string property14Expression = "", string property15Expression = "", string property16Expression = "", string callerFilePath = "", int callerLineNumber = 0)` | Observes 16 properties and emits a PropertyValues of their current values when subscribed and whenever any of them changes. This is a ReactiveUI compatibility shim. Available for 2 to 16 repeated arguments; the largest form is listed. | `TSender : class`: The type of the sender to monitor for property changes; `T1`: The type of the first observed property value; `T2`: The type of the second observed property value; `T3`: The type of the third observed property value; `T4`: The type of the fourth observed property value; `T5`: The type of the fifth observed property value; `T6`: The type of the sixth observed property value; `T7`: The type of the seventh observed property value; `T8`: The type of the eighth observed property value; `T9`: The type of the ninth observed property value; `T10`: The type of the tenth observed property value; `T11`: The type of the eleventh observed property value; `T12`: The type of the twelfth observed property value; `T13`: The type of the thirteenth observed property value; `T14`: The type of the fourteenth observed property value; `T15`: The type of the fifteenth observed property value; `T16`: The type of the sixteenth observed property value; `TSender` `sender`: The sender instance to observe for property changes; [`Expression<Func<TSender, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`Expression<Func<TSender, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: An expression that selects the second property to observe; [`Expression<Func<TSender, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: An expression that selects the third property to observe; [`Expression<Func<TSender, T4>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property4`: An expression that selects the fourth property to observe; [`Expression<Func<TSender, T5>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property5`: An expression that selects the fifth property to observe; [`Expression<Func<TSender, T6>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property6`: An expression that selects the sixth property to observe; [`Expression<Func<TSender, T7>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property7`: An expression that selects the seventh property to observe; [`Expression<Func<TSender, T8>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property8`: An expression that selects the eighth property to observe; [`Expression<Func<TSender, T9>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property9`: An expression that selects the ninth property to observe; [`Expression<Func<TSender, T10>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property10`: An expression that selects the tenth property to observe; [`Expression<Func<TSender, T11>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property11`: An expression that selects the eleventh property to observe; [`Expression<Func<TSender, T12>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property12`: An expression that selects the twelfth property to observe; [`Expression<Func<TSender, T13>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property13`: An expression that selects the thirteenth property to observe; [`Expression<Func<TSender, T14>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property14`: An expression that selects the fourteenth property to observe; [`Expression<Func<TSender, T15>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property15`: An expression that selects the fifteenth property to observe; [`Expression<Func<TSender, T16>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property16`: An expression that selects the sixteenth property to observe; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property1Expression`: The caller argument expression for `property1`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property2Expression`: The caller argument expression for `property2`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property3Expression`: The caller argument expression for `property3`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property4Expression`: The caller argument expression for `property4`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property5Expression`: The caller argument expression for `property5`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property6Expression`: The caller argument expression for `property6`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property7Expression`: The caller argument expression for `property7`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property8Expression`: The caller argument expression for `property8`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property9Expression`: The caller argument expression for `property9`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property10Expression`: The caller argument expression for `property10`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property11Expression`: The caller argument expression for `property11`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property12Expression`: The caller argument expression for `property12`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property13Expression`: The caller argument expression for `property13`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property14Expression`: The caller argument expression for `property14`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property15Expression`: The caller argument expression for `property15`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property16Expression`: The caller argument expression for `property16`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IObservable<PropertyValues<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits a PropertyValues holding the latest value of each property, on subscription and whenever any of them changes. |
| `WhenAnyValue<TSender, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TRet>(TSender sender, Expression<Func<TSender, T1>> property1, Expression<Func<TSender, T2>> property2, Expression<Func<TSender, T3>> property3, Expression<Func<TSender, T4>> property4, Expression<Func<TSender, T5>> property5, Expression<Func<TSender, T6>> property6, Expression<Func<TSender, T7>> property7, Expression<Func<TSender, T8>> property8, Expression<Func<TSender, T9>> property9, Expression<Func<TSender, T10>> property10, Expression<Func<TSender, T11>> property11, Expression<Func<TSender, T12>> property12, Expression<Func<TSender, T13>> property13, Expression<Func<TSender, T14>> property14, Expression<Func<TSender, T15>> property15, Expression<Func<TSender, T16>> property16, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TRet> selector, string property1Expression = "", string property2Expression = "", string property3Expression = "", string property4Expression = "", string property5Expression = "", string property6Expression = "", string property7Expression = "", string property8Expression = "", string property9Expression = "", string property10Expression = "", string property11Expression = "", string property12Expression = "", string property13Expression = "", string property14Expression = "", string property15Expression = "", string property16Expression = "", string callerFilePath = "", int callerLineNumber = 0)` | Observes 16 properties and emits the result of a selector applied to their current values when subscribed and whenever any of them changes. This is a ReactiveUI compatibility shim. Available for 1 to 16 repeated arguments; the largest form is listed. | `TSender : class`: The type of the sender to monitor for property changes; `T1`: The type of the first observed property value; `T2`: The type of the second observed property value; `T3`: The type of the third observed property value; `T4`: The type of the fourth observed property value; `T5`: The type of the fifth observed property value; `T6`: The type of the sixth observed property value; `T7`: The type of the seventh observed property value; `T8`: The type of the eighth observed property value; `T9`: The type of the ninth observed property value; `T10`: The type of the tenth observed property value; `T11`: The type of the eleventh observed property value; `T12`: The type of the twelfth observed property value; `T13`: The type of the thirteenth observed property value; `T14`: The type of the fourteenth observed property value; `T15`: The type of the fifteenth observed property value; `T16`: The type of the sixteenth observed property value; `TRet`: The return type of the selector function; `TSender` `sender`: The sender instance to observe for property changes; [`Expression<Func<TSender, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`Expression<Func<TSender, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: An expression that selects the second property to observe; [`Expression<Func<TSender, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: An expression that selects the third property to observe; [`Expression<Func<TSender, T4>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property4`: An expression that selects the fourth property to observe; [`Expression<Func<TSender, T5>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property5`: An expression that selects the fifth property to observe; [`Expression<Func<TSender, T6>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property6`: An expression that selects the sixth property to observe; [`Expression<Func<TSender, T7>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property7`: An expression that selects the seventh property to observe; [`Expression<Func<TSender, T8>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property8`: An expression that selects the eighth property to observe; [`Expression<Func<TSender, T9>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property9`: An expression that selects the ninth property to observe; [`Expression<Func<TSender, T10>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property10`: An expression that selects the tenth property to observe; [`Expression<Func<TSender, T11>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property11`: An expression that selects the eleventh property to observe; [`Expression<Func<TSender, T12>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property12`: An expression that selects the twelfth property to observe; [`Expression<Func<TSender, T13>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property13`: An expression that selects the thirteenth property to observe; [`Expression<Func<TSender, T14>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property14`: An expression that selects the fourteenth property to observe; [`Expression<Func<TSender, T15>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property15`: An expression that selects the fifteenth property to observe; [`Expression<Func<TSender, T16>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property16`: An expression that selects the sixteenth property to observe; [`Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TRet>`](https://learn.microsoft.com/dotnet/api/system.func-17) `selector`: A function that converts the observed property values to the return type; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property1Expression`: The caller argument expression for `property1`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property2Expression`: The caller argument expression for `property2`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property3Expression`: The caller argument expression for `property3`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property4Expression`: The caller argument expression for `property4`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property5Expression`: The caller argument expression for `property5`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property6Expression`: The caller argument expression for `property6`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property7Expression`: The caller argument expression for `property7`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property8Expression`: The caller argument expression for `property8`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property9Expression`: The caller argument expression for `property9`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property10Expression`: The caller argument expression for `property10`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property11Expression`: The caller argument expression for `property11`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property12Expression`: The caller argument expression for `property12`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property13Expression`: The caller argument expression for `property13`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property14Expression`: The caller argument expression for `property14`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property15Expression`: The caller argument expression for `property15`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property16Expression`: The caller argument expression for `property16`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IObservable<TRet>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits the selector result on subscription and whenever any of the observed properties changes. |

### WhenAny

[Full description and examples](observing.md).

Types: `ReactiveUI.Binding.ReactiveUIBindingExtensions`.

#### `ReactiveUIBindingExtensions`

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `WhenAny<TSender, TRet, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(TSender sender, Expression<Func<TSender, T1>> property1, Expression<Func<TSender, T2>> property2, Expression<Func<TSender, T3>> property3, Expression<Func<TSender, T4>> property4, Expression<Func<TSender, T5>> property5, Expression<Func<TSender, T6>> property6, Expression<Func<TSender, T7>> property7, Expression<Func<TSender, T8>> property8, Expression<Func<TSender, T9>> property9, Expression<Func<TSender, T10>> property10, Expression<Func<TSender, T11>> property11, Expression<Func<TSender, T12>> property12, Func<IObservedChange<TSender, T1>, IObservedChange<TSender, T2>, IObservedChange<TSender, T3>, IObservedChange<TSender, T4>, IObservedChange<TSender, T5>, IObservedChange<TSender, T6>, IObservedChange<TSender, T7>, IObservedChange<TSender, T8>, IObservedChange<TSender, T9>, IObservedChange<TSender, T10>, IObservedChange<TSender, T11>, IObservedChange<TSender, T12>, TRet> selector, string property1Expression = "", string property2Expression = "", string property3Expression = "", string property4Expression = "", string property5Expression = "", string property6Expression = "", string property7Expression = "", string property8Expression = "", string property9Expression = "", string property10Expression = "", string property11Expression = "", string property12Expression = "", string callerFilePath = "", int callerLineNumber = 0)` | Observes 12 properties and emits the selector applied to their observed changes, on subscription and whenever any of them changes. Available for 1 to 12 repeated arguments; the largest form is listed. | `TSender : class`: The type of the sender to monitor for property changes; `TRet`: The return type of the selector; `T1`: The type of property 1 value; `T2`: The type of property 2 value; `T3`: The type of property 3 value; `T4`: The type of property 4 value; `T5`: The type of property 5 value; `T6`: The type of property 6 value; `T7`: The type of property 7 value; `T8`: The type of property 8 value; `T9`: The type of property 9 value; `T10`: The type of property 10 value; `T11`: The type of property 11 value; `T12`: The type of property 12 value; `TSender` `sender`: The sender instance to observe for property changes; [`Expression<Func<TSender, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects property 1 to observe; [`Expression<Func<TSender, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: An expression that selects property 2 to observe; [`Expression<Func<TSender, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: An expression that selects property 3 to observe; [`Expression<Func<TSender, T4>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property4`: An expression that selects property 4 to observe; [`Expression<Func<TSender, T5>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property5`: An expression that selects property 5 to observe; [`Expression<Func<TSender, T6>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property6`: An expression that selects property 6 to observe; [`Expression<Func<TSender, T7>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property7`: An expression that selects property 7 to observe; [`Expression<Func<TSender, T8>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property8`: An expression that selects property 8 to observe; [`Expression<Func<TSender, T9>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property9`: An expression that selects property 9 to observe; [`Expression<Func<TSender, T10>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property10`: An expression that selects property 10 to observe; [`Expression<Func<TSender, T11>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property11`: An expression that selects property 11 to observe; [`Expression<Func<TSender, T12>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property12`: An expression that selects property 12 to observe; [`Func<IObservedChange<TSender, T1>, IObservedChange<TSender, T2>, IObservedChange<TSender, T3>, IObservedChange<TSender, T4>, IObservedChange<TSender, T5>, IObservedChange<TSender, T6>, IObservedChange<TSender, T7>, IObservedChange<TSender, T8>, IObservedChange<TSender, T9>, IObservedChange<TSender, T10>, IObservedChange<TSender, T11>, IObservedChange<TSender, T12>, TRet>`](https://learn.microsoft.com/dotnet/api/system.func-13) `selector`: A function that combines the observed changes into a result; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property1Expression`: The caller argument expression for `property1`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property2Expression`: The caller argument expression for `property2`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property3Expression`: The caller argument expression for `property3`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property4Expression`: The caller argument expression for `property4`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property5Expression`: The caller argument expression for `property5`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property6Expression`: The caller argument expression for `property6`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property7Expression`: The caller argument expression for `property7`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property8Expression`: The caller argument expression for `property8`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property9Expression`: The caller argument expression for `property9`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property10Expression`: The caller argument expression for `property10`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property11Expression`: The caller argument expression for `property11`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `property12Expression`: The caller argument expression for `property12`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IObservable<TRet>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable of selector results, emitted on subscription and whenever an observed property changes. |
| `WhenAnyDynamic<TSender, TRet>(TSender sender, Expression? property1, Expression? property2, Expression? property3, Expression? property4, Expression? property5, Expression? property6, Expression? property7, Expression? property8, Expression? property9, Expression? property10, Expression? property11, Expression? property12, Func<IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, TRet> selector)` | Observes 12 property chains named by run-time expressions and emits the selector applied to their observed changes, on subscription and whenever any of them changes. Available for 1 to 12 repeated arguments; the largest form is listed. | `TSender : class`: The type of the object the chains are rooted on; `TRet`: The type of the projected result; `TSender` `sender`: The object the chains are rooted on; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property1`: An expression naming property 1; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property2`: An expression naming property 2; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property3`: An expression naming property 3; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property4`: An expression naming property 4; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property5`: An expression naming property 5; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property6`: An expression naming property 6; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property7`: An expression naming property 7; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property8`: An expression naming property 8; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property9`: An expression naming property 9; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property10`: An expression naming property 10; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property11`: An expression naming property 11; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property12`: An expression naming property 12; [`Func<IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, TRet>`](https://learn.microsoft.com/dotnet/api/system.func-13) `selector`: Projects the observed changes into a result | [`IObservable<TRet>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable of selector results. |
| `WhenAnyDynamic<TSender, TRet>(TSender sender, Expression? property1, Expression? property2, Expression? property3, Expression? property4, Expression? property5, Expression? property6, Expression? property7, Expression? property8, Expression? property9, Expression? property10, Expression? property11, Expression? property12, Func<IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, TRet> selector, bool isDistinct)` | Observes 12 property chains named by run-time expressions and emits the selector applied to their observed changes, on subscription and whenever any of them changes. Available for 1 to 12 repeated arguments; the largest form is listed. | `TSender : class`: The type of the object the chains are rooted on; `TRet`: The type of the projected result; `TSender` `sender`: The object the chains are rooted on; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property1`: An expression naming property 1; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property2`: An expression naming property 2; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property3`: An expression naming property 3; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property4`: An expression naming property 4; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property5`: An expression naming property 5; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property6`: An expression naming property 6; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property7`: An expression naming property 7; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property8`: An expression naming property 8; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property9`: An expression naming property 9; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property10`: An expression naming property 10; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property11`: An expression naming property 11; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `property12`: An expression naming property 12; [`Func<IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, IObservedChange<TSender, object?>, TRet>`](https://learn.microsoft.com/dotnet/api/system.func-13) `selector`: Projects the observed changes into a result; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `isDistinct`: Whether a chain reports only when its value changes | [`IObservable<TRet>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable of selector results. |

### WhenAnyObservable

[Full description and examples](observing.md).

Types: `ReactiveUI.Binding.ReactiveUIBindingExtensions`.

#### `ReactiveUIBindingExtensions`

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `WhenAnyObservable<TSender, TRet>(TSender sender, Expression<Func<TSender, IObservable<TRet>?>> obs1, Expression<Func<TSender, IObservable<TRet>?>> obs2, Expression<Func<TSender, IObservable<TRet>?>> obs3, Expression<Func<TSender, IObservable<TRet>?>> obs4, Expression<Func<TSender, IObservable<TRet>?>> obs5, Expression<Func<TSender, IObservable<TRet>?>> obs6, Expression<Func<TSender, IObservable<TRet>?>> obs7, Expression<Func<TSender, IObservable<TRet>?>> obs8, Expression<Func<TSender, IObservable<TRet>?>> obs9, Expression<Func<TSender, IObservable<TRet>?>> obs10, Expression<Func<TSender, IObservable<TRet>?>> obs11, Expression<Func<TSender, IObservable<TRet>?>> obs12, string obs1Expression = "", string obs2Expression = "", string obs3Expression = "", string obs4Expression = "", string obs5Expression = "", string obs6Expression = "", string obs7Expression = "", string obs8Expression = "", string obs9Expression = "", string obs10Expression = "", string obs11Expression = "", string obs12Expression = "", string callerFilePath = "", int callerLineNumber = 0)` | Observes 12 observable properties and merges the values of the observables they hold, switching each when its property changes; a null value emits nothing. Available for 1 to 12 repeated arguments; the largest form is listed. | `TSender : class`: The type of the sender to monitor for property changes; `TRet`: The element type of the observed observables; `TSender` `sender`: The sender instance to observe for property changes; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs1`: An expression that selects observable property 1 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs2`: An expression that selects observable property 2 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs3`: An expression that selects observable property 3 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs4`: An expression that selects observable property 4 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs5`: An expression that selects observable property 5 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs6`: An expression that selects observable property 6 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs7`: An expression that selects observable property 7 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs8`: An expression that selects observable property 8 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs9`: An expression that selects observable property 9 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs10`: An expression that selects observable property 10 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs11`: An expression that selects observable property 11 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs12`: An expression that selects observable property 12 to observe; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs1Expression`: The caller argument expression for `obs1`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs2Expression`: The caller argument expression for `obs2`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs3Expression`: The caller argument expression for `obs3`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs4Expression`: The caller argument expression for `obs4`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs5Expression`: The caller argument expression for `obs5`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs6Expression`: The caller argument expression for `obs6`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs7Expression`: The caller argument expression for `obs7`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs8Expression`: The caller argument expression for `obs8`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs9Expression`: The caller argument expression for `obs9`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs10Expression`: The caller argument expression for `obs10`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs11Expression`: The caller argument expression for `obs11`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs12Expression`: The caller argument expression for `obs12`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IObservable<TRet>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits the values of the latest observable each property held, merged into one sequence. |
| `WhenAnyObservable<TSender, TRet, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(TSender sender, Expression<Func<TSender, IObservable<T1>?>> obs1, Expression<Func<TSender, IObservable<T2>?>> obs2, Expression<Func<TSender, IObservable<T3>?>> obs3, Expression<Func<TSender, IObservable<T4>?>> obs4, Expression<Func<TSender, IObservable<T5>?>> obs5, Expression<Func<TSender, IObservable<T6>?>> obs6, Expression<Func<TSender, IObservable<T7>?>> obs7, Expression<Func<TSender, IObservable<T8>?>> obs8, Expression<Func<TSender, IObservable<T9>?>> obs9, Expression<Func<TSender, IObservable<T10>?>> obs10, Expression<Func<TSender, IObservable<T11>?>> obs11, Expression<Func<TSender, IObservable<T12>?>> obs12, Func<T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, T9?, T10?, T11?, T12?, TRet> selector, string obs1Expression = "", string obs2Expression = "", string obs3Expression = "", string obs4Expression = "", string obs5Expression = "", string obs6Expression = "", string obs7Expression = "", string obs8Expression = "", string obs9Expression = "", string obs10Expression = "", string obs11Expression = "", string obs12Expression = "", string callerFilePath = "", int callerLineNumber = 0)` | Observes 12 observable properties of different element types and emits the selector applied to the latest value of each, once every observable has produced a value. Available for 2 to 12 repeated arguments; the largest form is listed. | `TSender : class`: The type of the sender to monitor for property changes; `TRet`: The return type of the selector; `T1`: The element type of observable property 1; `T2`: The element type of observable property 2; `T3`: The element type of observable property 3; `T4`: The element type of observable property 4; `T5`: The element type of observable property 5; `T6`: The element type of observable property 6; `T7`: The element type of observable property 7; `T8`: The element type of observable property 8; `T9`: The element type of observable property 9; `T10`: The element type of observable property 10; `T11`: The element type of observable property 11; `T12`: The element type of observable property 12; `TSender` `sender`: The sender instance to observe for property changes; [`Expression<Func<TSender, IObservable<T1>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs1`: An expression that selects observable property 1 to observe; [`Expression<Func<TSender, IObservable<T2>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs2`: An expression that selects observable property 2 to observe; [`Expression<Func<TSender, IObservable<T3>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs3`: An expression that selects observable property 3 to observe; [`Expression<Func<TSender, IObservable<T4>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs4`: An expression that selects observable property 4 to observe; [`Expression<Func<TSender, IObservable<T5>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs5`: An expression that selects observable property 5 to observe; [`Expression<Func<TSender, IObservable<T6>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs6`: An expression that selects observable property 6 to observe; [`Expression<Func<TSender, IObservable<T7>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs7`: An expression that selects observable property 7 to observe; [`Expression<Func<TSender, IObservable<T8>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs8`: An expression that selects observable property 8 to observe; [`Expression<Func<TSender, IObservable<T9>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs9`: An expression that selects observable property 9 to observe; [`Expression<Func<TSender, IObservable<T10>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs10`: An expression that selects observable property 10 to observe; [`Expression<Func<TSender, IObservable<T11>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs11`: An expression that selects observable property 11 to observe; [`Expression<Func<TSender, IObservable<T12>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs12`: An expression that selects observable property 12 to observe; [`Func<T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, T9?, T10?, T11?, T12?, TRet>`](https://learn.microsoft.com/dotnet/api/system.func-13) `selector`: A function that combines the latest values from all observables; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs1Expression`: The caller argument expression for `obs1`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs2Expression`: The caller argument expression for `obs2`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs3Expression`: The caller argument expression for `obs3`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs4Expression`: The caller argument expression for `obs4`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs5Expression`: The caller argument expression for `obs5`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs6Expression`: The caller argument expression for `obs6`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs7Expression`: The caller argument expression for `obs7`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs8Expression`: The caller argument expression for `obs8`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs9Expression`: The caller argument expression for `obs9`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs10Expression`: The caller argument expression for `obs10`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs11Expression`: The caller argument expression for `obs11`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `obs12Expression`: The caller argument expression for `obs12`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IObservable<TRet>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits the selector result each time one of the observables produces a value, once every observable has produced one. |

### Property paths

[Full description and examples](observing.md).

Types: `ReactiveUI.Binding.ExpressionChainParameters<TSender>`, `ReactiveUI.Binding.Expressions.ExpressionMixins`, `ReactiveUI.Binding.Expressions.Reflection`, `ReactiveUI.Binding.ObservableForProperty.ExpressionChainSink<TSender, TValue>`, `ReactiveUI.Binding.ObservableForProperty.ObservableForPropertySink<TSender, TValue>`, `ReactiveUI.Binding.ObservableForProperty.ReactiveNotifyPropertyChangedMixins`.

#### `ExpressionChainParameters<TSender>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `readonly struct ExpressionChainParameters<TSender>` | How one chain of property accesses is to be observed. | `TSender`: The root sender type surfaced on the emitted change | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ExpressionChainParameters(TSender? Source, Expression? Expression, Expression[] Links, bool BeforeChange, bool SkipInitial, bool IsDistinct, bool SuppressWarnings)` | How one chain of property accesses is to be observed. | `TSender?` `Source`: The root object of the chain; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `Expression`: The full expression surfaced on the emitted change; [`Expression[]`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `Links`: The member-access links of the chain, in order; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `BeforeChange`: Whether values are observed before they change; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `SkipInitial`: Whether the first value the chain produces is dropped; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `IsDistinct`: Whether consecutive equal leaf values are suppressed; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `SuppressWarnings`: Whether the warning a property with no notification mechanism raises is suppressed | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BeforeChange { get; init; }` | Whether values are observed before they change. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) |
| `Expression { get; init; }` | The full expression surfaced on the emitted change. | None. | [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) |
| `IsDistinct { get; init; }` | Whether consecutive equal leaf values are suppressed. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) |
| `Links { get; init; }` | The member-access links of the chain, in order. | None. | [`Expression[]`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) |
| `SkipInitial { get; init; }` | Whether the first value the chain produces is dropped. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) |
| `Source { get; init; }` | The root object of the chain. | None. | `TSender?` |
| `SuppressWarnings { get; init; }` | Whether the warning a property with no notification mechanism raises is suppressed. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) |

#### `ExpressionMixins`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class ExpressionMixins` | Extension methods that decompose property-access expression trees. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Expression.GetArgumentsArray()` | Gets the constant arguments passed to an indexer expression. | [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression` (receiver) | [`object?[]?`](https://learn.microsoft.com/dotnet/api/system.object): The argument values, or null when the expression is not an index expression. |
| `Expression.GetExpressionChain()` | Gets the member accesses and indexer accesses that make up an expression, ordered from the root parameter outward. | [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression` (receiver) | [`IEnumerable<Expression>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1): The links in order, each rebased onto a fresh parameter of its parent's type; empty when the expression is a parameter or `null`. |
| `Expression.GetMemberInfo()` | Gets the member an index or member-access expression names, looking through conversions. | [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression` (receiver) | [`MemberInfo?`](https://learn.microsoft.com/dotnet/api/system.reflection.memberinfo): The property, field or indexer the expression names. |
| `Expression.GetParent()` | Gets the expression an index or member-access expression is read from. | [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression` (receiver) | [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression): The object expression, or null for a static member. |

#### `Reflection`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class Reflection` | Reads and writes members along a property expression chain by reflection. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ExpressionToPropertyNames(Expression? expression)` | Converts an expression that points to a property chain into a dotted path string, such as `A.B[0].C`. | [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The expression to generate the property names from; an indexer's arguments must be constants | [`string`](https://learn.microsoft.com/dotnet/api/system.string): The member names joined by dots, with an indexer written as its name followed by its arguments in brackets. |
| `GetValueFetcherForProperty(MemberInfo? member)` | Converts a [`MemberInfo`](https://learn.microsoft.com/dotnet/api/system.reflection.memberinfo) into a delegate which fetches the value for the member. | [`MemberInfo?`](https://learn.microsoft.com/dotnet/api/system.reflection.memberinfo) `member`: The member info to convert | [`Func<object?, object?[]?, object?>?`](https://learn.microsoft.com/dotnet/api/system.func-3): A delegate that fetches the value, or null when the member is neither a field nor a property. The delegate for a field throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) when the field holds null. |
| `GetValueFetcherOrThrow(MemberInfo? member)` | Converts a [`MemberInfo`](https://learn.microsoft.com/dotnet/api/system.reflection.memberinfo) into a delegate which fetches the value for the member. Throws if the member is not a field or property. | [`MemberInfo?`](https://learn.microsoft.com/dotnet/api/system.reflection.memberinfo) `member`: The member info to convert | [`Func<object?, object?[]?, object?>`](https://learn.microsoft.com/dotnet/api/system.func-3): A delegate that fetches the value. |
| `GetValueSetterForProperty(MemberInfo? member)` | Converts a [`MemberInfo`](https://learn.microsoft.com/dotnet/api/system.reflection.memberinfo) into a delegate which sets the value for the member. | [`MemberInfo?`](https://learn.microsoft.com/dotnet/api/system.reflection.memberinfo) `member`: The member info to convert | [`Action<object?, object?, object?[]?>?`](https://learn.microsoft.com/dotnet/api/system.action-3): A delegate that sets the value, or null when the member is neither a field nor a property. |
| `GetValueSetterOrThrow(MemberInfo? member)` | Converts a [`MemberInfo`](https://learn.microsoft.com/dotnet/api/system.reflection.memberinfo) into a delegate which sets the value for the member. Throws if the member is not a field or property. | [`MemberInfo?`](https://learn.microsoft.com/dotnet/api/system.reflection.memberinfo) `member`: The member info to convert | [`Action<object?, object?, object?[]?>`](https://learn.microsoft.com/dotnet/api/system.action-3): A delegate that sets the value. |
| `Rewrite(Expression? expression)` | Simplifies an expression with the shared expression rewriter. | [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The expression to rewrite | [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression): The rewritten expression, or `null` when `expression` is `null`. |
| `TryGetAllValuesForPropertyChain(out IObservedChange<object, object?>[] changeValues, object? current, IEnumerable<Expression> expressionChain)` | Attempts to get all intermediate values in a property chain as observed changes. | out [`IObservedChange<object, object?>[]`](observing.md) `changeValues`: Receives an array with one entry per expression in the chain; the entries from the first null link onward are null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `current`: The object that starts the property chain; [`IEnumerable<Expression>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1) `expressionChain`: A sequence of expressions that point to properties/fields | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): True if all values were retrieved; false when `current` or a property partway along the chain is null. |
| `TryGetValueForPropertyChain<TValue>(out TValue changeValue, object? current, IEnumerable<Expression> expressionChain)` | Attempts to get the value of the last property in an expression chain. | `TValue`: The expected type of the final value; out `TValue` `changeValue`: Receives the value if the chain can be evaluated; otherwise the default; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `current`: The object that starts the property chain; [`IEnumerable<Expression>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1) `expressionChain`: A sequence of expressions that point to properties/fields | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): True if the value was retrieved; false when `current` or a property partway along the chain is null. |
| `TrySetValueToPropertyChain<TValue>(object? target, IEnumerable<Expression> expressionChain, TValue value)` | Attempts to set the value of the last property in an expression chain, throwing when a chain member is not a field or property. | `TValue`: The type of the end value being set; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `target`: The object that starts the property chain; [`IEnumerable<Expression>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1) `expressionChain`: A sequence of expressions that point to properties/fields; `TValue` `value`: The value to set on the last property in the chain | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): True if the value was set; false when the object owning the last property is null. |
| `TrySetValueToPropertyChain<TValue>(object? target, IEnumerable<Expression> expressionChain, TValue value, bool shouldThrow)` | Attempts to set the value of the last property in an expression chain. | `TValue`: The type of the end value being set; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `target`: The object that starts the property chain; [`IEnumerable<Expression>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1) `expressionChain`: A sequence of expressions that point to properties/fields; `TValue` `value`: The value to set on the last property in the chain; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `shouldThrow`: If true, throw when a chain member is not a field or property; otherwise, an unreadable link is skipped and an unwritable last member returns false | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): True if the value was set; false when the object owning the last property is null or the last member cannot be written. |

#### `ExpressionChainSink<TSender, TValue>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ExpressionChainSink<TSender, TValue>` | Observes the leaf of an expression member chain (`x.A.B.C`) and emits its value as an observed change each time any link changes. | `TSender`: The root sender type surfaced on the emitted change; `TValue`: The leaf value type | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ExpressionChainSink(TSender? source, Expression? expression, Expression[] links, bool beforeChange, bool skipInitial, bool isDistinct, bool suppressWarnings)` | Initializes a new instance of the [`ExpressionChainSink<TSender, TValue>`](observing.md) class. | `TSender?` `source`: The root object of the chain; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The full expression surfaced on the emitted change; [`Expression[]`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `links`: The member-access links of the chain, in order; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChange`: Whether values are observed before they change; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `skipInitial`: Whether the first value the chain produces is dropped; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `isDistinct`: Whether consecutive equal leaf values are suppressed; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `suppressWarnings`: Whether the warning a property with no notification mechanism raises is suppressed | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Subscribe(IObserver<IObservedChange<TSender, TValue>> observer)` | Notifies the provider that an observer is to receive notifications. | [`IObserver<IObservedChange<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.iobserver-1) `observer`: The object that is to receive notifications | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them. |

#### `ObservableForPropertySink<TSender, TValue>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ObservableForPropertySink<TSender, TValue>` | Emits a property's value on subscribe, unless skipped, and again on each notification. | `TSender`: The type of the observed object surfaced on the emitted change; `TValue`: The property value type | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ObservableForPropertySink(TSender sender, Expression expression, IObservable<IObservedChange<object, object?>> notifications, Func<TValue> readValue, bool skipInitial, bool isDistinct)` | Initializes a new instance of the [`ObservableForPropertySink<TSender, TValue>`](observing.md) class. | `TSender` `sender`: The observed object surfaced on the emitted change; [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The expression surfaced on the emitted change; [`IObservable<IObservedChange<object, object?>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `notifications`: The underlying change-notification source; [`Func<TValue>`](https://learn.microsoft.com/dotnet/api/system.func-1) `readValue`: Reads the current property value from the sender; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `skipInitial`: When `true`, the current value is not emitted on subscribe; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `isDistinct`: When `true`, consecutive equal values are suppressed | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Subscribe(IObserver<IObservedChange<TSender, TValue>> observer)` | Notifies the provider that an observer is to receive notifications. | [`IObserver<IObservedChange<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.iobserver-1) `observer`: The object that is to receive notifications | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them. |

#### `ReactiveNotifyPropertyChangedMixins`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class ReactiveNotifyPropertyChangedMixins` | Extension methods that observe a property, by name or through an expression chain, using the [`ICreatesObservableForProperty`](mechanisms.md) registrations from the service locator. Generated code does not call them. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ObservableForProperty<TSender, TValue>(TSender? item, Expression<Func<TSender, TValue>> property)` | Observes the property an expression points at after it changes, emitting its current value first and dropping consecutive equal values. | `TSender`; `TValue`: The value type; `TSender?` `item`; [`Expression<Func<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property`: An Expression representing the property | [`IObservable<IObservedChange<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable of the changes for the property. |
| `ObservableForProperty<TSender, TValue>(TSender? item, string propertyName)` | Observes a property by name after it changes, emitting its current value first and dropping consecutive equal values. | `TSender`; `TValue`: The value type; `TSender?` `item`; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name to observe | [`IObservable<IObservedChange<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable of the changes for the property. |
| `ObservableForProperty<TSender, TValue>(TSender? item, Expression<Func<TSender, TValue>> property, bool skipInitial)` | Observes the property an expression points at after it changes, dropping consecutive equal values and optionally skipping the current value emitted on subscription. | `TSender`; `TValue`: The value type; `TSender?` `item`; [`Expression<Func<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property`: An Expression representing the property; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `skipInitial`: If true, the Observable will not notify with the initial value | [`IObservable<IObservedChange<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable of the changes for the property. |
| `ObservableForProperty<TSender, TValue>(TSender? item, string propertyName, bool skipInitial)` | Observes a property by name after it changes, dropping consecutive equal values and optionally skipping the current value emitted on subscription. | `TSender`; `TValue`: The value type; `TSender?` `item`; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name to observe; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `skipInitial`: If true, the Observable will not notify with the initial value | [`IObservable<IObservedChange<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable of the changes for the property. |
| `ObservableForProperty<TSender, TValue>(TSender? item, Expression<Func<TSender, TValue>> property, bool beforeChange, bool skipInitial, bool isDistinct)` | Observes the property, or the member chain, an expression points at. | `TSender`; `TValue`: The value type; `TSender?` `item`; [`Expression<Func<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property`: An Expression representing the property; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChange`: If true, the Observable will notify immediately before a property is going to change; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `skipInitial`: If true, the Observable will not notify with the initial value; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `isDistinct`: If set to true, values are filtered with DistinctUntilChanged | [`IObservable<IObservedChange<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable of the changes for the last link of the chain. |
| `ObservableForProperty<TSender, TValue>(TSender? item, string propertyName, bool beforeChange, bool skipInitial, bool isDistinct)` | Observes a property by name through the highest-affinity [`ICreatesObservableForProperty`](mechanisms.md) registered for the source's runtime type. | `TSender`; `TValue`: The value type; `TSender?` `item`; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name to observe; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChange`: If true, the Observable will notify immediately before a property is going to change; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `skipInitial`: If true, the Observable will not notify with the initial value; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `isDistinct`: If set to true, values are filtered with DistinctUntilChanged | [`IObservable<IObservedChange<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable of the changes for the property; each value is read by reflection from the public instance property, or is the default when that is missing or null. |
| `SubscribeToExpressionChain<TSender, TValue>(TSender? item, Expression? expression)` | Observes an expression's member chain after each link changes, emitting the end value first and dropping consecutive equal values. | `TSender`; `TValue`: The end value we want to subscribe to; `TSender?` `item`; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: An expression which will point towards the property | [`IObservable<IObservedChange<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable which notifies about observed changes. |
| `SubscribeToExpressionChain<TSender, TValue>(TSender? item, Expression? expression, bool skipInitial)` | Observes an expression's member chain after each link changes, dropping consecutive equal values and optionally skipping the end value emitted on subscription. | `TSender`; `TValue`: The end value we want to subscribe to; `TSender?` `item`; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: An expression which will point towards the property; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `skipInitial`: If we don't want to get a notification about the default value of the property | [`IObservable<IObservedChange<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable which notifies about observed changes. |
| `SubscribeToExpressionChain<TSender, TValue>(TSender? item, Expression? expression, bool beforeChange, bool skipInitial, bool isDistinct)` | Observes each property in an expression's member chain, re-subscribing deeper links when an intermediate value changes, and reports the value at the end of the chain. | `TSender`; `TValue`: The end value we want to subscribe to; `TSender?` `item`; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: An expression which will point towards the property; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChange`: If we are interested in notifications before the property value is changed; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `skipInitial`: If we don't want to get a notification about the default value of the property; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `isDistinct`: If set to true, values are filtered with DistinctUntilChanged | [`IObservable<IObservedChange<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable which notifies about observed changes. |
| `SubscribeToExpressionChain<TSender, TValue>(TSender? item, Expression? expression, bool beforeChange, bool skipInitial, bool isDistinct, bool suppressWarnings)` | Observes each property in an expression's member chain, re-subscribing deeper links when an intermediate value changes, and reports the value at the end of the chain. | `TSender`; `TValue`: The end value we want to subscribe to; `TSender?` `item`; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: An expression which will point towards the property; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChange`: If we are interested in notifications before the property value is changed; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `skipInitial`: If we don't want to get a notification about the default value of the property; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `isDistinct`: If set to true, values are filtered with DistinctUntilChanged; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `suppressWarnings`: If set to true, a property that cannot notify is observed quietly | [`IObservable<IObservedChange<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable which notifies about observed changes. |

### Observed changes

[Full description and examples](observing.md).

Types: `ReactiveUI.Binding.IObservedChange<TSender, TValue>`, `ReactiveUI.Binding.ObservableForProperty.ObservedChangedMixins`, `ReactiveUI.Binding.ObservedChange<TSender, TValue>`.

#### `IObservedChange<TSender, TValue>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IObservedChange<TSender, TValue>` | Describes one property change: the object that raised it, the expression naming the property, and its value. | `TSender`: The type of the object that raised the change; `TValue`: The type of the property value | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Expression { get; }` | Gets the expression of the member that changed. | None. | [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) |
| `Sender { get; }` | Gets the object that raised the change. | None. | `TSender` |
| `Value { get; }` | Gets the value the change carries; a source that raises a notification without reading the property leaves it at the default. | None. | `TValue` |

#### `ObservedChangedMixins`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class ObservedChangedMixins` | A collection of helpers for [`IObservedChange<TSender, TValue>`](observing.md). | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `IObservedChange<TSender, TValue>.GetPropertyName<TSender, TValue>()` | Returns the dotted path of the property the change describes, such as `A.B[0].C`. | `TSender`; `TValue`; [`IObservedChange<TSender, TValue>`](observing.md) `item` (receiver) | [`string`](https://learn.microsoft.com/dotnet/api/system.string): The property path built from the change's expression. |
| `IObservedChange<TSender, TValue>.GetValue<TSender, TValue>()` | Returns the value carried by the change, or reads it from the sender through the change's expression when the carried value is the default. | `TSender`; `TValue`; [`IObservedChange<TSender, TValue>`](observing.md) `item` (receiver) | `TValue`: The current value of the property. |
| `IObservedChange<TSender, TValue>.GetValueOrDefault<TSender, TValue>()` | Returns the value `GetValue` would return, or the default when a property partway along the expression is `null`. | `TSender`; `TValue`; [`IObservedChange<TSender, TValue>`](observing.md) `item` (receiver) | `TValue?`: The current value of the property, or default. |
| `IObservable<IObservedChange<TSender, TValue>>.Value<TSender, TValue>()` | Projects each observed change to the current value of the property it describes. | `TSender`; `TValue`; [`IObservable<IObservedChange<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `stream` (receiver) | [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable of the current values; a change whose expression cannot be followed faults it with an [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception). |

#### `ObservedChange<TSender, TValue>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `class ObservedChange<TSender, TValue>` | Concrete implementation of [`IObservedChange<TSender, TValue>`](observing.md). | `TSender`: The type of the object that raised the change; `TValue`: The type of the property value | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ObservedChange(TSender sender, Expression? expression, TValue value)` | Initializes a new instance of the [`ObservedChange<TSender, TValue>`](observing.md) class. | `TSender` `sender`: The object that raised the change; [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The expression of the member that changed; `TValue` `value`: The value of the property; the default when the reader is to fetch it from `sender` | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Expression { get; }` | Gets the expression of the member that changed. | None. | [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) |
| `Sender { get; }` | Gets the object that raised the change. | None. | `TSender` |
| `Value { get; }` | Gets the value the change carries; a source that raises a notification without reading the property leaves it at the default. | None. | `TValue` |

### Property values

[Full description and examples](observing.md).

Types: `ReactiveUI.Binding.PropertyValues<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>`.

#### `PropertyValues<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `readonly struct PropertyValues<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>` | The values of 16 observed properties, as one emission. Available for 2 to 16 type arguments; the largest form is listed. | `T1`: The type of the first observed property; `T2`: The type of the second observed property; `T3`: The type of the third observed property; `T4`: The type of the fourth observed property; `T5`: The type of the fifth observed property; `T6`: The type of the sixth observed property; `T7`: The type of the seventh observed property; `T8`: The type of the eighth observed property; `T9`: The type of the ninth observed property; `T10`: The type of the tenth observed property; `T11`: The type of the eleventh observed property; `T12`: The type of the twelfth observed property; `T13`: The type of the thirteenth observed property; `T14`: The type of the fourteenth observed property; `T15`: The type of the fifteenth observed property; `T16`: The type of the sixteenth observed property | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `PropertyValues(T1 Property1, T2 Property2, T3 Property3, T4 Property4, T5 Property5, T6 Property6, T7 Property7, T8 Property8, T9 Property9, T10 Property10, T11 Property11, T12 Property12, T13 Property13, T14 Property14, T15 Property15, T16 Property16)` | The values of 16 observed properties, as one emission. | `T1` `Property1`: The value of the first observed property; `T2` `Property2`: The value of the second observed property; `T3` `Property3`: The value of the third observed property; `T4` `Property4`: The value of the fourth observed property; `T5` `Property5`: The value of the fifth observed property; `T6` `Property6`: The value of the sixth observed property; `T7` `Property7`: The value of the seventh observed property; `T8` `Property8`: The value of the eighth observed property; `T9` `Property9`: The value of the ninth observed property; `T10` `Property10`: The value of the tenth observed property; `T11` `Property11`: The value of the eleventh observed property; `T12` `Property12`: The value of the twelfth observed property; `T13` `Property13`: The value of the thirteenth observed property; `T14` `Property14`: The value of the fourteenth observed property; `T15` `Property15`: The value of the fifteenth observed property; `T16` `Property16`: The value of the sixteenth observed property | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Property1 { get; init; }` | The value of the first observed property. | None. | `T1` |
| `Property10 { get; init; }` | The value of the tenth observed property. | None. | `T10` |
| `Property11 { get; init; }` | The value of the eleventh observed property. | None. | `T11` |
| `Property12 { get; init; }` | The value of the twelfth observed property. | None. | `T12` |
| `Property13 { get; init; }` | The value of the thirteenth observed property. | None. | `T13` |
| `Property14 { get; init; }` | The value of the fourteenth observed property. | None. | `T14` |
| `Property15 { get; init; }` | The value of the fifteenth observed property. | None. | `T15` |
| `Property16 { get; init; }` | The value of the sixteenth observed property. | None. | `T16` |
| `Property2 { get; init; }` | The value of the second observed property. | None. | `T2` |
| `Property3 { get; init; }` | The value of the third observed property. | None. | `T3` |
| `Property4 { get; init; }` | The value of the fourth observed property. | None. | `T4` |
| `Property5 { get; init; }` | The value of the fifth observed property. | None. | `T5` |
| `Property6 { get; init; }` | The value of the sixth observed property. | None. | `T6` |
| `Property7 { get; init; }` | The value of the seventh observed property. | None. | `T7` |
| `Property8 { get; init; }` | The value of the eighth observed property. | None. | `T8` |
| `Property9 { get; init; }` | The value of the ninth observed property. | None. | `T9` |

## Properties

[Full description and examples](properties.md).

Types: `ReactiveUI.Binding.ReactiveUIBindingExtensions`, `ReactiveUI.Binding.ObservableAsPropertyHelper<T>`, `ReactiveUI.Binding.ObservableAsPropertyAttribute`.

#### `ReactiveUIBindingExtensions`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class ReactiveUIBindingExtensions` | Extension methods for binding commands from a view model to controls on a view. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static ObservableAsPropertyHelper<TRet> ToProperty<TObj, TRet>(this IObservable<TRet> target, TObj source, Func<TObj, TRet> property, ...)` | Backs the selected read-only property with the observable's latest value. | Extension method on `IObservable<TRet>`. `source` is `TObj : class`; `property` is a selector `x => x.Property`, or a constant `string` name. Optional overloads add `TRet initialValue`, a `Func<TRet> getInitialValue` factory, `bool deferSubscription`, an `ISequencer? scheduler`, and an `out ObservableAsPropertyHelper<TRet> result`. | [`ObservableAsPropertyHelper<TRet>`](#observableaspropertyhelpert). |

#### `ObservableAsPropertyAttribute`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ObservableAsPropertyAttribute : Attribute` | Marks a partial, get-only property as backed by an `ObservableAsPropertyHelper<T>`. | Attribute target: `Property`. `AllowMultiple = false`. | — |

#### `ObservableAsPropertyHelper<T>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ObservableAsPropertyHelper<T> : IDisposable` | Backs a read-only "output property" with an observable. | `T` is the property value's type. | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `T Value { get; }` | The current value, subscribing first when subscription was deferred. | None. | `T` |
| `bool IsSubscribed { get; }` | Whether the helper has subscribed to its observable. | None. | `bool` |
| `IObservable<Exception> ThrownExceptions { get; }` | Reports each error the source produces. | None. | [`IObservable<Exception>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ObservableAsPropertyHelper(IObservable<T?> observable, Action<T?> onChanged, ...)` | Constructs a helper directly. Overloads add `Action<T?>? onChanging`, `T? initialValue` or `Func<T?>? getInitialValue`, `bool deferSubscription`, and `ISequencer? scheduler`. | See [Properties](properties.md#construct-the-helper-directly). | — |
| `static ObservableAsPropertyHelper<T> Default()` / `Default(T? initialValue)` / `Default(T? initialValue, ISequencer? scheduler)` | Creates a helper that holds a fixed value and never subscribes to anything. | Optional `initialValue`, `scheduler`. | `ObservableAsPropertyHelper<T>` |
| `void Dispose()` | Stops following the observable; the property keeps its last value. | None. | — |

## Bindings

### One-way bindings

[Full description and examples](bindings.md).

Types: `ReactiveUI.Binding.ReactiveSchedulerExtensions`, `ReactiveUI.Binding.ReactiveUIBindingExtensions`.

#### `ReactiveSchedulerExtensions`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class ReactiveSchedulerExtensions` | Binding extension methods that deliver writes on a caller-supplied scheduler. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BindOneWay<TSource, TTarget, TProperty>(TSource source, TTarget target, Expression<Func<TSource, TProperty>> sourceProperty, Expression<Func<TTarget, TProperty>> targetProperty, ISequencer? scheduler, string sourcePropertyExpression = "", string targetPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Creates a one-way binding from a source property to a target property with a specified scheduler. | `TSource : class`; `TTarget : class`: The type of the target object; `TProperty : notnull`: The type of the property being bound; `TSource` `source`; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; `ISequencer?` `scheduler`: The scheduler the write to the target is delivered on, in place of the target's owning thread; an immediate scheduler writes inline; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `sourcePropertyExpression`: The caller argument expression for `sourceProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `targetPropertyExpression`: The caller argument expression for `targetProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindOneWay<TSource, TSourceProp, TTarget, TTargetProp>(TSource source, TTarget target, Expression<Func<TSource, TSourceProp>> sourceProperty, Expression<Func<TTarget, TTargetProp>> targetProperty, IBindingTypeConverter converter, object? conversionHint = null, ISequencer? scheduler = null, string callerFilePath = "", int callerLineNumber = 0)` | Creates a one-way binding from a source property to a target property using an explicit [`IBindingTypeConverter`](converters.md). | `TSource : class`; `TSourceProp : notnull`: The type of the source property; `TTarget : class`: The type of the target object; `TTargetProp : notnull`: The type of the target property; `TSource` `source`; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TSourceProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TTargetProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; [`IBindingTypeConverter`](converters.md) `converter`: The binding type converter to use for converting between source and target types; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: An optional hint passed to the converter (e.g., format string); `ISequencer?` `scheduler`: The scheduler the write to the target is delivered on, in place of the target's owning thread; an immediate scheduler writes inline; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindOneWay<TSource, TSourceProp, TTarget, TTargetProp>(TSource source, TTarget target, Expression<Func<TSource, TSourceProp>> sourceProperty, Expression<Func<TTarget, TTargetProp>> targetProperty, Func<TSourceProp, TTargetProp> conversionFunc, ISequencer? scheduler, string sourcePropertyExpression = "", string targetPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Creates a one-way binding from a source property to a target property with a conversion function and a specified scheduler. | `TSource : class`; `TSourceProp : notnull`: The type of the source property; `TTarget : class`: The type of the target object; `TTargetProp : notnull`: The type of the target property; `TSource` `source`; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TSourceProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TTargetProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; [`Func<TSourceProp, TTargetProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `conversionFunc`: A function that converts the source property value to the target property type; `ISequencer?` `scheduler`: The scheduler the write to the target is delivered on, in place of the target's owning thread; an immediate scheduler writes inline; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `sourcePropertyExpression`: The caller argument expression for `sourceProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `targetPropertyExpression`: The caller argument expression for `targetProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `OneWayBind<TView, TViewModel, TVMProp, TVProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty, IBindingTypeConverter converter, object? conversionHint = null, ISequencer? scheduler = null, string callerFilePath = "", int callerLineNumber = 0)` | Creates a one-way binding from a view model property to a view property using an explicit [`IBindingTypeConverter`](converters.md). | `TView : class, IViewFor`; `TViewModel : class`: The type of the view model; `TVMProp : notnull`: The type of the view model property; `TVProp : notnull`: The type of the view property; `TView` `view`; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update; [`IBindingTypeConverter`](converters.md) `converter`: The binding type converter to use for converting between source and target types; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: An optional hint passed to the converter (e.g., format string); `ISequencer?` `scheduler`: The scheduler the write to the view is delivered on, in place of the view's owning thread; an immediate scheduler writes inline; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IReactiveBinding<TView, TVProp>`](bindings.md): A reactive binding that can be disposed to disconnect the binding. |
| `OneWayBind<TView, TViewModel, TProp, TOut>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TProp>> viewModelProperty, Expression<Func<TView, TOut>> viewProperty, Func<TProp, TOut> selector, ISequencer? scheduler, string viewModelPropertyExpression = "", string viewPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Creates a one-way binding from a view model property to a view property with a specified selector and scheduler. | `TView : class, IViewFor`; `TViewModel : class`: The type of the view model; `TProp : notnull`: The type of the view model property; `TOut : notnull`: The type of the view property; `TView` `view`; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TOut>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update; [`Func<TProp, TOut>`](https://learn.microsoft.com/dotnet/api/system.func-2) `selector`: A function that converts the view model property value to the view property type; `ISequencer?` `scheduler`: The scheduler the write to the view is delivered on, in place of the view's owning thread; an immediate scheduler writes inline; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `viewModelPropertyExpression`: The caller argument expression for `viewModelProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `viewPropertyExpression`: The caller argument expression for `viewProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IReactiveBinding<TView, TOut>`](bindings.md): A reactive binding that can be disposed to disconnect the binding. |

#### `ReactiveUIBindingExtensions`

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BindOneWay<TSource, TTarget, TProperty>(TSource source, TTarget target, Expression<Func<TSource, TProperty>> sourceProperty, Expression<Func<TTarget, TProperty>> targetProperty, string sourcePropertyExpression = "", string targetPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Binds a source property to a target property one way, writing the current value and each later change on the target's owning thread. | `TSource : class`: The type of the source object; `TTarget : class`: The type of the target object; `TProperty`: The type of the property being bound; `TSource` `source`: The source object to observe for property changes; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `sourcePropertyExpression`: The caller argument expression for `sourceProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `targetPropertyExpression`: The caller argument expression for `targetProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding; a binding hook that vetoes the binding leaves nothing bound. |
| `BindOneWay<TSource, TSourceProp, TTarget, TTargetProp>(TSource source, TTarget target, Expression<Func<TSource, TSourceProp>> sourceProperty, Expression<Func<TTarget, TTargetProp>> targetProperty, Func<TSourceProp, TTargetProp> conversionFunc, string sourcePropertyExpression = "", string targetPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Binds a source property to a target property one way through a conversion function, writing on the target's owning thread. | `TSource : class`: The type of the source object; `TSourceProp`: The type of the source property; `TTarget : class`: The type of the target object; `TTargetProp`: The type of the target property; `TSource` `source`: The source object to observe for property changes; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TSourceProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TTargetProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; [`Func<TSourceProp, TTargetProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `conversionFunc`: A function that converts the source property value to the target property type; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `sourcePropertyExpression`: The caller argument expression for `sourceProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `targetPropertyExpression`: The caller argument expression for `targetProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding; a binding hook that vetoes the binding leaves nothing bound. |
| `OneWayBind<TViewModel, TView, TVMProp, TVProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty, string viewModelPropertyExpression = "", string viewPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Binds a view model property to a view property one way, writing the current value and each later change on the view's owning thread. | `TViewModel : class`: The type of the view model; `TView : class, IViewFor`: The type of the view; `TVMProp`: The type of the view model property; `TVProp`: The type of the view property; `TView` `view`: The view to bind to; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `viewModelPropertyExpression`: The caller argument expression for `viewModelProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `viewPropertyExpression`: The caller argument expression for `viewProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IReactiveBinding<TView, TVProp>`](bindings.md): A reactive binding that can be disposed to disconnect the binding, or null when a binding hook vetoes it. |
| `OneWayBind<TViewModel, TView, TProp, TOut>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TProp>> viewModelProperty, Expression<Func<TView, TOut>> viewProperty, Func<TProp, TOut> selector, string viewModelPropertyExpression = "", string viewPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Binds a view model property to a view property one way through a selector, writing on the view's owning thread. | `TViewModel : class`: The type of the view model; `TView : class, IViewFor`: The type of the view; `TProp`: The type of the view model property; `TOut`: The type of the view property; `TView` `view`: The view to bind to; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TOut>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update; [`Func<TProp, TOut>`](https://learn.microsoft.com/dotnet/api/system.func-2) `selector`: A function that converts the view model property value to the view property type; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `viewModelPropertyExpression`: The caller argument expression for `viewModelProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `viewPropertyExpression`: The caller argument expression for `viewProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IReactiveBinding<TView, TOut>`](bindings.md): A reactive binding that can be disposed to disconnect the binding, or null when a binding hook vetoes it. |

### Two-way bindings

[Full description and examples](bindings.md).

Types: `ReactiveUI.Binding.ReactiveSchedulerExtensions`, `ReactiveUI.Binding.ReactiveUIBindingExtensions`.

#### `ReactiveSchedulerExtensions`

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Bind<TView, TViewModel, TVMProp, TVProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty, IBindingTypeConverter viewModelToViewConverter, IBindingTypeConverter viewToViewModelConverter, object? conversionHint = null, ISequencer? scheduler = null, string callerFilePath = "", int callerLineNumber = 0)` | Creates a two-way binding between a view model property and a view property using explicit [`IBindingTypeConverter`](converters.md) instances. | `TView : class, IViewFor`; `TViewModel : class`: The type of the view model; `TVMProp : notnull`: The type of the view model property; `TVProp : notnull`: The type of the view property; `TView` `view`; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update; [`IBindingTypeConverter`](converters.md) `viewModelToViewConverter`: The converter for view model-to-view conversion; [`IBindingTypeConverter`](converters.md) `viewToViewModelConverter`: The converter for view-to-view model conversion; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: An optional hint passed to the converters (e.g., format string); `ISequencer?` `scheduler`: The scheduler each write is delivered on, in place of the view's owning thread; an immediate scheduler writes inline; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IReactiveBinding<TView, BindingChange>`](bindings.md): A reactive binding that can be disposed to disconnect the binding. |
| `Bind<TView, TViewModel, TVMProp, TVProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty, Func<TVMProp, TVProp> viewModelToViewConverter, Func<TVProp, TVMProp> viewToViewModelConverter, ISequencer? scheduler, string viewModelPropertyExpression = "", string viewPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Creates a two-way binding between a view model property and a view property with conversion functions and a specified scheduler. | `TView : class, IViewFor`; `TViewModel : class`: The type of the view model; `TVMProp : notnull`: The type of the view model property; `TVProp : notnull`: The type of the view property; `TView` `view`; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update; [`Func<TVMProp, TVProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `viewModelToViewConverter`: A function that converts the view model property value to the view property type; [`Func<TVProp, TVMProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `viewToViewModelConverter`: A function that converts the view property value back to the view model property type; `ISequencer?` `scheduler`: The scheduler each write is delivered on, in place of the view's owning thread; an immediate scheduler writes inline; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `viewModelPropertyExpression`: The caller argument expression for `viewModelProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `viewPropertyExpression`: The caller argument expression for `viewProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IReactiveBinding<TView, BindingChange>`](bindings.md): A reactive binding that can be disposed to disconnect the binding. |
| `BindTwoWay<TSource, TTarget, TProperty>(TSource source, TTarget target, Expression<Func<TSource, TProperty>> sourceProperty, Expression<Func<TTarget, TProperty>> targetProperty, ISequencer? scheduler, string sourcePropertyExpression = "", string targetPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Creates a two-way binding between a source property and a target property with a specified scheduler. | `TSource : class`; `TTarget : class`: The type of the target object; `TProperty : notnull`: The type of the property being bound; `TSource` `source`; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; `ISequencer?` `scheduler`: The scheduler each write is delivered on, in place of the target's owning thread; an immediate scheduler writes inline; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `sourcePropertyExpression`: The caller argument expression for `sourceProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `targetPropertyExpression`: The caller argument expression for `targetProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindTwoWay<TSource, TSourceProp, TTarget, TTargetProp>(TSource source, TTarget target, Expression<Func<TSource, TSourceProp>> sourceProperty, Expression<Func<TTarget, TTargetProp>> targetProperty, IBindingTypeConverter sourceToTargetConverter, IBindingTypeConverter targetToSourceConverter, object? conversionHint = null, ISequencer? scheduler = null, string callerFilePath = "", int callerLineNumber = 0)` | Creates a two-way binding between a source property and a target property using explicit [`IBindingTypeConverter`](converters.md) instances. | `TSource : class`; `TSourceProp : notnull`: The type of the source property; `TTarget : class`: The type of the target object; `TTargetProp : notnull`: The type of the target property; `TSource` `source`; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TSourceProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TTargetProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; [`IBindingTypeConverter`](converters.md) `sourceToTargetConverter`: The converter for source-to-target conversion; [`IBindingTypeConverter`](converters.md) `targetToSourceConverter`: The converter for target-to-source conversion; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: An optional hint passed to the converters (e.g., format string); `ISequencer?` `scheduler`: The scheduler each write is delivered on, in place of the target's owning thread; an immediate scheduler writes inline; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindTwoWay<TSource, TSourceProp, TTarget, TTargetProp>(TSource source, TTarget target, Expression<Func<TSource, TSourceProp>> sourceProperty, Expression<Func<TTarget, TTargetProp>> targetProperty, Func<TSourceProp, TTargetProp> sourceToTargetConv, Func<TTargetProp, TSourceProp> targetToSourceConv, ISequencer? scheduler, string sourcePropertyExpression = "", string targetPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Creates a two-way binding between a source property and a target property with conversion functions and a specified scheduler. | `TSource : class`; `TSourceProp : notnull`: The type of the source property; `TTarget : class`: The type of the target object; `TTargetProp : notnull`: The type of the target property; `TSource` `source`; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TSourceProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TTargetProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; [`Func<TSourceProp, TTargetProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `sourceToTargetConv`: A function that converts the source property value to the target property type; [`Func<TTargetProp, TSourceProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `targetToSourceConv`: A function that converts the target property value back to the source property type; `ISequencer?` `scheduler`: The scheduler each write is delivered on, in place of the target's owning thread; an immediate scheduler writes inline; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `sourcePropertyExpression`: The caller argument expression for `sourceProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `targetPropertyExpression`: The caller argument expression for `targetProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |

#### `ReactiveUIBindingExtensions`

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Bind<TViewModel, TView, TVMProp, TVProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty, string viewModelPropertyExpression = "", string viewPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Binds a view model property and a view property to each other, seeding the view and mirroring each change on the other side's owning thread. | `TViewModel : class`: The type of the view model; `TView : class, IViewFor`: The type of the view; `TVMProp`: The type of the view model property; `TVProp`: The type of the view property; `TView` `view`: The view to bind to; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `viewModelPropertyExpression`: The caller argument expression for `viewModelProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `viewPropertyExpression`: The caller argument expression for `viewProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IReactiveBinding<TView, BindingChange>`](bindings.md): A reactive binding that can be disposed to disconnect the binding, or null when a binding hook vetoes it. |
| `Bind<TViewModel, TView, TVMProp, TVProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty, Func<TVMProp, TVProp> viewModelToViewConverter, Func<TVProp, TVMProp> viewToViewModelConverter, string viewModelPropertyExpression = "", string viewPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Binds a view model property and a view property to each other through conversion functions, seeding the view and mirroring each change on the other side's owning thread. | `TViewModel : class`: The type of the view model; `TView : class, IViewFor`: The type of the view; `TVMProp`: The type of the view model property; `TVProp`: The type of the view property; `TView` `view`: The view to bind to; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update; [`Func<TVMProp, TVProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `viewModelToViewConverter`: A function that converts the view model property value to the view property type; [`Func<TVProp, TVMProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `viewToViewModelConverter`: A function that converts the view property value back to the view model property type; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `viewModelPropertyExpression`: The caller argument expression for `viewModelProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `viewPropertyExpression`: The caller argument expression for `viewProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IReactiveBinding<TView, BindingChange>`](bindings.md): A reactive binding that can be disposed to disconnect the binding, or null when a binding hook vetoes it. |
| `BindTwoWay<TSource, TTarget, TProperty>(TSource source, TTarget target, Expression<Func<TSource, TProperty>> sourceProperty, Expression<Func<TTarget, TProperty>> targetProperty, string sourcePropertyExpression = "", string targetPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Binds a source and a target property to each other, seeding the target from the source and mirroring each change on the other side's owning thread. | `TSource : class`: The type of the source object; `TTarget : class`: The type of the target object; `TProperty`: The type of the property being bound; `TSource` `source`: The source object to observe for property changes; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `sourcePropertyExpression`: The caller argument expression for `sourceProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `targetPropertyExpression`: The caller argument expression for `targetProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding; a binding hook that vetoes the binding leaves nothing bound. |
| `BindTwoWay<TSource, TSourceProp, TTarget, TTargetProp>(TSource source, TTarget target, Expression<Func<TSource, TSourceProp>> sourceProperty, Expression<Func<TTarget, TTargetProp>> targetProperty, Func<TSourceProp, TTargetProp> sourceToTargetConv, Func<TTargetProp, TSourceProp> targetToSourceConv, string sourcePropertyExpression = "", string targetPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Binds a source and a target property to each other through conversion functions, seeding the target and mirroring each change on the other side's owning thread. | `TSource : class`: The type of the source object; `TSourceProp`: The type of the source property; `TTarget : class`: The type of the target object; `TTargetProp`: The type of the target property; `TSource` `source`: The source object to observe for property changes; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TSourceProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TTargetProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; [`Func<TSourceProp, TTargetProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `sourceToTargetConv`: A function that converts the source property value to the target property type; [`Func<TTargetProp, TSourceProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `targetToSourceConv`: A function that converts the target property value back to the source property type; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `sourcePropertyExpression`: The caller argument expression for `sourceProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `targetPropertyExpression`: The caller argument expression for `targetProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding; a binding hook that vetoes the binding leaves nothing bound. |

### BindTo

[Full description and examples](bindings.md).

Types: `ReactiveUI.Binding.ReactiveUIBindingExtensions`.

#### `ReactiveUIBindingExtensions`

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `IObservable<TValue>.BindTo<TValue, TTarget, TTargetValue>(TTarget? target, Expression<Func<TTarget, TTargetValue?>> property, string propertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Writes each value the source produces to a target property, on the target's owning thread when its platform has one. | `TValue`: The type of the value produced by the source observable; `TTarget : class`: The type of the target object; `TTargetValue`: The type of the property on the target object; [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source` (receiver): The observable stream to bind to a target property; `TTarget?` `target`: The target object whose property will be set; [`Expression<Func<TTarget, TTargetValue?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property`: An expression that selects the target property to set; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyExpression`: The caller argument expression for `property`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `IObservable<TValue>.BindTo<TValue, TTarget, TTargetValue>(TTarget? target, Expression<Func<TTarget, TTargetValue?>> property, IBindingTypeConverter? converterOverride, string propertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Writes each value the source produces to a target property, coercing it with the supplied converter instead of a registered one. | `TValue`: The type of the value produced by the source observable; `TTarget : class`: The type of the target object; `TTargetValue`: The type of the property on the target object; [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source` (receiver): The observable stream to bind to a target property; `TTarget?` `target`: The target object whose property will be set; [`Expression<Func<TTarget, TTargetValue?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property`: An expression that selects the target property to set; [`IBindingTypeConverter?`](converters.md) `converterOverride`: An explicit converter to use when converting the source value to the target property type; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyExpression`: The caller argument expression for `property`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `IObservable<TValue>.BindTo<TValue, TTarget, TTargetValue>(TTarget? target, Expression<Func<TTarget, TTargetValue?>> property, object? conversionHint, string propertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Writes each value the source produces to a target property, passing the conversion hint to the converter that coerces the value to the property type. | `TValue`: The type of the value produced by the source observable; `TTarget : class`: The type of the target object; `TTargetValue`: The type of the property on the target object; [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source` (receiver): The observable stream to bind to a target property; `TTarget?` `target`: The target object whose property will be set; [`Expression<Func<TTarget, TTargetValue?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property`: An expression that selects the target property to set; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: An object that provides a hint to the converter. The semantics are defined by the converter; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyExpression`: The caller argument expression for `property`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `IObservable<TValue>.BindTo<TValue, TTarget, TTargetValue>(TTarget? target, Expression<Func<TTarget, TTargetValue?>> property, object? conversionHint, IBindingTypeConverter? converterOverride, string propertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Writes each value the source produces to a target property, coercing it with the supplied converter instead of a registered one and passing the conversion hint to it. | `TValue`: The type of the value produced by the source observable; `TTarget : class`: The type of the target object; `TTargetValue`: The type of the property on the target object; [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source` (receiver): The observable stream to bind to a target property; `TTarget?` `target`: The target object whose property will be set; [`Expression<Func<TTarget, TTargetValue?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property`: An expression that selects the target property to set; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: An object that provides a hint to the converter. The semantics are defined by the converter; [`IBindingTypeConverter?`](converters.md) `converterOverride`: An explicit converter to use when converting the source value to the target property type; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyExpression`: The caller argument expression for `property`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |

### Command bindings

[Full description and examples](bindings.md).

Types: `ReactiveUI.Binding.ReactiveUIBindingExtensions`.

#### `ReactiveUIBindingExtensions`

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BindCommand<TView, TViewModel, TProp, TControl>(TView view, TViewModel? viewModel, Expression<Func<TViewModel, TProp?>> propertyName, Expression<Func<TView, TControl>> controlName, string? toEvent = null, string propertyNameExpression = "", string controlNameExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Binds the command a view model property holds to the control a view property holds, rebinding when either changes. | `TView : class, IViewFor`: The type of the view; `TViewModel : class`: The type of the view model; `TProp : ICommand`: The type of the command property; `TControl : class`: The type of the control; `TView` `view`: The view to bind to; `TViewModel?` `viewModel`: The view model containing the command; null binds nothing; [`Expression<Func<TViewModel, TProp?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `propertyName`: An expression that selects the command property on the view model; [`Expression<Func<TView, TControl>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `controlName`: An expression that selects the control on the view; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `toEvent`: The event name to bind to. If null, a default event is selected; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyNameExpression`: The caller argument expression for `propertyName`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `controlNameExpression`: The caller argument expression for `controlName`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindCommand<TView, TViewModel, TProp, TControl, TParam>(TView view, TViewModel? viewModel, Expression<Func<TViewModel, TProp?>> propertyName, Expression<Func<TView, TControl>> controlName, IObservable<TParam?> withParameter, string? toEvent = null, string propertyNameExpression = "", string controlNameExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Binds the command a view model property holds to the control a view property holds, using an observable as the source of the command parameter. | `TView : class, IViewFor`: The type of the view; `TViewModel : class`: The type of the view model; `TProp : ICommand`: The type of the command property; `TControl : class`: The type of the control; `TParam`: The type of the command parameter; `TView` `view`: The view to bind to; `TViewModel?` `viewModel`: The view model containing the command; null binds nothing; [`Expression<Func<TViewModel, TProp?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `propertyName`: An expression that selects the command property on the view model; [`Expression<Func<TView, TControl>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `controlName`: An expression that selects the control on the view; [`IObservable<TParam?>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `withParameter`: An observable that provides the command parameter; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `toEvent`: The event name to bind to. If null, a default event is selected; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyNameExpression`: The caller argument expression for `propertyName`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `controlNameExpression`: The caller argument expression for `controlName`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindCommand<TView, TViewModel, TProp, TControl, TParam>(TView view, TViewModel? viewModel, Expression<Func<TViewModel, TProp?>> propertyName, Expression<Func<TView, TControl>> controlName, Expression<Func<TViewModel, TParam?>> withParameter, string? toEvent = null, string propertyNameExpression = "", string controlNameExpression = "", string withParameterExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Binds the command a view model property holds to the control a view property holds, using a view model property as the command parameter. | `TView : class, IViewFor`: The type of the view; `TViewModel : class`: The type of the view model; `TProp : ICommand`: The type of the command property; `TControl : class`: The type of the control; `TParam`: The type of the command parameter; `TView` `view`: The view to bind to; `TViewModel?` `viewModel`: The view model containing the command; null binds nothing; [`Expression<Func<TViewModel, TProp?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `propertyName`: An expression that selects the command property on the view model; [`Expression<Func<TView, TControl>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `controlName`: An expression that selects the control on the view; [`Expression<Func<TViewModel, TParam?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `withParameter`: An expression that selects the command parameter property on the view model; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `toEvent`: The event name to bind to. If null, a default event is selected; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyNameExpression`: The caller argument expression for `propertyName`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `controlNameExpression`: The caller argument expression for `controlName`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `withParameterExpression`: The caller argument expression for `withParameter`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |

### Invoke a command from a stream

[Full description and examples](bindings.md).

Types: `ReactiveUI.Binding.ReactiveUIBindingExtensions`.

#### `ReactiveUIBindingExtensions`

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `IObservable<T>.InvokeCommand<T>(ICommand command)` | Executes a command with each value the sequence produces as its parameter, skipping a value the command cannot execute. | `T`: The type of the value offered as the command parameter; [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source` (receiver): The sequence driving the executions; [`ICommand`](https://learn.microsoft.com/dotnet/api/system.windows.input.icommand) `command`: The command to execute | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, stops executing the command. |
| `IObservable<T>.InvokeCommand<T, TTarget>(TTarget? target, Expression<Func<TTarget, ICommand?>> commandProperty, string commandPropertyExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Executes the command a property holds with each value as its parameter, skipping a value while there is no command or the command cannot execute it. | `T`: The type of the value offered as the command parameter; `TTarget : class`: The type declaring the command property; [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source` (receiver): The sequence driving the executions; `TTarget?` `target`: The object declaring the command property; null executes nothing; [`Expression<Func<TTarget, ICommand?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `commandProperty`: An expression that selects the command property to execute; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `commandPropertyExpression`: The caller argument expression for `commandProperty`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, stops executing the command and stops observing the property. |

### Interactions

[Full description and examples](bindings.md).

Types: `ReactiveUI.Binding.IInteraction<TInput, TOutput>`, `ReactiveUI.Binding.IInteractionContext<TInput, TOutput>`, `ReactiveUI.Binding.IOutputContext<TInput, TOutput>`, `ReactiveUI.Binding.Interaction<TInput, TOutput>`, `ReactiveUI.Binding.InteractionContext<TInput, TOutput>`, `ReactiveUI.Binding.ReactiveUIBindingExtensions`, `ReactiveUI.Binding.UnhandledInteractionException<TInput, TOutput>`.

#### `IInteraction<TInput, TOutput>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IInteraction<TInput, TOutput>` | Represents an interaction between collaborating application components. | `TInput`: The interaction's input type; `TOutput`: The interaction's output type | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Handle(TInput input)` | Handles an interaction and asynchronously returns the result. | `TInput` `input`: The input for the interaction | [`Task<TOutput>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1): A task that completes with the output when the interaction is handled. |
| `RegisterHandler(Action<IInteractionContext<TInput, TOutput>> handler)` | Registers a synchronous interaction handler. | [`Action<IInteractionContext<TInput, TOutput>>`](https://learn.microsoft.com/dotnet/api/system.action-1) `handler`: The handler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable which, when disposed, will unregister the handler. |
| `RegisterHandler(Func<IInteractionContext<TInput, TOutput>, Task> handler)` | Registers a task-based asynchronous interaction handler. | [`Func<IInteractionContext<TInput, TOutput>, Task>`](https://learn.microsoft.com/dotnet/api/system.func-2) `handler`: The handler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable which, when disposed, will unregister the handler. |
| `RegisterHandler<TDontCare>(Func<IInteractionContext<TInput, TOutput>, IObservable<TDontCare>> handler)` | Registers an observable-based asynchronous interaction handler. | `TDontCare`: The signal type; [`Func<IInteractionContext<TInput, TOutput>, IObservable<TDontCare>>`](https://learn.microsoft.com/dotnet/api/system.func-2) `handler`: The handler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable which, when disposed, will unregister the handler. |

#### `IInteractionContext<TInput, TOutput>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IInteractionContext<TInput, TOutput>` | Contains contextual information for an interaction. | `TInput`: The type of the interaction's input; `TOutput`: The type of the interaction's output | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `SetOutput(TOutput output)` | Sets the output for the interaction. | `TOutput` `output`: The output | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Input { get; }` | Gets the input for the interaction. | None. | `TInput` |
| `IsHandled { get; }` | Gets a value indicating whether the interaction is handled. That is, whether the output has been set. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) |

#### `IOutputContext<TInput, TOutput>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IOutputContext<TInput, TOutput>` | Extends [`IInteractionContext<TInput, TOutput>`](bindings.md) with the ability to retrieve the output. | `TInput`: The type of the interaction's input; `TOutput`: The type of the interaction's output | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetOutput()` | Gets the output of the interaction. | None. | `TOutput`: The output. |

#### `Interaction<TInput, TOutput>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `class Interaction<TInput, TOutput>` | Represents an interaction between collaborating application components. | `TInput`: The interaction's input type; `TOutput`: The interaction's output type | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Interaction()` | Initializes a new instance of the Interaction<TInput, TOutput> class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `protected virtual GenerateContext(TInput input)` | Creates the context every handler receives for one call to `Handle`. | `TInput` `input`: The input passed to `Handle` | [`IOutputContext<TInput, TOutput>`](bindings.md): A new interaction context carrying the input. |
| `protected GetHandlers()` | Gets a copy of the registered handlers in order of registration. | None. | [`Func<IInteractionContext<TInput, TOutput>, Task>[]`](https://learn.microsoft.com/dotnet/api/system.func-2): The registered handlers, earliest first. |
| `virtual Handle(TInput input)` | Runs the handlers, latest registered first, until one sets an output, and returns that output. | `TInput` `input`: The input for the interaction | [`Task<TOutput>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1): A task that completes with the output the first handling handler set. |
| `RegisterHandler(Action<IInteractionContext<TInput, TOutput>> handler)` | Registers a synchronous interaction handler. | [`Action<IInteractionContext<TInput, TOutput>>`](https://learn.microsoft.com/dotnet/api/system.action-1) `handler`: The handler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable which, when disposed, will unregister the handler. |
| `RegisterHandler(Func<IInteractionContext<TInput, TOutput>, Task> handler)` | Registers a task-based asynchronous interaction handler. | [`Func<IInteractionContext<TInput, TOutput>, Task>`](https://learn.microsoft.com/dotnet/api/system.func-2) `handler`: The handler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable which, when disposed, will unregister the handler. |
| `RegisterHandler<TDontCare>(Func<IInteractionContext<TInput, TOutput>, IObservable<TDontCare>> handler)` | Registers a handler that finishes when the observable it returns completes. | `TDontCare`: The element type of the returned observable; the values are ignored; [`Func<IInteractionContext<TInput, TOutput>, IObservable<TDontCare>>`](https://learn.microsoft.com/dotnet/api/system.func-2) `handler`: The handler; the interaction moves to the next handler once the observable completes, and a fault in the observable faults `Handle` | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable which, when disposed, unregisters the handler. |

#### `InteractionContext<TInput, TOutput>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class InteractionContext<TInput, TOutput>` | Contains contextual information for an interaction. | `TInput`: The type of the interaction's input; `TOutput`: The type of the interaction's output | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetOutput()` | Gets the output of the interaction. | None. | `TOutput`: The output. |
| `SetOutput(TOutput output)` | Sets the output for the interaction and marks it handled. | `TOutput` `output`: The output | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Input { get; }` | Gets the input for the interaction. | None. | `TInput` |
| `IsHandled { get; }` | Gets a value indicating whether the interaction is handled. That is, whether the output has been set. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) |

#### `ReactiveUIBindingExtensions`

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BindInteraction<TViewModel, TView, TInput, TOutput>(TView view, TViewModel? viewModel, Expression<Func<TViewModel, IInteraction<TInput, TOutput>>> propertyName, Func<IInteractionContext<TInput, TOutput>, Task> handler, string propertyNameExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Registers a task-based handler on the interaction a view model property holds, moving it to the new interaction when the property changes. | `TViewModel : class`: The type of the view model; `TView : class, IViewFor`: The type of the view; `TInput`: The type of the interaction's input; `TOutput`: The type of the interaction's output; `TView` `view`: The view that provides the handler; `TViewModel?` `viewModel`: The view model that exposes the interaction; null registers nothing; [`Expression<Func<TViewModel, IInteraction<TInput, TOutput>>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `propertyName`: An expression that selects the interaction property on the view model; [`Func<IInteractionContext<TInput, TOutput>, Task>`](https://learn.microsoft.com/dotnet/api/system.func-2) `handler`: A task-based handler for the interaction; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyNameExpression`: The caller argument expression for `propertyName`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, unregisters the handler and stops observing the property. |
| `BindInteraction<TViewModel, TView, TInput, TOutput, TDontCare>(TView view, TViewModel? viewModel, Expression<Func<TViewModel, IInteraction<TInput, TOutput>>> propertyName, Func<IInteractionContext<TInput, TOutput>, IObservable<TDontCare>> handler, string propertyNameExpression = "", string callerFilePath = "", int callerLineNumber = 0)` | Registers an observable-based handler on the interaction a view model property holds, moving it to the new interaction when the property changes. | `TViewModel : class`: The type of the view model; `TView : class, IViewFor`: The type of the view; `TInput`: The type of the interaction's input; `TOutput`: The type of the interaction's output; `TDontCare`: The signal type of the observable handler; `TView` `view`: The view that provides the handler; `TViewModel?` `viewModel`: The view model that exposes the interaction; null registers nothing; [`Expression<Func<TViewModel, IInteraction<TInput, TOutput>>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `propertyName`: An expression that selects the interaction property on the view model; [`Func<IInteractionContext<TInput, TOutput>, IObservable<TDontCare>>`](https://learn.microsoft.com/dotnet/api/system.func-2) `handler`: An observable-based handler for the interaction; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyNameExpression`: The caller argument expression for `propertyName`. Auto-populated by the compiler; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `callerFilePath`: The source file path of the caller. Auto-populated by the compiler; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `callerLineNumber`: The source line number of the caller. Auto-populated by the compiler | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, unregisters the handler and stops observing the property. |

#### `UnhandledInteractionException<TInput, TOutput>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `class UnhandledInteractionException<TInput, TOutput> : Exception` | Indicates that an interaction has gone unhandled. | `TInput`: The type of the interaction's input; `TOutput`: The type of the interaction's output | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `UnhandledInteractionException()` | Initializes a new instance of the [`UnhandledInteractionException<TInput, TOutput>`](bindings.md) class. | None. | — |
| `UnhandledInteractionException(string message)` | Initializes a new instance of the [`UnhandledInteractionException<TInput, TOutput>`](bindings.md) class. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `message`: A message about the exception | — |
| `UnhandledInteractionException(Interaction<TInput, TOutput> interaction, TInput input)` | Initializes a new instance of the [`UnhandledInteractionException<TInput, TOutput>`](bindings.md) class for an interaction no handler handled. | [`Interaction<TInput, TOutput>`](bindings.md) `interaction`: The interaction that no handler handled; `TInput` `input`: The input into the interaction | — |
| `UnhandledInteractionException(string message, Exception innerException)` | Initializes a new instance of the [`UnhandledInteractionException<TInput, TOutput>`](bindings.md) class. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `message`: A message about the exception; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception) `innerException`: Any other exception that caused the issue | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Input { get; }` | Gets the input for the interaction that was not handled, or the default value when the exception was created without one. | None. | `TInput` |
| `Interaction { get; }` | Gets the interaction that was not handled, or null when the exception was created without one. | None. | [`Interaction<TInput, TOutput>?`](bindings.md) |

### Reactive bindings

[Full description and examples](bindings.md).

Types: `ReactiveUI.Binding.BindingChange`, `ReactiveUI.Binding.BindingDirection`, `ReactiveUI.Binding.IReactiveBinding<TView, TValue>`, `ReactiveUI.Binding.ReactiveBinding<TView, TValue>`.

#### `BindingChange`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `readonly struct BindingChange` | A value a two-way binding moved, and which side of the binding produced it. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BindingChange(object? Value, bool FromViewModel)` | A value a two-way binding moved, and which side of the binding produced it. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `Value`: The value that was written; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `FromViewModel`: Whether the view model produced the value, rather than the view | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromViewModel { get; init; }` | Whether the view model produced the value, rather than the view. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) |
| `Value { get; init; }` | The value that was written. | None. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) |

#### `BindingDirection`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `enum BindingDirection` | Specifies the direction of a property binding. | None. | — |

**Enum values**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `OneWay = 0` | One-way binding from source to target. | None. | — |
| `TwoWay = 1` | Two-way binding between source and target. | None. | — |
| `AsyncOneWay = 2` | One-way asynchronous binding from source to target. | None. | — |

#### `IReactiveBinding<TView, TValue>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IReactiveBinding<TView, TValue>` | Represents a binding between a view and a view model property. | `TView : IViewFor`: The type of the view; `TValue`: The type of the bound value | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Changed { get; }` | Gets an observable that signals when the binding value changes. | None. | [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) |
| `Direction { get; }` | Gets the direction of the binding. | None. | [`BindingDirection`](bindings.md) |
| `View { get; }` | Gets the view that is bound. | None. | `TView` |
| `ViewExpression { get; }` | Gets the expression representing the view property that is bound, or null when the binding carries none. | None. | [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) |
| `ViewModelExpression { get; }` | Gets the expression representing the view model property that is bound, or null when the binding carries none. | None. | [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) |

#### `ReactiveBinding<TView, TValue>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ReactiveBinding<TView, TValue>` | Default implementation of [`IReactiveBinding<TView, TValue>`](bindings.md) used by generated view-first bindings. | `TView : IViewFor`: The type of the view; `TValue`: The type of the bound value | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ReactiveBinding(TView view, IObservable<TValue> changed, BindingDirection direction, IDisposable subscription)` | Initializes a new instance of the [`ReactiveBinding<TView, TValue>`](bindings.md) class. | `TView` `view`: The view that is bound; [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `changed`: An observable that signals when the binding value changes; [`BindingDirection`](bindings.md) `direction`: The direction of the binding; [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) `subscription`: The underlying subscription to dispose when the binding is disposed | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Dispose()` | Disposes the underlying subscription on the first call; later calls do nothing. | None. | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Changed { get; }` | Gets an observable that signals when the binding value changes. | None. | [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) |
| `Direction { get; }` | Gets the direction of the binding. | None. | [`BindingDirection`](bindings.md) |
| `View { get; }` | Gets the view that is bound. | None. | `TView` |
| `ViewExpression { get; }` | Gets the view expression, which is always null because a generated binding carries no expression. | None. | [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) |
| `ViewModelExpression { get; }` | Gets the view model expression, which is always null because a generated binding carries no expression. | None. | [`Expression?`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) |

### Binding hooks

[Full description and examples](bindings.md).

Types: `ReactiveUI.Binding.BindingHooks`, `ReactiveUI.Binding.IPropertyBindingHook`.

#### `BindingHooks`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class BindingHooks` | Lets a registered [`IPropertyBindingHook`](bindings.md) inspect, or refuse, a binding as it is created. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Refresh()` | Discards the cached hooks so the next use reads them from the service locator again. | None. | — |
| `ShouldBind(object? source, object target, Func<IObservedChange<object, object>[]> getSourceProperties, Func<IObservedChange<object, object>[]> getTargetProperties, BindingDirection direction)` | Asks the registered hooks in turn whether this binding may be created, stopping at the first refusal. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `source`: The source object, typically the view model; may be null; [`object`](https://learn.microsoft.com/dotnet/api/system.object) `target`: The target object, typically the view; [`Func<IObservedChange<object, object>[]>`](https://learn.microsoft.com/dotnet/api/system.func-1) `getSourceProperties`: Reads the current source-side values; [`Func<IObservedChange<object, object>[]>`](https://learn.microsoft.com/dotnet/api/system.func-1) `getTargetProperties`: Reads the current target-side values; [`BindingDirection`](bindings.md) `direction`: Which way the binding runs | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` when the binding may proceed, including when no hook is registered; `false` when a hook refused it. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Any { get; }` | Gets a value indicating whether any hook was registered when the hooks were last read. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) |

#### `IPropertyBindingHook`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IPropertyBindingHook` | Implement this as a way to intercept bindings at the time that they are created and execute an additional action (or to cancel the binding). | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ExecuteHook(object? source, object target, Func<IObservedChange<object, object>[]> getCurrentViewModelProperties, Func<IObservedChange<object, object>[]> getCurrentViewProperties, BindingDirection direction)` | Called as a binding is set up, before it is wired, and can refuse the binding. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `source`: The source ViewModel; may be null; [`object`](https://learn.microsoft.com/dotnet/api/system.object) `target`: The target View (not the actual control); [`Func<IObservedChange<object, object>[]>`](https://learn.microsoft.com/dotnet/api/system.func-1) `getCurrentViewModelProperties`: Reads the current values along the view model property path when called; [`Func<IObservedChange<object, object>[]>`](https://learn.microsoft.com/dotnet/api/system.func-1) `getCurrentViewProperties`: Reads the current values along the view property path when called; [`BindingDirection`](bindings.md) `direction`: The Binding direction | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` to let the binding proceed; `false` to cancel it. |

### Binding errors

[Full description and examples](bindings.md).

Types: `ReactiveUI.Binding.BindingErrors`.

#### `BindingErrors`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class BindingErrors` | How a binding reacts when the sequence feeding its write faults. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Subscribe<T>(IObservable<T> source, Action<T> onNext, string bindingExpression)` | Subscribes a binding's write to its source, applying the binding fault contract. | `T`: The type of the value written; [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source`: The sequence feeding the write; [`Action<T>`](https://learn.microsoft.com/dotnet/api/system.action-1) `onNext`: The write itself; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `bindingExpression`: The bound expression, named in the log entry and the rethrown exception | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the write. |

## Converters

### Numbers

[Full description and examples](converters.md).

Types: `ReactiveUI.Binding.ByteToStringTypeConverter`, `ReactiveUI.Binding.DecimalToStringTypeConverter`, `ReactiveUI.Binding.DoubleToStringTypeConverter`, `ReactiveUI.Binding.IntegerToStringTypeConverter`, `ReactiveUI.Binding.LongToStringTypeConverter`, `ReactiveUI.Binding.ShortToStringTypeConverter`, `ReactiveUI.Binding.SingleToStringTypeConverter`, `ReactiveUI.Binding.StringToByteTypeConverter`, `ReactiveUI.Binding.StringToDecimalTypeConverter`, `ReactiveUI.Binding.StringToDoubleTypeConverter`, `ReactiveUI.Binding.StringToIntegerTypeConverter`, `ReactiveUI.Binding.StringToLongTypeConverter`, `ReactiveUI.Binding.StringToShortTypeConverter`, `ReactiveUI.Binding.StringToSingleTypeConverter`.

#### `ByteToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ByteToStringTypeConverter : BindingTypeConverter<byte, string>` | Converts a [`byte`](https://learn.microsoft.com/dotnet/api/system.byte) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) using the current culture. An [`int`](https://learn.microsoft.com/dotnet/api/system.int32) hint gives the minimum digit count (the `D` format) and a [`string`](https://learn.microsoft.com/dotnet/api/system.string) hint gives the format string; a malformed format throws [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ByteToStringTypeConverter()` | Initializes a new instance of the ByteToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(byte from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`byte`](https://learn.microsoft.com/dotnet/api/system.byte) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `DecimalToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class DecimalToStringTypeConverter : BindingTypeConverter<decimal, string>` | Converts a [`decimal`](https://learn.microsoft.com/dotnet/api/system.decimal) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) using the current culture. An [`int`](https://learn.microsoft.com/dotnet/api/system.int32) hint gives the number of decimal places (the `F` format) and a [`string`](https://learn.microsoft.com/dotnet/api/system.string) hint gives the format string; a malformed format throws [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `DecimalToStringTypeConverter()` | Initializes a new instance of the DecimalToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(decimal from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`decimal`](https://learn.microsoft.com/dotnet/api/system.decimal) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `DoubleToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class DoubleToStringTypeConverter : BindingTypeConverter<double, string>` | Converts a [`double`](https://learn.microsoft.com/dotnet/api/system.double) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) using the current culture. An [`int`](https://learn.microsoft.com/dotnet/api/system.int32) hint gives the number of decimal places (the `F` format) and a [`string`](https://learn.microsoft.com/dotnet/api/system.string) hint gives the format string; a malformed format throws [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `DoubleToStringTypeConverter()` | Initializes a new instance of the DoubleToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(double from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`double`](https://learn.microsoft.com/dotnet/api/system.double) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `IntegerToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class IntegerToStringTypeConverter : BindingTypeConverter<int, string>` | Converts an [`int`](https://learn.microsoft.com/dotnet/api/system.int32) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) using the current culture. An [`int`](https://learn.microsoft.com/dotnet/api/system.int32) hint gives the minimum digit count (the `D` format) and a [`string`](https://learn.microsoft.com/dotnet/api/system.string) hint gives the format string; a malformed format throws [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `IntegerToStringTypeConverter()` | Initializes a new instance of the IntegerToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(int from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `LongToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class LongToStringTypeConverter : BindingTypeConverter<long, string>` | Converts a [`long`](https://learn.microsoft.com/dotnet/api/system.int64) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) using the current culture. An [`int`](https://learn.microsoft.com/dotnet/api/system.int32) hint gives the minimum digit count (the `D` format) and a [`string`](https://learn.microsoft.com/dotnet/api/system.string) hint gives the format string; a malformed format throws [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `LongToStringTypeConverter()` | Initializes a new instance of the LongToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(long from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`long`](https://learn.microsoft.com/dotnet/api/system.int64) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `ShortToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ShortToStringTypeConverter : BindingTypeConverter<short, string>` | Converts a [`short`](https://learn.microsoft.com/dotnet/api/system.int16) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) using the current culture. An [`int`](https://learn.microsoft.com/dotnet/api/system.int32) hint gives the minimum digit count (the `D` format) and a [`string`](https://learn.microsoft.com/dotnet/api/system.string) hint gives the format string; a malformed format throws [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ShortToStringTypeConverter()` | Initializes a new instance of the ShortToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(short from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`short`](https://learn.microsoft.com/dotnet/api/system.int16) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `SingleToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class SingleToStringTypeConverter : BindingTypeConverter<float, string>` | Converts a [`float`](https://learn.microsoft.com/dotnet/api/system.single) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) using the current culture. An [`int`](https://learn.microsoft.com/dotnet/api/system.int32) hint gives the number of decimal places (the `F` format) and a [`string`](https://learn.microsoft.com/dotnet/api/system.string) hint gives the format string; a malformed format throws [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `SingleToStringTypeConverter()` | Initializes a new instance of the SingleToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(float from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`float`](https://learn.microsoft.com/dotnet/api/system.single) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToByteTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToByteTypeConverter : BindingTypeConverter<string, byte>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a [`byte`](https://learn.microsoft.com/dotnet/api/system.byte) with `TryParse` under the current culture; a null or unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToByteTypeConverter()` | Initializes a new instance of the StringToByteTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out byte result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`byte`](https://learn.microsoft.com/dotnet/api/system.byte) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToDecimalTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToDecimalTypeConverter : BindingTypeConverter<string, decimal>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a [`decimal`](https://learn.microsoft.com/dotnet/api/system.decimal) with `TryParse` under the current culture; a null or unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToDecimalTypeConverter()` | Initializes a new instance of the StringToDecimalTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out decimal result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`decimal`](https://learn.microsoft.com/dotnet/api/system.decimal) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToDoubleTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToDoubleTypeConverter : BindingTypeConverter<string, double>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a [`double`](https://learn.microsoft.com/dotnet/api/system.double) with `TryParse` under the current culture; a null or unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToDoubleTypeConverter()` | Initializes a new instance of the StringToDoubleTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out double result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`double`](https://learn.microsoft.com/dotnet/api/system.double) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToIntegerTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToIntegerTypeConverter : BindingTypeConverter<string, int>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to an [`int`](https://learn.microsoft.com/dotnet/api/system.int32) with `TryParse` under the current culture; a null or unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToIntegerTypeConverter()` | Initializes a new instance of the StringToIntegerTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out int result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToLongTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToLongTypeConverter : BindingTypeConverter<string, long>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a [`long`](https://learn.microsoft.com/dotnet/api/system.int64) with `TryParse` under the current culture; a null or unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToLongTypeConverter()` | Initializes a new instance of the StringToLongTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out long result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`long`](https://learn.microsoft.com/dotnet/api/system.int64) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToShortTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToShortTypeConverter : BindingTypeConverter<string, short>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a [`short`](https://learn.microsoft.com/dotnet/api/system.int16) with `TryParse` under the current culture; a null or unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToShortTypeConverter()` | Initializes a new instance of the StringToShortTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out short result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`short`](https://learn.microsoft.com/dotnet/api/system.int16) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToSingleTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToSingleTypeConverter : BindingTypeConverter<string, float>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a [`float`](https://learn.microsoft.com/dotnet/api/system.single) with `TryParse` under the current culture; a null or unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToSingleTypeConverter()` | Initializes a new instance of the StringToSingleTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out float result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`float`](https://learn.microsoft.com/dotnet/api/system.single) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

### Dates and times

[Full description and examples](converters.md).

Types: `ReactiveUI.Binding.DateOnlyToStringTypeConverter`, `ReactiveUI.Binding.DateTimeOffsetToStringTypeConverter`, `ReactiveUI.Binding.DateTimeToStringTypeConverter`, `ReactiveUI.Binding.StringToDateOnlyTypeConverter`, `ReactiveUI.Binding.StringToDateTimeOffsetTypeConverter`, `ReactiveUI.Binding.StringToDateTimeTypeConverter`, `ReactiveUI.Binding.StringToTimeOnlyTypeConverter`, `ReactiveUI.Binding.StringToTimeSpanTypeConverter`, `ReactiveUI.Binding.TimeOnlyToStringTypeConverter`, `ReactiveUI.Binding.TimeSpanToStringTypeConverter`.

#### `DateOnlyToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class DateOnlyToStringTypeConverter : BindingTypeConverter<DateOnly, string>` | Converts a [`DateOnly`](https://learn.microsoft.com/dotnet/api/system.dateonly) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) in the short date format of the current culture. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `DateOnlyToStringTypeConverter()` | Initializes a new instance of the DateOnlyToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(DateOnly from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`DateOnly`](https://learn.microsoft.com/dotnet/api/system.dateonly) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `DateTimeOffsetToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class DateTimeOffsetToStringTypeConverter : BindingTypeConverter<DateTimeOffset, string>` | Converts a [`DateTimeOffset`](https://learn.microsoft.com/dotnet/api/system.datetimeoffset) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) in the general date and time format of the current culture, including the offset. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `DateTimeOffsetToStringTypeConverter()` | Initializes a new instance of the DateTimeOffsetToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(DateTimeOffset from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`DateTimeOffset`](https://learn.microsoft.com/dotnet/api/system.datetimeoffset) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `DateTimeToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class DateTimeToStringTypeConverter : BindingTypeConverter<DateTime, string>` | Converts a [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) in the general date and time format of the current culture. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `DateTimeToStringTypeConverter()` | Initializes a new instance of the DateTimeToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(DateTime from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToDateOnlyTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToDateOnlyTypeConverter : BindingTypeConverter<string, DateOnly>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a [`DateOnly`](https://learn.microsoft.com/dotnet/api/system.dateonly) with `TryParse` under the current culture; a null or unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToDateOnlyTypeConverter()` | Initializes a new instance of the StringToDateOnlyTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out DateOnly result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`DateOnly`](https://learn.microsoft.com/dotnet/api/system.dateonly) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToDateTimeOffsetTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToDateTimeOffsetTypeConverter : BindingTypeConverter<string, DateTimeOffset>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a [`DateTimeOffset`](https://learn.microsoft.com/dotnet/api/system.datetimeoffset) with `TryParse` under the current culture; a null or unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToDateTimeOffsetTypeConverter()` | Initializes a new instance of the StringToDateTimeOffsetTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out DateTimeOffset result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`DateTimeOffset`](https://learn.microsoft.com/dotnet/api/system.datetimeoffset) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToDateTimeTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToDateTimeTypeConverter : BindingTypeConverter<string, DateTime>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) with `TryParse` under the current culture; a null or unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToDateTimeTypeConverter()` | Initializes a new instance of the StringToDateTimeTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out DateTime result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToTimeOnlyTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToTimeOnlyTypeConverter : BindingTypeConverter<string, TimeOnly>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a [`TimeOnly`](https://learn.microsoft.com/dotnet/api/system.timeonly) with `TryParse` under the current culture; a null or unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToTimeOnlyTypeConverter()` | Initializes a new instance of the StringToTimeOnlyTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out TimeOnly result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`TimeOnly`](https://learn.microsoft.com/dotnet/api/system.timeonly) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToTimeSpanTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToTimeSpanTypeConverter : BindingTypeConverter<string, TimeSpan>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) with `TryParse` under the current culture; a null or unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToTimeSpanTypeConverter()` | Initializes a new instance of the StringToTimeSpanTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out TimeSpan result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `TimeOnlyToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class TimeOnlyToStringTypeConverter : BindingTypeConverter<TimeOnly, string>` | Converts a [`TimeOnly`](https://learn.microsoft.com/dotnet/api/system.timeonly) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) in the short time format of the current culture. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `TimeOnlyToStringTypeConverter()` | Initializes a new instance of the TimeOnlyToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(TimeOnly from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`TimeOnly`](https://learn.microsoft.com/dotnet/api/system.timeonly) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `TimeSpanToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class TimeSpanToStringTypeConverter : BindingTypeConverter<TimeSpan, string>` | Converts a [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) in the culture-invariant constant format (`c`). | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `TimeSpanToStringTypeConverter()` | Initializes a new instance of the TimeSpanToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(TimeSpan from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

### Booleans, GUIDs and URIs

[Full description and examples](converters.md).

Types: `ReactiveUI.Binding.BooleanToStringTypeConverter`, `ReactiveUI.Binding.GuidToStringTypeConverter`, `ReactiveUI.Binding.StringToBooleanTypeConverter`, `ReactiveUI.Binding.StringToGuidTypeConverter`, `ReactiveUI.Binding.StringToUriTypeConverter`, `ReactiveUI.Binding.UriToStringTypeConverter`.

#### `BooleanToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class BooleanToStringTypeConverter : BindingTypeConverter<bool, string>` | Converts a [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) as "True" or "False". | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BooleanToStringTypeConverter()` | Initializes a new instance of the BooleanToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(bool from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `GuidToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class GuidToStringTypeConverter : BindingTypeConverter<Guid, string>` | Converts a [`Guid`](https://learn.microsoft.com/dotnet/api/system.guid) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) in the hyphenated `D` format. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GuidToStringTypeConverter()` | Initializes a new instance of the GuidToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(Guid from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`Guid`](https://learn.microsoft.com/dotnet/api/system.guid) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToBooleanTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToBooleanTypeConverter : BindingTypeConverter<string, bool>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) with `TryParse` (case-insensitive `true` or `false`); a null or unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToBooleanTypeConverter()` | Initializes a new instance of the StringToBooleanTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out bool result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToGuidTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToGuidTypeConverter : BindingTypeConverter<string, Guid>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a [`Guid`](https://learn.microsoft.com/dotnet/api/system.guid) with `TryParse`; a null or unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToGuidTypeConverter()` | Initializes a new instance of the StringToGuidTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out Guid result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`Guid`](https://learn.microsoft.com/dotnet/api/system.guid) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToUriTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToUriTypeConverter : BindingTypeConverter<string, Uri>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a [`Uri`](https://learn.microsoft.com/dotnet/api/system.uri) with `TryCreate`, accepting relative and absolute URIs; a null string or text that cannot form a URI fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToUriTypeConverter()` | Initializes a new instance of the StringToUriTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out Uri? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`Uri?`](https://learn.microsoft.com/dotnet/api/system.uri) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `UriToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class UriToStringTypeConverter : BindingTypeConverter<Uri, string>` | Converts a [`Uri`](https://learn.microsoft.com/dotnet/api/system.uri) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) with `ToString`; a null value fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `UriToStringTypeConverter()` | Initializes a new instance of the UriToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(Uri? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`Uri?`](https://learn.microsoft.com/dotnet/api/system.uri) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

### Nullable values

[Full description and examples](converters.md).

Types: `ReactiveUI.Binding.ByteToNullableByteTypeConverter`, `ReactiveUI.Binding.DecimalToNullableDecimalTypeConverter`, `ReactiveUI.Binding.DoubleToNullableDoubleTypeConverter`, `ReactiveUI.Binding.IntegerToNullableIntegerTypeConverter`, `ReactiveUI.Binding.LongToNullableLongTypeConverter`, `ReactiveUI.Binding.NullableBooleanToStringTypeConverter`, `ReactiveUI.Binding.NullableByteToByteTypeConverter`, `ReactiveUI.Binding.NullableByteToStringTypeConverter`, `ReactiveUI.Binding.NullableDateOnlyToStringTypeConverter`, `ReactiveUI.Binding.NullableDateTimeOffsetToStringTypeConverter`, `ReactiveUI.Binding.NullableDateTimeToStringTypeConverter`, `ReactiveUI.Binding.NullableDecimalToDecimalTypeConverter`, `ReactiveUI.Binding.NullableDecimalToStringTypeConverter`, `ReactiveUI.Binding.NullableDoubleToDoubleTypeConverter`, `ReactiveUI.Binding.NullableDoubleToStringTypeConverter`, `ReactiveUI.Binding.NullableGuidToStringTypeConverter`, `ReactiveUI.Binding.NullableIntegerToIntegerTypeConverter`, `ReactiveUI.Binding.NullableIntegerToStringTypeConverter`, `ReactiveUI.Binding.NullableLongToLongTypeConverter`, `ReactiveUI.Binding.NullableLongToStringTypeConverter`, `ReactiveUI.Binding.NullableShortToShortTypeConverter`, `ReactiveUI.Binding.NullableShortToStringTypeConverter`, `ReactiveUI.Binding.NullableSingleToSingleTypeConverter`, `ReactiveUI.Binding.NullableSingleToStringTypeConverter`, `ReactiveUI.Binding.NullableTimeOnlyToStringTypeConverter`, `ReactiveUI.Binding.NullableTimeSpanToStringTypeConverter`, `ReactiveUI.Binding.ShortToNullableShortTypeConverter`, `ReactiveUI.Binding.SingleToNullableSingleTypeConverter`, `ReactiveUI.Binding.StringToNullableBooleanTypeConverter`, `ReactiveUI.Binding.StringToNullableByteTypeConverter`, `ReactiveUI.Binding.StringToNullableDateOnlyTypeConverter`, `ReactiveUI.Binding.StringToNullableDateTimeOffsetTypeConverter`, `ReactiveUI.Binding.StringToNullableDateTimeTypeConverter`, `ReactiveUI.Binding.StringToNullableDecimalTypeConverter`, `ReactiveUI.Binding.StringToNullableDoubleTypeConverter`, `ReactiveUI.Binding.StringToNullableGuidTypeConverter`, `ReactiveUI.Binding.StringToNullableIntegerTypeConverter`, `ReactiveUI.Binding.StringToNullableLongTypeConverter`, `ReactiveUI.Binding.StringToNullableShortTypeConverter`, `ReactiveUI.Binding.StringToNullableSingleTypeConverter`, `ReactiveUI.Binding.StringToNullableTimeOnlyTypeConverter`, `ReactiveUI.Binding.StringToNullableTimeSpanTypeConverter`.

#### `ByteToNullableByteTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ByteToNullableByteTypeConverter` | Converts a [`byte`](https://learn.microsoft.com/dotnet/api/system.byte) to a nullable [`byte`](https://learn.microsoft.com/dotnet/api/system.byte); the conversion always succeeds. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ByteToNullableByteTypeConverter()` | Initializes a new instance of the ByteToNullableByteTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvert(byte from, object? conversionHint, out byte? result)` | Converts a value to the target type without boxing. | [`byte`](https://learn.microsoft.com/dotnet/api/system.byte) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`byte?`](https://learn.microsoft.com/dotnet/api/system.byte) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `DecimalToNullableDecimalTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class DecimalToNullableDecimalTypeConverter` | Converts a [`decimal`](https://learn.microsoft.com/dotnet/api/system.decimal) to a nullable [`decimal`](https://learn.microsoft.com/dotnet/api/system.decimal); the conversion always succeeds. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `DecimalToNullableDecimalTypeConverter()` | Initializes a new instance of the DecimalToNullableDecimalTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvert(decimal from, object? conversionHint, out decimal? result)` | Converts a value to the target type without boxing. | [`decimal`](https://learn.microsoft.com/dotnet/api/system.decimal) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`decimal?`](https://learn.microsoft.com/dotnet/api/system.decimal) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `DoubleToNullableDoubleTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class DoubleToNullableDoubleTypeConverter` | Converts a [`double`](https://learn.microsoft.com/dotnet/api/system.double) to a nullable [`double`](https://learn.microsoft.com/dotnet/api/system.double); the conversion always succeeds. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `DoubleToNullableDoubleTypeConverter()` | Initializes a new instance of the DoubleToNullableDoubleTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvert(double from, object? conversionHint, out double? result)` | Converts a value to the target type without boxing. | [`double`](https://learn.microsoft.com/dotnet/api/system.double) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`double?`](https://learn.microsoft.com/dotnet/api/system.double) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `IntegerToNullableIntegerTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class IntegerToNullableIntegerTypeConverter` | Converts an [`int`](https://learn.microsoft.com/dotnet/api/system.int32) to a nullable [`int`](https://learn.microsoft.com/dotnet/api/system.int32); the conversion always succeeds. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `IntegerToNullableIntegerTypeConverter()` | Initializes a new instance of the IntegerToNullableIntegerTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvert(int from, object? conversionHint, out int? result)` | Converts a value to the target type without boxing. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`int?`](https://learn.microsoft.com/dotnet/api/system.int32) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `LongToNullableLongTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class LongToNullableLongTypeConverter` | Converts a [`long`](https://learn.microsoft.com/dotnet/api/system.int64) to a nullable [`long`](https://learn.microsoft.com/dotnet/api/system.int64); the conversion always succeeds. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `LongToNullableLongTypeConverter()` | Initializes a new instance of the LongToNullableLongTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvert(long from, object? conversionHint, out long? result)` | Converts a value to the target type without boxing. | [`long`](https://learn.microsoft.com/dotnet/api/system.int64) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`long?`](https://learn.microsoft.com/dotnet/api/system.int64) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `NullableBooleanToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableBooleanToStringTypeConverter : BindingTypeConverter<bool?, string>` | Converts a nullable [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) as "True" or "False". A null value succeeds with a null string. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableBooleanToStringTypeConverter()` | Initializes a new instance of the NullableBooleanToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(bool? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`bool?`](https://learn.microsoft.com/dotnet/api/system.boolean) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `NullableByteToByteTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableByteToByteTypeConverter` | Converts a nullable [`byte`](https://learn.microsoft.com/dotnet/api/system.byte) to a [`byte`](https://learn.microsoft.com/dotnet/api/system.byte); a null value fails the conversion. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableByteToByteTypeConverter()` | Initializes a new instance of the NullableByteToByteTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvert(byte? from, object? conversionHint, out byte result)` | Converts a value to the target type without boxing. | [`byte?`](https://learn.microsoft.com/dotnet/api/system.byte) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`byte`](https://learn.microsoft.com/dotnet/api/system.byte) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `NullableByteToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableByteToStringTypeConverter : BindingTypeConverter<byte?, string>` | Converts a nullable [`byte`](https://learn.microsoft.com/dotnet/api/system.byte) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) using the current culture. An [`int`](https://learn.microsoft.com/dotnet/api/system.int32) hint gives the minimum digit count (the `D` format) and a [`string`](https://learn.microsoft.com/dotnet/api/system.string) hint gives the format string; a malformed format throws [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). A null value succeeds with a null string. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableByteToStringTypeConverter()` | Initializes a new instance of the NullableByteToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(byte? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`byte?`](https://learn.microsoft.com/dotnet/api/system.byte) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `NullableDateOnlyToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableDateOnlyToStringTypeConverter : BindingTypeConverter<DateOnly?, string>` | Converts a nullable [`DateOnly`](https://learn.microsoft.com/dotnet/api/system.dateonly) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) in the short date format of the current culture. A null value succeeds with a null string. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableDateOnlyToStringTypeConverter()` | Initializes a new instance of the NullableDateOnlyToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(DateOnly? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`DateOnly?`](https://learn.microsoft.com/dotnet/api/system.dateonly) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `NullableDateTimeOffsetToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableDateTimeOffsetToStringTypeConverter : BindingTypeConverter<DateTimeOffset?, string>` | Converts a nullable [`DateTimeOffset`](https://learn.microsoft.com/dotnet/api/system.datetimeoffset) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) in the general date and time format of the current culture, including the offset. A null value succeeds with a null string. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableDateTimeOffsetToStringTypeConverter()` | Initializes a new instance of the NullableDateTimeOffsetToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(DateTimeOffset? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`DateTimeOffset?`](https://learn.microsoft.com/dotnet/api/system.datetimeoffset) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `NullableDateTimeToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableDateTimeToStringTypeConverter : BindingTypeConverter<DateTime?, string>` | Converts a nullable [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) in the general date and time format of the current culture. A null value succeeds with a null string. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableDateTimeToStringTypeConverter()` | Initializes a new instance of the NullableDateTimeToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(DateTime? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`DateTime?`](https://learn.microsoft.com/dotnet/api/system.datetime) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `NullableDecimalToDecimalTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableDecimalToDecimalTypeConverter` | Converts a nullable [`decimal`](https://learn.microsoft.com/dotnet/api/system.decimal) to a [`decimal`](https://learn.microsoft.com/dotnet/api/system.decimal); a null value fails the conversion. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableDecimalToDecimalTypeConverter()` | Initializes a new instance of the NullableDecimalToDecimalTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvert(decimal? from, object? conversionHint, out decimal result)` | Converts a value to the target type without boxing. | [`decimal?`](https://learn.microsoft.com/dotnet/api/system.decimal) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`decimal`](https://learn.microsoft.com/dotnet/api/system.decimal) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `NullableDecimalToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableDecimalToStringTypeConverter : BindingTypeConverter<decimal?, string>` | Converts a nullable [`decimal`](https://learn.microsoft.com/dotnet/api/system.decimal) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) using the current culture. An [`int`](https://learn.microsoft.com/dotnet/api/system.int32) hint gives the number of decimal places (the `F` format) and a [`string`](https://learn.microsoft.com/dotnet/api/system.string) hint gives the format string; a malformed format throws [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). A null value succeeds with a null string. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableDecimalToStringTypeConverter()` | Initializes a new instance of the NullableDecimalToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(decimal? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`decimal?`](https://learn.microsoft.com/dotnet/api/system.decimal) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `NullableDoubleToDoubleTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableDoubleToDoubleTypeConverter` | Converts a nullable [`double`](https://learn.microsoft.com/dotnet/api/system.double) to a [`double`](https://learn.microsoft.com/dotnet/api/system.double); a null value fails the conversion. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableDoubleToDoubleTypeConverter()` | Initializes a new instance of the NullableDoubleToDoubleTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvert(double? from, object? conversionHint, out double result)` | Converts a value to the target type without boxing. | [`double?`](https://learn.microsoft.com/dotnet/api/system.double) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`double`](https://learn.microsoft.com/dotnet/api/system.double) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `NullableDoubleToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableDoubleToStringTypeConverter : BindingTypeConverter<double?, string>` | Converts a nullable [`double`](https://learn.microsoft.com/dotnet/api/system.double) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) using the current culture. An [`int`](https://learn.microsoft.com/dotnet/api/system.int32) hint gives the number of decimal places (the `F` format) and a [`string`](https://learn.microsoft.com/dotnet/api/system.string) hint gives the format string; a malformed format throws [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). A null value succeeds with a null string. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableDoubleToStringTypeConverter()` | Initializes a new instance of the NullableDoubleToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(double? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`double?`](https://learn.microsoft.com/dotnet/api/system.double) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `NullableGuidToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableGuidToStringTypeConverter : BindingTypeConverter<Guid?, string>` | Converts a nullable [`Guid`](https://learn.microsoft.com/dotnet/api/system.guid) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) in the hyphenated `D` format. A null value succeeds with a null string. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableGuidToStringTypeConverter()` | Initializes a new instance of the NullableGuidToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(Guid? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`Guid?`](https://learn.microsoft.com/dotnet/api/system.guid) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `NullableIntegerToIntegerTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableIntegerToIntegerTypeConverter` | Converts a nullable [`int`](https://learn.microsoft.com/dotnet/api/system.int32) to an [`int`](https://learn.microsoft.com/dotnet/api/system.int32); a null value fails the conversion. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableIntegerToIntegerTypeConverter()` | Initializes a new instance of the NullableIntegerToIntegerTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvert(int? from, object? conversionHint, out int result)` | Converts a value to the target type without boxing. | [`int?`](https://learn.microsoft.com/dotnet/api/system.int32) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `NullableIntegerToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableIntegerToStringTypeConverter : BindingTypeConverter<int?, string>` | Converts a nullable [`int`](https://learn.microsoft.com/dotnet/api/system.int32) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) using the current culture. An [`int`](https://learn.microsoft.com/dotnet/api/system.int32) hint gives the minimum digit count (the `D` format) and a [`string`](https://learn.microsoft.com/dotnet/api/system.string) hint gives the format string; a malformed format throws [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). A null value succeeds with a null string. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableIntegerToStringTypeConverter()` | Initializes a new instance of the NullableIntegerToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(int? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`int?`](https://learn.microsoft.com/dotnet/api/system.int32) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `NullableLongToLongTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableLongToLongTypeConverter` | Converts a nullable [`long`](https://learn.microsoft.com/dotnet/api/system.int64) to a [`long`](https://learn.microsoft.com/dotnet/api/system.int64); a null value fails the conversion. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableLongToLongTypeConverter()` | Initializes a new instance of the NullableLongToLongTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvert(long? from, object? conversionHint, out long result)` | Converts a value to the target type without boxing. | [`long?`](https://learn.microsoft.com/dotnet/api/system.int64) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`long`](https://learn.microsoft.com/dotnet/api/system.int64) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `NullableLongToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableLongToStringTypeConverter : BindingTypeConverter<long?, string>` | Converts a nullable [`long`](https://learn.microsoft.com/dotnet/api/system.int64) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) using the current culture. An [`int`](https://learn.microsoft.com/dotnet/api/system.int32) hint gives the minimum digit count (the `D` format) and a [`string`](https://learn.microsoft.com/dotnet/api/system.string) hint gives the format string; a malformed format throws [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). A null value succeeds with a null string. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableLongToStringTypeConverter()` | Initializes a new instance of the NullableLongToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(long? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`long?`](https://learn.microsoft.com/dotnet/api/system.int64) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `NullableShortToShortTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableShortToShortTypeConverter` | Converts a nullable [`short`](https://learn.microsoft.com/dotnet/api/system.int16) to a [`short`](https://learn.microsoft.com/dotnet/api/system.int16); a null value fails the conversion. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableShortToShortTypeConverter()` | Initializes a new instance of the NullableShortToShortTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvert(short? from, object? conversionHint, out short result)` | Converts a value to the target type without boxing. | [`short?`](https://learn.microsoft.com/dotnet/api/system.int16) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`short`](https://learn.microsoft.com/dotnet/api/system.int16) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `NullableShortToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableShortToStringTypeConverter : BindingTypeConverter<short?, string>` | Converts a nullable [`short`](https://learn.microsoft.com/dotnet/api/system.int16) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) using the current culture. An [`int`](https://learn.microsoft.com/dotnet/api/system.int32) hint gives the minimum digit count (the `D` format) and a [`string`](https://learn.microsoft.com/dotnet/api/system.string) hint gives the format string; a malformed format throws [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). A null value succeeds with a null string. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableShortToStringTypeConverter()` | Initializes a new instance of the NullableShortToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(short? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`short?`](https://learn.microsoft.com/dotnet/api/system.int16) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `NullableSingleToSingleTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableSingleToSingleTypeConverter` | Converts a nullable [`float`](https://learn.microsoft.com/dotnet/api/system.single) to a [`float`](https://learn.microsoft.com/dotnet/api/system.single); a null value fails the conversion. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableSingleToSingleTypeConverter()` | Initializes a new instance of the NullableSingleToSingleTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvert(float? from, object? conversionHint, out float result)` | Converts a value to the target type without boxing. | [`float?`](https://learn.microsoft.com/dotnet/api/system.single) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`float`](https://learn.microsoft.com/dotnet/api/system.single) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `NullableSingleToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableSingleToStringTypeConverter : BindingTypeConverter<float?, string>` | Converts a nullable [`float`](https://learn.microsoft.com/dotnet/api/system.single) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) using the current culture. An [`int`](https://learn.microsoft.com/dotnet/api/system.int32) hint gives the number of decimal places (the `F` format) and a [`string`](https://learn.microsoft.com/dotnet/api/system.string) hint gives the format string; a malformed format throws [`FormatException`](https://learn.microsoft.com/dotnet/api/system.formatexception). A null value succeeds with a null string. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableSingleToStringTypeConverter()` | Initializes a new instance of the NullableSingleToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(float? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`float?`](https://learn.microsoft.com/dotnet/api/system.single) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `NullableTimeOnlyToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableTimeOnlyToStringTypeConverter : BindingTypeConverter<TimeOnly?, string>` | Converts a nullable [`TimeOnly`](https://learn.microsoft.com/dotnet/api/system.timeonly) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) in the short time format of the current culture. A null value succeeds with a null string. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableTimeOnlyToStringTypeConverter()` | Initializes a new instance of the NullableTimeOnlyToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(TimeOnly? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`TimeOnly?`](https://learn.microsoft.com/dotnet/api/system.timeonly) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `NullableTimeSpanToStringTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NullableTimeSpanToStringTypeConverter : BindingTypeConverter<TimeSpan?, string>` | Converts a nullable [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) to a [`string`](https://learn.microsoft.com/dotnet/api/system.string) in the culture-invariant constant format (`c`). A null value succeeds with a null string. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NullableTimeSpanToStringTypeConverter()` | Initializes a new instance of the NullableTimeSpanToStringTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(TimeSpan? from, object? conversionHint, out string? result)` | Converts a value to the target type without boxing. | [`TimeSpan?`](https://learn.microsoft.com/dotnet/api/system.timespan) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `ShortToNullableShortTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ShortToNullableShortTypeConverter` | Converts a [`short`](https://learn.microsoft.com/dotnet/api/system.int16) to a nullable [`short`](https://learn.microsoft.com/dotnet/api/system.int16); the conversion always succeeds. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ShortToNullableShortTypeConverter()` | Initializes a new instance of the ShortToNullableShortTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvert(short from, object? conversionHint, out short? result)` | Converts a value to the target type without boxing. | [`short`](https://learn.microsoft.com/dotnet/api/system.int16) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`short?`](https://learn.microsoft.com/dotnet/api/system.int16) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `SingleToNullableSingleTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class SingleToNullableSingleTypeConverter` | Converts a [`float`](https://learn.microsoft.com/dotnet/api/system.single) to a nullable [`float`](https://learn.microsoft.com/dotnet/api/system.single); the conversion always succeeds. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `SingleToNullableSingleTypeConverter()` | Initializes a new instance of the SingleToNullableSingleTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvert(float from, object? conversionHint, out float? result)` | Converts a value to the target type without boxing. | [`float`](https://learn.microsoft.com/dotnet/api/system.single) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`float?`](https://learn.microsoft.com/dotnet/api/system.single) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `StringToNullableBooleanTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToNullableBooleanTypeConverter : BindingTypeConverter<string, bool?>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a nullable [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) with `TryParse` (case-insensitive `true` or `false`); a null or empty string succeeds with a null result and an unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToNullableBooleanTypeConverter()` | Initializes a new instance of the StringToNullableBooleanTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out bool? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`bool?`](https://learn.microsoft.com/dotnet/api/system.boolean) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToNullableByteTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToNullableByteTypeConverter : BindingTypeConverter<string, byte?>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a nullable [`byte`](https://learn.microsoft.com/dotnet/api/system.byte) with `TryParse` under the current culture; a null or empty string succeeds with a null result and an unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToNullableByteTypeConverter()` | Initializes a new instance of the StringToNullableByteTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out byte? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`byte?`](https://learn.microsoft.com/dotnet/api/system.byte) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToNullableDateOnlyTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToNullableDateOnlyTypeConverter : BindingTypeConverter<string, DateOnly?>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a nullable [`DateOnly`](https://learn.microsoft.com/dotnet/api/system.dateonly) with `TryParse` under the current culture; a null or empty string succeeds with a null result and an unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToNullableDateOnlyTypeConverter()` | Initializes a new instance of the StringToNullableDateOnlyTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out DateOnly? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`DateOnly?`](https://learn.microsoft.com/dotnet/api/system.dateonly) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToNullableDateTimeOffsetTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToNullableDateTimeOffsetTypeConverter : BindingTypeConverter<string, DateTimeOffset?>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a nullable [`DateTimeOffset`](https://learn.microsoft.com/dotnet/api/system.datetimeoffset) with `TryParse` under the current culture; a null or empty string succeeds with a null result and an unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToNullableDateTimeOffsetTypeConverter()` | Initializes a new instance of the StringToNullableDateTimeOffsetTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out DateTimeOffset? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`DateTimeOffset?`](https://learn.microsoft.com/dotnet/api/system.datetimeoffset) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToNullableDateTimeTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToNullableDateTimeTypeConverter : BindingTypeConverter<string, DateTime?>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a nullable [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) with `TryParse` under the current culture; a null or empty string succeeds with a null result and an unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToNullableDateTimeTypeConverter()` | Initializes a new instance of the StringToNullableDateTimeTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out DateTime? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`DateTime?`](https://learn.microsoft.com/dotnet/api/system.datetime) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToNullableDecimalTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToNullableDecimalTypeConverter : BindingTypeConverter<string, decimal?>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a nullable [`decimal`](https://learn.microsoft.com/dotnet/api/system.decimal) with `TryParse` under the current culture; a null or empty string succeeds with a null result and an unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToNullableDecimalTypeConverter()` | Initializes a new instance of the StringToNullableDecimalTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out decimal? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`decimal?`](https://learn.microsoft.com/dotnet/api/system.decimal) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToNullableDoubleTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToNullableDoubleTypeConverter : BindingTypeConverter<string, double?>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a nullable [`double`](https://learn.microsoft.com/dotnet/api/system.double) with `TryParse` under the current culture; a null or empty string succeeds with a null result and an unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToNullableDoubleTypeConverter()` | Initializes a new instance of the StringToNullableDoubleTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out double? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`double?`](https://learn.microsoft.com/dotnet/api/system.double) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToNullableGuidTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToNullableGuidTypeConverter : BindingTypeConverter<string, Guid?>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a nullable [`Guid`](https://learn.microsoft.com/dotnet/api/system.guid) with `TryParse`; a null or empty string succeeds with a null result and an unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToNullableGuidTypeConverter()` | Initializes a new instance of the StringToNullableGuidTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out Guid? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`Guid?`](https://learn.microsoft.com/dotnet/api/system.guid) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToNullableIntegerTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToNullableIntegerTypeConverter : BindingTypeConverter<string, int?>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a nullable [`int`](https://learn.microsoft.com/dotnet/api/system.int32) with `TryParse` under the current culture; a null or empty string succeeds with a null result and an unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToNullableIntegerTypeConverter()` | Initializes a new instance of the StringToNullableIntegerTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out int? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`int?`](https://learn.microsoft.com/dotnet/api/system.int32) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToNullableLongTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToNullableLongTypeConverter : BindingTypeConverter<string, long?>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a nullable [`long`](https://learn.microsoft.com/dotnet/api/system.int64) with `TryParse` under the current culture; a null or empty string succeeds with a null result and an unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToNullableLongTypeConverter()` | Initializes a new instance of the StringToNullableLongTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out long? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`long?`](https://learn.microsoft.com/dotnet/api/system.int64) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToNullableShortTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToNullableShortTypeConverter : BindingTypeConverter<string, short?>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a nullable [`short`](https://learn.microsoft.com/dotnet/api/system.int16) with `TryParse` under the current culture; a null or empty string succeeds with a null result and an unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToNullableShortTypeConverter()` | Initializes a new instance of the StringToNullableShortTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out short? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`short?`](https://learn.microsoft.com/dotnet/api/system.int16) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToNullableSingleTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToNullableSingleTypeConverter : BindingTypeConverter<string, float?>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a nullable [`float`](https://learn.microsoft.com/dotnet/api/system.single) with `TryParse` under the current culture; a null or empty string succeeds with a null result and an unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToNullableSingleTypeConverter()` | Initializes a new instance of the StringToNullableSingleTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out float? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`float?`](https://learn.microsoft.com/dotnet/api/system.single) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToNullableTimeOnlyTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToNullableTimeOnlyTypeConverter : BindingTypeConverter<string, TimeOnly?>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a nullable [`TimeOnly`](https://learn.microsoft.com/dotnet/api/system.timeonly) with `TryParse` under the current culture; a null or empty string succeeds with a null result and an unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToNullableTimeOnlyTypeConverter()` | Initializes a new instance of the StringToNullableTimeOnlyTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out TimeOnly? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`TimeOnly?`](https://learn.microsoft.com/dotnet/api/system.timeonly) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `StringToNullableTimeSpanTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringToNullableTimeSpanTypeConverter : BindingTypeConverter<string, TimeSpan?>` | Converts a [`string`](https://learn.microsoft.com/dotnet/api/system.string) to a nullable [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) with `TryParse` under the current culture; a null or empty string succeeds with a null result and an unparseable string fails. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringToNullableTimeSpanTypeConverter()` | Initializes a new instance of the StringToNullableTimeSpanTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(string? from, object? conversionHint, out TimeSpan? result)` | Converts a value to the target type without boxing. | [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`TimeSpan?`](https://learn.microsoft.com/dotnet/api/system.timespan) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

### Custom converters

[Full description and examples](converters.md).

Types: `ReactiveUI.Binding.BindingTypeConverter<TFrom, TTo>`, `ReactiveUI.Binding.Fallback.TwoWayConverterPair<TSourceProp, TTargetProp>`, `ReactiveUI.Binding.Fallback.TwoWayConverters`, `ReactiveUI.Binding.IBindingTypeConverter`, `ReactiveUI.Binding.IBindingTypeConverter<TFrom, TTo>`, `ReactiveUI.Binding.ISetMethodBindingConverter`.

#### `BindingTypeConverter<TFrom, TTo>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `abstract class BindingTypeConverter<TFrom, TTo>` | Base class for a converter between one type pair; it supplies `FromType`, `ToType` and an object-based `TryConvertTyped` over the typed `TryConvert`. | `TFrom`: The source type to convert from; `TTo`: The target type to convert to | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `protected BindingTypeConverter()` | Initializes a new instance of the BindingTypeConverter<TFrom, TTo> class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `abstract GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `abstract TryConvert(TFrom? from, object? conversionHint, out TTo? result)` | Converts a value to the target type without boxing. | `TFrom?` `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out `TTo?` `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value by casting it to `TFrom` and calling `TryConvert`. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value. A null is passed on as `default` when `TFrom` can hold null, and fails when it cannot; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, passed to `TryConvert` unchanged; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value, or null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `false` when `from` is not a `TFrom` or `TryConvert` fails; otherwise `true`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `TwoWayConverterPair<TSourceProp, TTargetProp>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed record TwoWayConverterPair<TSourceProp, TTargetProp>` | The pair of conversions a two-way binding needs to move a value in either direction. | `TSourceProp`: The type of the source property; `TTargetProp`: The type of the target property | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `TwoWayConverterPair(Func<TSourceProp, TTargetProp> Forward, Func<TTargetProp, TSourceProp> Reverse)` | The pair of conversions a two-way binding needs to move a value in either direction. | [`Func<TSourceProp, TTargetProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `Forward`: Converts a source value to the target's type; [`Func<TTargetProp, TSourceProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `Reverse`: Converts a target value back to the source's type | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Forward { get; init; }` | Converts a source value to the target's type. | None. | [`Func<TSourceProp, TTargetProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) |
| `Reverse { get; init; }` | Converts a target value back to the source's type. | None. | [`Func<TTargetProp, TSourceProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) |

#### `TwoWayConverters`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class TwoWayConverters` | Builds a [`TwoWayConverterPair<TSourceProp, TTargetProp>`](converters.md) without naming its type arguments. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Create<TSourceProp, TTargetProp>(Func<TSourceProp, TTargetProp> forward, Func<TTargetProp, TSourceProp> reverse)` | Pairs a forward and a reverse conversion. | `TSourceProp`: The type of the source property; `TTargetProp`: The type of the target property; [`Func<TSourceProp, TTargetProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `forward`: Converts a source value to the target's type; [`Func<TTargetProp, TSourceProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `reverse`: Converts a target value back to the source's type | [`TwoWayConverterPair<TSourceProp, TTargetProp>`](converters.md): The paired conversions. |

#### `IBindingTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IBindingTypeConverter` | Converts values from `FromType` to `ToType` for the binding APIs. Register an implementation to teach a binding how to convert between two types. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `IBindingTypeConverter<TFrom, TTo>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IBindingTypeConverter<TFrom, TTo>` | Extends [`IBindingTypeConverter`](converters.md) with a typed `TryConvert` for one type pair. | `TFrom`: The source type to convert from; `TTo`: The target type to convert to | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `TryConvert(TFrom? from, object? conversionHint, out TTo? result)` | Converts a value to the target type without boxing. | `TFrom?` `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out `TTo?` `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `ISetMethodBindingConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface ISetMethodBindingConverter` | Replaces how a binding writes a value to its target, for example to fill a collection instead of assigning a property. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects(Type? fromType, Type? toType)` | Returns this converter's priority for writing a value of one type to a target of another. | [`Type?`](https://learn.microsoft.com/dotnet/api/system.type) `fromType`: The type of the value being written; may be null; [`Type?`](https://learn.microsoft.com/dotnet/api/system.type) `toType`: The type of the target being written to; may be null | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when `PerformSet` applies; zero or less excludes the converter. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `PerformSet(object? toTarget, object? newValue, object?[]? arguments)` | Writes a value to the target. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `toTarget`: The object being written to; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `newValue`: The value to write; [`object?[]?`](https://learn.microsoft.com/dotnet/api/system.object) `arguments`: The index arguments for an indexer target; a generated collection write passes null | [`object?`](https://learn.microsoft.com/dotnet/api/system.object): The result of the write; a generated collection write casts it to the target type and reports it as the new value. |

### Converter registries

[Full description and examples](converters.md).

Types: `ReactiveUI.Binding.BindingConverters`, `ReactiveUI.Binding.BindingTypeConverterRegistry`, `ReactiveUI.Binding.ConverterService`, `ReactiveUI.Binding.DefaultConverterRegistration`, `ReactiveUI.Binding.SetMethodBindingConverterRegistry`.

#### `BindingConverters`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class BindingConverters` | Provides the process-wide [`ConverterService`](converters.md) that generated and runtime bindings convert through. | None. | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Current { get; }` | Gets the converter service in use. | None. | [`ConverterService`](converters.md) |

#### `BindingTypeConverterRegistry`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class BindingTypeConverterRegistry` | Holds typed binding converters grouped by their exact source and target type pair. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BindingTypeConverterRegistry()` | Initializes a new instance of the BindingTypeConverterRegistry class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAllConverters()` | Returns a copy of every registered converter, in no particular order across type pairs. | None. | [`IEnumerable<IBindingTypeConverter>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1): The converters registered at the time of the call; empty when none are registered. Later registrations do not change the returned sequence. |
| `Register(IBindingTypeConverter converter)` | Registers a converter under its `FromType` and `ToType` pair. | [`IBindingTypeConverter`](converters.md) `converter`: The converter to register | — |
| `TryGetConverter(Type fromType, Type toType)` | Returns the registered converter with the highest positive affinity for the exact type pair. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `fromType`: The source type to convert from; [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `toType`: The target type to convert to | [`IBindingTypeConverter?`](converters.md): The best converter, the earliest registered one on a tie in affinity; `null` when none is registered for the pair or every one reports an affinity of zero or less. |

#### `ConverterService`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ConverterService` | Owns the typed, fallback and set-method converter registries and resolves the best converter for a type pair. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ConverterService()` | Initializes a new instance of the [`ConverterService`](converters.md) class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ResolveConverter(Type fromType, Type toType)` | Returns the best typed converter for the type pair, or else the best fallback converter. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `fromType`: The source type to convert from; [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `toType`: The target type to convert to | [`object?`](https://learn.microsoft.com/dotnet/api/system.object): An [`IBindingTypeConverter`](converters.md) or an [`IBindingFallbackConverter`](converters.md); a typed converter always wins over a fallback one whatever their affinities. `null` when neither registry has a match. |
| `ResolveSetMethodConverter(Type? fromType, Type? toType)` | Returns the best set-method converter for the type pair. | [`Type?`](https://learn.microsoft.com/dotnet/api/system.type) `fromType`: The type of the value being written; may be null; [`Type?`](https://learn.microsoft.com/dotnet/api/system.type) `toType`: The type of the target being written to; may be null | [`ISetMethodBindingConverter?`](converters.md): The converter with the highest positive affinity, or `null` when none applies. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FallbackConverters { get; }` | Gets the registry of fallback converters, consulted when no typed converter applies. | None. | [`BindingFallbackConverterRegistry`](converters.md) |
| `SetMethodConverters { get; }` | Gets the registry of converters that replace how a binding writes to its target. | None. | [`SetMethodBindingConverterRegistry`](converters.md) |
| `TypedConverters { get; }` | Gets the registry of typed converters, each matched to an exact source and target type pair. | None. | [`BindingTypeConverterRegistry`](converters.md) |

#### `DefaultConverterRegistration`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class DefaultConverterRegistration` | Registers the built-in typed converters with a [`ConverterService`](converters.md). | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `RegisterDefaults(ConverterService service)` | Registers the string identity, equality, boolean, Guid, Uri, numeric and date and time converters, each with its nullable forms. | [`ConverterService`](converters.md) `service`: The converter service to register the converters with | — |

#### `SetMethodBindingConverterRegistry`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class SetMethodBindingConverterRegistry` | Holds set-method binding converters, which are asked about a type pair at lookup time rather than grouped by it. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `SetMethodBindingConverterRegistry()` | Initializes a new instance of the SetMethodBindingConverterRegistry class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAllConverters()` | Returns a copy of every registered set-method converter, in registration order. | None. | [`IEnumerable<ISetMethodBindingConverter>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1): The converters registered at the time of the call; empty when none are registered. |
| `Register(ISetMethodBindingConverter converter)` | Registers a set-method binding converter. | [`ISetMethodBindingConverter`](converters.md) `converter`: The converter to register | — |
| `TryGetConverter(Type? fromType, Type? toType)` | Asks each set-method converter for its affinity to the type pair and returns the highest positive one. | [`Type?`](https://learn.microsoft.com/dotnet/api/system.type) `fromType`: The type of the value being written; may be null; [`Type?`](https://learn.microsoft.com/dotnet/api/system.type) `toType`: The type of the target being written to; may be null | [`ISetMethodBindingConverter?`](converters.md): The converter with the highest positive affinity, the earliest registered one on a tie; `null` when none reports a positive affinity. |

### Fallback converters

[Full description and examples](converters.md).

Types: `ReactiveUI.Binding.BindingFallbackConverterRegistry`, `ReactiveUI.Binding.IBindingFallbackConverter`.

#### `BindingFallbackConverterRegistry`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class BindingFallbackConverterRegistry` | Holds fallback binding converters, which are asked about a type pair at lookup time rather than grouped by it. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BindingFallbackConverterRegistry()` | Initializes a new instance of the BindingFallbackConverterRegistry class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAllConverters()` | Returns a copy of every registered fallback converter, in registration order. | None. | [`IEnumerable<IBindingFallbackConverter>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1): The converters registered at the time of the call; empty when none are registered. |
| `Register(IBindingFallbackConverter converter)` | Registers a fallback binding converter. | [`IBindingFallbackConverter`](converters.md) `converter`: The converter to register | — |
| `TryGetConverter(Type fromType, Type toType)` | Asks each fallback converter for its affinity to the type pair and returns the highest positive one. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `fromType`: The source type to convert from; [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `toType`: The target type to convert to | [`IBindingFallbackConverter?`](converters.md): The converter with the highest positive affinity, the earliest registered one on a tie; `null` when none reports a positive affinity. |

#### `IBindingFallbackConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IBindingFallbackConverter` | Converts runtime type pairs that no typed converter covers. The converter service asks the fallback converters only when no typed converter with a positive affinity is registered for the pair. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects(Type fromType, Type toType)` | Calculates affinity for the specified runtime type pair. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `fromType`: The runtime source type; [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `toType`: The target type | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter handles the pair, where a higher value wins over other fallback converters; zero or less when it does not. |
| `TryConvert(Type fromType, object from, Type toType, object? conversionHint, out object? result)` | Attempts to convert the value to the target type. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `fromType`: The runtime source type (guaranteed non-null); [`object`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The value to convert (guaranteed non-null); [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `toType`: The target type (guaranteed non-null); [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined conversion hint (e.g., format string, culture); out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value. Implementations must produce a non-null value when returning `true` | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

### Migrating converters

[Full description and examples](converters.md).

Types: `ReactiveUI.Binding.ConverterMigrationHelper`, `ReactiveUI.Binding.ConverterMigrationHelperMixins`, `ReactiveUI.Binding.ExtractedConverters`.

#### `ConverterMigrationHelper`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class ConverterMigrationHelper` | Reads converters that were registered with a Splat dependency resolver. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ExtractConverters(IReadonlyDependencyResolver resolver)` | Collects every typed, fallback and set-method converter registered with a Splat resolver, without registering them anywhere. | `IReadonlyDependencyResolver` `resolver`: The Splat resolver to read | [`ExtractedConverters`](converters.md): The converters the resolver holds, in the order it returns them; a null registration is skipped. |

#### `ConverterMigrationHelperMixins`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class ConverterMigrationHelperMixins` | Extension members that import Splat-registered converters into a [`ConverterService`](converters.md). | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ConverterService.ImportFrom(IReadonlyDependencyResolver resolver)` | Registers the converters held by a Splat resolver with this service. | [`ConverterService`](converters.md) `converterService` (receiver); `IReadonlyDependencyResolver` `resolver`: The Splat resolver to import converters from | — |

#### `ExtractedConverters`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed record ExtractedConverters` | The converters found in a dependency resolver, grouped by the role each one fills. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ExtractedConverters(IList<IBindingTypeConverter> TypedConverters, IList<IBindingFallbackConverter> FallbackConverters, IList<ISetMethodBindingConverter> SetMethodConverters)` | The converters found in a dependency resolver, grouped by the role each one fills. | [`IList<IBindingTypeConverter>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1) `TypedConverters`: The converters that move a value between two known types; [`IList<IBindingFallbackConverter>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1) `FallbackConverters`: The converters consulted when no typed converter matches; [`IList<ISetMethodBindingConverter>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1) `SetMethodConverters`: The converters that write a value to a target rather than return one | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FallbackConverters { get; init; }` | The converters consulted when no typed converter matches. | None. | [`IList<IBindingFallbackConverter>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1) |
| `SetMethodConverters { get; init; }` | The converters that write a value to a target rather than return one. | None. | [`IList<ISetMethodBindingConverter>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1) |
| `TypedConverters { get; init; }` | The converters that move a value between two known types. | None. | [`IList<IBindingTypeConverter>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1) |

### String and equality converters

[Full description and examples](converters.md).

Types: `ReactiveUI.Binding.EqualityTypeConverter`, `ReactiveUI.Binding.StringConverter`.

#### `EqualityTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class EqualityTypeConverter` | Converts any value to a [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) that says whether it equals the conversion hint, using `Equals`. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `EqualityTypeConverter()` | Initializes a new instance of the EqualityTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

#### `StringConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class StringConverter` | Passes a [`string`](https://learn.microsoft.com/dotnet/api/system.string) through unchanged; a null or non-string value fails the conversion. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StringConverter()` | Initializes a new instance of the StringConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The registry picks the highest value and, on a tie, the earliest registered converter. |
| `TryConvertTyped(object? from, object? conversionHint, out object? result)` | Converts a boxed value. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `from`: The source value; null is accepted only where the converter can convert null; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint, such as a format string; out [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `result`: The converted value; null when the conversion fails or produces a null | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FromType { get; }` | Gets the source type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |
| `ToType { get; }` | Gets the target type supported by this converter. | None. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) |

## Mechanisms

### Binding affinity

[Full description and examples](mechanisms.md).

Types: `ReactiveUI.Binding.BindingAffinity`, `ReactiveUI.Binding.Fallback.CommandBindingAffinityChecker`, `ReactiveUI.Binding.Fallback.ObservationAffinityChecker`.

#### `BindingAffinity`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class BindingAffinity` | Common affinity scores shared by binding type converters, property observation factories, command binders, and the source generator's observation/command plugins. A higher value indicates a stronger match; zero means the candidate does not apply. `DefaultInternalTypeConverter`, `DefaultEvent`, `Explicit` and `ExactType` carry the same values as ReactiveUI's scores of those names; the platform-specific scores are this library's own. | None. | — |

**Fields**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `readonly DefaultEvent` | The affinity for binding to a type's conventional default event. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) |
| `readonly DefaultInternalTypeConverter` | The affinity returned by the built-in value and string type converters. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) |
| `readonly EventEnabledControl` | The affinity for an event-enabled control command binding. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) |
| `readonly ExactType` | The affinity for a strong, exact-type match, such as IReactiveObject. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) |
| `readonly Explicit` | The affinity for an explicit or interface-based match, such as INotifyPropertyChanged or a named event. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) |
| `readonly Fallback` | The fallback affinity for the reflection-based POCO observer (lowest non-zero match). | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) |
| `readonly Kvo` | The affinity for an Apple KVO (`NSObject`) match. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) |
| `readonly WinFormsEvent` | The affinity for a WinForms event-based property observation match. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) |
| `readonly WinUiDependencyObject` | The affinity for a WinUI `DependencyObject` dependency-property match. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) |
| `readonly WpfDependencyObject` | The affinity for a WPF `DependencyObject` dependency-property match. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) |

#### `CommandBindingAffinityChecker`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class CommandBindingAffinityChecker` | Reports whether a registered [`ICreatesCommandBinding`](mechanisms.md) has a higher affinity for a control type than the mechanism the generator selected, so generated code can defer to it at runtime. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `HasHigherAffinityPlugin<T>(int generatedAffinity, bool hasEventTarget)` | Returns `true` if a registered [`ICreatesCommandBinding`](mechanisms.md) outranks `generatedAffinity`. | `T`: The control type being bound to; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `generatedAffinity`: The affinity of the source generator's selected plugin; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `hasEventTarget`: Whether the caller specifies a custom event target | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if a user plugin should override the generated binding; the registrations are read from the service locator on every call. |

#### `ObservationAffinityChecker`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class ObservationAffinityChecker` | Finds a registered [`ICreatesObservableForProperty`](mechanisms.md) whose affinity for a type and property is higher than the affinity of the mechanism the generator selected, so generated code can defer to it at runtime. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FindHigherAffinityPlugin(Type type, string propertyName, int generatedAffinity, bool beforeChanged)` | Finds the registered [`ICreatesObservableForProperty`](mechanisms.md) that outranks `generatedAffinity`. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `type`: The type being observed; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property being observed on that type; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `generatedAffinity`: The affinity of the source generator's selected plugin; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChanged`: Whether before-change (PropertyChanging) observation is requested | [`ICreatesObservableForProperty?`](mechanisms.md): The highest-scoring registration whose score exceeds `generatedAffinity`, or `null` when none does. |
| `HasHigherAffinityPlugin(Type type, string propertyName, int generatedAffinity, bool beforeChanged)` | Returns `true` if a registered [`ICreatesObservableForProperty`](mechanisms.md) outranks `generatedAffinity`. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `type`: The type being observed; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property being observed on that type; a plugin scores a type together with a property; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `generatedAffinity`: The affinity of the source generator's selected plugin; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChanged`: Whether before-change (PropertyChanging) observation is requested | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if a user plugin should override the generated observation. |
| `Refresh()` | Discards the cached registrations and scores so the next lookup reads the service locator again. | None. | — |

### INotifyPropertyChanged and plain objects

[Full description and examples](mechanisms.md).

Types: `ReactiveUI.Binding.ObservableForProperty.INPCObservableForProperty`, `ReactiveUI.Binding.ObservableForProperty.POCOObservableForProperty`.

#### `INPCObservableForProperty`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `class INPCObservableForProperty` | Observes properties of objects that implement [`INotifyPropertyChanged`](https://learn.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanged) or [`INotifyPropertyChanging`](https://learn.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanging). | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `INPCObservableForProperty()` | Initializes a new instance of the INPCObservableForProperty class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObject(Type type, string propertyName, bool beforeChanged)` | Returns the explicit affinity when the type implements [`INotifyPropertyChanging`](https://learn.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanging) (before-change) or [`INotifyPropertyChanged`](https://learn.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanged) (after-change); otherwise, zero. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `type`: The type being observed; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name being observed; not consulted; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChanged`: Whether before-change notifications are requested | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): The affinity score, or zero when the type lacks the required interface. |
| `GetNotificationForProperty(object sender, Expression expression, string propertyName, bool beforeChanged, bool suppressWarnings)` | Returns a sequence that emits when the sender raises a notification for the property, or a sequence that never emits when the sender implements neither interface. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `sender`: The object to observe; [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The expression identifying the property; an indexer matches the name followed by `[]`; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChanged`: Whether to observe [`INotifyPropertyChanging`](https://learn.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanging); a sender that lacks it is observed through [`INotifyPropertyChanged`](https://learn.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanged); [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `suppressWarnings`: Not used | [`IObservable<IObservedChange<object, object?>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable of observed changes. |

#### `POCOObservableForProperty`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class POCOObservableForProperty` | Observes a property on a type that offers no change notification, at the lowest affinity. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `POCOObservableForProperty()` | Initializes a new instance of the POCOObservableForProperty class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObject(Type type, string propertyName, bool beforeChanged)` | Returns the fallback affinity for every type and property. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `type`: The type being observed; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name being observed; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChanged`: Whether before-change notifications are requested; not consulted | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): The lowest positive affinity, so any other registered mechanism outranks it. |
| `GetNotificationForProperty(object sender, Expression expression, string propertyName, bool beforeChanged, bool suppressWarnings)` | Returns a sequence that emits one observed change on subscribe and then stays silent. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `sender`: The object to observe; [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The expression identifying the property; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChanged`: Whether before-change notifications are requested; not consulted; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `suppressWarnings`: `true` to skip the debug message written the first time a type and property are observed | [`IObservable<IObservedChange<object, object?>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits once and never completes. |

### Custom observation providers

[Full description and examples](mechanisms.md).

Types: `ReactiveUI.Binding.CreatesObservableForPropertyMixins`, `ReactiveUI.Binding.ICreatesObservableForProperty`, `ReactiveUI.Binding.Observables.PluginObservationSource`, `ReactiveUI.Binding.Observables.PluginPropertyObservable<T>`.

#### `CreatesObservableForPropertyMixins`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class CreatesObservableForPropertyMixins` | Overloads of [`ICreatesObservableForProperty`](mechanisms.md) members that default the before-change and warning flags to `false`. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ICreatesObservableForProperty.GetAffinityForObject(Type type, string propertyName)` | Returns the affinity for after-change observation of the specified property. | [`ICreatesObservableForProperty`](mechanisms.md) `factory` (receiver); [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `type`: The type being observed; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name being observed | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): The affinity score. Positive means supported. |
| `ICreatesObservableForProperty.GetNotificationForProperty(object sender, Expression expression, string propertyName)` | Creates an observable that fires after the specified property changes, without suppressing warnings. | [`ICreatesObservableForProperty`](mechanisms.md) `factory` (receiver); [`object`](https://learn.microsoft.com/dotnet/api/system.object) `sender`: The object to observe; [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The expression identifying the property; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name | [`IObservable<IObservedChange<object, object?>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable of observed changes. |
| `ICreatesObservableForProperty.GetNotificationForProperty(object sender, Expression expression, string propertyName, bool beforeChanged)` | Creates an observable that fires when the specified property changes, without suppressing warnings. | [`ICreatesObservableForProperty`](mechanisms.md) `factory` (receiver); [`object`](https://learn.microsoft.com/dotnet/api/system.object) `sender`: The object to observe; [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The expression identifying the property; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChanged`: Whether to observe before-change events | [`IObservable<IObservedChange<object, object?>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable of observed changes. |

#### `ICreatesObservableForProperty`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface ICreatesObservableForProperty` | Provides property change notifications for the types it supports; the registration with the highest affinity for a type and property is the one used. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObject(Type type, string propertyName, bool beforeChanged)` | Returns how well this implementation can observe a property of a type. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `type`: The type being observed; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name being observed; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChanged`: Whether before-change (PropertyChanging) is requested | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive score when the property can be observed, where a higher score wins; zero or negative when it cannot. |
| `GetNotificationForProperty(object sender, Expression expression, string propertyName, bool beforeChanged, bool suppressWarnings)` | Creates an observable that emits each time the specified property changes. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `sender`: The object to observe; [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The expression identifying the property; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChanged`: Whether to observe before-change events; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `suppressWarnings`: Whether to suppress the warning written for a property that cannot notify | [`IObservable<IObservedChange<object, object?>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable of observed changes; callers read the current value from `sender` rather than from the emitted change. |

#### `PluginObservationSource`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class PluginObservationSource` | Chooses between the observation the generator emitted for one link of a property chain and a registered [`ICreatesObservableForProperty`](mechanisms.md) that outranks the mechanism it was built from. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Choose<T>(object source, Expression expression, string propertyName, bool beforeChange, int generatedAffinity, Func<object, T?> getter, IObservable<T> generated)` | Returns the registration's observation of a property, or the generated one when none outranks it. | `T`: The type of the property value; [`object`](https://learn.microsoft.com/dotnet/api/system.object) `source`: The object the property is read from; [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The property as the generated code names it; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The name of the property being observed; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChange`: Whether before-change notifications are being observed; [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `generatedAffinity`: The affinity of the mechanism the generator picked for this link; [`Func<object, T?>`](https://learn.microsoft.com/dotnet/api/system.func-2) `getter`: Reads the current property value from the source; [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `generated`: The observation the generator emitted for this link | [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): The generated observation when no registration outranks `generatedAffinity`; otherwise a [`PluginPropertyObservable<T>`](mechanisms.md) that does not filter consecutive equal values. |

#### `PluginPropertyObservable<T>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class PluginPropertyObservable<T>` | Observes one property through a registered [`ICreatesObservableForProperty`](mechanisms.md) that outranks the mechanism the generator selected, reading the value through a generated accessor. | `T`: The type of the property value | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `PluginPropertyObservable(ICreatesObservableForProperty plugin, object source, Expression expression, string propertyName, Func<object, T?> getter, bool beforeChange, bool distinctUntilChanged)` | Initializes a new instance of the [`PluginPropertyObservable<T>`](mechanisms.md) class. | [`ICreatesObservableForProperty`](mechanisms.md) `plugin`: The registration that outranked the generated mechanism; [`object`](https://learn.microsoft.com/dotnet/api/system.object) `source`: The object the property is read from; [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The property as the call site named it; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The name of the property being observed; [`Func<object, T?>`](https://learn.microsoft.com/dotnet/api/system.func-2) `getter`: Reads the current property value from the source; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChange`: Whether before-change notifications are being observed; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `distinctUntilChanged`: Whether to suppress duplicate consecutive values | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Subscribe(IObserver<T> observer)` | Notifies the provider that an observer is to receive notifications. | [`IObserver<T>`](https://learn.microsoft.com/dotnet/api/system.iobserver-1) `observer`: The object that is to receive notifications | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them. |

### Command binders

[Full description and examples](mechanisms.md).

Types: `ReactiveUI.Binding.CommandBinding.CommandBinderService`, `ReactiveUI.Binding.CommandBinding.CommandInvoker`, `ReactiveUI.Binding.ICreatesCommandBinding`.

#### `CommandBinderService`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class CommandBinderService` | Service that resolves the best [`ICreatesCommandBinding`](mechanisms.md) for a given control type using affinity scoring. Follows the same resolution pattern as property observation via [`ICreatesObservableForProperty`](mechanisms.md). | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetBinder<T>(bool hasEventTarget)` | Gets the highest-affinity [`ICreatesCommandBinding`](mechanisms.md) registered for the specified control type. | `T`: The type of the control; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `hasEventTarget`: Whether the caller specifies a custom event target | [`ICreatesCommandBinding?`](mechanisms.md): The best binder; the first registered wins a tie. Null when every registered binder returns an affinity of zero or less. |

#### `CommandInvoker`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class CommandInvoker` | Executes a command with each value a sequence produces. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Invoke<T>(IObservable<T> source, IObservable<ICommand?> commands)` | Executes whichever command the observed sequence of commands last produced. | `T`: The type of the value offered as the command parameter; [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source`: The sequence driving the executions; [`IObservable<ICommand?>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `commands`: The command to execute, as the observed property produces it | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, stops executing and stops observing the property. |
| `Invoke<T>(IObservable<T> source, ICommand command)` | Executes one command with each value the sequence produces, skipping values the command refuses. | `T`: The type of the value offered as the command parameter; [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source`: The sequence driving the executions; [`ICommand`](https://learn.microsoft.com/dotnet/api/system.windows.input.icommand) `command`: The command to execute | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, stops executing the command. |

#### `ICreatesCommandBinding`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface ICreatesCommandBinding` | Plugin interface for types that can bind an [`ICommand`](https://learn.microsoft.com/dotnet/api/system.windows.input.icommand) to a control. Implementations register with Splat and are resolved by affinity scoring. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BindCommandToObject<T>(ICommand? command, T? target, IObservable<object?> commandParameter)` | Binds an [`ICommand`](https://learn.microsoft.com/dotnet/api/system.windows.input.icommand) to a UI object using the default event. The default event is determined by the implementation (e.g., Click, TouchUpInside). | `T : class`: The type of the target object; [`ICommand?`](https://learn.microsoft.com/dotnet/api/system.windows.input.icommand) `command`: The command to bind. If `null`, no binding is created; `T?` `target`: The target object, usually a UI control; [`IObservable<object?>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `commandParameter`: An observable that provides the command parameter value | [`IDisposable?`](https://learn.microsoft.com/dotnet/api/system.idisposable): An [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) that disconnects the binding when disposed, or `null` if no binding was created. |
| `BindCommandToObject<T, TEventArgs>(ICommand? command, T? target, IObservable<object?> commandParameter, string eventName)` | Binds an [`ICommand`](https://learn.microsoft.com/dotnet/api/system.windows.input.icommand) to a UI object to a specific named event. | `T : class`: The type of the target object; `TEventArgs`: The event argument type; [`ICommand?`](https://learn.microsoft.com/dotnet/api/system.windows.input.icommand) `command`: The command to bind. If `null`, no binding is created; `T?` `target`: The target object, usually a UI control; [`IObservable<object?>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `commandParameter`: An observable that provides the command parameter value; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `eventName`: The event to bind to | [`IDisposable?`](https://learn.microsoft.com/dotnet/api/system.idisposable): An [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) that disconnects the binding when disposed, or `null` if no binding was created. |
| `BindCommandToObject<T, TEventArgs>(ICommand? command, T? target, IObservable<object?> commandParameter, Action<EventHandler<TEventArgs>> addHandler, Action<EventHandler<TEventArgs>> removeHandler)` | Binds a command to a specific event on a target object using explicit add/remove handler delegates. This overload is fully AOT-compatible as it avoids reflection-based event lookup. | `T : class`: The type of the target object; `TEventArgs : EventArgs`: The event arguments type; [`ICommand?`](https://learn.microsoft.com/dotnet/api/system.windows.input.icommand) `command`: The command to bind. If `null`, no binding is created; `T?` `target`: The target object; [`IObservable<object?>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `commandParameter`: An observable that supplies command parameter values; [`Action<EventHandler<TEventArgs>>`](https://learn.microsoft.com/dotnet/api/system.action-1) `addHandler`: Adds the handler to the target event; [`Action<EventHandler<TEventArgs>>`](https://learn.microsoft.com/dotnet/api/system.action-1) `removeHandler`: Removes the handler from the target event | [`IDisposable?`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that unbinds the command. |
| `GetAffinityForObject<T>(bool hasEventTarget)` | Returns a positive integer when this implementation supports binding a command to an object of the specified type. If the binding is not supported, the method returns a non-positive integer. In cases where multiple implementations return positive values, the one with the highest value wins. | `T`: The type of the control to bind to; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `hasEventTarget`: Whether the caller specifies a custom event target | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive integer if binding is supported, or zero/negative if not. |

### Observables

[Full description and examples](mechanisms.md).

Types: `ReactiveUI.Binding.Observables.AppliedChangeObservable`, `ReactiveUI.Binding.Observables.CombineLatestObservable`, `ReactiveUI.Binding.Observables.EventObservable<T>`, `ReactiveUI.Binding.Observables.NotifyPropertyChangedObservable`, `ReactiveUI.Binding.Observables.PropertyChangingObservable<T>`, `ReactiveUI.Binding.Observables.PropertyObservable<T>`, `ReactiveUI.Binding.Observables.UnchangingPropertyObservable<T>`.

#### `AppliedChangeObservable`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class AppliedChangeObservable` | The changes a two-way binding actually wrote, handed to whoever subscribes to the binding. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `AppliedChangeObservable()` | Initializes a new instance of the AppliedChangeObservable class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `OnNext(BindingChange value)` | Reports a change the binding has written. | [`BindingChange`](bindings.md) `value`: The change that was written | — |
| `Subscribe(IObserver<BindingChange> observer)` | Notifies the provider that an observer is to receive notifications. | [`IObserver<BindingChange>`](https://learn.microsoft.com/dotnet/api/system.iobserver-1) `observer`: The object that is to receive notifications | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `HasObservers { get; }` | Gets a value indicating whether at least one observer is subscribed. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) |

#### `CombineLatestObservable`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class CombineLatestObservable` | Creates an observable that combines the latest value of each of two to sixteen sources. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(IObservable<T1> source1, IObservable<T2> source2, IObservable<T3> source3, IObservable<T4> source4, IObservable<T5> source5, IObservable<T6> source6, IObservable<T7> source7, IObservable<T8> source8, IObservable<T9> source9, IObservable<T10> source10, IObservable<T11> source11, IObservable<T12> source12, IObservable<T13> source13, IObservable<T14> source14, IObservable<T15> source15, IObservable<T16> source16, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> resultSelector)` | Combines the latest values from sixteen observables using a result selector. Available for 2 to 16 repeated arguments; the largest form is listed. | `T1`: Source element type 1; `T2`: Source element type 2; `T3`: Source element type 3; `T4`: Source element type 4; `T5`: Source element type 5; `T6`: Source element type 6; `T7`: Source element type 7; `T8`: Source element type 8; `T9`: Source element type 9; `T10`: Source element type 10; `T11`: Source element type 11; `T12`: Source element type 12; `T13`: Source element type 13; `T14`: Source element type 14; `T15`: Source element type 15; `T16`: Source element type 16; `TResult`: The result element type; [`IObservable<T1>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source1`: Source observable 1; [`IObservable<T2>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source2`: Source observable 2; [`IObservable<T3>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source3`: Source observable 3; [`IObservable<T4>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source4`: Source observable 4; [`IObservable<T5>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source5`: Source observable 5; [`IObservable<T6>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source6`: Source observable 6; [`IObservable<T7>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source7`: Source observable 7; [`IObservable<T8>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source8`: Source observable 8; [`IObservable<T9>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source9`: Source observable 9; [`IObservable<T10>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source10`: Source observable 10; [`IObservable<T11>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source11`: Source observable 11; [`IObservable<T12>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source12`: Source observable 12; [`IObservable<T13>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source13`: Source observable 13; [`IObservable<T14>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source14`: Source observable 14; [`IObservable<T15>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source15`: Source observable 15; [`IObservable<T16>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source16`: Source observable 16; [`Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>`](https://learn.microsoft.com/dotnet/api/system.func-17) `resultSelector`: The function to combine the latest values | [`IObservable<TResult>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits once every source has produced a value, then again on each later value from any source. |

#### `EventObservable<T>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class EventObservable<T>` | Emits a value on subscribe, then again each time an [`EventHandler`](https://learn.microsoft.com/dotnet/api/system.eventhandler) event is raised. | `T`: The type of the property value | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `EventObservable(Action<EventHandler> addHandler, Action<EventHandler> removeHandler, Func<T> getter, bool distinctUntilChanged)` | Initializes a new instance of the [`EventObservable<T>`](mechanisms.md) class. | [`Action<EventHandler>`](https://learn.microsoft.com/dotnet/api/system.action-1) `addHandler`: A delegate that subscribes an [`EventHandler`](https://learn.microsoft.com/dotnet/api/system.eventhandler) to the property change event; [`Action<EventHandler>`](https://learn.microsoft.com/dotnet/api/system.action-1) `removeHandler`: A delegate that unsubscribes an [`EventHandler`](https://learn.microsoft.com/dotnet/api/system.eventhandler) from the property change event; [`Func<T>`](https://learn.microsoft.com/dotnet/api/system.func-1) `getter`: A delegate that reads the current property value; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `distinctUntilChanged`: Whether to suppress duplicate consecutive values | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Subscribe(IObserver<T> observer)` | Notifies the provider that an observer is to receive notifications. | [`IObserver<T>`](https://learn.microsoft.com/dotnet/api/system.iobserver-1) `observer`: The object that is to receive notifications | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them. |

#### `NotifyPropertyChangedObservable`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class NotifyPropertyChangedObservable` | Emits an observed change each time the sender raises a notification for one property. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NotifyPropertyChangedObservable(object sender, Expression expression, string expectedName, bool beforeChanged)` | Initializes a new instance of the [`NotifyPropertyChangedObservable`](mechanisms.md) class. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `sender`: The source object raising the notifications; [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The expression surfaced on the emitted observed change; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `expectedName`: The property name to match, ending in `[]` for an indexer; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChanged`: Whether to observe before-change notifications | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Subscribe(IObserver<IObservedChange<object, object?>> observer)` | Notifies the provider that an observer is to receive notifications. | [`IObserver<IObservedChange<object, object?>>`](https://learn.microsoft.com/dotnet/api/system.iobserver-1) `observer`: The object that is to receive notifications | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them. |

#### `PropertyChangingObservable<T>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class PropertyChangingObservable<T>` | Emits the current value of a property on subscribe, then its value as it stands before each change that an [`INotifyPropertyChanging`](https://learn.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanging) source announces. | `T`: The type of the property value | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `PropertyChangingObservable(INotifyPropertyChanging source, string propertyName, Func<INotifyPropertyChanging, T?> getter)` | Initializes a new instance of the [`PropertyChangingObservable<T>`](mechanisms.md) class. | [`INotifyPropertyChanging`](https://learn.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanging) `source`: The object implementing [`INotifyPropertyChanging`](https://learn.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanging); [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name to observe; [`Func<INotifyPropertyChanging, T?>`](https://learn.microsoft.com/dotnet/api/system.func-2) `getter`: A delegate that reads the property value from the source | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Subscribe(IObserver<T> observer)` | Notifies the provider that an observer is to receive notifications. | [`IObserver<T>`](https://learn.microsoft.com/dotnet/api/system.iobserver-1) `observer`: The object that is to receive notifications | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them. |

#### `PropertyObservable<T>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class PropertyObservable<T>` | Emits a property's value on subscribe, then its new value after each change the source announces. | `T`: The type of the property value | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `PropertyObservable(INotifyPropertyChanged source, string propertyName, Func<INotifyPropertyChanged, T?> getter, bool distinctUntilChanged)` | Initializes a new instance of the [`PropertyObservable<T>`](mechanisms.md) class. | [`INotifyPropertyChanged`](https://learn.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanged) `source`: The object implementing [`INotifyPropertyChanged`](https://learn.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanged); [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name to observe; [`Func<INotifyPropertyChanged, T?>`](https://learn.microsoft.com/dotnet/api/system.func-2) `getter`: A delegate that reads the property value from the source; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `distinctUntilChanged`: Whether to suppress duplicate consecutive values | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Subscribe(IObserver<T> observer)` | Notifies the provider that an observer is to receive notifications. | [`IObserver<T>`](https://learn.microsoft.com/dotnet/api/system.iobserver-1) `observer`: The object that is to receive notifications | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them. |

#### `UnchangingPropertyObservable<T>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class UnchangingPropertyObservable<T>` | Observes a property that has no change notification: its value, once, and then silence. | `T`: The type of the property value | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `UnchangingPropertyObservable(T value)` | Initializes a new instance of the [`UnchangingPropertyObservable<T>`](mechanisms.md) class. | `T` `value`: The property's current value, which is also its final one | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Subscribe(IObserver<T> observer)` | Notifies the provider that an observer is to receive notifications. | [`IObserver<T>`](https://learn.microsoft.com/dotnet/api/system.iobserver-1) `observer`: The object that is to receive notifications | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them. |

## Views

### Views

[Full description and examples](views.md).

Types: `ReactiveUI.Binding.IActivatableView`, `ReactiveUI.Binding.IViewFor`, `ReactiveUI.Binding.IViewFor<T>`.

#### `IActivatableView`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IActivatableView` | Marker interface that every [`IViewFor`](views.md) implements; it declares no members. | None. | — |

#### `IViewFor`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IViewFor` | Non-generic interface for views that display a view model. | None. | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ViewModel { get; set; }` | Gets or sets the view model displayed by the view. | None. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) |

#### `IViewFor<T>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IViewFor<T>` | Generic interface for views that display a specific view model type. | `T : class`: The type of the view model | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ViewModel { get; set; }` | Gets or sets the view model displayed by the view. | None. | `T?` |

### View locators

[Full description and examples](views.md).

Types: `ReactiveUI.Binding.DefaultViewLocator`, `ReactiveUI.Binding.IViewLocator`, `ReactiveUI.Binding.ViewLocator`, `ReactiveUI.Binding.ViewLocatorMixins`, `ReactiveUI.Binding.ViewLocatorNotFoundException`.

#### `DefaultViewLocator`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class DefaultViewLocator` | Default implementation of [`IViewLocator`](views.md) that resolves views for view models using a three-tier resolution strategy: source-generated AOT-safe dispatch, explicit runtime mappings, and service locator fallback. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `DefaultViewLocator()` | Initializes a new instance of the DefaultViewLocator class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ResolveView(object? viewModel, string? contract)` | Resolves a view for the view model using its runtime type. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `viewModel`: The view model instance to resolve a view for; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `contract`: The contract to resolve under, or null for the default view | [`IViewFor?`](views.md): The resolved view, or `null` if no view is found. |
| `ResolveView<TViewModel>(TViewModel viewModel, string? contract)` | Resolves a view for the view model using its compile-time type, without reflection over the view model's runtime type. | `TViewModel : class`: The type of the view model; `TViewModel` `viewModel`: The view model instance to resolve a view for; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `contract`: The contract to resolve under, or null for the default view | [`IViewFor?`](views.md): The resolved view, or `null` if no view is found. |
| `static SetGeneratedViewDispatch(Func<object, string, IViewFor?> dispatch)` | Adds a source-generated view dispatch function. Called by `__ReactiveUIGeneratedBindings`: from a module initializer in a C# 9 or newer project, and from its static constructor in an older one. Each assembly that contains views registers its own. | [`Func<object, string, IViewFor?>`](https://learn.microsoft.com/dotnet/api/system.func-3) `dispatch`: The dispatch function that resolves views by type-switching on the view model instance | — |

#### `IViewLocator`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IViewLocator` | Resolves views for view models. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ResolveView(object? viewModel, string? contract)` | Resolves a view for the view model using its runtime type. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `viewModel`: The view model instance to resolve a view for; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `contract`: The contract to resolve under, or null for the default view | [`IViewFor?`](views.md): The resolved view, or `null` if no view is found. |
| `ResolveView<TViewModel>(TViewModel viewModel, string? contract)` | Resolves a view for the view model using its compile-time type, without reflection over the view model's runtime type. | `TViewModel : class`: The type of the view model; `TViewModel` `viewModel`: The view model instance to resolve a view for; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `contract`: The contract to resolve under, or null for the default view | [`IViewFor?`](views.md): The resolved view, or `null` if no view is found. |

#### `ViewLocator`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class ViewLocator` | Static accessor for the current [`IViewLocator`](views.md) instance. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetCurrent()` | Gets the current [`IViewLocator`](views.md) from the service locator. | None. | [`IViewLocator`](views.md): The current [`IViewLocator`](views.md) instance. |

#### `ViewLocatorMixins`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class ViewLocatorMixins` | Convenience overloads for [`IViewLocator`](views.md) that supply the default (null) contract. Provided as overloads rather than optional parameters so the interface stays free of optional parameters. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `IViewLocator.ResolveView(object? viewModel)` | Resolves a view for the specified view model instance using the default contract. | [`IViewLocator`](views.md) `locator` (receiver); [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `viewModel`: The view model instance to resolve a view for | [`IViewFor?`](views.md): The resolved view, or `null` if no view is found. |
| `IViewLocator.ResolveView<TViewModel>(TViewModel viewModel)` | Resolves a view for the specified view model type using the default contract. | `TViewModel : class`: The type of the view model; [`IViewLocator`](views.md) `locator` (receiver); `TViewModel` `viewModel`: The view model instance to resolve a view for | [`IViewFor?`](views.md): The resolved view, or `null` if no view is found. |

#### `ViewLocatorNotFoundException`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `class ViewLocatorNotFoundException : Exception` | Exception thrown when a view locator is not registered with the dependency resolver. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ViewLocatorNotFoundException()` | Initializes a new instance of the [`ViewLocatorNotFoundException`](views.md) class with a message naming the builder calls that register the default locator. | None. | — |
| `ViewLocatorNotFoundException(string message)` | Initializes a new instance of the [`ViewLocatorNotFoundException`](views.md) class with a specified error message. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `message`: The message that describes the error | — |
| `ViewLocatorNotFoundException(string message, Exception innerException)` | Initializes a new instance of the [`ViewLocatorNotFoundException`](views.md) class with a specified error message and a reference to the inner exception. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `message`: The message that describes the error; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception) `innerException`: The exception that is the cause of the current exception | — |

### View mapping

[Full description and examples](views.md).

Types: `ReactiveUI.Binding.DefaultViewLocator`, `ReactiveUI.Binding.ExcludeFromViewRegistrationAttribute`, `ReactiveUI.Binding.SingleInstanceViewAttribute`, `ReactiveUI.Binding.ViewContractAttribute`, `ReactiveUI.Binding.ViewMappingBuilder`.

#### `DefaultViewLocator`

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `CreateMappingBuilder()` | Creates a new [`ViewMappingBuilder`](views.md) for fluent registration of view-to-view-model mappings. | None. | [`ViewMappingBuilder`](views.md): A new [`ViewMappingBuilder`](views.md) targeting this locator instance. |
| `Map<TViewModel, TView>()` | Registers an explicit view mapping for a view model type. | `TViewModel : class`: The view model type; `TView : IViewFor, new()`: The view type. Must implement [`IViewFor`](views.md) | — |
| `Map<TViewModel>(Func<IViewFor> factory)` | Registers an explicit view mapping with a custom factory for a view model type. | `TViewModel : class`: The view model type; [`Func<IViewFor>`](https://learn.microsoft.com/dotnet/api/system.func-1) `factory`: A factory function that creates the view | — |
| `Map<TViewModel, TView>(string? contract)` | Registers an explicit view mapping for a view model type. | `TViewModel : class`: The view model type; `TView : IViewFor, new()`: The view type. Must implement [`IViewFor`](views.md); [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `contract`: A contract string for named registrations | — |
| `Map<TViewModel>(Func<IViewFor> factory, string? contract)` | Registers an explicit view mapping with a custom factory for a view model type. | `TViewModel : class`: The view model type; [`Func<IViewFor>`](https://learn.microsoft.com/dotnet/api/system.func-1) `factory`: A factory function that creates the view; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `contract`: A contract string for named registrations | — |
| `Unmap<TViewModel>()` | Removes an explicit view mapping for a view model type. | `TViewModel : class`: The view model type | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if the mapping was removed; otherwise, `false`. |
| `Unmap<TViewModel>(string? contract)` | Removes an explicit view mapping for a view model type. | `TViewModel : class`: The view model type; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `contract`: A contract string for named registrations | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if the mapping was removed; otherwise, `false`. |

#### `ExcludeFromViewRegistrationAttribute`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ExcludeFromViewRegistrationAttribute : Attribute` | Leaves a view class out of the view dispatch the source generator builds from [`IViewFor<T>`](views.md) implementations. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ExcludeFromViewRegistrationAttribute()` | Initializes a new instance of the ExcludeFromViewRegistrationAttribute class. | None. | — |

#### `SingleInstanceViewAttribute`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class SingleInstanceViewAttribute : Attribute` | Indicates that this View should be constructed once and then reused every time its ViewModel's View is resolved. The source generator will emit a cached singleton pattern instead of creating a new instance per resolution. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `SingleInstanceViewAttribute()` | Initializes a new instance of the SingleInstanceViewAttribute class. | None. | — |

#### `ViewContractAttribute`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ViewContractAttribute : Attribute` | Registers an [`IViewFor<T>`](views.md) view in the generated view dispatch under a contract string, so one view model can have a different view per contract. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ViewContractAttribute(string contract)` | Registers an [`IViewFor<T>`](views.md) view in the generated view dispatch under a contract string, so one view model can have a different view per contract. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `contract`: The contract value for view resolution | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Contract { get; }` | Gets the contract to use when resolving the view. | None. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) |

#### `ViewMappingBuilder`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ViewMappingBuilder` | Fluent builder for registering view-to-view-model mappings on a [`DefaultViewLocator`](views.md). | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Map<TViewModel, TView>()` | Maps a view model type to a view type constructed with its parameterless constructor, replacing an existing mapping for the same view model type. | `TViewModel : class`: The view model type; `TView : IViewFor, new()`: The view type. Must have a parameterless constructor | [`ViewMappingBuilder`](views.md): This builder for chaining. |
| `Map<TViewModel>(Func<IViewFor> factory)` | Maps a view model type to a view created by a factory, replacing an existing mapping for the same view model type. | `TViewModel : class`: The view model type; [`Func<IViewFor>`](https://learn.microsoft.com/dotnet/api/system.func-1) `factory`: A factory function that creates the view | [`ViewMappingBuilder`](views.md): This builder for chaining. |
| `Map<TViewModel, TView>(string? contract)` | Maps a view model type and contract to a view type constructed with its parameterless constructor, replacing an existing mapping for the same pair. | `TViewModel : class`: The view model type; `TView : IViewFor, new()`: The view type. Must have a parameterless constructor; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `contract`: The contract the mapping is registered under; null registers the default mapping | [`ViewMappingBuilder`](views.md): This builder for chaining. |
| `Map<TViewModel>(Func<IViewFor> factory, string? contract)` | Maps a view model type and contract to a view created by a factory, replacing an existing mapping for the same pair. | `TViewModel : class`: The view model type; [`Func<IViewFor>`](https://learn.microsoft.com/dotnet/api/system.func-1) `factory`: A factory function that creates the view; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `contract`: The contract the mapping is registered under; null registers the default mapping | [`ViewMappingBuilder`](views.md): This builder for chaining. |

## Threading

### Schedulers

[Full description and examples](threading.md).

Types: `ReactiveUI.Binding.BindingSchedulers`.

#### `BindingSchedulers`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class BindingSchedulers` | Where a binding delivers its writes to the view. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ObserveOnSequencer<T>(IObservable<T> source, ISequencer scheduler)` | Routes an observable onto a sequencer, delivering only the latest value that is waiting on it. | `T`: The type of the observed values; [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source`: The observable feeding a write; `ISequencer` `scheduler`: The sequencer every delivery waits on | [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): The source, observed on the sequencer with a newer value replacing one that has not been delivered. |
| `ObserveOnViewThread<T>(IObservable<T> source, object? target)` | Routes an observable onto the thread that owns the object being written to. | `T`: The type of the observed values; [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source`: The observable feeding a write; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `target`: The object the write lands on | [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): The source itself when `target` is null or no registered invoker claims it; otherwise the source routed onto its thread. |
| `ObserveOnViewThread<T>(IObservable<T> source, object? target, IViewThreadInvoker fallback)` | Routes an observable onto the thread that owns the object being written to, falling back to a known invoker. | `T`: The type of the observed values; [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source`: The observable feeding a write; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `target`: The object the write lands on; [`IViewThreadInvoker`](threading.md) `fallback`: The invoker for the object's platform, used when no registered invoker claims it | [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): The source itself when `target` is null; otherwise the source routed onto its thread. |
| `UseSynchronizationContext(SynchronizationContext? context)` | Delivers writes from another thread through a synchronization context. | [`SynchronizationContext?`](https://learn.microsoft.com/dotnet/api/system.threading.synchronizationcontext) `context`: The context owning the view, or null to use the view's own dispatcher | — |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `MainThread { get; set; }` | Gets or sets the sequencer a write from another thread is delivered on, or null to use the view's own dispatcher. | None. | `ISequencer?` |

### View thread invokers

[Full description and examples](threading.md).

Types: `ReactiveUI.Binding.IViewThreadInvoker`, `ReactiveUI.Binding.ViewThreadInvokers`.

#### `IViewThreadInvoker`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IViewThreadInvoker` | Runs a binding's write on the thread that owns the object it lands on, through the platform's own dispatcher. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `CheckAccess(object target)` | Determines whether the calling thread may write to `target` directly. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `target`: An object this invoker claims | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` on the owning thread, or when the object has no owning thread; otherwise `false`. |
| `Claims(object target)` | Determines whether this invoker handles `target`. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `target`: The object a binding writes to | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` when the object belongs to this invoker's platform; otherwise `false`. |
| `Post(object target, Action<object?> callback, object? state)` | Queues a callback on the thread that owns `target`. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `target`: An object this invoker claims; [`Action<object?>`](https://learn.microsoft.com/dotnet/api/system.action-1) `callback`: The callback to run; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `state`: The value handed to `callback` | — |

#### `ViewThreadInvokers`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class ViewThreadInvokers` | Finds the registered invoker for the object a binding writes to. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ForTarget(object? target)` | Finds the first registered invoker that claims `target`. | [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `target`: The object a binding is about to write to | [`IViewThreadInvoker?`](threading.md): The invoker, or null when `target` is null or nothing claims it. |
| `Refresh()` | Re-reads the registered invokers, for a host that registers one after its first binding. | None. | — |

## Setup

### Builder

[Full description and examples](setup.md).

Types: `ReactiveUI.Binding.Builder.IReactiveUIBindingBuilder`, `ReactiveUI.Binding.Builder.IReactiveUIBindingInstance`, `ReactiveUI.Binding.Builder.ReactiveUIBindingBuilder`, `ReactiveUI.Binding.Builder.ReactiveUIBindingModule`, `ReactiveUI.Binding.Builder.RxBindingBuilder`, `ReactiveUI.Binding.Builder.RxBindingBuilderMixins`, `ReactiveUI.Binding.Mixins.BuilderMixins`.

#### `IReactiveUIBindingBuilder`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IReactiveUIBindingBuilder` | Fluent builder that configures ReactiveUI.Binding services, converters, and platform modules before building an application instance. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BuildApp()` | Builds the application, publishes the converter service and marks ReactiveUI.Binding initialized. | None. | [`IReactiveUIBindingInstance`](setup.md): The configured application instance. |
| `ConfigureViewLocator(Action<ViewMappingBuilder> configure)` | Registers a default view locator holding the explicit view-to-view-model mappings. | [`Action<ViewMappingBuilder>`](https://learn.microsoft.com/dotnet/api/system.action-1) `configure`: An action that receives a [`ViewMappingBuilder`](views.md) for registering mappings | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `WithCommandBinder(ICreatesCommandBinding binder)` | Registers a custom command binder for binding [`ICommand`](https://learn.microsoft.com/dotnet/api/system.windows.input.icommand) instances to UI controls. | [`ICreatesCommandBinding`](mechanisms.md) `binder`: The command binder instance to register | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `WithConverter(IBindingTypeConverter converter)` | Registers a typed binding converter. | [`IBindingTypeConverter`](converters.md) `converter`: The converter instance to register | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `WithCoreServices()` | Registers the core ReactiveUI.Binding services (INPC/POCO observation, default converters, default view locator). Hides `WithCoreServices` to return [`IReactiveUIBindingBuilder`](setup.md) for fluent chaining. | None. | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `WithFallbackConverter(IBindingFallbackConverter converter)` | Registers a fallback binding converter. | [`IBindingFallbackConverter`](converters.md) `converter`: The fallback converter instance to register | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `WithPlatformModule<T>(T module)` | Registers a platform-specific module with the builder. | `T : IModule`: The type of the platform module. Must implement `IModule`; `T` `module`: The platform module instance to register | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `WithRegistration(Action<IMutableDependencyResolver> configureAction)` | Runs a registration action against the mutable dependency resolver. | [`Action<IMutableDependencyResolver>`](https://learn.microsoft.com/dotnet/api/system.action-1) `configureAction`: An action that receives the mutable dependency resolver | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `WithSetMethodConverter(ISetMethodBindingConverter converter)` | Registers a set-method binding converter. | [`ISetMethodBindingConverter`](converters.md) `converter`: The set-method converter instance to register | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |

#### `IReactiveUIBindingInstance`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `interface IReactiveUIBindingInstance` | Represents a configured ReactiveUI.Binding application instance. | None. | — |

#### `ReactiveUIBindingBuilder`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ReactiveUIBindingBuilder : AppBuilder` | Configures ReactiveUI.Binding services, converters and platform modules on a Splat `AppBuilder`. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ReactiveUIBindingBuilder(IMutableDependencyResolver resolver, IReadonlyDependencyResolver? current)` | Initializes a new instance of the [`ReactiveUIBindingBuilder`](setup.md) class, initializing Splat on the resolver and registering `ConverterService` with it. | `IMutableDependencyResolver` `resolver`: The dependency resolver to configure; `IReadonlyDependencyResolver?` `current`: The resolver that reads the configured services; may be null | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BuildApp()` | Builds the application, publishes `ConverterService` through [`BindingConverters`](converters.md) and marks ReactiveUI.Binding initialized. | None. | [`IReactiveUIBindingInstance`](setup.md): The configured application instance. |
| `ConfigureViewLocator(Action<ViewMappingBuilder> configure)` | Creates a [`DefaultViewLocator`](views.md) holding the explicit mappings and registers it as the [`IViewLocator`](views.md). | [`Action<ViewMappingBuilder>`](https://learn.microsoft.com/dotnet/api/system.action-1) `configure`: An action that receives a [`ViewMappingBuilder`](views.md) for registering mappings | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `WithCommandBinder(ICreatesCommandBinding binder)` | Registers a custom command binder for binding commands to UI controls. | [`ICreatesCommandBinding`](mechanisms.md) `binder`: The command binder instance to register | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `WithConverter(IBindingTypeConverter converter)` | Registers a typed binding converter. | [`IBindingTypeConverter`](converters.md) `converter`: The converter instance to register | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `override WithCoreServices()` | Registers the default converters, the INPC and POCO observation services and the default view locator; calling it again registers nothing more. | None. | `IAppBuilder`: The builder instance for chaining. |
| `WithFallbackConverter(IBindingFallbackConverter converter)` | Registers a fallback binding converter. | [`IBindingFallbackConverter`](converters.md) `converter`: The fallback converter instance to register | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `WithPlatformModule<T>(T module)` | Registers a platform-specific module with the builder. | `T : IModule`: The type of the platform module. Must implement `IModule`; `T` `module`: The platform module instance to register | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `WithRegistration(Action<IMutableDependencyResolver> configureAction)` | Runs a registration action against the mutable dependency resolver immediately. | [`Action<IMutableDependencyResolver>`](https://learn.microsoft.com/dotnet/api/system.action-1) `configureAction`: An action that receives the mutable dependency resolver | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `WithSetMethodConverter(ISetMethodBindingConverter converter)` | Registers a set-method binding converter. | [`ISetMethodBindingConverter`](converters.md) `converter`: The set-method converter instance to register | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |

**Properties**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ConverterService { get; }` | Gets the converter service used for binding type conversions. | None. | [`ConverterService`](converters.md) |

#### `ReactiveUIBindingModule`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ReactiveUIBindingModule` | Registers the [`ICreatesObservableForProperty`](mechanisms.md) implementations for INotifyPropertyChanged and plain object properties. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ReactiveUIBindingModule()` | Initializes a new instance of the ReactiveUIBindingModule class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Configure(IMutableDependencyResolver resolver)` | Registers the INPC and POCO property observation services with the resolver. | `IMutableDependencyResolver` `resolver`: The dependency resolver to configure | — |

#### `RxBindingBuilder`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class RxBindingBuilder` | Static factory for creating [`ReactiveUIBindingBuilder`](setup.md) instances. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `CreateReactiveUIBindingBuilder()` | Creates a new [`ReactiveUIBindingBuilder`](setup.md) using the current Splat locator. | None. | [`ReactiveUIBindingBuilder`](setup.md): A new builder instance. |
| `EnsureInitialized()` | Returns when a builder's `BuildApp()` has completed, and throws otherwise. | None. | — |

#### `RxBindingBuilderMixins`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class RxBindingBuilderMixins` | Resolver-scoped entry points for creating [`ReactiveUIBindingBuilder`](setup.md) instances. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `IMutableDependencyResolver.CreateReactiveUIBindingBuilder()` | Creates a new [`ReactiveUIBindingBuilder`](setup.md) that registers into the specified resolver. | `IMutableDependencyResolver` `resolver` (receiver) | [`ReactiveUIBindingBuilder`](setup.md): A new builder instance that reads services from the resolver when it is also an `IReadonlyDependencyResolver`, and from the current locator otherwise. |

#### `BuilderMixins`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class BuilderMixins` | Extension methods that bridge `IAppBuilder` to [`IReactiveUIBindingBuilder`](setup.md) for fluent chaining. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `IAppBuilder.BuildApp()` | Builds the ReactiveUI.Binding application from an `IAppBuilder`. | `IAppBuilder` `appBuilder` (receiver): The app builder instance | [`IReactiveUIBindingInstance`](setup.md): The configured application instance. |
| `IAppBuilder.ConfigureViewLocator(Action<ViewMappingBuilder> configure)` | Configures the default view locator with explicit view-to-view-model mappings from an `IAppBuilder`. | `IAppBuilder` `appBuilder` (receiver): The app builder instance; [`Action<ViewMappingBuilder>`](https://learn.microsoft.com/dotnet/api/system.action-1) `configure`: An action that receives a [`ViewMappingBuilder`](views.md) for registering mappings | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `IAppBuilder.WithConverter(IBindingTypeConverter converter)` | Registers a typed binding converter from an `IAppBuilder`. | `IAppBuilder` `appBuilder` (receiver): The app builder instance; [`IBindingTypeConverter`](converters.md) `converter`: The converter instance to register | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `IAppBuilder.WithFallbackConverter(IBindingFallbackConverter converter)` | Registers a fallback binding converter from an `IAppBuilder`. | `IAppBuilder` `appBuilder` (receiver): The app builder instance; [`IBindingFallbackConverter`](converters.md) `converter`: The fallback converter instance to register | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `IAppBuilder.WithPlatformModule<T>(T module)` | Registers a platform-specific module from an `IAppBuilder`. | `T : IModule`: The type of the platform module; `IAppBuilder` `appBuilder` (receiver): The app builder instance; `T` `module`: The platform module instance to register | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `IAppBuilder.WithRegistration(Action<IMutableDependencyResolver> configureAction)` | Adds a custom registration action from an `IAppBuilder`. | `IAppBuilder` `appBuilder` (receiver): The app builder instance; [`Action<IMutableDependencyResolver>`](https://learn.microsoft.com/dotnet/api/system.action-1) `configureAction`: An action that receives the mutable dependency resolver | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `IAppBuilder.WithSetMethodConverter(ISetMethodBindingConverter converter)` | Registers a set-method binding converter from an `IAppBuilder`. | `IAppBuilder` `appBuilder` (receiver): The app builder instance; [`ISetMethodBindingConverter`](converters.md) `converter`: The set-method converter instance to register | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |

## Unsafe and runtime

### Unsafe overloads

[Full description and examples](unsafe.md).

Types: `ReactiveUI.Binding.ReactiveSchedulerExtensions`, `ReactiveUI.Binding.ReactiveUIBindingExtensions`.

#### `ReactiveSchedulerExtensions`

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BindOneWayUnsafe<TSource, TTarget, TProperty>(TSource source, TTarget target, Expression<Func<TSource, TProperty>> sourceProperty, Expression<Func<TTarget, TProperty>> targetProperty, ISequencer? scheduler)` | Creates a one-way binding from a source property to a target property with a specified scheduler, resolving the property chains by reflection. | `TSource : class`: The type of the source; `TTarget : class`: The type of the target object; `TProperty`: The type of the property being bound; `TSource` `source`: The source the binding is rooted on; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; `ISequencer?` `scheduler`: The scheduler the write to the target is delivered on, or null for its owning thread; an immediate scheduler writes inline | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindOneWayUnsafe<TSource, TSourceProp, TTarget, TTargetProp>(TSource source, TTarget target, Expression<Func<TSource, TSourceProp>> sourceProperty, Expression<Func<TTarget, TTargetProp>> targetProperty, Func<TSourceProp, TTargetProp> conversionFunc, ISequencer? scheduler)` | Creates a one-way binding from a source property to a target property with a conversion function and a specified scheduler, resolving the property chains by reflection. | `TSource : class`: The type of the source; `TSourceProp`: The type of the source property; `TTarget : class`: The type of the target object; `TTargetProp`: The type of the target property; `TSource` `source`: The source the binding is rooted on; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TSourceProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TTargetProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; [`Func<TSourceProp, TTargetProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `conversionFunc`: A function that converts the source property value to the target property type; `ISequencer?` `scheduler`: The scheduler the write to the target is delivered on, or null for its owning thread; an immediate scheduler writes inline | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindOneWayUnsafe<TSource, TSourceProp, TTarget, TTargetProp>(TSource source, TTarget target, Expression<Func<TSource, TSourceProp>> sourceProperty, Expression<Func<TTarget, TTargetProp>> targetProperty, IBindingTypeConverter converter, ISequencer? scheduler, object? conversionHint)` | Creates a one-way binding from a source property to a target property using an explicit [`IBindingTypeConverter`](converters.md), resolving the property chains by reflection. | `TSource : class`: The type of the source; `TSourceProp`: The type of the source property; `TTarget : class`: The type of the target object; `TTargetProp`: The type of the target property; `TSource` `source`: The source the binding is rooted on; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TSourceProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TTargetProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; [`IBindingTypeConverter`](converters.md) `converter`: The binding type converter to use for converting between source and target types; `ISequencer?` `scheduler`: The scheduler the write to the target is delivered on, or null for its owning thread; an immediate scheduler writes inline; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: An optional hint passed to the converter (e.g., format string) | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindTwoWayUnsafe<TSource, TTarget, TProperty>(TSource source, TTarget target, Expression<Func<TSource, TProperty>> sourceProperty, Expression<Func<TTarget, TProperty>> targetProperty, ISequencer? scheduler)` | Creates a two-way binding between a source property and a target property with a specified scheduler, resolving the property chains by reflection. | `TSource : class`: The type of the source; `TTarget : class`: The type of the target object; `TProperty`: The type of the property being bound; `TSource` `source`: The source the binding is rooted on; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; `ISequencer?` `scheduler`: The scheduler the write to the target is delivered on, or null for its owning thread; an immediate scheduler writes inline. Writes back are not scheduled | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindTwoWayUnsafe<TSource, TSourceProp, TTarget, TTargetProp>(TSource source, TTarget target, Expression<Func<TSource, TSourceProp>> sourceProperty, Expression<Func<TTarget, TTargetProp>> targetProperty, Func<TSourceProp, TTargetProp> sourceToTargetConv, Func<TTargetProp, TSourceProp> targetToSourceConv, ISequencer? scheduler)` | Creates a two-way binding between a source property and a target property with conversion functions and a specified scheduler, resolving the property chains by reflection. | `TSource : class`: The type of the source; `TSourceProp`: The type of the source property; `TTarget : class`: The type of the target object; `TTargetProp`: The type of the target property; `TSource` `source`: The source the binding is rooted on; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TSourceProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TTargetProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; [`Func<TSourceProp, TTargetProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `sourceToTargetConv`: A function that converts the source property value to the target property type; [`Func<TTargetProp, TSourceProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `targetToSourceConv`: A function that converts the target property value back to the source property type; `ISequencer?` `scheduler`: The scheduler the write to the target is delivered on, or null for its owning thread; an immediate scheduler writes inline. Writes back are not scheduled | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindTwoWayUnsafe<TSource, TSourceProp, TTarget, TTargetProp>(TSource source, TTarget target, Expression<Func<TSource, TSourceProp>> sourceProperty, Expression<Func<TTarget, TTargetProp>> targetProperty, IBindingTypeConverter sourceToTargetConverter, IBindingTypeConverter targetToSourceConverter, ISequencer? scheduler, object? conversionHint)` | Creates a two-way binding between a source property and a target property using explicit [`IBindingTypeConverter`](converters.md) instances, resolving the property chains by reflection. | `TSource : class`: The type of the source; `TSourceProp`: The type of the source property; `TTarget : class`: The type of the target object; `TTargetProp`: The type of the target property; `TSource` `source`: The source the binding is rooted on; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TSourceProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TTargetProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; [`IBindingTypeConverter`](converters.md) `sourceToTargetConverter`: The converter for source-to-target conversion; [`IBindingTypeConverter`](converters.md) `targetToSourceConverter`: The converter for target-to-source conversion; `ISequencer?` `scheduler`: The scheduler the write to the target is delivered on, or null for its owning thread; an immediate scheduler writes inline. Writes back are not scheduled; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: An optional hint passed to the converters (e.g., format string) | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindUnsafe<TView, TViewModel, TVMProp, TVProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty, Func<TVMProp, TVProp> viewModelToViewConverter, Func<TVProp, TVMProp> viewToViewModelConverter, ISequencer? scheduler)` | Creates a two-way binding between a view model property and a view property with conversion functions and a specified scheduler, resolving the property chains by reflection. | `TView : class, IViewFor`: The type of the view; `TViewModel : class`: The type of the view model; `TVMProp`: The type of the view model property; `TVProp`: The type of the view property; `TView` `view`: The view the binding is rooted on; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update; [`Func<TVMProp, TVProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `viewModelToViewConverter`: A function that converts the view model property value to the view property type; [`Func<TVProp, TVMProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `viewToViewModelConverter`: A function that converts the view property value back to the view model property type; `ISequencer?` `scheduler`: The scheduler the write to the view is delivered on, or null for its owning thread; an immediate scheduler writes inline. Writes back are not scheduled | [`IReactiveBinding<TView, BindingChange>`](bindings.md): A reactive binding that can be disposed to disconnect the binding. |
| `BindUnsafe<TView, TViewModel, TVMProp, TVProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty, IBindingTypeConverter viewModelToViewConverter, IBindingTypeConverter viewToViewModelConverter, ISequencer? scheduler, object? conversionHint)` | Creates a two-way binding between a view model property and a view property using explicit [`IBindingTypeConverter`](converters.md) instances, resolving the property chains by reflection. | `TView : class, IViewFor`: The type of the view; `TViewModel : class`: The type of the view model; `TVMProp`: The type of the view model property; `TVProp`: The type of the view property; `TView` `view`: The view the binding is rooted on; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update; [`IBindingTypeConverter`](converters.md) `viewModelToViewConverter`: The converter for view model-to-view conversion; [`IBindingTypeConverter`](converters.md) `viewToViewModelConverter`: The converter for view-to-view model conversion; `ISequencer?` `scheduler`: The scheduler the write to the view is delivered on, or null for its owning thread; an immediate scheduler writes inline. Writes back are not scheduled; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: An optional hint passed to the converters (e.g., format string) | [`IReactiveBinding<TView, BindingChange>`](bindings.md): A reactive binding that can be disposed to disconnect the binding. |
| `OneWayBindUnsafe<TView, TViewModel, TProp, TOut>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TProp>> viewModelProperty, Expression<Func<TView, TOut>> viewProperty, Func<TProp, TOut> selector, ISequencer? scheduler)` | Creates a one-way binding from a view model property to a view property with a specified selector and scheduler, resolving the property chains by reflection. | `TView : class, IViewFor`: The type of the view; `TViewModel : class`: The type of the view model; `TProp`: The type of the view model property; `TOut`: The type of the view property; `TView` `view`: The view the binding is rooted on; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TOut>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update; [`Func<TProp, TOut>`](https://learn.microsoft.com/dotnet/api/system.func-2) `selector`: A function that converts the view model property value to the view property type; `ISequencer?` `scheduler`: The scheduler the write to the view is delivered on, or null for its owning thread; an immediate scheduler writes inline | [`IReactiveBinding<TView, TOut>`](bindings.md): A reactive binding that can be disposed to disconnect the binding. |
| `OneWayBindUnsafe<TView, TViewModel, TVMProp, TVProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty, IBindingTypeConverter converter, ISequencer? scheduler, object? conversionHint)` | Creates a one-way binding from a view model property to a view property using an explicit [`IBindingTypeConverter`](converters.md), resolving the property chains by reflection. | `TView : class, IViewFor`: The type of the view; `TViewModel : class`: The type of the view model; `TVMProp`: The type of the view model property; `TVProp`: The type of the view property; `TView` `view`: The view the binding is rooted on; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update; [`IBindingTypeConverter`](converters.md) `converter`: The binding type converter to use for converting between source and target types; `ISequencer?` `scheduler`: The scheduler the write to the view is delivered on, or null for its owning thread; an immediate scheduler writes inline; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: An optional hint passed to the converter (e.g., format string) | [`IReactiveBinding<TView, TVProp>`](bindings.md): A reactive binding that can be disposed to disconnect the binding. |

#### `ReactiveUIBindingExtensions`

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BindCommandUnsafe<TView, TViewModel, TProp, TControl>(TView view, TViewModel? viewModel, Expression<Func<TViewModel, TProp?>> propertyName, Expression<Func<TView, TControl>> controlName, string? toEvent)` | Binds the command a view model property holds to the control a view property holds, rebinding when either changes. | `TView : class, IViewFor`: The type of the view; `TViewModel : class`: The type of the view model; `TProp : ICommand`: The type of the command property; `TControl : class`: The type of the control; `TView` `view`: The view to bind to; `TViewModel?` `viewModel`: The view model containing the command; null binds nothing; [`Expression<Func<TViewModel, TProp?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `propertyName`: An expression that selects the command property on the view model; [`Expression<Func<TView, TControl>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `controlName`: An expression that selects the control on the view; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `toEvent`: The event name to bind to. If null, a default event is selected | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindCommandUnsafe<TView, TViewModel, TProp, TControl, TParam>(TView view, TViewModel? viewModel, Expression<Func<TViewModel, TProp?>> propertyName, Expression<Func<TView, TControl>> controlName, IObservable<TParam?> withParameter, string? toEvent)` | Binds the command a view model property holds to the control a view property holds, using an observable as the source of the command parameter. | `TView : class, IViewFor`: The type of the view; `TViewModel : class`: The type of the view model; `TProp : ICommand`: The type of the command property; `TControl : class`: The type of the control; `TParam`: The type of the command parameter; `TView` `view`: The view to bind to; `TViewModel?` `viewModel`: The view model containing the command; null binds nothing; [`Expression<Func<TViewModel, TProp?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `propertyName`: An expression that selects the command property on the view model; [`Expression<Func<TView, TControl>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `controlName`: An expression that selects the control on the view; [`IObservable<TParam?>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `withParameter`: An observable that provides the command parameter; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `toEvent`: The event name to bind to. If null, a default event is selected | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindCommandUnsafe<TView, TViewModel, TProp, TControl, TParam>(TView view, TViewModel? viewModel, Expression<Func<TViewModel, TProp?>> propertyName, Expression<Func<TView, TControl>> controlName, Expression<Func<TViewModel, TParam?>> withParameter, string? toEvent)` | Binds the command a view model property holds to the control a view property holds, using a view model property as the command parameter. | `TView : class, IViewFor`: The type of the view; `TViewModel : class`: The type of the view model; `TProp : ICommand`: The type of the command property; `TControl : class`: The type of the control; `TParam`: The type of the command parameter; `TView` `view`: The view to bind to; `TViewModel?` `viewModel`: The view model containing the command; null binds nothing; [`Expression<Func<TViewModel, TProp?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `propertyName`: An expression that selects the command property on the view model; [`Expression<Func<TView, TControl>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `controlName`: An expression that selects the control on the view; [`Expression<Func<TViewModel, TParam?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `withParameter`: An expression that selects the command parameter property on the view model; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `toEvent`: The event name to bind to. If null, a default event is selected | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindInteractionUnsafe<TViewModel, TView, TInput, TOutput>(TView view, TViewModel? viewModel, Expression<Func<TViewModel, IInteraction<TInput, TOutput>>> propertyName, Func<IInteractionContext<TInput, TOutput>, Task> handler)` | Registers a task-based handler on the interaction a view model property holds, moving it to the new interaction when the property changes. | `TViewModel : class`: The type of the view model; `TView : class, IViewFor`: The type of the view; `TInput`: The type of the interaction's input; `TOutput`: The type of the interaction's output; `TView` `view`: The view that provides the handler; `TViewModel?` `viewModel`: The view model that exposes the interaction; null registers nothing; [`Expression<Func<TViewModel, IInteraction<TInput, TOutput>>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `propertyName`: An expression that selects the interaction property on the view model; [`Func<IInteractionContext<TInput, TOutput>, Task>`](https://learn.microsoft.com/dotnet/api/system.func-2) `handler`: A task-based handler for the interaction | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, unregisters the handler and stops observing the property. |
| `BindInteractionUnsafe<TViewModel, TView, TInput, TOutput, TDontCare>(TView view, TViewModel? viewModel, Expression<Func<TViewModel, IInteraction<TInput, TOutput>>> propertyName, Func<IInteractionContext<TInput, TOutput>, IObservable<TDontCare>> handler)` | Registers an observable-based handler on the interaction a view model property holds, moving it to the new interaction when the property changes. | `TViewModel : class`: The type of the view model; `TView : class, IViewFor`: The type of the view; `TInput`: The type of the interaction's input; `TOutput`: The type of the interaction's output; `TDontCare`: The signal type of the observable handler; `TView` `view`: The view that provides the handler; `TViewModel?` `viewModel`: The view model that exposes the interaction; null registers nothing; [`Expression<Func<TViewModel, IInteraction<TInput, TOutput>>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `propertyName`: An expression that selects the interaction property on the view model; [`Func<IInteractionContext<TInput, TOutput>, IObservable<TDontCare>>`](https://learn.microsoft.com/dotnet/api/system.func-2) `handler`: An observable-based handler for the interaction | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, unregisters the handler and stops observing the property. |
| `BindOneWayUnsafe<TSource, TTarget, TProperty>(TSource source, TTarget target, Expression<Func<TSource, TProperty>> sourceProperty, Expression<Func<TTarget, TProperty>> targetProperty)` | Binds a source property to a target property one way, writing the current value and each later change on the target's owning thread. | `TSource : class`: The type of the source object; `TTarget : class`: The type of the target object; `TProperty`: The type of the property being bound; `TSource` `source`: The source object to observe for property changes; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindOneWayUnsafe<TSource, TSourceProp, TTarget, TTargetProp>(TSource source, TTarget target, Expression<Func<TSource, TSourceProp>> sourceProperty, Expression<Func<TTarget, TTargetProp>> targetProperty, Func<TSourceProp, TTargetProp> conversionFunc)` | Binds a source property to a target property one way through a conversion function, writing on the target's owning thread. | `TSource : class`: The type of the source object; `TSourceProp`: The type of the source property; `TTarget : class`: The type of the target object; `TTargetProp`: The type of the target property; `TSource` `source`: The source object to observe for property changes; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TSourceProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TTargetProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; [`Func<TSourceProp, TTargetProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `conversionFunc`: A function that converts the source property value to the target property type | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `IObservable<TValue>.BindToUnsafe<TValue, TTarget, TTargetValue>(TTarget? target, Expression<Func<TTarget, TTargetValue?>> property)` | Writes each value the source produces to a target property, resolving the property chain by reflection. | `TValue`: The type of the value produced by the source observable; `TTarget : class`: The type of the target object; `TTargetValue`: The type of the property on the target object; [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source` (receiver): The observable stream to bind to a target property; `TTarget?` `target`: The target object whose property will be set; [`Expression<Func<TTarget, TTargetValue?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property`: An expression that selects the target property to set | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `IObservable<TValue>.BindToUnsafe<TValue, TTarget, TTargetValue>(TTarget? target, Expression<Func<TTarget, TTargetValue?>> property, IBindingTypeConverter? converterOverride)` | Writes each value the source produces to a target property, converting it with the supplied converter and resolving the property chain by reflection. | `TValue`: The type of the value produced by the source observable; `TTarget : class`: The type of the target object; `TTargetValue`: The type of the property on the target object; [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source` (receiver): The observable stream to bind to a target property; `TTarget?` `target`: The target object whose property will be set; [`Expression<Func<TTarget, TTargetValue?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property`: An expression that selects the target property to set; [`IBindingTypeConverter?`](converters.md) `converterOverride`: An explicit converter to use when converting the source value to the target property type | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `IObservable<TValue>.BindToUnsafe<TValue, TTarget, TTargetValue>(TTarget? target, Expression<Func<TTarget, TTargetValue?>> property, object? conversionHint)` | Writes each value the source produces to a target property, passing the conversion hint to the converter and resolving the property chain by reflection. | `TValue`: The type of the value produced by the source observable; `TTarget : class`: The type of the target object; `TTargetValue`: The type of the property on the target object; [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source` (receiver): The observable stream to bind to a target property; `TTarget?` `target`: The target object whose property will be set; [`Expression<Func<TTarget, TTargetValue?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property`: An expression that selects the target property to set; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: An object that provides a hint to the converter. The semantics are defined by the converter | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `IObservable<TValue>.BindToUnsafe<TValue, TTarget, TTargetValue>(TTarget? target, Expression<Func<TTarget, TTargetValue?>> property, object? conversionHint, IBindingTypeConverter? converterOverride)` | Writes each value the source produces to a target property, converting it with the supplied converter and conversion hint and resolving the property chain by reflection. | `TValue`: The type of the value produced by the source observable; `TTarget : class`: The type of the target object; `TTargetValue`: The type of the property on the target object; [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source` (receiver): The observable stream to bind to a target property; `TTarget?` `target`: The target object whose property will be set; [`Expression<Func<TTarget, TTargetValue?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property`: An expression that selects the target property to set; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: An object that provides a hint to the converter. The semantics are defined by the converter; [`IBindingTypeConverter?`](converters.md) `converterOverride`: An explicit converter to use when converting the source value to the target property type | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindTwoWayUnsafe<TSource, TTarget, TProperty>(TSource source, TTarget target, Expression<Func<TSource, TProperty>> sourceProperty, Expression<Func<TTarget, TProperty>> targetProperty)` | Binds a source and a target property to each other, seeding the target from the source and mirroring each change on the other side's owning thread. | `TSource : class`: The type of the source object; `TTarget : class`: The type of the target object; `TProperty`: The type of the property being bound; `TSource` `source`: The source object to observe for property changes; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TProperty>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindTwoWayUnsafe<TSource, TSourceProp, TTarget, TTargetProp>(TSource source, TTarget target, Expression<Func<TSource, TSourceProp>> sourceProperty, Expression<Func<TTarget, TTargetProp>> targetProperty, Func<TSourceProp, TTargetProp> sourceToTargetConv, Func<TTargetProp, TSourceProp> targetToSourceConv)` | Binds a source and a target property to each other through conversion functions, seeding the target and mirroring each change on the other side's owning thread. | `TSource : class`: The type of the source object; `TSourceProp`: The type of the source property; `TTarget : class`: The type of the target object; `TTargetProp`: The type of the target property; `TSource` `source`: The source object to observe for property changes; `TTarget` `target`: The target object whose property will be updated; [`Expression<Func<TSource, TSourceProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: An expression that selects the source property to observe; [`Expression<Func<TTarget, TTargetProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: An expression that selects the target property to update; [`Func<TSourceProp, TTargetProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `sourceToTargetConv`: A function that converts the source property value to the target property type; [`Func<TTargetProp, TSourceProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `targetToSourceConv`: A function that converts the target property value back to the source property type | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, disconnects the binding. |
| `BindUnsafe<TViewModel, TView, TVMProp, TVProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty)` | Creates a two-way binding between a view model property and a view property, resolving the property chains by reflection. | `TViewModel : class`: The type of the view model; `TView : class, IViewFor`: The type of the view; `TVMProp`: The type of the view model property; `TVProp`: The type of the view property; `TView` `view`: The view to bind to; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update | [`IReactiveBinding<TView, BindingChange>`](bindings.md): The binding; its change stream reports each value with the side it came from, and disposing it disconnects both directions. |
| `BindUnsafe<TViewModel, TView, TVMProp, TVProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty, Func<TVMProp, TVProp> viewModelToViewConverter, Func<TVProp, TVMProp> viewToViewModelConverter)` | Creates a two-way binding between a view model property and a view property with conversion functions, resolving the property chains by reflection. | `TViewModel : class`: The type of the view model; `TView : class, IViewFor`: The type of the view; `TVMProp`: The type of the view model property; `TVProp`: The type of the view property; `TView` `view`: The view to bind to; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update; [`Func<TVMProp, TVProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `viewModelToViewConverter`: A function that converts the view model property value to the view property type; [`Func<TVProp, TVMProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `viewToViewModelConverter`: A function that converts the view property value back to the view model property type | [`IReactiveBinding<TView, BindingChange>`](bindings.md): The binding; its change stream reports each value with the side it came from, and disposing it disconnects both directions. |
| `BindUnsafe<TViewModel, TView, TVMProp, TVProp, TDontCare>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty, IObservable<TDontCare>? signalViewUpdate, TriggerUpdate triggerUpdate = ViewToViewModel)` | Binds a view model property and a view property in both directions, with an update stream driving one direction. | `TViewModel : class`: The view model type; `TView : class, IViewFor`: The view type; `TVMProp`: The view model property type; `TVProp`: The view property type; `TDontCare`: The ignored signal payload; `TView` `view`: The view to bind; `TViewModel` `viewModel`: The view model to bind; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: The view model property path; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: The view property path; [`IObservable<TDontCare>?`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `signalViewUpdate`: The update stream, or null to observe both properties' own notifications; [`TriggerUpdate`](unsafe.md) `triggerUpdate`: The direction the update stream drives; see [`TriggerUpdate`](unsafe.md) | [`IReactiveBinding<TView, BindingChange>`](bindings.md): The binding; its change stream reports each write with the side it came from, and disposing it disconnects both directions. |
| `BindUnsafe<TViewModel, TView, TVMProp, TVProp, TDontCare>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty, Func<TVMProp, TVProp> viewModelToViewConverter, Func<TVProp, TVMProp> viewToViewModelConverter, IObservable<TDontCare>? signalViewUpdate, TriggerUpdate triggerUpdate = ViewToViewModel)` | Binds a view model property and a view property in both directions with explicit conversions, with an update stream driving one direction. | `TViewModel : class`: The view model type; `TView : class, IViewFor`: The view type; `TVMProp`: The view model property type; `TVProp`: The view property type; `TDontCare`: The ignored signal payload; `TView` `view`: The view to bind; `TViewModel` `viewModel`: The view model to bind; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: The view model property path; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: The view property path; [`Func<TVMProp, TVProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `viewModelToViewConverter`: Converts a value written to the view; [`Func<TVProp, TVMProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `viewToViewModelConverter`: Converts a value written to the view model; [`IObservable<TDontCare>?`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `signalViewUpdate`: The update stream, or null to observe both properties' own notifications; [`TriggerUpdate`](unsafe.md) `triggerUpdate`: The direction the update stream drives; see [`TriggerUpdate`](unsafe.md) | [`IReactiveBinding<TView, BindingChange>`](bindings.md): The binding; its change stream reports each write with the side it came from, and disposing it disconnects both directions. |
| `IObservable<T>.InvokeCommandUnsafe<T, TTarget>(TTarget? target, Expression<Func<TTarget, ICommand?>> commandProperty)` | Executes the command a property holds with each value as its parameter, skipping a value while there is no command or the command cannot execute it. | `T`: The type of the value offered as the command parameter; `TTarget : class`: The type declaring the command property; [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source` (receiver): The sequence driving the executions; `TTarget?` `target`: The object declaring the command property; null executes nothing; [`Expression<Func<TTarget, ICommand?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `commandProperty`: An expression that selects the command property to execute | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, stops executing the command and stops observing the property. |
| `OneWayBindUnsafe<TViewModel, TView, TVMProp, TVProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty)` | Binds a view model property to a view property one way, writing the current value and each later change on the view's owning thread. | `TViewModel : class`: The type of the view model; `TView : class, IViewFor`: The type of the view; `TVMProp`: The type of the view model property; `TVProp`: The type of the view property; `TView` `view`: The view to bind to; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update | [`IReactiveBinding<TView, TVProp>`](bindings.md): A reactive binding that can be disposed to disconnect the binding. |
| `OneWayBindUnsafe<TViewModel, TView, TProp, TOut>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TProp>> viewModelProperty, Expression<Func<TView, TOut>> viewProperty, Func<TProp, TOut> selector)` | Binds a view model property to a view property one way through a selector, writing on the view's owning thread. | `TViewModel : class`: The type of the view model; `TView : class, IViewFor`: The type of the view; `TProp`: The type of the view model property; `TOut`: The type of the view property; `TView` `view`: The view to bind to; `TViewModel` `viewModel`: The view model to observe; [`Expression<Func<TViewModel, TProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: An expression that selects the view model property to observe; [`Expression<Func<TView, TOut>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: An expression that selects the view property to update; [`Func<TProp, TOut>`](https://learn.microsoft.com/dotnet/api/system.func-2) `selector`: A function that converts the view model property value to the view property type | [`IReactiveBinding<TView, TOut>`](bindings.md): A reactive binding that can be disposed to disconnect the binding. |
| `ToPropertyUnsafe<TObj, TRet>(this IObservable<TRet> target, TObj source, Expression<Func<TObj, TRet>> property, ...)` | Backs a read-only property with the observable's latest value, finding the property and its raise member by reflection. | `TObj : class`. `property` is `x => x.Property` or, in the string overloads, any property name. Optional: `TRet initialValue` or `Func<TRet> getInitialValue`, `bool deferSubscription`, `ISequencer? scheduler`, `out ObservableAsPropertyHelper<TRet> result`. | `ObservableAsPropertyHelper<TRet>`. Throws `InvalidOperationException` when the type raises no notification reflection can reach. |
| `WhenAnyObservableUnsafe<TSender, TRet>(TSender sender, Expression<Func<TSender, IObservable<TRet>?>> obs1, Expression<Func<TSender, IObservable<TRet>?>> obs2, Expression<Func<TSender, IObservable<TRet>?>> obs3, Expression<Func<TSender, IObservable<TRet>?>> obs4, Expression<Func<TSender, IObservable<TRet>?>> obs5, Expression<Func<TSender, IObservable<TRet>?>> obs6, Expression<Func<TSender, IObservable<TRet>?>> obs7, Expression<Func<TSender, IObservable<TRet>?>> obs8, Expression<Func<TSender, IObservable<TRet>?>> obs9, Expression<Func<TSender, IObservable<TRet>?>> obs10, Expression<Func<TSender, IObservable<TRet>?>> obs11, Expression<Func<TSender, IObservable<TRet>?>> obs12)` | Observes 12 observable-valued properties by reflection and merges the values of their current observables, each switching when its property changes. Available for 1 to 12 repeated arguments; the largest form is listed. | `TSender : class`: The type of the sender to monitor for property changes; `TRet`: The element type of the observed observables; `TSender` `sender`: The sender instance to observe for property changes; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs1`: An expression that selects observable property 1 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs2`: An expression that selects observable property 2 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs3`: An expression that selects observable property 3 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs4`: An expression that selects observable property 4 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs5`: An expression that selects observable property 5 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs6`: An expression that selects observable property 6 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs7`: An expression that selects observable property 7 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs8`: An expression that selects observable property 8 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs9`: An expression that selects observable property 9 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs10`: An expression that selects observable property 10 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs11`: An expression that selects observable property 11 to observe; [`Expression<Func<TSender, IObservable<TRet>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs12`: An expression that selects observable property 12 to observe | [`IObservable<TRet>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable sequence that emits values from the merged observed observables. |
| `WhenAnyObservableUnsafe<TSender, TRet, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(TSender sender, Expression<Func<TSender, IObservable<T1>?>> obs1, Expression<Func<TSender, IObservable<T2>?>> obs2, Expression<Func<TSender, IObservable<T3>?>> obs3, Expression<Func<TSender, IObservable<T4>?>> obs4, Expression<Func<TSender, IObservable<T5>?>> obs5, Expression<Func<TSender, IObservable<T6>?>> obs6, Expression<Func<TSender, IObservable<T7>?>> obs7, Expression<Func<TSender, IObservable<T8>?>> obs8, Expression<Func<TSender, IObservable<T9>?>> obs9, Expression<Func<TSender, IObservable<T10>?>> obs10, Expression<Func<TSender, IObservable<T11>?>> obs11, Expression<Func<TSender, IObservable<T12>?>> obs12, Func<T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, T9?, T10?, T11?, T12?, TRet> selector)` | Observes 12 observable-valued properties of different element types by reflection and applies a selector to their latest values once every observable has produced one. Available for 2 to 12 repeated arguments; the largest form is listed. | `TSender : class`: The type of the sender to monitor for property changes; `TRet`: The return type of the selector; `T1`: The element type of observable property 1; `T2`: The element type of observable property 2; `T3`: The element type of observable property 3; `T4`: The element type of observable property 4; `T5`: The element type of observable property 5; `T6`: The element type of observable property 6; `T7`: The element type of observable property 7; `T8`: The element type of observable property 8; `T9`: The element type of observable property 9; `T10`: The element type of observable property 10; `T11`: The element type of observable property 11; `T12`: The element type of observable property 12; `TSender` `sender`: The sender instance to observe for property changes; [`Expression<Func<TSender, IObservable<T1>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs1`: An expression that selects observable property 1 to observe; [`Expression<Func<TSender, IObservable<T2>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs2`: An expression that selects observable property 2 to observe; [`Expression<Func<TSender, IObservable<T3>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs3`: An expression that selects observable property 3 to observe; [`Expression<Func<TSender, IObservable<T4>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs4`: An expression that selects observable property 4 to observe; [`Expression<Func<TSender, IObservable<T5>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs5`: An expression that selects observable property 5 to observe; [`Expression<Func<TSender, IObservable<T6>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs6`: An expression that selects observable property 6 to observe; [`Expression<Func<TSender, IObservable<T7>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs7`: An expression that selects observable property 7 to observe; [`Expression<Func<TSender, IObservable<T8>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs8`: An expression that selects observable property 8 to observe; [`Expression<Func<TSender, IObservable<T9>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs9`: An expression that selects observable property 9 to observe; [`Expression<Func<TSender, IObservable<T10>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs10`: An expression that selects observable property 10 to observe; [`Expression<Func<TSender, IObservable<T11>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs11`: An expression that selects observable property 11 to observe; [`Expression<Func<TSender, IObservable<T12>?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `obs12`: An expression that selects observable property 12 to observe; [`Func<T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, T9?, T10?, T11?, T12?, TRet>`](https://learn.microsoft.com/dotnet/api/system.func-13) `selector`: A function that combines the latest values from all observables | [`IObservable<TRet>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable sequence of selector results. |
| `WhenAnyUnsafe<TSender, TRet, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(TSender sender, Expression<Func<TSender, T1>> property1, Expression<Func<TSender, T2>> property2, Expression<Func<TSender, T3>> property3, Expression<Func<TSender, T4>> property4, Expression<Func<TSender, T5>> property5, Expression<Func<TSender, T6>> property6, Expression<Func<TSender, T7>> property7, Expression<Func<TSender, T8>> property8, Expression<Func<TSender, T9>> property9, Expression<Func<TSender, T10>> property10, Expression<Func<TSender, T11>> property11, Expression<Func<TSender, T12>> property12, Func<IObservedChange<TSender, T1>, IObservedChange<TSender, T2>, IObservedChange<TSender, T3>, IObservedChange<TSender, T4>, IObservedChange<TSender, T5>, IObservedChange<TSender, T6>, IObservedChange<TSender, T7>, IObservedChange<TSender, T8>, IObservedChange<TSender, T9>, IObservedChange<TSender, T10>, IObservedChange<TSender, T11>, IObservedChange<TSender, T12>, TRet> selector)` | Observes 12 properties by reflection and applies a selector to their observed changes, first on subscription and again after any of them changes. Available for 1 to 12 repeated arguments; the largest form is listed. | `TSender : class`: The type of the sender to monitor for property changes; `TRet`: The return type of the selector; `T1`: The type of property 1 value; `T2`: The type of property 2 value; `T3`: The type of property 3 value; `T4`: The type of property 4 value; `T5`: The type of property 5 value; `T6`: The type of property 6 value; `T7`: The type of property 7 value; `T8`: The type of property 8 value; `T9`: The type of property 9 value; `T10`: The type of property 10 value; `T11`: The type of property 11 value; `T12`: The type of property 12 value; `TSender` `sender`: The sender instance to observe for property changes; [`Expression<Func<TSender, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects property 1 to observe; [`Expression<Func<TSender, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: An expression that selects property 2 to observe; [`Expression<Func<TSender, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: An expression that selects property 3 to observe; [`Expression<Func<TSender, T4>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property4`: An expression that selects property 4 to observe; [`Expression<Func<TSender, T5>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property5`: An expression that selects property 5 to observe; [`Expression<Func<TSender, T6>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property6`: An expression that selects property 6 to observe; [`Expression<Func<TSender, T7>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property7`: An expression that selects property 7 to observe; [`Expression<Func<TSender, T8>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property8`: An expression that selects property 8 to observe; [`Expression<Func<TSender, T9>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property9`: An expression that selects property 9 to observe; [`Expression<Func<TSender, T10>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property10`: An expression that selects property 10 to observe; [`Expression<Func<TSender, T11>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property11`: An expression that selects property 11 to observe; [`Expression<Func<TSender, T12>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property12`: An expression that selects property 12 to observe; [`Func<IObservedChange<TSender, T1>, IObservedChange<TSender, T2>, IObservedChange<TSender, T3>, IObservedChange<TSender, T4>, IObservedChange<TSender, T5>, IObservedChange<TSender, T6>, IObservedChange<TSender, T7>, IObservedChange<TSender, T8>, IObservedChange<TSender, T9>, IObservedChange<TSender, T10>, IObservedChange<TSender, T11>, IObservedChange<TSender, T12>, TRet>`](https://learn.microsoft.com/dotnet/api/system.func-13) `selector`: A function that combines the observed changes into a result | [`IObservable<TRet>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable sequence of selector results. |
| `WhenAnyValueUnsafe<TSender, T1>(TSender sender, Expression<Func<TSender, T1>> property1)` | Observes a property by reflection, emitting its current value on subscription and its new value after each change. This is a ReactiveUI compatibility shim. | `TSender : class`: The type of the sender to monitor for property changes; `T1`: The type of the first observed property value; `TSender` `sender`: The sender instance to observe for property changes; [`Expression<Func<TSender, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe | [`IObservable<T1>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable sequence that emits the property value when it changes. |
| `WhenAnyValueUnsafe<TSender, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TSender sender, Expression<Func<TSender, T1>> property1, Expression<Func<TSender, T2>> property2, Expression<Func<TSender, T3>> property3, Expression<Func<TSender, T4>> property4, Expression<Func<TSender, T5>> property5, Expression<Func<TSender, T6>> property6, Expression<Func<TSender, T7>> property7, Expression<Func<TSender, T8>> property8, Expression<Func<TSender, T9>> property9, Expression<Func<TSender, T10>> property10, Expression<Func<TSender, T11>> property11, Expression<Func<TSender, T12>> property12, Expression<Func<TSender, T13>> property13, Expression<Func<TSender, T14>> property14, Expression<Func<TSender, T15>> property15, Expression<Func<TSender, T16>> property16)` | Observes 16 properties by reflection and emits their values as a tuple, first on subscription and again after any of them changes. This is a ReactiveUI compatibility shim. Available for 2 to 16 repeated arguments; the largest form is listed. | `TSender : class`: The type of the sender to monitor for property changes; `T1`: The type of the first observed property value; `T2`: The type of the second observed property value; `T3`: The type of the third observed property value; `T4`: The type of the fourth observed property value; `T5`: The type of the fifth observed property value; `T6`: The type of the sixth observed property value; `T7`: The type of the seventh observed property value; `T8`: The type of the eighth observed property value; `T9`: The type of the ninth observed property value; `T10`: The type of the tenth observed property value; `T11`: The type of the eleventh observed property value; `T12`: The type of the twelfth observed property value; `T13`: The type of the thirteenth observed property value; `T14`: The type of the fourteenth observed property value; `T15`: The type of the fifteenth observed property value; `T16`: The type of the sixteenth observed property value; `TSender` `sender`: The sender instance to observe for property changes; [`Expression<Func<TSender, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`Expression<Func<TSender, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: An expression that selects the second property to observe; [`Expression<Func<TSender, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: An expression that selects the third property to observe; [`Expression<Func<TSender, T4>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property4`: An expression that selects the fourth property to observe; [`Expression<Func<TSender, T5>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property5`: An expression that selects the fifth property to observe; [`Expression<Func<TSender, T6>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property6`: An expression that selects the sixth property to observe; [`Expression<Func<TSender, T7>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property7`: An expression that selects the seventh property to observe; [`Expression<Func<TSender, T8>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property8`: An expression that selects the eighth property to observe; [`Expression<Func<TSender, T9>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property9`: An expression that selects the ninth property to observe; [`Expression<Func<TSender, T10>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property10`: An expression that selects the tenth property to observe; [`Expression<Func<TSender, T11>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property11`: An expression that selects the eleventh property to observe; [`Expression<Func<TSender, T12>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property12`: An expression that selects the twelfth property to observe; [`Expression<Func<TSender, T13>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property13`: An expression that selects the thirteenth property to observe; [`Expression<Func<TSender, T14>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property14`: An expression that selects the fourteenth property to observe; [`Expression<Func<TSender, T15>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property15`: An expression that selects the fifteenth property to observe; [`Expression<Func<TSender, T16>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property16`: An expression that selects the sixteenth property to observe | [`IObservable<PropertyValues<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable sequence that emits a tuple of all observed property values when any of them changes. |
| `WhenAnyValueUnsafe<TSender, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TRet>(TSender sender, Expression<Func<TSender, T1>> property1, Expression<Func<TSender, T2>> property2, Expression<Func<TSender, T3>> property3, Expression<Func<TSender, T4>> property4, Expression<Func<TSender, T5>> property5, Expression<Func<TSender, T6>> property6, Expression<Func<TSender, T7>> property7, Expression<Func<TSender, T8>> property8, Expression<Func<TSender, T9>> property9, Expression<Func<TSender, T10>> property10, Expression<Func<TSender, T11>> property11, Expression<Func<TSender, T12>> property12, Expression<Func<TSender, T13>> property13, Expression<Func<TSender, T14>> property14, Expression<Func<TSender, T15>> property15, Expression<Func<TSender, T16>> property16, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TRet> selector)` | Observes 16 properties by reflection and emits the selector's result for their values, first on subscription and again after any of them changes. This is a ReactiveUI compatibility shim. Available for 1 to 16 repeated arguments; the largest form is listed. | `TSender : class`: The type of the sender to monitor for property changes; `T1`: The type of the first observed property value; `T2`: The type of the second observed property value; `T3`: The type of the third observed property value; `T4`: The type of the fourth observed property value; `T5`: The type of the fifth observed property value; `T6`: The type of the sixth observed property value; `T7`: The type of the seventh observed property value; `T8`: The type of the eighth observed property value; `T9`: The type of the ninth observed property value; `T10`: The type of the tenth observed property value; `T11`: The type of the eleventh observed property value; `T12`: The type of the twelfth observed property value; `T13`: The type of the thirteenth observed property value; `T14`: The type of the fourteenth observed property value; `T15`: The type of the fifteenth observed property value; `T16`: The type of the sixteenth observed property value; `TRet`: The return type of the selector function; `TSender` `sender`: The sender instance to observe for property changes; [`Expression<Func<TSender, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`Expression<Func<TSender, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: An expression that selects the second property to observe; [`Expression<Func<TSender, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: An expression that selects the third property to observe; [`Expression<Func<TSender, T4>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property4`: An expression that selects the fourth property to observe; [`Expression<Func<TSender, T5>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property5`: An expression that selects the fifth property to observe; [`Expression<Func<TSender, T6>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property6`: An expression that selects the sixth property to observe; [`Expression<Func<TSender, T7>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property7`: An expression that selects the seventh property to observe; [`Expression<Func<TSender, T8>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property8`: An expression that selects the eighth property to observe; [`Expression<Func<TSender, T9>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property9`: An expression that selects the ninth property to observe; [`Expression<Func<TSender, T10>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property10`: An expression that selects the tenth property to observe; [`Expression<Func<TSender, T11>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property11`: An expression that selects the eleventh property to observe; [`Expression<Func<TSender, T12>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property12`: An expression that selects the twelfth property to observe; [`Expression<Func<TSender, T13>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property13`: An expression that selects the thirteenth property to observe; [`Expression<Func<TSender, T14>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property14`: An expression that selects the fourteenth property to observe; [`Expression<Func<TSender, T15>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property15`: An expression that selects the fifteenth property to observe; [`Expression<Func<TSender, T16>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property16`: An expression that selects the sixteenth property to observe; [`Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TRet>`](https://learn.microsoft.com/dotnet/api/system.func-17) `selector`: A function that converts the observed property values to the return type | [`IObservable<TRet>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable sequence that emits the selector result when any of the observed properties changes. |
| `WhenChangedUnsafe<TObj, T1>(TObj objectToMonitor, Expression<Func<TObj, T1>> property1)` | Observes a property by reflection, emitting its current value on subscription and its new value after each change. | `TObj : class`: The type of the object to monitor for property changes; `T1`: The type of the first observed property value; `TObj` `objectToMonitor`: The object instance to observe for property changes; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe | [`IObservable<T1>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable sequence that emits the property value when it changes. |
| `WhenChangedUnsafe<TObj, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TObj objectToMonitor, Expression<Func<TObj, T1>> property1, Expression<Func<TObj, T2>> property2, Expression<Func<TObj, T3>> property3, Expression<Func<TObj, T4>> property4, Expression<Func<TObj, T5>> property5, Expression<Func<TObj, T6>> property6, Expression<Func<TObj, T7>> property7, Expression<Func<TObj, T8>> property8, Expression<Func<TObj, T9>> property9, Expression<Func<TObj, T10>> property10, Expression<Func<TObj, T11>> property11, Expression<Func<TObj, T12>> property12, Expression<Func<TObj, T13>> property13, Expression<Func<TObj, T14>> property14, Expression<Func<TObj, T15>> property15, Expression<Func<TObj, T16>> property16)` | Observes 16 properties by reflection and emits their values as a tuple, first on subscription and again after any of them changes. Available for 2 to 16 repeated arguments; the largest form is listed. | `TObj : class`: The type of the object to monitor for property changes; `T1`: The type of the first observed property value; `T2`: The type of the second observed property value; `T3`: The type of the third observed property value; `T4`: The type of the fourth observed property value; `T5`: The type of the fifth observed property value; `T6`: The type of the sixth observed property value; `T7`: The type of the seventh observed property value; `T8`: The type of the eighth observed property value; `T9`: The type of the ninth observed property value; `T10`: The type of the tenth observed property value; `T11`: The type of the eleventh observed property value; `T12`: The type of the twelfth observed property value; `T13`: The type of the thirteenth observed property value; `T14`: The type of the fourteenth observed property value; `T15`: The type of the fifteenth observed property value; `T16`: The type of the sixteenth observed property value; `TObj` `objectToMonitor`: The object instance to observe for property changes; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`Expression<Func<TObj, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: An expression that selects the second property to observe; [`Expression<Func<TObj, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: An expression that selects the third property to observe; [`Expression<Func<TObj, T4>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property4`: An expression that selects the fourth property to observe; [`Expression<Func<TObj, T5>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property5`: An expression that selects the fifth property to observe; [`Expression<Func<TObj, T6>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property6`: An expression that selects the sixth property to observe; [`Expression<Func<TObj, T7>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property7`: An expression that selects the seventh property to observe; [`Expression<Func<TObj, T8>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property8`: An expression that selects the eighth property to observe; [`Expression<Func<TObj, T9>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property9`: An expression that selects the ninth property to observe; [`Expression<Func<TObj, T10>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property10`: An expression that selects the tenth property to observe; [`Expression<Func<TObj, T11>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property11`: An expression that selects the eleventh property to observe; [`Expression<Func<TObj, T12>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property12`: An expression that selects the twelfth property to observe; [`Expression<Func<TObj, T13>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property13`: An expression that selects the thirteenth property to observe; [`Expression<Func<TObj, T14>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property14`: An expression that selects the fourteenth property to observe; [`Expression<Func<TObj, T15>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property15`: An expression that selects the fifteenth property to observe; [`Expression<Func<TObj, T16>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property16`: An expression that selects the sixteenth property to observe | [`IObservable<PropertyValues<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable sequence that emits a tuple of all observed property values when any of them changes. |
| `WhenChangedUnsafe<TObj, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>(TObj objectToMonitor, Expression<Func<TObj, T1>> property1, Expression<Func<TObj, T2>> property2, Expression<Func<TObj, T3>> property3, Expression<Func<TObj, T4>> property4, Expression<Func<TObj, T5>> property5, Expression<Func<TObj, T6>> property6, Expression<Func<TObj, T7>> property7, Expression<Func<TObj, T8>> property8, Expression<Func<TObj, T9>> property9, Expression<Func<TObj, T10>> property10, Expression<Func<TObj, T11>> property11, Expression<Func<TObj, T12>> property12, Expression<Func<TObj, T13>> property13, Expression<Func<TObj, T14>> property14, Expression<Func<TObj, T15>> property15, Expression<Func<TObj, T16>> property16, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> conversionFunc)` | Observes 16 properties by reflection and emits the conversion function's result for their values, first on subscription and again after any of them changes. Available for 2 to 16 repeated arguments; the largest form is listed. | `TObj : class`: The type of the object to monitor for property changes; `T1`: The type of the first observed property value; `T2`: The type of the second observed property value; `T3`: The type of the third observed property value; `T4`: The type of the fourth observed property value; `T5`: The type of the fifth observed property value; `T6`: The type of the sixth observed property value; `T7`: The type of the seventh observed property value; `T8`: The type of the eighth observed property value; `T9`: The type of the ninth observed property value; `T10`: The type of the tenth observed property value; `T11`: The type of the eleventh observed property value; `T12`: The type of the twelfth observed property value; `T13`: The type of the thirteenth observed property value; `T14`: The type of the fourteenth observed property value; `T15`: The type of the fifteenth observed property value; `T16`: The type of the sixteenth observed property value; `TReturn`: The return type of the conversion function; `TObj` `objectToMonitor`: The object instance to observe for property changes; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`Expression<Func<TObj, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: An expression that selects the second property to observe; [`Expression<Func<TObj, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: An expression that selects the third property to observe; [`Expression<Func<TObj, T4>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property4`: An expression that selects the fourth property to observe; [`Expression<Func<TObj, T5>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property5`: An expression that selects the fifth property to observe; [`Expression<Func<TObj, T6>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property6`: An expression that selects the sixth property to observe; [`Expression<Func<TObj, T7>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property7`: An expression that selects the seventh property to observe; [`Expression<Func<TObj, T8>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property8`: An expression that selects the eighth property to observe; [`Expression<Func<TObj, T9>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property9`: An expression that selects the ninth property to observe; [`Expression<Func<TObj, T10>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property10`: An expression that selects the tenth property to observe; [`Expression<Func<TObj, T11>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property11`: An expression that selects the eleventh property to observe; [`Expression<Func<TObj, T12>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property12`: An expression that selects the twelfth property to observe; [`Expression<Func<TObj, T13>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property13`: An expression that selects the thirteenth property to observe; [`Expression<Func<TObj, T14>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property14`: An expression that selects the fourteenth property to observe; [`Expression<Func<TObj, T15>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property15`: An expression that selects the fifteenth property to observe; [`Expression<Func<TObj, T16>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property16`: An expression that selects the sixteenth property to observe; [`Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>`](https://learn.microsoft.com/dotnet/api/system.func-17) `conversionFunc`: A function that converts the observed property values to the return type | [`IObservable<TReturn>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable sequence that emits the converted result when any of the observed properties changes. |
| `WhenChangingUnsafe<TObj, T1>(TObj objectToMonitor, Expression<Func<TObj, T1>> property1)` | Observes a property by reflection, emitting its current value on subscription and its value before each change. | `TObj : class`: The type of the object to monitor for property changes; `T1`: The type of the first observed property value; `TObj` `objectToMonitor`: The object instance to observe for property changes; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe | [`IObservable<T1>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable sequence that emits the property value before it changes. |
| `WhenChangingUnsafe<TObj, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TObj objectToMonitor, Expression<Func<TObj, T1>> property1, Expression<Func<TObj, T2>> property2, Expression<Func<TObj, T3>> property3, Expression<Func<TObj, T4>> property4, Expression<Func<TObj, T5>> property5, Expression<Func<TObj, T6>> property6, Expression<Func<TObj, T7>> property7, Expression<Func<TObj, T8>> property8, Expression<Func<TObj, T9>> property9, Expression<Func<TObj, T10>> property10, Expression<Func<TObj, T11>> property11, Expression<Func<TObj, T12>> property12, Expression<Func<TObj, T13>> property13, Expression<Func<TObj, T14>> property14, Expression<Func<TObj, T15>> property15, Expression<Func<TObj, T16>> property16)` | Observes 16 properties by reflection and emits their values as a tuple, first on subscription and again before any of them changes. Available for 2 to 16 repeated arguments; the largest form is listed. | `TObj : class`: The type of the object to monitor for property changes; `T1`: The type of the first observed property value; `T2`: The type of the second observed property value; `T3`: The type of the third observed property value; `T4`: The type of the fourth observed property value; `T5`: The type of the fifth observed property value; `T6`: The type of the sixth observed property value; `T7`: The type of the seventh observed property value; `T8`: The type of the eighth observed property value; `T9`: The type of the ninth observed property value; `T10`: The type of the tenth observed property value; `T11`: The type of the eleventh observed property value; `T12`: The type of the twelfth observed property value; `T13`: The type of the thirteenth observed property value; `T14`: The type of the fourteenth observed property value; `T15`: The type of the fifteenth observed property value; `T16`: The type of the sixteenth observed property value; `TObj` `objectToMonitor`: The object instance to observe for property changes; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`Expression<Func<TObj, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: An expression that selects the second property to observe; [`Expression<Func<TObj, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: An expression that selects the third property to observe; [`Expression<Func<TObj, T4>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property4`: An expression that selects the fourth property to observe; [`Expression<Func<TObj, T5>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property5`: An expression that selects the fifth property to observe; [`Expression<Func<TObj, T6>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property6`: An expression that selects the sixth property to observe; [`Expression<Func<TObj, T7>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property7`: An expression that selects the seventh property to observe; [`Expression<Func<TObj, T8>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property8`: An expression that selects the eighth property to observe; [`Expression<Func<TObj, T9>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property9`: An expression that selects the ninth property to observe; [`Expression<Func<TObj, T10>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property10`: An expression that selects the tenth property to observe; [`Expression<Func<TObj, T11>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property11`: An expression that selects the eleventh property to observe; [`Expression<Func<TObj, T12>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property12`: An expression that selects the twelfth property to observe; [`Expression<Func<TObj, T13>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property13`: An expression that selects the thirteenth property to observe; [`Expression<Func<TObj, T14>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property14`: An expression that selects the fourteenth property to observe; [`Expression<Func<TObj, T15>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property15`: An expression that selects the fifteenth property to observe; [`Expression<Func<TObj, T16>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property16`: An expression that selects the sixteenth property to observe | [`IObservable<PropertyValues<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable sequence that emits a tuple of all observed property values before any of them changes. |
| `WhenChangingUnsafe<TObj, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>(TObj objectToMonitor, Expression<Func<TObj, T1>> property1, Expression<Func<TObj, T2>> property2, Expression<Func<TObj, T3>> property3, Expression<Func<TObj, T4>> property4, Expression<Func<TObj, T5>> property5, Expression<Func<TObj, T6>> property6, Expression<Func<TObj, T7>> property7, Expression<Func<TObj, T8>> property8, Expression<Func<TObj, T9>> property9, Expression<Func<TObj, T10>> property10, Expression<Func<TObj, T11>> property11, Expression<Func<TObj, T12>> property12, Expression<Func<TObj, T13>> property13, Expression<Func<TObj, T14>> property14, Expression<Func<TObj, T15>> property15, Expression<Func<TObj, T16>> property16, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> conversionFunc)` | Observes 16 properties by reflection and emits the conversion function's result for their values, first on subscription and again before any of them changes. Available for 2 to 16 repeated arguments; the largest form is listed. | `TObj : class`: The type of the object to monitor for property changes; `T1`: The type of the first observed property value; `T2`: The type of the second observed property value; `T3`: The type of the third observed property value; `T4`: The type of the fourth observed property value; `T5`: The type of the fifth observed property value; `T6`: The type of the sixth observed property value; `T7`: The type of the seventh observed property value; `T8`: The type of the eighth observed property value; `T9`: The type of the ninth observed property value; `T10`: The type of the tenth observed property value; `T11`: The type of the eleventh observed property value; `T12`: The type of the twelfth observed property value; `T13`: The type of the thirteenth observed property value; `T14`: The type of the fourteenth observed property value; `T15`: The type of the fifteenth observed property value; `T16`: The type of the sixteenth observed property value; `TReturn`: The return type of the conversion function; `TObj` `objectToMonitor`: The object instance to observe for property changes; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: An expression that selects the first property to observe; [`Expression<Func<TObj, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: An expression that selects the second property to observe; [`Expression<Func<TObj, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: An expression that selects the third property to observe; [`Expression<Func<TObj, T4>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property4`: An expression that selects the fourth property to observe; [`Expression<Func<TObj, T5>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property5`: An expression that selects the fifth property to observe; [`Expression<Func<TObj, T6>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property6`: An expression that selects the sixth property to observe; [`Expression<Func<TObj, T7>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property7`: An expression that selects the seventh property to observe; [`Expression<Func<TObj, T8>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property8`: An expression that selects the eighth property to observe; [`Expression<Func<TObj, T9>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property9`: An expression that selects the ninth property to observe; [`Expression<Func<TObj, T10>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property10`: An expression that selects the tenth property to observe; [`Expression<Func<TObj, T11>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property11`: An expression that selects the eleventh property to observe; [`Expression<Func<TObj, T12>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property12`: An expression that selects the twelfth property to observe; [`Expression<Func<TObj, T13>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property13`: An expression that selects the thirteenth property to observe; [`Expression<Func<TObj, T14>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property14`: An expression that selects the fourteenth property to observe; [`Expression<Func<TObj, T15>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property15`: An expression that selects the fifteenth property to observe; [`Expression<Func<TObj, T16>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property16`: An expression that selects the sixteenth property to observe; [`Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn>`](https://learn.microsoft.com/dotnet/api/system.func-17) `conversionFunc`: A function that converts the observed property values to the return type | [`IObservable<TReturn>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable sequence that emits the converted result before any of the observed properties changes. |

### Trigger updates

[Full description and examples](unsafe.md).

Types: `ReactiveUI.Binding.TriggerUpdate`.

#### `TriggerUpdate`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `enum TriggerUpdate` | Selects the direction driven by a binding's supplied update stream. | None. | — |

**Enum values**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ViewToViewModel = 0` | The stream replaces view notifications and requests writes to the view model. | None. | — |
| `ViewModelToView = 1` | The stream requests writes to the view in place of view model notifications after the first; view notifications are still observed. | None. | — |

### Runtime fallback

[Full description and examples](unsafe.md).

Types: `ReactiveUI.Binding.Fallback.RuntimeBindingConverter`, `ReactiveUI.Binding.Fallback.RuntimeBindingFallback`, `ReactiveUI.Binding.Fallback.RuntimeCommandBindingFallback`, `ReactiveUI.Binding.Fallback.RuntimeCommandFallback`, `ReactiveUI.Binding.Fallback.RuntimeInteractionFallback`, `ReactiveUI.Binding.Fallback.RuntimeObservationFallback`.

#### `RuntimeBindingConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class RuntimeBindingConverter` | Runtime conversion entry point used by generated `BindTo` bindings to coerce a source value to the target property type when the two differ, or when an explicit converter or conversion hint is supplied. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `TryConvert<TFrom, TTo>(TFrom value, object? conversionHint, IBindingTypeConverter? converterOverride, out TTo result)` | Attempts to convert `value` from `TFrom` to `TTo` for a generated `BindTo` assignment. | `TFrom`: The declared source value type; `TTo`: The target property type; `TFrom` `value`: The value produced by the source observable; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: An optional conversion hint forwarded to the converter; [`IBindingTypeConverter?`](converters.md) `converterOverride`: An optional explicit converter that takes precedence over the registry; out `TTo` `result`: The converted value when conversion succeeds | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise `false`. |

#### `RuntimeBindingFallback`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class RuntimeBindingFallback` | Drives a binding through the runtime observation engine instead of the generated one. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Bind<TViewModel, TView, TProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TProp>> viewModelProperty, Expression<Func<TView, TProp>> viewProperty, ISequencer? scheduler, string bindingExpression)` | Binds a view-model property and a view property of the same type to each other. | `TViewModel : class`: The type declaring the view-model property; `TView : class, IViewFor`: The type declaring the view property; `TProp`: The type of the value carried across the binding; `TView` `view`: The view side of the binding; `TViewModel` `viewModel`: The view-model side of the binding; [`Expression<Func<TViewModel, TProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: The property on `viewModel`; [`Expression<Func<TView, TProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: The property on `view`; `ISequencer?` `scheduler`: The sequencer the view write is delivered on, or null to write on the thread that owns it; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `bindingExpression`: The bound expression, named when a write faults | [`IReactiveBinding<TView, BindingChange>`](bindings.md): The binding, which disconnects both directions when disposed. |
| `Bind<TViewModel, TView, TVMProp, TVProp, TDontCare>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty, IObservable<TDontCare>? signalViewUpdate, TriggerUpdate triggerUpdate)` | Binds properties using converter registration and direction signals. | `TViewModel : class`: The view model type; `TView : class, IViewFor`: The view type; `TVMProp`: The view model property type; `TVProp`: The view property type; `TDontCare`: The ignored signal payload; `TView` `view`: The view to bind; `TViewModel` `viewModel`: The view model to bind; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: The view model property path; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: The view property path; [`IObservable<TDontCare>?`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `signalViewUpdate`: The update stream, or null to observe both properties; [`TriggerUpdate`](unsafe.md) `triggerUpdate`: The direction driven by the stream | [`IReactiveBinding<TView, BindingChange>`](bindings.md): The connected two-way binding. |
| `Bind<TViewModel, TView, TViewModelProp, TViewProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TViewModelProp>> viewModelProperty, Expression<Func<TView, TViewProp>> viewProperty, TwoWayConverterPair<TViewModelProp, TViewProp> converters, ISequencer? scheduler, string bindingExpression)` | Binds a view-model property and a view property of differing types, converting in both directions. | `TViewModel : class`: The type declaring the view-model property; `TView : class, IViewFor`: The type declaring the view property; `TViewModelProp`: The type of the view-model property; `TViewProp`: The type of the view property; `TView` `view`: The view side of the binding; `TViewModel` `viewModel`: The view-model side of the binding; [`Expression<Func<TViewModel, TViewModelProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: The property on `viewModel`; [`Expression<Func<TView, TViewProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: The property on `view`; [`TwoWayConverterPair<TViewModelProp, TViewProp>`](converters.md) `converters`: Converts view model to view, and view back to view model; `ISequencer?` `scheduler`: The sequencer the view write is delivered on, or null to write on the thread that owns it; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `bindingExpression`: The bound expression, named when a write faults | [`IReactiveBinding<TView, BindingChange>`](bindings.md): The binding, which disconnects both directions when disposed. |
| `Bind<TViewModel, TView, TVMProp, TVProp, TDontCare>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMProp>> viewModelProperty, Expression<Func<TView, TVProp>> viewProperty, TwoWayConverterPair<TVMProp, TVProp> conversions, IObservable<TDontCare>? signalViewUpdate, TriggerUpdate triggerUpdate)` | Binds properties using explicit conversions and direction signals. | `TViewModel : class`: The view model type; `TView : class, IViewFor`: The view type; `TVMProp`: The view model property type; `TVProp`: The view property type; `TDontCare`: The ignored signal payload; `TView` `view`: The view to bind; `TViewModel` `viewModel`: The view model to bind; [`Expression<Func<TViewModel, TVMProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: The view model property path; [`Expression<Func<TView, TVProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: The view property path; [`TwoWayConverterPair<TVMProp, TVProp>`](converters.md) `conversions`: The conversions applied before comparison and writing; [`IObservable<TDontCare>?`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `signalViewUpdate`: The update stream, or null to observe both properties; [`TriggerUpdate`](unsafe.md) `triggerUpdate`: The direction driven by the stream | [`IReactiveBinding<TView, BindingChange>`](bindings.md): The connected two-way binding. |
| `BindOneWay<TSource, TTarget, TProp>(TSource source, TTarget target, Expression<Func<TSource, TProp>> sourceProperty, Expression<Func<TTarget, TProp>> targetProperty, ISequencer? scheduler, string bindingExpression)` | Binds a source property one way onto a target property of the same type. | `TSource : class`: The type declaring the observed property; `TTarget`: The type declaring the written property; `TProp`: The type of the value carried across the binding; `TSource` `source`: The object to observe; `TTarget` `target`: The object to write to; [`Expression<Func<TSource, TProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: The property to observe; [`Expression<Func<TTarget, TProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: The property to write; `ISequencer?` `scheduler`: The sequencer the write is delivered on, or null to write on the thread that owns it; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `bindingExpression`: The bound expression, named when a write faults | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that disconnects the binding. |
| `BindOneWay<TSource, TTarget, TSourceProp, TTargetProp>(TSource source, TTarget target, Expression<Func<TSource, TSourceProp>> sourceProperty, Expression<Func<TTarget, TTargetProp>> targetProperty, Func<TSourceProp, TTargetProp> conversion, ISequencer? scheduler, string bindingExpression)` | Binds a source property one way onto a target property of another type, applying a conversion. | `TSource : class`: The type declaring the observed property; `TTarget`: The type declaring the written property; `TSourceProp`: The type of the observed property; `TTargetProp`: The type of the written property; `TSource` `source`: The object to observe; `TTarget` `target`: The object to write to; [`Expression<Func<TSource, TSourceProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: The property to observe; [`Expression<Func<TTarget, TTargetProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: The property to write; [`Func<TSourceProp, TTargetProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `conversion`: Converts the observed value to the written one; `ISequencer?` `scheduler`: The sequencer the write is delivered on, or null to write on the thread that owns it; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `bindingExpression`: The bound expression, named when a write faults | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that disconnects the binding. |
| `BindTo<TValue, TTarget, TTargetValue>(IObservable<TValue> source, TTarget? target, Expression<Func<TTarget, TTargetValue>> targetProperty, object? conversionHint, IBindingTypeConverter? converterOverride, ISequencer? scheduler, string bindingExpression)` | Writes every value a sequence produces into a property, converting it on the way. | `TValue`: The type the sequence produces; `TTarget : class`: The type declaring the written property; `TTargetValue`: The type of the written property; [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source`: The sequence driving the writes; `TTarget?` `target`: The object declaring the written property; [`Expression<Func<TTarget, TTargetValue>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: The property to write; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: An optional hint handed to the converter; [`IBindingTypeConverter?`](converters.md) `converterOverride`: A converter that takes precedence over the registered ones; `ISequencer?` `scheduler`: The scheduler the writes are delivered on, or null to write on the thread that owns it; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `bindingExpression`: The bound expression, named when a write faults | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, stops writing. |
| `BindTwoWay<TSource, TTarget, TProp>(TSource source, TTarget target, Expression<Func<TSource, TProp>> sourceProperty, Expression<Func<TTarget, TProp>> targetProperty, ISequencer? scheduler, string bindingExpression)` | Binds a source and a target property of the same type to each other. | `TSource : class`: The type declaring the source property; `TTarget : class`: The type declaring the target property; `TProp`: The type of the value carried across the binding; `TSource` `source`: The first object of the binding; `TTarget` `target`: The second object of the binding; [`Expression<Func<TSource, TProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: The property on `source`; [`Expression<Func<TTarget, TProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: The property on `target`; `ISequencer?` `scheduler`: The sequencer the target write is delivered on, or null to write on the thread that owns it; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `bindingExpression`: The bound expression, named when a write faults | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that disconnects both directions. |
| `BindTwoWay<TSource, TTarget, TSourceProp, TTargetProp>(TSource source, TTarget target, Expression<Func<TSource, TSourceProp>> sourceProperty, Expression<Func<TTarget, TTargetProp>> targetProperty, TwoWayConverterPair<TSourceProp, TTargetProp> converters, ISequencer? scheduler, string bindingExpression)` | Binds a source and a target property of differing types to each other, converting in both directions. | `TSource : class`: The type declaring the source property; `TTarget : class`: The type declaring the target property; `TSourceProp`: The type of the source property; `TTargetProp`: The type of the target property; `TSource` `source`: The first object of the binding; `TTarget` `target`: The second object of the binding; [`Expression<Func<TSource, TSourceProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `sourceProperty`: The property on `source`; [`Expression<Func<TTarget, TTargetProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `targetProperty`: The property on `target`; [`TwoWayConverterPair<TSourceProp, TTargetProp>`](converters.md) `converters`: Converts source to target, and target back to source; `ISequencer?` `scheduler`: The sequencer the target write is delivered on, or null to write on the thread that owns it; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `bindingExpression`: The bound expression, named when a write faults | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that disconnects both directions. |
| `OneWayBind<TViewModel, TView, TProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TProp>> viewModelProperty, Expression<Func<TView, TProp>> viewProperty, ISequencer? scheduler, string bindingExpression)` | Binds a view-model property one way onto a view property of the same type. | `TViewModel : class`: The type declaring the view-model property; `TView : IViewFor`: The type declaring the view property; `TProp`: The type of the value carried across the binding; `TView` `view`: The view being written to; `TViewModel` `viewModel`: The view model being observed; [`Expression<Func<TViewModel, TProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: The property to observe; [`Expression<Func<TView, TProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: The property to write; `ISequencer?` `scheduler`: The sequencer the write is delivered on, or null to write on the thread that owns it; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `bindingExpression`: The bound expression, named when a write faults | [`IReactiveBinding<TView, TProp>`](bindings.md): The binding, which disconnects when disposed. |
| `OneWayBind<TViewModel, TView, TViewModelProp, TViewProp>(TView view, TViewModel viewModel, Expression<Func<TViewModel, TViewModelProp>> viewModelProperty, Expression<Func<TView, TViewProp>> viewProperty, Func<TViewModelProp, TViewProp> conversion, ISequencer? scheduler, string bindingExpression)` | Binds a view-model property one way onto a view property of another type, applying a conversion. | `TViewModel : class`: The type declaring the view-model property; `TView : IViewFor`: The type declaring the view property; `TViewModelProp`: The type of the view-model property; `TViewProp`: The type of the view property; `TView` `view`: The view being written to; `TViewModel` `viewModel`: The view model being observed; [`Expression<Func<TViewModel, TViewModelProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewModelProperty`: The property to observe; [`Expression<Func<TView, TViewProp>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `viewProperty`: The property to write; [`Func<TViewModelProp, TViewProp>`](https://learn.microsoft.com/dotnet/api/system.func-2) `conversion`: Converts the observed value to the view's type; `ISequencer?` `scheduler`: The sequencer the write is delivered on, or null to write on the thread that owns it; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `bindingExpression`: The bound expression, named when a write faults | [`IReactiveBinding<TView, TViewProp>`](bindings.md): The binding, which disconnects when disposed. |

#### `RuntimeCommandBindingFallback`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class RuntimeCommandBindingFallback` | Binds a command to a control, resolving the command and control properties at runtime. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BindCommand<TView, TViewModel, TProp, TControl>(TView view, TViewModel? viewModel, Expression<Func<TViewModel, TProp?>> commandProperty, Expression<Func<TView, TControl>> controlProperty, IObservable<object?> commandParameter, string? toEvent, string bindingExpression)` | Keeps the command an observed property holds bound to the control another one holds. | `TView : class`: The type declaring the control property; `TViewModel : class`: The type declaring the command property; `TProp : ICommand`: The type of the command property; `TControl : class`: The type of the control the command is bound to; `TView` `view`: The object declaring the control property; `TViewModel?` `viewModel`: The object declaring the command property; [`Expression<Func<TViewModel, TProp?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `commandProperty`: The property holding the command to bind; [`Expression<Func<TView, TControl>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `controlProperty`: The property holding the control to bind it to; [`IObservable<object?>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `commandParameter`: The values offered as the command parameter; [`string?`](https://learn.microsoft.com/dotnet/api/system.string) `toEvent`: The control event that executes the command, or null for the control's default; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `bindingExpression`: The bound expression, named when the observation faults | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, unbinds the command and stops observing. |

#### `RuntimeCommandFallback`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class RuntimeCommandFallback` | Resolves the command an `InvokeCommand` executes through the runtime expression engine. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `InvokeCommand<T, TTarget>(IObservable<T> source, TTarget? target, Expression<Func<TTarget, ICommand?>> commandProperty)` | Executes the command an observed property holds with each value the sequence produces. | `T`: The type of the value offered as the command parameter; `TTarget : class`: The type declaring the observed command property; [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) `source`: The sequence driving the executions; `TTarget?` `target`: The object declaring the command property; [`Expression<Func<TTarget, ICommand?>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `commandProperty`: The property holding the command to execute | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, stops executing the command. |

#### `RuntimeInteractionFallback`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class RuntimeInteractionFallback` | Registers an interaction handler against a property the runtime expression engine resolves. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BindInteraction<TViewModel, TInput, TOutput>(TViewModel? viewModel, Expression<Func<TViewModel, IInteraction<TInput, TOutput>>> interactionProperty, Func<IInteraction<TInput, TOutput>, IDisposable> register, string bindingExpression)` | Keeps a handler registered against whichever interaction the observed property holds. | `TViewModel : class`: The type declaring the interaction property; `TInput`: The type the interaction takes; `TOutput`: The type the interaction produces; `TViewModel?` `viewModel`: The object declaring the interaction property; [`Expression<Func<TViewModel, IInteraction<TInput, TOutput>>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `interactionProperty`: The property holding the interaction to handle; [`Func<IInteraction<TInput, TOutput>, IDisposable>`](https://learn.microsoft.com/dotnet/api/system.func-2) `register`: Registers the caller's handler against one interaction; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `bindingExpression`: The bound expression, named when the observation faults | [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable): A disposable that, when disposed, unregisters the handler and stops observing. |

#### `RuntimeObservationFallback`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class RuntimeObservationFallback` | Observes properties of WhenChanged, WhenChanging and WhenAnyValue calls the source generator did not handle, by walking the property expression at runtime and resolving each link through the registered [`ICreatesObservableForProperty`](mechanisms.md) implementations. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `WhenAnyValue<TSender, TValue>(TSender sender, Expression<Func<TSender, TValue>> property)` | Observes a property, emitting its value after each change. | `TSender : class`: The type of object being observed; `TValue`: The type of the property value; `TSender` `sender`: The object to observe; [`Expression<Func<TSender, TValue>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property`: The property expression | [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits property values after they change. |
| `WhenAnyValue<TSender, T1, T2>(TSender sender, Expression<Func<TSender, T1>> property1, Expression<Func<TSender, T2>> property2)` | Observes two properties, emitting both values whenever either changes. | `TSender : class`: The type of object being observed; `T1`: The type of the first property value; `T2`: The type of the second property value; `TSender` `sender`: The object to observe; [`Expression<Func<TSender, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: The first property expression; [`Expression<Func<TSender, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: The second property expression | [`IObservable<PropertyValues<T1, T2>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits a tuple of property values when any changes. |
| `WhenAnyValue<TSender, T1, T2, T3>(TSender sender, Expression<Func<TSender, T1>> property1, Expression<Func<TSender, T2>> property2, Expression<Func<TSender, T3>> property3)` | Observes three properties, emitting all their values whenever any changes. | `TSender : class`: The type of object being observed; `T1`: The type of the first property value; `T2`: The type of the second property value; `T3`: The type of the third property value; `TSender` `sender`: The object to observe; [`Expression<Func<TSender, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: The first property expression; [`Expression<Func<TSender, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: The second property expression; [`Expression<Func<TSender, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: The third property expression | [`IObservable<PropertyValues<T1, T2, T3>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits a tuple of property values when any changes. |
| `WhenChanged<TObj, TValue>(TObj obj, Expression<Func<TObj, TValue>> property)` | Observes a property, emitting its value after each change. | `TObj : class`: The type of object being observed; `TValue`: The type of the property value; `TObj` `obj`: The object to observe; [`Expression<Func<TObj, TValue>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property`: The property expression | [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits property values after they change. |
| `WhenChanged<TObj, T1, T2>(TObj obj, Expression<Func<TObj, T1>> property1, Expression<Func<TObj, T2>> property2)` | Observes two properties, emitting both values whenever either changes. | `TObj : class`: The type of object being observed; `T1`: The type of the first property value; `T2`: The type of the second property value; `TObj` `obj`: The object to observe; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: The first property expression; [`Expression<Func<TObj, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: The second property expression | [`IObservable<PropertyValues<T1, T2>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits a tuple of property values when any changes. |
| `WhenChanged<TObj, T1, T2, T3>(TObj obj, Expression<Func<TObj, T1>> property1, Expression<Func<TObj, T2>> property2, Expression<Func<TObj, T3>> property3)` | Observes three properties, emitting all their values whenever any changes. | `TObj : class`: The type of object being observed; `T1`: The type of the first property value; `T2`: The type of the second property value; `T3`: The type of the third property value; `TObj` `obj`: The object to observe; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: The first property expression; [`Expression<Func<TObj, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: The second property expression; [`Expression<Func<TObj, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: The third property expression | [`IObservable<PropertyValues<T1, T2, T3>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits a tuple of property values when any changes. |
| `WhenChanging<TObj, TValue>(TObj obj, Expression<Func<TObj, TValue>> property)` | Observes a property, emitting its value just before each change. | `TObj : class`: The type of object being observed; `TValue`: The type of the property value; `TObj` `obj`: The object to observe; [`Expression<Func<TObj, TValue>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property`: The property expression | [`IObservable<TValue>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits property values before they change. |
| `WhenChanging<TObj, T1, T2>(TObj obj, Expression<Func<TObj, T1>> property1, Expression<Func<TObj, T2>> property2)` | Observes two properties, emitting both values whenever either is about to change. | `TObj : class`: The type of object being observed; `T1`: The type of the first property value; `T2`: The type of the second property value; `TObj` `obj`: The object to observe; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: The first property expression; [`Expression<Func<TObj, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: The second property expression | [`IObservable<PropertyValues<T1, T2>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits a tuple of property values before any changes. |
| `WhenChanging<TObj, T1, T2, T3>(TObj obj, Expression<Func<TObj, T1>> property1, Expression<Func<TObj, T2>> property2, Expression<Func<TObj, T3>> property3)` | Observes three properties, emitting all their values whenever any is about to change. | `TObj : class`: The type of object being observed; `T1`: The type of the first property value; `T2`: The type of the second property value; `T3`: The type of the third property value; `TObj` `obj`: The object to observe; [`Expression<Func<TObj, T1>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property1`: The first property expression; [`Expression<Func<TObj, T2>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property2`: The second property expression; [`Expression<Func<TObj, T3>>`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression-1) `property3`: The third property expression | [`IObservable<PropertyValues<T1, T2, T3>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that emits a tuple of property values before any changes. |

## Platforms

### WPF

[Full description and examples](threading.md).

Types: `ReactiveUI.Binding.Wpf.BooleanToVisibilityHints`, `ReactiveUI.Binding.Wpf.BooleanToVisibilityTypeConverter`, `ReactiveUI.Binding.Wpf.Builder.WpfBindingBuilderExtensions`, `ReactiveUI.Binding.Wpf.DependencyObjectObservableForProperty`, `ReactiveUI.Binding.Wpf.DispatcherViewThreadInvoker`, `ReactiveUI.Binding.Wpf.VisibilityToBooleanTypeConverter`, `ReactiveUI.Binding.Wpf.WpfBindingModule`.

#### `BooleanToVisibilityHints`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `enum BooleanToVisibilityHints` | Conversion hints, passed as the conversion hint, that change how a boolean maps to a `Visibility`. | None. | — |

**Enum values**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `None = 0` | True is Visible and false is Collapsed. | None. | — |
| `Inverse = 2` | Swaps the mapping, so true is not visible and false is Visible. | None. | — |
| `UseHidden = 4` | Uses Hidden rather than Collapsed as the value that is not visible. | None. | — |

#### `BooleanToVisibilityTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class BooleanToVisibilityTypeConverter : BindingTypeConverter<bool, Visibility>` | Converts a [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) to `Visible` or `Collapsed`; the conversion always succeeds. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BooleanToVisibilityTypeConverter()` | Initializes a new instance of the BooleanToVisibilityTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(bool from, object? conversionHint, out Visibility result)` | Converts a value to the target type without boxing. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`Visibility`](https://learn.microsoft.com/dotnet/api/system.windows.visibility) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `WpfBindingBuilderExtensions`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class WpfBindingBuilderExtensions` | WPF-specific extensions for the ReactiveUI.Binding builder. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `IReactiveUIBindingBuilder.WithWpf()` | Registers the WPF module, which adds dependency-property observation and the dispatcher view thread invoker. | [`IReactiveUIBindingBuilder`](setup.md) `builder` (receiver) | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `IAppBuilder.WithWpf()` | Registers the WPF module, which adds dependency-property observation and the dispatcher view thread invoker. | `IAppBuilder` `builder` (receiver) | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |

#### `DependencyObjectObservableForProperty`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `class DependencyObjectObservableForProperty` | Observes a WPF `DependencyObject` property through its `{PropertyName}Property` dependency property. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `DependencyObjectObservableForProperty()` | Initializes a new instance of the DependencyObjectObservableForProperty class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObject(Type type, string propertyName, bool beforeChanged)` | Returns the WPF dependency-object affinity when the type declares a public static `{propertyName}Property` field. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `type`: The type that owns the property; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name, without the `Property` suffix; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChanged`: Ignored | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): `WpfDependencyObject` for a `DependencyObject` type with such a field; otherwise zero. |
| `GetNotificationForProperty(object sender, Expression expression, string propertyName, bool beforeChanged, bool suppressWarnings)` | Returns an observable that raises whenever the dependency property changes on `sender`. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `sender`: The `DependencyObject` to observe; [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The expression carried on each notification; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name, without the `Property` suffix; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChanged`: Ignored; notifications are always after the change; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `suppressWarnings`: `true` to skip the debug message written when no descriptor is found | [`IObservable<IObservedChange<object, object?>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that subscribes to the descriptor's value-changed event and unsubscribes on disposal. |

#### `DispatcherViewThreadInvoker`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class DispatcherViewThreadInvoker` | Routes writes to a WPF `DispatcherObject` onto the thread its dispatcher owns. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `DispatcherViewThreadInvoker()` | Initializes a new instance of the DispatcherViewThreadInvoker class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `CheckAccess(object target)` | Returns whether the calling thread owns the target's dispatcher. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `target`: A `DispatcherObject`; any other type throws [`InvalidCastException`](https://learn.microsoft.com/dotnet/api/system.invalidcastexception) | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` when the calling thread may touch the target. |
| `Claims(object target)` | Determines whether this invoker handles `target`. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `target`: The object a binding writes to | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` when the object belongs to this invoker's platform; otherwise `false`. |
| `Post(object target, Action<object?> callback, object? state)` | Queues `callback` on the target's dispatcher at normal priority, or runs it inline when the target has no dispatcher. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `target`: A `DispatcherObject`; any other type throws [`InvalidCastException`](https://learn.microsoft.com/dotnet/api/system.invalidcastexception); [`Action<object?>`](https://learn.microsoft.com/dotnet/api/system.action-1) `callback`: The callback to run; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `state`: The value passed to `callback` | — |

#### `VisibilityToBooleanTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class VisibilityToBooleanTypeConverter : BindingTypeConverter<Visibility, bool>` | Converts a [`Visibility`](https://learn.microsoft.com/dotnet/api/system.windows.visibility) to a [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean), true for Visible and false for Hidden or Collapsed; the conversion always succeeds. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `VisibilityToBooleanTypeConverter()` | Initializes a new instance of the VisibilityToBooleanTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(Visibility from, object? conversionHint, out bool result)` | Converts a value to the target type without boxing. | [`Visibility`](https://learn.microsoft.com/dotnet/api/system.windows.visibility) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `WpfBindingModule`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class WpfBindingModule` | Registers the WPF dependency-property observer and view thread invoker with the dependency resolver. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `WpfBindingModule()` | Initializes a new instance of the WpfBindingModule class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Configure(IMutableDependencyResolver resolver)` | Configures the specified dependency resolver with required services and components. | `IMutableDependencyResolver` `resolver`: The dependency resolver to configure. Cannot be null | — |

### WinForms

[Full description and examples](threading.md).

Types: `ReactiveUI.Binding.WinForms.Builder.WinFormsBindingBuilderExtensions`, `ReactiveUI.Binding.WinForms.ControlViewThreadInvoker`, `ReactiveUI.Binding.WinForms.WinFormsBindingModule`, `ReactiveUI.Binding.WinForms.WinFormsCreatesObservableForProperty`.

#### `WinFormsBindingBuilderExtensions`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class WinFormsBindingBuilderExtensions` | WinForms-specific extensions for the ReactiveUI.Binding builder. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `IReactiveUIBindingBuilder.WithWinForms()` | Registers the WinForms module, which adds event-based property observation and the control view thread invoker. | [`IReactiveUIBindingBuilder`](setup.md) `builder` (receiver) | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `IAppBuilder.WithWinForms()` | Registers the WinForms module, which adds event-based property observation and the control view thread invoker. | `IAppBuilder` `builder` (receiver) | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |

#### `ControlViewThreadInvoker`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ControlViewThreadInvoker` | Routes writes to a WinForms `Control` onto the thread that created its handle. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `ControlViewThreadInvoker()` | Initializes a new instance of the ControlViewThreadInvoker class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `CheckAccess(object target)` | Returns whether the calling thread may write to the control; also true while the control has no handle. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `target`: A `Control`; any other type throws [`InvalidCastException`](https://learn.microsoft.com/dotnet/api/system.invalidcastexception) | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` when `InvokeRequired` is false. |
| `Claims(object target)` | Determines whether this invoker handles `target`. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `target`: The object a binding writes to | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` when the object belongs to this invoker's platform; otherwise `false`. |
| `Post(object target, Action<object?> callback, object? state)` | Queues `callback` with `BeginInvoke`, or runs it inline when no invoke is required. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `target`: A `Control`; any other type throws [`InvalidCastException`](https://learn.microsoft.com/dotnet/api/system.invalidcastexception); [`Action<object?>`](https://learn.microsoft.com/dotnet/api/system.action-1) `callback`: The callback to run; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `state`: The value passed to `callback` | — |

#### `WinFormsBindingModule`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class WinFormsBindingModule` | Registers the WinForms event-based property observer and view thread invoker with the dependency resolver. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `WinFormsBindingModule()` | Initializes a new instance of the WinFormsBindingModule class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Configure(IMutableDependencyResolver resolver)` | Configures the specified dependency resolver with required services and components. | `IMutableDependencyResolver` `resolver`: The dependency resolver to configure. Cannot be null | — |

#### `WinFormsCreatesObservableForProperty`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `class WinFormsCreatesObservableForProperty` | Observes a WinForms `Component` property by subscribing to its public `{PropertyName}Changed` event through reflection. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `WinFormsCreatesObservableForProperty()` | Initializes a new instance of the WinFormsCreatesObservableForProperty class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GetAffinityForObject(Type type, string propertyName, bool beforeChanged)` | Returns the WinForms event affinity when the component type has a public instance `{propertyName}Changed` event. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `type`: The type that owns the property; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name, without the `Changed` suffix; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChanged`: `true` always yields zero, since WinForms raises no before-change event | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): `WinFormsEvent` for a `Component` type with such an event; otherwise zero. |
| `GetNotificationForProperty(object sender, Expression expression, string propertyName, bool beforeChanged, bool suppressWarnings)` | Returns an observable that raises whenever `sender` raises its `{propertyName}Changed` event. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `sender`: The component to observe; [`Expression`](https://learn.microsoft.com/dotnet/api/system.linq.expressions.expression) `expression`: The expression carried on each notification; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `propertyName`: The property name, without the `Changed` suffix; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `beforeChanged`: Ignored; notifications are always after the change; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `suppressWarnings`: Ignored | [`IObservable<IObservedChange<object, object?>>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): An observable that adds the event handler on subscription and removes it on disposal. |

### .NET MAUI

[Full description and examples](threading.md).

Types: `ReactiveUI.Binding.Maui.BooleanToVisibilityHints`, `ReactiveUI.Binding.Maui.BooleanToVisibilityTypeConverter`, `ReactiveUI.Binding.Maui.Builder.MauiBindingBuilderExtensions`, `ReactiveUI.Binding.Maui.DispatcherViewThreadInvoker`, `ReactiveUI.Binding.Maui.MauiBindingModule`, `ReactiveUI.Binding.Maui.VisibilityToBooleanTypeConverter`.

#### `BooleanToVisibilityHints`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `enum BooleanToVisibilityHints` | Conversion hints, passed as the conversion hint, that change how a boolean maps to a `Visibility`. | None. | — |

**Enum values**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `None = 0` | True is Visible and false is Collapsed. | None. | — |
| `Inverse = 2` | Swaps the mapping, so true is not visible and false is Visible. | None. | — |
| `UseHidden = 4` | Uses Hidden rather than Collapsed as the value that is not visible; ignored on WinUI, which has no Hidden. | None. | — |

#### `BooleanToVisibilityTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class BooleanToVisibilityTypeConverter : BindingTypeConverter<bool, Visibility>` | Converts a [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) to `Visible` or `Collapsed`; the conversion always succeeds. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BooleanToVisibilityTypeConverter()` | Initializes a new instance of the BooleanToVisibilityTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(bool from, object? conversionHint, out Visibility result)` | Converts a value to the target type without boxing. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`Visibility`](https://learn.microsoft.com/dotnet/api/microsoft.maui.visibility) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |

#### `MauiBindingBuilderExtensions`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static class MauiBindingBuilderExtensions` | MAUI-specific extensions for the ReactiveUI.Binding builder. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `IReactiveUIBindingBuilder.WithMaui()` | Registers the MAUI module, which adds the view thread invoker, the Visibility converters and, in the WinUI build, dependency-property observation. | [`IReactiveUIBindingBuilder`](setup.md) `builder` (receiver) | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |
| `IAppBuilder.WithMaui()` | Registers the MAUI module, which adds the view thread invoker, the Visibility converters and, in the WinUI build, dependency-property observation. | `IAppBuilder` `builder` (receiver) | [`IReactiveUIBindingBuilder`](setup.md): The builder instance for chaining. |

#### `DispatcherViewThreadInvoker`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class DispatcherViewThreadInvoker` | Routes writes to a MAUI `BindableObject` through the dispatcher it carries. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `DispatcherViewThreadInvoker()` | Initializes a new instance of the DispatcherViewThreadInvoker class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `CheckAccess(object target)` | Returns whether the calling thread may write to the target; also true when the target has no dispatcher. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `target`: A `BindableObject`; any other type throws [`InvalidCastException`](https://learn.microsoft.com/dotnet/api/system.invalidcastexception) | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `false` only when the target's dispatcher requires a dispatch from the calling thread. |
| `Claims(object target)` | Determines whether this invoker handles `target`. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `target`: The object a binding writes to | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` when the object belongs to this invoker's platform; otherwise `false`. |
| `Post(object target, Action<object?> callback, object? state)` | Queues `callback` on the target's dispatcher, or runs it inline when the target has no dispatcher. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) `target`: A `BindableObject`; any other type throws [`InvalidCastException`](https://learn.microsoft.com/dotnet/api/system.invalidcastexception); [`Action<object?>`](https://learn.microsoft.com/dotnet/api/system.action-1) `callback`: The callback to run; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `state`: The value passed to `callback` | — |

#### `MauiBindingModule`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class MauiBindingModule` | Registers the MAUI view thread invoker, the Visibility converters and, in the WinUI build, the dependency-property observer with the dependency resolver. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `MauiBindingModule()` | Initializes a new instance of the MauiBindingModule class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `Configure(IMutableDependencyResolver resolver)` | Configures the specified dependency resolver with required services and components. | `IMutableDependencyResolver` `resolver`: The dependency resolver to configure. Cannot be null | — |

#### `VisibilityToBooleanTypeConverter`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class VisibilityToBooleanTypeConverter : BindingTypeConverter<Visibility, bool>` | Converts a [`Visibility`](https://learn.microsoft.com/dotnet/api/microsoft.maui.visibility) to a [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean), true for Visible and false for any other value; the conversion always succeeds. | None. | — |

**Constructors**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `VisibilityToBooleanTypeConverter()` | Initializes a new instance of the VisibilityToBooleanTypeConverter class. | None. | — |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `override GetAffinityForObjects()` | Returns this converter's priority among the converters registered for the same type pair. | None. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32): A positive value when the converter applies; zero or less excludes it. The highest value wins and the earliest registered converter wins a tie. The built-in converters return 2, and [`EqualityTypeConverter`](converters.md) returns 1, so a larger value outranks them. |
| `override TryConvert(Visibility from, object? conversionHint, out bool result)` | Converts a value to the target type without boxing. | [`Visibility`](https://learn.microsoft.com/dotnet/api/microsoft.maui.visibility) `from`: The value to convert; [`object?`](https://learn.microsoft.com/dotnet/api/system.object) `conversionHint`: Implementation-defined hint for conversion (e.g., format string, locale); out [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `result`: The converted value. May be `null` when conversion succeeds for nullable targets | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): `true` if conversion succeeded; otherwise, `false`. |
