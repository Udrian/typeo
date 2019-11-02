using System;
using System.Collections.Generic;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Graphics
    {
        public abstract partial class Window : IHasTypeO
        {
            TypeO IHasTypeO.TypeO { get; set; }
            protected TypeO TypeO { get { return (this as IHasTypeO).GetTypeO(); } }

            public virtual string Title      { get; set; }
            public virtual Vec2   Position   { get; set; }
            public virtual Vec2   Size       { get; set; }
            public virtual bool   Fullscreen { get; set; }
            public virtual bool   Borderless { get; set; }
        }
    }

    public abstract partial class Game
    {
        public T CreateWindow<T>(params object[] args) where T : Window
        {
            var constructorArgs = new List<object>();
            constructorArgs.AddRange(args);
            var win = (T)Activator.CreateInstance(typeof(T), constructorArgs.ToArray());
            (win as IHasTypeO).SetTypeO(TypeO);
            return win;
        }
    }
}
