using Typedeaf.TypeOCore.Engine.Hardwares;
using Typedeaf.TypeOCore.Engine.Hardwares.Interfaces;
using Typedeaf.TypeOCore.Interfaces;
using Typedeaf.TypeOCore.Services;
using Typedeaf.TypeOCore.Services.Interfaces;

namespace Typedeaf.TypeOCore
{
    public abstract class Module : ITypeO, IHasTypeO
    {
        TypeO IHasTypeO.TypeO { get; set; }
        protected TypeO TypeO { get => (this as IHasTypeO).TypeO; set => (this as IHasTypeO).TypeO = value; }

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
