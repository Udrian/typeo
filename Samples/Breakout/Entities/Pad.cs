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
        public int BlockSize { get; set; } = 25;
        public int Blocks { get { return (int)Size.X; } set { Size = new Vec2(value * BlockSize, BlockSize); } }

        public override void Initialize()
        {
            Blocks = 10;
            Position = new Vec2((Scene.Window.Size.X - Size.X) / 2, Scene.Window.Size.Y - (Size.Y * 2));
            Speed = Scene.Window.Size.X;
        }

        public override void Cleanup() 
        {
        }

        public void Update(double dt)
        {
            if (KeyboardInputService.IsDown("Left") && Position.X > 0)
            {
                Position.X -= Speed * dt;
            }
            else if (KeyboardInputService.IsDown("Right") && Position.X + Size.X < Scene.Window.Size.X)
            {
                Position.X += Speed * dt;
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
                    var p = ((ball.Position.X + ball.Size.X/2) - (Position.X + Size.X/2)) / Size.X - 0.5;

                    if(p >= -0.15 && p <= 0.15)
                        ball.Direction = new Vec2(0, -1);
                    else
                        ball.Direction = new Vec2(ball.Direction.X + p, -ball.Direction.Y);
                    ball.Direction.Normalize();
                    ball.Position.Y = Position.Y - ball.Size.Y - 1;
                }
            }

            foreach (var powerup in Scene.Entities.List<Powerup>())
            {
                var r1x = Position.X;
                var r1y = Position.Y;
                var r1w = Size.X;
                var r1h = Size.Y;

                var r2x = powerup.Position.X;
                var r2y = powerup.Position.Y;
                var r2w = powerup.Size.X;
                var r2h = powerup.Size.Y;

                if (r1x + r1w >= r2x &&
                    r1x <= r2x + r2w &&
                    r1y + r1h >= r2y &&
                    r1y <= r2y + r2h)
                {
                    powerup.Pickup();
                }
            }
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, Size, true, Color.CapeHoney);
        }
    }
}