using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables
    {
        public abstract class Drawable : IDrawable
        {
            public Entity Entity { get; internal set; }
            public bool Hidden { get; set; }

            protected Drawable() { }

            public abstract void Initialize();
            public abstract void Cleanup();
            public abstract void Draw(Canvas canvas);
        }
    }
}
