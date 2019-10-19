using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore.Entities;
using Typedeaf.TypeOCore.Entities.Drawables;
using Typedeaf.TypeOCore.Graphics;
using Typedeaf.TypeOSDL.Content;

namespace SampleGameCore.Entites
{
    public class BlobEntity : Entity2d<Game>, IHasDrawable<DrawableTexture>
    {
        public SDLTexture LoadedTexture { get; set; }
        public DrawableTexture Drawable { get; set; }

        public BlobEntity(SampleGame game, SDLTexture texture, Vec2 position, Vec2? scale = null, double rotation = 0, Vec2 origin = new Vec2(), Color color = null, Texture.Flipped flipped = Texture.Flipped.None) : base(game, position, scale, rotation, origin, color, flipped)
        {
            Drawable = new DrawableTexture(this, texture);
        }

        public void DrawDrawable(Canvas canvas)
        {
            Drawable.Draw(canvas);
        }
    }
}