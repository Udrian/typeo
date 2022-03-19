﻿using TypeD.Models.Interfaces;

namespace TypeD
{
    public abstract class TypeDModuleInitializer
    {
        public IHookModel Hooks { get; internal set; }
        public IResourceModel Resources { get; internal set; }
        public abstract void Initializer();
        public abstract void Uninitializer();
    }
}
