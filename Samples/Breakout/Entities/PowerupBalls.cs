using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;

namespace Breakout.Entities
{
    class PowerupBalls : Powerup
    {
        public override void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, Size, true, Color.LightYellow);
        }

        public override void Pickup()
        {
            var ball = Scene.Entities.Create<Ball>(position: Position);
            ball.Direction = new Vec2(0, -1);
        }
    }
}
