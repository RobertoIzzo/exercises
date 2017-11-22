using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAndThread
{
    class Program
    {
        static void Main(string[] args)
        {
            //aspetta 10 secondi e il programa termina
            //se metto join non scrive hello prima dei 10 sec
            Thread t1 = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(10000);
            }));
            t1.Start();
            t1.Join();
            Console.WriteLine("hello from main");

            //se non metto t.wait oppure t.result  NON ASPETTA != SEC !!!!
            //Tasks are executed by threads which
            //are coming from the system thread pool.
            //A thread that comes from thread pool is executed in background by default.!!!
            Task tk1 = Task.Run(() =>
            {
                Thread.Sleep(10000);
            });
            tk1.Wait();
            Task<int> tk0 = Task.Run(() =>
             {

                 Thread.Sleep(9000);
                 Console.WriteLine("tk0");
                 return 1;
             });
            Task<int> tk2 = Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("tk2");
                return 2;
            });
            Task<int> tk3 = Task.Run(() =>
            {
                Thread.Sleep(6000);
                Console.WriteLine("tk3");
                return 3;
            });
            //questi wait non bloccano
            //Task.WaitAll();
            //Task.WaitAny();
            //questo si
            tk3.Wait();

            Console.WriteLine("end main");

            Console.ReadLine();

        }
    }
}
