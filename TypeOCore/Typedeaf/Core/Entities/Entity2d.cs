using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities
    {
        public abstract class Entity2d : Entity, IDrawContext2d
        {
            public Vec2    Position { get; set; }
            public Vec2    Scale    { get; set; }
            public double  Rotation { get; set; }
            public Color   Color    { get; set; }
            public Flipped Flipped  { get; set; }
            public Vec2    Size     { get; set; }
            public Vec2    Origin   { get; set; }

            public new Entity2d Parent { get { return base.Parent as Entity2d; } set { base.Parent = value as Entity2d; } }
            
            protected Entity2d() : base()
            {
                (this as IDrawContext2d).InitializeDrawContext2d();
                Size = Vec2.Zero;
                Origin = Vec2.Zero;
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