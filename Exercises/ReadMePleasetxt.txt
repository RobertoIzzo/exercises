Usato per ottenere la dimensione in byte per un tipo non gestito. I tipi non gestiti includono i tipi predefiniti che sono elencati nella tabella riportata di seguito, nonché quanto segue:
Tipi enum
Tipi puntatore
Struct definiti dall'utente che non contengono campi o proprietà che sono tipi di riferimento
Nell'esempio seguente viene illustrato come ottenere la dimensione di un int:
C#


***typeof  
int intSize = sizeof(int);
System.Type type = typeof(int);  

***GetType
Per ottenere il tipo di runtime di un'espressione, è possibile usare il metodo di .NET Framework GetType, come nell'esempio seguente:
int i = 0;  
System.Type type = i.GetType();

***nameof
https://docs.microsoft.com/it-it/dotnet/csharp/language-reference/keywords/nameof
int p {  
   get { return this.p; }  
   set { this.p = value; PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.p)); } // nameof(p) works too  
}

ESEMPI
void f(string s) {  
   if (s == null) throw new ArgumentNullException(nameof(s));  
}

void f(int i) {  
   Log(nameof(f), "method entry");  
}

[DebuggerDisplay("={" + nameof(GetString) + "()}")]  
class C {  
   string GetString() { }  
}
----------------------
segmentation fault
race condition
deadlock
memoryleak
stackoverflow
bufferoverflow
------------------

webfarm
webgarden 
mutex / monitor
crud same table :  webapp/iis, webapi/iis, wcf/iis, services, localapp  

iis request queue -->clr trheadpool--->porcessing request
limited number of threads in the .Net Thread Pool (250 per CPU by default),
-------------------
 https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
 ***async void
 Async void methods have different error-handling semantics. When an exception is thrown out of an async Task or async Task<T> method,
 that exception is captured and placed on the Task object. With async void methods, there is no Task object, 
 so any exceptions thrown out of an async void method will be raised directly on the SynchronizationContext that was active when the async void
 method started. 
 By default, when an incomplete Task is awaited, the current “context” is captured and used to resume the method when the Task completes. 
 This “context” is
 the current SynchronizationContext unless it’s null, in which case it’s the current TaskScheduler. 
 GUI and ASP.NET applications have a SynchronizationContext that permits only one chunk of code to run at a time. 
 Note that console applications don’t cause this deadlock. 
 They have a thread pool SynchronizationContext instead of a one-chunk-at-a-time SynchronizationContext, so when the await completes,
 it schedules the remainder of the async method on a thread pool thread. The method is able to complete, which completes its returned task, 
 and there’s no deadlock.
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

the Main method can’t be async. If the Main method were async, it could return before it completed, 
causing the program to end. Figure 4 demonstrates this exception to the guideline:
The Main method for a console application is one of the few situations where code may block on an asynchronous method.
Stephen Cleary
***Avoid Async Void
1)To summarize this first guideline, you should prefer async Task to async void.
 Async Task methods enable easier error-handling, composability and testability.
 The exception to this guideline is asynchronous event handlers, which must return void. 
 This exception includes methods that are logically event handlers even 
 if they’re not literally event handlers (for example, ICommand.Execute implementations).
 
***Async All the Way
 2)shows a simple example where one method blocks on the result of an async method. 
 This code will work just fine in a console application but will deadlock when called from a GUI or ASP.NET context. 
 This behavior can be confusing, especially considering that stepping through the debugger implies that it’s the await that never completes. 
 The actual cause of the deadlock is further up the call stack when Task.Wait is called.
 The root cause of this deadlock is due to the way await handles contexts.
 By default, when an incomplete Task is awaited, the current “context” is captured and used to resume the method when the Task completes.
 This “context” is the current SynchronizationContext unless it’s null, in which case it’s the current TaskScheduler. 
 GUI and ASP.NET applications have a SynchronizationContext that permits only one chunk of code to run at a time. 
 When the await completes, it attempts to execute the remainder of the async method within the captured context. But that context already has a thread in it, which is (synchronously) waiting for the async method to complete. They’re each waiting for the other, causing a deadlock.

Note that console applications don’t cause this deadlock. They have a thread pool SynchronizationContext 
instead of a one-chunk-at-a-time SynchronizationContext, so when the await completes, it schedules the remainder of the async method on a thread pool thread.
 The method is able to complete, which completes its returned task, and there’s no deadlock. 
 This difference in behavior can be confusing when programmers write a test console program, 
 observe the partially async code work as expected, and then move the same code into a GUI or ASP.NET application, where it deadlocks.
 
 ***Configure Context
 3)You should not use ConfigureAwait when you have code after the await in the method that needs the context.
 For GUI apps, this includes any code that manipulates GUI elements, writes data-bound properties or 
 depends on a GUI-specific type such as Dispatcher/CoreDispatcher. 
 For ASP.NET apps, this includes any code that uses HttpContext.
 Current or builds an ASP.NET response, including return statements in controller actions.
 Figure 7demonstrates one common pattern in GUI apps—having an async event handler disable its control at
 the beginning of the method, perform some awaits and then re-enable its control at the end of the handler;
 the event handler can’t give up its context because it needs to re-enable its control.
 Each async method has its own context, so if one async method calls another async method, their contexts are independent.
 rivate async void button1_Click(object sender, EventArgs e)
{
  button1.Enabled = false;
  try
  {
    // Can't use ConfigureAwait here ...
    await Task.Delay(1000);
  }
  finally
  {
    // Because we need the context here.
    button1.Enabled = true;
  }
}
