using Typedeaf.TypeOCore.Entities.Drawables;
using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Entities
    {
        public interface IHasDrawable
        {
            //TODO: I'm not super happy about having this as an interface, investigate further. Might need to .net core first
            public void DrawDrawable(Canvas canvas);
        }
        public interface IHasDrawable<D> : IHasDrawable where D : Drawable
        {
            D Drawable { get; set; }
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