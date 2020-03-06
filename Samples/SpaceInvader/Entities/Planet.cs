using SpaceInvader.Data.Entities;
using SpaceInvader.Scenes;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Entities
{
    public class Planet : Entity2d, IHasDrawable<DrawableTexture>, IHasScene<SpaceScene>, IHasData<PlanetData>, IIsUpdatable, IHasGame<SpaceInvaderGame>
    {
        public DrawableTexture Drawable { get; set; }
        public bool Hidden { get; set; }
        public PlanetData EntityData { get; set; }
        public bool Pause { get; set; }
        public SpaceInvaderGame Game { get; set; }
        public SpaceScene Scene { get; set; }

        public override void Initialize()
        {
            Drawable.Texture = Scene.ContentLoader.LoadContent<Texture>("content/planet.png");
            Size = Drawable.Texture.Size;

            Position = new Vec2(Game.Random.Next((int)(Scene.Window.Size.X - Size.X)), -Size.Y);
            EntityData.Speed = 50;
        }

        public override void Cleanup()
        {
            Drawable.Cleanup();
        }

        public void Update(double dt)
        {
            Position.Y += EntityData.Speed * dt;

            if (Position.X <= Scene.Player.Position.X + Scene.Player.Size.X && (Position.X + Size.X) >= Scene.Player.Position.X &&
                Position.Y <= Scene.Player.Position.Y + Scene.Player.Size.Y && (Position.Y + Size.Y) >= Scene.Player.Position.Y)
            {
                Remove();
                Scene.SpaceSceneLogic.EntityData.PlanetSpawned = false;
                Scene.Scenes.SetScene<PlanetScene>();
            }

            if (Position.Y >= Scene.Window.Size.Y)
            {
                Remove();
                Scene.SpaceSceneLogic.EntityData.PlanetSpawned = false;
            }
        }
    }
}
