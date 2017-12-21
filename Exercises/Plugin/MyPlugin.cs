using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    public class Class1 : IPlugin
    {
        public string Name { get; }
        public string Dsc { get; }
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
