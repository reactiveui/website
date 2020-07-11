title: Observable.Average(IObservable<Nullable<Int64>>)
---
# Observable.Average Method (IObservable\<Nullable\<Int64\>\>)

Computes the average of an observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IObservable(Of Nullable(Of Long)) _
) As IObservable(Of Nullable(Of Double))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Long))
Dim returnValue As IObservable(Of Nullable(Of Double))

returnValue = source.Average()
```

```csharp
public static IObservable<Nullable<double>> Average(
    this IObservable<Nullable<long>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<double>>^ Average(
    IObservable<Nullable<long long>>^ source
)
```

```fsharp
static member Average : 
        source:IObservable<Nullable<int64>> -> IObservable<Nullable<float>> 
```

```jscript
public static function Average(
    source : IObservable<Nullable<long>>
) : IObservable<Nullable<double>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>  
  A sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values to calculate the average of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
The average of an observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Average Overload](Average/Observable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Average Method (IObservable\<Double\>)

Computes the average of an observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IObservable(Of Double) _
) As IObservable(Of Double)
```

```vb
'Usage
Dim source As IObservable(Of Double)
Dim returnValue As IObservable(Of Double)

returnValue = source.Average()
```

```csharp
public static IObservable<double> Average(
    this IObservable<double> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<double>^ Average(
    IObservable<double>^ source
)
```

```fsharp
static member Average : 
        source:IObservable<float> -> IObservable<float> 
```

```jscript
public static function Average(
    source : IObservable<double>
) : IObservable<double>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
  A sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values to calculate the average of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
The average of an observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Average Overload](Average/Observable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Average Method (IObservable\<Nullable\<Single\>\>)

Computes the average of an observable sequence of nullable Float values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IObservable(Of Nullable(Of Single)) _
) As IObservable(Of Nullable(Of Single))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Single))
Dim returnValue As IObservable(Of Nullable(Of Single))

returnValue = source.Average()
```

```csharp
public static IObservable<Nullable<float>> Average(
    this IObservable<Nullable<float>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<float>>^ Average(
    IObservable<Nullable<float>>^ source
)
```

```fsharp
static member Average : 
        source:IObservable<Nullable<float32>> -> IObservable<Nullable<float32>> 
```

```jscript
public static function Average(
    source : IObservable<Nullable<float>>
) : IObservable<Nullable<float>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>  
  A sequence of nullable Float values to calculate the average of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>  
The average of an observable sequence of nullable Float values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Average Overload](Average/Observable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Average Method (IObservable\<Single\>)

Computes the average of an observable sequence of Float values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IObservable(Of Single) _
) As IObservable(Of Single)
```

```vb
'Usage
Dim source As IObservable(Of Single)
Dim returnValue As IObservable(Of Single)

returnValue = source.Average()
```

```csharp
public static IObservable<float> Average(
    this IObservable<float> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<float>^ Average(
    IObservable<float>^ source
)
```

```fsharp
static member Average : 
        source:IObservable<float32> -> IObservable<float32> 
```

```jscript
public static function Average(
    source : IObservable<float>
) : IObservable<float>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>  
  A sequence of Float values to calculate the average of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>  
The average of an observable sequence of Float values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Average Overload](Average/Observable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Average Method

Include Protected Members  
Include Inherited Members

Computes the average of an observable sequence.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IObservable<Decimal>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.average(system.iobservable%7bsystem.decimal%7d)(v=VS.103))Computes the average of an observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IObservable<Double>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.average(system.iobservable%7bsystem.double%7d)(v=VS.103))Computes the average of an observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IObservable<Int32>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.average(system.iobservable%7bsystem.int32%7d)(v=VS.103))Computes the average of an observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IObservable<Int64>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.average(system.iobservable%7bsystem.int64%7d)(v=VS.103))Computes the average of an observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IObservable<Nullable<Decimal>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.average(system.iobservable%7bsystem.nullable%7bsystem.decimal%7d%7d)(v=VS.103))Computes the average of an observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IObservable<Nullable<Double>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.average(system.iobservable%7bsystem.nullable%7bsystem.double%7d%7d)(v=VS.103))Computes the average of an observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IObservable<Nullable<Int32>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.average(system.iobservable%7bsystem.nullable%7bsystem.int32%7d%7d)(v=VS.103))Computes the average of an observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IObservable<Nullable<Int64>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.average(system.iobservable%7bsystem.nullable%7bsystem.int64%7d%7d)(v=VS.103))Computes the average of an observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IObservable<Nullable<Single>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.average(system.iobservable%7bsystem.nullable%7bsystem.single%7d%7d)(v=VS.103))Computes the average of an observable sequence of nullable Float values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IObservable<Single>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.average(system.iobservable%7bsystem.single%7d)(v=VS.103))Computes the average of an observable sequence of Float values.Top

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)





# Observable.Average Method (IObservable\<Nullable\<Int32\>\>)

Computes the average of an observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IObservable(Of Nullable(Of Integer)) _
) As IObservable(Of Nullable(Of Double))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Integer))
Dim returnValue As IObservable(Of Nullable(Of Double))

returnValue = source.Average()
```

```csharp
public static IObservable<Nullable<double>> Average(
    this IObservable<Nullable<int>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<double>>^ Average(
    IObservable<Nullable<int>>^ source
)
```

```fsharp
static member Average : 
        source:IObservable<Nullable<int>> -> IObservable<Nullable<float>> 
```

```jscript
public static function Average(
    source : IObservable<Nullable<int>>
) : IObservable<Nullable<double>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>  
  A sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values to calculate the average of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
The average of an observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Average Overload](Average/Observable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Average Method (IObservable\<Decimal\>)

Computes the average of an observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IObservable(Of Decimal) _
) As IObservable(Of Decimal)
```

```vb
'Usage
Dim source As IObservable(Of Decimal)
Dim returnValue As IObservable(Of Decimal)

returnValue = source.Average()
```

```csharp
public static IObservable<decimal> Average(
    this IObservable<decimal> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Decimal>^ Average(
    IObservable<Decimal>^ source
)
```

```fsharp
static member Average : 
        source:IObservable<decimal> -> IObservable<decimal> 
```

```jscript
public static function Average(
    source : IObservable<decimal>
) : IObservable<decimal>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>  
  A sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values to calculate the average of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>  
The average of an observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Average Overload](Average/Observable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Average Method (IObservable\<Int64\>)

Computes the average of an observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IObservable(Of Long) _
) As IObservable(Of Double)
```

```vb
'Usage
Dim source As IObservable(Of Long)
Dim returnValue As IObservable(Of Double)

returnValue = source.Average()
```

```csharp
public static IObservable<double> Average(
    this IObservable<long> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<double>^ Average(
    IObservable<long long>^ source
)
```

```fsharp
static member Average : 
        source:IObservable<int64> -> IObservable<float> 
```

```jscript
public static function Average(
    source : IObservable<long>
) : IObservable<double>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
  A sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values to calculate the average of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
The average of an observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Average Overload](Average/Observable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Average Method (IObservable\<Nullable\<Double\>\>)

Computes the average of an observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IObservable(Of Nullable(Of Double)) _
) As IObservable(Of Nullable(Of Double))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Double))
Dim returnValue As IObservable(Of Nullable(Of Double))

returnValue = source.Average()
```

```csharp
public static IObservable<Nullable<double>> Average(
    this IObservable<Nullable<double>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<double>>^ Average(
    IObservable<Nullable<double>>^ source
)
```

```fsharp
static member Average : 
        source:IObservable<Nullable<float>> -> IObservable<Nullable<float>> 
```

```jscript
public static function Average(
    source : IObservable<Nullable<double>>
) : IObservable<Nullable<double>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
  A sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values to calculate the average of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
The average of an observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Average Overload](Average/Observable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Average Method (IObservable\<Nullable\<Decimal\>\>)

Computes the average of an observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IObservable(Of Nullable(Of Decimal)) _
) As IObservable(Of Nullable(Of Decimal))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Decimal))
Dim returnValue As IObservable(Of Nullable(Of Decimal))

returnValue = source.Average()
```

```csharp
public static IObservable<Nullable<decimal>> Average(
    this IObservable<Nullable<decimal>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<Decimal>>^ Average(
    IObservable<Nullable<Decimal>>^ source
)
```

```fsharp
static member Average : 
        source:IObservable<Nullable<decimal>> -> IObservable<Nullable<decimal>> 
```

```jscript
public static function Average(
    source : IObservable<Nullable<decimal>>
) : IObservable<Nullable<decimal>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>  
  A sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values to calculate the average of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>  
The average of an observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Average Overload](Average/Observable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Average Method (IObservable\<Int32\>)

Computes the average of an observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IObservable(Of Integer) _
) As IObservable(Of Double)
```

```vb
'Usage
Dim source As IObservable(Of Integer)
Dim returnValue As IObservable(Of Double)

returnValue = source.Average()
```

```csharp
public static IObservable<double> Average(
    this IObservable<int> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<double>^ Average(
    IObservable<int>^ source
)
```

```fsharp
static member Average : 
        source:IObservable<int> -> IObservable<float> 
```

```jscript
public static function Average(
    source : IObservable<int>
) : IObservable<double>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
  A sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values to calculate the average of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
The average of an observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Average Overload](Average/Observable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)







