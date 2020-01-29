using Typedeaf.TypeOCore.Engine.Hardwares;
using Typedeaf.TypeOCore.Engine.Hardwares.Interfaces;
using Typedeaf.TypeOCore.Services;
using Typedeaf.TypeOCore.Services.Interfaces;

namespace Typedeaf.TypeOCore
{
    namespace Interfaces
    {
        public interface ITypeO
        {
            public void Start();
            public void Exit();

            public ITypeO AddService<I, S>() where I : IService where S : Service, new();
            public ITypeO AddHardware<I, H>() where I : IHardware where H : Hardware, new();
            public M LoadModule<M>() where M : Module, new();
        }
    }
}
