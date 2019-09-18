﻿using System;
using System.Collections.Generic;

namespace Typedeaf.TypeO.Engine.Core
{
    public abstract class Module
    {
        public Module(TypeO typeO)
        {
            TypeO = typeO;
        }
        protected TypeO TypeO { get; private set; }
        public abstract void Init();
        public abstract void Cleanup();
        public abstract void Update(float dt);
    }

    public partial class TypeO
    {
        public List<Module> Modules;

        public partial class Runner<T> where T : Game
        {
            public TypeO.Runner<T> LoadModule<C>(params object[] args) where C : Module
            {
                var constructorArgs = new List<object>() { TypeO };
                constructorArgs.AddRange(args);
                var module = (C)Activator.CreateInstance(typeof(C), constructorArgs.ToArray());
                module.Init();
                TypeO.Modules.Add(module);
                return this;
            }
        }
    }
}
