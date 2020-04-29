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
                var typeO = new TypeO();
                typeO.Context = new Context(new G(), typeO, name);

                return typeO;
            }

            public Context Context { get; private set; }
            public Version Version { get; private set; }

            internal TypeO() : base()
            {
                Version = new Version(0, 1, 2);
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
                Context.Logger = new DefaultLogger()
                {
                    LogLevel = logLevel
                };
                return this;
            }

            public ITypeO SetLogger<L>(LogLevel logLevel = LogLevel.None) where L : ILogger, new()
            {
                Context.Logger = new L
                {
                    LogLevel = logLevel
                };
                return this;
            }

            public ITypeO LoadModule<M>(ModuleOption option = null, bool loadExtensions = true) where M : Module, new()
            {
                var module = new M();

                if(option == null)
                {
                    module.CreateOption();
                }
                else
                {
                    module.GetType().GetProperty("Option").SetValue(module, option);
                }

                module.GetType().GetProperty("TypeO").SetValue(module, this);
                module.GetType().GetProperty("WillLoadExtensions").SetValue(module, loadExtensions);

                Context.Modules.Add(module);
                return this;
            }

            public ITypeO RequireModule<M>(Version version) where M : Module, new()
            {
                Context.ModuleRequirements.Add(new Tuple<Type, Version>(typeof(M), version));
                return this;
            }

            public ITypeO RequireTypeO(Version version)
            {
                if(!Context.RequiredTypeOVersion.Eligable(version))
                    Context.RequiredTypeOVersion = version;
                return this;
            }

            public void Start()
            {
                Context.Start();
            }
        }
    }
}
