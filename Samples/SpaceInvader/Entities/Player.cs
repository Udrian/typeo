 using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using SpaceInvader.Logics;
using TypeOEngine.Typedeaf.Core.Common;
using SpaceInvader.Data.Entities;
using SpaceInvader.Logics.Aliens;

namespace SpaceInvader.Entities
{
    public class Player : Entity2d, IHasDrawable<DrawableTexture>, IHasData<PlayerData>, IHasLogic<LogicMulti>, IHasScene
    {
        public DrawableTexture Drawable { get; set; }
        public Texture HealthTexture { get; set; }
        public PlayerData EntityData { get; set; }

        public bool Pause { get; set; }
        public bool Hidden { get; set; }

        public Scene Scene { get; set; }
        public LogicMulti Logic { get; set; }
        public bool PauseLogic { get; set; }

        public override Vec2 Size { get => Drawable.Texture.Size; set { } }

        public override void Initialize()
        {
            LoadContent();
            HealthTexture = Drawable.Texture;
            EntityData.Speed = 500;
            EntityData.Health = 3;

            Position = new Vec2(100, Scene.Window.Size.Y * 0.8f);

            Logic.CreateLogic<PlayerMoveLogic>();
            Logic.CreateLogic<PlayerAlienDamageLogic>();
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
            Scene.Entities.Create<Bullet>(new Vec2(Position.X + 25         , Position.Y + Size.Y - 55)).EntityData.Speed += EntityData.Speed;
            Scene.Entities.Create<Bullet>(new Vec2(Position.X + Size.X - 35, Position.Y + Size.Y - 55)).EntityData.Speed += EntityData.Speed;
        }
    }
}
