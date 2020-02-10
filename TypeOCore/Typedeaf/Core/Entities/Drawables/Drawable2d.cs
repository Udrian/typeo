using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables
    {
        public abstract class Drawable2d : Drawable, IDrawContext2d
        {
            public Vec2     Position { get; set; }
            public Vec2     Scale    { get; set; }
            public double   Rotation { get; set; }
            public Color    Color    { get; set; }
            public Flipped  Flipped  { get; set; }
            public abstract Vec2 Size { get; protected set; }

            public new Entity2d Entity { get { return base.Entity as Entity2d; } set { base.Entity = value as Entity2d; } }

            protected Drawable2d() : base()
            {
                (this as IDrawContext2d).InitializeDrawContext2d();
            }
        }
    }
}
