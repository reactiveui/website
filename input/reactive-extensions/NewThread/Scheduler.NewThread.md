title: Scheduler.NewThread Property
---
# Scheduler.NewThread Property

Gets the scheduler that schedules work on a new thread.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared ReadOnly Property NewThread As NewThreadScheduler
    Get
```

```vb
'Usage
Dim value As NewThreadScheduler

value = Scheduler.NewThread
```

```csharp
public static NewThreadScheduler NewThread { get; }
```

```c++
public:
static property NewThreadScheduler^ NewThread {
    NewThreadScheduler^ get ();
}
```

```fsharp
static member NewThread : NewThreadScheduler
```

```javascript
static function get NewThread () : NewThreadScheduler
```

#### Property Value

Type: [System.Reactive.Concurrency.NewThreadScheduler](NewThreadScheduler/NewThreadScheduler)  
The new thread scheduler.

## Remarks

The NewThread scheduler schedules the execution of operations on a new thread. This scheduler is ideal for long running operations.

## Examples

The example code generates an endless sequence of emails from an IEnumerable that randomly yields an email within three seconds. The emails are timestamped using the IObservable.TimeStamp operator. Then they are buffered into a buffer that holds all emails that occur within a ten second time span. A subscription to the buffered sequence is created. Finally, each group of emails is then written to the console window along with the corresponding timestamp generated for the email. In this example, we use a timer to control the frequency of emails delivered from the email buffer. This timer will be on another thread since the main thread will be blocked waiting on a key press.

    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Reactive.Linq;
    using System.Reactive;
    
    namespace Example
    {
    
      class Program
      {
        static void Main()
        {
          //************************************************************************************************************************//
          //*** By generating an observable sequence from the enumerator, we can use Rx to push the emails to an email buffer    ***//
          //*** and have the buffer dumped at an interval we choose. This simulates how often email is checked for new messages. ***//
          //************************************************************************************************************************//
          IObservable<string> myInbox = EndlessBarrageOfEmails().ToObservable();
    
    
          //************************************************************************************************************************//
          //*** We can use the Timestamp operator to additionally timestamp each email in the sequence when it is received.      ***//
          //************************************************************************************************************************//
          IObservable<Timestamped<string>> myInboxTimestamped = myInbox.Timestamp();
    
    
          //******************************************************************************************************************************//
          //*** The timer controls the frequency of emails delivered from the email buffer. This timer will be on another thread since ***//
          //*** the main thread will be blocked waiting on a key press.                                                                ***//
          //******************************************************************************************************************************//
          System.Reactive.Concurrency.IScheduler scheduleOnNewThread = System.Reactive.Concurrency.Scheduler.NewThread;
    
    
          //***************************************************************************************************************************//
          //*** Create a buffer with Rx that will hold all emails received within 10 secs and execute subscription handlers for the ***//
          //*** buffer every 10 secs.                                                                                               ***//
          //*** Schedule the timers associated with emptying the buffer to be created on the new thread.                            ***//
          //***************************************************************************************************************************//
          IObservable<IList<Timestamped<string>>> newMail = myInboxTimestamped.Buffer(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10), 
                                                                                      scheduleOnNewThread);
    
    
          //******************************************************//
          //*** Activate the subscription on a separate thread ***//
          //******************************************************//
          IDisposable handle = newMail.SubscribeOn(scheduleOnNewThread).Subscribe(emailList =>
          {
            Console.WriteLine("\nYou've got mail!  {0} messages.\n", emailList.Count);
            foreach (Timestamped<string> email in emailList)
            {
              Console.WriteLine("Message   : {0}\nTimestamp : {1}\n", email.Value, email.Timestamp.ToString());
            }
          });
    
          Console.ReadLine();
          handle.Dispose();
        }
    
    
    
        //*********************************************************************************************//
        //***                                                                                       ***//
        //*** This method will continually yield a random email at a random interval within 3 sec.  ***//
        //***                                                                                       ***//
        //*********************************************************************************************//
        static IEnumerable<string> EndlessBarrageOfEmails()
        {
          Random random = new Random();
    
          //***************************************************************//
          //*** For this example we are using this fixed list of emails ***//
          //***************************************************************//
          List<string> emails = new List<string> { "Email Msg from John ", 
                                                   "Email Msg from Bill ", 
                                                   "Email Msg from Marcy ", 
                                                   "Email Msg from Wes "};
    
          //***********************************************************************************//
          //*** Yield an email from the list continually at a random interval within 3 sec. ***//
          //***********************************************************************************//
          while (true)
          {
            yield return emails[random.Next(emails.Count)];
            Thread.Sleep(random.Next(3000));
          }
        }
      }
    }

Here is example output from the example code.

    You've got mail!  6 messages.
    
    Message   : Email Msg from John
    Timestamp : 5/16/2011 3:45:09 PM -04:00
    
    Message   : Email Msg from Wes
    Timestamp : 5/16/2011 3:45:12 PM -04:00
    
    Message   : Email Msg from Marcy
    Timestamp : 5/16/2011 3:45:13 PM -04:00
    
    Message   : Email Msg from Bill
    Timestamp : 5/16/2011 3:45:13 PM -04:00
    
    Message   : Email Msg from Marcy
    Timestamp : 5/16/2011 3:45:13 PM -04:00
    
    Message   : Email Msg from Marcy
    Timestamp : 5/16/2011 3:45:15 PM -04:00
    
    
    You've got mail!  7 messages.
    
    Message   : Email Msg from Marcy
    Timestamp : 5/16/2011 3:45:17 PM -04:00
    
    Message   : Email Msg from Bill
    Timestamp : 5/16/2011 3:45:18 PM -04:00
    
    Message   : Email Msg from Wes
    Timestamp : 5/16/2011 3:45:19 PM -04:00
    
    Message   : Email Msg from Bill
    Timestamp : 5/16/2011 3:45:21 PM -04:00
    
    Message   : Email Msg from Bill
    Timestamp : 5/16/2011 3:45:24 PM -04:00
    
    Message   : Email Msg from Bill
    Timestamp : 5/16/2011 3:45:26 PM -04:00
    
    Message   : Email Msg from Marcy
    Timestamp : 5/16/2011 3:45:26 PM -04:00

## See Also

#### Reference

[Scheduler Class](Scheduler/Scheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)







