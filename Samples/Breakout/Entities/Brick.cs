using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;

namespace Breakout.Entities
{
    class Brick : Entity2d, IIsDrawable
    {
        public bool Hidden { get; set; }

        public override void Initialize()
        {
            Size = new Vec2(75, 25);
        }

        public override void Cleanup()
        {
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, Size, true, Color.Green);
        }
    }
}
