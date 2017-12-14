using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LinqEx
{
    class Program
    {
        static void Main(string[] args)
        {
            //implicity variable type => var

            //obj initialization sintax new myobj{filed= val}

            //lambda expression (short for anonymous function
            Func<int, string> pippo = x => "";

            //delegate
            Func<int, string> pippo1 = delegate(int x)
            {
                return "";
            };

            //linq is based totally in extension method

            //person is anonymous type (projecton in linq)
            var person = new {name = "fra"};
        }
    }

}
