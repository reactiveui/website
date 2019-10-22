title: Qbservable.Min(IQbservable<Double>)
---
# Qbservable.Min Method (IQbservable\<Double\>)

Returns the minimum value in a queryable observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IQbservable(Of Double) _
) As IQbservable(Of Double)
```

```vb
'Usage
Dim source As IQbservable(Of Double)
Dim returnValue As IQbservable(Of Double)

returnValue = source.Min()
```

```csharp
public static IQbservable<double> Min(
    this IQbservable<double> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<double>^ Min(
    IQbservable<double>^ source
)
```

```fsharp
static member Min : 
        source:IQbservable<float> -> IQbservable<float> 
```

```jscript
public static function Min(
    source : IQbservable<double>
) : IQbservable<double>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
  A sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values to determine the minimum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
The minimum value in a queryable observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Min Overload](Min\Qbservable.Min.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Qbservable.Min Method (IQbservable\<Single\>)

Returns the minimum value in a queryable observable sequence of Float values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IQbservable(Of Single) _
) As IQbservable(Of Single)
```

```vb
'Usage
Dim source As IQbservable(Of Single)
Dim returnValue As IQbservable(Of Single)

returnValue = source.Min()
```

```csharp
public static IQbservable<float> Min(
    this IQbservable<float> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<float>^ Min(
    IQbservable<float>^ source
)
```

```fsharp
static member Min : 
        source:IQbservable<float32> -> IQbservable<float32> 
```

```jscript
public static function Min(
    source : IQbservable<float>
) : IQbservable<float>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>  
  A sequence of Float values to determine the minimum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>  
The minimum value in a queryable observable sequence of Float values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Min Overload](Min\Qbservable.Min.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Qbservable.Min Method (IQbservable\<Nullable\<Single\>\>)

Returns the minimum value in a queryable observable sequence of nullable Float values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IQbservable(Of Nullable(Of Single)) _
) As IQbservable(Of Nullable(Of Single))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Single))
Dim returnValue As IQbservable(Of Nullable(Of Single))

returnValue = source.Min()
```

```csharp
public static IQbservable<Nullable<float>> Min(
    this IQbservable<Nullable<float>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<float>>^ Min(
    IQbservable<Nullable<float>>^ source
)
```

```fsharp
static member Min : 
        source:IQbservable<Nullable<float32>> -> IQbservable<Nullable<float32>> 
```

```jscript
public static function Min(
    source : IQbservable<Nullable<float>>
) : IQbservable<Nullable<float>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>  
  A sequence of nullable Float values to determine the minimum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>  
The minimum value in a queryable observable sequence of nullable Float values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Min Overload](Min\Qbservable.Min.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Qbservable.Min Method (IQbservable\<Decimal\>)

Returns the minimum value in a queryable observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IQbservable(Of Decimal) _
) As IQbservable(Of Decimal)
```

```vb
'Usage
Dim source As IQbservable(Of Decimal)
Dim returnValue As IQbservable(Of Decimal)

returnValue = source.Min()
```

```csharp
public static IQbservable<decimal> Min(
    this IQbservable<decimal> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Decimal>^ Min(
    IQbservable<Decimal>^ source
)
```

```fsharp
static member Min : 
        source:IQbservable<decimal> -> IQbservable<decimal> 
```

```jscript
public static function Min(
    source : IQbservable<decimal>
) : IQbservable<decimal>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>  
  A sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values to determine the minimum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>  
The minimum value in a queryable observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Min Overload](Min\Qbservable.Min.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Qbservable.Min Method (IQbservable\<Nullable\<Int32\>\>)

Returns the minimum value in a queryable observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IQbservable(Of Nullable(Of Integer)) _
) As IQbservable(Of Nullable(Of Integer))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Integer))
Dim returnValue As IQbservable(Of Nullable(Of Integer))

returnValue = source.Min()
```

```csharp
public static IQbservable<Nullable<int>> Min(
    this IQbservable<Nullable<int>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<int>>^ Min(
    IQbservable<Nullable<int>>^ source
)
```

```fsharp
static member Min : 
        source:IQbservable<Nullable<int>> -> IQbservable<Nullable<int>> 
```

```jscript
public static function Min(
    source : IQbservable<Nullable<int>>
) : IQbservable<Nullable<int>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>  
  A sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values to determine the minimum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>  
The minimum value in a queryable observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Min Overload](Min\Qbservable.Min.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Qbservable.Min Method (IQbservable\<Nullable\<Decimal\>\>)

Returns the minimum value in a queryable observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IQbservable(Of Nullable(Of Decimal)) _
) As IQbservable(Of Nullable(Of Decimal))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Decimal))
Dim returnValue As IQbservable(Of Nullable(Of Decimal))

returnValue = source.Min()
```

```csharp
public static IQbservable<Nullable<decimal>> Min(
    this IQbservable<Nullable<decimal>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<Decimal>>^ Min(
    IQbservable<Nullable<Decimal>>^ source
)
```

```fsharp
static member Min : 
        source:IQbservable<Nullable<decimal>> -> IQbservable<Nullable<decimal>> 
```

```jscript
public static function Min(
    source : IQbservable<Nullable<decimal>>
) : IQbservable<Nullable<decimal>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>  
  A sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values to determine the minimum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>  
The minimum value in a queryable observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Min Overload](Min\Qbservable.Min.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Qbservable.Min Method (IQbservable\<Nullable\<Double\>\>)

Returns the minimum value in a queryable observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IQbservable(Of Nullable(Of Double)) _
) As IQbservable(Of Nullable(Of Double))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Double))
Dim returnValue As IQbservable(Of Nullable(Of Double))

returnValue = source.Min()
```

```csharp
public static IQbservable<Nullable<double>> Min(
    this IQbservable<Nullable<double>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<double>>^ Min(
    IQbservable<Nullable<double>>^ source
)
```

```fsharp
static member Min : 
        source:IQbservable<Nullable<float>> -> IQbservable<Nullable<float>> 
```

```jscript
public static function Min(
    source : IQbservable<Nullable<double>>
) : IQbservable<Nullable<double>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
  A sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values to determine the minimum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
The minimum value in a queryable observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Min Overload](Min\Qbservable.Min.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Qbservable.Min Method

Include Protected Members  
Include Inherited Members

Returns the minimum value.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IQbservable<Decimal>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.min(system.reactive.linq.iqbservable%7bsystem.decimal%7d)(v=VS.103))Returns the minimum value in a queryable observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IQbservable<Double>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.min(system.reactive.linq.iqbservable%7bsystem.double%7d)(v=VS.103))Returns the minimum value in a queryable observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IQbservable<Int32>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.min(system.reactive.linq.iqbservable%7bsystem.int32%7d)(v=VS.103))Returns the minimum value in a queryable observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IQbservable<Int64>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.min(system.reactive.linq.iqbservable%7bsystem.int64%7d)(v=VS.103))Returns the minimum value in a queryable observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IQbservable<Nullable<Decimal>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.min(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.decimal%7d%7d)(v=VS.103))Returns the minimum value in a queryable observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IQbservable<Nullable<Double>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.min(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.double%7d%7d)(v=VS.103))Returns the minimum value in a queryable observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IQbservable<Nullable<Int32>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.min(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.int32%7d%7d)(v=VS.103))Returns the minimum value in a queryable observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IQbservable<Nullable<Int64>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.min(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.int64%7d%7d)(v=VS.103))Returns the minimum value in a queryable observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IQbservable<Nullable<Single>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.min(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.single%7d%7d)(v=VS.103))Returns the minimum value in a queryable observable sequence of nullable Float values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min(IQbservable<Single>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.min(system.reactive.linq.iqbservable%7bsystem.single%7d)(v=VS.103))Returns the minimum value in a queryable observable sequence of Float values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min<TSource>(IQbservable<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.min%60%601(system.reactive.linq.iqbservable%7b%60%600%7d)(v=VS.103))Returns the minimum element in a queryable observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Min<TSource>(IQbservable<TSource>, IComparer<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.min%60%601(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.collections.generic.icomparer%7b%60%600%7d)(v=VS.103))Returns the minimum value in a queryable observable sequence according to the specified comparer.Top

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)





# Qbservable.Min Method (IQbservable\<Nullable\<Int64\>\>)

Returns the minimum value in a queryable observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IQbservable(Of Nullable(Of Long)) _
) As IQbservable(Of Nullable(Of Long))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Long))
Dim returnValue As IQbservable(Of Nullable(Of Long))

returnValue = source.Min()
```

```csharp
public static IQbservable<Nullable<long>> Min(
    this IQbservable<Nullable<long>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<long long>>^ Min(
    IQbservable<Nullable<long long>>^ source
)
```

```fsharp
static member Min : 
        source:IQbservable<Nullable<int64>> -> IQbservable<Nullable<int64>> 
```

```jscript
public static function Min(
    source : IQbservable<Nullable<long>>
) : IQbservable<Nullable<long>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>  
  A sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values to determine the minimum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>  
The minimum value in a queryable observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Min Overload](Min\Qbservable.Min.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Qbservable.Min Method (IQbservable\<Int64\>)

Returns the minimum value in a queryable observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IQbservable(Of Long) _
) As IQbservable(Of Long)
```

```vb
'Usage
Dim source As IQbservable(Of Long)
Dim returnValue As IQbservable(Of Long)

returnValue = source.Min()
```

```csharp
public static IQbservable<long> Min(
    this IQbservable<long> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<long long>^ Min(
    IQbservable<long long>^ source
)
```

```fsharp
static member Min : 
        source:IQbservable<int64> -> IQbservable<int64> 
```

```jscript
public static function Min(
    source : IQbservable<long>
) : IQbservable<long>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
  A sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values to determine the minimum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
The minimum value in a queryable observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Min Overload](Min\Qbservable.Min.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Qbservable.Min Method (IQbservable\<Int32\>)

Returns the minimum value in a queryable observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min ( _
    source As IQbservable(Of Integer) _
) As IQbservable(Of Integer)
```

```vb
'Usage
Dim source As IQbservable(Of Integer)
Dim returnValue As IQbservable(Of Integer)

returnValue = source.Min()
```

```csharp
public static IQbservable<int> Min(
    this IQbservable<int> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<int>^ Min(
    IQbservable<int>^ source
)
```

```fsharp
static member Min : 
        source:IQbservable<int> -> IQbservable<int> 
```

```jscript
public static function Min(
    source : IQbservable<int>
) : IQbservable<int>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
  A sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values to determine the minimum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
The minimum value in a queryable observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Min Overload](Min\Qbservable.Min.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)







