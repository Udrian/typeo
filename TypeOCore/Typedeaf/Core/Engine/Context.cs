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
        public class Context
        {
            public string Name { get; private set; }
            public Game Game { get; private set; }
            public DateTime LastTick { get; private set; }
            public List<Module> Modules { get; set; }
            public Dictionary<Type, Hardware> Hardwares { get; set; }
            public Dictionary<Type, Service> Services { get; set; }
            public Dictionary<Type, Type> ContentBinding { get; set; }
            public ILogger Logger { get; set; }

            public Context(Game game, string name) : base()
            {
                Name = name;
                Game = game;
                LastTick = DateTime.UtcNow;
                Modules = new List<Module>();
                Hardwares = new Dictionary<Type, Hardware>();
                Services = new Dictionary<Type, Service>();
                ContentBinding = new Dictionary<Type, Type>();
            }

            private bool ExitApplication = false;
            public void Exit()
            {
                ExitApplication = true;
            }

            public void Start()
            {
                if(Logger is null)
                {
                    Logger = new DefaultLogger
                    {
                        LogLevel = LogLevel.None
                    };
                }
                //Initialize Hardware
                foreach (var hardware in Hardwares.Values)
                {
                    hardware.Initialize();
                    SetLogger(hardware);
                }

                //Create Services
                foreach (var servicePair in Services)
                {
                    var service = servicePair.Value;
                    //Add Hardware to service using reflection on the Service properties
                    SetHardwares(service);
                    SetServices(service);
                    SetLogger(service);

                    service.Initialize();

                    //Game.AddService(servicePair.Key, service);
                }

                //Set modules Hardware and initialize
                foreach (var module in Modules)
                {
                    SetHardwares(module);
                    SetServices(module);
                    SetLogger(module);
                    module.Initialize();
                }

                //Initialize the game
                SetServices(Game);
                SetLogger(Game);
                Game.Initialize();

                while (!ExitApplication)
                {
                    var dt = (DateTime.UtcNow - LastTick).TotalSeconds;
                    LastTick = DateTime.UtcNow;

                    foreach (var module in Modules)
                    {
                        if ((module as IIsUpdatable)?.Pause == false)
                            (module as IIsUpdatable)?.Update(dt);
                    }

                    foreach (var hardware in Hardwares.Values)
                    {
                        if ((hardware as IIsUpdatable)?.Pause == false)
                            (hardware as IIsUpdatable)?.Update(dt);
                    }

                    foreach (var service in Services.Values)
                    {
                        if ((service as IIsUpdatable)?.Pause == false)
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
                        throw new InvalidOperationException($"Hardware type '{property.PropertyType.Name}' is not loaded for '{obj.GetType().Name}'");
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
                        throw new InvalidOperationException($"Service type '{property.PropertyType.Name}' is not loaded for '{obj.GetType().Name}'");
                    }

                    property.SetValue(obj, Services[property.PropertyType]);
                }
            }

            public void SetLogger(object obj)
            {
                var type = obj.GetType();
                var properties = type.GetProperties();
                foreach (var property in properties)
                {
                    if (property.PropertyType != typeof(ILogger))
                    {
                        continue;
                    }

                    property.SetValue(obj, Logger);
                    break;
                }
            }
        }
    }
}
