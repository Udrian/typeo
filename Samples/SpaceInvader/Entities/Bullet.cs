using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;

namespace SpaceInvader.Entities
{
    public class BulletData : EntityData
    {
        public double Speed { get; set; }

        public override void Initialize() { }
    }

    public class Bullet : Entity2d, IIsDrawable, IIsUpdatable, IHasData<BulletData>
    {
        public BulletData EntityData { get; set; }

        public Vec2 Size { get; set; } = new Vec2(10, 10);

        public Bullet() : base() { }

        public override void Initialize()
        {
            EntityData = new BulletData()
            {
                Speed = 500
            };
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, Size, true, Color.CapeHoney);
        }

        public void Update(double dt)
        {
            Position = new Vec2(Position.X, Position.Y - EntityData.Speed * dt);

            if (Position.Y <= -Size.Y)
                Remove();
        }

        public bool Pause { get; set; }
        public bool Hidden { get; set; }
    }
}
