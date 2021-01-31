﻿using System.IO;
using System.Threading.Tasks;
using TypeD.Code;
using TypeD.Data;
using TypeD.Helpers;
using TypeD.Models;
using TypeOEngine.Typedeaf.Core.Entities;

namespace TypeD.Commands.Project
{
    public static partial class ProjectCommand
    {
        public static async Task<ProjectModel> Load(string projectFilePath)
        {
            if (!projectFilePath.EndsWith(".typeo")) return null;

            try
            {
                var task = new Task<ProjectModel>(() =>
                {
                    var projectData = JSON.Deserialize<ProjectData>(projectFilePath);
                    var project = new ProjectModel(Path.GetDirectoryName(projectFilePath), projectData);

                    // Prepare
                    project.AddCode(new ProgramCode(project));
                    project.AddCode(new GameCode(project));
                    project.AddCode(new GameTypeDCode(project));

                    foreach(var type in project.Types)
                    {
                        if(type.IsSubclassOf(typeof(Entity)))
                        {
                            project.AddCode(new EntityCode(project, type.Name, type.Namespace));
                            project.AddCode(new EntityTypeDCode(project, type.Name, type.Namespace));
                        }
                    }

                    return project;
                });

                task.Start();
                return await task;
            }
            catch
            {
                return null;
            }
        }
    }
}
