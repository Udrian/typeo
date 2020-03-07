using SpaceInvader.Data.Entities;
using SpaceInvader.Logics.Aliens;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Entities
{
    class Alien : Entity2d, IHasDrawable<DrawableTexture>, IHasData<AlienData>, IHasLogic<LogicMulti>, IHasScene
    {
        public DrawableTexture Drawable { get; set; }
        public bool Pause { get; set; }
        public bool Hidden { get; set; }
        
        public AlienData EntityData { get; set; }
        public Scene Scene { get; set; }
        public LogicMulti Logic { get; set; }
        public bool PauseLogic { get; set; }

        public override Vec2 Size { get => Drawable.Size; set { } }

        public override void Initialize()
        {
            Logic.CreateLogic<AlienBulletDamageLogic>();
        }

        public override void Cleanup()
        {
            Drawable.Cleanup();
        }
    }
}
