title: Observable.FromEventPattern<TEventArgs>(Object, String)
---
# Observable.FromEventPattern\<TEventArgs\> Method (Object, String)

Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence, using reflection to find an instance event.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromEventPattern(Of TEventArgs As EventArgs) ( _
    target As Object, _
    eventName As String _
) As IObservable(Of EventPattern(Of TEventArgs))
```

```vb
'Usage
Dim target As Object
Dim eventName As String
Dim returnValue As IObservable(Of EventPattern(Of TEventArgs))

returnValue = Observable.FromEventPattern(target, _
    eventName)
```

```csharp
public static IObservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs>(
    Object target,
    string eventName
)
where TEventArgs : EventArgs
```

```c++
public:
generic<typename TEventArgs>
where TEventArgs : EventArgs
static IObservable<EventPattern<TEventArgs>^>^ FromEventPattern(
    Object^ target, 
    String^ eventName
)
```

```fsharp
static member FromEventPattern : 
        target:Object * 
        eventName:string -> IObservable<EventPattern<'TEventArgs>>  when 'TEventArgs : EventArgs
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TEventArgs  
  The type of event.

#### Parameters

- target  
  Type: [System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  The object instance that exposes the event to convert.

- eventName  
  Type: [System.String](https://msdn.microsoft.com/en-us/library/s1wwdcbf)  
  The name of the event to convert.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[EventPattern](EventPattern\EventPattern(TEventArgs).md)\<TEventArgs\>\>  
The return value is an observable sequence that contains data representations of invocations of the underlying .NET event.

## Remarks

The FromEventPattern operator converts a .Net event to a sequence of [EventPattern\<TEventArgs\>](EventPattern\EventPattern(TEventArgs).md). Each EventPattern instance contains the event arguments and the object sending the event. The event arguments are provided in the EventArgs property of each EventPattern delivered in the sequence. The object sending the event is provided in the Sender property of the EventPattern instance. The desired event is specified by passing the object that exposes the event as the **target** parameter and by setting the **eventName** parameter to the name of the event. The **TEventArgs** type specifies the type of event arguments that will be delivered with each event.

## Examples

This example code demonstrates using the FromEventPattern operator to listen for Create, Rename, and Delete events on a System.IO.FileSystemWatcher. The example watches for changes in the C:\\Users\\Public folder and writes the events to the console window.

    using System;
    using System.Reactive.Linq;
    using System.Reactive;
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
    
    
          //***************************************************************************************//
          //*** Use the FromEventPattern operator to setup a subscription to the Created event. ***//
          //***************************************************************************************//
    
          IObservable<EventPattern<FileSystemEventArgs>> fswCreated = Observable.FromEventPattern<FileSystemEventArgs>(fsw, "Created");
          fswCreated.Subscribe(pattern => Console.WriteLine("{0} was created in {1}.", pattern.EventArgs.Name, ((FileSystemWatcher)pattern.Sender).Path));
    
    
          //***************************************************************************************//
          //*** Use the FromEventPattern operator to setup a subscription to the Renamed event. ***//
          //***************************************************************************************//
    
          IObservable<EventPattern<RenamedEventArgs>> fswRenamed = Observable.FromEventPattern<RenamedEventArgs>(fsw, "Renamed");
          fswRenamed.Subscribe(pattern => Console.WriteLine("{0} was renamed to {1} in {2}.", pattern.EventArgs.OldName, 
                                                            pattern.EventArgs.Name, ((FileSystemWatcher)pattern.Sender).Path));
    
    
          //***************************************************************************************//
          //*** Use the FromEventPattern operator to setup a subscription to the Deleted event. ***//
          //***************************************************************************************//
    
          IObservable<EventPattern<FileSystemEventArgs>> fswDeleted = Observable.FromEventPattern<FileSystemEventArgs>(fsw, "Deleted");
          fswDeleted.Subscribe(pattern => Console.WriteLine("{0} was deleted in {1}.", pattern.EventArgs.Name, ((FileSystemWatcher)pattern.Sender).Path));
    
    
          Console.WriteLine("Press ENTER to exit...\n");
          Console.ReadLine();
        }
      }
    }

The following output demonstrates running the example code to create a new text file in the C:\\Users\\Public directory. The file is renamed to ExFile.txt and then deleted.

    Press ENTER to exit...
    
    New Text Document.txt was created in C:\Users\Public.
    New Text Document.txt was renamed to ExFile.txt in C:\Users\Public.
    ExFile.txt was deleted in C:\Users\Public.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[FromEventPattern Overload](FromEventPattern\Observable.FromEventPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)










# Observable.FromEventPattern\<TEventArgs\> Method (Action\<EventHandler\<TEventArgs\>\>, Action\<EventHandler\<TEventArgs\>\>)

Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence with the specified add handler and remove handler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromEventPattern(Of TEventArgs As EventArgs) ( _
    addHandler As Action(Of EventHandler(Of TEventArgs)), _
    removeHandler As Action(Of EventHandler(Of TEventArgs)) _
) As IObservable(Of EventPattern(Of TEventArgs))
```

```vb
'Usage
Dim addHandler As Action(Of EventHandler(Of TEventArgs))
Dim removeHandler As Action(Of EventHandler(Of TEventArgs))
Dim returnValue As IObservable(Of EventPattern(Of TEventArgs))

returnValue = Observable.FromEventPattern(addHandler, _
    removeHandler)
```

```csharp
public static IObservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs>(
    Action<EventHandler<TEventArgs>> addHandler,
    Action<EventHandler<TEventArgs>> removeHandler
)
where TEventArgs : EventArgs
```

```c++
public:
generic<typename TEventArgs>
where TEventArgs : EventArgs
static IObservable<EventPattern<TEventArgs>^>^ FromEventPattern(
    Action<EventHandler<TEventArgs>^>^ addHandler, 
    Action<EventHandler<TEventArgs>^>^ removeHandler
)
```

```fsharp
static member FromEventPattern : 
        addHandler:Action<EventHandler<'TEventArgs>> * 
        removeHandler:Action<EventHandler<'TEventArgs>> -> IObservable<EventPattern<'TEventArgs>>  when 'TEventArgs : EventArgs
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TEventArgs  
  The type of event.

#### Parameters

- addHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[EventHandler](https://msdn.microsoft.com/en-us/library/db0etb8x)\<TEventArgs\>\>  
  The action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[EventHandler](https://msdn.microsoft.com/en-us/library/db0etb8x)\<TEventArgs\>\>  
  The action that detaches the given event handler from the underlying .NET event.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[EventPattern](EventPattern\EventPattern(TEventArgs).md)\<TEventArgs\>\>  
The observable sequence that contains data representations of invocations of the underlying .NET event.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[FromEventPattern Overload](FromEventPattern\Observable.FromEventPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Observable.FromEventPattern\<TEventArgs\> Method (Type, String)

Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence, using reflection to find a static event.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromEventPattern(Of TEventArgs As EventArgs) ( _
    type As Type, _
    eventName As String _
) As IObservable(Of EventPattern(Of TEventArgs))
```

```vb
'Usage
Dim type As Type
Dim eventName As String
Dim returnValue As IObservable(Of EventPattern(Of TEventArgs))

returnValue = Observable.FromEventPattern(type, _
    eventName)
```

```csharp
public static IObservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs>(
    Type type,
    string eventName
)
where TEventArgs : EventArgs
```

```c++
public:
generic<typename TEventArgs>
where TEventArgs : EventArgs
static IObservable<EventPattern<TEventArgs>^>^ FromEventPattern(
    Type^ type, 
    String^ eventName
)
```

```fsharp
static member FromEventPattern : 
        type:Type * 
        eventName:string -> IObservable<EventPattern<'TEventArgs>>  when 'TEventArgs : EventArgs
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TEventArgs  
  The type of event.

#### Parameters

- type  
  Type: [System.Type](https://msdn.microsoft.com/en-us/library/42892f65)  
  The type that exposes the static event to convert.

- eventName  
  Type: [System.String](https://msdn.microsoft.com/en-us/library/s1wwdcbf)  
  The name of the event to convert.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[EventPattern](EventPattern\EventPattern(TEventArgs).md)\<TEventArgs\>\>  
The observable sequence that contains data representations of invocations of the underlying .NET event.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[FromEventPattern Overload](FromEventPattern\Observable.FromEventPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)







