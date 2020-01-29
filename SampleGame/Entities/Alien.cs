using System;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Contents;
using Typedeaf.TypeOCore.Entities;
using Typedeaf.TypeOCore.Entities.Drawables;
using Typedeaf.TypeOCore.Entities.Drawables.Interfaces;
using Typedeaf.TypeOCore.Interfaces;
using Typedeaf.TypeOSDL.Contents;

namespace SampleGame.Entities
{
    public class Alien : Entity2d, IHasGame<SpaceInvaderGame>, IHasDrawable<DrawableTexture>, IIsUpdatable
    {
        public SpaceInvaderGame Game { get; set; }
        public DrawableTexture Drawable { get; set; }

        public double Speed { get; set; } = 100;
        public Vec2 Size { get; set; } = new Vec2(58, 57);
        public double SinTime { get; set; } = 0;
        public double Amplitude { get; set; } = 250;
        public double Frequency { get; set; } = 3;
        public double Phase { get; set; } = 0;

        public int Health { get; set; } = 5;

        public override void Initialize()
        {
            Drawable = new DrawableTexture(this, (Texture)Game.Window.CurrentScene.ContentLoader.LoadContent<SDLTexture>("content/alien.png"));

            Position = new Vec2(Game.Window.Size.X/2, -50);
        }

        public void Update(double dt)
        {
            SinTime += dt;
            Position = new Vec2((double)Math.Sin((Frequency * SinTime) + Phase) * Amplitude + Game.Window.Size.X / 2 - Size.X/2, Position.Y + Speed * dt);

            if (Position.Y >= Game.Window.Size.Y)
                Remove();
        }

        public bool WillBeDeleted { get; private set; }
        public void Remove()
        {
            WillBeDeleted = true;
        }
    }
}
