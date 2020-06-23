using SpaceInvader.Logics.Aliens;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Entities.Aliens
{
    class AlienGrunt : Stub<Alien>, IHasScene
    {
        public Scene Scene { get; set; }

        public override void Initialize() { }

        protected override void InitializeEntity(Alien entity)
        {
            var alienSwayLogic = entity.Logics.CreateLogic<AlienSwayLogic>();

            alienSwayLogic.SinTime = 0;
            alienSwayLogic.Amplitude = (Scene.Window.Size.X / 2) * 0.8;
            alienSwayLogic.Frequency = 1;
            alienSwayLogic.Phase = 0;

            entity.EntityData.Speed = 100;
            entity.EntityData.Health = 5;

            entity.Drawable.Texture = Scene.ContentLoader.LoadContent<Texture>("content/alien.png");

            entity.Position = new Vec2(Scene.Window.Size.X / 2, -entity.Size.Y);
        }
    }
}
