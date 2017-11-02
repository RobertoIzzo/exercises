using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {


            Thread t1 = new Thread(new ThreadStart(() =>
            {
                for (int i = 0; i < 30; i++)
                {
                    Console.WriteLine("t1");
                }
            }));

            t1.Start();
            //aspetta che t1 finisce
            t1.Join();
            Console.WriteLine("fine t1");
            Console.WriteLine("aspetto 3 secondi");
            Thread.Sleep(3000);
            Console.WriteLine("fine main");
            Console.ReadLine();
        }
    }
}
