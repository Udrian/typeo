using Typedeaf.TypeOCore.Interfaces;

namespace Typedeaf.TypeOCore
{
    namespace Services
    {
        public abstract class Service : IHasTypeO
        {
            TypeO IHasTypeO.TypeO { get; set; }
            protected TypeO TypeO { get => (this as IHasTypeO).TypeO; set => (this as IHasTypeO).TypeO = value; }

            public bool Pause { get; set; }

            protected Service() { }

            public abstract void Initialize();
        }
    }
}
