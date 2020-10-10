using System.Collections.Generic;

namespace TypeD.Model.Code
{
    public class ProgramCode : Codalyzer
    {
        public ProgramCode(Project project) : base(project, "Program", $"{project.Name}")
        {
            Usings = new List<string>()
            {
                "System"
            };
        }

        protected override void Generate()
        {
            AddLine($"static void Main(string[] args)");
            AddLeftCurlyBracket();
            AddLine($"Console.WriteLine(\"Hello '{Name}' World!\");");
        }
    }
}
