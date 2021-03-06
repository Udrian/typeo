﻿using System;
using System.Collections.Generic;
using System.Reflection;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
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
            public TypeO TypeO { get; private set; }
            public TimeSpan TimeSinceStart { get; private set; }
            public DateTime StartTime { get; private set; }
            public DateTime LastTick { get; private set; }
            public List<Module> Modules { get; set; }
            public List<Tuple<Type, Version>> ModuleRequirements { get; set; }
            public Version RequiredTypeOVersion { get; set; }
            public Dictionary<Type, Hardware> Hardwares { get; set; }
            public Dictionary<Type, Service> Services { get; set; }
            public Dictionary<Type, Type> ContentBinding { get; set; }
            public ILogger Logger { get; set; }

            internal Context(Game game, TypeO typeO, string name) : base()
            {
                Name = name;
                Game = game;
                TypeO = typeO;
                LastTick = DateTime.UtcNow;
                Modules = new List<Module>();
                ModuleRequirements = new List<Tuple<Type, Version>>();
                RequiredTypeOVersion = new Version(0, 0, 0);
                Hardwares = new Dictionary<Type, Hardware>();
                Services = new Dictionary<Type, Service>();
                ContentBinding = new Dictionary<Type, Type>();
            }

            private bool ExitApplication = false;
            public void Exit()
            {
                ExitApplication = true;
            }

            internal void Start()
            {
                StartTime = DateTime.UtcNow;

                foreach(var module in Modules)
                {
                    if(module.WillLoadExtensions)
                    {
                        module.LoadExtensions();
                    }
                }

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
                Logger.Log($"Game started at: {StartTime}");
                Logger.Log($"Logger of type '{Logger.GetType().FullName}' loaded");

                //Initialize Hardware
                foreach(var hardware in Hardwares.Values)
                {
                    InitializeObject(hardware);
                    hardware.Initialize();

                    Logger.Log($"Hardware of type '{hardware.GetType().FullName}' loaded");
                }

                //Create Services
                foreach(var servicePair in Services)
                {
                    var service = servicePair.Value;
                    InitializeObject(service);
                    service.Initialize();

                    Logger.Log($"Service of type '{service.GetType().FullName}' loaded");
                }

                //Set modules Hardware and initialize
                foreach(var module in Modules)
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

                    if(!bindingTo.IsSubclassOf(bindingFrom))
                    {
                        var message = $"Content Binding from '{bindingFrom.Name}' must be of a base type to '{bindingTo.Name}'";
                        Logger.Log(LogLevel.Fatal, message);
                        throw new ArgumentException(message);
                    }
                }

                if(!TypeO.Version.Eligable(RequiredTypeOVersion))
                {
                    var message = $"TypeOCore required at atleast version '{RequiredTypeOVersion}'";
                    Logger.Log(LogLevel.Fatal, message);
                    throw new InvalidOperationException(message);
                }

                //Check if all referenced modules are loaded
                foreach(var moduleReference in ModuleRequirements)
                {
                    var found = false;
                    foreach(var module in Modules)
                    {
                        if(module.GetType() == moduleReference.Item1 && module.Version.Eligable(moduleReference.Item2))
                        {
                            found = true;
                            break;
                        }
                    }
                    if(!found)
                    {
                        var message = $"Required Module '{moduleReference.Item1.FullName}' at atleast version '{moduleReference.Item2}' needs to be loaded";
                        Logger.Log(LogLevel.Fatal, message);
                        throw new InvalidOperationException(message);
                    }
                }

                //Initialize the game
                InitializeObject(Game);
                Game.InternalInitialize();
                Game.Initialize();

                Logger.Log($"Game of type '{Game.GetType().FullName}' loaded");

                Logger.Log($"Everything loaded successfully, spinning up game loop");
                while(!ExitApplication)
                {
                    var now = DateTime.UtcNow;
                    TimeSinceStart = (now - StartTime);
                    var dt = (now - LastTick).TotalSeconds;
                    LastTick = now;

                    foreach(var module in Modules)
                    {
                        if((module as IIsUpdatable)?.Pause == false)
                            (module as IIsUpdatable)?.Update(dt);
                    }

                    foreach(var hardware in Hardwares.Values)
                    {
                        if((hardware as IIsUpdatable)?.Pause == false)
                            (hardware as IIsUpdatable)?.Update(dt);
                    }

                    foreach(var service in Services.Values)
                    {
                        if((service as IIsUpdatable)?.Pause == false)
                            (service as IIsUpdatable)?.Update(dt);
                    }

                    Game.Update(dt);
                    Game.Draw();
                }
                Logger.Log("Exiting game, initiating cleanup");

                //Cleanup
                Game.Cleanup();

                foreach(var service in Services)
                {
                    service.Value.Cleanup();
                }

                foreach(var hardware in Hardwares)
                {
                    hardware.Value.Cleanup();
                }

                foreach(var module in Modules)
                {
                    module.Cleanup();
                }

                Logger.Log("Bye bye\n\r\n\r");
                Logger?.Cleanup();
            }

            public void InitializeObject(object obj, object from = null)
            {
                Logger.Log(LogLevel.Debug, $"Initializing obj '{obj.GetType().FullName}'" + (from != null ? $" from '{from.GetType().FullName}'" : ""));

                (obj as IHasContext)?.SetContext(this);
                SetHardwares(obj);
                SetServices(obj);
                SetLogger(obj);

                if(obj is IHasGame)
                {
                    Logger.Log(LogLevel.Ludacris, $"Injecting Game of type '{Game.GetType().FullName}' into {obj.GetType().FullName}");
                    (obj as IHasGame).Game = Game;
                }

                if((obj is IHasData))
                {
                    var hasData = (obj as IHasData);

                    if(obj is Logic && from is IHasData)
                    {
                        (obj as IHasData).EntityData = (from as IHasData).EntityData;
                        Logger.Log(LogLevel.Ludacris, $"Injecting EntityData of type '{(obj as IHasData).EntityData.GetType().FullName}' from '{from.GetType().FullName}' into {obj.GetType().FullName}");
                    }
                    else
                    {
                        hasData.CreateData();
                        if(hasData.EntityData != null)
                        {
                            Logger.Log(LogLevel.Ludacris, $"Creating EntityData of type '{(obj as IHasData).EntityData.GetType().FullName}' into {obj.GetType().FullName}");
                            hasData.EntityData.Initialize();
                        }
                        else
                        {
                            Logger.Log(LogLevel.Ludacris, $"EntityData creation skipped on {obj.GetType().FullName}");
                        }
                    }

                    if((obj as IHasData).EntityData == null)
                    {
                        Logger.Log(LogLevel.Warning, $"EntityData is null in {obj.GetType().FullName}");
                    }
                }

                if(obj is IHasScene)
                {
                    if(from is Scene)
                    {
                        (obj as IHasScene).Scene = from as Scene;
                    }
                    else
                    {
                        (obj as IHasScene).Scene = (from as IHasScene)?.Scene;
                    }
                    Logger.Log(LogLevel.Ludacris, $"Injecting Scene of type '{(obj as IHasScene).Scene?.GetType().FullName}' from '{from.GetType().FullName}' into {obj.GetType().FullName}");
                    if((obj as IHasScene).Scene == null)
                    {
                        Logger.Log(LogLevel.Warning, $"Scene is null in {obj.GetType().FullName}");
                    }
                }

                if(obj is IHasEntity)
                {
                    (obj as IHasEntity).Entity = from as Entity;
                    Logger.Log(LogLevel.Ludacris, $"Injecting Entity of type '{(obj as IHasEntity).Entity?.GetType().FullName}' from '{from.GetType().FullName}' into {obj.GetType().FullName}");

                    if((obj as IHasEntity).Entity == null)
                    {
                        Logger.Log(LogLevel.Warning, $"Entity is null in {obj.GetType().FullName}");
                    }
                }

                if(obj is IHasEntities)
                {
                    var hasEntities = obj as IHasEntities;

                    hasEntities.Entities = new EntityList();
                    Logger.Log(LogLevel.Ludacris, $"Creating EntityList in {obj.GetType().FullName}");
                    InitializeObject(hasEntities.Entities, obj);
                }
            }

            private void SetHardwares(object obj)
            {
                var type = obj.GetType();
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach(var property in properties)
                {
                    if(property.PropertyType.GetInterface(nameof(IHardware)) == null)
                    {
                        continue;
                    }
                    if(!Hardwares.ContainsKey(property.PropertyType))
                    {
                        var message = $"Hardware type '{property.PropertyType.Name}' is not loaded for '{obj.GetType().Name}'";
                        Logger.Log(LogLevel.Fatal, message);
                        throw new InvalidOperationException(message);
                    }

                    Logger.Log(LogLevel.Ludacris, $"Hardware '{Hardwares[property.PropertyType].GetType().FullName}' injected to property '{property.Name}' on object '{obj.GetType().FullName}'");
                    property.SetValue(obj, Hardwares[property.PropertyType]);
                }
            }

            private void SetServices(object obj)
            {
                var type = obj.GetType();
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach(var property in properties)
                {
                    if(property.PropertyType.GetInterface(nameof(IService)) == null)
                    {
                        continue;
                    }
                    if(!Services.ContainsKey(property.PropertyType))
                    {
                        var message = $"Service type '{property.PropertyType.Name}' is not loaded for '{obj.GetType().Name}'";
                        Logger.Log(LogLevel.Fatal, message);
                        throw new InvalidOperationException(message);
                    }

                    Logger.Log(LogLevel.Ludacris, $"Service '{Services[property.PropertyType].GetType().FullName}' injected to property '{property.Name}' on object '{obj.GetType().FullName}'");
                    property.SetValue(obj, Services[property.PropertyType]);
                }
            }

            private void SetLogger(object obj)
            {
                var type = obj.GetType();
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach(var property in properties)
                {
                    if(property.PropertyType != typeof(ILogger))
                    {
                        continue;
                    }

                    Logger.Log(LogLevel.Ludacris, $"Logger injected to property '{property.Name}' on object '{obj.GetType().FullName}'");
                    property.SetValue(obj, Logger);
                    break;
                }
            }

            internal D CreateDrawable<D>(object obj, DrawStack drawStack) where D : Drawable, new()
            {
                Logger.Log(LogLevel.Ludacris, $"Creating Drawable of type '{typeof(D).FullName}' into {obj.GetType().FullName}");

                var drawable = new D()
                {
                    Entity = obj as Entity
                };

                InitializeObject(drawable, obj);
                drawable.Initialize();

                if(drawStack != null)
                {
                    drawStack.Push(drawable);
                }

                return drawable;
            }

            internal void DestroyDrawable(Drawable drawable, DrawStack drawStack)
            {
                if(drawStack != null)
                {
                    drawStack.Pop(drawable);
                }
                drawable.Cleanup();
            }

            internal L CreateLogic<L>(object obj, UpdateLoop updateLoop) where L : Logic, new()
            {
                Logger.Log(LogLevel.Ludacris, $"Creating Logic of type '{typeof(L).FullName}' into {obj.GetType().FullName}");

                var logic = new L()
                {
                    Parent = obj as Entity
                };

                InitializeObject(logic, obj);
                logic.Initialize();

                if(updateLoop != null)
                {
                    updateLoop.Push(logic);
                }

                return logic;
            }

            internal void DestroyLogic(Logic logic, UpdateLoop updateLoop)
            {
                if(updateLoop != null)
                {
                    updateLoop.Pop(logic);
                }
            }
        }
    }
}
