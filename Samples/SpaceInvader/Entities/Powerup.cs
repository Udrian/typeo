using SpaceInvader.Logics.Players;
using System.Linq;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Entities
{
    class PowerUp : Entity2d, IHasScene, IUpdatable, IHasGame<SpaceInvaderGame>
    {
        public bool Hidden { get; set; }
        public DrawableTexture Drawable { get; set; }
        public Scene Scene { get; set; }
        public SpaceInvaderGame Game { get; set; }

        public override Vec2 Size { get => Drawable.Size; set { } }
        public bool Pause { get; set; }

        public double Speed { get; set; } = 200;

        public override void Initialize()
        {
            Drawable = Drawables.Create(new DrawableTextureOption() { Texture = ContentLoader.LoadContent<Texture>("content/powerup.png") });
            Position = new Vec2(Game.Random.Next(0, (int)(Scene.Window.Size.X - Size.X)), -Size.Y);
        }

        public override void Cleanup() { }

        public void Update(double dt)
        {
            Position = Position.TransformY(Speed * dt);

            var player = Scene.Entities.List<Player>().FirstOrDefault();

            var r1x = Position.X;
            var r1y = Position.Y;
            var r1w = Size.X;
            var r1h = Size.Y;

            var r2x = player.Position.X;
            var r2y = player.Position.Y;
            var r2w = player.Size.X;
            var r2h = player.Size.Y;

            if (r1x + r1w >= r2x &&
                r1x <= r2x + r2w &&
                r1y + r1h >= r2y &&
                r1y <= r2y + r2h)
            {
                player.Logics.Create<PlayerPowerUpLogic>();
                Remove();
            }
        }
    }
}
