using System;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Core;

namespace SampleGameCore
{
    public class SampleGame : Game
    {
        public SampleGame(TypeO typeO) : base(typeO) { }

        public override void Init()
        {
            CreateWindow("Hello World", new Vec2(100, 100), new Vec2(640, 480));
        }

        public async override Task Draw()
        {
            await Task.Yield();
        }

        public async override Task Update(float dt)
        {
            Console.WriteLine(dt);
            if (dt == 10000)
                Exit = true;
            await Task.Yield();
        }
    }
}
