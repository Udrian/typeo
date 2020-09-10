using SpaceInvader.Data;
using SpaceInvader.Entities;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services;

namespace SpaceInvader.Logics.Players
{
    class PlayerPowerUpLogic : Logic, IHasEntity<Player>, IHasData<IMovementData>, IHasScene
    {
        public KeyboardInputService KeyboardInputService { get; set; }
        public IMovementData EntityData { get; set; }
        public Player Entity { get; set; }
        public Scene Scene { get; set; }

        public double ShootTimer { get; set; } = 0;
        public double ShootTime { get; set; } = 0.1;

        public double PowerUpTimer { get; set; } = 0;
        public double PowerUpTime { get; set; } = 10;

        public override void Initialize()
        {
        }

        public override void Update(double dt)
        {
            ShootTimer += dt;
            if(KeyboardInputService.IsDown("Shoot"))
            {
                if(ShootTimer >= ShootTime)
                {
                    ShootTimer = 0;

                    Scene.Entities.Create<Bullet>(new Vec2(Entity.Position.X + Entity.Size.X / 2 - 2, Entity.Position.Y)).EntityData.Speed += EntityData.Speed;
                }
            }

            PowerUpTimer += dt;
            if(PowerUpTimer >= PowerUpTime)
            {
                Entity.Logics.Destroy<PlayerPowerUpLogic>();
            }
        }
    }
}
