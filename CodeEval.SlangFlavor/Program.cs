using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval.SlangFlavor
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

            SlangFile file = new SlangFile(args[0]);

            Console.WriteLine(file.GetSlangContents());
        }
    }

    public class SlangFile
    {
        private string _fileName;
        private string _fileContents;
        private string _slangedContents;

        private static readonly char[] PUNCTUATION = {'.', '!', '?'};
        private string[] _suffixes = {
            ", yeah!",
            ", this is crazy, I tell ya.",
            ", can U believe this?",
            ", eh?",
            ", aw yea.",
            ", yo.",
            "? No way!",
            ". Awesome!",
        };
        private int _nextSuffix = 0;

        public SlangFile(string fileName)
        {
            _fileName = fileName;
            _fileContents = File.ReadAllText(_fileName);

            StringBuilder text = new StringBuilder();
            int i = 0;
            int lineNumber = 1;
            while(i < _fileContents.Length)
            {
                char c = _fileContents[i];
                if (Array.IndexOf(PUNCTUATION, c) > -1)
                {
                    if ((lineNumber % 2 == 0))
                    {
                        text.Append(_suffixes[_nextSuffix]);
                        _nextSuffix++;
                        _nextSuffix = _nextSuffix % _suffixes.Length;
                    }
                    else
                    {
                        text.Append(c);
                    }
                    
                    lineNumber++;
                }
                else
                {
                    text.Append(c);
                }
                
                i++;
            }
           _slangedContents = text.ToString();
        }

        public string GetRegularContents()
        {
            return _fileContents;
        }

        public string GetSlangContents()
        {
            return _slangedContents;
        }
    }
}
