using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Entities;
using Typedeaf.TypeOCore.Entities.Drawables;
using Typedeaf.TypeOCore.Graphics;

namespace SampleGame.Entities
{
    public class Bullet : Entity2d, IIsDrawable, IIsUpdatable
    {
        public float Speed { get; set; } = 500;

        public Bullet(Vec2 position) : base(position) { }

        public override void Initialize() {}

        public void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, new Vec2(10, 10), true, Color.CapeHoney);
        }

        public void Update(float dt)
        {
            Position = new Vec2(Position.X, Position.Y - Speed * dt);
        }
    }
}
