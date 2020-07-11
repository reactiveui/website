title: Qbservable.Average(IQbservable<Nullable<Single>>)
---
# Qbservable.Average Method (IQbservable\<Nullable\<Single\>\>)

Computes the average of a queryable observable sequence of nullable Float values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IQbservable(Of Nullable(Of Single)) _
) As IQbservable(Of Nullable(Of Single))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Single))
Dim returnValue As IQbservable(Of Nullable(Of Single))

returnValue = source.Average()
```

```csharp
public static IQbservable<Nullable<float>> Average(
    this IQbservable<Nullable<float>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<float>>^ Average(
    IQbservable<Nullable<float>>^ source
)
```

```fsharp
static member Average : 
        source:IQbservable<Nullable<float32>> -> IQbservable<Nullable<float32>> 
```

```jscript
public static function Average(
    source : IQbservable<Nullable<float>>
) : IQbservable<Nullable<float>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>  
  A sequence of nullable Float values to calculate the average of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>  
The average of a queryable observable sequence of nullable Float values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Average Overload](Average/Qbservable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Qbservable.Average Method (IQbservable\<Nullable\<Int32\>\>)

Computes the average of a queryable observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IQbservable(Of Nullable(Of Integer)) _
) As IQbservable(Of Nullable(Of Double))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Integer))
Dim returnValue As IQbservable(Of Nullable(Of Double))

returnValue = source.Average()
```

```csharp
public static IQbservable<Nullable<double>> Average(
    this IQbservable<Nullable<int>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<double>>^ Average(
    IQbservable<Nullable<int>>^ source
)
```

```fsharp
static member Average : 
        source:IQbservable<Nullable<int>> -> IQbservable<Nullable<float>> 
```

```jscript
public static function Average(
    source : IQbservable<Nullable<int>>
) : IQbservable<Nullable<double>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>  
  A sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values to calculate the average of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
The average of a queryable observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Average Overload](Average/Qbservable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Qbservable.Average Method (IQbservable\<Nullable\<Decimal\>\>)

Computes the average of a queryable observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IQbservable(Of Nullable(Of Decimal)) _
) As IQbservable(Of Nullable(Of Decimal))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Decimal))
Dim returnValue As IQbservable(Of Nullable(Of Decimal))

returnValue = source.Average()
```

```csharp
public static IQbservable<Nullable<decimal>> Average(
    this IQbservable<Nullable<decimal>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<Decimal>>^ Average(
    IQbservable<Nullable<Decimal>>^ source
)
```

```fsharp
static member Average : 
        source:IQbservable<Nullable<decimal>> -> IQbservable<Nullable<decimal>> 
```

```jscript
public static function Average(
    source : IQbservable<Nullable<decimal>>
) : IQbservable<Nullable<decimal>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>  
  A sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values to calculate the average of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>  
The average of a queryable observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Average Overload](Average/Qbservable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Qbservable.Average Method (IQbservable\<Decimal\>)

Computes the average of a queryable observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IQbservable(Of Decimal) _
) As IQbservable(Of Decimal)
```

```vb
'Usage
Dim source As IQbservable(Of Decimal)
Dim returnValue As IQbservable(Of Decimal)

returnValue = source.Average()
```

```csharp
public static IQbservable<decimal> Average(
    this IQbservable<decimal> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Decimal>^ Average(
    IQbservable<Decimal>^ source
)
```

```fsharp
static member Average : 
        source:IQbservable<decimal> -> IQbservable<decimal> 
```

```jscript
public static function Average(
    source : IQbservable<decimal>
) : IQbservable<decimal>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>  
  A sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values to calculate the average of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>  
The average of a queryable observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Average Overload](Average/Qbservable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Qbservable.Average Method

Include Protected Members  
Include Inherited Members

Computes the average of a queryable observable sequence.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IQbservable<Decimal>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.average(system.reactive.linq.iqbservable%7bsystem.decimal%7d)(v=VS.103))Computes the average of a queryable observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IQbservable<Double>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.average(system.reactive.linq.iqbservable%7bsystem.double%7d)(v=VS.103))Computes the average of a queryable observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IQbservable<Int32>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.average(system.reactive.linq.iqbservable%7bsystem.int32%7d)(v=VS.103))Computes the average of a queryable observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IQbservable<Int64>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.average(system.reactive.linq.iqbservable%7bsystem.int64%7d)(v=VS.103))Computes the average of a queryable observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IQbservable<Nullable<Decimal>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.average(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.decimal%7d%7d)(v=VS.103))Computes the average of a queryable observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IQbservable<Nullable<Double>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.average(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.double%7d%7d)(v=VS.103))Computes the average of a queryable observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IQbservable<Nullable<Int32>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.average(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.int32%7d%7d)(v=VS.103))Computes the average of a queryable observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IQbservable<Nullable<Int64>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.average(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.int64%7d%7d)(v=VS.103))Computes the average of a queryable observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IQbservable<Nullable<Single>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.average(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.single%7d%7d)(v=VS.103))Computes the average of a queryable observable sequence of nullable Float values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Average(IQbservable<Single>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.average(system.reactive.linq.iqbservable%7bsystem.single%7d)(v=VS.103))Computes the average of a queryable observable sequence of Float values.Top

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)





# Qbservable.Average Method (IQbservable\<Nullable\<Int64\>\>)

Computes the average of a queryable observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IQbservable(Of Nullable(Of Long)) _
) As IQbservable(Of Nullable(Of Double))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Long))
Dim returnValue As IQbservable(Of Nullable(Of Double))

returnValue = source.Average()
```

```csharp
public static IQbservable<Nullable<double>> Average(
    this IQbservable<Nullable<long>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<double>>^ Average(
    IQbservable<Nullable<long long>>^ source
)
```

```fsharp
static member Average : 
        source:IQbservable<Nullable<int64>> -> IQbservable<Nullable<float>> 
```

```jscript
public static function Average(
    source : IQbservable<Nullable<long>>
) : IQbservable<Nullable<double>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>  
  A sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values to calculate the average of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
The average of a queryable observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Average Overload](Average/Qbservable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Qbservable.Average Method (IQbservable\<Nullable\<Double\>\>)

Computes the average of a queryable observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IQbservable(Of Nullable(Of Double)) _
) As IQbservable(Of Nullable(Of Double))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Double))
Dim returnValue As IQbservable(Of Nullable(Of Double))

returnValue = source.Average()
```

```csharp
public static IQbservable<Nullable<double>> Average(
    this IQbservable<Nullable<double>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<double>>^ Average(
    IQbservable<Nullable<double>>^ source
)
```

```fsharp
static member Average : 
        source:IQbservable<Nullable<float>> -> IQbservable<Nullable<float>> 
```

```jscript
public static function Average(
    source : IQbservable<Nullable<double>>
) : IQbservable<Nullable<double>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
  A sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values to calculate the average of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
The average of a queryable observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Average Overload](Average/Qbservable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Qbservable.Average Method (IQbservable\<Int32\>)

Computes the average of a queryable observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IQbservable(Of Integer) _
) As IQbservable(Of Double)
```

```vb
'Usage
Dim source As IQbservable(Of Integer)
Dim returnValue As IQbservable(Of Double)

returnValue = source.Average()
```

```csharp
public static IQbservable<double> Average(
    this IQbservable<int> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<double>^ Average(
    IQbservable<int>^ source
)
```

```fsharp
static member Average : 
        source:IQbservable<int> -> IQbservable<float> 
```

```jscript
public static function Average(
    source : IQbservable<int>
) : IQbservable<double>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
  A sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values to calculate the average of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
The average of a queryable observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Average Overload](Average/Qbservable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Qbservable.Average Method (IQbservable\<Double\>)

Computes the average of a queryable observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IQbservable(Of Double) _
) As IQbservable(Of Double)
```

```vb
'Usage
Dim source As IQbservable(Of Double)
Dim returnValue As IQbservable(Of Double)

returnValue = source.Average()
```

```csharp
public static IQbservable<double> Average(
    this IQbservable<double> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<double>^ Average(
    IQbservable<double>^ source
)
```

```fsharp
static member Average : 
        source:IQbservable<float> -> IQbservable<float> 
```

```jscript
public static function Average(
    source : IQbservable<double>
) : IQbservable<double>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
  A sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values to calculate the average of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
The average of a queryable observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Average Overload](Average/Qbservable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Qbservable.Average Method (IQbservable\<Single\>)

Computes the average of a queryable observable sequence of Float values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IQbservable(Of Single) _
) As IQbservable(Of Single)
```

```vb
'Usage
Dim source As IQbservable(Of Single)
Dim returnValue As IQbservable(Of Single)

returnValue = source.Average()
```

```csharp
public static IQbservable<float> Average(
    this IQbservable<float> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<float>^ Average(
    IQbservable<float>^ source
)
```

```fsharp
static member Average : 
        source:IQbservable<float32> -> IQbservable<float32> 
```

```jscript
public static function Average(
    source : IQbservable<float>
) : IQbservable<float>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>  
  A sequence of Float values to calculate the average of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>  
The average of a queryable observable sequence of Float values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Average Overload](Average/Qbservable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Qbservable.Average Method (IQbservable\<Int64\>)

Computes the average of a queryable observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Average ( _
    source As IQbservable(Of Long) _
) As IQbservable(Of Double)
```

```vb
'Usage
Dim source As IQbservable(Of Long)
Dim returnValue As IQbservable(Of Double)

returnValue = source.Average()
```

```csharp
public static IQbservable<double> Average(
    this IQbservable<long> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<double>^ Average(
    IQbservable<long long>^ source
)
```

```fsharp
static member Average : 
        source:IQbservable<int64> -> IQbservable<float> 
```

```jscript
public static function Average(
    source : IQbservable<long>
) : IQbservable<double>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
  A sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values to calculate the average of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
The average of a queryable observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Average Overload](Average/Qbservable.Average)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)







