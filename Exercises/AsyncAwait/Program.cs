using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait
{
    //use for i/o operation that take  a lot time
    //async await  just maske more responsive the application , not more performance
    class Program
    {
        static void Main(string[] args)
        {
            string res = Doit().Result;
            Console.WriteLine(res);
            Console.ReadLine();
        }


        public static async Task<string> Doit()
        {
            Console.WriteLine("chiamo Download");
            string res = await Download();
            Console.WriteLine("ho chiamato Download");
            return res;
        }


        public static async Task<string> Download()
        {
            using (HttpClient client = new HttpClient())
            {
                string res = await client.GetStringAsync("http://www.microsoft.it");
                return res;
            }
        }
    }
}
