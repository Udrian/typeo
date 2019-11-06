using System;
using System.Collections.Generic;
using System.Linq;

namespace Typedeaf.TypeOCore
{
    public abstract class Service
    {
        public bool Pause { get; set; }

        public Game Game { get; private set; }
        public Service(Game game)
        {
            Game = game;
        }

        public abstract void Initialize();
        public abstract void Update(float dt);
    }

    partial class Game
    {
        private Dictionary<Type, Service> Services { get; set; }

        public void AddService<S>(params object[] args) where S : Service {
            var constructorArgs = new List<object>(){ this };
            constructorArgs.AddRange(args);
            Services.Add(typeof(S), (S)Activator.CreateInstance(typeof(S), constructorArgs.ToArray()));
        }

        public void AddService(Service service) {
            Services.Add(service.GetType(), service);

            service.Initialize();
        }

        public S GetService<S>() where S : Service {
            if (!Services.ContainsKey(typeof(S))) return null;
            var service = Services[typeof(S)];
            return (S)Convert.ChangeType(service, typeof(S));
        }

        public List<Service> GetServices() {
            return Services.Values.ToList();
        }

        public void RemoveService<S>() where S : Service {
            Services.Remove(typeof(S));
        }
    }
}
