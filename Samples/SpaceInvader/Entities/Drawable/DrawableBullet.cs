using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace SpaceInvader.Entities.Drawable
{
    public class DrawableBullet : Drawable2d
    {
        public override Vec2 Size { get; protected set; }

        public override void Initialize()
        {
            Size = new Vec2(10, 10);
        }

        public override void Draw(Entity2d entity, Canvas canvas)
        {
            canvas.DrawRectangle(entity.Position, Size, true, Color.CapeHoney);
        }
    }
}
