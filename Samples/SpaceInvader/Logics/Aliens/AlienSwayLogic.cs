using SpaceInvader.Data;
using System;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Logics.Aliens
{
    public class AlienSwayLogic : Logic, IHasGame<SpaceInvaderGame>, IHasData<IMovementData>, IHasEntity<Entity2d>, IHasScene
    {
        public IMovementData EntityData { get; set; }
        public SpaceInvaderGame Game { get; set; }
        public Entity2d Entity { get; set; }
        public Scene Scene { get; set; }

        public double SinTime { get; set; }
        public double Amplitude { get; set; }
        public double Frequency { get; set; }
        public double Phase { get; set; }

        public override void Initialize()
        {
        }

        public override void Update(double dt)
        {
            SinTime += dt;
            Entity.Position = new Vec2(Math.Sin((Frequency * SinTime) + Phase) * Amplitude + Scene.Window.Size.X / 2 - Entity.Size.X / 2, Entity.Position.Y + EntityData.Speed * dt);

            if (Entity.Position.Y >= Scene.Window.Size.Y)
                Entity.Remove();
        }
    }
}
