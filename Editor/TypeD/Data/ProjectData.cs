using System.Collections.Generic;

namespace TypeD.Data
{
    public class ProjectData
    {
        public string ProjectName { get; set; }
        public string CSSolutionPath { get; set; }
        public string CSProjName { get; set; }
        public List<ModuleData> Modules { get; set; } = new List<ModuleData>();
        public string StartScene { get; set; }
    }
}
