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
    public class Alien : Entity2d, IHasGame<SpaceInvaderGame>, IHasDrawable<DrawableTexture>, IHasData<AlienData>, IHasScene
    {
        public SpaceInvaderGame Game { get; set; }
        public DrawableTexture Drawable { get; set; }
        public bool Pause { get; set; }
        public bool Hidden { get; set; }
        
        public AlienData EntityData { get; set; }
        public Scene Scene { get; set; }

        public override void Initialize()
        {
            Size = new Vec2(58, 57);


            EntityData.SinTime = 0;
            EntityData.Amplitude = 250;
            EntityData.Frequency = 3;
            EntityData.Phase = 0;

            EntityData.Speed = 100;
            EntityData.Health = 5;

            Drawable.Texture = Scene.ContentLoader.LoadContent<Texture>("content/alien.png");

            Position = new Vec2(Game.Window.Size.X/2, -50);

            CreateLogic<DownwardSwayLogic>();
        }
    }
}
