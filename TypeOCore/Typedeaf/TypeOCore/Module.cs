using Typedeaf.TypeOCore.Engine.Hardwares;
using Typedeaf.TypeOCore.Services;

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
            where I : IService
            where S : Service, new()
        {
            return TypeO.AddService<I, S>();
        }

        public ITypeO AddHardware<I, H>()
            where I : IHardware
            where H : Hardware, new()
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
