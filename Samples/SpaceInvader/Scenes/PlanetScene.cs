using SpaceInvader.Entities;
using System.Linq;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;

namespace SpaceInvader.Scenes
{
    public class PlanetScene : Scene, IHasGame<SpaceInvaderGame>
    {
        public IKeyboardInputService KeyboardInputService { get; set; }

        public Font LoadedFont { get; set; }
        public SpaceInvaderGame Game { get; set; }
        public PlayerGround Player { get; set; }


        public int AlienSpawns { get; set; } = 100;
        public double AlienSpawnTimer { get; set; } = 0;
        public double AlienSpawnTime { get; set; } = 0.25;

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

            if (AlienSpawns > 0)
            {
                AlienSpawnTimer += dt;
                if (AlienSpawnTimer >= AlienSpawnTime)
                {
                    AlienSpawnTimer -= AlienSpawnTime;
                    Entities.Create<AlienGround>();
                    AlienSpawns--;
                }
            }
            if(AlienSpawns == 0 && Entities.List<AlienGround>().ToList().Count() == 0)
            {
                Game.Window.SetScene<PlayScene>();
            }

            if (KeyboardInputService.IsPressed("Shoot"))
            {
                Entities.Create<Bullet>(new Vec2(Player.Position.X + Player.Size.X / 2 - 2, Player.Position.Y));
            }

            foreach (var alien in Entities.List<AlienGround>())
            {
                foreach (var bullet in Entities.List<Bullet>())
                {
                    if (alien.Position.X <= bullet.Position.X && (alien.Position.X + alien.Size.X) >= bullet.Position.X &&
                        alien.Position.Y <= bullet.Position.Y && (alien.Position.Y + alien.Size.Y) >= bullet.Position.Y)
                    {
                        bullet.Remove();
                        alien.EntityData.Health--;
                        if (alien.EntityData.Health <= 0)
                        {
                            alien.Remove();
                            Game.Score++;
                        }
                    }
                }

                if (alien.Position.X <= Player.Position.X + Player.Size.X && (alien.Position.X + alien.Size.X) >= Player.Position.X &&
                    alien.Position.Y <= Player.Position.Y + Player.Size.Y && (alien.Position.Y + alien.Size.Y) >= Player.Position.Y)
                {
                    alien.Remove();
                    Game.Score--;
                }
            }
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
        }

        public override void OnExit(Scene to)
        {
            AlienSpawns = 100;
        }
    }
}
