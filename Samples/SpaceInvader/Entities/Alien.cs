using SpaceInvader.Entities.Data;
using System;
using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.SDL.Engine.Contents;

namespace SpaceInvader.Entities
{
    public class Alien : Entity2d, IHasGame<SpaceInvaderGame>, IHasDrawable<DrawableTexture>, IIsUpdatable, IHasData<AlienData>, IHasScene
    {
        public SpaceInvaderGame Game { get; set; }
        public DrawableTexture Drawable { get; set; }
        public bool Pause { get; set; }
        public bool Hidden { get; set; }
        
        public Vec2 Size { get; set; } = new Vec2(58, 57);

        public AlienData EntityData { get; set; }
        public Scene Scene { get; set; }

        public override void Initialize()
        {
            EntityData = new AlienData()
            {
                SinTime = 0,
                Amplitude = 250,
                Frequency = 3,
                Phase = 0,

                Speed = 100,
                Health = 5
            };

            Drawable.Texture = Scene.ContentLoader.LoadContent<SDLTexture>("content/alien.png");

            Position = new Vec2(Game.Window.Size.X/2, -50);
        }

        public void Update(double dt)
        {
            EntityData.SinTime += dt;
            Position = new Vec2(Math.Sin((EntityData.Frequency * EntityData.SinTime) + EntityData.Phase) * EntityData.Amplitude + Game.Window.Size.X / 2 - Size.X/2, Position.Y + EntityData.Speed * dt);

            if (Position.Y >= Game.Window.Size.Y)
                Remove();
        }
    }
}
