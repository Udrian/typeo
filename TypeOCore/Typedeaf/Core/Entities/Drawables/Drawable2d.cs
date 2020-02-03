using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables
    {
        public abstract class Drawable2d : Drawable
        {
            protected Drawable2d() : base() { }

            public override void Draw(Entity entity, Canvas canvas)
            {
                if (entity is Entity2d)
                    Draw(entity as Entity2d, canvas);
            }

            public abstract void Draw(Entity2d entity, Canvas canvas);

            public abstract Vec2 Size { get; protected set; }
        }
    }
}
