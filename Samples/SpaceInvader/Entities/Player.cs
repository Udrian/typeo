using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using SpaceInvader.Entities.Data;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using SpaceInvader.Logics;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;

namespace SpaceInvader.Entities
{
    public class Player : Entity2d, IHasDrawable<DrawableTexture>, IIsDrawable, IHasData<PlayerData>, IHasLogic<LogicMulti>, IHasScene
    {
        public DrawableTexture Drawable { get; set; }
        private Texture HealthTexture { get; set; }
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
            EntityData.Speed = 5;
            EntityData.Health = 3;

            Position = new Vec2(100, Scene.Window.Size.Y * 0.8f);

            Logic.CreateLogic<PlayerMoveLogic>();
        }

        public override void Cleanup()
        {
            Drawable.Cleanup();
        }

        public virtual void LoadContent()
        {
            Drawable.Texture = Scene.ContentLoader.LoadContent<Texture>("content/ship.png");
        }

        public void Draw(Canvas canvas)
        {
            for(int i = 0; i < EntityData.Health; i++)
            {
                canvas.DrawImage(HealthTexture, new Vec2(Scene.Window.Size.X - (((HealthTexture.Size.X * 0.5 + 15) * i) + 15 + HealthTexture.Size.X), 25), scale: new Vec2(0.5));
            }
        }
    }
}
