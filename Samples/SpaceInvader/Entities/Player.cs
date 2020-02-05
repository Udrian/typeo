using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using SpaceInvader.Entities.Data;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using SpaceInvader.Logics;
using TypeOEngine.Typedeaf.Core.Common;

namespace SpaceInvader.Entities
{
    public class Player : Entity2d, IHasGame<SpaceInvaderGame>, IHasDrawable<DrawableTexture>, IHasData<PlayerData>, IHasScene
    {
        public SpaceInvaderGame Game { get; set; }
        public DrawableTexture Drawable { get; set; }

        public PlayerData EntityData { get; set; }

        public bool Pause { get; set; }
        public bool Hidden { get; set; }

        public Scene Scene { get; set; }

        public override void Initialize()
        {
            CreateLogic<PlayerMoveLogic>();
            Size = new Vec2(46, 29);

            EntityData.Speed = 5;

            Drawable.Texture = Scene.ContentLoader.LoadContent<Texture>("content/ship.png");
            Position = new Vec2(100, 400);
        }
    }
}
