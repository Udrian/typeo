using System;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Entities;
using Typedeaf.TypeOCore.Entities.Drawables;
using Typedeaf.TypeOSDL.Content;

namespace SampleGame.Entities
{
    public class Alien : Entity2d, IHasGame<SpaceInvaderGame>, IHasDrawable<DrawableTexture>, IIsUpdatable
    {
        public SpaceInvaderGame Game { get; set; }
        public DrawableTexture Drawable { get; set; }

        public float Speed { get; set; } = 100f;
        public Vec2 Size { get; set; } = new Vec2(58, 57);
        public float SinTime { get; set; } = 0;
        public float Amplitude { get; set; } = 250f;
        public float Frequency { get; set; } = 3f;
        public float Phase { get; set; } = 0f;

        public int Health { get; set; } = 5;

        public override void Initialize()
        {
            var sdlContentloader = Game.Window.CurrentScene.ContentLoader as SDLContentLoader;
            Drawable = new DrawableTexture(this, sdlContentloader.LoadTexture("content/alien.png"));

            Position = new Vec2(Game.Window.Size.X/2, -50);
        }

        public void Update(float dt)
        {
            SinTime += dt;
            Position = new Vec2((float)Math.Sin((Frequency * SinTime) + Phase) * Amplitude + Game.Window.Size.X / 2 - Size.X/2, Position.Y + Speed * dt);

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
