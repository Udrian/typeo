using System;
using TypeOEngine.Typedeaf.Core;
using SpaceInvader.Entities;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.Core.Common;

namespace SpaceInvader.Scenes
{
    public class ScoreScene : Scene, IHasGame<SpaceInvaderGame>
    {
        public IKeyboardInputService KeyboardInputService { get; set; }

        public SpaceInvaderGame Game { get; set; }
        public Font LoadedFont { get; set; }

        public override void Initialize()
        {
            LoadedFont = ContentLoader.LoadContent<Font>("content/Awesome.ttf");
            LoadedFont.FontSize = 54;
        }

        public override void Update(double dt)
        {
            if (KeyboardInputService.IsDown("Quit"))
            {
                Game.Exit();
            }
        }

        public override void Draw()
        {
            Canvas.Clear(Color.Black);

            Canvas.DrawText(LoadedFont, $"Score: {Game.Score}", new Vec2(15, Window.Size.Y*0.5 - 27), color: Color.Green);

            Canvas.Present();
        }

        public override void OnEnter(Scene from)
        {
        }

        public override void OnExit(Scene to)
        {
        }
    }
}
