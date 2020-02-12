using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    public abstract partial class Game : IHasTypeO
    {
        TypeO IHasTypeO.TypeO { get; set; }
        private TypeO TypeO { get => (this as IHasTypeO).TypeO; set => (this as IHasTypeO).TypeO = value; }

        protected Game() {}

        public abstract void Initialize();
        public abstract void Update(double dt);
        public abstract void Draw();
        public void Exit() { TypeO.Exit(); }
    }
}
