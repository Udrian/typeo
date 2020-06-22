using SpaceInvader.Data.Entities;
using SpaceInvader.Logics.Players;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Entities
{
    class Player : Entity2d, IHasData<PlayerData>, IHasScene
    {
        public DrawableTexture Drawable { get; set; }
        public Texture HealthTexture { get; set; }
        public PlayerData EntityData { get; set; }

        public bool Pause { get; set; }
        public bool Hidden { get; set; }

        public Scene Scene { get; set; }
        public bool PauseLogic { get; set; }

        public override Vec2 Size { get => Drawable.Size; set { } }

        public Anchor2d LeftGunAnchor;
        public Anchor2d RightGunAnchor;

        public override void Initialize()
        {
            Drawable = Drawables.CreateDrawable<DrawableTexture>();
            LoadContent();
            HealthTexture = Drawable.Texture;
            EntityData.Speed = 500;
            EntityData.Health = 3;

            Position = new Vec2(100, Scene.Window.Size.Y * 0.8f);

            CreateLogic<PlayerMoveLogic>();
            CreateLogic<PlayerAlienDamageLogic>();

            LeftGunAnchor = CreateAnchor(new Vec2(25, 32), Orientation2d.UpperLeft);
            RightGunAnchor = CreateAnchor(new Vec2(35, 32), Orientation2d.UpperRight);
        }

        public override void Cleanup()
        {
            Drawable.Cleanup();
        }

        public virtual void LoadContent()
        {
            Drawable.Texture = Scene.ContentLoader.LoadContent<Texture>("content/ship.png");
        }

        public virtual void Shoot()
        {
            Scene.Entities.Create<Bullet>(LeftGunAnchor.ScreenBounds.Pos).EntityData.Speed += EntityData.Speed;
            Scene.Entities.Create<Bullet>(RightGunAnchor.ScreenBounds.Pos).EntityData.Speed += EntityData.Speed;
        }
    }
}
