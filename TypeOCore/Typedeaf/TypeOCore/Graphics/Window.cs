using System;
using System.Collections.Generic;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Contents;
using Typedeaf.TypeOCore.Interfaces;

namespace Typedeaf.TypeOCore
{
    namespace Graphics
    {
        public abstract partial class Window : IHasTypeO
        {
            TypeO IHasTypeO.TypeO { get; set; }
            protected TypeO TypeO { get => (this as IHasTypeO).TypeO; set => (this as IHasTypeO).TypeO = value; }
            public Game Game { get; set; }

            protected Window()
            {
                Scenes = new Dictionary<Type, Scene>();
            }

            public abstract void Initialize(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false);

            public virtual string Title      { get; set; }
            public virtual Vec2   Position   { get; set; }
            public virtual Vec2   Size       { get; set; }
            public virtual bool   Fullscreen { get; set; }
            public virtual bool   Borderless { get; set; }

            public virtual void Update(double dt)
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
