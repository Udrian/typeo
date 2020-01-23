using System;
using System.Collections.Generic;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Contents;

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
                Scenes = new Dictionary<Type, Scene>();
            }

            public virtual string Title      { get; set; }
            public virtual Vec2   Position   { get; set; }
            public virtual Vec2   Size       { get; set; }
            public virtual bool   Fullscreen { get; set; }
            public virtual bool   Borderless { get; set; }

            public virtual void Update(float dt)
            {
                CurrentScene?.Update(dt);
            }
            public virtual void Draw()
            {
                CurrentScene?.Draw();
            }

            public abstract Canvas CreateCanvas();
            public abstract Canvas CreateCanvas(Rectangle viewport);
            public abstract ContentLoader CreateContentLoader(Canvas canvas);
        }
    }
}
