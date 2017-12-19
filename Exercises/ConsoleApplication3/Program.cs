using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        //ThreadLocal inizializza sempre dal valore che gli dai
        //per ogni thread _count inizia da 5
        //la differenza con thredstatic è che tlocal inizilizza sempre la variabile
        /*
        Thread-local storage (TLS) is a computer programming method that uses static or global memory local to a thread. 
        All threads of a process share the virtual address space of the process.
        The local variables of a function are unique to each thread that runs the function. 
        However, the static and global variables are shared by all threads in the process. 
        With thread local storage (TLS), you can provide unique data for each thread that the process can access using a global index.
        One thread allocates the index, which can be used by the other threads to retrieve the unique data associated with the index.
        In the .NET Framework version 4, you can use the System.Threading.ThreadLocal<T> class to create thread-local objects.
        */
        
        
        public static ThreadLocal<int> _count =
        new ThreadLocal<int>(() => 5);
      


        static void Main(string[] args)
        {
            Thread t1 = new Thread(new ThreadStart(DoSomeThing));
            t1.Start();
            for (int i = 0; i < 5; i++)
            {
                _count.Value++;
                Console.WriteLine("main _count " + _count);
            }


            ThreadPool.QueueUserWorkItem(ThreadProc);

            Console.ReadKey();
        }
        //https://msdn.microsoft.com/it-it/library/0ka9477y(v=vs.110).aspx
        //http://www.c-sharpcorner.com/UploadFile/1d42da/threading-pooling-in-C-Sharp/
        private static void ThreadProc(object state)
        {
            
            Console.WriteLine("Hello from the thread pool.");
        }

        private static void DoSomeThing()
        {
            for (int i = 0; i < 5; i++)
            {
                _count.Value++;
                Console.WriteLine("dosomething _count " + _count);
                //Thread.Sleep(10000);
            }

        }
    }
}

