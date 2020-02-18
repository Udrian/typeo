using System;
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
        public class TypeO : ITypeO
        {
            public static ITypeO Create<G>(string name) where G : Game, new()
            {
                var typeO = new TypeO
                {
                    Context = new Context(new G(), name)
                };

                return typeO;
            }

            public Context Context { get; private set; }

            public TypeO() : base()
            {
            }

            public ITypeO AddService<I, S>()
                where I : IService
                where S : Service, new()
            {
                //Instantiate the Service
                var service = new S();

                Context.Services.Add(typeof(I), service);
                return this;
            }

            public ITypeO AddHardware<I, H>()
                where I : IHardware
                where H : Hardware, new()
            {
                //Instantiate the Hardware
                var hardware = new H();

                Context.Hardwares.Add(typeof(I), hardware);
                return this;
            }

            public ITypeO BindContent<CFrom, CTo>()
                where CFrom : Content
                where CTo : Content, new()
            {
                Context.ContentBinding.Add(typeof(CFrom), typeof(CTo));

                return this;
            }

            public ITypeO SetLogger(LogLevel logLevel = LogLevel.None)
            {
                return SetLogger<DefaultLogger>(logLevel);
            }

            public ITypeO SetLogger<L>(LogLevel logLevel = LogLevel.None) where L : ILogger, new()
            {
                Context.Logger = new L
                {
                    LogLevel = logLevel
                };
                return this;
            }

            public M LoadModule<M>() where M : Module
            {
                var module = (M)Activator.CreateInstance(typeof(M), this);

                Context.Modules.Add(module);
                return module;
            }

            public ITypeO ReferenceModule<M>() where M : Module
            {
                Context.ModuleReferences.Add(typeof(M));
                return this;
            }

            public void Start()
            {
                Context.Start();
            }
        }
    }
}
