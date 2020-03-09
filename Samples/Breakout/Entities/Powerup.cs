using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace Breakout.Entities
{
    class Powerup : Entity2d, IIsUpdatable, IIsDrawable, IHasScene
    {
        public bool Hidden { get; set; }
        public bool Pause { get; set; }
        public Scene Scene { get; set; }

        public double Speed { get; set; }

        public override void Initialize()
        {
            Size = new Vec2(25);
            Speed = Scene.Window.Size.Y/2;
        }

        public override void Cleanup()
        {
        }

        public void Update(double dt)
        {
            Position.Y += Speed * dt;

            if(Position.Y >= Scene.Window.Size.Y)
            {
                Remove();
            }
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, Size, true, Color.DarkGreen);
        }

        public void Pickup()
        {
            var ball = Scene.Entities.Create<Ball>(position: Position);
                ball.Direction = new Vec2(0, -1);

            Remove();
        }
    }
}
