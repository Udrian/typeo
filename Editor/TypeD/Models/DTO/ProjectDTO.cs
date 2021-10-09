using System.Collections.Generic;
using TypeD.Data;

namespace TypeD.Models.DTO
{
    public class ProjectDTO
    {
        public string ProjectName { get; set; }
        public string CSSolutionPath { get; set; }
        public string CSProjName { get; set; }
        public List<ModuleData> Modules { get; set; } = new List<ModuleData>();
        public string StartScene { get; set; }
    }
}
