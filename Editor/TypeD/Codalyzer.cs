﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TypeD.Models.Data;

namespace TypeD
{
    public abstract class Codalyzer
    {
        public class CodeWriter
        {
            private StringBuilder Output { get; set; } = new StringBuilder();
            public int Tabs { get; set; }
            public int TabSkew { get; set; }
            public void AddLine(string line = "", bool tab = false)
            {
                if (tab) Tabs++;
                Output.Append($"{string.Concat(Enumerable.Repeat(Tab, Tabs + TabSkew))}{line}{Environment.NewLine}");
            }

            public void AppendLine(string line= "")
            {
                Output.Append(line);
            }

            public void AddLeftCurlyBracket()
            {
                AddLine("{");
                Tabs++;
            }

            public void AddRightCurlyBrackets(int count = 1)
            {
                count = (count < Tabs ? count : Tabs);
                for (int i = 0; i < count; i++)
                {
                    if (Tabs > 0)
                        Tabs--;
                    AddLine("}");
                }
            }

            public void AddAllClosingBrackets()
            {
                AddRightCurlyBrackets(Tabs);
            }

            public void Clear()
            {
                Tabs = 0;
                TabSkew = 0;
                Output.Clear();
            }

            public void Save(string path)
            {
                if (Output != null)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    File.WriteAllText(path, Output.ToString());
                }
            }
        }

        public class Function
        {
            public string Definition { get; set; }

            public Action<CodeWriter> Body { get; set; }

            public Function(string definition, Action<CodeWriter> body)
            {
                Definition = definition;
                Body = body;
            }

            public Function(string definition, Action body)
            {
                Definition = definition;
                Body = (_)=> { body(); };
            }

            public void Generate(CodeWriter writer)
            {
                writer.AddLine(Definition);
                writer.AddLeftCurlyBracket();
                Body(writer);
                writer.AddRightCurlyBrackets();
            }
        }

        public class Property
        {
            public string Definition { get; set; }

            public Action<CodeWriter> Body { get; set; }

            public Property(string definition)
            {
                Definition = definition;
            }

            public Property(string definition, Action body)
            {
                Definition = definition;
                Body = (_) => { body(); };
            }
            public Property(string definition, Action<CodeWriter> body)
            {
                Definition = definition;
                Body = body;
            }

            public void Generate(CodeWriter writer)
            {
                if(Body == null)
                {
                    writer.AddLine($"{Definition} {{ get; set; }}");
                }
                else
                {
                    writer.AddLine(Definition);
                    writer.AddLeftCurlyBracket();
                    Body(writer);
                    writer.AddRightCurlyBrackets();
                }
            }
        }

        public Project        Project   { get; private set; }
        private static string Tab       { get { return "    "; } }
        protected static string AutoGeneratedText { 
            get { 
                return 
@"/* This file have been autogenerated by TypeD
** Do not change any of it's content */"; 
            } 
        }

        protected CodeWriter Writer { get; private set; }
        protected Func<List<string>> DynamicUsings { get; set; }
        private List<Function> Functions { get; set; }
        private List<Property> Properties { get; set; }
        private List<string> Interfaces { get; set; }
        public List<string> Usings { get; set; }
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public string BaseClass { get; set; }
        public string FileName { get; set; }
        public bool AutoGeneratedFile { get; set; }
        public bool PartialClass { get; set; }
        public bool TypeDClass { get; set; }

        public Codalyzer(Project project, string className, string @namespace, string filename = null)
        {
            Writer = new CodeWriter();
            Functions = new List<Function>();
            Properties = new List<Property>();
            Interfaces = new List<string>();
            Project = project;
            ClassName = className;
            Namespace = @namespace;
            FileName = filename ?? className;
            Usings = new List<string>();
            AutoGeneratedFile = true;
        }

        public void AddFunction(Function function)
        {
            Functions.Add(function);
        }

        public void AddInterface(string @interface)
        {
            Interfaces.Add(@interface);
        }

        public void AddProperty(Property property)
        {
            Properties.Add(property);
        }

        public void Generate()
        {
            List<string> inheritance = new List<string>();
            if(!string.IsNullOrEmpty(BaseClass))
            {
                inheritance.Add(BaseClass);
            }
            inheritance.AddRange(Interfaces);

            Writer.Clear();

            if (AutoGeneratedFile)
            {
                Writer.AddLine(AutoGeneratedText);
                Writer.AddLine();
            }

            List<string> dynamicUsings = new List<string>();
            if(DynamicUsings != null)
            {
                dynamicUsings = DynamicUsings();
            }
            var usings = Usings.Union(dynamicUsings).Distinct().ToList();
            foreach (var @using in usings)
            {
                Writer.AddLine($"using {@using};");
            }
            Writer.AddLine();
            Writer.AddLine($"namespace {Namespace}");
            Writer.AddLeftCurlyBracket();
            Writer.AddLine($"{(PartialClass? "partial ":"")}class {ClassName}{(inheritance.Count == 0 ? "" : $" : {string.Join(", ", inheritance)}")}");
            Writer.AddLeftCurlyBracket();

            foreach(var property in Properties)
            {
                property.Generate(Writer);
            }

            foreach(var function in Functions)
            {
                function.Generate(Writer);
            }

            Writer.AddAllClosingBrackets();
        }

        public virtual void Save()
        {
            var path = FilePath();
            if (!AutoGeneratedFile && File.Exists(path))
            {
                return;
            }

            Writer.Save(path);
        }

        protected string FilePath()
        {
            return Path.Combine(Project.Location, Namespace.Replace(".", @"\\"), $"{FileName}{(TypeDClass ? ".typed" : "")}.cs");
        }
    }
}
