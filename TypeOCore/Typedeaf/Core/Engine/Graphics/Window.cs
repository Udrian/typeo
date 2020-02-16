using System;
using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Graphics
    {
        public abstract partial class Window : IHasContext, IHasGame
        {
            Context IHasContext.Context { get; set; }
            protected Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }
            public ILogger Logger { get; set; }
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
