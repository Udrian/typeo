using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine.Contents;

namespace SpaceInvader.Entities
{
    public class PlayerGround : Player
    {
        public override void LoadContent()
        {
            Drawable.Texture = Scene.ContentLoader.LoadContent<Texture>("content/player_ground.png");
        }
    }
}
