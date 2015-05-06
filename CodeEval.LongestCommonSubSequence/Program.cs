using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval.LongestCommonSubSequence
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
                var lcs = LongestCommonSequence(line.LeftSequence, line.RightSequence);
                Console.WriteLine(lcs);
            }

            Console.ReadKey();
        }

        public static string LongestCommonSequence(string a, string b)
        {
            if (a.Length == 0 || b.Length == 0)
            {
                return String.Empty;
            }

            string leftoverA = a.Substring(0, a.Length - 1);
            string leftoverB = b.Substring(0, b.Length - 1);
            if (a.Length - 1 < 0)
            {
                leftoverA = a.Substring(0, 0);
            }

            if (b.Length - 1 < 0)
            {
                leftoverB = b.Substring(0, 0);
            }

            if (a[a.Length - 1] == b[b.Length - 1])
            {
                return LongestCommonSequence(leftoverA, leftoverB) + a[a.Length - 1];
            }

            string x = LongestCommonSequence(a, leftoverB);
            string y = LongestCommonSequence(leftoverA, b);

            return (x.Length > y.Length) ? x : y;
        }

        public class InputFile : IEnumerable<Line>
        {
            private IList<Line> _lines = new List<Line>();

            public InputFile(string fileName)
            {
                var lines = File.ReadAllLines(fileName);

                foreach (string line in lines)
                {
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        _lines.Add(new Line(line));
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
            public string LeftSequence { get; private set; }
            public string RightSequence { get; private set; }

            public Line(string lineFromFile)
            {
                string[] lineParts = lineFromFile.Split(';');

                LeftSequence = lineParts[0];
                RightSequence = lineParts[1];
            }
        }
    }
}
