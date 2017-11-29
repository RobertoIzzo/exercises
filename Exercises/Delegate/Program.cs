using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    class Program
    {
        private delegate Father covariance(int a, int b);
        private delegate void controvariance(Son s);

        private delegate int calc(int a, int b);
        private delegate int calc1(int a, int b);
        private delegate int anon(int a, int b);
        static void Main(string[] args)
        {

            bool a = false;
            bool b = true;
            //xor
            bool res = a ^ b;

            Console.WriteLine(res);



            Console.WriteLine(add(1, 2));

            calc c = add;

            Console.WriteLine(c(1, 2));

            c = mult;
            Console.WriteLine(c(1, 2));


            //Multicasting delegate are executed in order, se ce ne sono 3 e la seconda  va in eccezione la terza non viene eseguita!!!!!bisogna 
            //cacharla in una lista di eccezioni e poi  fare un throw di un aggregate exception
            Console.WriteLine("Multicasting");
            calc1 c1 = add;

            Console.WriteLine(c1(1, 2));

            c1 += mult;
            Console.WriteLine(c1(1, 2));
            Console.WriteLine("method " + c1.GetInvocationList().GetLength(0));

            //il delegate non deve rispettare esattamente la firma del metodo

            //covariance il metodo può avere nel valore di ritorno la classe più derivata di quella definita nel delegate 
            covariance cov = Covariancemethod;

            //controvariance il metodo può avere nei parametri la classe meno derivata di quella definita nel delegate 
            controvariance con = Controvariancemethod;


            //lambda expression and anonymous method
            anon aaaa = (x, y) => x + y;
            aaaa += ((x, y) =>
            {
                Console.WriteLine("anon");
                return 1;

            });
            Console.WriteLine(aaaa(5, 5));

            //FUNC ACTION PREDICATE


            Func<int, int, int> f = add;

            Console.WriteLine("func "+ f(6,6));

            Action<int, int> action = (x, y) => {Console.WriteLine();};

            Predicate<int> pred = (x) => x > 1;
            //EVENTS
            //1
            Pub pub = new Pub();
            pub.OnChange = () => { Console.WriteLine("callback"); };
            pub.Raise();


            Pub1 pub1 = new Pub1();
            pub1.OnChange += () => { Console.WriteLine("callback"); };
            //pub1.OnChange = null;
            //pub1.OnChange = () => { Console.WriteLine("callback"); };
            pub1.Raise();
            Console.ReadLine();

            Pub2 pub2 = new Pub2();
            pub2.OnChange += (sender, myArgs) => { Console.WriteLine(myArgs.Value); };
            pub2.Raise();//esegue OnChange

            MyClass_Event myClassEvent = new MyClass_Event();
            myClassEvent.OnChange += DoSome;
            myClassEvent.Raise();

           
        }

       

        static void DoSome(object sender, EventArgs e)
        {
            Console.WriteLine("DoSome");
        }

        static Son Covariancemethod(int a, int b)
        {
            return new Son();
        }

        static void Controvariancemethod(Father f)
        {
        }

        static int add(int a, int b)
        {
            return a + b;
        }

        static int mult(int a, int b)
        {
            return a * b;
        }
    }

   

    class Father { }
    class Son : Father { }


    class Pub
    {
        public Action OnChange { get; set; }

        public void Raise()
        {
            OnChange?.Invoke();
        }
    }


    class Pub1
    {
        public event Action OnChange = delegate { };

        public void Raise()
        {
            OnChange();
        }
    }

    class Pub2
    {
        public event EventHandler<MyArgs> OnChange = delegate { };

        public void Raise()
        {
            OnChange(this, new MyArgs(42));
        }
    }


    class Pub3
    {
        public event EventHandler OnChange = delegate { };

        List<Exception> exceptions = new List<Exception>();

  
        public void Raise()
        {
            foreach (var item in OnChange.GetInvocationList())
            {
                try
                {
                    item.DynamicInvoke(this, EventArgs.Empty);

                }
                catch (Exception ex)
                {

                    exceptions.Add(ex);
                }
            }
            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }

        }
    }

    internal class MyArgs :EventArgs
    {
        public MyArgs(int value)
        {
            Value = value;
        }
        public int Value { get; set; }
    }

    //https://www.codeproject.com/Articles/11541/The-Simplest-C-Events-Example-Imaginable
    //https://docs.microsoft.com/en-us/dotnet/standard/events/
    //event vs callback
    public class MyClass_Event
    {
        public event EventHandler OnChange;

        public void Raise()
        {
            if (OnChange == null)
                throw new Exception("Set the event MakeMeDoWork before calling this method.");
            OnChange(this, EventArgs.Empty);
        }
    }

    public class MyClass_Callback
    {
        public void DoWork(EventHandler callback)
        {
            if (callback == null)
                throw new ArgumentException("Set the callback.", "callback"); // better design
            callback(this, EventArgs.Empty);
        }
    }

  

}
