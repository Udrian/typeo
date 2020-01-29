using TypeOEngine.Typedeaf.Core.Entities.Drawables.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables
    {
        public abstract class Drawable {
            public abstract void Draw(Canvas canvas);
        }

        public abstract class Drawable<E> : Drawable, IIsDrawable where E : Entity
        {
            protected E Entity { get; private set; }
            
            public Drawable(E entity)
            {
                Entity = entity;
            }

            public abstract void Init(E entity);
        }
    }
}
