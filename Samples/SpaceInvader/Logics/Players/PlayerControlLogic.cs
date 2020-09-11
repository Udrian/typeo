using SpaceInvader.Data;
using SpaceInvader.Entities;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services;

namespace SpaceInvader.Logics.Players
{
    class PlayerMoveLogic : Logic, IHasEntity<Player>, IHasData<IMovementData>, IHasScene
    {
        public KeyboardInputService KeyboardInputService { get; set; }

        public Player Entity { get; set; }
        public IMovementData EntityData { get; set; }
        public Scene Scene { get; set; }

        public double ShootTimer { get; set; } = 0;
        public double ShootTime { get; set; } = 0.25;

        public override void Initialize()
        {
        }

        public override void Update(double dt)
        {
            if(KeyboardInputService.IsDown("Left") && Entity.Position.X > 0)
            {
                Entity.Position = Entity.Position.TransformX(-EntityData.Speed * dt);
            }
            else if(KeyboardInputService.IsDown("Right") && Entity.Position.X + Entity.Size.X < Scene.Window.Size.X)
            {
                Entity.Position = Entity.Position.TransformX(EntityData.Speed * dt);
            }
            if(KeyboardInputService.IsDown("Up") && Entity.Position.Y > 0)
            {
                Entity.Position = Entity.Position.TransformY(-EntityData.Speed * dt);
            }
            else if(KeyboardInputService.IsDown("Down") && Entity.Position.Y + Entity.Size.Y < Scene.Window.Size.Y)
            {
                Entity.Position = Entity.Position.TransformY(EntityData.Speed * dt);
            }

            if(Entity.Position.X < 0) Entity.Position = Entity.Position.SetX(0);
            if(Entity.Position.X > Scene.Window.Size.X - Entity.Size.X) Entity.Position = Entity.Position.SetX(Scene.Window.Size.X - Entity.Size.X);
            if(Entity.Position.Y < 0) Entity.Position = Entity.Position.SetY(0);
            if(Entity.Position.Y > Scene.Window.Size.Y - Entity.Size.Y) Entity.Position = Entity.Position.SetY(Scene.Window.Size.Y - Entity.Size.Y);

            ShootTimer += dt;
            if(KeyboardInputService.IsDown("Shoot"))
            {
                if(ShootTimer >= ShootTime)
                {
                    ShootTimer = 0;

                    Entity.Shoot();
                }
            }
        }
    }
}
