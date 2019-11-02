using System;
using System.Collections.Generic;

namespace Typedeaf.TypeOCore
{
    public abstract class Module : IHasTypeO
    {
        TypeO IHasTypeO.TypeO { get; set; }
        protected TypeO TypeO { get { return (this as IHasTypeO).GetTypeO(); } }
        public abstract void Initialize();
        public abstract void Cleanup();
        public abstract void Update(float dt);
    }

    public partial class TypeO
    {
        private List<Module> Modules;

        public partial class Runner<T> where T : Game
        {
            public TypeO.Runner<T> LoadModule<C>(params object[] args) where C : Module
            {
                var constructorArgs = new List<object>();
                constructorArgs.AddRange(args);
                var module = (C)Activator.CreateInstance(typeof(C), constructorArgs.ToArray());
                (module as IHasTypeO).SetTypeO(TypeO);
                module.Initialize();
                TypeO.Modules.Add(module);
                return this;
            }
        }
    }
}
