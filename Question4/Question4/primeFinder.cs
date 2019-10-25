using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question4
{
    class primeFinder
    {
        /*
         - numPrimes will hold the value of the number of primes to be found.
         - intCount will be used to iterate through the natural number set to find potential primes.
         - arrPrimes is the array that will store the found primes.
        */

        private int numPrimes,intCount;
        private int[] arrPrimes;

        //Class constructor taking an initial integer value 'primes' which denotes the number of primes to be found.
        public primeFinder(int primes)
        {
            numPrimes = primes;
            //The count is started at 2 to avoid computational problems with the value 1 in the findPrimes loop function.
            intCount = 2;
            //Run the loop to find the number of wanted primes.
            findPrimes();
        }

        private void findPrimes()
        {
            /*Using a list variable here as it is not neccessary to set the size when initializing (unlike when initializing arrays), this
              works well because it is uncertain how many primes will be found and values can easily just be appended to a list variable
            */
            List<int> primeList = new List<int>();

            while(primeList.Count < numPrimes)
            {
                if(intCount == 2)
                {
                    /*As the next block will test if intCount is even and therefore not a prime, it is necessary to handle 
                      the exception of 2 being the only even prime number first.
                    */
                    primeList.Add(intCount);
                }else if(intCount % 2 == 0)
                {
                    //If intCount is even and greater than 2, it cannot be a prime number. Therefore do nothing.
                }else
                {
                    //This else block will handle unequal iterations of intCount from 3 upwards. ie: 3,5,7,..,n

                    /*Start by assuming the intCount varaible is a prime and test whether
                      this assumption is false by checking for factors of intCount.
                     */
                    bool isPrime = true;

                    /*Loop through all values smaller than (intCount/2) and test whether the value is a factor of intCount
                      (note: factors of intCount mathematically cannot be larger than intCount/2)
                     */
                    for (int i = 2; i <= ((intCount)/2); i++)
                    {
                        if (intCount % i == 0)
                        {
                            //Thus intCount has a factor and therefore is not a prime.
                            isPrime = false;
                            //Break the loop to prevent unnecessary further testing.
                            break;
                        }
                    }
                    //Having proved the assumption add the prime number to the list.
                    if (isPrime)
                    {
                        primeList.Add(intCount);
                    }
                }
                intCount++;
            }
            //Adding the found primes to the array
            arrPrimes = primeList.ToArray();
            Console.WriteLine("Looped through " + arrPrimes.Last() + " values and found " + arrPrimes.Length + " primes.");
            Console.WriteLine("==============================================");
        }
        //Simple get method to retrieve the sum of all the primes
        public int getSum()
        {
            int sum = 0;
            //Loop through the array of primes and add it to the sum.
            for(int i = 0; i < arrPrimes.Length; i++)
            {
                sum = sum + arrPrimes[i];
            }
            return sum;
        }
        
    }
}
