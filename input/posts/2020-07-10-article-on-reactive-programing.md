NoTitle: true
IsBlog: true
Title: On Reactive Programming
Tags: Article
Author: Rich Bryant
Published: 2020-07-10
---

<img src="https://img.favpng.com/22/20/20/question-mark-information-clip-art-png-favpng-xUsis3eLNHY4KHfJcLDsF87rq.jpg" align="right" style="height: 8em" alt="img"/>

## On Reactive Programming
## Why are we even here?  
  
Well, some say the Creator took a handful of sand and - wait, no.  Why are we _here_? Why ReactiveUI?  

Tough question.  
  
There are many MVVM frameworks although not as many as there used to be, but one thing that always seems to happen if you ask on [Stack Overflow](https://www.stackoverflow.com/) is you'll get all kinds of advice and one bit in particular which stuck with me.  It was this -   
  
> "Prism is great but heavyweight, MVVMLite is great but shallow, Caliburn is pretty cool but has some odd dialect features, ReactiveUI is incredibly powerful but you'll need to learn a whole new way of thinking about everything, so basically use Prism."  

And it's nonsense.  But most people just accept it.  Who needs the overhead of learning a new way to think when you've got work to do and real problems to solve?  That's how I felt about it.  For years. And then...  
  
And then I took a job working on automating production lines.  If you've ever done this, it's fascinating stuff.  A very specialized feed box collects data over time from the machinery, that gets aggregated by a huge, unwieldy and horrible piece of 3rd party proprietary software called KepWare and then you have to write clients to display and control via the output from that.  In whatever.  Usually desktop Windows because no way do you want this lot open to the usual array of pranksters on the Internet.   
  
And we had a problem. There was a specialist piece that somebody before me had written for a company that made glue - seriously, glue, big business - and it ran fine.  Except that every two or three weeks, it crashed to desktop.  This is bad.  Lose data on a production line and anything can and does happen.  
  
I was pretty convinced that somewhere in the codebase, a C# Event was being added and never removed.  You know how easy that is.  And it occurred to me, and this is important, that _events suck_.  Furthermore, they're not events.  An event is a thing that happens, once.  We weren't dealing with an event.  We were dealing the shape of the data changing over time.    
  
Here's a dummy event.  
  
```csharp
public class GlueMachineMonitor
{
    public event EventHandler<StickyEventArgs> BungedUp;

    public void Run()
    {
        var eventHandling = BungedUp;
        eventHandling?.Invoke(this, new StickyEventArgs());
    }
}

public class GlueMachineIssueTracker : IDisposable
{
    private readonly GlueMachineMonitor _monitor;

    public GlueMachineIssueTracker(GlueMachineMonitor monitor)
    {
        _monitor = monitor;
        _monitor.BungedUp += ItGotGummedUp;
    }

    private void ItGotGummedUp(object sender, StickyEventArgs e)
    {
        Console.WriteLine("get the bloody solvent out, lads");
    }

    public void Dispose()
    {
        _monitor.BungedUp -= ItGotGummedUp;
    }
}
```

  
I went away and I thought about this.  I dreamed about it.  I woke up in the morning still trying to think in 4 dimensions and then I discovered that back in 2010, Erik Meijer and Bart de Smet had already modeled it.  And they called it [Reactive Extensions](https://www.reactivex.io).
  
So I went through the codebase, and I ripped out every last event.  Like this -    
  
```csharp
public class BetterGlueMachineMonitor
{
    private readonly Subject<StickyEventArgs> _blockageStatus = new Subject<StickyEventArgs>();

    public IObservable<StickyEventArgs> BlockageStatus 
        => _blockageStatus.AsObservable();

    public void Run() 
        => _blockageStatus.OnNext(new StickyEventArgs());
}

public class BetterGlueMachineTracker : IDisposable
{
    private readonly IDisposable _blockageStatusWatcher;

    public BetterGlueMachineTracker(BetterGlueMachineMonitor monitor) 
        => _blockageStatusWatcher = monitor.BlockageStatus.Subscribe(ItChangesOverTime);

    private static void ItChangesOverTime(StickyEventArgs e) 
        => Console.WriteLine("get the bloody solvent out, lads");

    public void Dispose() 
        => _blockageStatusWatcher.Dispose();
}
```

and then I stress-tested the absolute mother of all monstrous hell out of it because trust me, you have never seen so much incoming data in your life as you will from an automated production line.

Take a look at that second block of code, though, the one with the Observables.  It's nice, isn't it?  Neat.  Tidy.  You can see exactly what it does and how.  "This data has changed shape, express that to any observers".  And then clean up on Dispose.  
  
I was intrigued.  
  
Compare and contract to the usual imperative style.  
  
```  
Do this thing.  
Get this data.  
If data is this, do that.  
If data is null, explode horribly.   
Return data, assuming you haven't exploded horribly. 
```  

I exaggerate of course, but I'd just discovered _Functional Reactive Programming_.  I don't tell the code what to do, I tell it _how to react when things happen_.  I was pretty excited.  
  
Then I started to apply the LINQ i already knew but mistrusted to Observables because with that tidal wave of data, I don't want to write down everything.  The database would explode (and indeed, this had been a problem too. Regular manual truncates just to keep the system up due to gigs of redundant data).   

And suddenly, I had _Functional Pipelines_.  I'd shied away from these before - how do you unit test a pipeline? - but the benefits of dealing with 4D data correctly were too good to pass by.  And obviously, you test the units of the pipeline, god I was such a fool.  
  
So when the time came to think about rewriting the front-end - trust me, it needed it - I was already thinking in terms of describing how I wanted the interface to react to user input (and massive tons of data streaming in from bloody KepWare).  I won't say there was no learning curve, there's always a learning curve.  
  
The data is a wave, not a particle.  It's predictable within certain bounds but you need to react to it, not control it.  Reactive programming turns data into a wave you can surf. And that's why I'm here.  
  
Also, events suck.  
  
Tune in next week and we'll talk about how nulls suck and Exceptions suck and how you can surf those, too.  
