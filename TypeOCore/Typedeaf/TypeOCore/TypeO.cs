using System;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Engine.Hardwares;
using Typedeaf.TypeOCore.Entities;
using Typedeaf.TypeOCore.Services;

namespace Typedeaf.TypeOCore
{
    public interface IHasTypeO
    {
        protected ITypeO TypeO { get; set; }
        public void SetTypeO(ITypeO typeO)
        {
            TypeO = typeO;
        }
        public ITypeO GetTypeO()
        {
            return TypeO;
        }
    }

    public interface ITypeO
    {
        public void Start();
        public void Exit();

        public ITypeO AddService<I, S>() where I : IService where S : Service, new();
        public ITypeO AddHardware<I, H>() where I : IHardware where H : Hardware, new();
        public M LoadModule<M>() where M : Module, new();
    }

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
        private Dictionary<Type, Type> Services { get; set; }

        public TypeO() : base()
        {
            LastTick = DateTime.UtcNow;
            Modules = new List<Module>();
            Hardwares = new Dictionary<Type, Hardware>();
            Services = new Dictionary<Type, Type>();
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
            Services.Add(typeof(I), typeof(S));
            return this;
        }

        public ITypeO AddHardware<I, H>()
            where I : IHardware
            where H : Hardware, new()
        {
            //Instantiate the Hardware
            var hardware = new H();
            if (hardware is IHasGame)
            {
                (hardware as IHasGame).SetGame(Game);
            }
            (hardware as IHasTypeO).SetTypeO(this);
            hardware.Initialize();

            Hardwares.Add(typeof(I), hardware);
            return this;
        }

        public M LoadModule<M>() where M : Module, new()
        {
            var module = new M();
            (module as IHasTypeO).SetTypeO(this);
            if(module is IHasGame)
            {
                (module as IHasGame).SetGame(Game);
            }
            module.Initialize();
            Modules.Add(module);

            return module;
        }

        public void Start()
        {
            //Create Services
            foreach(var servicePair in Services)
            {
                var service = (Service)Activator.CreateInstance(servicePair.Value);
                if (service is IHasGame)
                {
                    (service as IHasGame).SetGame(Game);
                }
                (service as IHasTypeO).SetTypeO(this);

                //Add Hardware to service using reflection on the Service properties
                SetHardware(service);

                service.Initialize();

                Game.AddService(servicePair.Key, service);
            }

            //Set modules Hardware
            foreach (var module in Modules)
            {
                SetHardware(module);
            }

            //Initialize the game
            Game.Initialize();

            while (!ExitApplication)
            {
                var dt = (DateTime.UtcNow - LastTick).TotalSeconds;
                LastTick = DateTime.UtcNow;

                foreach (var module in Modules)
                {
                    (module as IIsUpdatable)?.Update(dt);
                }

                foreach (var service in Game.GetServices())
                {
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

        private void SetHardware(object obj)
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
    }
}
