NoTitle: true
IsBlog: true
Title: In Praise of Elevated Values 
Tags: Article
Author: Rich Bryant
Published: 2020-07-16
---

<img src="https://i.imgur.com/i9ngTbc.png" align="right" style="height: 8em" alt="img"/>

## In Praise of Elevated Values
## Always Wear Protection
  
You know what you don't get when you work with Enumerables and Observables?  
  
This.  
  
<img src ="https://i.stack.imgur.com/zD45E.png" align="center" alt="img"/>  
  

I hate NullReferenceExceptions.  There's a very simple reason why you don't get them, of course, and that's because both IEnumerable and IObservable are a sort of a collection, and a collection is special in LINQ and Functional Programming terms because of how you _apply_ functions to it.  Let's take a simple `predicate` lambda, for example.  
  
```  
    var uselessListOfFalseBools = myCollection.Where(item => item.BooleanProperty ==  true)
       .Select(newItem => false);
```


Here I have a collection of items, each of which has a property named `BooleanProperty`.  I filter for the items where `BooleanProperty` is true and I create a new (admittedly useless) collection of `bool` values, each set to false.

Fine.  What happens if `myCollection` is empty?  

You already know what happens - nothing happens.  The `Where` operation produces an empty collection and the `Select` operation is simply never applied.  In essence, the pipeline breaks - without throwing any exceptions! - and `uselessListOfFalseBools` is an empty list.  
  
This, in FP terms, is an elevated value.  You're protected from NullReferenceExceptions by virtue of the fact that your values are enclosed in a collection structure.  
  
The same happens with anything that implements `IObservable<T>`.  
  
```  
    myObservable.Where(item => item.BooleanProperty == true)
       .Subscribe(() => DoFunction());`
```
`DoFunction` is only called when the value of the item that ticks through myObservable has a `BooleanProperty` set to `true` - otherwise the `Subscribe` is never applied.  If `BooleanProperty` is `false`, the "collection" produced by `Where` is empty.

Wouldn't it be nice if you could do this with _any_ class?  If only there was some way you could elevate those classes so that NullReferenceException never happened, operate on them with nice, clean testable pipelines and just have the pipeline break without error if it encountered a `null` value, or in elevated terms, an empty collection?
  
I mentioned in [a previous post](https://www.reactiveui.net/blog/2020/07/article-on-reactive-programing) that I'd talk about how to avoid `null` and `Exception` as well as `Event` - events replaced by observables, we did this one first - and this seems like a good time to do that.  What follows is not necessary to use ReactiveUI or even System.Reactive.  You could even regard it as horribly off-topic for this website if you so chose.  My belief, however, is that once you get used to elevated values and the virtues of _immutability_ - we'll talk about this later - which **are** required for System.Reactive and ReactiveUI, you're going to want the rest of your code to have the same benefits.    
  
Short version - you don't have to read any further but I strongly believe it will benefit you and anyone else who has to maintain your code if you do.
  
## LanguageExt  
  
Let's knock up a perfectly normal, boring class.  
  
```
    public class Person
    {
        public int Id  { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public bool LikesMarmite { get; set; }
    }
```

Let's say we want to get a person with an Id of 127 from the database and because we like Linq, we're using EF Core. Although if you're interested, I actually prefer [Linq2Db](https://github.com/linq2db/linq2db) because DbContext isn't thread-safe, but anyway, demo stuff so EF Core it is.  

```
    var myPerson = _dbContext.People().First(person => person.Id == 127);
```

There he is.  Unless I don't have a person with an ID of 127, in which case I'll get an exception.  I don't want that.  So let's change it a bit.  
  
```
    var myPerson = _dbContext.People().FirstOrDefault(person => person.Id == 127);
```

Obviously, we want to do something with this person object, so let's say he's filled in our questionnaire and we have determined that he likes Marmite because he is one of the good people.

```
    var myPerson = _dbContext.People().FirstOrDefault(person => person.Id == 127);
    myPerson.LikesMarmite = true;
```

Hang on though.  What if we've got something wrong?  What if 127 is not the Id of the sensible, well-adjusted person who therefore obviously likes Marmite?  What if we just returned a default - a null object?  We're going to get an exception.  Uh-oh.  
  
At this point, we could reach for try/catch and trap and log the missing Id and all the usual stuff but there's a better way.  There's a Functional way.  There's [LanguageExt](https://github.com/louthy/language-ext/).    From the GitHub repo...  
  
> This library uses and abuses the features of C# to provide a functional-programming 'base class library' that, if you squint, can look like extensions to the language itself. The desire here is to make programming in C# much more reliable and to make the engineer's inertia flow in the direction of declarative and functional code rather than imperative.

I'm not going to go into all the features of LanguageExt here but I'll point out some of the more useful bits you can pick up and immediately roll with.  Let's start with `Option<T>`.

`Option<T>` is like a collection for a single class.  Either it contains a non-null value (the `Some` condition) or it's empty (the `None` condition).  You can apply linq to `Option<T>` - and in fact, to all of LanguageExt's elevated classes - so that it's easy to put your work together in pipelines. It also has implicit conversion to `Option<MyClass>` which makes life much easier.  So let's split this up a bit.

```
    Option<Person> myPerson = _dbContext.People().FirstOrDefault(person => person.Id == 127);
```

So now I have my Person as an Elevated value.  Could be null, could be not null.  How to do I deal with that?  Well, this is Functional Programming so.... a function?  
  
```
    private Person SetMarmitePreference(Person person, bool likesMarmite)
        => new Person
           { 
              Id = person.Id,
              Firstname = person.FirstName,
              LastName = person.LastName,
              LikesMarmite = likesMarmite
           };
```

Okay.  You'll note we're returning a new Person.  Immutability is a feature of FP and it's great because it's entirely thread-safe.  If somebody else is working on Person127, it doesn't matter to us - we've got our own version and only we can touch it.

So how do we use this?  Well, you've probably guessed already but...  
  
```
    Option<Person> person127 = _dbContext.People().FirstOrDefault(person => person.Id == 127);
    var myPerson = person127.Select(person => SetMarmitePreference(person, true));
```

It's finally safe.  `Select` will never apply `SetMarmitePreference` to `person127` unless it contains a `Person`.  If it's null, nothing happens and `myPerson` is an Option in the `None` condition.  I like this, but I don't quite like it enough yet.  Too many steps. An unnecessary allocation.  Let's very quickly refactor the first statement to a function.  
  
```
   private Option<Person> GetPersonFromDatabaseById(int id)
       => _dbContext.People().FirstOrDefault(person => person.Id == id);
```

So now what do we have?  

Well, now we have this -   
  
```
   var myPerson = GetPersonFromDatabaseById(127)
                     .Select(person => SetMarmitePreference(person, true));
```

and that means we have two testable functions, working in a pipeline.  Just like you have with an Enumerable or an Observable.  We're null-safe and the style is consistent with System.Reactive and ReactiveUI.  And I find this is much more important that you'd expect.  Consistency may well be the bugbear of little minds - like mine - but damn, it helps when you have to review somebody else's code.  Or even come back to your own.  
  
LanguageExt has absolutely loads of useful features.  I won't go through them all here but I especially recommend you look into `Either<L, R>` which helps guard you against Exceptions and `Try<T>` which takes out all those horrible Try/Catch blocks when you really need Exceptions, like in network code or anything else that might suddenly be unavailable for external reasons. And `Validation<T>`.  Guess what that does. 
  
It's by [Paul Louth](https://twitter.com/paullouth), it's on [Nuget](https://www.nuget.org/packages/LanguageExt.Core/) and you should try it out.  I think you'll appreciate it.  
  
There are even extensions for Observables!

Code safe out there, folks.
