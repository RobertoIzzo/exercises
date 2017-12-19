using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Program
    {
        //ThreadStatic inizializza da 5 il primo thread , l'altro invece inizializza 
        //da valore di default
        //la variabile è statica per ogni thread
        static bool basta = false;
        [ThreadStatic]
        private static int _count = 5;
        static void Main(string[] args)
        {
          
            Thread t1 = new Thread(new ThreadStart(DoSomeThing));

            //t1.IsBackground = false;
            t1.Start();
            //t1.Join();//blocca thread corrente (main) ed esegue tutto thread DoSomeThing
            for (int i = 0; i < 5; i++)
            {
                _count++;
                Console.WriteLine("main _count "+ _count);
            }

            Thread t2 = new Thread(new ParameterizedThreadStart(DoSomeThingPara));

            t2.Start(5);

            //Thread t3 = new Thread(new ThreadStart(cazzarola));
            //t3.Start();
            Console.ReadLine();
            basta = true;
            Console.WriteLine("pippo terminato");
            Console.WriteLine("_count :"+ _count);
            Console.ReadLine();

        }

        private static void pippo()
        {
            while (!basta)
            {
                Console.WriteLine("cazzarola");
            }
        }

        private static void DoSomeThing()
        {
            for (int i = 0; i < 5; i++)
            {
                _count++;
                Console.WriteLine("dosomething _count "+ _count);
                //Thread.Sleep(10000);
            }
           
        }

        private static void DoSomeThingPara(object o)
        {
            for (int i = 0; i < (int)o; i++)
            {
                Console.WriteLine("DoSomeThingPara wiht  "+(int)o);
                //Thread.Sleep(10000);
            }

        }
    }
}
