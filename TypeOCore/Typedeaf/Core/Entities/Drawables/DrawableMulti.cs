using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables
    {
        public class DrawableMulti : Drawable
        {
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

                if (drawable is IHasGame)
                {
                    (drawable as IHasGame).Game = (Entity as IHasGame)?.Game;
                }

                drawable.Initialize();

                Drawables.Add(drawable);

                return drawable;
            }
        }
    }
}
