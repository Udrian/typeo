using System.Collections.Generic;
using TypeD.Models;

namespace TypeD.Code
{
    public class ProgramCode : Codalyzer
    {
        public ProgramCode(ProjectModel project) : base(project, "Program", $"{project.ProjectName}")
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
            AddLine($"TypeO.Create<{Project.ProjectName}Game>(\"{Project.ProjectName}\")");
            foreach(var module in Project.Modules)
            {
                var moduleType = module.ModuleTypeInfo;
                if (moduleType == null) continue;
                AddLine($".LoadModule<{moduleType.Name}>()");
            }
            AddLine(".Start();");
            AddRightCurlyBrackets();
        }
    }
}