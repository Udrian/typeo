using Typedeaf.TypeOCore.Entities.Drawables;
using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Entities
    {
        public interface IHasDrawable
        {
            public void DrawDrawable(Canvas canvas);
        }

        public interface IHasDrawable<D> : IHasDrawable where D : Drawable
        {
            D Drawable { get; set; }
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

        public interface IIsUpdatable
        {
            void Update(float dt);
        }

        public abstract class Entity
        {
            public Entity() {}
        }

        public abstract class Entity<G> : Entity where G : Game
        {
            public G Game { get; private set; }
            public Entity(G game)
            {
                Game = game;
            }
        }
    }
}