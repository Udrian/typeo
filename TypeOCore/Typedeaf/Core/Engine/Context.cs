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
            public List<Type> ModuleReferences { get; set; }
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
                ModuleReferences = new List<Type>();
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
                if(Logger == null)
                {
                    Logger = new DefaultLogger
                    {
                        LogLevel = LogLevel.None
                    };
                }
                (Logger as IHasContext)?.SetContext(this);
                Logger.Log($"\n\r\n\rGame started at: {DateTime.UtcNow.ToString()}");

                //Check if all referenced modules are loaded
                foreach (var moduleReference in ModuleReferences)
                {
                    var found = false;
                    foreach (var module in Modules)
                    {
                        if (module.GetType() == moduleReference)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        var message = $"Referenced Module '{moduleReference.Name}' needs to be loaded";
                        Logger.Log(LogLevel.Fatal, message);
                        throw new InvalidOperationException(message);
                    }
                }

                Logger.Log($"Logger of type '{Logger.GetType().FullName}' loaded");
                //Initialize Hardware
                foreach (var hardware in Hardwares.Values)
                {
                    SetLogger(hardware);
                    hardware.Initialize();

                    Logger.Log($"Hardware of type '{hardware.GetType().FullName}' loaded");
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

                    Logger.Log($"Service of type '{service.GetType().FullName}' loaded");
                }

                //Set modules Hardware and initialize
                foreach (var module in Modules)
                {
                    SetHardwares(module);
                    SetServices(module);
                    SetLogger(module);
                    module.Initialize();

                    Logger.Log($"Module of type '{module.GetType().FullName}' loaded");
                }

                //Setup content binding
                foreach(var binding in ContentBinding)
                {
                    var bindingTo = binding.Value;
                    var bindingFrom = binding.Key;

                    if (!bindingTo.IsSubclassOf(bindingFrom))
                    {
                        var message = $"Content Binding from '{bindingFrom.Name}' must be of a base type to '{bindingTo.Name}'";
                        Logger.Log(LogLevel.Fatal, message);
                        throw new ArgumentException(message);
                    }
                }

                //Initialize the game
                SetServices(Game);
                SetLogger(Game);
                if(Game is IHasScenes)
                {
                    var scenes = new SceneList();
                    (scenes as IHasContext).SetContext(this);
                    (scenes as IHasGame).Game = Game;
                    SetLogger(scenes);
                    (Game as IHasScenes).Scenes = scenes;
                }
                Game.Initialize();

                if ((Game as IHasScenes)?.Scenes.Window == null)
                    Logger.Log(LogLevel.Warning, $"Window have not been instantiated to SceneList on '{Game.GetType().FullName}'");
                if ((Game as IHasScenes)?.Scenes.Canvas == null)
                    Logger.Log(LogLevel.Warning, $"Canvas have not been instantiated to SceneList on '{Game.GetType().FullName}'");
                if ((Game as IHasScenes)?.Scenes.ContentLoader == null)
                    Logger.Log(LogLevel.Warning, $"ContentLoader have not been instantiated to SceneList on '{Game.GetType().FullName}'");

                Logger.Log($"Game of type '{Game.GetType().FullName}' loaded");

                Logger.Log($"Everything loaded successfully, spinning up game loop");
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
                        var message = $"Hardware type '{property.PropertyType.Name}' is not loaded for '{obj.GetType().Name}'";
                        Logger.Log(LogLevel.Fatal, message);
                        throw new InvalidOperationException(message);
                    }

                    Logger.Log($"Hardware '{Hardwares[property.PropertyType].GetType().FullName}' injected to property '{property.Name}' on object '{obj.GetType().FullName}'");
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
                        var message = $"Service type '{property.PropertyType.Name}' is not loaded for '{obj.GetType().Name}'";
                        Logger.Log(LogLevel.Fatal, message);
                        throw new InvalidOperationException(message);
                    }

                    Logger.Log($"Service '{Services[property.PropertyType].GetType().FullName}' injected to property '{property.Name}' on object '{obj.GetType().FullName}'");
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

                    Logger.Log($"Logger injected to property '{property.Name}' on object '{obj.GetType().FullName}'");
                    property.SetValue(obj, Logger);
                    break;
                }
            }
        }
    }
}
