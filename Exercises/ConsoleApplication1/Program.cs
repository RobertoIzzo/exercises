using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {   
        [ThreadStatic]
        private static int _count = 5;
        static void Main(string[] args)
        {
            Thread t1 = new Thread(new ThreadStart(() =>
            {
                for (int i = 0; i < _count; i++)
                {
                    Console.WriteLine("t1");
                }
            }));
            t1.Start();

            Thread t2 = new Thread(new ThreadStart(() =>
            {
                for (int i = 0; i < _count; i++)
                {
                    Console.WriteLine("t2");
                }
            }));
            t2.Start();

            Console.ReadLine();
        }
    }
}
