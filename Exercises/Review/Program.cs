using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Review
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start program");

            #region Cancellation token
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            Task tk1 = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.WriteLine("tk1 start");
                    Thread.Sleep(2000);
                }
            }, token);

            Console.WriteLine("digit for finish");
            Console.ReadLine();
            cts.Cancel();
            Console.WriteLine("Cancellation Requested ");


            CancellationTokenSource cts1 = new CancellationTokenSource();
            CancellationToken token1 = cts1.Token;
            Task tk2 = Task.Run(() =>
            {
                while (!token1.IsCancellationRequested)
                {
                    Console.WriteLine("tk2 start");
                    Thread.Sleep(5000);
                    token1.ThrowIfCancellationRequested();
                }
            }, token1);

            try
            {
                Console.WriteLine("press for stop task");
                Console.ReadLine();
                cts1.Cancel();
                tk2.Wait();
            }
            catch (AggregateException ex)
            {

                Console.WriteLine(ex.InnerExceptions[0].Message);
            }

            Console.ReadLine();
            #endregion // end of MyRegion

            #region parent

            Task<int[]> parent = new Task<int[]>(() =>
            {
                int[] ret = new int[2];
                Task child1 = new Task(() =>
                {
                    ret[0] = 1;
                }, TaskCreationOptions.AttachedToParent);
                child1.Start();

                Task child2 = new Task(() =>
                {
                    ret[1] = 2;
                }, TaskCreationOptions.AttachedToParent);
                child2.Start();

                return ret;
            });
            parent.Start();

            foreach (var task in parent.Result)
            {
                Console.WriteLine(task.ToString());
            }

            #endregion // end of MyRegion


            #region task and thread


            //wait and result fermano il flusso
            //join fa uguale per i threafd
            Thread th1 = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(10000);
            }));
            th1.Start();
            th1.Join();
            Console.WriteLine("hello from main");
            Console.ReadLine();
            //mentre  questi wait non bloccano
            //Task.WaitAll();
            //Task.WaitAny();
            //questo aspetta mytask per 2 sec poi chiude prg
            //Task.WaitAny(new[] { mytask }, 2000);


            int count = 0;
            Task t0 = Task.Run(() =>
            {
                Console.WriteLine(" start task");

                for (int i = 0; i < 20000; i++)
                {
                    count++;
                }
            });
            t0.Wait();


            //finche il task non è finito , il prg rimane qui...
            Console.WriteLine("count :" + count);

            Console.WriteLine("after task");
            Console.WriteLine("start main");

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("main");
            }
            #endregion // end of MyRegion

            #region continue
            //http://www.blackwasp.co.uk/ContinuationTasks.aspx
            Task<string> t1 = Task.Run(() => "ciao ");
            var t2 = t1.ContinueWith((antecedent) => antecedent.Result + "Roberto");
            var t3 = t2.ContinueWith((antecedent) => antecedent.Result + " Izzo", TaskContinuationOptions.OnlyOnRanToCompletion);
            var t4 = t3.ContinueWith((antecedent) =>
            {
                Thread.Sleep(5000);
                return antecedent.Result + "!";
            }
            , TaskContinuationOptions.OnlyOnRanToCompletion);


            //finche il task non è finito , il prg rimane qui...
            Console.WriteLine("after task");
            Console.WriteLine("result = " + t4.Result);

            Console.WriteLine("start main");

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("main");
            }

            #endregion // end of MyRegion

            Console.ReadLine();
        }
    }
}
