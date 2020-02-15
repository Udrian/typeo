using System;
using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

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

                (typeO.Context.Game as IHasContext).SetContext(typeO.Context);
                return typeO;
            }

            private List<Type> ModuleReferences { get; set; }
            public Context Context { get; private set; }

            public TypeO() : base()
            {
                ModuleReferences = new List<Type>();
            }

            public ITypeO AddService<I, S>()
                where I : IService
                where S : Service, new()
            {
                //Instantiate the Service
                var service = new S();
                (service as IHasContext).SetContext(Context);
                if(service is IHasGame)
                {
                    (service as IHasGame).Game = Context.Game;
                }

                Context.Services.Add(typeof(I), service);
                return this;
            }

            public ITypeO AddHardware<I, H>()
                where I : IHardware
                where H : Hardware, new()
            {
                //Instantiate the Hardware
                var hardware = new H();
                (hardware as IHasContext).SetContext(Context);
                if (hardware is IHasGame)
                {
                    (hardware as IHasGame).Game = Context.Game;
                }

                Context.Hardwares.Add(typeof(I), hardware);
                return this;
            }

            public ITypeO BindContent<CFrom, CTo>()
                where CFrom : Content
                where CTo : Content, new()
            {
                if (!typeof(CTo).IsSubclassOf(typeof(CFrom)))
                {
                    throw new ArgumentException($"Content Binding from '{typeof(CFrom).Name}' must be of a base type to '{typeof(CTo).Name}'");
                }

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
                if (module is IHasGame)
                {
                    (module as IHasGame).Game = Context.Game;
                }

                Context.Modules.Add(module);
                return module;
            }

            public void Start()
            {
                //Check if all referenced modules are loaded
                foreach (var moduleReference in ModuleReferences)
                {
                    var found = false;
                    foreach (var module in Context.Modules)
                    {
                        if (module.GetType() == moduleReference)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        throw new InvalidOperationException($"Referenced Module '{moduleReference.Name}' needs to be loaded");
                    }
                }

                Context.Start();
            }

            public ITypeO ReferenceModule<M>() where M : Module
            {
                ModuleReferences.Add(typeof(M));
                return this;
            }
        }
    }
}
