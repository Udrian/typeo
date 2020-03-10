using Breakout.Entities;
using System;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;

namespace Breakout
{
    class GameScene : Scene, IHasGame<BreakoutGame>
    {
        public IKeyboardInputService KeyboardInputService { get; set; }

        public Font LoadedFont { get; set; }
        public BreakoutGame Game { get; set; }

        public override void Initialize()
        {
            Entities.Create<Pad>();
            var ball = Entities.Create<Ball>();
                ball.Position = (Window.Size - ball.Size) / 2;
                ball.Direction = new Vec2(0, 1);

            for (int x = 1; x < 16; x++)
            {
                for(int y = 1; y < 14; y++)
                {
                    CreateBrick(new Vec2(x, y), new Color(Game.Random.NextDouble(), Game.Random.NextDouble(), Game.Random.NextDouble()));
                }
            }
        }

        public override void Update(double dt)
        {
            Entities.Update(dt);
            if (KeyboardInputService.IsDown("Quit") || Entities.List<Ball>().Count == 0)
            {
                Game.Exit();
            }
        }

        public void CreateBrick(Vec2 pos, Color color)
        {
            var brick = Entities.Create<Brick>();
                brick.Position = new Vec2(brick.Size * pos);
                brick.Color = color;
        }

        public override void Draw()
        {
            Entities.Draw(Canvas);
        }

        public override void OnEnter(Scene from)
        {
        }

        public override void OnExit(Scene to)
        {
        }
    }
}
