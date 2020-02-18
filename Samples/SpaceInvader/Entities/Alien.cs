using SpaceInvader.Entities.Data;
using SpaceInvader.Logics;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Entities
{
    public class Alien : Entity2d, IHasDrawable<DrawableTexture>, IHasData<AlienData>, IHasLogic<DownwardSwayLogic>, IHasScene
    {
        public DrawableTexture Drawable { get; set; }
        public bool Pause { get; set; }
        public bool Hidden { get; set; }
        
        public AlienData EntityData { get; set; }
        public Scene Scene { get; set; }
        public DownwardSwayLogic Logic { get; set; }
        public bool PauseLogic { get; set; }

        public override void Initialize()
        {
            Size = new Vec2(58, 57);

            EntityData.SinTime = 0;
            EntityData.Amplitude = (Scene.Window.Size.X / 2) * 0.8;
            EntityData.Frequency = 1;
            EntityData.Phase = 0;

            EntityData.Speed = 100;
            EntityData.Health = 5;

            Drawable.Texture = Scene.ContentLoader.LoadContent<Texture>("content/alien.png");

            Position = new Vec2(Scene.Window.Size.X/2, -50);
        }
    }
}
