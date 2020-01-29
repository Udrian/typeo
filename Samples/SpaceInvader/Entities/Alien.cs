using System;
using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Entities.Drawables.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.SDL.Engine.Contents;

namespace SpaceInvader.Entities
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
            Drawable = new DrawableTexture(this, Game.Window.CurrentScene.ContentLoader.LoadContent<SDLTexture>("content/alien.png"));

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
