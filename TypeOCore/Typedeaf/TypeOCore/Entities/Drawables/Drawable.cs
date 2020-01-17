using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Entities.Drawables
    {
        public interface IHasDrawable
        {
            public void DrawDrawable(Canvas canvas);
        }

        public interface IHasDrawable<D> : IHasDrawable where D : Drawable
        {
            public D Drawable { get; set; }
            void IHasDrawable.DrawDrawable(Canvas canvas)
            {
                Drawable.Draw(canvas);
            }
        }

        public interface IIsDrawable
        {
            void Draw(Canvas canvas);
        }

        public interface IIsDrawable<C> where C : Canvas
        {
            void Draw(C canvas);
        }

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
