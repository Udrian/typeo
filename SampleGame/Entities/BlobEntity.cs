using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Entities;
using Typedeaf.TypeOSDL.Content;
using Typedeaf.TypeOSDL.Entities;
using Typedeaf.TypeOSDL.Graphics;

namespace SampleGameCore.Entites
{
    public class BlobEntity : Entity2d<SampleGame>, ISDLDrawable
    {
        public SDLTexture LoadedTexture { get; set; }

        public BlobEntity(SampleGame game, SDLTexture texture, Vec2 position) : base(game, position)
        {
            LoadedTexture = texture;
        }

        public void Draw(SDLCanvas canvas)
        {
            canvas.DrawImage(LoadedTexture, Position);
        }
    }
}