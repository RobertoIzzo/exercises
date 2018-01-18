using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;

namespace Free
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var myClass = kernel.Get<IPippo>();

            //MyClass myClass = new MyClass();
            Test testA = new Test(myClass);

            MyClass1 myClass1 = new MyClass1();
            Test testB = new Test(myClass1);
        }
    }

    class Test
    {
        public Test(IPippo pippo)
        {
            pippo.DoIt();
        }
    }

    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IPippo>().To<MyClass>();
        }
    }

    interface IPippo
    {
        void DoIt();
    }

    class MyClass :IPippo
    {
        public void DoIt()
        {
            Console.WriteLine("MyClass");
        }
    }

    class MyClass1 : IPippo
    {
        public void DoIt()
        {
            Console.WriteLine("MyClass1");

        }
    }
}
