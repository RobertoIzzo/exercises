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
            //Thread t1 = new Thread(new ThreadStart(() =>
            //{
            //    Thread.Sleep(10000);
            //}));
            //t1.Start();
            //t1.Join();
            //Console.WriteLine("hello from main");

            //se non metto t.wait oppure t.result  NON ASPETTA != SEC !!!!
            //Tasks are executed by threads which 
            //are coming from the system thread pool.
            //A thread that comes from thread pool is executed in background by default.!!!
           //Task tk1 = Task.Run(() =>
           //{
           //    Thread.Sleep(10000);
           //});
           //tk1.Wait();
           Task <int> tk1 = Task.Run(() =>
            {
                Thread.Sleep(10000);
                return 3;
            } );
            

            Console.WriteLine("hello from main , task result"+ tk1.Result);

        }
    }
}
