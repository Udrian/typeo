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

        public void AddService(Type interfaceType, Service service) {
            Services.Add(interfaceType, service);
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
