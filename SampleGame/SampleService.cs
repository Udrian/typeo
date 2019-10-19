using Typedeaf.TypeOCore;

namespace SampleGameCore
{
    public class SampleService : Service
    {
        public SampleScene Scene { get; private set; }

        public SampleService(Game game, SampleScene scene) : base(game)
        {
            Scene = scene;
        }

        public override void Initialize()
        {
        }

        public override void Update(float dt)
        {
            //Scene.TextureRot += Scene.TextureRotSpeed * dt;
        }
    }
}
