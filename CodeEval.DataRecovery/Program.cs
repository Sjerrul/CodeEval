using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval.DataRecovery
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine(String.Format("Usage: '{0} <path_to_file>'", System.AppDomain.CurrentDomain.FriendlyName));
                return;
            }

            InputFile file = new InputFile(args[0]);

            foreach (var line in file)
            {
                Console.WriteLine(line.ToString());
            }
            Console.ReadKey();
        }

        public class InputFile : IEnumerable<Line>
        {
            private IList<Line> _lines = new List<Line>();

            public InputFile(string fileName)
            {
                var lines = File.ReadAllLines(fileName);
                foreach (string line in lines)
                {
                    Line parsedLine = new Line(line);
                    _lines.Add(parsedLine);
                }
            }

            public IEnumerator<Line> GetEnumerator()
            {
                return GetFileEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetFileEnumerator();
            }

            private IEnumerator<Line> GetFileEnumerator()
            {
                foreach (Line line in _lines)
                {
                    yield return line;
                }
            }
        }

        public class Line
        {
            private string[] _words;
            private int[] _sequence;

            public Line(string lineFromFile)
            {
                var lineParts = lineFromFile.Split(';');
                _words = lineParts[0].Split(' ');
                _sequence = lineParts[1].Split(' ').Select(Int32.Parse).ToArray();
            }     
      
            public override string ToString()
            {
                string[] orderedWords = new string[_words.Length];

                int currentWord = 0;
                foreach (int wordPlace in _sequence)
                {
                    orderedWords[wordPlace - 1] = _words[currentWord];
                    currentWord++;
                }

                string remainingWord = _words.Last();
                for (int i = 0; i < orderedWords.Length; i++)
                {
                    if (orderedWords[i] == null)
                    {
                        orderedWords[i] = remainingWord;
                        break;
                    }
                }

                return String.Join(" ", orderedWords);
            }
        }
    }
}
