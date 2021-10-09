using System.IO;
using System.Reflection;

namespace TypeD.Models.Data
{
    public class Module
    {
        // Data
        public string Name { get; set; }
        public string Version { get; set; }

        // Paths
        internal string ModulePath { get { return Path.Combine(ModuleModel.ModuleCachePath, Name, Version); } }
        internal string ModuleDLLPath { get { return Path.Combine(ModulePath, $"{Name}.dll"); } }

        // Loaded data
        internal Assembly Assembly { get; set; }
        public TypeInfo ModuleTypeInfo { get; set; }
    }
}
