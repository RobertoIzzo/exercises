MSBUILD di trova in ogni .net framework
ogni visualstudio ha un msbuild
msbuild è INDIPENDENTE da visualstudio

vs2015 msbuild .net 4.6
vs2013 msbuild .net 4.5.1
vs2012 msbuild .net 4.5
vs2010 msbuild .net 4
vs2008 msbuild .net 3.5

è possibile compilare un codice in c# da linea di comando con msbuild.exe





C#
Guida c#
https://docs.microsoft.com/it-it/dotnet/csharp/index
parole chiave c#
https://docs.microsoft.com/it-it/dotnet/csharp/language-reference/keywords/

						                        MARSHALING
https://msdn.microsoft.com/it-it/library/eaw10et3(v=vs.110).aspx#marshaling_and_com_apartments

									WCF
For use httpcontext in wcf (default is null) is necessary use attribute AspNetCompatibility,


													IIS
iis serve pagine html javascript e immagini
Classic mode
 (the only mode in IIS6 and below) is a mode where IIS only works with ISAPI extensions and 
 ISAPI filters directly. In fact, in this mode, ASP.NET is just an ISAPI extension (aspnet_isapi.dll)
  and an ISAPI filter (aspnet_filter.dll). IIS just treats ASP.NET as an external plugin
   implemented in ISAPI and works with it like a black box (and only when it's needs 
   to give out the request to ASP.NET). In this mode, ASP.NET is not much different from PHP or 
   other technologies for IIS.

Integrated mode,
 on the other hand, is a new mode in IIS7 where IIS pipeline is tightly integrated (i.e. is just the same) 
 as ASP.NET request pipeline. ASP.NET can see every request it wants to and manipulate things along the way.
  ASP.NET is no longer treated as an external plugin. It's completely blended and integrated in IIS.ù
   In this mode, ASP.NET HttpModules basically have nearly as much power as 
   an ISAPI filter would have had and ASP.NET HttpHandlers can have nearly equivalent capability as an ISAPI 
   extension could have. In this mode, ASP.NET is basically a part of IIS.

   https://stackoverflow.com/questions/716049/what-is-the-difference-between-classic-and-integrated-pipeline-mode-in-iis7
aspnet isapi extention  serve aspx, asmx ,ashx
			                                     	
													ISAPI

 (Internet Server Application Programming Interface):
vivie in un iis process, usa systemaccount,call w3wp(workerprocess)
											
											    WORCKER PROCESS THREAD
	https://fullsocrates.wordpress.com/2013/02/28/asp-net-threads-thread-parameters-in-iis-worker-processes-2/										
Previously, in Win32 native codes, IIS was running ASP Applications, and its thread parameter is quite simple. IIS has a setting – ‘ASPProcessorThreadMax’, which is 25(per CPU) by default, defined in a IIS Metabase (metabase.xml in c:\windows\system32\inetsrv)
So, this means that if there’re 26 ‘Active Server Pages’ applications, those take very very long time to finish, then 25 ASP’s are running in worker threads, and 1 ASP should stay in a queue, called ‘ASP Request Queue’ very long time.

In other words, if IIS has 2 CPU’s, and 60 ASP’s to run forever, then 50(=25×2) ASP’s are being handled by worker threads, 
and the rest 10 ASP’s should stay in ‘ASP request queue’.

The above concept is very important. It helps you to understand how to monitor ASP Applications, and how to determine optimal number of worker threads. For example, if a performance counter – ‘Active Server Pages\Requests Queued’ counter indicates a non-zero value, it means there’re queued requests, that means every worker threads are busy. 
It also means there’s a delay, and you may need to increase #threads, unless %CPU utilization is bad.

In summary, ASPProcessorThreadMax means ‘number of worker threads for ASP, per CPU, per worker process(w3wp)’.

In managed codes, such as ASP.NET Applications, it’s usually difficult to guess how many threads are working on the ASP.NET, or .NET applications. This article handles ‘ASP.NET’ threads, not ‘.NET Application’ threads, because it’s another story.

Before moving forward, it’s required to understand what threads mean, and what a process mean. There can be various definitions, depending on what you have seen. In this article, a process is more about IIS Worker Process, W3WP.exe, that includes & 
manages thread-pools.	

w3wp => threadpools =>worker thread pippo.aspx
		    =>worker thread pluto.aspx
	           =>worker thread pluto.asmx
			
A process is an isolated memory space, using virtual memory addresses, and it includes 1, or more threads, those are executed by Processors. A single processor runs one thread at a time, and that’s why thread parameters are usually multiplied by #CPU.

In W3WP process, you can assume that a thread is running one application, such as report.aspx. So, in order to run 2 ASP’s at a time, 2 processors need to run them in multiple threads – those are ‘Worker Threads’. (W3WP also has other types of threads)

So, if a worker process has many kind of applications to run, such as ASP(*.asp), ASP.NET(*.aspx), and Web Services(*.asmx), the status of threads will be as shown above. You can see that w3wp process runs 7 applications in 7 threads.
													
													W3WP
An Internet Information Services (IIS) worker process is a windows process (w3wp.exe ,Win32 applications.) 
which runs Web applications, and is responsible for handling requests sent to a 
Web Server for a specific application pool.

It is the worker process for IIS. Each application pool creates at least one instance of w3wp.exe and 
that is what actually processes requests in your application.
It is not dangerous to attach to this, that is just a standard windows message.



										       	HTTP RUNTIME
http application
http context
http request
http response
wcf non ha httpcontex gli va dato 


											A P P   D O M A I N
 Runtime Hosts : When we create an application in .NET, regardless of the type of the application 
(Console Applications, Windows applications, Web services, etc), 
it has to be hosted by a Win32 process.  This is because none of these are Win32 applications. 
 This process is what we call the Runtime Host.  The following are the runtime hosts shipped with .NET Framework:
  ASP.NET, IE and Shell.  It is also possible to write our own Runtime host using some Microsoft APIs.
https://docs.microsoft.com/en-us/dotnet/framework/app-domains/application-domains
https://msdn.microsoft.com/en-us/library/cxk374d9.aspx	
http://www.c-sharpcorner.com/article/appdomain-concept-in-Asp-Net/	
https://www.codeproject.com/Articles/34985/Threads-Process-and-AppDomains	
https://www.codeproject.com/Articles/6578/Use-AppDomains-To-Reduce-Memory-Consumption-in-NET


An AppDomain basically provides an isolated region in which code runs inside of a process.

An easy way to think of it is almost like a lighter-weight process sitting inside of your main process. 
Each AppDomain exists within a process in complete isolation, 
which allows you to run code safely (it can be unloaded without tearing down the whole process if needed),
 with separate security, etc.

As to your specifics - if you run code in 2 different AppDomains within a process,
 the code will run in isolation.
  Any communication between the AppDomains will get either serialized or handled via MarshallByRefObject.
   It behaves very much like using remoting in this regard. This provides a huge amount of security - you can run code that you don't trust, and if it does something wrong, it will not affect you.

In .NET, the basic unit of execution is NOT the process, 
  rather it is that of the Application Domain. The only true process is what is called a Runtime Host.
   The CLR is a DLL which means that, in order to run, it must be hosted in some process: 
   The runtime host. 
   There are basically three runtimes with the .NET framework:  Internet Explorer, ASP.NET , Windows shell

												 Internet Explorer 
Creates application domains in which to run managed controls. The .NET Framework supports the download and execution of browser-based controls. The runtime interfaces with the extensibility mechanism of Microsoft Internet Explorer through a mime filter to create application domains in which to run the managed controls. By default, one application domain is created for each Web site.
													
													ASP.NET 
Loads the runtime into the process that is to handle the Web request. ASP.NET also creates an application domain for each Web application that will run on a Web server.
												
												Windows shell
Invokes runtime hosting code to transfer control to the runtime each time an executable is launched from the shell.



Operating systems and runtime environments typically provide some form of isolation between applications.
 For example, Windows uses processes to isolate applications. This isolation is necessary 
 to ensure that code running in one application cannot adversely affect other, unrelated applications.
 The point I’m trying to make here is that everything in .NET runs within an application domain. 
 Even though you never create an AppDomain explicitly, the runtime host creates a default domain for you before running your application runs. 
 What makes them even more powerful, is that a single process can have multiple domains running within it. 
 
 Unlike a thread, each application domain runs isolated from the others with its own address space and memory. So where’s the benefit? What do we get by running multiple applications in the same process versus running multiple processes with single (defaulted) application domains.
Application domains provide an isolation boundary for security, reliability, and versioning, 
and for unloading assemblies. Application domains are typically created by runtime hosts, 
which are responsible for bootstrapping the common language runtime before an application is run.
use application domains to provide isolation between assemblies.

APP DOMAIN AND EXCEPTION 
Because your exceptions may need to be marshalled between different AppDomains and if they aren't (properly) serializable you will lose precious debugging information. Unlike other classes, you won't have control over whether your exception will be marshalled -- it will.

When I mean "you won't have control" I mean that classes you create generally have a finite space of existence and the existence is well known. If it's a return value and someone tries to call it in a different AppDomain (or on a different machine) they will get a fault and can just say "Don't use it that way." The caller knows they have to convert it into a type that can be serialized (by wrapping the method call). However since exceptions are bubbled up to the very top if not caught they can transcend AppDomain boundaries you didn't even know you had. Your custom application exception 20 levels deep in a different AppDomain might be the exception reported at Main() and nothing along the way is going to convert it into a serializable exception for you.

							                        SERIALIZABLE
What is it?
When you create an object in a .Net framework application, you don't need to think about how the data is stored in memory. Because the .Net Framework takes care of that for you. However, if you want to store the contents of an object to a file, send an object to another process or transmit it across the network, you do have to think about how the object is represented because you will need to convert to a different format. This conversion is called SERIALIZATION.

In informatica, la serializzazione è un processo per salvare un oggetto in un supporto di memorizzazione lineare (ad esempio, un file o un'area di memoria), o per trasmetterlo su una connessione di rete. La serializzazione può essere in forma binaria o può utilizzare codifiche testuali (ad esempio il formato XML) direttamente leggibili dagli esseri umani. Lo scopo della serializzazione è di trasmettere l'intero stato dell'oggetto in modo che esso possa essere successivamente ricreato nello stesso identico stato dal processo inverso, chiamato deserializzazione.
						serialiaùzzazione leggibile ad esseri umani
Alla fine degli anni novanta si sono affermati protocolli alternativi a quelli standard usati in precedenza: il linguaggio di markup XML è stato usato per generare codice leggibile in formato testo. Questa tecnica è particolarmente utile per rendere gli oggetti persistenti comprensibili agli umani, oppure per trasmetterli ad altri sistemi che utilizzano diversi linguaggi di programmazione, anche se presenta lo svantaggio di perdere la compattezza di altri tipi di codifica byte-stream, di solito preferiti per motivi di praticità. Una possibile soluzione futura a questo problema potrebbero essere i cosiddetti schemi di compressione trasparente (vedere XML binario). XML è oggi spesso usato per trasferimenti sincroni di dati strutturati fra client e server in applicazioni WEB sviluppate in AJAX. In alternativa si usa JSON, un protocollo di serializzazione di tipo testo più leggero che utilizza la sintassi di JavaScript ma è supportato anche da molti altri linguaggi di programmazione.

				Uses for Serialization

Serialization allows the developer to save the state of an object and recreate it as needed, providing storage of objects as well as data exchange. Through serialization, a developer can perform actions like sending the object to a remote application by means of a Web Service, passing an object from one domain to another, passing an object through a firewall as an XML string, or maintaining security or user-specific information across applications.

		            esempio serializzazione
appwebX use jsonclient(servicestack) to send class pipporequest  to e webapiY .
in appwebX pipporequest is serialize in json and send whit rest  call  (POST) to webapiY
webapiY deserialize json in class pipporequest
appwebx and webapiy have both class pipporequest (dto) but is not necessari that they have the same variable 

SERIALIZZATORE JSON prende stream + classe pippo e invia a qualcuno che deserializza


												RUNTIME HOST

 
And from a simplified traditional OS point of view, the CLR is really just a set of DLLs. 
So, you need a OS process to load and execute the entry point of that CLR DLL. 
This hosting executable is your runtime host. 
In reality the .Net runtime host does a lot of things for the CLR
The hosting process is started like any other process. 
Basically, what makes it a .Net runtime host is that it loads the CLR.

The CLR has been designed to support a variety of different types of applications,
 from Web server applications to applications with a traditional rich Windows user interface. 
 Each type of application requires a runtime host to start it.
  The runtime host loads the runtime into a process, creates the application domains within the process,
   and loads user code into the application domains.

The .NET Framework ships with a number of different runtime hosts, including the hosts listed in the following table.
1)everything in .NET runs within an application domain. 
2) Even though you never create an AppDomain explicitly, the runtime host creates a default domain for you before running
   your application runs. 
3)if you run code in 2 different AppDomains within a process,
 the code will run in isolation.
  Any communication between the AppDomains will get either serialized or handled via MarshallByRefObject.
4)The only true process is what is called a Runtime Host (Win32 applications)
5)  The CLR is a DLL which means that, in order to run, it must be hosted in some process: 
   The runtime host. 


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

																***async void***

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
 
 
												**synchronization context***
												https://www.codeproject.com/Articles/31971/Understanding-SynchronizationContext-Part-I
Let's get some technical points out of the way so we can show how to use this class. 
A SynchronizationContext allows a thread to communicate with another thread. Suppose you have two threads, Thread1 and Thread2. 
Say, Thread1 is doing some work, and then Thread1 wishes to execute code on Thread2. One possible way to do it is to ask 
Thread2 for its SynchronizationContext object, give it to Thread1, and then Thread1 can call SynchronizationContext.
Send to execute the code on Thread2. Sounds like magic... Well, there is a something you should know. 
Not every thread has a SynchronizationContext attached to it. One thread that always has a SynchronizationContext is the UI thread.
ConfigureAwait(true)   default  when the task is done, the continuation will be serialized back to the original thread  
ConfigureAwait(false)  when the task is done, the continuation will be serialized to the thread of task
 
 
 
												**Async await in console program***


 Unfortunately, that doesn’t work (and in fact, the Visual Studio 11 compiler will reject an async Main method). 
 Remember from our intro post that an async method will return to its caller before it is complete. 
 This works perfectly in UI applications (the method just returns to the UI event loop) and 
 ASP.NET applications (the method returns off the thread but keeps the request alive).
 It doesn’t work out so well for Console programs: Main returns to the OS - so your program exits.

You can work around this by providing your own async-compatible context. 
AsyncContext is a general-purpose context that can be used to enable an asynchronous MainAsync:

the Main method can’t be async. If the Main method were async, it could return before it completed, 
causing the program to end. 
The Main method for a console application is one of the few situations where code may block on an asynchronous method.
Stephen Cleary

													***Avoid Async Void***

1)To summarize this first guideline, you should prefer async Task to async void.
 Async Task methods enable easier error-handling, composability and testability.
 The exception to this guideline is asynchronous event handlers, which must return void. 
 This exception includes methods that are logically event handlers even 
 if they’re not literally event handlers (for example, ICommand.Execute implementations).
 
													***Async All the Way  *** 

 2)shows a simple example where one method blocks on the result of an async method. 
 This code will work just fine in a console application but will deadlock when called from a GUI or ASP.NET context. 
 This behavior can be confusing, especially considering that stepping through the debugger implies that it’s the await that never completes. 
 The actual cause of the deadlock is further up the call stack when Task.Wait is called.
 The root cause of this deadlock is due to the way await handles contexts.
 By default, when an incomplete Task is awaited, the current “context” is captured and used to resume the method when the Task completes.
 This “context” is the current SynchronizationContext unless it’s null, in which case it’s the current TaskScheduler. 
 GUI and ASP.NET applications have a SynchronizationContext that permits only one chunk of code to run at a time. 
 When the await completes, it attempts to execute the remainder of the async method within the captured context. 
 But that context already has a thread in it, which is (synchronously) waiting for the async method to complete. 
 They’re each waiting for the other, causing a deadlock.

Note that console applications don’t cause this deadlock. They have a thread pool SynchronizationContext 
instead of a one-chunk-at-a-time SynchronizationContext, so when the await completes, it schedules the remainder of the async method on a thread pool thread.
 The method is able to complete, which completes its returned task, and there’s no deadlock. 
 This difference in behavior can be confusing when programmers write a test console program, 
 observe the partially async code work as expected, and then move the same code into a GUI or ASP.NET application, where it deadlocks.
 
													 ***Configure Context***

 3)You should not use ConfigureAwait when you have code after the await in the method that needs the context.
 For GUI apps, this includes any code that manipulates GUI elements, writes data-bound properties or 
 depends on a GUI-specific type such as Dispatcher/CoreDispatcher. 
 For ASP.NET apps, this includes any code that uses HttpContext.
 Current or builds an ASP.NET response, including return statements in controller actions.
 Figure 7demonstrates one common pattern in GUI apps—having an async event handler disable its control at
 the beginning of the method, perform some awaits and then re-enable its control at the end of the handler;
 the event handler can’t give up its context because it needs to re-enable its control.
 Each async method has its own context, so if one async method calls another async method, their contexts are independent.
 private async void button1_Click(object sender, EventArgs e)
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
