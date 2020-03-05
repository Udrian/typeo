﻿using SpaceInvader.Data;
using SpaceInvader.Entities;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;

namespace SpaceInvader.Logics
{
    public class PlayerMoveLogic : Logic, IHasEntity<Entity2d>, IHasData<IMovementData>, IHasScene
    {
        public IKeyboardInputService KeyboardInputService { get; set; }

        public Entity2d Entity { get; set; }
        public IMovementData EntityData { get; set; }
        public Scene Scene { get; set; }

        public double ShootTimer { get; set; } = 0;
        public double ShootTime { get; set; } = 0.25;

        public override void Initialize()
        {
        }

        public override void Update(double dt)
        {
            if (KeyboardInputService.IsDown("Left") && Entity.Position.X > 0)
            {
                Entity.Position.X -= EntityData.Speed * dt;
            }
            else if (KeyboardInputService.IsDown("Right") && Entity.Position.X + Entity.Size.X < Scene.Window.Size.X)
            {
                Entity.Position.X += EntityData.Speed * dt;
            }
            if (KeyboardInputService.IsDown("Up") && Entity.Position.Y > 0)
            {
                Entity.Position.Y -= EntityData.Speed * dt;
            }
            else if (KeyboardInputService.IsDown("Down") && Entity.Position.Y + Entity.Size.Y < Scene.Window.Size.Y)
            {
                Entity.Position.Y += EntityData.Speed * dt;
            }

            if (Entity.Position.X < 0) Entity.Position.X = 0;
            if (Entity.Position.X > Scene.Window.Size.X - Entity.Size.X) Entity.Position.X = Scene.Window.Size.X - Entity.Size.X;
            if (Entity.Position.Y < 0) Entity.Position.Y = 0;
            if (Entity.Position.Y > Scene.Window.Size.Y - Entity.Size.Y) Entity.Position.Y = Scene.Window.Size.Y - Entity.Size.Y;

            ShootTimer += dt;

            if (KeyboardInputService.IsDown("Shoot"))
            {
                if (ShootTimer >= ShootTime)
                {
                    ShootTimer = 0;
                    if (Entity is PlayerGround)
                    {
                        Scene.Entities.Create<Bullet>(new Vec2(Entity.Position.X + Entity.Size.X / 2 - 2, Entity.Position.Y)).EntityData.Speed += EntityData.Speed;
                    }
                    else
                    {
                        Scene.Entities.Create<Bullet>(new Vec2(Entity.Position.X + 25, Entity.Position.Y + Entity.Size.Y - 55)).EntityData.Speed += EntityData.Speed;
                        Scene.Entities.Create<Bullet>(new Vec2(Entity.Position.X + Entity.Size.X - 35, Entity.Position.Y + Entity.Size.Y - 55)).EntityData.Speed += EntityData.Speed;
                    }
                }
            }
        }
    }
}