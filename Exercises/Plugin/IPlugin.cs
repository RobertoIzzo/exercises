using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
   public interface IPlugin
    {
        string Name { get; }
        string Dsc { get; }
        void PrintMessage(string message);
    }
}
