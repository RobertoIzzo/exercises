using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeReload;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Cell[][] word = new Cell[3][];
            word[0] = new Cell[3];
            word[1] = new Cell[3];
            word[2] = new Cell[3];

            //center
            Cell center = new Cell(1, 1);
            center.SetLive();
            word[1][1] = center;

            //topsx
            Cell topsx = new Cell(0, 0);
            word[0][0] = topsx;

            //bottomsx
            Cell bottomsx = new Cell(2, 0);
            word[2][0] = bottomsx;
           
            //top
            Cell top = new Cell(0, 1);
            word[0][1] = top;
            
            //bottom
            Cell bottom = new Cell(2, 1);
            word[2][1] = bottom;
            
            //left
            Cell left = new Cell(1, 0);
            left.SetLive();
            word[1][0] = left;
            
            //right
            Cell right = new Cell(1, 2);
            right.SetLive();
            word[1][2] = right;
            
            //topdx
            Cell topdx = new Cell(0, 2);
            word[0][2] = topdx;
            
            //bottomdx
            Cell bottomdx = new Cell(2, 2);
            word[2][2] = bottomdx;


            foreach (var cellx in word)
            {
                foreach (var celly in cellx)
                {
                    celly.Search(word);
                    if (celly.IsLive)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write("O");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            foreach (var cellx in word)
            {
                foreach (var celly in cellx)
                {
                    celly.Convert();
                    if (celly.IsLive)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write("O");
                    }
                }
                Console.WriteLine();

            }
            Console.ReadLine();
        }
    }
}
