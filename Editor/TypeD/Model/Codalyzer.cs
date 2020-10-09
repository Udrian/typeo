using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TypeD.Model
{
    public class Codalyzer
    {
        public List<string> Usings { get; set; }
        public string Namespace { get; set; }
        public string Name { get; set; }


        private StringBuilder Output { get; set; }
        private int Tabs { get; set; }
        private string Tab { get { return "    "; } }

        public Codalyzer()
        {
            Usings = new List<string>();
        }

        private void AddLine(string line = "", bool tab = false)
        {
            if (tab) Tabs++;
            Output.Append($"{string.Concat(Enumerable.Repeat(Tab, Tabs))}{line}{Environment.NewLine}");
        }
        private void AddLeftCurlyBracket()
        {
            AddLine("{");
            Tabs++;
        }

        private void AddRightCurlyBrackets(int count = 1)
        {
            count = (count < Tabs ? count : Tabs);
            for (int i = 0; i < count; i++)
            {
                if(Tabs > 0)
                    Tabs--;
                AddLine("}");
            }
        }

        public void Generate(string location)
        {
            Output = new StringBuilder();

            foreach(var @using in Usings)
            {
                AddLine($"using {@using};");
            }
            AddLine();
            AddLine($"namespace {Namespace}");
            AddLeftCurlyBracket();
            AddLine($"class Program");
            AddLeftCurlyBracket();
            AddLine($"static void Main(string[] args)");
            AddLeftCurlyBracket();
            AddLine($"Console.WriteLine(\"Hello '{Name}' World!\");");
            AddRightCurlyBrackets(Tabs);

            File.WriteAllText(Path.Combine(location, Namespace, $"{Name}.cs"), Output.ToString());
        }
    }
}
