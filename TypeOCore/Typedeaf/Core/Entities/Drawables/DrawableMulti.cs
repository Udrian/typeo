using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables
    {
        public class DrawableMulti : Drawable, IHasContext
        {
            Context IHasContext.Context { get; set; }
            private Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }

            protected List<Drawable> Drawables { get; set; }

            public DrawableMulti() : base() { }

            public override void Initialize()
            {
                Drawables = new List<Drawable>();
            }

            public override void Draw(Canvas canvas)
            {
                foreach(var drawable in Drawables)
                {
                    drawable.Draw(canvas);
                }
            }

            public D CreateDrawable<D>() where D : Drawable, new()
            {
                var drawable = new D
                {
                    Entity = Entity
                };

                Context.InitializeObject(drawable, Entity);
                drawable.Initialize();

                Drawables.Add(drawable);

                return drawable;
            }
        }
    }
}
