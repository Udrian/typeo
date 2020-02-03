using TypeOEngine.Typedeaf.Core.Engine.Graphics;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables
    {
        public abstract class Drawable
        {
            protected Drawable() { }

            public abstract void Init();
            public abstract void Draw(Entity entity, Canvas canvas);
        }
    }
}
