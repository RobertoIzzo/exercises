using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t1 =  Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("task");
                }
            });
            t1.Wait();
            Task<int> t2 = Task.Run(() => 3).ContinueWith((i) => i.Result *5);

            Console.WriteLine("t2 result "+t2.Result);
            Task<int> t3 = Task.Run(() => 1);

            Console.WriteLine("t3 result " + t2.Result);

            Task<int> t4 = Task.Run(() => 3).ContinueWith((i) => i.Result * 5, TaskContinuationOptions.OnlyOnFaulted);
            Console.WriteLine("t4 result " + t2.Result);

            Task t6 = Task.Run(() =>
            {
                throw new Exception();
            });

            //se finisce bene fai questo
            var ok = t6.ContinueWith((i) =>
            {
                Console.WriteLine("Faulted" + i.Exception);
            }, TaskContinuationOptions.OnlyOnFaulted);


            ok.Wait();

            //child task
            Task<int[]> parentTask = new Task<int[]>(() =>
            {
                int[] results = new int[3];

                Task child1 = new Task(() => { Thread.Sleep(3000); results[0] = 0; }, TaskCreationOptions.AttachedToParent);
                Task child2 = new Task(() => { Thread.Sleep(3000); results[1] = 1; }, TaskCreationOptions.AttachedToParent);
                Task child3 = new Task(() => { Thread.Sleep(3000); results[2] = 2; }, TaskCreationOptions.AttachedToParent);

                child1.Start();
                child2.Start();
                child3.Start();
              
                return results;
            });

            parentTask.Start();

            Task finalTask = parentTask.ContinueWith(parent =>
            {
                foreach (int result in parent.Result)
                {
                    Console.WriteLine("child result=>"+result);
                }
            });

            finalTask.Wait();


            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("main");
            }
            Console.ReadLine();
        }
    }
}
