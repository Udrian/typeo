
using Typedeaf.TypeO.Engine.Core;

namespace SampleGameCore
{
    public class SampleService : Service
    {
        public SampleScene SampleScene { get; set; }

        public SampleService(Game game) : base(game)
        {
        }

        public override void Init()
        {
        }

        public override void Update(float dt)
        {
            SampleScene.TextureRot += SampleScene.TextureRotSpeed * dt;
        }
    }
}
