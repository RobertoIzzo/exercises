using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace summary
{
    class Program
    {
        static void Main(string[] args)
        {
            //1
            //Thread t1 = new Thread(new ThreadStart(() =>
            //{
            //    Console.WriteLine("Thread start");
            //    Thread.Sleep(10000);
            //    Console.WriteLine("Thread end");
            //}));
            //t1.Start();
            //t1.Join();

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("tmanin");
            //}

            //2
            //Task<int> tk1 = Task.Run(() =>
            //{
            //    Console.WriteLine("tk1 start");
            //    Thread.Sleep(10000);
            //    Console.WriteLine("tk1 end");
            //    return 2;
            //});
            //var a = tk1.Result;


            //3
            //Task tk1 = Task.Run(() =>
            //{
            //    Console.WriteLine("tk1 start");
            //    Thread.Sleep(10000);
            //    Console.WriteLine("tk1 end");
            //});
            //tk1.Wait();

            //4
            //CancellationTokenSource cts = new CancellationTokenSource();
            //CancellationToken token = cts.Token;

            //Task tk1 = Task.Run(() =>
            //{
            //    while (!token.IsCancellationRequested)
            //    {
            //        Console.WriteLine("tk1 start");
            //        Thread.Sleep(2000);
            //    }
            //}, token);

            //Console.WriteLine("digit for finish");

            //Console.ReadLine();
            //cts.Cancel();
            //Console.WriteLine("Cancellation Requested ");

            //il main aspetta solo per 2 sec poi chiude , mentre il t0 dura 8 sec
            //Task tk0 = Task.Run(() =>
            //{
            //    Console.WriteLine("tk0 start");

            //    Thread.Sleep(8000);
            //});
            //Task.WaitAny(new[] { tk0 }, 2000);
            //Console.WriteLine("tk0 timeout");
            //Console.ReadLine();


            //5 signal outside that the task have been canceled use ThrowIfCancellationRequested
            //CancellationTokenSource cts = new CancellationTokenSource();
            //CancellationToken token = cts.Token;

            //Task tk1 = Task.Run(() =>
            //{
            //    while (!token.IsCancellationRequested)
            //    {
            //        Console.WriteLine("tk1 start");
            //        Thread.Sleep(2000);
            //        token.ThrowIfCancellationRequested();
            //    }
            //}, token);

            //try
            //{
            //    Console.WriteLine("press for stop task");
            //    Console.ReadLine();
            //    cts.Cancel();
            //    tk1.Wait();
            //}
            //catch (AggregateException ex)
            //{

            //    Console.WriteLine(ex.InnerExceptions[0].Message);
            //}

            //Console.WriteLine("press for close app");
            //Console.ReadLine();

            //6
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            Task tk1 = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.WriteLine("tk1 start");
                    Thread.Sleep(2000);
                }

                throw new OperationCanceledException();
            }, token).ContinueWith((t) =>
            {
                t.Exception?.Handle((e) => true);
                Console.WriteLine("you have canceled a task");
            }, TaskContinuationOptions.OnlyOnCanceled);


            Console.ReadLine();



        }
    }
}
