using System.Collections.Generic;

namespace TypeD.Data
{
    public class ProjectData
    {
        public string ProjectName { get; set; }
        public string CSSolutionPath { get; set; }
        public string CSProjName { get; set; }
        public List<string> Modules { get; set; } = new List<string>();
    }
}
