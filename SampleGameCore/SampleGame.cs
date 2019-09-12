using System;
using System.Threading.Tasks;
using Typedeaf.TypeO.Engine.Core;

namespace SampleGameCore
{
    public class SampleGame : Game
    {
        public override void Init()
        {
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
