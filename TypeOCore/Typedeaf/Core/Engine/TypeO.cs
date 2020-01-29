using System;
using System.Collections.Generic;
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
            public static ITypeO Create<G>() where G : Game, new()
            {
                var typeO = new TypeO
                {
                    Game = new G()
                };
                (typeO.Game as IHasTypeO).SetTypeO(typeO);
                return typeO;
            }

            public Game Game { get; set; }
            private DateTime LastTick { get; set; }
            private List<Module> Modules { get; set; }
            private Dictionary<Type, Hardware> Hardwares { get; set; }
            private Dictionary<Type, Service> Services { get; set; }

            public TypeO() : base()
            {
                LastTick = DateTime.UtcNow;
                Modules = new List<Module>();
                Hardwares = new Dictionary<Type, Hardware>();
                Services = new Dictionary<Type, Service>();
            }

            private bool ExitApplication = false;
            public void Exit()
            {
                ExitApplication = true;
            }

            public ITypeO AddService<I, S>()
                where I : IService
                where S : Service, new()
            {
                //Instantiate the Service
                var service = new S();
                (service as IHasTypeO).SetTypeO(this);
                (service as IHasGame)?.SetGame(Game);

                Services.Add(typeof(I), service);
                return this;
            }

            public ITypeO AddHardware<I, H>()
                where I : IHardware
                where H : Hardware, new()
            {
                //Instantiate the Hardware
                var hardware = new H();
                (hardware as IHasTypeO).SetTypeO(this);
                (hardware as IHasGame)?.SetGame(Game);

                Hardwares.Add(typeof(I), hardware);
                return this;
            }

            public M LoadModule<M>() where M : Module, new()
            {
                var module = new M();
                (module as IHasTypeO).SetTypeO(this);
                (module as IHasGame)?.SetGame(Game);

                Modules.Add(module);
                return module;
            }

            public void Start()
            {
                //Initialize Hardware
                foreach (var hardware in Hardwares.Values)
                {
                    hardware.Initialize();
                }

                //Create Services
                foreach (var servicePair in Services)
                {
                    var service = servicePair.Value;
                    //Add Hardware to service using reflection on the Service properties
                    SetHardwares(service);
                    SetServices(service);

                    service.Initialize();

                    //Game.AddService(servicePair.Key, service);
                }

                //Set modules Hardware and initialize
                foreach (var module in Modules)
                {
                    SetHardwares(module);
                    SetServices(module);
                    module.Initialize();
                }

                //Initialize the game
                SetServices(Game);
                Game.Initialize();

                while (!ExitApplication)
                {
                    var dt = (DateTime.UtcNow - LastTick).TotalSeconds;
                    LastTick = DateTime.UtcNow;

                    foreach (var module in Modules)
                    {
                        (module as IIsUpdatable)?.Update(dt);
                    }

                    foreach (var hardware in Hardwares.Values)
                    {
                        (hardware as IIsUpdatable)?.Update(dt);
                    }

                    foreach (var service in Services.Values)
                    {
                        if (!service.Pause)
                            (service as IIsUpdatable)?.Update(dt);
                    }

                    Game.Update(dt);
                    Game.Draw();
                }

                //Cleanup
                foreach (var module in Modules)
                {
                    module.Cleanup();
                }
            }

            private void SetHardwares(object obj)
            {
                var type = obj.GetType();
                var properties = type.GetProperties();
                foreach (var property in properties)
                {
                    if (property.PropertyType.GetInterface(nameof(IHardware)) == null)
                    {
                        continue;
                    }
                    if (!Hardwares.ContainsKey(property.PropertyType))
                    {
                        throw new Exception($"Hardware type '{property.PropertyType.Name}' is not loaded for '{obj.GetType().Name}'");
                    }

                    property.SetValue(obj, Hardwares[property.PropertyType]);
                }
            }

            public void SetServices(object obj)
            {
                var type = obj.GetType();
                var properties = type.GetProperties();
                foreach (var property in properties)
                {
                    if (property.PropertyType.GetInterface(nameof(IService)) == null)
                    {
                        continue;
                    }
                    if (!Services.ContainsKey(property.PropertyType))
                    {
                        throw new Exception($"Service type '{property.PropertyType.Name}' is not loaded for '{obj.GetType().Name}'");
                    }

                    property.SetValue(obj, Services[property.PropertyType]);
                }
            }
        }
    }
}
