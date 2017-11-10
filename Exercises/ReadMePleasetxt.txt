race condition
deadlock
memoryleask
stackoverflow
bufferoverflow

------------------

webfarm
webgarden 
mutex / monitor
crud same table :  webapp/iis, webapi/iis, wcf/iis, services, localapp  
crud same table :  webapp/iis, webapi/iis, wcf/iis, services, localapp 

signle thread
  sync  thread1 task1-> task2-> task3
  async  thread1 task2-> task1-> task3

multi thread
sync  
 thread1 task1-> task2-> task3
 thread2 task1-> task2-> task3
async  
 thread1 task1-> task3-> task2
 thread2 task3-> task2-> task1

  
Multi-threading : more thread

Asynchronous
sync : end wait task one by one  
async : dont wait end task


iis request queue -->clr trheadpool--->porcessing request
limited number of threads in the .Net Thread Pool (250 per CPU by default),
--------------------

tmain-->start newthread1 no join   :  they go parallel =>no result ,no block tcaller
tmain-->start newthread1 join      :  main is block wait finish thread1 => no result , block tcaller, know whe thread1 end (tmain need t1 finish)
tmain-->start task1 wait or result :  main is block wait finish task1 => result , block tcaller (tmain need task1 finish)
tmain-->start ASYNC task1          :  main is not block wait finish task1 => result , no block tcaller
 
 
 https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
 Async void methods have different error-handling semantics. When an exception is thrown out of an async Task or async Task<T> method,
 that exception is captured and placed on the Task object. With async void methods, there is no Task object, 
 so any exceptions thrown out of an async void method will be raised directly on the SynchronizationContext that was active when the async void method started. 
  By default, when an incomplete Task is awaited, the current “context” is captured and used to resume the method when the Task completes. This “context” is
  the current SynchronizationContext unless it’s null, in which case it’s the current TaskScheduler. 
  GUI and ASP.NET applications have a SynchronizationContext that permits only one chunk of code to run at a time. 
 Note that console applications don’t cause this deadlock. 
 They have a thread pool SynchronizationContext instead of a one-chunk-at-a-time SynchronizationContext, so when the await completes,
 it schedules the remainder of the async method on a thread pool thread. The method is able to complete, which completes its returned task, and there’s no deadlock.
 This difference in behavior can be confusing when programmers write a test console program, 
 observe the partially async code work as expected, and then move the same code into a GUI or ASP.NET application, where it deadlocks.
 
 
 **synchronization context
ConfigureAwait(true)   default  when the task is done, the continuation will be serialized back to the original thread  
ConfigureAwait(false)  when the task is done, the continuation will be serialized to the thread of task
 
 
 
 **Async await in console program
 Unfortunately, that doesn’t work (and in fact, the Visual Studio 11 compiler will reject an async Main method). 
 Remember from our intro post that an async method will return to its caller before it is complete. 
 This works perfectly in UI applications (the method just returns to the UI event loop) and 
 ASP.NET applications (the method returns off the thread but keeps the request alive).
 It doesn’t work out so well for Console programs: Main returns to the OS - so your program exits.

You can work around this by providing your own async-compatible context. 
AsyncContext is a general-purpose context that can be used to enable an asynchronous MainAsync:

