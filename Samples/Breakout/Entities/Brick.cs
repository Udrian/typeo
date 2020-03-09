using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace Breakout.Entities
{
    class Brick : Entity2d, IIsDrawable, IHasScene
    {
        public bool Hidden { get; set; }
        public Color Color { get; set; }
        public Scene Scene { get; set; }

        public override void Initialize()
        {
            Size = new Vec2(75, 25);
        }

        public override void Cleanup()
        {
        }

        public void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, Size, true, Color);
        }

        public void Hit()
        {
            Remove();
            Scene.Entities.Create<Powerup>(position: Position + new Vec2(Size.X / 3, 0));
        }
    }
}
