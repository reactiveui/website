title: Observable.MaxBy<TSource, TKey>(IObservable<TSource>, Func<TSource, TKey>, IComparer<TKey>)
---
# Observable.MaxBy\<TSource, TKey\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>, IComparer\<TKey\>)

Returns the elements in an observable sequence with the maximum key value.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function MaxBy(Of TSource, TKey) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey), _
    comparer As IComparer(Of TKey) _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim comparer As IComparer(Of TKey)
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.MaxBy(keySelector, _
    comparer)
```

```csharp
public static IObservable<IList<TSource>> MaxBy<TSource, TKey>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector,
    IComparer<TKey> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey>
static IObservable<IList<TSource>^>^ MaxBy(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector, 
    IComparer<TKey>^ comparer
)
```

```fsharp
static member MaxBy : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> * 
        comparer:IComparer<'TKey> -> IObservable<IList<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TKey  
  The type of key.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence to get the maximum elements for.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  The key selector function.

- comparer  
  Type: [System.Collections.Generic.IComparer](https://msdn.microsoft.com/en-us/library/8ehhxeaf)\<TKey\>  
  The comparer used to compare key values.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The elements in an observable sequence with the maximum key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[MaxBy Overload](MaxBy\Observable.MaxBy.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.MaxBy\<TSource, TKey\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>)

Returns the elements in an observable sequence with the maximum key value.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function MaxBy(Of TSource, TKey) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey) _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.MaxBy(keySelector)
```

```csharp
public static IObservable<IList<TSource>> MaxBy<TSource, TKey>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey>
static IObservable<IList<TSource>^>^ MaxBy(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector
)
```

```fsharp
static member MaxBy : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> -> IObservable<IList<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TKey  
  The type of key.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence to get the maximum elements for.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  The key selector function.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The elements in an observable sequence with the maximum key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

The MaxBy operator is used obtain the items in the sequence that generate a maximum key value. For example, if the sequence was a sequence of all processes running on a machine the MaxBy operator could be used to return the processes that have allocated the most physical memory . The example code in this topic demonstrates this.

## Examples

The following example code creates an observable sequence of all running processes on the local machine. Then the MaxBy operator is used to return an observable sequence which contains a list of the processes with that have allocated the most physical memory. The process information for the processes in the list is written to the console window.

    using System;
    using System.Reactive.Linq;
    using System.Diagnostics;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //*********************************************************************************************//
          //*** Generate a sequence of processes running on the local machine.                        ***//
          //*********************************************************************************************//
    
          var seqProcesses = System.Diagnostics.Process.GetProcesses().ToObservable();
    
    
          //*********************************************************************************************//
          //*** Use the MaxBy operator to get a list of the processes that have the highest amount    ***//
          //*** of physical memory allocated.                                                         ***//
          //*********************************************************************************************//
    
          var maxWorkingSet = seqProcesses.MaxBy(p => p.WorkingSet64);
    
    
          //*********************************************************************************************//
          //*** Write the process information to the console window for the processes which have      ***//
          //*** allocated the most physical memory                                                    ***//
          //*********************************************************************************************//
    
          maxWorkingSet.Subscribe(maxList => 
          {
            foreach (Process process in maxList)
            {
              Console.WriteLine("\nDescription   : {0}\n" + 
                                "Filename      : {1}\n" + 
                                "Working Set   : {2}", 
                                process.MainModule.FileVersionInfo.FileDescription, 
                                process.MainModule.FileName, 
                                process.WorkingSet64);
            }
          });
    
          Console.WriteLine("\nPress ENTER to exit...\n");
          Console.ReadLine();
        }
      }
    }

The following output was generated by the example code.

    Description   : Desktop Window Manager
    Filename      : C:\Windows\system32\Dwm.exe
    Working Set   : 363646976
    
    Press ENTER to exit...

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[MaxBy Overload](MaxBy\Observable.MaxBy.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)










