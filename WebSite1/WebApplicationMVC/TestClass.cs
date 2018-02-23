using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApplicationMVC
{
    public class TestClass
    {
        public async Task<string> DoLongTask()
        {
            string res = await DoIt();
            Console.WriteLine("xxx");
            return res;
        }

        private Task<string> DoIt()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(10000);
                return "";
            });
        }
    }
}