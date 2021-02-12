﻿using TypeD.Models;

namespace TypeD.Commands.Project
{
    public partial class ProjectCommand
    {
        public static void Clear(ProjectModel project)
        {
            if (project == null) return;

            project.Nodes.Clear();
        }
    }
}
