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

        public Vec2   TextureDir      { get; set; } = new Vec2(1, 1);
        public float  TextureSpeed    { get; set; } = 50f;
        public double TextureRotSpeed { get; set; } = Math.PI/4;
        public Font   LoadedFont      { get; set; }

        public Player Player { get; set; }
        public List<Entity> Entities { get; set; } = new List<Entity>();

        public override void Initialize()
        {
            CreateContentLoader("");
            var sdlContentloader = ContentLoader as SDLContentLoader;

            LoadedFont = sdlContentloader.LoadFont("content/Awesome.ttf", 26);

            Player = new Player();
            Player.Drawable = new DrawableTexture(Player, sdlContentloader.LoadTexture("content/ship.png"));
            EntityAdd(Player);
        }

        public override void Update(float dt)
        {
            if (Game.KeyboardInputService.IsDown("Quit"))
            {
                Game.Exit();
            }
            if (Game.KeyboardInputService.IsPressed("Shoot"))
            {
                var bullet = new Bullet(Player);
                EntityAdd(bullet);
            }

            EntityUpdate(dt);
        }

        public override void Draw()
        {
            Canvas.Clear(Color.Black);

            EntityDraw();

            Canvas.DrawText(LoadedFont, "Test", new Vec2(100, 100), color: Color.Green);

            Canvas.Present();
        }

        public void EntityAdd(Entity2d entity)
        {
            if (entity is IHasGame)
            {
                (entity as IHasGame).SetGame(Game);
            }

            entity.Initialize();
            Entities.Add(entity);
        }

        private void EntityUpdate(float dt)
        {
            foreach (var entity in Entities)
            {
                if (entity is IIsUpdatable)
                {
                    (entity as IIsUpdatable).Update(dt);
                }
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
