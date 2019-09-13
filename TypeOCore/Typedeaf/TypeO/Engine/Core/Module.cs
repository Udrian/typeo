using System.Collections.Generic;

namespace Typedeaf.TypeO.Engine.Core
{
    public abstract class Module
    {
        public TypeO TypeO { get; set; }
        public abstract void Init();
        public abstract void Cleanup();
        public abstract void Update(float dt);
    }

    public partial class TypeO
    {
        public List<Module> Modules;

        public partial class Runner<T> where T : Game
        {
            public TypeO.Runner<T> LoadModule(Module module)
            {
                module.TypeO = TypeO;
                module.Init();
                TypeO.Modules.Add(module);
                return this;
            }
        }
    }
}
