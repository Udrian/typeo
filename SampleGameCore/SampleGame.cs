using System;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Content;
using Typedeaf.TypeO.Engine.Core;
using Typedeaf.TypeO.Engine.Graphics;

namespace SampleGameCore
{
    public class SampleGame : Game
    {
        public SampleGame(TypeO typeO) : base(typeO) { }

        private Vec2    ScreenSize      { get; set; } = new Vec2(640, 480);
        private Vec2    TexturePos      { get; set; } = new Vec2(175, 175);
        private Vec2    TextureDir      { get; set; } = new Vec2(1  , 1  );
        private float   TextureSpeed    { get; set; } = 50f;
        private double  TextureRot      { get; set; } = 0;
        private double  TextureRotSpeed { get; set; } = Math.PI/4;
        private Texture LoadedTexture   { get; set; }

        public override void Init()
        {
            var win = CreateWindow("Hello World", new Vec2(100, 100), ScreenSize);
            var canvas = win.CreateCanvas();
            LoadedTexture = canvas.LoadTexture("content/image.png");
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Clear(Color.Red);
            canvas.DrawLine(new Vec2(10, 10), new Vec2(150, 150), Color.Blue);


            canvas.DrawImage(LoadedTexture, new Vec2(0), origin: new Vec2(25, 25), rotate: Math.PI/4);

            canvas.DrawImage(LoadedTexture, new Vec2(125, 25));

            canvas.DrawImage(LoadedTexture, TexturePos,
                scale: new Vec2(2, 1),
                rotate: TextureRot,
                origin: new Vec2(LoadedTexture.Size.X, LoadedTexture.Size.Y) / 2,
                color: Color.Green,
                flipped: Texture.Flipped.Both,
                source: new Rectangle(5, 5, 25, 25));

            canvas.Present();
        }

        public override void Update(float dt)
        {
            TextureRot += TextureRotSpeed * dt;
            TexturePos += TextureDir * TextureSpeed * dt;

            if (TexturePos.X + LoadedTexture.Size.X   >= ScreenSize.X) TextureDir = new Vec2(-1, TextureDir.Y);
            if (TexturePos.X - LoadedTexture.Size.X   <= 0           ) TextureDir = new Vec2( 1, TextureDir.Y);
            if (TexturePos.Y + LoadedTexture.Size.Y/2 >= ScreenSize.Y) TextureDir = new Vec2(TextureDir.X, -1);
            if (TexturePos.Y - LoadedTexture.Size.Y/2 <= 0           ) TextureDir = new Vec2(TextureDir.X,  1);
        }
    }
}
