using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Entities.Drawables
    {
        public abstract class Drawable2d : Drawable<Entity2d<Game>>
        {
            public Drawable2d(Entity2d<Game> entity) : base(entity) { }

            public abstract Vec2 GetSize();
        }
    }
}
