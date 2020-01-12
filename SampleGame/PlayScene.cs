using System;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL.Content;
using Typedeaf.TypeOSDL.Graphics;
using Typedeaf.TypeOSDL;
using Typedeaf.TypeOCore.Entities;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Entities.Drawables;
using SampleGameCore.Entities;

namespace SampleGameCore
{
    public class PlayScene : SDLScene, IHasGame<SpaceInvaderGame>
    {
        public SpaceInvaderGame Game { get; set; }

        public Vec2   TextureDir      { get; set; } = new Vec2(1, 1);
        public float  TextureSpeed    { get; set; } = 50f;
        public double TextureRotSpeed { get; set; } = Math.PI/4;
        public Font   LoadedFont      { get; set; }

        public List<Entity> Entities { get; set; } = new List<Entity>();

        public override void Initialize()
        {
            CreateContentLoader("");
            var sdlContentloader = ContentLoader as SDLContentLoader;

            LoadedFont = sdlContentloader.LoadFont("content/Awesome.ttf", 26);

            var player = new Player();

            if(player is IHasGame)
            {
                (player as IHasGame).SetGame(Game);
            }

            player.Initialize();
            player.Drawable = new DrawableTexture(player, sdlContentloader.LoadTexture("content/image.png"));
            Entities.Add(player);
        }

        public override void Update(float dt)
        {
            if (Game.KeyboardInputService.IsDown("Quit"))
            {
                Game.Exit();
            }

            foreach (var entity in Entities)
            {
                if(entity is IIsUpdatable)
                {
                    (entity as IIsUpdatable).Update(dt);
                }
            }
        }

        public override void Draw()
        {
            Canvas.Clear(Color.Black);

            foreach(var entity in Entities)
            {
                if(entity is IHasDrawable) {
                    ((IHasDrawable)entity).DrawDrawable(Canvas);
                }

                if (entity is IIsDrawable)
                    ((IIsDrawable)entity).Draw(Canvas);

            }

            Canvas.DrawText(LoadedFont, "Test", new Vec2(100, 100), color: Color.Green);

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
