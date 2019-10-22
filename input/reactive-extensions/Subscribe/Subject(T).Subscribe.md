title: Subject<T>.Subscribe()
---
# Subject\<T\>.Subscribe Method

Subscribes an observer to the subject.

**Namespace:**  [System.Reactive.Subjects](System.Reactive.Subjects\System.Reactive.Subjects.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Subscribe ( _
    observer As IObserver(Of T) _
) As IDisposable
```

```vb
'Usage
Dim instance As Subject
Dim observer As IObserver(Of T)
Dim returnValue As IDisposable

returnValue = instance.Subscribe(observer)
```

```csharp
public IDisposable Subscribe(
    IObserver<T> observer
)
```

```c++
public:
virtual IDisposable^ Subscribe(
    IObserver<T>^ observer
) sealed
```

```fsharp
abstract Subscribe : 
        observer:IObserver<'T> -> IDisposable 
override Subscribe : 
        observer:IObserver<'T> -> IDisposable 
```

```jscript
public final function Subscribe(
    observer : IObserver<T>
) : IDisposable
```

#### Parameters

- observer  
  Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<[T](Subject\Subject(T).md)\>  
  Observer to subscribe to the subject.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
IDisposable object that can be used to unsubscribe the observer from the subject.

#### Implements

[IObservable\<T\>.Subscribe(IObserver\<T\>)](https://msdn.microsoft.com/en-us/library/m:system.iobservable%601.subscribe(system.iobserver%7b%600%7d)(v=VS.103))

## Examples

This example demonstrates the use of the subject class. An instance of a Subject of the string type is used to subscribe to two example news feeds. These feeds just publish random news headlines at an interval no greater than five seconds apart. Two subscriptions against the subject’s observable interface are then created to receive the combined data stream. One subscription reports each item from the data stream as “all news”. The other subscription filters each headline in the stream to only report local headlines. The subscriptions both write each headline received to the console window. Processing terminates when the user presses the enter key and Dispose is called to cancel both subscriptions.

    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Reactive.Concurrency;
    using System.Threading;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //*****************************************************************************************************//
          //***                                                                                               ***//
          //*** A subject acts similar to a proxy in that it acts as both a subscriber and a publisher        ***//
          //*** It's IObserver interface can be used to subscribe to multiple streams or sequences of data.   ***//
          //*** The data is then published through it's IObservable interface.                                ***//
          //***                                                                                               ***//
          //*** In this example a simple string based subject is used to subscribe to multiple news feeds     ***//
          //*** that provide random news headlines. Subscribers can then subscribe to the subject's           ***//
          //*** observable interface to observe the data stream(s) or a subset ofthe stream(s). Below we      ***//
          //*** subscribe the subject to two different news headline feeds. Then two subscriptions are        ***//
          //*** created: one for delivery of all news headlines, the other receives only local news headlines ***//
          //***                                                                                               ***//
          //*** A local news headline just contains the newsLocation substring ("in your area.").             ***//
          //***                                                                                               ***//
          //*****************************************************************************************************//
    
          Subject<string> mySubject = new Subject<string>();
    
    
          //*********************************************************//
          //*** Create news feed #1 and subscribe mySubject to it ***//
          //*********************************************************//
          NewsHeadlineFeed NewsFeed1 = new NewsHeadlineFeed("Headline News Feed #1");
          NewsFeed1.HeadlineFeed.Subscribe(mySubject);
    
          //*********************************************************//
          //*** Create news feed #2 and subscribe mySubject to it ***//
          //*********************************************************//
          NewsHeadlineFeed NewsFeed2 = new NewsHeadlineFeed("Headline News Feed #2");
          NewsFeed2.HeadlineFeed.Subscribe(mySubject);
    
    
          Console.WriteLine("Subscribing to news headline feeds.\n\nPress ENTER to exit.\n");
    
          //*****************************************************************************************************//
          //*** Create a subscription to the subject's observable sequence. This subscription will receive    ***//
          //*** all headlines.                                                                                ***//
          //*****************************************************************************************************//
          IDisposable allNewsSubscription = mySubject.Subscribe(x => 
          {
            Console.WriteLine("Subscription : All news subscription\n{0}\n", x);
          });
    
    
          //*****************************************************************************************************//
          //*** Create a subscription to the subject's observable sequence. This subscription will filter for ***//
          //*** only local headlines.                                                                         ***//
          //*****************************************************************************************************//
    
          IDisposable localNewsSubscription = mySubject.Where(x => x.Contains("in your area.")).Subscribe(x => 
          {
            Console.WriteLine("\n************************************\n" +
                              "***[ Local news headline report ]***\n" +
                              "************************************\n{0}\n\n", x);
          });
    
          Console.ReadLine();
    
    
          //*********************************//
          //*** Cancel both subscriptions ***//
          //*********************************//
    
          allNewsSubscription.Dispose();
          localNewsSubscription.Dispose();
        }
      }
    
    
    
      //*********************************************************************************//
      //*** The NewsHeadlineFeed class is just a mock news feed in the form of an     ***//
      //*** observable sequence in Reactive Extensions.                               ***//
      //*********************************************************************************//
      class NewsHeadlineFeed
      {
        private string feedName;                     // Feedname used to label the stream
        private IObservable<string> headlineFeed;    // The actual data stream
        private readonly Random rand = new Random(); // Used to stream random headlines.
    
    
        //*** A list of predefined news events to combine with a simple location string ***//
        static readonly string[] newsEvents = { "A tornado occurred ",
                                                "Weather watch for snow storm issued ",
                                                "A robbery occurred ",
                                                "We have a lottery winner ",
                                                "An earthquake occurred ",
                                                "Severe automobile accident "};
    
        //*** A list of predefined location strings to combine with a news event. ***//
        static readonly string[] newsLocations = { "in your area.",
                                                   "in Dallas, Texas.",
                                                   "somewhere in Iraq.",
                                                   "Lincolnton, North Carolina",
                                                   "Redmond, Washington"};
    
        public IObservable<string> HeadlineFeed
        {
          get { return headlineFeed; }
        }
    
        public NewsHeadlineFeed(string name)
        {
          feedName = name;
    
          //*****************************************************************************************//
          //*** Using the Generate operator to generate a continous stream of headline that occur ***//
          //*** randomly within 5 seconds.                                                        ***//
          //*****************************************************************************************//
          headlineFeed = Observable.Generate(RandNewsEvent(),
                                             evt => true,
                                             evt => RandNewsEvent(),
                                             evt => { Thread.Sleep(rand.Next(5000)); return evt; },
                                             Scheduler.ThreadPool);
        }
    
    
        //****************************************************************//
        //*** Some very simple formatting of the headline event string ***//
        //****************************************************************//
        private string RandNewsEvent()
        {
          return "Feedname     : " + feedName + "\nHeadline     : " + newsEvents[rand.Next(newsEvents.Length)] + 
                 newsLocations[rand.Next(newsLocations.Length)];
        }
      }
    }

The following output was generated with the example code.

    Subscribing to news headline feeds.
    
    Press ENTER to exit.
    
    Subscription : All news subscription
    Feedname     : Headline News Feed #2
    Headline     : A robbery occurred somewhere in Iraq.
    
    Subscription : All news subscription
    Feedname     : Headline News Feed #2
    Headline     : An earthquake occurred in Dallas, Texas.
    
    Subscription : All news subscription
    Feedname     : Headline News Feed #1
    Headline     : We have a lottery winner in your area.

_**********************************_
_**[ Local news headline report ]**_
_**********************************_
Feedname     : Headline News Feed #1
Headline     : We have a lottery winner in your area.

    Subscription : All news subscription
    Feedname     : Headline News Feed #2
    Headline     : Severe automobile accident Redmond, Washington
    
    Subscription : All news subscription
    Feedname     : Headline News Feed #2
    Headline     : We have a lottery winner in Dallas, Texas.
    
    Subscription : All news subscription
    Feedname     : Headline News Feed #2
    Headline     : An earthquake occurred in Dallas, Texas.
    
    Subscription : All news subscription
    Feedname     : Headline News Feed #1
    Headline     : We have a lottery winner somewhere in Iraq.
    
    Subscription : All news subscription
    Feedname     : Headline News Feed #2
    Headline     : Severe automobile accident somewhere in Iraq.
    
    Subscription : All news subscription
    Feedname     : Headline News Feed #2
    Headline     : An earthquake occurred in your area.

_**********************************_
_**[ Local news headline report ]**_
_**********************************_
Feedname     : Headline News Feed #2
Headline     : An earthquake occurred in your area.

## See Also

#### Reference

[Subject\<T\> Class](Subject\Subject(T).md)

[System.Reactive.Subjects Namespace](System.Reactive.Subjects\System.Reactive.Subjects.md)
