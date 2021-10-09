﻿using System.Collections.Generic;
using TypeD;
using TypeD.Models.Data;

namespace TypeDCore.Code.Game
{
    class GameCode : Codalyzer
    {
        public GameCode(Project project) : base(project, $"{project.ProjectName}Game", $"{project.ProjectName}")
        {
            AutoGeneratedFile = false;
            PartialClass = true;
            TypeDClass = false;

            BaseClass = "Game";
            Usings = new List<string>()
            {
                "TypeOEngine.Typedeaf.Core"
            };

            AddFunction(new Function("protected void InternalInitialize()", () => { }));
            AddFunction(new Function("protected void InternalCleanup()", () => { }));
        }
    }
}
