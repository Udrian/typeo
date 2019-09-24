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

    public partial class Game
    {
        protected Dictionary<Type, Service> Services { get; set; }

        public void AddService<T>(params object[] args) where T : Service {
            var constructorArgs = new List<object>(){ this };
            constructorArgs.AddRange(args);
            Services.Add(typeof(T), (T)Activator.CreateInstance(typeof(T), constructorArgs.ToArray()));
        }

        public void AddService(Service service) {
            Services.Add(service.GetType(), service);

            service.Initialize();
        }

        public T GetService<T>() where T : Service {
            if (!Services.ContainsKey(typeof(T))) return null;
            var service = Services[typeof(T)];
            return (T)Convert.ChangeType(service, typeof(T));
        }

        public List<Service> GetServices() {
            return Services.Values.ToList();
        }

        public void RemoveService<T>() where T : Service {
            Services.Remove(typeof(T));
        }
    }
}
