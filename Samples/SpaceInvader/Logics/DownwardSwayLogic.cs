﻿using SpaceInvader.Entities.Data;
using System;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Logics
{
    public class DownwardSwayLogic : Logic, IHasGame<SpaceInvaderGame>, IHasData<AlienData>, IHasEntity<Entity2d>, IHasScene
    {
        public AlienData EntityData { get; set; }
        public SpaceInvaderGame Game { get; set; }
        public Entity2d Entity { get; set; }
        public Scene Scene { get; set; }

        public override void Initialize()
        {
        }

        public override void Update(double dt)
        {
            EntityData.SinTime += dt;
            Entity.Position = new Vec2(Math.Sin((EntityData.Frequency * EntityData.SinTime) + EntityData.Phase) * EntityData.Amplitude + Scene.Window.Size.X / 2 - Entity.Size.X / 2, Entity.Position.Y + EntityData.Speed * dt);

            if (Entity.Position.Y >= Scene.Window.Size.Y)
                Entity.Remove();
        }
    }
}
