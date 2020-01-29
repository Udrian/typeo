using Typedeaf.TypeOCore.Interfaces;

namespace Typedeaf.TypeOCore
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
