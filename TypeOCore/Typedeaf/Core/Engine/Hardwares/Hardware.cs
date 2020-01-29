using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Hardwares
    {
        public abstract class Hardware : IHasTypeO
        {
            TypeO IHasTypeO.TypeO { get; set; }
            protected TypeO TypeO { get => (this as IHasTypeO).TypeO; set => (this as IHasTypeO).TypeO = value; }

            protected Hardware() { }

            public abstract void Initialize();
        }
    }
}
