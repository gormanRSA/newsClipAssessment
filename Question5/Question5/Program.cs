using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question5
{
    class Program
    {
        static void Main(string[] args)
        {
            int intTerms = 50;
            Console.WriteLine("Generated first "+intTerms+" terms of the Fibonacci Sequence");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            printSequence(intTerms);
            Console.WriteLine();
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static void printSequence(int intTerms)
        {
            /*The Fibonacci Sequence is the series of numbers: 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, ... 
              where the next iteration is found by adding up the two iterations before it.
            */

            /*The sequence has exponential growth so it will be necessary to store the integer values in 
              a 'long' variable which supports large 64-bit integers. This is done to prevent large iterations
              of the sequence overflowing the limited 'int' variable.
            */
            long[] lSequence = new long[intTerms];

            /*As the sequence needs 2 previous numbers to add before it can create a new entry we hardcode
              the first 2 iterations of the Fibonacci sequence.
            */
            lSequence[0] = 0;
            lSequence[1] = 1;
            
            //Loop through and generate the remaining iterations of the sequence
            for (int i = 2; i < intTerms; i++)
            {
                lSequence[i] = lSequence[i - 2] + lSequence[i - 1];
            }

            //Join the long array together with a colon and print to console
            Console.WriteLine(string.Join(":", lSequence));
        }

    }
}
