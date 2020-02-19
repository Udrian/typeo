using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Interfaces
    {
        public interface ITypeO
        {
            public void Start();

            public ITypeO AddService<I, S>() where I : IService where S : Service, new();
            public ITypeO AddHardware<I, H>() where I : IHardware where H : Hardware, new();
            public ITypeO BindContent<CFrom, CTo>() where CFrom : Content where CTo : Content, new();
            public ITypeO SetLogger(LogLevel logLevel = LogLevel.None);
            public ITypeO SetLogger<L>(LogLevel logLevel = LogLevel.None) where L : ILogger, new();
            public M LoadModule<M>() where M : Module, new();
        }
    }
}
