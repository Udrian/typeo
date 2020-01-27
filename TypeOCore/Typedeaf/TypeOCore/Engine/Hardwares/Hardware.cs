namespace Typedeaf.TypeOCore
{
    namespace Engine.Hardwares
    {
        public abstract class Hardware : IHasTypeO
        {
            ITypeO IHasTypeO.TypeO { get; set; }
            protected ITypeO TypeO { get { return (this as IHasTypeO).GetTypeO(); } }

            protected Hardware() { }

            public abstract void Initialize();
        }
    }
}
