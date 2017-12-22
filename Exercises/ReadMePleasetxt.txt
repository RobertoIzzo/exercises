																C#
Guida c#
https://docs.microsoft.com/it-it/dotnet/csharp/index
parole chiave c#
https://docs.microsoft.com/it-it/dotnet/csharp/language-reference/keywords/

											A P P   D O M A I N
https://docs.microsoft.com/en-us/dotnet/framework/app-domains/application-domains
https://msdn.microsoft.com/en-us/library/cxk374d9.aspx			 
Operating systems and runtime environments typically provide some form of isolation between applications. For example, Windows uses processes to isolate applications. This isolation is necessary to ensure that code running in one application cannot adversely affect other, unrelated applications.

Application domains provide an isolation boundary for security, reliability, and versioning, and for unloading assemblies. Application domains are typically created by runtime hosts, which are responsible for bootstrapping the common language runtime before an application is run.
use application domains to provide isolation between assemblies.			 
			                                 C A S  - (Code Access Security)

-https://en.wikipedia.org/wiki/Code_Access_Security
-https://docs.microsoft.com/en-us/dotnet/framework/misc/code-access-security-basics
-http://www.c-sharpcorner.com/UploadFile/84c85b/net-code-access-security-cas/

Every application that targets the common language runtime (that is, every managed application) must interact with the runtime's security system. When a managed application is loaded, its host automatically grants it a set of permissions. These permissions are determined by the host's local security settings or by the sandbox the application is in. Depending on these permissions, the application either runs properly or generates a security exception.
The default host for desktop applications allows code to run in full trust. Therefore, if your application targets the desktop, it has an unrestricted permission set. Other hosts or sandboxes provide a limited permission set for applications. Because the permission set can change from host to host, you must design your application to use only the permissions that your target host allows.
You must be familiar with the following code access security concepts in order to write effective applications that target the common language runtime:

1)Type-safe code: Type-safe code is code that accesses types only in well-defined, allowable ways. For example, given a valid object reference, type-safe code can access memory at fixed offsets that correspond to actual field members. If the code accesses memory at arbitrary offsets outside the range of memory that belongs to that object's publicly exposed fields, it is not type-safe

2)Imperative and declarative syntax: Code that targets the common language runtime can interact with the security system by requesting permissions, demanding that callers have specified permissions, and overriding certain security settings (given enough privileges). You use two forms of syntax to programmatically interact with the .NET Framework security system: declarative syntax and imperative syntax. Declarative calls are performed using attributes; imperative calls are performed using new instances of classes within your code. Some calls can be performed only imperatively, others can be performed only declaratively, and some calls can be performed in either manner.

3)Secure class libraries: A secure class library uses security demands to ensure that the library's callers have permission to access the resources that the library exposes. For example, a secure class library might have a method for creating files that would demand that its callers have permissions to create files. The .NET Framework consists of secure class libraries. You should be aware of the permissions required to access any library that your code uses

4)Transparent code: Starting with the .NET Framework 4, in addition to identifying specific permissions, you must also determine whether your code should run as security-transparent. Security-transparent code cannot call types or members that are identified as security-critical. This rule applies to full-trust applications as well as partially trusted applications

Writing Verifiably Type-Safe Code
Just-in-time (JIT) compilation performs a verification process that examines code and tries to determine whether the code is type-safe. Code that is proven during verification to be type-safe is called verifiably type-safe code. Code can be type-safe, yet might not be verifiably type-safe because of the limitations of the verification process or of the compiler. Not all languages are type-safe, and some language compilers, such as Microsoft Visual C++, cannot generate verifiably type-safe managed code. To determine whether the language compiler you use generates verifiably type-safe code, consult the compiler's documentation. If you use a language compiler that generates verifiably type-safe code only when you avoid certain language constructs, you might want to use the PEVerify tool to determine whether your code is verifiably type-safe.
Code that is not verifiably type-safe can attempt to execute if security policy allows the code to bypass verification. However, because type safety is an essential part of the runtime's mechanism for isolating assemblies, security cannot be reliably enforced if code violates the rules of type safety. By default, code that is not type-safe is allowed to run only if it originates from the local computer. Therefore, mobile code should be type-safe.

                                         SAFE AND UNSAFE CODE

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

EXPRESSION VS FUNC<T>

Most times you're going to want Func or Action if all that needs to happen is to run some code. You need Expression when the code needs to be analyzed, serialized, or optimized before it is run. Expression is for thinking about code, Func/Action is for running it.
------------------------




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
