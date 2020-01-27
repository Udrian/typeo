namespace Typedeaf.TypeOCore
{
    namespace Engine.Hardware
    {
        public abstract class HardwareBase : IHasTypeO
        {
            ITypeO IHasTypeO.TypeO { get; set; }
            protected ITypeO TypeO { get { return (this as IHasTypeO).GetTypeO(); } }

            protected HardwareBase() { }

            public abstract void Initialize();
        }
    }
}
