using TypeOEngine.Typedeaf.Core.Common;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities
    {
        public abstract class Entity2d : Entity
        {
            public Vec2    Position { get; set; }
            public Vec2    Scale    { get; set; }
            public double  Rotation { get; set; }
            public Vec2    Size     { get; set; }
            public Vec2    Origin   { get; set; }

            public new Entity2d Parent { get { return base.Parent as Entity2d; } set { base.Parent = value as Entity2d; } }
            
            protected Entity2d() : base()
            {
                Position = Vec2.Zero;
                Scale    = Vec2.One;
                Rotation = 0;
                Size     = Vec2.Zero;
                Origin   = Vec2.Zero;
            }

            public Rectangle DrawBounds {
                get {
                    return new Rectangle(
                           Position + (Parent?.DrawBounds.Pos  ?? Vec2.Zero),
                           Size//     + (Parent?.DrawBounds.Size ?? Vec2.Zero)
                        );
                }
                set {
                    Position = value.Pos  - (Parent?.DrawBounds.Pos  ?? Vec2.Zero);
                    Size     = value.Size;// - (Parent?.DrawBounds.Size ?? Vec2.Zero);
                }
            }
        }
    }
}