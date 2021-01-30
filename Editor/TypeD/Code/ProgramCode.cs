using System.Collections.Generic;
using System.IO;
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

            AddFunction(new Function($"static void Main(string[] args)", () =>
            {
                Writer.AddLine($"TypeO.Create<{Project.ProjectName}Game>(\"{Project.ProjectName}\")");
                foreach (var module in Project.Modules)
                {
                    var moduleType = module.ModuleTypeInfo;
                    if (moduleType == null) continue;
                    Writer.AddLine($".LoadModule<{moduleType.Name}>()");
                }
                Writer.AddLine(".Start();", true);
                Writer.Tabs--;
            }));
        }

        public override void Save(string location)
        {
            Writer.Save(Path.Combine(location, Namespace, $"{ClassName}.cs"));
        }
    }
}