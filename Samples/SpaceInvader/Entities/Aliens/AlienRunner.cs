using SpaceInvader.Logics.Aliens;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Entities.Aliens
{
    class AlienRunner : Stub<Alien>, IHasScene, IHasGame<SpaceInvaderGame>
    {
        public Scene Scene { get; set; }
        public SpaceInvaderGame Game { get; set; }

        public override void Initialize() { }

        protected override void InitializeEntity(Alien entity)
        {
            entity.CreateLogic<AlienStraightMoveLogic>();

            entity.EntityData.Speed = 250;
            entity.EntityData.Health = 2;

            entity.Drawable.Texture = Scene.ContentLoader.LoadContent<Texture>("content/alien_ground.png");

            entity.Position = new Vec2(Game.Random.Next((int)Scene.Window.Size.X) - entity.Size.X, -entity.Size.Y);
        }
    }
}
