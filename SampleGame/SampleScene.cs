using System;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL.Content;
using SampleGameCore.Entites;
using Typedeaf.TypeOCore.Entities;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Entities.Drawables;
using Typedeaf.TypeOSDL;

namespace SampleGameCore
{
    public class SampleScene : SDLScene, IHasGame<SampleGame>
    {
        public SampleGame Game { get; set; }

        public Vec2   TextureDir      { get; set; } = new Vec2(1, 1);
        public float  TextureSpeed    { get; set; } = 50f;
        public double TextureRotSpeed { get; set; } = Math.PI/4;
        public Font   LoadedFont      { get; set; }

        public SDLTexture   LoadedTexture { get; set; }
        public BlobEntity   MovingBlob    { get; set; }
        public List<Entity> Entities      { get; set; } = new List<Entity>();

        public override void Initialize()
        {
            Canvas = CreateCanvas();
            ContentLoader = CreateContentLoader<SDLContentLoader>("", Canvas);
            SDLContentLoader sdlContentloader = ContentLoader as SDLContentLoader;

            LoadedTexture = sdlContentloader.LoadTexture("content/image.png");
            LoadedFont    = sdlContentloader.LoadFont("content/Awesome.ttf", 26);

            Entities.Add(new BlobEntity(Game, LoadedTexture, new Vec2(100, 100)));
            Entities.Add(new BlobEntity(Game, LoadedTexture, new Vec2(0), origin: new Vec2(25, 25), rotation: Math.PI / 4));
            Entities.Add(new BlobEntity(Game, LoadedTexture, new Vec2(125, 25)));
            MovingBlob = new BlobEntity(Game, LoadedTexture, new Vec2(175, 175),
                scale: new Vec2(2, 1),
                rotation: 0,
                origin: new Vec2(LoadedTexture.Size.X, LoadedTexture.Size.Y) / 2,
                color: Color.Green,
                flipped: Texture.Flipped.Both);

            Entities.Add(MovingBlob);
            //DrawEntities.Add(MovingBlob);
                //source: new Rectangle(5, 5, 25, 25));
        }

        public override void Update(float dt)
        {
            if (Game.Input.Key.IsDown("Quit"))
            {
                Game.Exit();
            }

            MovingBlob.Position += TextureDir * TextureSpeed * dt;

            if (MovingBlob.Position.X + LoadedTexture.Size.X   >= Game.ScreenSize.X) TextureDir = new Vec2(-1, TextureDir.Y);
            if (MovingBlob.Position.X - LoadedTexture.Size.X   <= 0                ) TextureDir = new Vec2( 1, TextureDir.Y);
            if (MovingBlob.Position.Y + LoadedTexture.Size.Y/2 >= Game.ScreenSize.Y) TextureDir = new Vec2(TextureDir.X, -1);
            if (MovingBlob.Position.Y - LoadedTexture.Size.Y/2 <= 0                ) TextureDir = new Vec2(TextureDir.X,  1);
        }

        public override void Draw()
        {
            foreach(var entity in Entities)
            {
                (entity as IHasDrawable)?.DrawDrawable(Canvas);
                (entity as IIsDrawable)?.Draw(Canvas);

            }

            Canvas.DrawText(LoadedFont, "Test", new Vec2(100, 100), color: Color.Green);
        }

        public override void OnEnter(Scene from)
        {
        }

        public override void OnExit(Scene to)
        {
        }
    }
}
