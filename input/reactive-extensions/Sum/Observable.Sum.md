# Observable.Sum Method (IObservable\<Nullable\<Single\>\>)

Computes the sum of a sequence of nullable Float values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Sum ( _
    source As IObservable(Of Nullable(Of Single)) _
) As IObservable(Of Nullable(Of Single))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Single))
Dim returnValue As IObservable(Of Nullable(Of Single))

returnValue = source.Sum()
```

```csharp
public static IObservable<Nullable<float>> Sum(
    this IObservable<Nullable<float>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<float>>^ Sum(
    IObservable<Nullable<float>>^ source
)
```

```fsharp
static member Sum : 
        source:IObservable<Nullable<float32>> -> IObservable<Nullable<float32>> 
```

```jscript
public static function Sum(
    source : IObservable<Nullable<float>>
) : IObservable<Nullable<float>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>  
  A sequence of nullable Float values to calculate the sum of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>  
The sum of a sequence of nullable Float values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Sum Overload](Sum\Observable.Sum.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Sum Method (IObservable\<Nullable\<Int32\>\>)

Computes the sum of a sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Sum ( _
    source As IObservable(Of Nullable(Of Integer)) _
) As IObservable(Of Nullable(Of Integer))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Integer))
Dim returnValue As IObservable(Of Nullable(Of Integer))

returnValue = source.Sum()
```

```csharp
public static IObservable<Nullable<int>> Sum(
    this IObservable<Nullable<int>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<int>>^ Sum(
    IObservable<Nullable<int>>^ source
)
```

```fsharp
static member Sum : 
        source:IObservable<Nullable<int>> -> IObservable<Nullable<int>> 
```

```jscript
public static function Sum(
    source : IObservable<Nullable<int>>
) : IObservable<Nullable<int>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>  
  A sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values to calculate the sum of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>  
The sum of a sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Sum Overload](Sum\Observable.Sum.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Sum Method (IObservable\<Nullable\<Double\>\>)

Computes the sum of a sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Sum ( _
    source As IObservable(Of Nullable(Of Double)) _
) As IObservable(Of Nullable(Of Double))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Double))
Dim returnValue As IObservable(Of Nullable(Of Double))

returnValue = source.Sum()
```

```csharp
public static IObservable<Nullable<double>> Sum(
    this IObservable<Nullable<double>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<double>>^ Sum(
    IObservable<Nullable<double>>^ source
)
```

```fsharp
static member Sum : 
        source:IObservable<Nullable<float>> -> IObservable<Nullable<float>> 
```

```jscript
public static function Sum(
    source : IObservable<Nullable<double>>
) : IObservable<Nullable<double>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
  A sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values to calculate the sum of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>  
The sum of a sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Sum Overload](Sum\Observable.Sum.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Sum Method (IObservable\<Nullable\<Decimal\>\>)

Computes the sum of a sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Sum ( _
    source As IObservable(Of Nullable(Of Decimal)) _
) As IObservable(Of Nullable(Of Decimal))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Decimal))
Dim returnValue As IObservable(Of Nullable(Of Decimal))

returnValue = source.Sum()
```

```csharp
public static IObservable<Nullable<decimal>> Sum(
    this IObservable<Nullable<decimal>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<Decimal>>^ Sum(
    IObservable<Nullable<Decimal>>^ source
)
```

```fsharp
static member Sum : 
        source:IObservable<Nullable<decimal>> -> IObservable<Nullable<decimal>> 
```

```jscript
public static function Sum(
    source : IObservable<Nullable<decimal>>
) : IObservable<Nullable<decimal>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>  
  A sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values to calculate the sum of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>  
The sum of a sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Sum Overload](Sum\Observable.Sum.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Sum Method (IObservable\<Nullable\<Int64\>\>)

Computes the sum of a sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Sum ( _
    source As IObservable(Of Nullable(Of Long)) _
) As IObservable(Of Nullable(Of Long))
```

```vb
'Usage
Dim source As IObservable(Of Nullable(Of Long))
Dim returnValue As IObservable(Of Nullable(Of Long))

returnValue = source.Sum()
```

```csharp
public static IObservable<Nullable<long>> Sum(
    this IObservable<Nullable<long>> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Nullable<long long>>^ Sum(
    IObservable<Nullable<long long>>^ source
)
```

```fsharp
static member Sum : 
        source:IObservable<Nullable<int64>> -> IObservable<Nullable<int64>> 
```

```jscript
public static function Sum(
    source : IObservable<Nullable<long>>
) : IObservable<Nullable<long>>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>  
  A sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values to calculate the sum of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>  
The sum of a sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Nullable](https://msdn.microsoft.com/en-us/library/b3h38hb0)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Sum Overload](Sum\Observable.Sum.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Sum Method (IObservable\<Single\>)

Computes the sum of a sequence of Float values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Sum ( _
    source As IObservable(Of Single) _
) As IObservable(Of Single)
```

```vb
'Usage
Dim source As IObservable(Of Single)
Dim returnValue As IObservable(Of Single)

returnValue = source.Sum()
```

```csharp
public static IObservable<float> Sum(
    this IObservable<float> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<float>^ Sum(
    IObservable<float>^ source
)
```

```fsharp
static member Sum : 
        source:IObservable<float32> -> IObservable<float32> 
```

```jscript
public static function Sum(
    source : IObservable<float>
) : IObservable<float>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>  
  A sequence of Float values to calculate the sum of

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>  
The sum of a sequence of Float values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Single](https://msdn.microsoft.com/en-us/library/3www918f)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Sum Overload](Sum\Observable.Sum.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Sum Method (IObservable\<Decimal\>)

Computes the sum of a sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Sum ( _
    source As IObservable(Of Decimal) _
) As IObservable(Of Decimal)
```

```vb
'Usage
Dim source As IObservable(Of Decimal)
Dim returnValue As IObservable(Of Decimal)

returnValue = source.Sum()
```

```csharp
public static IObservable<decimal> Sum(
    this IObservable<decimal> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Decimal>^ Sum(
    IObservable<Decimal>^ source
)
```

```fsharp
static member Sum : 
        source:IObservable<decimal> -> IObservable<decimal> 
```

```jscript
public static function Sum(
    source : IObservable<decimal>
) : IObservable<decimal>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>  
  A sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values to calculate the sum of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>  
The sum of a sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Sum Overload](Sum\Observable.Sum.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Sum Method (IObservable\<Int64\>)

Computes the sum of a sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Sum ( _
    source As IObservable(Of Long) _
) As IObservable(Of Long)
```

```vb
'Usage
Dim source As IObservable(Of Long)
Dim returnValue As IObservable(Of Long)

returnValue = source.Sum()
```

```csharp
public static IObservable<long> Sum(
    this IObservable<long> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<long long>^ Sum(
    IObservable<long long>^ source
)
```

```fsharp
static member Sum : 
        source:IObservable<int64> -> IObservable<int64> 
```

```jscript
public static function Sum(
    source : IObservable<long>
) : IObservable<long>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
  A sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values to calculate the sum of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
The sum of a sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Sum Overload](Sum\Observable.Sum.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Sum Method (IObservable\<Int32\>)

Computes the sum of a sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Sum ( _
    source As IObservable(Of Integer) _
) As IObservable(Of Integer)
```

```vb
'Usage
Dim source As IObservable(Of Integer)
Dim returnValue As IObservable(Of Integer)

returnValue = source.Sum()
```

```csharp
public static IObservable<int> Sum(
    this IObservable<int> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<int>^ Sum(
    IObservable<int>^ source
)
```

```fsharp
static member Sum : 
        source:IObservable<int> -> IObservable<int> 
```

```jscript
public static function Sum(
    source : IObservable<int>
) : IObservable<int>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
  A sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values to calculate the sum of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
The sum of a sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Sum Overload](Sum\Observable.Sum.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Sum Method

Include Protected Members  
Include Inherited Members

Computes the sum of a sequence.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Sum(IObservable<Decimal>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.sum(system.iobservable%7bsystem.decimal%7d)(v=VS.103))Computes the sum of a sequence of [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Sum(IObservable<Double>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.sum(system.iobservable%7bsystem.double%7d)(v=VS.103))Computes the sum of a sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Sum(IObservable<Int32>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.sum(system.iobservable%7bsystem.int32%7d)(v=VS.103))Computes the sum of a sequence of [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Sum(IObservable<Int64>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.sum(system.iobservable%7bsystem.int64%7d)(v=VS.103))Computes the sum of a sequence of [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Sum(IObservable<Nullable<Decimal>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.sum(system.iobservable%7bsystem.nullable%7bsystem.decimal%7d%7d)(v=VS.103))Computes the sum of a sequence of nullable [Decimal](https://msdn.microsoft.com/en-us/library/1k2e8atx) values.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Sum(IObservable<Nullable<Double>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.sum(system.iobservable%7bsystem.nullable%7bsystem.double%7d%7d)(v=VS.103))Computes the sum of a sequence of nullable [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Sum(IObservable<Nullable<Int32>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.sum(system.iobservable%7bsystem.nullable%7bsystem.int32%7d%7d)(v=VS.103))Computes the sum of a sequence of nullable [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) values.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Sum(IObservable<Nullable<Int64>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.sum(system.iobservable%7bsystem.nullable%7bsystem.int64%7d%7d)(v=VS.103))Computes the sum of a sequence of nullable [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) values.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Sum(IObservable<Nullable<Single>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.sum(system.iobservable%7bsystem.nullable%7bsystem.single%7d%7d)(v=VS.103))Computes the sum of a sequence of nullable Float values.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Sum(IObservable<Single>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.sum(system.iobservable%7bsystem.single%7d)(v=VS.103))Computes the sum of a sequence of Float values.Top

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Sum Method (IObservable\<Double\>)

Computes the sum of a sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Sum ( _
    source As IObservable(Of Double) _
) As IObservable(Of Double)
```

```vb
'Usage
Dim source As IObservable(Of Double)
Dim returnValue As IObservable(Of Double)

returnValue = source.Sum()
```

```csharp
public static IObservable<double> Sum(
    this IObservable<double> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<double>^ Sum(
    IObservable<double>^ source
)
```

```fsharp
static member Sum : 
        source:IObservable<float> -> IObservable<float> 
```

```jscript
public static function Sum(
    source : IObservable<double>
) : IObservable<double>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
  A sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values to calculate the sum of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>  
The sum of a sequence of [Double](https://msdn.microsoft.com/en-us/library/643eft0t) values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Double](https://msdn.microsoft.com/en-us/library/643eft0t)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Sum Overload](Sum\Observable.Sum.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
