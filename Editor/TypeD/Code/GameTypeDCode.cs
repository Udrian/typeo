﻿using System.Collections.Generic;
using TypeD.Models;
using TypeD.Types;

namespace TypeD.Code
{
    class GameTypeDCode : Codalyzer
    {
        public GameTypeDCode(ProjectModel project) : base(project, $"{project.ProjectName}Game", $"{project.ProjectName}")
        {
            AutoGeneratedFile = true;
            PartialClass = true;
            TypeDClass = true;

            BaseClass = "Game";
            Usings = new List<string>()
            {
                "TypeOEngine.Typedeaf.Core",
                "TypeOEngine.Typedeaf.Core.Engine"
            };
            DynamicUsings = () =>
            {
                var usings = new List<string>();

                TypeOType defaultScene = project.GetTypeFromName(project.StartScene).Find(t => { return t.TypeOBaseType == "Scene"; });
                if (defaultScene != null)
                {
                    usings.Add(defaultScene.Namespace);
                }

                return usings;
            };

            AddProperty(new Property("protected SceneList Scenes"));

            AddFunction(new Function("public override void Initialize()", () => {
                Writer.AddLine("Scenes = CreateSceneHandler();");
                TypeOType defaultScene = project.GetTypeFromName(project.StartScene).Find(t => { return t.TypeOBaseType == "Scene"; });
                if (defaultScene != null)
                {
                    Writer.AddLine($"Scenes.SetScene<{defaultScene.ClassName}>();");
                }
                Writer.AddLine("InternalInitialize();");
            }));
            AddFunction(new Function("public override void Update(double dt)", () => {
                Writer.AddLine("Scenes.Update(dt);");
            }));
            AddFunction(new Function("public override void Draw()", () => {
                Writer.AddLine("Scenes.Draw();");
            }));
            AddFunction(new Function("public override void Cleanup()", () => {
                Writer.AddLine("Scenes.Cleanup();");
                Writer.AddLine("InternalCleanup();");
            }));
        }     
    }
}