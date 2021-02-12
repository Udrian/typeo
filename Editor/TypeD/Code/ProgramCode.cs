﻿using System.Collections.Generic;
using TypeD.Models;

namespace TypeD.Code
{
    class ProgramCode : Codalyzer
    {
        public ProgramCode(ProjectModel project) : base(project, "Program", $"{project.ProjectName}")
        {
            AutoGeneratedFile = true;
            PartialClass = false;
            TypeDClass = false;

            Usings = new List<string>()
            {
                "TypeOEngine.Typedeaf.Core.Engine"
            };

            AddFunction(new Function($"static void Main()", () =>
            {
                Writer.AddLine($"TypeO.Create<{Project.ProjectName}Game>(\"{Project.ProjectName}\")");
                foreach (var module in Project.Modules)
                {
                    var moduleType = module.ModuleTypeInfo;
                    if (moduleType == null || moduleType.Name == "TypeOCore") continue;
                    Writer.AddLine($".LoadModule<{moduleType.Name}>()");
                }
                Writer.AddLine(".Start();", true);
                Writer.Tabs--;
            }));
        }
    }
}