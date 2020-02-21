using TypeOEngine.Typedeaf.Core;
using SpaceInvader.Entities;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using SpaceInvader.Logics.Scenes;

namespace SpaceInvader.Scenes
{
    public class SpaceScene : Scene, IHasGame<SpaceInvaderGame>, IHasLogic<LogicMulti>
    {
        public IKeyboardInputService KeyboardInputService { get; set; }

        public SpaceInvaderGame Game { get; set; }
        public Font LoadedFont { get; set; }
        public Player Player { get; set; }
        public LogicMulti Logic { get; set; }
        public SpaceSceneLogic SpaceSceneLogic { get; set; }
        public bool PauseLogic { get; set; }

        public override void Initialize()
        {
            LoadedFont = ContentLoader.LoadContent<Font>("content/Awesome.ttf");
            LoadedFont.FontSize = 26;

            Entities.Create<Space>();

            Player = Entities.Create<Player>();
            Entities.Create<Powerup>();

            SpaceSceneLogic = Logic.CreateLogic<SpaceSceneLogic>();
        }

        public override void Update(double dt)
        {
            Entities.Update(dt);
            if (KeyboardInputService.IsDown("Quit"))
            {
                Game.Exit();
            }

            Logic.Update(dt);
        }

        public override void Draw()
        {
            Canvas.Clear(Color.Black);

            Entities.Draw(Canvas);

            Canvas.DrawText(LoadedFont, $"Score: {Game.Score}", new Vec2(15, 15), color: Color.Green);

            Canvas.Present();
        }

        public override void OnEnter(Scene from)
        {
        }

        public override void OnExit(Scene to)
        {
            foreach (var bullet in Entities.List<Bullet>())
            {
                bullet.Remove();
            }
        }
    }
}
