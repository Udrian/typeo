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
            Position = (Scene.Window.Size - Size) / 2;
            Direction = new Vec2(0, 1);
            Speed = Scene.Window.Size.Y / 4;
        }

        public override void Cleanup()
        {
        }

        public void Update(double dt)
        {
            Position += Direction * Speed * dt;
            if(Position.Y >= Scene.Window.Size.Y)
            {
                Game.Exit();
            }
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, Size, true, Color.CapeHoney);
        }
    }
}
