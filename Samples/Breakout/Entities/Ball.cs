using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace Breakout.Entities
{
    class Ball : Entity2d, IIsUpdatable, IIsDrawable, IHasScene, IHasGame
    {
        public bool Pause { get; set; }
        public bool Hidden { get; set; }
        public Scene Scene { get; set; }

        public double Speed { get; set; }
        public Vec2 Direction { get; set; }
        public Game Game { get; set; }

        public override void Initialize()
        {
            Size = new Vec2(25);
            Speed = Scene.Window.Size.Y / 4;
        }

        public override void Cleanup()
        {
        }

        public void Update(double dt)
        {
            Position += Direction * Speed * dt;
            if(Position.Y <= 0)
            {
                Position.Y = 0;
                Direction.Y = -Direction.Y;
            }
            if (Position.X <= 0)
            {
                Position.X = 0;
                Direction.X = -Direction.X;
            }
            if (Position.X >= Scene.Window.Size.X - Size.X)
            {
                Position.X = Scene.Window.Size.X - Size.X;
                Direction.X = -Direction.X;
            }
            if (Position.Y >= Scene.Window.Size.Y)
            {
                Remove();
            }

            foreach(var brick in Scene.Entities.List<Brick>())
            {
                var r1x = Position.X;
                var r1y = Position.Y;
                var r1w = Size.X;
                var r1h = Size.Y;

                var r2x = brick.Position.X;
                var r2y = brick.Position.Y;
                var r2w = brick.Size.X;
                var r2h = brick.Size.Y;

                if (r1x + r1w >= r2x &&
                    r1x <= r2x + r2w &&
                    r1y + r1h >= r2y &&
                    r1y <= r2y + r2h)
                {
                    //TODO: Make proper breakout brick bounce logic
                    Direction.Y = -Direction.Y;
                    Position.Y = brick.Position.Y + Size.Y + 1;
                    brick.Hit();
                    continue;
                }
            }
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, Size, true, Color.CapeHoney);
        }
    }
}
