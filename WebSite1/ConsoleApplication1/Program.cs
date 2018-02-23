using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Plugin1;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var plugins = PluginLoader.PluginLoader.Load(@"C:\Users\roberto.INT\Documents\Visual Studio 2015\Projects\WebSite1\ConsoleApplication1\plugin\");
            foreach (var plugin in plugins)
            {
                if (plugin.GetType().GetCustomAttributes(false).Any())
                {
                    foreach (var attribute in plugin.GetType().GetCustomAttributes(false))
                    {
                        Type att = attribute.GetType();
                        if (att == typeof(DeBugInfo))
                        {
                            DeBugInfo dbi = (DeBugInfo)attribute;
                            try
                            {
                                Expression<Func<int, bool>> filterExpression = null;
                                filterExpression = number => number <= 10;
                                IQueryable<int> numbers = new List<int>() {1,2,3,11}.AsQueryable();
                                if(GetTrue(numbers, filterExpression))
                                    Console.WriteLine("ci sono numeri > 10");

                                filterExpression = number => number <= 10;
                                //Console.WriteLine("Developer: {0}", dbi.Developer);
                                //Console.WriteLine("Remarks: {0}", dbi.Message);

                                if (dbi.Developer != "Developer")
                                {
                                    plugin.DoIt();
                                    MethodInfo[] methodInfo = plugin.GetType().GetMethods();
                                    foreach (var method in methodInfo)
                                    {
                                        try
                                        {
                                            method.Invoke(plugin, new object[0]);
                                            Console.WriteLine("ok " + method.Name);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("fail" + method.Name);
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }
                        }
                    }
                }
                else
                {
                    plugin.DoIt();
                    MethodInfo[] methodInfo = plugin.GetType().GetMethods();
                    foreach (var method in methodInfo)
                    {
                        try
                        {
                            method.Invoke(plugin, new object[0]);
                            Console.WriteLine("ok " + method.Name);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("fail" + method.Name);
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            Console.ReadLine();
        }

        public static bool GetTrue(IQueryable<int> numbners, Expression<Func<int, bool>> filterExpression)
        {
            return numbners.Where(filterExpression).Any();
        }

        public static IQueryable<int> GetFilter(IQueryable<int> numbners, Expression<Func<int, bool>> filterExpression)
        {
            return numbners.Where(filterExpression);
        }
    }
}
