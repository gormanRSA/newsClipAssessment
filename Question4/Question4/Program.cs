using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Invoke a new primeFinder class instance and search for the first 59 primes (see: primeFinder.cs)
            primeFinder newFinder = new primeFinder(59);
            //Get the sum of all the found primes using the getSum() class function.
            Console.WriteLine("Sum of the primes: " + newFinder.getSum());
            Console.ReadLine();
        }
    }
}
