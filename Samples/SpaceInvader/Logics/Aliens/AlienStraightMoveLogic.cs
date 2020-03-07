using SpaceInvader.Data;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Logics.Aliens
{
    class AlienStraightMoveLogic : Logic, IHasData<IMovementData>, IHasEntity<Entity2d>, IHasScene
    {
        public IMovementData EntityData { get; set; }
        public Entity2d Entity { get; set; }
        public Scene Scene { get; set; }

        public override void Initialize()
        {
        }

        public override void Update(double dt)
        {
            Entity.Position.Y += EntityData.Speed * dt;

            if (Entity.Position.Y >= Scene.Window.Size.Y)
            {
                Entity.Remove();
            }
        }
    }
}
