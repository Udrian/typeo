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
    public class PlanetScene : Scene, IHasGame<SpaceInvaderGame>, IHasLogic<PlanetSpawnLogic>
    {
        public IKeyboardInputService KeyboardInputService { get; set; }

        public Font LoadedFont { get; set; }
        public SpaceInvaderGame Game { get; set; }
        public PlayerGround Player { get; set; }
        public PlanetSpawnLogic Logic { get; set; }
        public bool PauseLogic { get; set; }

        public override void Initialize()
        {
            LoadedFont = ContentLoader.LoadContent<Font>("content/Awesome.ttf");
            LoadedFont.FontSize = 48;

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

            for (int i = 0; i < Player.EntityData.Health; i++)
            {
                Canvas.DrawImage(Player.HealthTexture, new Vec2(Window.Size.X - (((Player.HealthTexture.Size.X * 0.5 + 15) * i) + 15 + Player.HealthTexture.Size.X), 25), scale: new Vec2(0.5));
            }

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
            foreach (var alien in Entities.List<Alien>())
            {
                alien.Remove();
            }
        }
    }
}
