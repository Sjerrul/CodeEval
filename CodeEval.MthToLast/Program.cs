using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval.MthToLast
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
                Console.WriteLine(line.GetMthToLast());
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

                    if (parsedLine.IsValidLine())
                    {
                        _lines.Add(parsedLine);
                    }
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
            public char[] Characters { get; private set; }
            public int M { get; set; }

            public Line(string lineFromFile)
            {
                string[] elements = lineFromFile.Split(' ');

                int m = Int32.Parse(elements.Last());
                char[] characters = elements.Take(elements.Length - 1).Select(Convert.ToChar).ToArray();

                Characters = characters;
                M = m;
            }

            public Line(char[] characters, int m)
            {
                Characters = characters;
                M = m;
            }

            public char GetMthToLast()
            {
                return Characters[Characters.Length - M];
            }

            public bool IsValidLine()
            {
                return M <= Characters.Length;
            }
        }
    }    
}
