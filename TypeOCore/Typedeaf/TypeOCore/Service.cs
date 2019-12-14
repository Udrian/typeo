using System;
using System.Collections.Generic;
using System.Linq;

namespace Typedeaf.TypeOCore
{
    public abstract class Service : IHasTypeO
    {
        ITypeO IHasTypeO.TypeO { get; set; }
        protected ITypeO TypeO { get { return (this as IHasTypeO).GetTypeO(); } }

        public bool Pause { get; set; }

        protected Service() {}

        public abstract void Initialize();
        public abstract void Update(float dt);
    }

    partial class Game
    {
        private Dictionary<Type, Service> Services { get; set; }

        public void AddService<S>() where S : Service, new() {
            var service = new S();
            if (service is IHasGame)
            {
                (service as IHasGame).SetGame(this);
            }
            (service as IHasTypeO).SetTypeO(TypeO);

            service.Initialize();

            Services.Add(typeof(S), service);
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
