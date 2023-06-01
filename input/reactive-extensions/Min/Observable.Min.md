title: Observable.Min(IObservable<Nullable<Single>>)
---
# Observable.Min Method (IObservable\<Nullable\<Single\>\>)

Returns the minimum value in an observable sequence of nullable Float values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IObservable(Of Nullable(Of Single)) _
) As IObservable(Of Nullable(Of Single))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Single))
Dim returnValue As IObservable(Of Nullable(Of Single))

returnValue = source.Min()
```

```csharp
public static IObservable<Nullable<float>> Min(
    this IObservable<Nullable<float>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<float>>^ Min(
    IObservable<Nullable<float>>^ source
)
```

```fsharp
static member Min : 
        source:IObservable<Nullable<float32>> -> IObservable<Nullable<float32>> 
```

```javascript
public static function Min(
    source : IObservable<Nullable<float>>
) : IObservable<Nullable<float>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>  
  A sequence of nullable Float values to determine the minimum value of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>  
The minimum value in an observable sequence of nullable Float values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Min Overload](Min/Observable.Min)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Min Method (IObservable\<Nullable\<Double\>\>)

Returns the minimum value in an observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IObservable(Of Nullable(Of Double)) _
) As IObservable(Of Nullable(Of Double))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Double))
Dim returnValue As IObservable(Of Nullable(Of Double))

returnValue = source.Min()
```

```csharp
public static IObservable<Nullable<double>> Min(
    this IObservable<Nullable<double>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<double>>^ Min(
    IObservable<Nullable<double>>^ source
)
```

```fsharp
static member Min : 
        source:IObservable<Nullable<float>> -> IObservable<Nullable<float>> 
```

```javascript
public static function Min(
    source : IObservable<Nullable<double>>
) : IObservable<Nullable<double>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
  A sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values to determine the minimum value of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
The minimum value in an observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Min Overload](Min/Observable.Min)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Min Method

Include Protected Members  
Include Inherited Members

Returns the minimum value.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IObservable<Decimal>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.min(system.iobservable%7bsystem.decimal%7d)(v=VS.103))Returns the minimum value in an observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IObservable<Double>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.min(system.iobservable%7bsystem.double%7d)(v=VS.103))Returns the minimum value in an observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IObservable<Int32>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.min(system.iobservable%7bsystem.int32%7d)(v=VS.103))Returns the minimum value in an observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IObservable<Int64>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.min(system.iobservable%7bsystem.int64%7d)(v=VS.103))Returns the minimum value in an observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IObservable<Nullable<Decimal>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.min(system.iobservable%7bsystem.nullable%7bsystem.decimal%7d%7d)(v=VS.103))Returns the minimum value in an observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IObservable<Nullable<Double>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.min(system.iobservable%7bsystem.nullable%7bsystem.double%7d%7d)(v=VS.103))Returns the minimum value in an observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IObservable<Nullable<Int32>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.min(system.iobservable%7bsystem.nullable%7bsystem.int32%7d%7d)(v=VS.103))Returns the minimum value in an observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IObservable<Nullable<Int64>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.min(system.iobservable%7bsystem.nullable%7bsystem.int64%7d%7d)(v=VS.103))Returns the minimum value in an observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IObservable<Nullable<Single>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.min(system.iobservable%7bsystem.nullable%7bsystem.single%7d%7d)(v=VS.103))Returns the minimum value in an observable sequence of nullable Float values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IObservable<Single>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.min(system.iobservable%7bsystem.single%7d)(v=VS.103))Returns the minimum value in an observable sequence of Float values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min<TSource>(IObservable<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.min%60%601(system.iobservable%7b%60%600%7d)(v=VS.103))Returns the minimum element in an observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min<TSource>(IObservable<TSource>, IComparer<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.min%60%601(system.iobservable%7b%60%600%7d%2csystem.collections.generic.icomparer%7b%60%600%7d)(v=VS.103))Returns the minimum value in an observable sequence according to the specified comparer.Top

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)





# Observable.Min Method (IObservable\<Decimal\>)

Returns the minimum value in an observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IObservable(Of Decimal) _
) As IObservable(Of Decimal)
```

```vb
'Usage
Dim source As IObservable(Of Decimal)
Dim returnValue As IObservable(Of Decimal)

returnValue = source.Min()
```

```csharp
public static IObservable<decimal> Min(
    this IObservable<decimal> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Decimal>^ Min(
    IObservable<Decimal>^ source
)
```

```fsharp
static member Min : 
        source:IObservable<decimal> -> IObservable<decimal> 
```

```javascript
public static function Min(
    source : IObservable<decimal>
) : IObservable<decimal>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>  
  A sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values to determine the minimum value of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>  
The minimum value in an observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Min Overload](Min/Observable.Min)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Min Method (IObservable\<Nullable\<Decimal\>\>)

Returns the minimum value in an observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IObservable(Of Nullable(Of Decimal)) _
) As IObservable(Of Nullable(Of Decimal))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Decimal))
Dim returnValue As IObservable(Of Nullable(Of Decimal))

returnValue = source.Min()
```

```csharp
public static IObservable<Nullable<decimal>> Min(
    this IObservable<Nullable<decimal>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<Decimal>>^ Min(
    IObservable<Nullable<Decimal>>^ source
)
```

```fsharp
static member Min : 
        source:IObservable<Nullable<decimal>> -> IObservable<Nullable<decimal>> 
```

```javascript
public static function Min(
    source : IObservable<Nullable<decimal>>
) : IObservable<Nullable<decimal>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>  
  A sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values to determine the minimum value of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>  
The minimum value in an observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Min Overload](Min/Observable.Min)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Min Method (IObservable\<Int64\>)

Returns the minimum value in an observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IObservable(Of Long) _
) As IObservable(Of Long)
```

```vb
'Usage
Dim source As IObservable(Of Long)
Dim returnValue As IObservable(Of Long)

returnValue = source.Min()
```

```csharp
public static IObservable<long> Min(
    this IObservable<long> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<long long>^ Min(
    IObservable<long long>^ source
)
```

```fsharp
static member Min : 
        source:IObservable<int64> -> IObservable<int64> 
```

```javascript
public static function Min(
    source : IObservable<long>
) : IObservable<long>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
  A sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values to determine the minimum value of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
The minimum value in an observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Min Overload](Min/Observable.Min)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Min Method (IObservable\<Int32\>)

Returns the minimum value in an observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IObservable(Of Integer) _
) As IObservable(Of Integer)
```

```vb
'Usage
Dim source As IObservable(Of Integer)
Dim returnValue As IObservable(Of Integer)

returnValue = source.Min()
```

```csharp
public static IObservable<int> Min(
    this IObservable<int> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<int>^ Min(
    IObservable<int>^ source
)
```

```fsharp
static member Min : 
        source:IObservable<int> -> IObservable<int> 
```

```javascript
public static function Min(
    source : IObservable<int>
) : IObservable<int>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
  A sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values to determine the minimum value of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
The minimum value in an observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Min Overload](Min/Observable.Min)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Min Method (IObservable\<Nullable\<Int32\>\>)

Returns the minimum value in an observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IObservable(Of Nullable(Of Integer)) _
) As IObservable(Of Nullable(Of Integer))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Integer))
Dim returnValue As IObservable(Of Nullable(Of Integer))

returnValue = source.Min()
```

```csharp
public static IObservable<Nullable<int>> Min(
    this IObservable<Nullable<int>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<int>>^ Min(
    IObservable<Nullable<int>>^ source
)
```

```fsharp
static member Min : 
        source:IObservable<Nullable<int>> -> IObservable<Nullable<int>> 
```

```javascript
public static function Min(
    source : IObservable<Nullable<int>>
) : IObservable<Nullable<int>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>  
  A sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values to determine the minimum value of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>  
The minimum value in an observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Min Overload](Min/Observable.Min)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Min Method (IObservable\<Double\>)

Returns the minimum value in an observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IObservable(Of Double) _
) As IObservable(Of Double)
```

```vb
'Usage
Dim source As IObservable(Of Double)
Dim returnValue As IObservable(Of Double)

returnValue = source.Min()
```

```csharp
public static IObservable<double> Min(
    this IObservable<double> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<double>^ Min(
    IObservable<double>^ source
)
```

```fsharp
static member Min : 
        source:IObservable<float> -> IObservable<float> 
```

```javascript
public static function Min(
    source : IObservable<double>
) : IObservable<double>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
  A sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values to determine the minimum value of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
The minimum value in an observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Min Overload](Min/Observable.Min)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Min Method (IObservable\<Single\>)

Returns the minimum value in an observable sequence of Float values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IObservable(Of Single) _
) As IObservable(Of Single)
```

```vb
'Usage
Dim source As IObservable(Of Single)
Dim returnValue As IObservable(Of Single)

returnValue = source.Min()
```

```csharp
public static IObservable<float> Min(
    this IObservable<float> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<float>^ Min(
    IObservable<float>^ source
)
```

```fsharp
static member Min : 
        source:IObservable<float32> -> IObservable<float32> 
```

```javascript
public static function Min(
    source : IObservable<float>
) : IObservable<float>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>  
  A sequence of Float values to determine the minimum value of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>  
The minimum value in an observable sequence of Float values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Min Overload](Min/Observable.Min)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Min Method (IObservable\<Nullable\<Int64\>\>)

Returns the minimum value in an observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IObservable(Of Nullable(Of Long)) _
) As IObservable(Of Nullable(Of Long))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Long))
Dim returnValue As IObservable(Of Nullable(Of Long))

returnValue = source.Min()
```

```csharp
public static IObservable<Nullable<long>> Min(
    this IObservable<Nullable<long>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<long long>>^ Min(
    IObservable<Nullable<long long>>^ source
)
```

```fsharp
static member Min : 
        source:IObservable<Nullable<int64>> -> IObservable<Nullable<int64>> 
```

```javascript
public static function Min(
    source : IObservable<Nullable<long>>
) : IObservable<Nullable<long>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>  
  A sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values to determine the minimum value of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>  
The minimum value in an observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Min Overload](Min/Observable.Min)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)







