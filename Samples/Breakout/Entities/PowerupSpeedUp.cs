using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;

namespace Breakout.Entities
{
    class PowerupSpeedUp : Powerup
    {
        public override void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, Size, true, Color.DarkGreen);
        }

        public override void Pickup()
        {
            foreach(var ball in Scene.Entities.List<Ball>())
            {
                ball.Speed *= 2;
            }
        }
    }
}
