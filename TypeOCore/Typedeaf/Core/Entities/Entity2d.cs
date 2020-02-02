using Typedeaf.Common;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities
    {
        public abstract class Entity2d : Entity
        {
            public Vec2    Position { get; set; } = Vec2.Zero;
            public Vec2    Scale    { get; set; } = Vec2.One;
            public double  Rotation { get; set; }
            public Vec2    Origin   { get; set; } = Vec2.Zero;
            public Color   Color    { get; set; } = Color.White;
            public Flipped Flipped  { get; set; }

            protected Entity2d() : base() {}
        }
    }
}