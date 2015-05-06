using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval.FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1) {
                Console.WriteLine(String.Format("Usage: '{0} <path_to_file>'", System.AppDomain.CurrentDomain.FriendlyName));
                return;
            }

            try
            {
                FizzBuzzFile file = new FizzBuzzFile(args[0]);
                foreach (FizzBuzzLine line in file)
                {
                    Console.WriteLine(FizzBuzz.Go(line));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error encountered:");
                Console.WriteLine(exception.Message);
            }

            ShowExitMessage();
        }

        private static void ShowExitMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Press a key to exit...");
            Console.ReadKey();
        }
    }

    public class FizzBuzzFile : IEnumerable<FizzBuzzLine>
    {
        private string _fileName;
        private string[] _fileContents;

        public FizzBuzzFile(string fileName)
        {
            _fileName = fileName;
            _fileContents = File.ReadAllLines(_fileName);
        }

        public IEnumerator<FizzBuzzLine> GetEnumerator()
        {
            return GetFizzBuzzEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetFizzBuzzEnumerator();
        }

        private IEnumerator<FizzBuzzLine> GetFizzBuzzEnumerator()
        {
            foreach (string line in _fileContents)
            {
                if (line == null)
                {
                    break;
                }

                //We may assume that the file is correctly formatted, so no extra checking required
                var parts = line.Split(' ').Select(Int32.Parse).ToList();
                int fizzDivider = parts[0];
                int buzzDivider = parts[1];
                int count = parts[2];

                yield return new FizzBuzzLine(fizzDivider, buzzDivider, count);
            }
        }
    }

    public class FizzBuzzLine
    {
        private static readonly int FIZZBUZZ_MINIMUM_VALUE = 1;
        private static readonly int FIZZBUZZ_MAXIMUM_VALUE = 20;
        private static readonly int COUNT_MINIMUM_VALUE = 21;
        private static readonly int COUNT_MAXIMUM_VALUE = 100;
        
        public int FizzDivider { get; private set; }
        public int BuzzDivider { get; private set; }
        public int Count { get; private set; }

        public FizzBuzzLine(int fizzDivider, int buzzDivider, int count)
        {
            AssertConstraint(fizzDivider, buzzDivider, count);

            FizzDivider = fizzDivider;
            BuzzDivider = buzzDivider;
            Count = count;
        }

        private void AssertConstraint(int fizzDivider, int buzzDivider, int count) 
        {
            if (fizzDivider < FIZZBUZZ_MINIMUM_VALUE || fizzDivider > FIZZBUZZ_MAXIMUM_VALUE)
            {
                throw new ArgumentException(String.Format("FizzDivider {0} is not between {1} and {2}.", fizzDivider, FIZZBUZZ_MINIMUM_VALUE, FIZZBUZZ_MAXIMUM_VALUE));
            }

            if (buzzDivider < FIZZBUZZ_MINIMUM_VALUE || buzzDivider > FIZZBUZZ_MAXIMUM_VALUE)
            {
                throw new ArgumentException(String.Format("BuzzDivider {0} is not between {1} and {2}.", buzzDivider, FIZZBUZZ_MINIMUM_VALUE, FIZZBUZZ_MAXIMUM_VALUE));
            }

            if (count < COUNT_MINIMUM_VALUE || count > COUNT_MAXIMUM_VALUE)
            {
                throw new ArgumentException(String.Format("Count {0} is not between {1} and {2}.", count, COUNT_MINIMUM_VALUE, COUNT_MAXIMUM_VALUE));
            }
        }
    }

    public static class FizzBuzz
    {
        public static string Go(FizzBuzzLine line)
        {
            StringBuilder fizzbuzzString = new StringBuilder();
            int count = line.Count;

            for (int i = 1; i <= count; i++)
            {
                fizzbuzzString.Append(FizzBuzzify(i, line.FizzDivider, line.BuzzDivider));
                fizzbuzzString.Append(" ");
            }

            return fizzbuzzString.ToString();
        }

        private static string FizzBuzzify(int number, int fizzDivider, int buzzDivider)
        {
            StringBuilder returnValue = new StringBuilder();
            if (number % fizzDivider == 0)
            {
                returnValue.Append("F");
            }

            if (number % buzzDivider == 0)
            {
                returnValue.Append("B");
            }

            return returnValue.Length == 0 ? number.ToString() : returnValue.ToString();
        }
    }
}
