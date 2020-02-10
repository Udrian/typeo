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
            
            protected Entity2d() : base()
            {
                (this as IDrawContext2d).InitializeDrawContext2d();
                Size = Vec2.Zero;
                Origin = Vec2.Zero;
            }
        }
    }
}