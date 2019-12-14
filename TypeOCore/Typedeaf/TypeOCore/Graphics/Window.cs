using System.Collections.Generic;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Graphics
    {
        public abstract partial class Window : IHasTypeO
        {
            ITypeO IHasTypeO.TypeO { get; set; }
            protected ITypeO TypeO { get { return (this as IHasTypeO).GetTypeO(); } }
            public Game Game { get; set; }

            protected Window()
            {
                Scenes = new Dictionary<System.Type, Scene>();
            }

            public virtual string Title      { get; set; }
            public virtual Vec2   Position   { get; set; }
            public virtual Vec2   Size       { get; set; }
            public virtual bool   Fullscreen { get; set; }
            public virtual bool   Borderless { get; set; }
        }
    }

    partial class Game
    {
        public W CreateWindow<W>() where W : Window, new()
        {
            var win = new W();
            win.Game = this;
            (win as IHasTypeO).SetTypeO(TypeO);

            return win;
        }
    }
}
