using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine
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
}
