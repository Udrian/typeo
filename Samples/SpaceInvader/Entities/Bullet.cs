using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Entities
{
    public class Bullet : Entity2d, IIsDrawable, IIsUpdatable
    {
        public double Speed { get; set; } = 500;
        public Vec2 Size { get; set; } = new Vec2(10, 10);

        public Bullet(Vec2 position) : base(position) { }

        public override void Initialize() {}

        public void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, Size, true, Color.CapeHoney);
        }

        public void Update(double dt)
        {
            Position = new Vec2(Position.X, Position.Y - Speed * dt);

            if (Position.Y <= -Size.Y)
                Remove();
        }

        public bool WillBeDeleted { get; private set; }
        public void Remove()
        {
            WillBeDeleted = true;
        }
    }
}
