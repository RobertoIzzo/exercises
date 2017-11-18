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
        private delegate void controvariance(Soon s);

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


            //Multicasting
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
            
            //EVENTS

            Console.ReadLine();

        }

        static Soon Covariancemethod(int a, int b)
        {
            return new Soon();
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
    class Soon : Father { }
}
