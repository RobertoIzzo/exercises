using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinqEx
{
    class Program
    {
        static List<int> MyList = new List<int>();

        static void Main(string[] args)
        {

            #region Linq
            //implicity variable type => var

            //obj initialization sintax new myobj{filed= val}

            //lambda expression (short for anonymous function
            Func<int, string> pippo = x => "";

            //delegate
            Func<int, string> pippo1 = delegate (int x)
            {
                return "";
            };

            //linq is based totally in extension method

            //person is anonymous type (projecton in linq)
            var person = new { name = "fra" };

            int[] data = { 1, 2, 5, 8, 11 };
            var result = from d in data
                         where d % 2 == 0
                         orderby d descending
                         select d;

            foreach (int i in data)
            {
                Console.WriteLine(i);
            }

            var result1 = data.Where(x => x % 2 == 0);

            int[] datax = { 2, 8 };
            int[] datay = { 1, 5, 11 };
            var resultc = from x in datax
                          from y in datay
                          select x * y;
            List<Order> orders = new List<Order>();

            OrderLine ol1 = new OrderLine();
            OrderLine ol2 = new OrderLine();
            OrderLine ol3 = new OrderLine();
            ol1.Product = new Product()
            {
                Dsc = "pippo"
            };
            ol2.Product = new Product()
            {
                Dsc = "pippo"
            };
            ol2.Product = new Product()
            {
                Dsc = "pluto"
            };
            Order o1 = new Order();
            Order o2 = new Order();
            o1.OrderLines = new List<OrderLine>();
            o2.OrderLines = new List<OrderLine>();
            o1.OrderLines.Add(ol1);
            o1.OrderLines.Add(ol2);
            o2.OrderLines.Add(ol3);
            orders.Add(o1);
            orders.Add(o2);
            var average = orders.Average(o => o.OrderLines.Count);

            Console.WriteLine("average = " + average);

            //grouping and projecton
            var resultd = from o in orders
                          from ol in o.OrderLines
                          group ol by ol.Product
                          into p
                          select new
                          {
                              dsc = p.Key
                          };

            foreach (var item in resultd)
            {
                Console.WriteLine(item.dsc);
            }

            string[] dataw = { "roberto", "massimo", "francesco" };
            string[] datae = {"roberto","massimo" };

            var joinresut = from s in dataw
                join s1 in datae on s equals s1
                select s;
            foreach (var item in joinresut)
            {
                Console.WriteLine("join=>"+item);
            }

            #endregion

            #region YIELD 
            FillValues(); // Fills the list with 5 values
            var resultb = FilterWithoutYield();

            /*                                                     Yield
             * 
             * Si utilizza un'istruzione yield return per restituire un elemento alla volta.
             * 
             * 
             * Il tipo restituito deve essere IEnumerable, IEnumerable<T>, IEnumerator o IEnumerator<T>.
            Yield keyword helps us to do custom stateful iteration over.NET collections.
            There are two scenarios where “yield” keyword is useful:-
            Customized iteration through a collection without creating a temporary collection. (prendere elementi secondo una condizione senza la varibile temporanea
            Stateful iteration. (ritorna al caller subito il promo item della lista
            Con yield mi ritorna subito il primo risultato, posso farci , qualcosa e quando il foreach riprende il controllo riparte dal secondo
            */
            var resulta = FilterWithYield();


            foreach (var item in FilterWithYield())
            {
                Console.WriteLine("temp count : " + item);
            }


            #endregion



            Console.ReadLine();

        }

        static IEnumerable<int> FilterWithoutYield()
        {
            List<int> temp = new List<int>();
            foreach (int i in MyList)
            {
                if (i > 3)
                {
                    temp.Add(i);
                }
            }
            return temp;
        }

        static IEnumerable<int> FilterWithYield()
        {
            int count = 0;
            foreach (int i in MyList)
            {
                count += i;
                if (i > 3) yield return i;
            }
        }



        static void FillValues()
        {
            MyList.Add(1);
            MyList.Add(2);
            MyList.Add(3);
            MyList.Add(4);
            MyList.Add(5);
            MyList.Add(6);
            MyList.Add(7);
            MyList.Add(8);
            MyList.Add(9);
        }
    }

    public class Product
    {
        public string Dsc { get; set; }
        public decimal Price { get; set; }
    }
    public class OrderLine
    {
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
    public class Order
    {
        public List<OrderLine> OrderLines { get; set; }
    }
}
