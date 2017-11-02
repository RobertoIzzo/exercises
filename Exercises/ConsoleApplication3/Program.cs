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

            Console.ReadKey();
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

