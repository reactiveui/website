# ReplaySubject\<T\>.Subscribe Method

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
Dim instance As ReplaySubject
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
  Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<[T](ReplaySubject\ReplaySubject(T).md)\>  
  Observer to subscribe to the subject.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
IDisposable object that can be used to unsubscribe the observer from the subject.

#### Implements

[IObservable\<T\>.Subscribe(IObserver\<T\>)](https://msdn.microsoft.com/en-us/library/m:system.iobservable%601.subscribe(system.iobserver%7b%600%7d)(v=VS.103))

## Remarks

Calling Subscribe creates a subscription to the ReplaySubject’s IObservable interface. The ReplaySubject will publish the items it receives from its own subscription(s) through its IObservable interface.

## Examples

In this example we have created a NewsHeadlineFeed class which is just a mock news feed in the form of an observable sequence of strings. It uses the Generate operator to continuoously generate a random news headline within three seconds.

A ReplaySubject is created to subscribe to two news feeds of the NewsHeadlineFeed class. Before the subject is subscribed to the feeds, the Timestamp operator is used to timestamp each headline. So the sequence that the ReplaySubject actually subscribes to is of the type IObservable\<Timestamped\<string\>\>. With the headline sequence timestamped, subscribers can subscribe to the subject's observable interface to observe the data stream(s) or a subset of the stream(s) based on the timestamp.

A ReplaySubject buffers items it receives. So a subscription created at a later time can access items from the sequence which have already been buffered and published. A subscriptions is created to the ReplaySubject that receives only local news headlines which occurred 10 seconds before the local news subscription was created. So we basically have the ReplaySubject "replay" what happened 10 seconds earlier.

A local news headline just contains the newsLocation substring ("in your area.").

    using System;
    using System.Reactive;
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
          //*** A subject acts similar to a proxy in that it acts as both a subscriber and a publisher        ***//
          //*** It's IObserver interface can be used to subscribe to multiple streams or sequences of data.   ***//
          //*** The data is then published through it's IObservable interface.                                ***//
          //***                                                                                               ***//
          //*** In this example a simple ReplaySubject is used to subscribe to multiple news feeds            ***//
          //*** that provide random news headlines. Before the subject is subscribed to the feeds, we use     ***//
          //*** Timestamp operator to timestamp each headline. Subscribers can then subscribe to the subject  ***//
          //*** observable interface to observe the data stream(s) or a subset of the stream(s) based on      ***//
          //*** time.                                                                                         ***//
          //***                                                                                               ***//
          //*** A ReplaySubject buffers items it receives. So a subscription created at a later time can      ***//
          //*** access items from the sequence which have already been published.                             ***//
          //***                                                                                               ***//
          //*** A subscriptions is created to the ReplaySubject that receives only local news headlines which ***//
          //*** occurred 10 seconds before the local news subscription was created. So we basically have the  ***//
          //*** ReplaySubject "replay" what happened 10 seconds earlier.                                      ***//
          //***                                                                                               ***//
          //*** A local news headline just contains the newsLocation substring ("in your area.").             ***//
          //***                                                                                               ***//
          //*****************************************************************************************************//
    
          ReplaySubject<Timestamped<string>> myReplaySubject = new ReplaySubject<Timestamped<String>>();
    
    
          //*****************************************************************//
          //*** Create news feed #1 and subscribe the ReplaySubject to it ***//
          //*****************************************************************//
    
          NewsHeadlineFeed NewsFeed1 = new NewsHeadlineFeed("Headline News Feed #1");
          NewsFeed1.HeadlineFeed.Timestamp().Subscribe(myReplaySubject);
    
    
          //*****************************************************************//
          //*** Create news feed #2 and subscribe the ReplaySubject to it ***//
          //*****************************************************************//
    
          NewsHeadlineFeed NewsFeed2 = new NewsHeadlineFeed("Headline News Feed #2");
          NewsFeed2.HeadlineFeed.Timestamp().Subscribe(myReplaySubject);
    
    
          //*****************************************************************************************************//
          //*** Create a subscription to the subject's observable sequence. This subscription will filter for ***//
          //*** only local headlines that occurred 10 seconds before the subscription was created.            ***//
          //***                                                                                               ***//
          //*** Since we are using a ReplaySubject with timestamped headlines, we can subscribe to the        ***//
          //*** headlines already past. The ReplaySubject will "replay" them for the localNewSubscription     ***//
          //*** from its buffered sequence of headlines.                                                      ***//
          //*****************************************************************************************************//
    
          Console.WriteLine("Waiting for 10 seconds before subscribing to local news headline feed.\n");
          Thread.Sleep(10000);
    
          Console.WriteLine("\n*** Creating local news headline subscription at {0} ***\n", DateTime.Now.ToString());
          Console.WriteLine("This subscription asks the ReplaySubject for the buffered headlines that\n" +
                            "occurred within the last 10 seconds.\n\nPress ENTER to exit.", DateTime.Now.ToString());
    
          DateTime lastestHeadlineTime = DateTime.Now;
          DateTime earliestHeadlineTime = lastestHeadlineTime - TimeSpan.FromSeconds(10);     
          
          IDisposable localNewsSubscription = myReplaySubject.Where(x => x.Value.Contains("in your area.") &&
                                                                   (x.Timestamp >= earliestHeadlineTime) &&
                                                                   (x.Timestamp < lastestHeadlineTime)).Subscribe(x =>
          {
            Console.WriteLine("\n************************************\n" +
                              "***[ Local news headline report ]***\n" +
                              "************************************\n" + 
                              "Time         : {0}\n{1}\n\n", x.Timestamp.ToString(), x.Value);
          });
    
          Console.ReadLine();
    
    
          //*******************************//
          //*** Cancel the subscription ***//
          //*******************************//
    
          localNewsSubscription.Dispose();
          
    
          //*************************************************************************//
          //*** Unsubscribe all the ReplaySubject's observers and free resources. ***//
          //*************************************************************************//
    
          myReplaySubject.Dispose();    
        }
      }
    
    
    
      //*********************************************************************************//
      //***                                                                           ***//
      //*** The NewsHeadlineFeed class is just a mock news feed in the form of an     ***//
      //*** observable sequence in Reactive Extensions.                               ***//
      //***                                                                           ***//
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
                                             evt => { Thread.Sleep(rand.Next(3000)); return evt; },
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

The following output was generated with the example code. The new feeds are random so it is possible that you may have to run it more than once to see a local news headline.

    Waiting for 10 seconds before subscribing to local news headline feed.

_** Creating local news headline subscription at 5/9/2011 4:07:48 AM **_

    This subscription asks the ReplaySubject for the buffered headlines that
    occurred within the last 10 seconds.
    
    Press ENTER to exit.

_**********************************_
_**[ Local news headline report ]**_
_**********************************_
Time         : 5/9/2011 4:07:42 AM -04:00
Feedname     : Headline News Feed #2
Headline     : We have a lottery winner in your area.

_**********************************_
_**[ Local news headline report ]**_
_**********************************_
Time         : 5/9/2011 4:07:47 AM -04:00
Feedname     : Headline News Feed #1
Headline     : Weather watch for snow storm issued in your area.

## See Also

#### Reference

[ReplaySubject\<T\> Class](ReplaySubject\ReplaySubject(T).md)

[System.Reactive.Subjects Namespace](System.Reactive.Subjects\System.Reactive.Subjects.md)
