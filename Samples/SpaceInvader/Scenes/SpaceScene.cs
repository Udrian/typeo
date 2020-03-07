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
    class SpaceScene : Scene, IHasGame<SpaceInvaderGame>, IHasLogic<LogicMulti>
    {
        public IKeyboardInputService KeyboardInputService { get; set; }

        public SpaceInvaderGame Game { get; set; }
        public Font LoadedFont { get; set; }
        public Player Player { get; set; }
        public LogicMulti Logic { get; set; }
        public SpaceSpawnLogic SpaceSceneLogic { get; set; }
        public bool PauseLogic { get; set; }

        public override void Initialize()
        {
            LoadedFont = ContentLoader.LoadContent<Font>("content/Awesome.ttf");
            LoadedFont.FontSize = 48;

            Entities.Create<Space>();

            Player = Entities.Create<Player>();

            SpaceSceneLogic = Logic.CreateLogic<SpaceSpawnLogic>();
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

            for (int i = 0; i < Player.EntityData.Health; i++)
            {
                Canvas.DrawImage(Player.HealthTexture, new Vec2(Window.Size.X - (((Player.HealthTexture.Size.X * 0.5 + 15) * i) + 15 + Player.HealthTexture.Size.X), 25), scale: new Vec2(0.5));
            }

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
