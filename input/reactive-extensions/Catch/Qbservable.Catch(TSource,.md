title: Qbservable.Catch<TSource, TException>(IQbservable<TSource>, Expression<Func<TException, IObservable<TSource>>>)
---
# Qbservable.Catch\<TSource, TException\> Method (IQbservable\<TSource\>, Expression\<Func\<TException, IObservable\<TSource\>\>\>)

Continues a queryable observable sequence that is terminated by an exception of the specified type with the queryable observable sequence produced by the handler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Catch(Of TSource, TException) ( _
    source As IQbservable(Of TSource), _
    handler As Expression(Of Func(Of TException, IObservable(Of TSource))) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim handler As Expression(Of Func(Of TException, IObservable(Of TSource)))
Dim returnValue As IQbservable(Of TSource)

returnValue = source.Catch(handler)
```

```csharp
public static IQbservable<TSource> Catch<TSource, TException>(
    this IQbservable<TSource> source,
    Expression<Func<TException, IObservable<TSource>>> handler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TException>
static IQbservable<TSource>^ Catch(
    IQbservable<TSource>^ source, 
    Expression<Func<TException, IObservable<TSource>^>^>^ handler
)
```

```fsharp
static member Catch : 
        source:IQbservable<'TSource> * 
        handler:Expression<Func<'TException, IObservable<'TSource>>> -> IQbservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TException  
  The type of the exception.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence.

- handler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TException, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>\>  
  The exception handler function, producing another queryable observable sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
A queryable observable sequence containing the source sequence's elements, followed by the elements produced by the handler's resulting queryable observable sequence in case an exception occurred.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Catch Overload](Catch/Qbservable.Catch)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








