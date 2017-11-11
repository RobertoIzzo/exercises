using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynchronizingResources
{
    class Program
    {

        //lock
        //volatile disable certain compiler oprtimization (usato poco)
        private static volatile int _flag = 0;
        //interlock
        static void Main(string[] args)
        {

        object _sync = new object();

            //Concurrence
            Console.WriteLine("date " + DateTime.Now);
            var list = Enumerable.Range(0, 100000);
            List<int> ll = new List<int>();
            Parallel.ForEach(list, item =>
            {
                //senza questo ll non sarà 100000 ma meno
                lock (_sync)
                {
                    int x = item - 100;
                    ll.Add(x);
                }

            });
            Console.WriteLine("tot l " + ll.Count);
            Console.WriteLine("date " + DateTime.Now);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("date " + DateTime.Now);
            var list1 = Enumerable.Range(0, 100000);
            List<int> ll1 = new List<int>();
            foreach (var item in list1)
            {
                var x = item - 1;
                ll1.Add(x);
            }
            Console.WriteLine("tot l " + ll1.Count);
            Console.WriteLine("date " + DateTime.Now);

            //nnn non sarà zero 
            int nnn = 0;
            //var t = Task.Run(() =>
            //{
            //    for (int i = 1; i < 100000000; i++)
            //    {
            //        nnn++;
            //    }
            //});

            //for (int i = 1; i < 100000000; i++)
            //{
            //    nnn--;
            //}
            //t.Wait();
            //Console.WriteLine("nnn = " + nnn);

            //così sara zero
            //puo essere usato per operazioni semplice 
            var t = Task.Run(() =>
            {
                for (int i = 1; i < 100000000; i++)
                {
                    Interlocked.Increment(ref nnn);
                }
            });

            for (int i = 1; i < 100000000; i++)
            {
                Interlocked.Decrement(ref nnn);
            }
            t.Wait();
            Console.WriteLine("nnn = " + nnn);


            Console.ReadLine();
        }
    }
}
