namespace Typedeaf.TypeOCore
{
    namespace Services
    {

        public abstract class Service : IHasTypeO
        {
            ITypeO IHasTypeO.TypeO { get; set; }
            protected ITypeO TypeO { get { return (this as IHasTypeO).GetTypeO(); } }

            public bool Pause { get; set; }

            protected Service() { }

            public abstract void Initialize();
        }
    }
}
