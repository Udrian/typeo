using TypeOEngine.Typedeaf.Core.Common;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities
    {
        public abstract class Entity2d : Entity
        {
            public Vec2    Position { get; set; } = Vec2.Zero;
            public Vec2    Scale    { get; set; } = Vec2.One;
            public Vec2    Size     { get; set; } = Vec2.Zero;
            public double  Rotation { get; set; }
            public Vec2    Origin   { get; set; } = Vec2.Zero;
            public Color   Color    { get; set; } = Color.White;
            public Flipped Flipped  { get; set; }

            public new Entity2d Parent { get { return base.Parent as Entity2d; } set { base.Parent = value as Entity2d; } }

            protected Entity2d() : base() {}
        }
    }
}