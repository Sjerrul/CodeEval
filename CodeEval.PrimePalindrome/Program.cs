using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval.PrimePalindrome
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<int> primes = GetAllPrimes(1000);

            int largestPalindrome = primes.First();
            foreach(int primeNumber in primes)
            {
                if (isPalindrome(primeNumber))
                {
                    largestPalindrome = primeNumber;
                }
            }

            Console.WriteLine(largestPalindrome);
        }

        private static IList<int> GetAllPrimes(int maxPrime)
        {
            BitArray isPrime = new BitArray(maxPrime, true);
            for (int i = 2; i < isPrime.Count; i++)
            {
               if (isPrime[i])
               {
                   for (int p = 2; (p * i) < maxPrime; p++)
                   {
                       isPrime[p * i] = false;
                   }
               }
            }

            IList<int> primes = new List<int>();
            for (int i = 2; i < isPrime.Count; i++)
            {
                if (isPrime[i])
                {
                    primes.Add(i);
                }
            }

            return primes;
        }

        private static bool isPalindrome(int number)
        {
            string numberString = number.ToString();

            var i = 0;
            var j = numberString.Length - 1;

            var isPalindrome = true;
            while(i < j)
            {
                if (numberString[i] == numberString[j])
                {
                    i++;
                    j--;
                }
                else
                {
                    isPalindrome = false;
                    break;
                }
            }

            return isPalindrome;
        }
    }

    
}
