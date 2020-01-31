using Typedeaf.Common;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities
    {
        public abstract class Entity2d : Entity
        {
            public Vec2    Position { get; set; }
            public Vec2    Scale    { get; set; } = Vec2.One;
            public double  Rotation { get; set; }
            public Vec2    Origin   { get; set; }
            public Color   Color    { get; set; }
            public Flipped Flipped  { get; set; }

            protected Entity2d() : base() {}
        }
    }
}