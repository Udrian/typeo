using Typedeaf.Common;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables
    {
        public abstract class Drawable2d : Drawable<Entity2d>
        {
            public Drawable2d(Entity2d entity) : base(entity) { }

            public abstract Vec2 GetSize();
        }
    }
}
