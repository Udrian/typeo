using Typedeaf.TypeOCommon;

namespace Typedeaf.TypeOCore
{
    namespace Entities
    {
        public abstract class Entity2d<G> : Entity<G> where G : Game
        {
            public Vec2 Position { get; set; }

            public Entity2d(G game, Vec2 position) : base(game)
            {
                Position = position;
            }
        }
    }
}