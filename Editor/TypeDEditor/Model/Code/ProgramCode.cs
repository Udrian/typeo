using System.Collections.Generic;

namespace TypeD.Model.Code
{
    public class ProgramCode : Codalyzer
    {
        public ProgramCode(Project project) : base(project, "Program", $"{project.Name}")
        {
            Usings = new List<string>()
            {
                "TypeOEngine.Typedeaf.Core.Engine"
            };
        }

        protected override void GenerateBody()
        {
            AddLine($"static void Main(string[] args)");
            AddLeftCurlyBracket();
            AddLine($"TypeO.Create<{Project.Name}Game>(\"{Project.Name}\")");
            foreach(var module in Project.Modules)
            {
                var moduleType = module.GetModuleType();
                if (moduleType == null) continue;
                AddLine($".LoadModule<{moduleType.Name}>()");
            }
            AddLine(".Start();");
            AddRightCurlyBrackets();
        }
    }
}