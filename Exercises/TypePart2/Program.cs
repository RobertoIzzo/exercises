using System;
using System.Collections.Generic;
using System.Linq;
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


    //implicit interface => 2 interface with same name method
    interface IRight
    {
        void Move();
    }
    interface ILeft
    {
        void Move();
    }

    public class Test1 : IRight, ILeft
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
    }
}
