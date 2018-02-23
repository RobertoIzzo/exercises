using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin1
{
    [DeBugInfo("Developer", Message = "do not work")]
    public class Class1 :IPlugin.IPlugin
    {
        public void DoIt()
        {
           Console.WriteLine("Doit plugin1");
        }
    }

    [AttributeUsage(AttributeTargets.Class |
                    AttributeTargets.Constructor |
                    AttributeTargets.Field |
                    AttributeTargets.Method |
                    AttributeTargets.Property,
        AllowMultiple = true)]
    public class DeBugInfo : System.Attribute
    {
        public string message;
        private string developer;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
        public string Developer
        {
            get
            {
                return developer;
            }
        }
        public DeBugInfo(string dev)
        {
            this.developer = dev;
        }
    }
}
