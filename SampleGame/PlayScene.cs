using System;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL.Content;
using Typedeaf.TypeOSDL;
using Typedeaf.TypeOCore.Entities;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Entities.Drawables;
using SampleGame.Entities;

namespace SampleGame
{
    public class PlayScene : SDLScene, IHasGame<SpaceInvaderGame>
    {
        public SpaceInvaderGame Game { get; set; }
        public Font LoadedFont { get; set; }
        public List<Entity> Entities { get; set; } = new List<Entity>();
        public List<Bullet> Bullets { get; set; } = new List<Bullet>();
        public Player Player { get; set; }

        public int Score { get; set; } = 0;
        public float AlienSpawnTimer { get; set; } = 0;
        public float AlienSpawnTime { get; set; } = 5f;
        public float AlienSpawnFrequencyTimer { get; set; } = 0;
        public float AlienSpawnFrequencyTime { get; set; } = 0.25f;
        public int   AlienSpawnAmount { get; set; } = 4;
        public int   AlienSpawns { get; set; } = 0;
        public bool  AlienSpawning { get; set; } = false;
        public float AlienSpawnPhase { get; set; } = 0;

        public override void Initialize()
        {
            CreateContentLoader("");
            var sdlContentloader = ContentLoader as SDLContentLoader;

            LoadedFont = sdlContentloader.LoadFont("content/Awesome.ttf", 26);

            EntityAdd(new Space());

            Player = new Player();
            EntityAdd(Player);
        }

        public override void Update(float dt)
        {
            if (!AlienSpawning)
            {
                AlienSpawnTimer += dt;
                if (AlienSpawnTimer >= AlienSpawnTime)
                {
                    AlienSpawnTimer -= AlienSpawnTime;
                    AlienSpawning = true;
                    AlienSpawnPhase = (float)(Game.Random.NextDouble() * Math.PI * 3);
                }
            } 
            else
            {
                AlienSpawnFrequencyTimer += dt;
                if(AlienSpawnFrequencyTimer >= AlienSpawnFrequencyTime)
                {
                    AlienSpawnFrequencyTimer -= AlienSpawnFrequencyTime;
                    (EntityAdd(new Alien()) as Alien).Phase = AlienSpawnPhase;

                    AlienSpawns++;
                    if(AlienSpawns >= AlienSpawnAmount)
                    {
                        AlienSpawning = false;
                        AlienSpawnFrequencyTimer = 0;
                        AlienSpawns = 0;
                    }
                }
            }

            if (Game.KeyboardInputService.IsDown("Quit"))
            {
                Game.Exit();
            }
            if (Game.KeyboardInputService.IsPressed("Shoot"))
            {
                Bullets.Add((Bullet)EntityAdd(new Bullet(new Vec2(Player.Position.X, Player.Position.Y + 19))));
                Bullets.Add((Bullet)EntityAdd(new Bullet(new Vec2(Player.Position.X + 36, Player.Position.Y + 19))));
            }

            EntityUpdate(dt);
        }

        public override void Draw()
        {
            Canvas.Clear(Color.Black);

            EntityDraw();

            Canvas.DrawText(LoadedFont, $"Score: {Score}", new Vec2(15, 15), color: Color.Green);

            Canvas.Present();
        }

        public Entity2d EntityAdd(Entity2d entity)
        {
            if (entity is IHasGame)
            {
                (entity as IHasGame).SetGame(Game);
            }

            entity.Initialize();
            Entities.Add(entity);

            return entity;
        }

        private void EntityUpdate(float dt)
        {
            foreach (var entity in Entities)
            {
                if (entity is IIsUpdatable)
                {
                    (entity as IIsUpdatable).Update(dt);
                }

                if(entity is Alien)
                {
                    var alien = entity as Alien;
                    foreach(var bullet in Bullets)
                    {
                        if(alien.Position.X <= bullet.Position.X && (alien.Position.X + alien.Size.X) >= bullet.Position.X &&
                           alien.Position.Y <= bullet.Position.Y && (alien.Position.Y + alien.Size.Y) >= bullet.Position.Y)
                        {
                            bullet.Remove();
                            alien.Health--;
                            if (alien.Health <= 0)
                            {
                                alien.Remove();
                                Score++;
                            }
                        }
                    }

                    if (alien.Position.X <= Player.Position.X + Player.Size.X && (alien.Position.X + alien.Size.X) >= Player.Position.X &&
                        alien.Position.Y <= Player.Position.Y + Player.Size.Y && (alien.Position.Y + alien.Size.Y) >= Player.Position.Y)
                    {
                        alien.Remove();
                        Score--;
                    }
                }
            }

            for(int i = Entities.Count - 1; i >= 0; i--)
            {
                var bullet = Entities[i] as Bullet;
                var alien = Entities[i] as Alien;
                if (bullet?.WillBeDeleted == true)
                {
                    Entities.RemoveAt(i);
                    for(int j = Bullets.Count -1; j >= 0; j--)
                    {
                        if(Bullets[j] == bullet)
                        {
                            Bullets.RemoveAt(j);
                            break;
                        }
                    }
                }
                if (alien?.WillBeDeleted == true) Entities.RemoveAt(i);
            }
        }

        private void EntityDraw()
        {
            foreach (var entity in Entities)
            {
                if (entity is IHasDrawable)
                {
                    ((IHasDrawable)entity).DrawDrawable(Canvas);
                }

                if (entity is IIsDrawable)
                {
                    ((IIsDrawable)entity).Draw(Canvas);
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
