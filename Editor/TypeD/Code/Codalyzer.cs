﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;

namespace TypeD.Code
{
    public abstract class Codalyzer
    {
        // Class Definitions
        public class CodeWriter
        {
            // Statics
            private static string Tab { get { return "    "; } }

            // Properties
            public CodeFile TargetFile { get; set; }
            public int Tabs { get; set; }
            public int TabSkew { get; set; }

            // Constructors
            public CodeWriter()
            {
            }

            // Functions
            public void AddLine(string line = "", bool tab = false)
            {
                if (tab) Tabs++;
                TargetFile.Output.Append($"{string.Concat(Enumerable.Repeat(Tab, Tabs + TabSkew))}{line}{Environment.NewLine}");
            }

            public void AppendLine(string line = "")
            {
                TargetFile.Output.Append(line);
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
                TargetFile.Output.Clear();
            }
        }

        public class Function
        {
            // Properties
            public string Definition { get; set; }

            public Action<CodeWriter> Body { get; set; }

            // Constructors
            public Function(string definition, Action<CodeWriter> body)
            {
                Definition = definition;
                Body = body;
            }

            public Function(string definition, Action body)
            {
                Definition = definition;
                Body = (_) => { body(); };
            }

            // Functions
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
            // Properties
            public string Definition { get; set; }

            public Action<CodeWriter> Body { get; set; }

            // Constructors
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

            // Functions
            public void Generate(CodeWriter writer)
            {
                if (Body == null)
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

        public class CodeFile
        {
            // Properties
            public string FilePath { get; set; }
            public StringBuilder Output { get; set; }
            public List<Type> Interfaces { get; set; }
            public List<Function> Functions { get; set; }
            public List<Property> Properties { get; set; }
            public List<string> Usings { get; set; }
            public Func<List<string>> DynamicUsing { get; set; }
            public bool AutoGeneratedFile { get; set; }

            // Constructors
            public CodeFile()
            {
                Output = new StringBuilder();
                Functions = new List<Function>();
                Properties = new List<Property>();
                Interfaces = new List<Type>();
                Usings = new List<string>();
            }

            // Functions
            public void Save()
            {
                if (Output != null && Output.Length > 0)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
                    File.WriteAllText(FilePath, Output.ToString());
                }
            }
        }

        // Statics
        protected static string AutoGeneratedText
        {
            get
            {
                return
@"/* This file have been autogenerated by TypeD
** Do not change any of it's content */";
            }
        }

        // Properties
        public Project Project { get; internal set; }
        public IResourceModel Resources { get; internal set; }
        protected CodeWriter Writer { get; private set; }
        protected CodeFile BaseFile { get; set; }
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public string BaseClass { get; set; }
        public bool PartialClass { get; set; }
        public bool Initialized { get; private set; }

        // Constructors
        public Codalyzer()
        {
            PartialClass = false;
            Writer = new CodeWriter();
            BaseFile = new CodeFile();
        }

        public virtual void Init()
        {
            Writer.TargetFile = BaseFile;
            InitClass();
            BaseFile.FilePath = FilePath();
            Initialized = true;
        }

        protected abstract void InitClass();

        // Functions
        public void AddFunction(Function function)
        {
            Writer.TargetFile.Functions.Add(function);
        }

        public void AddInterface(Type @interface)
        {
            Writer.TargetFile.Interfaces.Add(@interface);
        }

        public void AddProperty(Property property)
        {
            Writer.TargetFile.Properties.Add(property);
        }
        public void AddUsings(List<string> usings)
        {
            Writer.TargetFile.Usings.AddRange(usings);
        }
        public void AddUsing(string @using)
        {
            Writer.TargetFile.Usings.Add(@using);
        }

        public void SetDynamicUsing(Func<List<string>> dynamicUsing)
        {
            Writer.TargetFile.DynamicUsing = dynamicUsing;
        }

        public List<Type> GetInterfaces()
        {
            return Writer.TargetFile.Interfaces;
        }

        public virtual void Generate()
        {
            Writer.TargetFile = BaseFile;
            Writer.Clear();
            WriteFile();
        }

        public void WriteFile()
        {
            var inheritance = new List<string>();
            if (!string.IsNullOrEmpty(BaseClass))
            {
                inheritance.Add(BaseClass.Split(".").LastOrDefault());
            }
            inheritance.AddRange(Writer.TargetFile.Interfaces.Select((i) => { return i.Name; }));

            if (Writer.TargetFile.AutoGeneratedFile)
            {
                Writer.AddLine(AutoGeneratedText);
                Writer.AddLine();
            }

            var dynamicUsings = new List<string>();
            if(Writer.TargetFile.DynamicUsing != null)
            {
                dynamicUsings = Writer.TargetFile.DynamicUsing();
            }
            dynamicUsings.AddRange(Writer.TargetFile.Interfaces.Select((i) => { return i.Namespace; }));
            if (!string.IsNullOrEmpty(BaseClass) && BaseClass.Contains("."))
            {
                dynamicUsings.Add(string.Join(".", BaseClass.Split(".").SkipLast(1)));
            }
            var usings = Writer.TargetFile.Usings.Union(dynamicUsings).Distinct().Where(u => u != Namespace && u != "").ToList();
            foreach (var @using in usings)
            {
                Writer.AddLine($"using {@using};");
            }
            Writer.AddLine();
            Writer.AddLine($"namespace {Namespace}");
            Writer.AddLeftCurlyBracket();
            Writer.AddLine($"{(PartialClass? "partial ":"")}class {ClassName}{(inheritance.Count == 0 ? "" : $" : {string.Join(", ", inheritance)}")}");
            Writer.AddLeftCurlyBracket();

            if(Writer.TargetFile.Properties.Count > 0)
                Writer.AddLine("// Properties");
            foreach(var property in Writer.TargetFile.Properties)
            {
                property.Generate(Writer);
            }

            if (Writer.TargetFile.Functions.Count > 0)
            {
                if (Writer.TargetFile.Properties.Count > 0)
                    Writer.AddLine();
                Writer.AddLine("// Functions");
            }
            foreach (var function in Writer.TargetFile.Functions)
            {
                function.Generate(Writer);
                if(Writer.TargetFile.Functions.LastOrDefault() != function)
                    Writer.AddLine();
            }

            Writer.AddAllClosingBrackets();
        }

        public virtual void Save()
        {
            if (!BaseFile.AutoGeneratedFile && File.Exists(FilePath()))
            {
                return;
            }

            BaseFile.Save();
        }

        protected string FilePath()
        {
            return Path.Combine(Project.Location, Namespace.Replace(".", @"\\"), $"{ClassName}.cs");
        }
    }
}
