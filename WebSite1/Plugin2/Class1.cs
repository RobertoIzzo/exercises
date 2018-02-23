using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin2
{
    public class Class1 :IPlugin.IPlugin

    {
        public void DoIt()
        {
            Console.WriteLine("Doit plugin2");
        }
    }
}
