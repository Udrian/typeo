using System;
using System.Collections.Generic;
using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Graphics
    {
        public abstract partial class Window : IHasTypeO
        {
            TypeO IHasTypeO.TypeO { get; set; }
            protected TypeO TypeO { get => (this as IHasTypeO).TypeO; set => (this as IHasTypeO).TypeO = value; }
            public Game Game { get; set; }

            public virtual string Title { get; set; }
            public virtual Vec2 Size { get; set; }

            protected Window()
            {
                Scenes = new Dictionary<Type, Scene>();
            }

            public abstract void Initialize();

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
