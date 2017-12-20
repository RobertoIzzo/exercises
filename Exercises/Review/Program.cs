using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Review
{
    class Program
    {
        //la differenza con thredstatic è che tlocal inizilizza sempre la variabile
        public static ThreadLocal<int> _count = new ThreadLocal<int>(() => 5);

        [ThreadStatic]
        private static int _count1 = 5;
        static void Main(string[] args)
        {
            Console.WriteLine("inizio");

            #region Cancellation token
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            Task tk1 = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.WriteLine("tk1 start");
                    Thread.Sleep(2000);
                }
            }, token);

            Console.WriteLine("digit for finish");
            //Console.ReadLine();
            cts.Cancel();
            Console.WriteLine("Cancellation Requested ");


            CancellationTokenSource cts1 = new CancellationTokenSource();
            CancellationToken token1 = cts1.Token;
            Task tk2 = Task.Run(() =>
            {
                while (!token1.IsCancellationRequested)
                {
                    Console.WriteLine("tk2 start");
                    Thread.Sleep(5000);
                    token1.ThrowIfCancellationRequested();
                }
            }, token1);

            try
            {
                Console.WriteLine("press for stop task");
                //Console.ReadLine();
                cts1.Cancel();
                tk2.Wait();
            }
            catch (AggregateException ex)
            {

                Console.WriteLine(ex.InnerExceptions[0].Message);
            }

            //Console.ReadLine();
            #endregion // end of MyRegion

            #region parent

            Task<int[]> parent = new Task<int[]>(() =>
            {
                int[] ret = new int[2];
                Task child1 = new Task(() =>
                {
                    ret[0] = 1;
                }, TaskCreationOptions.AttachedToParent);
                child1.Start();

                Task child2 = new Task(() =>
                {
                    ret[1] = 2;
                }, TaskCreationOptions.AttachedToParent);
                child2.Start();

                return ret;
            });
            parent.Start();

            foreach (var task in parent.Result)
            {
                Console.WriteLine(task.ToString());
            }

            #endregion // end of MyRegion

            #region task and thread


            //wait and result fermano il flusso
            //join fa uguale per i threafd
            Thread th1 = new Thread(new ThreadStart(() =>
            {
                _count.Value++;
                _count1++;
                Console.WriteLine("static"+_count1);
                Console.WriteLine("local" + _count);

                Thread.Sleep(10000);
            }));
            Thread th2 = new Thread(new ThreadStart(() =>
            {
                _count.Value++;
                _count1++;
                Console.WriteLine("static" + _count1);
                Console.WriteLine("local" + _count);
                Thread.Sleep(10000);
            }));
            th1.Start();
            th2.Start();
            th1.Join();
            th2.Join();
            Console.WriteLine("hello from main");
            //Console.ReadLine();
            //mentre  questi wait non bloccano
            //Task.WaitAll();
            //Task.WaitAny();
            //questo aspetta mytask per 2 sec poi chiude prg
            //Task.WaitAny(new[] { mytask }, 2000);


            int count = 0;
            Task t0 = Task.Run(() =>
            {
                Console.WriteLine(" start task");

                for (int i = 0; i < 20000; i++)
                {
                    count++;
                }
            });
            t0.Wait();


            //finche il task non è finito , il prg rimane qui...
            Console.WriteLine("count :" + count);

            Console.WriteLine("after task");
            Console.WriteLine("start main");

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("main");
            }
            #endregion // end of MyRegion

            #region continue
            //http://www.blackwasp.co.uk/ContinuationTasks.aspx
            Task<string> t1 = Task.Run(() => "ciao ");
            var t2 = t1.ContinueWith((antecedent) => antecedent.Result + "Roberto");
            var t3 = t2.ContinueWith((antecedent) => antecedent.Result + " Izzo", TaskContinuationOptions.OnlyOnRanToCompletion);
            var t4 = t3.ContinueWith((antecedent) =>
            {
                Thread.Sleep(5000);
                return antecedent.Result + "!";
            }
            , TaskContinuationOptions.OnlyOnRanToCompletion);


            //finche il task non è finito , il prg rimane qui...
            Console.WriteLine("after task");
            Console.WriteLine("result = " + t4.Result);

            Console.WriteLine("start main");

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("main");
            }

            #endregion // end of MyRegion

            #region Threadsafe

            int nnn = 0;
            var t = Task.Run(() =>
            {
                for (int i = 1; i < 100000000; i++)
                {
                    Interlocked.Increment(ref nnn);
                }
            });

            for (int i = 1; i < 100000000; i++)
            {
                Interlocked.Decrement(ref nnn);
            }
            t.Wait();
            Console.WriteLine("fine");

            object _sync = new object();

            //Concurrence
            Console.WriteLine("date " + DateTime.Now);
            var list = Enumerable.Range(0, 100000);
            List<int> ll = new List<int>();
            Parallel.ForEach(list, item =>
            {
                //senza questo ll non sarà 100000 ma meno
                lock (_sync)
                {
                    int x = item - 100;
                    ll.Add(x);
                }

            });

            #endregion // end of MyRegion

            #region TODO
            //plinq and Concurrent collection
            //async await
            //garbage collector
            //dynamic
            //IComparable
            //IEnumerable
            //IDisposable
            //IUnknown
            //IFormattable
            //IFormatProvider 

            #endregion // end of MyRegion

            #region Exception
            try
            {
                int i = int.Parse("ciao");
            }
            catch (MyException myex)
            {
                //log()
                //retrhow original exception whit some more message
                throw new ArgumentException("MyException ", "paramname", myex);
            }
            catch (Exception ex)
            {
                //log()
                //retrhow original exception whit some more message
                throw new ArgumentException("message ", "paramname", ex);
            }
            finally
            {
                Importance value = Importance.Critical;
                Console.WriteLine("Importance : " + value.ToString().GetLower());
            }

            #endregion // end of MyRegion

            #region Generics Method
            /*
             * https://docs.microsoft.com/it-it/dotnet/csharp/programming-guide/generics/
            I generics sono stati aggiunti alla versione 2.0 del linguaggio C# e di Common Language Runtime (CLR).
            I generics introducono in .NET Framework il concetto dei parametri di tipo, che consentono di progettare classi e metodi 
            che rinviano la specifica di uno o più tipi finché non si dichiara la classe o il metodo e si crea un'istanza dal codice client. 
            Ad esempio, usando un parametro di tipo generico T è possibile scrivere un'unica classe che un altro codice client può usare 
            senza rischiare cast di runtime o operazioni di boxing

            -Usare i tipi generici per ottimizzare il riutilizzo del codice, l'indipendenza dai tipi e le prestazioni.
            -L'uso più comune dei generics consiste nel creare classi di raccolte.
               La libreria di classi .NET Framework contiene diverse nuove classi di raccolte generiche nello spazio dei nomi System.Collections.Generic.
            -È possibile creare interfacce, classi, metodi, eventi e delegati generici.
            -Le classi generiche possono essere limitate in modo da abilitare l'accesso ai metodi per particolari tipi di dati.
            -Le informazioni sui tipi usati in un tipo di dati generico possono essere ottenute usando la reflection in fase di esecuzione

            where T : struct  – Type argument must be a value type
            where T : class – Type argument must be a reference type
            where T : new() – Type argument must have a public parameterless constructor.
            where T : <base class> – Type argument must inherit from<base class> class.
            where T : <interface> –  Type argument must implement from<interface> interface.
            where T : U – There are two type arguments T and U.T must be inherit from U.
            */

            var res = GetData(3);
            /*
             * C# is a strongly typed language, but in C# 3.0 a new feature was introduced  to minimize the impact of being a strongly typed language.
             * The feature is called type inference and the keyword used is the "var" keyword.
             * Ai metodi statici e ai metodi di istanza si applicano le stesse regole relative all'inferenza del tipo. 
             * Il compilatore può dedurre i parametri di tipo in base agli argomenti metodo passati, 
             * ma NON può dedurli solo da un vincolo o da un valore restituito. 
             * Di conseguenza, l'inferenza del tipo NON funziona con metodi che non includono parametri.
             * L'inferenza del tipo avviene in fase di compilazione prima che il compilatore provi a risolvere le firme dei metodi di overload. 
             * Il compilatore applica la logica di inferenza del tipo a tutti i metodi generici che condividono lo stesso nome. 
             * Nel passaggio di risoluzione dell'overload, il compilatore include solo i metodi generici per cui l'inferenza del tipo è riuscita.
             */
            //i parametri di tipo <int>, <string> etc NON SONO OBBLIGATORI...
            var res0 = GetQueryString<int>("3", Int32.MinValue);
            var res1 = GetQueryString<string>("ciao", String.Empty);
            var res2 = GetQueryString<DateTime>("2017/12/19", DateTime.MinValue);

            //qui si invece...
            var res3 = MyConvert<int>("3");
            var res4 = MyConvert<string>("ciao");
            var res5 = MyConvert<DateTime>("2017/12/19");
            #endregion // end of MyRegion

            Myclass1<string> testMyclass = new Myclass1<string>();
            var result =testMyclass.Compare("ciao", "cia");
            Myclass1<int> testMyclass1 = new Myclass1<int>();
            var result1 = testMyclass1.Compare(3, 4);
            Myclass1<string> testMyclass2 = new Myclass1<string>();
            var result2 = testMyclass2.Compare("ciao", "ciao");

            //todo class interface generic, delegate ,reflection and much more...

            Console.ReadLine();
        }
        public static T GetData<T>(T obj)
        {
            T result;
            try
            {
                result = (T)Convert.ChangeType(obj, obj.GetType());
                Console.WriteLine("INSIDE GetData<T>," + obj.GetType().Name);

            }
            catch
            {
                //se non riesco a convertirlo uso il valore di default empty se stringa 
                result = default(T);
            }

            return result;
        }

        public static T MyConvert<T>(string key)
        {
            T result;
            try
            {
                result = (T)Convert.ChangeType(key, typeof(T));
            }
            catch
            {
                //se non riesco a convertirlo uso il valore di default empty se stringa 
                result = default(T);
            }

            return result;
        }

        public static T GetQueryString<T>(string key, T defaultValue)
        {
            T result;
            try
            {
                result = (T)Convert.ChangeType(key, typeof(T));
            }
            catch
            {
                //se non riesco a convertirlo uso il valore di default empty se stringa 
                result = defaultValue;
            }

            return result;

        }

        public static T GetQueryString<T, T1>(string key, T defaultValue, T1 defaultValue1)
        {
            T result;
            try
            {
                result = (T)Convert.ChangeType(key, typeof(T));
            }
            catch
            {
                //se non riesco a convertirlo uso il valore di default empty se stringa 
                result = defaultValue;
            }

            return result;

        }
       
    }

    //interface  covariant / controvariant
    interface IInterface3<out T, in T1> where T : class
    {
        T MyGenericMethod(T1 value);
    }

    //generic class
    /*
    In type theory, a the type T is greater (>) than type S if S is a subtype (derives from) T, 
    which means that there is a quantitative description for types in a type hierarchy.

        n C# (and .NET), variance is a relation between a generic type definition and a particular generic type parameter.

    Given two types Base and Derived, such that:

    There is a reference (or identity) conversion between Base and Derived
    Base ≥ Derived
    A generic type definition Generic<T> is:

    -covariant in T if the ordering of the constructed types follows the ordering of the generic type parameters: 
    Generic<Base> ≥ Generic<Derived>.
    
    -contravariant in T if the ordering of the constructed types is reversed from the ordering of the generic type parameters: 
    Generic<Base> ≤ Generic<Derived>.
    
    -invariant in T if neither of the above apply.
    */
    class Myclass3<T, T1>
    {
        public T MyProperty { get; set; }

        public T1 Test(T t1, T t2)
        {
            return default(T1);
        }
    }

    class Myclass1<T> 
    {
        public T MyProperty { get; set; }

        public bool Compare(T t1, T t2)
        {
            return (t1.Equals(t2));
        }
    }


    class Myclass<T> where T : class, new()
    {
        public Myclass()
        {
            MyProperty = new T();
        }

        public T MyProperty { get; set; }
    }

    class Myclass2
    {
        public bool Compare<T>(T t1, T t2)
        {
            return (t1.Equals(t2));
        }
    }


    enum Importance
    {
        None = 0,
        Trivial = 1,
        Regular = 2,
        Important = 3,
        Critical = 4
    };

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

    //extension
    public static class MyExtension
    {
        public static string GetLower(this string arg)
        {
            return arg.ToLower();
        }
    }

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
            Console.WriteLine("  animale dorme " + ore);
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

    public class Pippo1
    {
        public static void Message(string msg)
        {
            Console.WriteLine(msg);
        }


        public static void OldMethod(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    interface IRight
    {
        void Move();
    }

    interface ILeft
    {
        void Move();
    }

    public interface IIPippo
    {
        int Count { get; set; }
        void Dodo();
    }

    public abstract class ABase
    {
        private int field = 0;
        public void DoSome(int arg) { }

        public virtual void DoSomeVirtual()
        {
            Console.WriteLine("Abase");
        }

        protected abstract int DoSomeAbstarct();
    }

    public abstract class AChild : ABase
    {
        protected abstract override int DoSomeAbstarct();
    }

    public class Child : AChild, IIPippo, ILeft, IRight
    {
        public override void DoSomeVirtual()
        {
            Console.WriteLine("Child");
        }

        protected override int DoSomeAbstarct()
        {
            throw new NotImplementedException();
        }

        public int Count { get; set; }

        public void Dodo()
        {
            Console.WriteLine("Child");
        }

        void ILeft.Move()
        {
            throw new NotImplementedException();
        }

        void IRight.Move()
        {
            throw new NotImplementedException();
        }
    }

    public class Test
    {
        public int Property1;
        internal int Property2;
        private int _property3;
        protected int Property4;
        protected internal int Property5;
        private int _property6 = 6;


        public Test(int property3)
        {
            Property3 = property3;
        }

        //fake read only field
        public int Property3
        {
            get; private set;
        }

        //really read only filed
        public int Property6
        {
            // ReSharper disable once ConvertPropertyToExpressionBody
            get { return _property6; }
        }
    }

    public class TestSon : Test
    {
        public TestSon(int property3) : base(property3)
        {
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
}
