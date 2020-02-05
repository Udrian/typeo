using SpaceInvader.Entities.Data;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;

namespace SpaceInvader.Logics
{
    public class PlayerMoveLogic : Logic, IHasEntity<Entity2d>, IHasData<IMovementData>
    {
        public Entity2d Entity { get; set; }
        public IKeyboardInputService KeyboardInputService { get; set; }
        public IMovementData EntityData { get; set; }

        public override void Initialize()
        {
        }

        public override void Update(double dt)
        {
            if (KeyboardInputService.IsDown("Left"))
            {
                Entity.Position.X -= EntityData.Speed;
            }
            if (KeyboardInputService.IsDown("Right"))
            {
                Entity.Position.X += EntityData.Speed;
            }
        }
    }
}
