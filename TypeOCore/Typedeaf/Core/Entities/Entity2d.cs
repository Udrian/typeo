using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Contents;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities
    {
        public abstract class Entity2d : Entity
        {
            public Vec2            Position { get; set; }
            public Vec2            Scale    { get; set; } = Vec2.One;
            public double          Rotation { get; set; }
            public Vec2            Origin   { get; set; }
            public Color           Color    { get; set; }
            public Texture.Flipped Flipped  { get; set; }

            public Entity2d(Vec2 position = null, Vec2 scale = null, double rotation = 0, Vec2 origin = null, Color color = null, Texture.Flipped flipped = Texture.Flipped.None) : base()
            {
                Position = position ?? Vec2.Zero;
                Scale    = scale    ?? Vec2.One;
                Rotation = rotation;
                Origin   = origin;
                Color    = color;
                Flipped  = flipped;
            }
        }
    }
}