using System;
using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
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
                //We need to set Context here before so that the logger works in InitializeObject
                (Logger as IHasContext)?.SetContext(this);
                InitializeObject(Logger);
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
                    InitializeObject(hardware);
                    hardware.Initialize();

                    Logger.Log($"Hardware of type '{hardware.GetType().FullName}' loaded");
                }

                //Create Services
                foreach (var servicePair in Services)
                {
                    var service = servicePair.Value;
                    InitializeObject(service);
                    service.Initialize();

                    Logger.Log($"Service of type '{service.GetType().FullName}' loaded");
                }

                //Set modules Hardware and initialize
                foreach (var module in Modules)
                {
                    InitializeObject(module);
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
                InitializeObject(Game);
                Game.Initialize();

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

            public void InitializeObject(object obj, object from = null)
            {
                Logger.Log($"Initializing obj '{obj.GetType().FullName}'" + (from != null ? $" from '{from.GetType().FullName}'" : ""));

                (obj as IHasContext)?.SetContext(this);
                SetHardwares(obj);
                SetServices(obj);
                SetLogger(obj);

                if(obj is IHasGame)
                {
                    Logger.Log(LogLevel.Debug, $"Injecting Game of type '{Game.GetType().FullName}' into {obj.GetType().FullName}");
                    (obj as IHasGame).Game = Game;
                }

                if((obj is IHasData))
                {
                    var hasData = (obj as IHasData);

                    if(obj is Logic && from is IHasData)
                    {
                        (obj as IHasData).EntityData = (from as IHasData).EntityData;
                        Logger.Log(LogLevel.Debug, $"Injecting EntityData of type '{(obj as IHasData).EntityData.GetType().FullName}' from '{from.GetType().FullName}' into {obj.GetType().FullName}");
                    }
                    else
                    {
                        hasData.CreateData();
                        Logger.Log(LogLevel.Debug, $"Creating EntityData of type '{(obj as IHasData).EntityData.GetType().FullName}' into {obj.GetType().FullName}");
                        hasData.EntityData.Initialize();
                    }
                }

                if (obj is IHasScene)
                {
                    if (from is Scene)
                    {
                        (obj as IHasScene).Scene = from as Scene;
                    }
                    else
                    {
                        (obj as IHasScene).Scene = (from as IHasScene)?.Scene;
                    }
                    Logger.Log(LogLevel.Debug, $"Injecting Scene of type '{(obj as IHasScene).Scene?.GetType().FullName}' from '{from.GetType().FullName}' into {obj.GetType().FullName}");
                }

                if (obj is IHasDrawable)
                {
                    var hasDrawable = obj as IHasDrawable;

                    hasDrawable.CreateDrawable(obj as Entity);
                    Logger.Log(LogLevel.Debug, $"Creating Drawable of type '{hasDrawable.Drawable?.GetType().FullName}' into {obj.GetType().FullName}");
                    InitializeObject(hasDrawable.Drawable, obj);

                    hasDrawable.Drawable.Initialize();
                }

                if (obj is IHasEntity)
                {
                    (obj as IHasEntity).Entity = from as Entity;
                    Logger.Log(LogLevel.Debug, $"Injecting Entity of type '{(obj as IHasEntity).Entity?.GetType().FullName}' from '{from.GetType().FullName}' into {obj.GetType().FullName}");
                }

                if (obj is IHasEntities)
                {
                    var hasEntities = obj as IHasEntities;

                    hasEntities.Entities = new EntityList();
                    Logger.Log(LogLevel.Debug, $"Creating EntityList in {obj.GetType().FullName}");
                    InitializeObject(hasEntities.Entities, obj);
                }

                if (obj is IHasLogic)
                {
                    var hasLogic = obj as IHasLogic;
                    hasLogic.CreateLogic();
                    Logger.Log(LogLevel.Debug, $"Creating Logic of type '{hasLogic.Logic.GetType().FullName}' into {obj.GetType().FullName}");
                    InitializeObject(hasLogic.Logic, obj);

                    hasLogic.Logic.Initialize();
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

            private void SetServices(object obj)
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

            private void SetLogger(object obj)
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
