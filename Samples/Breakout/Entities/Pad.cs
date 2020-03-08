using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;

namespace Breakout.Entities
{
    class Pad : Entity2d, IIsDrawable, IHasScene, IIsUpdatable
    {
        public IKeyboardInputService KeyboardInputService { get; set; }
        public bool Hidden { get; set; }
        public Scene Scene { get; set; }
        public bool Pause { get; set; }

        public double Speed { get; set; }

        public override void Initialize()
        {
            Size = new Vec2(100, 25);
            Position = new Vec2((Scene.Window.Size.X - 100) / 2, Scene.Window.Size.Y - (Size.Y * 2));
            Speed = Scene.Window.Size.X;
        }

        public override void Cleanup()
        {
        }

        public void Update(double dt)
        {
            var moveSpeed = 0d;
            if (KeyboardInputService.IsDown("Left") && Position.X > 0)
            {
                moveSpeed = Speed;
                Position.X -= moveSpeed * dt;
            }
            else if (KeyboardInputService.IsDown("Right") && Position.X + Size.X < Scene.Window.Size.X)
            {
                moveSpeed = Speed;
                Position.X += moveSpeed * dt;
            }

            if (Position.X < 0) Position.X = 0;
            if (Position.X > Scene.Window.Size.X - Size.X) Position.X = Scene.Window.Size.X - Size.X;

            foreach(var ball in Scene.Entities.List<Ball>())
            {
                var r1x = Position.X;
                var r1y = Position.Y;
                var r1w = Size.X;
                var r1h = Size.Y;

                var r2x = ball.Position.X;
                var r2y = ball.Position.Y;
                var r2w = ball.Size.X;
                var r2h = ball.Size.Y;

                if (r1x + r1w >= r2x &&
                    r1x <= r2x + r2w &&
                    r1y + r1h >= r2y &&
                    r1y <= r2y + r2h)
                {
                    ball.Direction.Y = -ball.Direction.Y;
                    ball.Position.Y = Position.Y - ball.Size.Y - 1;
                }
            }
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, Size, true, Color.CapeHoney);
        }
    }
}