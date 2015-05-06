using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEval.PrefixExpressions
{
    class Program
    {
        private static readonly int MAX_NUMBER_OF_CASES = 40;
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
                Console.WriteLine(Calculate(line));
            }
        }

        public static int Calculate(Line line)
        {
            var elements = line.GetElements();
            Stack<Element> stack = new Stack<Element>();
            elements = elements.Reverse().ToList();

            foreach(Element element in elements)
            {
                if (element.Type == ElementType.Operand)
                {
                    stack.Push(element);
                }
                    
                if (element.Type == ElementType.Operator)
                {
                    Operand operand1 = stack.Pop() as Operand;
                    Operand operand2 = stack.Pop() as Operand;
                    Operator op = element as Operator;
                    var result = op.Calculate(operand1.Value, operand2.Value);
                    stack.Push(new Operand(result));
                }
            }

            var x = stack.Pop() as Operand;
            return x.Value;
        }

        public class InputFile : IEnumerable<Line>
        {
            private IList<Line> _lines = new List<Line>();

            public InputFile(string fileName)
            {
                var lines = File.ReadAllLines(fileName);

                int lineCounter = 0;
                foreach (string line in lines)
                {
                    Line parsedLine = new Line(line);
                    _lines.Add(parsedLine);

                    if (lineCounter >= MAX_NUMBER_OF_CASES - 1)
                    {
                        break;
                    }
                    lineCounter++;
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
            private IList<Element> _elements = new List<Element>();

            public Line(string lineFromFile)
            {
                foreach (string element in lineFromFile.Split(' '))
                {
                    _elements.Add(ElementFactory.CreateElement(element));
                }
            }

            public IList<Element> GetElements()
            {
                return _elements;
            }
        }

        public abstract class Element
        {
            public abstract ElementType Type { get; }
        }

        public class Operand : Element
        {
            public int Value { get; private set; }
            public override ElementType Type
            { 
                get {
                    return ElementType.Operand;
                } 
            }

            public Operand(int value)
            {
                Value = value;
            }
        }

        public class Operator : Element
        {
            private string _op;
            public override ElementType Type
            {
                get
                {
                    return ElementType.Operator;
                }
            }

            public Operator(string op)
            {
                _op = op;
            }

            public int Calculate(int operant1, int operant2)
            {
                switch (_op)
                {
                    case "+": return operant1 + operant2;
                    case "-": return operant1 - operant2;
                    case "/": return operant1 / operant2;
                    case "*": return operant1 * operant2;
                    default: throw new ArgumentException(String.Format("'{0}' is not a valid operator", _op));
                }
            }
        }

       public class ElementFactory
       {
           public static Element CreateElement(string value)
           {
                int number;
                if (Int32.TryParse(value, out number))
                {
                    return new Operand(number);
                }
                else
                {
                    return new Operator(value);
                }
           }
       }

        public enum ElementType
        {
            Operand,
            Operator
        }
    }
}
