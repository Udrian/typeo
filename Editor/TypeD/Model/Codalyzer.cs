using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TypeD.Model
{
    public abstract class Codalyzer
    {
        public List<string> Usings { get; set; }
        public string Namespace { get; set; }
        public string Name { get; set; }
        public string Base { get; set; }

        public Project Project { get; set; }

        private StringBuilder Output { get; set; }
        private int Tabs { get; set; }
        private string Tab { get { return "    "; } }

        public Codalyzer(Project project, string name, string ns)
        {
            Project = project;
            Name = name;
            Namespace = ns;
            Usings = new List<string>();
        }

        public Codalyzer(Project project, TypeInfo from)
        {
            Project = project;
            Name = from.Name;
            Namespace = from.Namespace;

            Usings = new List<string>();
            var nestedTypes = from.GetNestedTypes();
            foreach(var type in nestedTypes)
            {
                Usings.Add(type.Namespace);
            }

            Output = new StringBuilder(from.Assembly.CodeBase);
        }

        protected void AddLine(string line = "", bool tab = false)
        {
            if (tab) Tabs++;
            Output.Append($"{string.Concat(Enumerable.Repeat(Tab, Tabs))}{line}{Environment.NewLine}");
        }
        protected void AddLeftCurlyBracket()
        {
            AddLine("{");
            Tabs++;
        }

        protected void AddRightCurlyBrackets(int count = 1)
        {
            count = (count < Tabs ? count : Tabs);
            for (int i = 0; i < count; i++)
            {
                if(Tabs > 0)
                    Tabs--;
                AddLine("}");
            }
        }

        protected void AddAllClosingBrackets()
        {
            AddRightCurlyBrackets(Tabs);
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
            AddLine($"class {Name}{(string.IsNullOrEmpty(Base)?"":$" : {Base}")}");
            AddLeftCurlyBracket();
            Generate();
            AddAllClosingBrackets();

            File.WriteAllText(Path.Combine(location, Namespace, $"{Name}.cs"), Output.ToString());
        }

        protected abstract void Generate();
    }
}
