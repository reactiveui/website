# Visual Studio for Mac

This is like Rx debugging pro-tip #1:

Under the `Breakpoints` pad, click `New Exception Catchpoint` and just enter `System.Exception` as the exception to break on. Exceptions w/subscriptions terminate the subscription. They tick "onError" instead of "onNext" or "onCompleted". This allows you to hook and figure out before "onError" ticks.
