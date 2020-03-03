using SpaceInvader.Data.Entities;
using SpaceInvader.Logics.Aliens;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Entities
{
    public class AlienGround : Entity2d, IHasGame<SpaceInvaderGame>, IHasDrawable<DrawableTexture>, IHasData<AlienData>, IIsUpdatable, IHasScene
    {
        public SpaceInvaderGame Game { get; set; }
        public DrawableTexture Drawable { get; set; }
        public bool Pause { get; set; }
        public bool Hidden { get; set; }

        public AlienData EntityData { get; set; }
        public Scene Scene { get; set; }
        public AlienSwayLogic Logic { get; set; }
        public bool PauseLogic { get; set; }

        public override Vec2 Size { get => Drawable.Texture.Size; set { } }

        public override void Initialize()
        {
            Drawable.Texture = Scene.ContentLoader.LoadContent<Texture>("content/alien_ground.png");

            EntityData.Speed = 250;
            EntityData.Health = 2;

            Position = new Vec2(Game.Random.Next((int)Scene.Window.Size.X) - Size.X, -Size.Y);
        }

        public override void Cleanup()
        {
            Drawable.Cleanup();
        }

        public void Update(double dt)
        {
            Position.Y += EntityData.Speed * dt;


            if (Position.Y >= Scene.Window.Size.Y)
            {
                Remove();
            }
        }
    }
}
