using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine
    {
        public abstract class Module : ITypeO
        {
            protected TypeO TypeO { get; set; }

            protected Module(TypeO typeO)
            {
                TypeO = typeO;
            }

            public abstract void Initialize();
            public abstract void Cleanup();

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

            public M LoadModule<M>() where M : Module
            {
                return TypeO.LoadModule<M>();
            }

            public void Start()
            {
                TypeO.Start();
            }

            public ITypeO BindContent<CFrom, CTo>()
                where CFrom : Content
                where CTo : Content, new()
            {
                return TypeO.BindContent<CFrom, CTo>();
            }
        }
    }
}
