# ReactiveAssert.AreElementsEqual\<T\> Method (IObservable\<T\>, IObservable\<T\>, String)

Asserts that both observable sequences have equal length and equal elements.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Shared Sub AreElementsEqual(Of T) ( _
    expected As IObservable(Of T), _
    actual As IObservable(Of T), _
    message As String _
)
```

```vb
'Usage
Dim expected As IObservable(Of T)
Dim actual As IObservable(Of T)
Dim message As String

ReactiveAssert.AreElementsEqual(expected, _
    actual, message)
```

```csharp
public static void AreElementsEqual<T>(
    IObservable<T> expected,
    IObservable<T> actual,
    string message
)
```

```c++
public:
generic<typename T>
static void AreElementsEqual(
    IObservable<T>^ expected, 
    IObservable<T>^ actual, 
    String^ message
)
```

```fsharp
static member AreElementsEqual : 
        expected:IObservable<'T> * 
        actual:IObservable<'T> * 
        message:string -> unit 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T  
  The type of the elements.

#### Parameters

- expected  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<T\>  
  The expected sequence.

- actual  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<T\>  
  The actual sequence to compare against the expected one.

- message  
  Type: [System.String](https://msdn.microsoft.com/en-us/library/s1wwdcbf)  
  The error message for assert failure.

## See Also

#### Reference

[ReactiveAssert Class](ReactiveAssert\ReactiveAssert.md)

[AreElementsEqual Overload](AreElementsEqual\ReactiveAssert.AreElementsEqual.md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)







# ReactiveAssert.AreElementsEqual\<T\> Method (IEnumerable\<T\>, IEnumerable\<T\>)

Asserts that both enumerable sequences have equal length and equal elements.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Shared Sub AreElementsEqual(Of T) ( _
    expected As IEnumerable(Of T), _
    actual As IEnumerable(Of T) _
)
```

```vb
'Usage
Dim expected As IEnumerable(Of T)
Dim actual As IEnumerable(Of T)

ReactiveAssert.AreElementsEqual(expected, _
    actual)
```

```csharp
public static void AreElementsEqual<T>(
    IEnumerable<T> expected,
    IEnumerable<T> actual
)
```

```c++
public:
generic<typename T>
static void AreElementsEqual(
    IEnumerable<T>^ expected, 
    IEnumerable<T>^ actual
)
```

```fsharp
static member AreElementsEqual : 
        expected:IEnumerable<'T> * 
        actual:IEnumerable<'T> -> unit 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T  
  The type of the elements.

#### Parameters

- expected  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<T\>  
  The expected sequence.

- actual  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<T\>  
  The actual sequence to compare against the expected one.

## See Also

#### Reference

[ReactiveAssert Class](ReactiveAssert\ReactiveAssert.md)

[AreElementsEqual Overload](AreElementsEqual\ReactiveAssert.AreElementsEqual.md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)







# ReactiveAssert.AreElementsEqual\<T\> Method (IObservable\<T\>, IObservable\<T\>)

Asserts that both observable sequences have equal length and equal elements.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Shared Sub AreElementsEqual(Of T) ( _
    expected As IObservable(Of T), _
    actual As IObservable(Of T) _
)
```

```vb
'Usage
Dim expected As IObservable(Of T)
Dim actual As IObservable(Of T)

ReactiveAssert.AreElementsEqual(expected, _
    actual)
```

```csharp
public static void AreElementsEqual<T>(
    IObservable<T> expected,
    IObservable<T> actual
)
```

```c++
public:
generic<typename T>
static void AreElementsEqual(
    IObservable<T>^ expected, 
    IObservable<T>^ actual
)
```

```fsharp
static member AreElementsEqual : 
        expected:IObservable<'T> * 
        actual:IObservable<'T> -> unit 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T  
  The type of the elements.

#### Parameters

- expected  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<T\>  
  The expected sequence.

- actual  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<T\>  
  The actual sequence to compare against the expected one.

## See Also

#### Reference

[ReactiveAssert Class](ReactiveAssert\ReactiveAssert.md)

[AreElementsEqual Overload](AreElementsEqual\ReactiveAssert.AreElementsEqual.md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)







# ReactiveAssert.AreElementsEqual\<T\> Method (IEnumerable\<T\>, IEnumerable\<T\>, String)

Asserts that both enumerable sequences have equal length and equal elements.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Shared Sub AreElementsEqual(Of T) ( _
    expected As IEnumerable(Of T), _
    actual As IEnumerable(Of T), _
    message As String _
)
```

```vb
'Usage
Dim expected As IEnumerable(Of T)
Dim actual As IEnumerable(Of T)
Dim message As String

ReactiveAssert.AreElementsEqual(expected, _
    actual, message)
```

```csharp
public static void AreElementsEqual<T>(
    IEnumerable<T> expected,
    IEnumerable<T> actual,
    string message
)
```

```c++
public:
generic<typename T>
static void AreElementsEqual(
    IEnumerable<T>^ expected, 
    IEnumerable<T>^ actual, 
    String^ message
)
```

```fsharp
static member AreElementsEqual : 
        expected:IEnumerable<'T> * 
        actual:IEnumerable<'T> * 
        message:string -> unit 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T  
  The type of the elements.

#### Parameters

- expected  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<T\>  
  The expected sequence.

- actual  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<T\>  
  The actual sequence to compare against the expected one.

- message  
  Type: [System.String](https://msdn.microsoft.com/en-us/library/s1wwdcbf)  
  The error message for assert failure.

## See Also

#### Reference

[ReactiveAssert Class](ReactiveAssert\ReactiveAssert.md)

[AreElementsEqual Overload](AreElementsEqual\ReactiveAssert.AreElementsEqual.md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)






