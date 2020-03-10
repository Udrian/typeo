using System;
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
                var w = 0.5 * (Size.X + brick.Size.X);
                var h = 0.5 * (Size.Y + brick.Size.Y);
                var dx = (Position + Size / 2).X - (brick.Position + brick.Size / 2).X;
                var dy = (Position + Size / 2).Y - (brick.Position + brick.Size / 2).Y;

                if (Math.Abs(dx) <= w && Math.Abs(dy) <= h)
                {
                    var wy = w * dy;
                    var hx = h * dx;

                    if (wy > hx)
                    {
                        if (wy > -hx)
                        {
                            Direction.Y = Math.Abs(Direction.Y);
                        }
                        else
                        {
                            Direction.X = -Math.Abs(Direction.X);
                        }
                    }

                    else
                    {
                        if (wy > -hx)
                        {
                            Direction.X = Math.Abs(Direction.X);
                        }
                        else
                        {
                            Direction.Y = -Math.Abs(Direction.Y);
                        }
                    }

                    brick.Hit();
                }
            }
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, Size, true, Color.CapeHoney);
        }
    }
}
