namespace Typedeaf.TypeO.Engine.Core
{
    public abstract class Module
    {
        public abstract void Init(TypeO typeO);
    }

    public partial class TypeO
    {
        public partial class Runner<T> where T : Game
        {
            public TypeO.Runner<T> LoadModule(Module module)
            {
                module.Init(TypeO);
                return this;
            }
        }
    }
}
