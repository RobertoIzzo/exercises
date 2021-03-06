﻿#define DEBUG
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TypePart2
{
    /*
     *
     * 
     * https://www.codeproject.com/Articles/22769/Introduction-to-Object-Oriented-Programming-Concep
     * 
     * 
     *  1. Encapsulation
     *
     *  2. Abstraction
     *
     *  3. Inheritance
     *
     *  4. Polymorphism.
     *
     *
     * association, aggregation, and composition.
     */
    class Program
    {
        static void Main(string[] args)
        {
            Child c1 = new Child();
            c1.DoSomeVirtual();
            c1.DoSome(1);

            //uso implementazione di Child di DoSomeVirtual
            ABase b1 = new Child();
            b1.DoSomeVirtual();


            //ho solo il metodo di IIPippo
            IIPippo p1 = new Child();
            p1.Dodo();

            //IComparable
            //IEnumerable
            //IDisposable
            //IUnknown
            //IFormattable

            //Reflection
            int ii = 5;
            Type type1 = ii.GetType();
            /*
            http://aspalliance.com/778_Attributes_and_Reflection_in_C
            Attributes are a mechanism for adding metadata. Reflection is the process by which a program can read its own metadata. 
Attributes provide a powerful way to extend metadata by associating declarative information with C# code.
The attribute information is stored with the metadata of the element and can be easily retrieved at runtime using reflection.
Attribute
An attribute is essentially an object that represents the data that is associated with a program element. 
The element to which an attribute is attached is referred to as the target of that attribute.
Attribute targets can be one of the following:

·         All
·         Assembly
·         Class
·         Constructor
·         Delegate
·         Enum
·         Event
·         Field
·         Interface
·         Method
·         Module
·         Parameter
·         ReturnValue
·         Struct

Types of Attributes
Attributes are basically of two types, intrinsic and custom.
Intrinsic Attributes
Intrinsic attributes are supplied as part of the Common Language Runtime, and they are integrated into .NET.
In this example below we use a pre–defined attribute, Obsolete, which marks a program entity as obsolete. The attribute accepts two parameters of which the first is a message and the second a boolean value. If the second parameter is true, the compiler gives an error if the method is invoked, and a warning otherwise.
            Predefined Attributes
            The .Net Framework provides three pre-defined attributes :
            - AttributeUsage
            - Conditional
            - Obsolete
             */

            /*Attribute is one of METADATA
            Type type = typeof(Pippo1);
            //iterating through the attribtues of the Rectangle class
            foreach (Object attributes in type.GetCustomAttributes(false))
            {
                DeBugInfo dbi = (DeBugInfo)attributes;

                if (null != dbi)
                {
                    Console.WriteLine("Developer: {0}", dbi.Developer);
                    Console.WriteLine("Remarks: {0}", dbi.Message);
                }
            }

            //iterating through the method attribtues
            foreach (MethodInfo m in type.GetMethods())
            {

                foreach (Attribute a in m.GetCustomAttributes(true))
                {
                    DeBugInfo dbi = (DeBugInfo)a;

                    if (null != dbi)
                    {
                        Console.WriteLine("Developer: {0}", dbi.Developer);
                        Console.WriteLine("Remarks: {0}", dbi.Message);
                    }
                }
            }
            //Null reference ex
            //Pluto p = new Pluto();
            //Paperino<Pluto> pp = new Pluto();
            //Console.WriteLine(pp.GetName(p).GetName(p));


            Console.ReadLine();


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
        public DeBugInfo( string dev)
        {
            this.developer = dev;
        }
    }

    [DeBugInfo("Rob", Message = "Return type mismatch")]
    public class Pippo1
    {
        [Conditional("DEBUG")]
        public static void Message(string msg)
        {
            Console.WriteLine(msg);
        }


        [Obsolete("Don't use OldMethod, use NewMethod instead", true)]
        public static void OldMethod(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    public interface IIPippo
    {
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

    public class Child : AChild, IIPippo
    {
        public override void DoSomeVirtual()
        {
            Console.WriteLine("Child");
        }

        protected override int DoSomeAbstarct()
        {
            throw new NotImplementedException();
        }

        public void Dodo()
        {
            Console.WriteLine("Child");
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

   
    interface IInterface
    {
        int Count { get; set; }
    }

    //implicit interface => 2 interface with same name method
    interface IRight
    {
        void Move();
    }

    interface ILeft
    {
        void Move();
    }

    public class Test1 : IRight, ILeft, IInterface
    {
        private Test1()
        {

        }
        public static Test1 CreateTest1()
        {
            return new Test1();
        }

        void IRight.Move()
        {
            throw new NotImplementedException();
        }

        void ILeft.Move()
        {
            throw new NotImplementedException();
        }

        public int Count { get; set; }

        public Test GetIt(Test arg)
        {
            throw new NotImplementedException();
        }
    }

    public class  Pluto : Paperino<Pluto> { }

    public class Paperino<T>
    {
        public T GetName(T arg)
        {
            return default(T);
        }
    }
}
