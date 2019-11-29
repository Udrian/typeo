using System;
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
            public Game Game { get; set; }

            protected Window() { }

            public virtual string Title      { get; set; }
            public virtual Vec2   Position   { get; set; }
            public virtual Vec2   Size       { get; set; }
            public virtual bool   Fullscreen { get; set; }
            public virtual bool   Borderless { get; set; }
        }
    }

    partial class Game
    {
        public T CreateWindow<T>() where T : Window, new()
        {
            var win = new T();
            win.Game = this;
            (win as IHasTypeO).SetTypeO(TypeO);
            return win;
        }
    }
}
