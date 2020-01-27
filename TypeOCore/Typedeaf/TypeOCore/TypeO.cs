using System;
using System.Collections.Generic;
using System.Linq;
using Typedeaf.TypeOCore.Engine.Hardwares;
using Typedeaf.TypeOCore.Entities;

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

        public ITypeO AddService<I, S>() where I : class where S : Service, new();
        public ITypeO AddHardware<I, H>() where I : IHardware where H : HardwareBase, new();
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
        private Dictionary<Type, Type> Hardwares { get; set; }
        private Dictionary<Type, Type> Services { get; set; }

        public TypeO() : base()
        {
            LastTick = DateTime.UtcNow;
            Modules = new List<Module>();
            Hardwares = new Dictionary<Type, Type>();
            Services = new Dictionary<Type, Type>();
        }

        private bool ExitApplication = false;
        public void Exit()
        {
            ExitApplication = true;
        }

        public ITypeO AddService<I, S>()
            where I : class
            where S : Service, new()
        {
            Services.Add(typeof(I), typeof(S));
            return this;
        }

        public ITypeO AddHardware<I, H>()
            where I : IHardware
            where H : HardwareBase, new()
        {
            Hardwares.Add(typeof(I), typeof(H));
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
                if (!servicePair.Key.IsInterface)
                {
                    throw new ArgumentException($"Generic argument <{servicePair.Key.Name}> must be of interface type");
                }

                var service = (Service)Activator.CreateInstance(servicePair.Value);
                if (service is IHasGame)
                {
                    (service as IHasGame).SetGame(Game);
                }
                (service as IHasTypeO).SetTypeO(this);

                //Instaniate and add Hardware to service using reflection on the Service properties
                var serviceType = service.GetType();
                var serviceProperties = serviceType.GetProperties();
                foreach(var serviceProperty in serviceProperties)
                {
                    if(serviceProperty.PropertyType.GetInterface(nameof(IHardware)) == null)
                    {
                        continue;
                    }
                    if (!Hardwares.ContainsKey(serviceProperty.PropertyType))
                    {
                        throw new Exception($"Hardware type '{serviceProperty.PropertyType.Name}' is not loaded for Service '{service.GetType().Name}'");
                    }

                    //Instantiate the Hardware
                    var hardware = (HardwareBase)Activator.CreateInstance(Hardwares[serviceProperty.PropertyType]);
                    if (hardware is IHasGame)
                    {
                        (hardware as IHasGame).SetGame(Game);
                    }
                    (hardware as IHasTypeO).SetTypeO(this);
                    hardware.Initialize();

                    serviceProperty.SetValue(service, hardware);
                }

                service.Initialize();

                Game.AddService(servicePair.Key, service);
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
    }
}
