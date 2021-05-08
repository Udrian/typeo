using System.IO;
using TypeD.Models;

namespace TypeD.Commands.Module
{
    public partial class ModuleCommand
    {
        public static bool Add(string name, string version, ProjectModel project)
        {
            var modulePath = $"{ModuleModel.ModuleCachePath}/{name}/{version}";
            if (!Directory.Exists($"{modulePath}/{name}")) return false;

            project.AddModule(new ModuleModel(name, version));
            return true;
        }
    }
}
