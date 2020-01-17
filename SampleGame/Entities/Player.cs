using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Entities;
using Typedeaf.TypeOCore.Entities.Drawables;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOSDL.Content;

namespace SampleGame.Entities
{
    public class Player : Entity2d, IHasGame<SpaceInvaderGame>, IHasDrawable<DrawableTexture>, IIsUpdatable
    {
        public SpaceInvaderGame Game { get; set; }
        public DrawableTexture Drawable { get; set; }

        public float Speed { get; set; } = 5f;
        public Vec2 Size { get; set; } = new Vec2(46, 29);

        public override void Initialize()
        {
            var sdlContentloader = Game.Window.CurrentScene.ContentLoader as SDLContentLoader;
            Drawable = new DrawableTexture(this, sdlContentloader.LoadTexture("content/ship.png"));
            Position = new Vec2(100, 400);
        }

        public void Update(float dt)
        {
            if (Game.KeyboardInputService.IsDown("Left"))
            {
                Position += new Vec2(-Speed, 0);
            }
            if (Game.KeyboardInputService.IsDown("Right"))
            {
                Position += new Vec2(Speed, 0);
            }
        }
    }
}
