using Typedeaf.TypeOCore.Engine.Hardware;

namespace Typedeaf.TypeOCore
{
    public abstract class Module : ITypeO, IHasTypeO
    {
        ITypeO IHasTypeO.TypeO { get; set; }
        protected ITypeO TypeO { get { return (this as IHasTypeO).GetTypeO(); } }

        public abstract void Initialize();
        public abstract void Cleanup();

        public virtual ITypeO AddModuleServices() { return TypeO; }

        public void Exit()
        {
            TypeO.Exit();
        }

        public ITypeO AddService<I, S>()
            where I : class
            where S : Service, new()
        {
            return TypeO.AddService<I, S>();
        }

        public ITypeO AddHardware<I, H>()
            where I : IHardware
            where H : HardwareBase, new()
        {
            return TypeO.AddHardware<I, H>();
        }

        public M LoadModule<M>() where M : Module, new()
        {
            return TypeO.LoadModule<M>();
        }

        public void Start()
        {
            TypeO.Start();
        }
    }
}
