using SpaceInvader.Entities;
using SpaceInvader.Logics.Scenes;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;

namespace SpaceInvader.Scenes
{
    public class PlanetScene : Scene, IHasGame<SpaceInvaderGame>, IHasLogic<PlanetSceneLogic>
    {
        public IKeyboardInputService KeyboardInputService { get; set; }

        public Font LoadedFont { get; set; }
        public SpaceInvaderGame Game { get; set; }
        public PlayerGround Player { get; set; }
        public PlanetSceneLogic Logic { get; set; }
        public bool PauseLogic { get; set; }

        public override void Initialize()
        {
            LoadedFont = ContentLoader.LoadContent<Font>("content/Awesome.ttf");
            LoadedFont.FontSize = 26;

            Player = Entities.Create<PlayerGround>();
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
            Canvas.Clear(Color.DarkGreen);

            Entities.Draw(Canvas);

            Canvas.DrawText(LoadedFont, $"Score: {Game.Score}", new Vec2(15, 15), color: Color.Green);

            Canvas.Present();
        }

        public override void OnEnter(Scene from)
        {
            Player.EntityData.Health = 3;
        }

        public override void OnExit(Scene to)
        {
            Logic.EntityData.AlienSpawns = 100;
            foreach (var bullet in Entities.List<Bullet>())
            {
                bullet.Remove();
            }
        }
    }
}
