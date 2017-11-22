using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace exceptionAndType
{
    class Program
    {
        static void Main(string[] args)
        {
            //extension method non nestes , non generic static class
            string pippo = "Pippo";
            Console.WriteLine(pippo + " to lower => " + pippo.GetLower());
            Console.ReadLine();

            #region TYPES

            // special Value type Enum
            Importance value = Importance.Critical;

            if (value == Importance.Trivial)
            {
                Console.WriteLine("Not true");
            }

            if ((int)value == 0)
            {
                Console.WriteLine("none");
            }

            //Value type
            Point p = new Point(1, 2);

            //refernce type
            Test t = new Test();
            t.DoSomething(1, 2);
            var fielfStatic = Test.FieldStatic;

            //indexer
            List myList = new List();
            var card = myList[0];
            Console.WriteLine(card.segno);
            #endregion

            #region EXCEPTION
            try
            {
                int i = int.Parse("ciao");
            }
            catch (Exception ex)
            {
                //lose stacktrace
                throw new ArgumentException();
            }

            try
            {
                int i = int.Parse("ciao");
            }
            catch (Exception ex)
            {
                //retrhow original exception whit some more message
                throw new ArgumentException("message ", "paramname", ex);
            }

            try
            {
                int i = int.Parse("ciao");

            }
            catch (Exception ex)
            {
                //log(ex)
                throw; //retrhow original exception
            }

            ExceptionDispatchInfo possibleex = null;
            try
            {
                int i = int.Parse("ciao");
            }
            catch (Exception ex)
            {
                //is possible catch in one thread 
                possibleex = ExceptionDispatchInfo.Capture(ex);
            }
            //and throw in another trhead es async await
            possibleex?.Throw();

            try
            {
                int i = int.Parse("42");
                if (i == 42) Environment.FailFast("ha inserito 42");
            }
            finally
            {
                Console.WriteLine("non ci arrivo qui");
            }
            #endregion

            //Generic
            Myclass<Card> mc = new Myclass<Card>();
            Console.WriteLine(mc.MyProperty.numero);

            //Conversion



            //Dynamic



            Console.ReadLine();

        }

        // special Value type Enum
        enum Importance
        {
            None = 0,
            Trivial = 1,
            Regular = 2,
            Important = 3,
            Critical = 4
        };

        //Generics Method
        public T MyGenericMethod<T>()
        {
            var defaultValue = default(T);
            return defaultValue;
        }
    }

    //Generics Interface

    //interface covariant
    interface IInterface1<out T> where T :class
    {
        T MyGenericMethod();
    }

    //interface controvariant
    interface IInterface2<in T> where T : class
    {
        void MyGenericMethod(T value);
    }

    //interface  covariant / controvariant
    interface IInterface3<out T, in T1> where T : class
    {
        T MyGenericMethod(T1 value);
    }

    //interface  
    interface IInterface4<T> where T : class
    {
        T MyGenericMethod(T value);
    }

    //generic class
    class Myclass<T> where T : class, new()
    {
        public Myclass()
        {
            MyProperty = new T();
        }

        public T MyProperty { get; set; }
    }

    //Generics Nullable
    struct Nullable<T> where T : struct
    {
        private T _value;
        private bool _hasValue;

        public Nullable(T value)
        {
            _value = value;
            _hasValue = true;
        }

        public T Value
        {
            get
            {
                if (!_hasValue) throw new ArgumentException();
                return _value;
            }
        }

        public T GetValueOrDefault()
        {
            return _value;
        }
    }

    //extension
    public static class MyExtension
    {
        public static string GetLower(this string arg)
        {
            return arg.ToLower();
        }
    }

    
    //Interface

    //Abstract


    //Inheritance 
    public class Animale
    {
        public virtual void faverso()
        {
            Console.WriteLine("verso animale");
        }

        public virtual void getTipo()
        {
            Console.WriteLine("tipo animale");
        }

        public virtual void cammina()
        {
            Console.WriteLine(" cammina animale");
        }

        public virtual void mangia()
        {
            Console.WriteLine(" mangia animale");
        }

        public virtual void dormi(int ore)
        {
            Console.WriteLine("  animale dorme "+ ore);
        }
    }

    public sealed class Cane : Animale
    {
        //hide base case sensitive
        public void faverso()
        {
            Console.WriteLine("verso Cane");
        }

        //new
        public new void getTipo()
        {
            Console.WriteLine("tipo Cane");
        }

        public override void cammina()
        {
            Console.WriteLine(" cammina Cane");
        }

        public void mangia(string cibo)
        {
            Console.WriteLine(" il  Cane mangia :" + cibo);
        }
        //error compile
        //public override void dormi()
        //{
        //    Console.WriteLine(" dormi Cane");
        //}
    }

    //SEALED Cane
    //public class caneLupo : Cane
    //{

    //}

    public class Father
    {
        private int _filed1;
        public Father(int filed1)
        {
            _filed1 = filed1;
        }
    }

    public class Soon : Father
    {
        private int _filed2;

        public Soon(int filed1, int field2)
            : base(filed1)
        {
            _filed2 = field2;
        }
    }

    public class Test1
    {
        private int _filed1;
        private int _filed2;
        private int _filed3;
        public int Filed4 { get; set; }

        public int Filed3
        {
            get
            {
                return _filed3;
            }

            set
            {
                _filed3 = value;
            }
        }
        public Test1(int filed1)
        {
            _filed1 = filed1;
        }

        public Test1(int filed1, int field2)
            : this(filed1)
        {
            _filed2 = field2;
        }
    }

    //Value type
    public struct Point
    {
        public int x, y;

        public Point(int p1, int p2)
        {
            x = p1;
            y = p2;
        }
    }

    class List
    {
        public List()
        {
            cards.Add(new Card()
            {
                segno = "cuori"
            });

        }
        private List<Card> cards = new List<Card>();

        public Card this[int i]
        {
            get
            {
                return cards[i];
            }
        }
    }

    class Card
    {

        public string segno;
        public int numero;
    }

    //refernce type
    public class Test
    {
        public int Field;
        public static int FieldStatic;
        public int DoSomething(int a, int b, int c = 3)
        {
            return a + b;
        }
        public int DoSomething(int a, int b, int d, int c = 3)
        {
            return a + b;
        }
    }

    [Serializable]
    public class MyException : Exception, ISerializable
    {
        public MyException(int myid, string message)
            : base(message)
        {

        }

        public MyException(string message, Exception inException)
           : base(message, inException)
        {

        }
    }
}
