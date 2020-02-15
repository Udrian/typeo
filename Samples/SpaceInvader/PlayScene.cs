using System;
using TypeOEngine.Typedeaf.Core;
using SpaceInvader.Entities;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.Core.Common;

namespace SpaceInvader
{
    public class PlayScene : Scene, IHasGame<SpaceInvaderGame>
    {
        public IKeyboardInputService KeyboardInputService { get; set; }

        public SpaceInvaderGame Game { get; set; }
        public Font LoadedFont { get; set; }
        public Player Player { get; set; }

        public double AlienSpawnTimer { get; set; } = 0;
        public double AlienSpawnTime { get; set; } = 5;
        public double AlienSpawnFrequencyTimer { get; set; } = 0;
        public double AlienSpawnFrequencyTime { get; set; } = 0.25;
        public int    AlienSpawnAmount { get; set; } = 4;
        public int    AlienSpawns { get; set; } = 0;
        public bool   AlienSpawning { get; set; } = false;
        public double AlienSpawnPhase { get; set; } = 0;

        public double PlanetSpawnTimer { get; set; } = 0;
        public double PlanetSpawnTime { get; set; } = 30;
        public bool   PlanetSpawned { get; set; }

        public override void Initialize()
        {
            LoadedFont = ContentLoader.LoadContent<Font>("content/Awesome.ttf");
            LoadedFont.FontSize = 26;

            Entities.Create<Space>();

            Player = Entities.Create<Player>();
            Entities.Create<Powerup>();
        }

        public override void Update(double dt)
        {
            if (!PlanetSpawned)
            {
                PlanetSpawnTimer += dt;
                if (PlanetSpawnTimer >= PlanetSpawnTime)
                {
                    PlanetSpawnTimer = 0;
                    PlanetSpawned = true;
                    Entities.Create<Planet>();
                }
            }

            if (!AlienSpawning)
            {
                AlienSpawnTimer += dt;
                if (AlienSpawnTimer >= AlienSpawnTime)
                {
                    AlienSpawnTimer -= AlienSpawnTime;
                    AlienSpawning = true;
                    AlienSpawnPhase = Game.Random.NextDouble() * Math.PI * 3;
                }
            } 
            else
            {
                AlienSpawnFrequencyTimer += dt;
                if(AlienSpawnFrequencyTimer >= AlienSpawnFrequencyTime)
                {
                    AlienSpawnFrequencyTimer -= AlienSpawnFrequencyTime;
                    var alien = Entities.Create<Alien>();
                    alien.EntityData.Phase = AlienSpawnPhase;

                    AlienSpawns++;
                    if(AlienSpawns >= AlienSpawnAmount)
                    {
                        AlienSpawning = false;
                        AlienSpawnFrequencyTimer = 0;
                        AlienSpawns = 0;
                    }
                }
            }

            if (KeyboardInputService.IsDown("Quit"))
            {
                Game.Exit();
            }
            if (KeyboardInputService.IsPressed("Shoot"))
            {
                Entities.Create<Bullet>(new Vec2(Player.Position.X, Player.Position.Y + 19));
                Entities.Create<Bullet>(new Vec2(Player.Position.X + 36, Player.Position.Y + 19));
            }

            EntityUpdate(dt);
        }

        public override void Draw()
        {
            Canvas.Clear(Color.Black);

            Entities.Draw(Canvas);

            Canvas.DrawText(LoadedFont, $"Score: {Game.Score}", new Vec2(15, 15), color: Color.Green);

            Canvas.Present();
        }

        private void EntityUpdate(double dt)
        {
            Entities.Update(dt);

            foreach (var alien in Entities.List<Alien>())
            {
                foreach(var bullet in Entities.List<Bullet>())
                {
                    if(alien.Position.X <= bullet.Position.X && (alien.Position.X + alien.Size.X) >= bullet.Position.X &&
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

        public override void OnEnter(Scene from)
        {
        }

        public override void OnExit(Scene to)
        {
        }
    }
}
