Order: 5
---

Users expect real time data. They want their tweets now. Their order confirmed now. They need prices accurate as of now. Their online games need to be responsive. As a developer, you demand fire-and-forget messaging. You don't want to be blocked waiting for a result. You want to have the result pushed to you when it is ready. Even better, when working with result sets, you want to receive individual results as they are ready. You do not want to wait for the entire set to be processed before you see the first row. The world has moved to push; users are waiting for us to catch up. Developers have tools to push data, this is easy. Developers need tools to react to push data.

Rx and specifically IObservable<T> is not a replacement for IEnumerable<T>. I would not recommend trying to take something that is naturally pull based and force it to be push based.

Translating existing IEnumerable<T> values to IObservable<T> just so that the code base can be "more Rx"
Message queues. Queues like in MSMQ or a JMS implementation generally have transactionality and are by definition sequential. I feel IEnumerable<T> is a natural fit for here.
By choosing the best tool for the job your code should be easier to maintain, provide better performance and you will probably get better support.

