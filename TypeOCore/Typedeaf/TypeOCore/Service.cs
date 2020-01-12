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
    }

    partial class Game
    {
        private Dictionary<Type, Service> Services { get; set; }

        public void AddService<I, S>() where I : class where S : Service, new() {
            if (!typeof(I).IsInterface)
            {
                throw new ArgumentException($"Generic argument <{nameof(I)}> must be of interface type");
            }

            var service = new S();
            if (service is IHasGame)
            {
                (service as IHasGame).SetGame(this);
            }
            (service as IHasTypeO).SetTypeO(TypeO);

            service.Initialize();

            Services.Add(typeof(I), service);
        }

        public I GetService<I>() where I : class {
            if (!Services.ContainsKey(typeof(I))) return default;
            var service = Services[typeof(I)];
            return service as I;
        }

        public List<Service> GetServices() {
            return Services.Values.ToList();
        }

        public void RemoveService<I>() where I : class {
            Services.Remove(typeof(I));
        }
    }
}
