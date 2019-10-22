title: Observable.FromEvent<TDelegate, TEventArgs>(Action<TDelegate>, Action<TDelegate>)
---
# Observable.FromEvent\<TDelegate, TEventArgs\> Method (Action\<TDelegate\>, Action\<TDelegate\>)

Converts a .NET event to an observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromEvent(Of TDelegate, TEventArgs) ( _
    addHandler As Action(Of TDelegate), _
    removeHandler As Action(Of TDelegate) _
) As IObservable(Of TEventArgs)
```

```vb
'Usage
Dim addHandler As Action(Of TDelegate)
Dim removeHandler As Action(Of TDelegate)
Dim returnValue As IObservable(Of TEventArgs)

returnValue = Observable.FromEvent(addHandler, _
    removeHandler)
```

```csharp
public static IObservable<TEventArgs> FromEvent<TDelegate, TEventArgs>(
    Action<TDelegate> addHandler,
    Action<TDelegate> removeHandler
)
```

```c++
public:
generic<typename TDelegate, typename TEventArgs>
static IObservable<TEventArgs>^ FromEvent(
    Action<TDelegate>^ addHandler, 
    Action<TDelegate>^ removeHandler
)
```

```fsharp
static member FromEvent : 
        addHandler:Action<'TDelegate> * 
        removeHandler:Action<'TDelegate> -> IObservable<'TEventArgs> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TDelegate  
  The type of delegate.

- TEventArgs  
  The type of event.

#### Parameters

- addHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>  
  Action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>  
  Action that detaches the given event handler from the underlying .NET event.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TEventArgs\>  
Observable sequence that contains data representations of invocations of the underlying .NET event.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[FromEvent Overload](FromEvent\Observable.FromEvent.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Observable.FromEvent\<TDelegate, TEventArgs\> Method (Func\<Action\<TEventArgs\>, TDelegate\>, Action\<TDelegate\>, Action\<TDelegate\>)

Converts a .NET event to an observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromEvent(Of TDelegate, TEventArgs) ( _
    conversion As Func(Of Action(Of TEventArgs), TDelegate), _
    addHandler As Action(Of TDelegate), _
    removeHandler As Action(Of TDelegate) _
) As IObservable(Of TEventArgs)
```

```vb
'Usage
Dim conversion As Func(Of Action(Of TEventArgs), TDelegate)
Dim addHandler As Action(Of TDelegate)
Dim removeHandler As Action(Of TDelegate)
Dim returnValue As IObservable(Of TEventArgs)

returnValue = Observable.FromEvent(conversion, _
    addHandler, removeHandler)
```

```csharp
public static IObservable<TEventArgs> FromEvent<TDelegate, TEventArgs>(
    Func<Action<TEventArgs>, TDelegate> conversion,
    Action<TDelegate> addHandler,
    Action<TDelegate> removeHandler
)
```

```c++
public:
generic<typename TDelegate, typename TEventArgs>
static IObservable<TEventArgs>^ FromEvent(
    Func<Action<TEventArgs>^, TDelegate>^ conversion, 
    Action<TDelegate>^ addHandler, 
    Action<TDelegate>^ removeHandler
)
```

```fsharp
static member FromEvent : 
        conversion:Func<Action<'TEventArgs>, 'TDelegate> * 
        addHandler:Action<'TDelegate> * 
        removeHandler:Action<'TDelegate> -> IObservable<'TEventArgs> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TDelegate  
  The type of delegate.

- TEventArgs  
  The type of event.

#### Parameters

- conversion  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TEventArgs\>, TDelegate\>  
  A function used to convert the given event handler to a delegate compatible with the underlying .NET event. The resulting delegate is used in calls to the addHandler and removeHandler action parameters.

- addHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>  
  Action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>  
  Action that detaches the given event handler from the underlying .NET event.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TEventArgs\>  
Observable sequence that contains data representations of invocations of the underlying .NET event.

## Remarks

The FromEvent operator creates an observable sequence of the event arguments provided with the underlying events when they are fired. The FromEvent operator only works with delegates of the type Action\<T\>. So a conversion function must be used to create an event handler that is compatible with the underlying .NET event. The example code in this topic demonstrates this conversion for the System.IO.FileSystemEventHandler and System.IO.RenamedEventHandler delegates.

## Examples

This example code demonstrates using the FromEvent operator to listen for Create, Rename, and Delete events on a System.IO.FileSystemWatcher. The example watches for these events in the C:\\Users\\Public folder. The FromEvent operator only supports delegates of the type Action\<T\>. So this example uses the conversion parameter to define a lambda expression to convert the System\<T\> delegate to the System.IO.FileSystemEventHandler and System.IO.RenamedEventHandler delegates. The events are observed using a subscription to each of the observable sequences. Each of the events is written to the console window.

    using System;
    using System.Reactive.Linq;
    using System.IO;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //*********************************************************************************************************************//
          //*** Create a FileSystemWatcher to watch the C:\Users\Public directory using the default NotifyFilter watching for ***//
          //*** changes to any type of file.                                                                                  ***//
          //*********************************************************************************************************************//
    
          FileSystemWatcher fsw = new FileSystemWatcher(@"C:\Users\Public", "*.*");
          fsw.EnableRaisingEvents = true;
    
    
          //******************************************************************************************//
          //*** Use the FromEvent operator to setup a subscription to the Created event.           ***//
          //***                                                                                    ***//
          //*** The first lambda expression performs the conversion of Action<FileSystemEventArgs> ***//
          //*** to FileSystemEventHandler. The FileSystemEventHandler just calls the handler       ***//
          //*** passing the FileSystemEventArgs.                                                   ***//
          //***                                                                                    ***//
          //*** The other lambda expressions add and remove the FileSystemEventHandler to and from ***//
          //*** the event.                                                                         ***//
          //******************************************************************************************//
    
          var fswCreated = Observable.FromEvent<FileSystemEventHandler, FileSystemEventArgs>(handler => 
                                      {
                                        FileSystemEventHandler fsHandler = (sender, e) =>
                                        {
                                          handler(e); 
                                        };
    
                                        return fsHandler; 
                                      },
                                      fsHandler => fsw.Created += fsHandler,
                                      fsHandler => fsw.Created -= fsHandler);
    
    fswCreated.Subscribe(e => Console.WriteLine("{0} was created.", e.FullPath));
    
    
          //******************************************************************************************//
          //*** Use the FromEvent operator to setup a subscription to the Renamed event.           ***//
          //***                                                                                    ***//
          //*** The first lambda expression performs the conversion of Action<RenamedEventArgs>    ***//
          //*** to RenamedEventHandler. The RenamedEventHandler just calls the handler passing the ***//
          //*** RenamedEventArgs.                                                                  ***//
          //***                                                                                    ***//
          //*** The other lambda expressions add and remove the RenamedEventHandler to and from    ***//
          //*** the event.                                                                         ***//
          //******************************************************************************************//
    
          var fswRenamed = Observable.FromEvent<RenamedEventHandler, RenamedEventArgs>(handler => 
                                      {
                                        RenamedEventHandler fsHandler = (sender, e) =>
                                        {
                                          handler(e); 
                                        };
          
                                        return fsHandler; 
                                      },
                                      fsHandler => fsw.Renamed += fsHandler,
                                      fsHandler => fsw.Renamed -= fsHandler);
    
          fswRenamed.Subscribe(e => Console.WriteLine("{0} was renamed to {1}.", e.OldFullPath, e.FullPath));
    
    
          //******************************************************************************************//
          //*** Use the FromEvent operator to setup a subscription to the Deleted event.           ***//
          //***                                                                                    ***//
          //*** The first lambda expression performs the conversion of Action<FileSystemEventArgs> ***//
          //*** to FileSystemEventHandler. The FileSystemEventHandler just calls the handler       ***//
          //*** passing the FileSystemEventArgs.                                                   ***//
          //***                                                                                    ***//
          //*** The other lambda expressions add and remove the FileSystemEventHandler to and from ***//
          //*** the event.                                                                         ***//
          //******************************************************************************************//
    
          var fswDeleted = Observable.FromEvent<FileSystemEventHandler, FileSystemEventArgs>(handler =>
                                      {
                                        FileSystemEventHandler fsHandler = (sender, e) =>
                                        {
                                          handler(e);
                                        };
          
                                        return fsHandler;
                                      },
                                      fsHandler => fsw.Deleted += fsHandler,
                                      fsHandler => fsw.Deleted -= fsHandler);
    
          fswDeleted.Subscribe(e => Console.WriteLine("{0} was deleted.", e.FullPath));
                    
    
          Console.WriteLine("Press ENTER to exit...\n");
          Console.ReadLine();
        }
      }
    }

The following output was generated with the example code.

    Press ENTER to exit...
    
    C:\Users\Public\New Text Document.txt was created.
    C:\Users\Public\New Text Document.txt was renamed to C:\Users\Public\TestFile.txt.
    C:\Users\Public\TestFile.txt was deleted.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[FromEvent Overload](FromEvent\Observable.FromEvent.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









