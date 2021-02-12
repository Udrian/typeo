﻿using System.Collections.Generic;
using TypeD.Models;

namespace TypeD.Code
{
    class SceneTypeDCode : Codalyzer
    {
        public SceneTypeDCode(ProjectModel project, string className, string @namespace) : base(project, className, @namespace)
        {
            AutoGeneratedFile = true;
            PartialClass = true;
            TypeDClass = true;

            BaseClass = "Scene";
            Usings = new List<string>()
            {
                "TypeOEngine.Typedeaf.Core"
            };
        }
    }
}
