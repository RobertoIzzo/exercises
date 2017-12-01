using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly pluginAssembly = Assembly.Load("ReflectionDemo");
            Assembly pluginAssembly1 = Assembly.Load("exceptionAndType");
            var plugins = from type in pluginAssembly.GetTypes()
                where typeof (IPlugin).IsAssignableFrom(type) && !type.IsInterface
                select type;

            var plugins1 = from type in pluginAssembly1.GetTypes()
                          select type;
            foreach (Type pluginType in plugins)
            {
                IPlugin plugin = Activator.CreateInstance(pluginType) as IPlugin;
                if (plugin != null)
                {
                    Console.WriteLine(plugin.Name);
                    
                    MethodInfo[] methodInfo = plugin.GetType().GetMethods();
                    foreach (var method in methodInfo)
                    {
                        try
                        {
                            method.Invoke(plugin, new object[] { "ciao" });

                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);

                        }
                    }
                }
            }

            //foreach (Type pluginType in plugins1)
            //{
            //    object plugin = Activator.CreateInstance(pluginType);
            //    var aaaa = plugin.GetType().GetField(BindingFlags.Instance.ToString());
            //}

            Console.ReadLine();
        }
    }


    public interface IPlugin
    {
        string Name { get;}
        string Dsc { get; }
        
    }

    public class Plugin : IPlugin
    {
        public string Name
        {
            get { return "My Plugin"; } 
        }

        public string Dsc
        {
            get { return "plugin"; }
        }

        public void ProntMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
