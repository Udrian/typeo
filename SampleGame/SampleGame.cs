using System;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Content;
using Typedeaf.TypeO.Engine.Core;
using Typedeaf.TypeO.Engine.Graphics;

namespace SampleGameCore
{
    public class SampleGame : Game
    {
        public Vec2 ScreenSize { get; set; } = new Vec2(640, 480);
        public SampleScene Scene { get; set; }

        public SampleGame(TypeO typeO) : base(typeO) { }

        public override void Initialize()
        {
            var win = CreateWindow("Hello World", new Vec2(100, 100), ScreenSize);
            var canvas = win.CreateCanvas();
            Scene = new SampleScene(this, canvas);
            Scene.Initialize();
        }

        public override void Draw()
        {
            Scene.Canvas.Clear(Color.Red);
            Scene.Canvas.DrawLine(new Vec2(10, 10), new Vec2(150, 150), Color.Blue);

            Scene.Draw();

            Scene.Canvas.Present();
        }

        public override void Update(float dt)
        {
            Scene.Update(dt);
        }
    }
}