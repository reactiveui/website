# Observable.ToObservable\<TSource\> Method (IEnumerable\<TSource\>)

Converts an enumerable sequence to an observable sequence with a specified source.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToObservable(Of TSource) ( _
    source As IEnumerable(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IEnumerable(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = source.ToObservable()
```

```csharp
public static IObservable<TSource> ToObservable<TSource>(
    this IEnumerable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ ToObservable(
    IEnumerable<TSource>^ source
)
```

```fsharp
static member ToObservable : 
        source:IEnumerable<'TSource> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TSource\>  
  The enumerable sequence to convert to an observable sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence whose elements are pulled from the given enumerable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[ToObservable Overload](ToObservable\Observable.ToObservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.ToObservable\<TSource\> Method (IEnumerable\<TSource\>, IScheduler)

Converts an enumerable sequence to an observable sequence with a specified source and scheduler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToObservable(Of TSource) ( _
    source As IEnumerable(Of TSource), _
    scheduler As IScheduler _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IEnumerable(Of TSource)
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TSource)

returnValue = source.ToObservable(scheduler)
```

```csharp
public static IObservable<TSource> ToObservable<TSource>(
    this IEnumerable<TSource> source,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ ToObservable(
    IEnumerable<TSource>^ source, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToObservable : 
        source:IEnumerable<'TSource> * 
        scheduler:IScheduler -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TSource\>  
  The enumerable sequence to convert to an observable sequence.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run the enumeration of the input sequence on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence whose elements are pulled from the given enumerable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

The ToObservable operator creates an observable sequence from an object that supports the IEnumerable interface. A scheduler parameter is provided with this operator to allow different scheduling options for the processing that is necessary to create the observable sequence. For example, you may want to schedule the enumeration processing that is necessary to occur on another thread.

## Examples

This example creates an observable sequence of strings (IObservable\<string\>) from the IEnumerable\<string\> exposed by the string array returned from the System.IO. Directory.GetDirectories method. The [ThreadPoolScheduler](ThreadPoolScheduler\ThreadPoolScheduler.md) scheduler is passed for the scheduler parameter to the ToObservable operator. This will cause the enumeration to be run on a thread from the .NET thread pool. So the main thread will not be blocked.

    using System;
    using System.IO;
    using System.Reactive.Linq;
    using System.Reactive.Concurrency;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //*********************************************************************************************************************//
          //*** Create a new observable sequence from the IEnumerable<String> exposed by the string array returned from       ***//
          //*** System.IO.Directory.GetDirectories().                                                                         ***//
          //***                                                                                                               ***//
          //*** In this example we use the ThreadPool scheduler to run the enumeration on a thread in the .NET thread pool.   ***//
          //*** This way our main thread is not blocked by the enumeration and we can process user interaction.               ***//
          //*********************************************************************************************************************//
    
          var fileList = Directory.GetDirectories(@"C:\Program Files");
          var seqfiles = fileList.ToObservable(Scheduler.ThreadPool);
    
    
          //*********************************************************************************************************************//
          //*** We subscribe to this sequence with a lambda expression as the action event handler for the OnNext action. It  ***//
          //*** writes each filename to the console window. A action event handler for the OnCompleted action is also         ***//
          //*** provided to inform the user the sequence has ended and prompt for the ENTER key.                              ***//
          //*********************************************************************************************************************//
    
          Console.WriteLine("\nCreating subscription. Press ENTER to exit...\n");      
          seqfiles.Subscribe(f => Console.WriteLine(f.ToString()));
    
    
          //*********************************************************************************************************************//
          //*** Since we used the ThreadPool scheduler when creating the observable sequence, the enumeration is running on a ***//
          //*** thread from the .NET thread pool. So the main thread is not blocked and can terminate the example if the user ***//
          //*** presses ENTER for a long running enumeration.                                                                 ***//
          //*********************************************************************************************************************//
    
          Console.ReadLine();
        }
      }
    }

The following output was generated using the example code.

    Creating subscription. Press ENTER to exit...
    
    C:\Program Files\Common Files
    C:\Program Files\IIS
    C:\Program Files\Internet Explorer
    C:\Program Files\Microsoft Games
    C:\Program Files\Microsoft Help Viewer
    C:\Program Files\Microsoft IntelliType Pro
    C:\Program Files\Microsoft LifeCam
    C:\Program Files\Microsoft Lync
    C:\Program Files\Microsoft Office
    C:\Program Files\Microsoft SDKs
    C:\Program Files\Microsoft Security Client
    C:\Program Files\Microsoft SQL Server
    C:\Program Files\Microsoft SQL Server Compact Edition
    C:\Program Files\Microsoft Sync Framework
    C:\Program Files\Microsoft Synchronization Services
    C:\Program Files\Microsoft Visual Studio 10.0
    C:\Program Files\Microsoft Visual Studio 9.0
    C:\Program Files\Microsoft.NET
    C:\Program Files\MSBuild
    C:\Program Files\Reference Assemblies
    C:\Program Files\Uninstall Information
    C:\Program Files\Windows Journal
    C:\Program Files\Windows Live
    C:\Program Files\Windows Mail
    C:\Program Files\Windows Media Components
    C:\Program Files\Windows Media Player
    C:\Program Files\Windows NT
    C:\Program Files\Windows Photo Viewer
    C:\Program Files\Windows Portable Devices
    C:\Program Files\Windows Sidebar
    C:\Program Files\Zune

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[ToObservable Overload](ToObservable\Observable.ToObservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
