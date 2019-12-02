using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Entities;
using Typedeaf.TypeOCore.Entities.Drawables;

namespace SampleGameCore.Entities
{
    public class Player : Entity2d, IHasGame<SampleGame>, IHasDrawable<DrawableTexture>
    {
        public SampleGame Game { get; set; }
        public DrawableTexture Drawable { get; set; }

        public Player()
        {
            Drawable = new DrawableTexture(this, Game.ContentLoader.LoadTexture("content/image.png"));
        }
    }
}
