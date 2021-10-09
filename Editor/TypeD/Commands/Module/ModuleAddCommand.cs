using System.IO;
using TypeD.Models;
using TypeD.Models.Data;

namespace TypeD.Commands.Module
{
    public partial class ModuleCommand
    {
        public bool Add(string name, string version, ProjectModel projectModel, Project project)
        {
            var modulePath = $"{ModuleModel.ModuleCachePath}/{name}/{version}";
            if (!Directory.Exists($"{modulePath}")) return false;

            projectModel.AddModule(project, new ModuleModel(name, version));
            return true;
        }
    }
}
