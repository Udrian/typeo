using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Entities;
using Typedeaf.TypeOCore.Entities.Drawables;
using Typedeaf.TypeOCore.Graphics;

namespace SampleGame.Entities
{
    public class Bullet : Entity2d, IIsDrawable, IIsUpdatable
    {
        private Player Player { get; }

        public Bullet(Player player)
        {
            Player = player;
        }

        public override void Initialize()
        {
            Position = Player.Position;
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, new Vec2(10, 10), true, Color.CapeHoney);
        }

        public void Update(float dt)
        {
            Position = new Vec2(Position.X, Position.Y - 500 * dt);
        }
    }
}
