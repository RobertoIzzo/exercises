using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plink
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //parallel language integrated query PLINQ 
            //parallel query 
            var list11 = Enumerable.Range(0, 10);
            var enumerable = list11 as int[] ?? list11.ToArray();
            var listq = enumerable.Reverse();
            //not order result
            var ss = listq.AsParallel().Where(i => i < 10).ToArray();
            foreach (var item in ss)
            {
                Console.WriteLine(item);
            }
            //order result
            ss = listq.AsParallel().AsOrdered().Where(i => i < 10).ToArray();
            foreach (var item in ss)
            {
                Console.WriteLine(item); 
            }
            //aggregate exception


            var agg = Enumerable.Range(0, 10);
            try
            {
                var aaa = agg.AsParallel().Where(Check);
                aaa.ForAll(Console.WriteLine);
            }
            catch (AggregateException ae)
            {

                Console.WriteLine("ex : {0}" ,ae.InnerExceptions.Count);
            }

            //Concurrent collection
            //BlockingCollection
            //ConcurrentBag
            //Concurrenttask 
            //ConcurrentQueue
            //ConcurrentDictionary
            Console.ReadLine();

        }

        private static bool Check(int arg)
        {
            throw new ArgumentException();
        }
    }
}
