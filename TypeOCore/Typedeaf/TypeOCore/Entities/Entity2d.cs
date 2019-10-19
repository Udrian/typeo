using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Content;

namespace Typedeaf.TypeOCore
{
    namespace Entities
    {
        public abstract class Entity2d<G> : Entity<G> where G : Game
        {
            public Vec2            Position { get; set; }
            public Vec2            Scale    { get; set; } = Vec2.One;
            public double          Rotation { get; set; }
            public Vec2            Origin   { get; set; }
            public Color           Color    { get; set; }
            public Texture.Flipped Flipped  { get; set; }

            public Entity2d(G game, Vec2 position, Vec2? scale = null, double rotation = 0, Vec2 origin = new Vec2(), Color color = null, Texture.Flipped flipped = Texture.Flipped.None) : base(game)
            {
                Position = position;
                Scale    = scale.HasValue ? scale.Value : Scale;
                Rotation = rotation;
                Origin   = origin;
                Color    = color;
                Flipped  = flipped;
            }
        }
    }
}