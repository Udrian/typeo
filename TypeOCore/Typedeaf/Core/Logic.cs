using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    public abstract class Logic : IIsUpdatable
    {
        public bool Pause { get; set; }

        public abstract void Initialize();
        public abstract void Update(double dt);
    }
}
