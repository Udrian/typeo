using System;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Content;
using Typedeaf.TypeO.Engine.Core;
using Typedeaf.TypeO.Engine.Graphics;

namespace SampleGameCore
{
    public class SampleScene : Scene<SampleGame>
    {
        public Vec2    TexturePos      { get; set; } = new Vec2(175, 175);
        public Vec2    TextureDir      { get; set; } = new Vec2(1  , 1  );
        public float   TextureSpeed    { get; set; } = 50f;
        public double  TextureRot      { get; set; } = 0;
        public double  TextureRotSpeed { get; set; } = Math.PI/4;
        public Texture LoadedTexture   { get; set; }
        public Font    LoadedFont      { get; set; }

        public SampleScene(SampleGame game, Canvas canvas) : base(game, canvas) { }

        public override void Initialize()
        {
            LoadedTexture = Canvas.LoadTexture("content/image.png");
            LoadedFont = Canvas.LoadFont("lazy.ttf", 26);

            Game.AddService<SampleService>(this);
        }

        public override void Update(float dt)
        {
            if (Game.Input.Key.IsReleased("Quit"))
            {
                Game.Exit();
            }

            TexturePos += TextureDir * TextureSpeed * dt;

            if (TexturePos.X + LoadedTexture.Size.X   >= Game.ScreenSize.X) TextureDir = new Vec2(-1, TextureDir.Y);
            if (TexturePos.X - LoadedTexture.Size.X   <= 0                ) TextureDir = new Vec2( 1, TextureDir.Y);
            if (TexturePos.Y + LoadedTexture.Size.Y/2 >= Game.ScreenSize.Y) TextureDir = new Vec2(TextureDir.X, -1);
            if (TexturePos.Y - LoadedTexture.Size.Y/2 <= 0                ) TextureDir = new Vec2(TextureDir.X,  1);
        }

        public override void Draw()
        {
            Canvas.DrawImage(LoadedTexture, new Vec2(0), origin: new Vec2(25, 25), rotate: Math.PI / 4);

            Canvas.DrawImage(LoadedTexture, new Vec2(125, 25));

            Canvas.DrawImage(LoadedTexture, TexturePos,
                scale: new Vec2(2, 1),
                rotate: TextureRot,
                origin: new Vec2(LoadedTexture.Size.X, LoadedTexture.Size.Y) / 2,
                color: Color.Green,
                flipped: Texture.Flipped.Both,
                source: new Rectangle(5, 5, 25, 25));

            Canvas.DrawText(LoadedFont, "Test", new Vec2(100, 100), color: Color.Green);
        }

        public override void OnEnter(Scene<SampleGame> from)
        {
        }

        public override void OnExit(Scene<SampleGame> to)
        {
        }
    }
}
