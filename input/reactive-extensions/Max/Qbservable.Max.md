title: Qbservable.Max(IQbservable<Int64>)
---
# Qbservable.Max Method (IQbservable\<Int64\>)

Returns the maximum value in a queryable observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Max ( _
    source As IQbservable(Of Long) _
) As IQbservable(Of Long)
```

```vb
'Usage
Dim source As IQbservable(Of Long)
Dim returnValue As IQbservable(Of Long)

returnValue = source.Max()
```

```csharp
public static IQbservable<long> Max(
    this IQbservable<long> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<long long>^ Max(
    IQbservable<long long>^ source
)
```

```fsharp
static member Max : 
        source:IQbservable<int64> -> IQbservable<int64> 
```

```jscript
public static function Max(
    source : IQbservable<long>
) : IQbservable<long>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
  A sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values to determine the maximum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
The maximum value in a queryable observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Max Overload](Max\Qbservable.Max.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Max Method (IQbservable\<Int32\>)

Returns the maximum value in a queryable observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Max ( _
    source As IQbservable(Of Integer) _
) As IQbservable(Of Integer)
```

```vb
'Usage
Dim source As IQbservable(Of Integer)
Dim returnValue As IQbservable(Of Integer)

returnValue = source.Max()
```

```csharp
public static IQbservable<int> Max(
    this IQbservable<int> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<int>^ Max(
    IQbservable<int>^ source
)
```

```fsharp
static member Max : 
        source:IQbservable<int> -> IQbservable<int> 
```

```jscript
public static function Max(
    source : IQbservable<int>
) : IQbservable<int>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
  A sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values to determine the maximum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
The maximum value in a queryable observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Max Overload](Max\Qbservable.Max.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Max Method (IQbservable\<Single\>)

Returns the maximum value in a queryable observable sequence of Float values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Max ( _
    source As IQbservable(Of Single) _
) As IQbservable(Of Single)
```

```vb
'Usage
Dim source As IQbservable(Of Single)
Dim returnValue As IQbservable(Of Single)

returnValue = source.Max()
```

```csharp
public static IQbservable<float> Max(
    this IQbservable<float> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<float>^ Max(
    IQbservable<float>^ source
)
```

```fsharp
static member Max : 
        source:IQbservable<float32> -> IQbservable<float32> 
```

```jscript
public static function Max(
    source : IQbservable<float>
) : IQbservable<float>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>  
  A sequence of Float values to determine the maximum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>  
The maximum value in a queryable observable sequence of Float values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Max Overload](Max\Qbservable.Max.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Max Method (IQbservable\<Nullable\<Double\>\>)

Returns the maximum value in a queryable observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Max ( _
    source As IQbservable(Of Nullable(Of Double)) _
) As IQbservable(Of Nullable(Of Double))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Double))
Dim returnValue As IQbservable(Of Nullable(Of Double))

returnValue = source.Max()
```

```csharp
public static IQbservable<Nullable<double>> Max(
    this IQbservable<Nullable<double>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<double>>^ Max(
    IQbservable<Nullable<double>>^ source
)
```

```fsharp
static member Max : 
        source:IQbservable<Nullable<float>> -> IQbservable<Nullable<float>> 
```

```jscript
public static function Max(
    source : IQbservable<Nullable<double>>
) : IQbservable<Nullable<double>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
  A sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values to determine the maximum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
The maximum value in a queryable observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Max Overload](Max\Qbservable.Max.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Max Method (IQbservable\<Nullable\<Int64\>\>)

Returns the maximum value in a queryable observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Max ( _
    source As IQbservable(Of Nullable(Of Long)) _
) As IQbservable(Of Nullable(Of Long))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Long))
Dim returnValue As IQbservable(Of Nullable(Of Long))

returnValue = source.Max()
```

```csharp
public static IQbservable<Nullable<long>> Max(
    this IQbservable<Nullable<long>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<long long>>^ Max(
    IQbservable<Nullable<long long>>^ source
)
```

```fsharp
static member Max : 
        source:IQbservable<Nullable<int64>> -> IQbservable<Nullable<int64>> 
```

```jscript
public static function Max(
    source : IQbservable<Nullable<long>>
) : IQbservable<Nullable<long>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>  
  A sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values to determine the maximum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>  
The maximum value in a queryable observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Max Overload](Max\Qbservable.Max.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Max Method

Include Protected Members  
Include Inherited Members

Returns the maximum value.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Max(IQbservable<Decimal>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.max(system.reactive.linq.iqbservable%7bsystem.decimal%7d)(v=VS.103))Returns the maximum value in a queryable observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Max(IQbservable<Double>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.max(system.reactive.linq.iqbservable%7bsystem.double%7d)(v=VS.103))Returns the maximum value in a queryable observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Max(IQbservable<Int32>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.max(system.reactive.linq.iqbservable%7bsystem.int32%7d)(v=VS.103))Returns the maximum value in a queryable observable sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Max(IQbservable<Int64>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.max(system.reactive.linq.iqbservable%7bsystem.int64%7d)(v=VS.103))Returns the maximum value in a queryable observable sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Max(IQbservable<Nullable<Decimal>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.max(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.decimal%7d%7d)(v=VS.103))Returns the maximum value in a queryable observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Max(IQbservable<Nullable<Double>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.max(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.double%7d%7d)(v=VS.103))Returns the maximum value in a queryable observable sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Max(IQbservable<Nullable<Int32>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.max(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.int32%7d%7d)(v=VS.103))Returns the maximum value in a queryable observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Max(IQbservable<Nullable<Int64>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.max(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.int64%7d%7d)(v=VS.103))Returns the maximum value in a queryable observable sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Max(IQbservable<Nullable<Single>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.max(system.reactive.linq.iqbservable%7bsystem.nullable%7bsystem.single%7d%7d)(v=VS.103))Returns the maximum value in a queryable observable sequence of nullable Float values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Max(IQbservable<Single>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.max(system.reactive.linq.iqbservable%7bsystem.single%7d)(v=VS.103))Returns the maximum value in a queryable observable sequence of Float values.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Max<TSource>(IQbservable<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.max%60%601(system.reactive.linq.iqbservable%7b%60%600%7d)(v=VS.103))Returns the maximum element in a queryable observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Max<TSource>(IQbservable<TSource>, IComparer<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.max%60%601(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.collections.generic.icomparer%7b%60%600%7d)(v=VS.103))Returns the maximum value in a queryable observable sequence according to the specified comparer.Top

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Max Method (IQbservable\<Nullable\<Single\>\>)

Returns the maximum value in a queryable observable sequence of nullable Float values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Max ( _
    source As IQbservable(Of Nullable(Of Single)) _
) As IQbservable(Of Nullable(Of Single))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Single))
Dim returnValue As IQbservable(Of Nullable(Of Single))

returnValue = source.Max()
```

```csharp
public static IQbservable<Nullable<float>> Max(
    this IQbservable<Nullable<float>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<float>>^ Max(
    IQbservable<Nullable<float>>^ source
)
```

```fsharp
static member Max : 
        source:IQbservable<Nullable<float32>> -> IQbservable<Nullable<float32>> 
```

```jscript
public static function Max(
    source : IQbservable<Nullable<float>>
) : IQbservable<Nullable<float>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>  
  A sequence of nullable Float values to determine the maximum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>  
The maximum value in a queryable observable sequence of nullable Float values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Max Overload](Max\Qbservable.Max.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Max Method (IQbservable\<Double\>)

Returns the maximum value in a queryable observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Max ( _
    source As IQbservable(Of Double) _
) As IQbservable(Of Double)
```

```vb
'Usage
Dim source As IQbservable(Of Double)
Dim returnValue As IQbservable(Of Double)

returnValue = source.Max()
```

```csharp
public static IQbservable<double> Max(
    this IQbservable<double> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<double>^ Max(
    IQbservable<double>^ source
)
```

```fsharp
static member Max : 
        source:IQbservable<float> -> IQbservable<float> 
```

```jscript
public static function Max(
    source : IQbservable<double>
) : IQbservable<double>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
  A sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values to determine the maximum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
The maximum value in a queryable observable sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Max Overload](Max\Qbservable.Max.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Max Method (IQbservable\<Nullable\<Int32\>\>)

Returns the maximum value in a queryable observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Max ( _
    source As IQbservable(Of Nullable(Of Integer)) _
) As IQbservable(Of Nullable(Of Integer))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Integer))
Dim returnValue As IQbservable(Of Nullable(Of Integer))

returnValue = source.Max()
```

```csharp
public static IQbservable<Nullable<int>> Max(
    this IQbservable<Nullable<int>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<int>>^ Max(
    IQbservable<Nullable<int>>^ source
)
```

```fsharp
static member Max : 
        source:IQbservable<Nullable<int>> -> IQbservable<Nullable<int>> 
```

```jscript
public static function Max(
    source : IQbservable<Nullable<int>>
) : IQbservable<Nullable<int>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>  
  A sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values to determine the maximum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>  
The maximum value in a queryable observable sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Max Overload](Max\Qbservable.Max.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Max Method (IQbservable\<Decimal\>)

Returns the maximum value in a queryable observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Max ( _
    source As IQbservable(Of Decimal) _
) As IQbservable(Of Decimal)
```

```vb
'Usage
Dim source As IQbservable(Of Decimal)
Dim returnValue As IQbservable(Of Decimal)

returnValue = source.Max()
```

```csharp
public static IQbservable<decimal> Max(
    this IQbservable<decimal> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Decimal>^ Max(
    IQbservable<Decimal>^ source
)
```

```fsharp
static member Max : 
        source:IQbservable<decimal> -> IQbservable<decimal> 
```

```jscript
public static function Max(
    source : IQbservable<decimal>
) : IQbservable<decimal>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>  
  A sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values to determine the maximum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>  
The maximum value in a queryable observable sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Max Overload](Max\Qbservable.Max.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Max Method (IQbservable\<Nullable\<Decimal\>\>)

Returns the maximum value in a queryable observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Max ( _
    source As IQbservable(Of Nullable(Of Decimal)) _
) As IQbservable(Of Nullable(Of Decimal))
```

```vb
'Usage
Dim source As IQbservable(Of Nullable(Of Decimal))
Dim returnValue As IQbservable(Of Nullable(Of Decimal))

returnValue = source.Max()
```

```csharp
public static IQbservable<Nullable<decimal>> Max(
    this IQbservable<Nullable<decimal>> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Nullable<Decimal>>^ Max(
    IQbservable<Nullable<Decimal>>^ source
)
```

```fsharp
static member Max : 
        source:IQbservable<Nullable<decimal>> -> IQbservable<Nullable<decimal>> 
```

```jscript
public static function Max(
    source : IQbservable<Nullable<decimal>>
) : IQbservable<Nullable<decimal>>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>  
  A sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values to determine the maximum value of.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>  
The maximum value in a queryable observable sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Max Overload](Max\Qbservable.Max.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
