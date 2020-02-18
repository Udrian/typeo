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
    public class Player : Entity2d, IHasDrawable<DrawableTexture>, IHasData<PlayerData>, IHasLogic<PlayerMoveLogic>, IHasScene
    {
        public DrawableTexture Drawable { get; set; }
        public PlayerData EntityData { get; set; }

        public bool Pause { get; set; }
        public bool Hidden { get; set; }

        public Scene Scene { get; set; }
        public PlayerMoveLogic Logic { get; set; }
        public bool PauseLogic { get; set; }

        public override Vec2 Size { get => Drawable.Texture.Size; set { } }

        public override void Initialize()
        {
            LoadContent();
            EntityData.Speed = 5;

            Position = new Vec2(100, Scene.Window.Size.Y * 0.8f);
        }

        public virtual void LoadContent()
        {
            Drawable.Texture = Scene.ContentLoader.LoadContent<Texture>("content/ship.png");
        }
    }
}
