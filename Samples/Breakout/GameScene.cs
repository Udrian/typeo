using Breakout.Entities;
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
            Entities.Create<Ball>();

            Entities.Create<Brick>(position: new Vec2(100, 100));
        }

        public override void Update(double dt)
        {
            Entities.Update(dt);
            if (KeyboardInputService.IsDown("Quit"))
            {
                Game.Exit();
            }
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
