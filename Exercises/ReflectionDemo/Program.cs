using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Plugin;

namespace ReflectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //carico una dll 
            //questo progetto deve refernziare  Plugin
            Assembly pluginAssembly1 = Assembly.Load("Plugin");
          

            var plugins1 = from type in pluginAssembly1.GetTypes()
                          where typeof(Plugin.IPlugin).IsAssignableFrom(type) && !type.IsInterface
                          select type;

            foreach (Type pluginType in plugins1)
            {
                IPlugin plugin = Activator.CreateInstance(pluginType) as IPlugin;
                if (plugin != null)
                {
                    Console.WriteLine(plugin.Name);
                    //ciclo i metodi e li invoco
                    MethodInfo[] methodInfo = plugin.GetType().GetMethods();
                    foreach (var method in methodInfo)
                    {
                        try
                        {
                            method.Invoke(plugin, new object[] { "ciao" });
                            Console.WriteLine("ok "+method.Name);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("fail"+method.Name);
                            Console.WriteLine(ex.Message);

                        }
                    }
                }
            }

            Console.ReadLine();
        }
    }
   }


//    public interface IPlugin
//    {
//        string Name { get;}
//        string Dsc { get; }
        
//    }

//    public class Plugin : IPlugin
//    {
//        public string Name
//        {
//            get { return "My Plugin"; } 
//        }

//        public string Dsc
//        {
//            get { return "plugin"; }
//        }

//        public void ProntMessage(string message)
//        {
//            Console.WriteLine(message);
//        }
//    }
//}
