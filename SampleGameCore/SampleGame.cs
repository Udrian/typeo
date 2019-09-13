using System;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Core;
using Typedeaf.TypeO.Engine.Graphics;

namespace SampleGameCore
{
    public class SampleGame : Game
    {
        public SampleGame(TypeO typeO) : base(typeO) { }

        public override void Init()
        {
            var win = CreateWindow("Hello World", new Vec2(100, 100), new Vec2(640, 480));
            win.CreateCanvas();
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Clear();
            canvas.DrawLine();
            canvas.Present();
        }

        float TimeToQuit = 5;
        public override void Update(float dt)
        {
            TimeToQuit -= dt;
            //Console.WriteLine(TimeToQuit);
            //if (TimeToQuit <= 0)
            //    Exit = true;
        }
    }
}
