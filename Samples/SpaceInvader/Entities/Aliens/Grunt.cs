using SpaceInvader.Logics.Aliens;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;

namespace SpaceInvader.Entities.Aliens
{
    public class Grunt : Alien
    {
        public override void Initialize()
        {
            var alienSwayLogic = Logic.CreateLogic<AlienSwayLogic>();

            alienSwayLogic.SinTime = 0;
            alienSwayLogic.Amplitude = (Scene.Window.Size.X / 2) * 0.8;
            alienSwayLogic.Frequency = 1;
            alienSwayLogic.Phase = 0;

            EntityData.Speed = 100;
            EntityData.Health = 5;

            Drawable.Texture = Scene.ContentLoader.LoadContent<Texture>("content/alien.png");

            Position = new Vec2(Scene.Window.Size.X / 2, -50);
        }
    }
}
