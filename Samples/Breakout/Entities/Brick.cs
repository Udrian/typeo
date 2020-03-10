using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace Breakout.Entities
{
    class Brick : Entity2d, IIsDrawable, IHasScene, IHasGame<BreakoutGame>
    {
        public bool Hidden { get; set; }
        public Color Color { get; set; }
        public Scene Scene { get; set; }
        public BreakoutGame Game { get; set; }

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

            var randSpawn = Game.Random.NextDouble();

            if(randSpawn <= 0.25)
            {
                var randPower = Game.Random.NextDouble();
                if(randPower <= 0.3)
                {
                    Scene.Entities.Create<PowerupSpeedUp>(position: Position + new Vec2(Size.X / 3, 0));
                }
                else if(randPower <= 0.6)
                {
                    Scene.Entities.Create<PowerupSpeedDown>(position: Position + new Vec2(Size.X / 3, 0));
                }
                else
                {
                    Scene.Entities.Create<PowerupBalls>(position: Position + new Vec2(Size.X / 3, 0));
                }
            }
        }
    }
}
