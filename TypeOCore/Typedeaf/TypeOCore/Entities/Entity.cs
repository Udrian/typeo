using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Entities
    {
        public interface IDrawable<T> where T : Canvas
        {
            void Draw(T canvas);
        }

        public interface IUpdatable
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