using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unsafe
{
    //    * Esegue il riferimento indiretto al puntatore.
    //    ->	Accede a un membro di struct tramite un puntatore.
    //    []  Indicizza un puntatore.
    //    &	Ottiene l'indirizzo di una variabile.
    //    ++ e --	Incrementa e decrementa puntatori.
    //    + e -	Utilizza l'aritmetica dei puntatori.
    //    ==, !=, <, >, <= e >=	Confronta puntatori.
    //   stackalloc	Alloca memoria nello stack.

    class Program
    {
       unsafe static void Main(string[] args)
        {
            //p è un puntatore a un Integer.
            int i = 5;
            //0x00000000
            int* p = null;
            //0x06a6ea88 just for ex
            p = &i;
            string strcptr = Convert.ToString((int)p, 16);
            Console.WriteLine("Ox{0} is the char ptr hex address", strcptr);
            // Unsafe method: uses address-of operator (&):
            SquarePtrParam(p);
            Console.WriteLine(i);
            Console.ReadLine();
        }

        // Unsafe method: takes pointer to int:
        unsafe static void SquarePtrParam(int* p)
        {
            *p *= *p;
        }
    }
}
