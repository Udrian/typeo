using SpaceInvader.Data.Entities;
using SpaceInvader.Entities.Drawable;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Entities
{
    class Bullet : Entity2d, IIsUpdatable, IHasData<BulletData>
    {
        public bool Pause { get; set; }
        public bool Hidden { get; set; }
        public DrawableBullet Drawable { get; set; }
        public BulletData EntityData { get; set; }

        public override Vec2 Size { get => Drawable.Size; set { } }

        public Bullet() : base() { }

        public override void Initialize()
        {
            Drawable = CreateDrawable<DrawableBullet>();
            EntityData.Speed = 500;
        }

        public void Update(double dt)
        {
            Position = new Vec2(Position.X, Position.Y - EntityData.Speed * dt);

            if(Position.Y <= -Drawable.Size.Y)
                Remove();
        }

        public override void Cleanup()
        {
        }
    }
}