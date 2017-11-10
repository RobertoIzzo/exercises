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
        private static void Main(string[] args)
        {
            Task t1 = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("task");
                }
            });
            t1.Wait();

            Task<int> t2 = Task.Run(() => 3).ContinueWith((i) => i.Result * 5);

            Console.WriteLine("t2 result " + t2.Result);
            Task<int> t3 = Task.Run(() => 1);

            Console.WriteLine("t3 result " + t2.Result);

            Task<int> t4 = Task.Run(() => 3).ContinueWith((i) => i.Result * 5, TaskContinuationOptions.OnlyOnRanToCompletion);
            Console.WriteLine("t4 result " + t2.Result);

            Task t6 = Task.Run(() =>
            {
                throw new Exception();
            });

            var ok = t6.ContinueWith((i) =>
            {
                Console.WriteLine("Faulted" + i.Exception);
            }, TaskContinuationOptions.OnlyOnFaulted);


            ok.Wait();

            //child task
            Task<int[]> parentTask = new Task<int[]>(() =>
            {
                int[] results = new int[3];

                Task child1 = new Task(() =>
                {
                    Thread.Sleep(3000);
                    results[0] = 0;
                }, TaskCreationOptions.AttachedToParent);

                Task child2 = new Task(() =>
                {
                    Thread.Sleep(3000);
                    results[1] = 1;
                }, TaskCreationOptions.AttachedToParent);

                Task child3 = new Task(() =>
                {
                    Thread.Sleep(3000);
                    results[2] = 2;
                }, TaskCreationOptions.AttachedToParent);

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
                    Console.WriteLine("child result=>" + result);
                }
            });

            finalTask.Wait();

            // 5 Method for create task

            //0
            Task tx = Task.Run(() => Console.WriteLine("task"));

            //1 Factory
            Task.Factory.StartNew(() => { Console.WriteLine("Hello Task library!"); });
            
            // 2 Using Action
            Task task0 = new Task(new Action(PrintMessage));
            task0.Start();


            //  3 Using a delegate
            Task task8 = new Task(delegate { PrintMessage(); });
            task8.Start();
            
            //4Lambda and named method
            Task task9 = new Task(() => PrintMessage());
            task9.Start();
            
            //5Lambda and anonymous method
            Task task11 = new Task(() => { Console.WriteLine("Hello Task library!"); });
            task11.Start();


            Task task12 = new Task(PrintMessage);
            task12.Start();
           
        }

        private static void PrintMessage()
        {
            Console.WriteLine("Hello Task library!");
        }

        //Using Task.Run in .NET4.5
        public async Task DoWork()
        {
            await Task.Run(() => PrintMessage());
        }

        //Using Task.FromResult in .NET4.5 to return a result from a Task
        public async Task DoWork1()
        {
            int res = await Task.FromResult<int>(GetSum(4, 5));
        }

        private int GetSum(int i, int i1)
        {
            return i + i1;
        }



    }
}
