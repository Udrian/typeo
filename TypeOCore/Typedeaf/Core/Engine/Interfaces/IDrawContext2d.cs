using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Entities;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Interfaces
    {
        public interface IDrawContext2d
        {
            public Vec2    Position { get; set; }
            public Vec2    Scale    { get; set; }
            public double  Rotation { get; set; }
            public Color   Color    { get; set; }
            public Flipped Flipped  { get; set; }

            public void InitializeDrawContext2d()
            {
                Position = Vec2.Zero;
                Scale    = Vec2.One;
                Rotation = 0;
                Color    = Color.White;
                Flipped  = Flipped.None;
            }
        }
    }
}
