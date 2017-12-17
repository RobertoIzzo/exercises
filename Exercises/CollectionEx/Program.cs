using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionEx
{
    class Program
    {
        static void Main(string[] args)
        {
            //arrays

            //faster find element but not duplicate
            //list
            //dictionary
            
            //sets
            

            //queue
            //stacks


            //tuple
            //Tuples are commonly used in four ways:
            //1. To represent a single set of data.For example, a tuple can represent a database record, and its components can represent individual fields of the record.
            //2. To provide  access to, and manipulation of, a data set.
            //3. To return multiple values from a method without using out parameters(in C#) or ByRef parameters (in Visual Basic).
            //4. To pass multiple values to a method through a single parameter.For example, the Thread.Start(Object) method has a single parameter that lets you supply one value to the method that the thread executes at startup time.If you supply a Tuple < T1, T2, T3 > object as the method argument, you can supply the thread’s startup routine with three items of data.
            //Tuple<> is intended to be immutable.
            //Use a custom Pair class if I need a mutable type 
            /*
           public class Pair<T1, T2>
           {
             public T1 First { get; set; }
             public T2 Second { get; set; }
            }
           */

            //Create a 7-tuple.
            var population = new Tuple<string, int, int, int, int, int, int>(
                                               "New York", 7891957, 7781984,
                                               7894862, 7071639, 7322564, 8008278);
            // Display the first and last elements.
            Console.WriteLine("Population of {0} in 2000: {1:N0}",
                              population.Item1, population.Item7);
            // The example displays the following output:
            //       Population of New York in 2000: 8,008,278

            // Create a 7-tuple.
            var population1 = Tuple.Create("New York", 7891957, 7781984, 7894862, 7071639, 7322564, 8008278);
            // Display the first and last elements.
            Console.WriteLine("Population of {0} in 2000: {1:N0}",
                              population1.Item1, population.Item7);
            // The example displays the following output:
            //       Population of New York in 2000: 8,008,278
        }
    }
}
