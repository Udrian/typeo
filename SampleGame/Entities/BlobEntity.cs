using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore.Entities;
using Typedeaf.TypeOCore.Entities.Drawables;
using Typedeaf.TypeOSDL.Content;

namespace SampleGameCore.Entites
{
    public class BlobEntity : Entity2d, IHasGame<SampleGame>, IHasDrawable<DrawableTexture>
    {
        public SDLTexture LoadedTexture { get; set; }
        public SampleGame Game { get; set; }
        public DrawableTexture Drawable { get; set; }

        public BlobEntity(SampleGame game, SDLTexture texture, Vec2 position, Vec2? scale = null, double rotation = 0, Vec2 origin = new Vec2(), Color color = null, Texture.Flipped flipped = Texture.Flipped.None) : base(position, scale, rotation, origin, color, flipped)
        {
            Game = game;
            Drawable = new DrawableTexture(this, texture);
        }
    }
}