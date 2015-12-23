using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval.NaughtyNice
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<string> inputStrings = ReadInput("input.txt");

            inputStrings = DrieKlinkers(inputStrings);
            inputStrings = TweeKeerAchterElkaar(inputStrings);
            inputStrings = BevatNietDeFouteCombinaties(inputStrings);

            int result = inputStrings.Count;

            Console.WriteLine("Het antwoord is: " + result);
            Console.ReadKey();
        }

        private static IList<string> BevatNietDeFouteCombinaties(IList<string> alleWoorden)
        {
            alleWoorden = alleWoorden.Where(x => !x.Contains("ab")).ToList();
            alleWoorden = alleWoorden.Where(x => !x.Contains("cd")).ToList();
            alleWoorden = alleWoorden.Where(x => !x.Contains("pq")).ToList();
            alleWoorden = alleWoorden.Where(x => !x.Contains("xy")).ToList();

            return alleWoorden;
        }


        private static IList<string> TweeKeerAchterElkaar(IList<string> alleWoorden)
        {
            IList<string> goedeWoorden = new List<string>();

            //Dioe iets
            //Splits in letters
            //Bekij de eerste
            //Als die letter en de volgende gelijk zijn
            //VOeg toe aan goede woorden
            // Anders pak de volgende
            // herhaal tot de eennalaatste letter
            foreach (var woord in alleWoorden)
            {
                char[] letters = woord.ToCharArray();
                int index = 0;

                while (index != woord.Length - 1)
                {
                    char huidigeLetter = woord[index];
                    char volgendeLetter = woord[index + 1];

                    if (huidigeLetter == volgendeLetter)
                    {
                        goedeWoorden.Add(woord);
                        break;
                    }

                    index++;
                }
            }

            return goedeWoorden;
        }


        private static IList<string> DrieKlinkers(IList<string> alleWoorden)
        {
            IList<string> goedeWoorden = new List<string>();


            foreach (var woord in alleWoorden)
            {
                char[] letters = woord.ToCharArray();
                int aantalKlinkers = 0;
                foreach (char letter in letters)
                {
                    switch (letter)
                    {
                        case 'a':
                        case 'e':
                        case 'i':
                        case 'o':
                        case 'u': aantalKlinkers++; break;
                    }

                    if (aantalKlinkers == 3)
                    {
                        goedeWoorden.Add(woord);
                        break;
                    }
                }
            }

            return goedeWoorden;
        }





        private static IList<string> ReadInput(string fileName)
        {
            return File.ReadAllLines(fileName);
        }
    }
}
