using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using Typedeaf.Common;
using TypeOEngine.Typedeaf.SDL.Contents;
using TypeOEngine.Typedeaf.Core.Services.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Drawables.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Entities
{
    public class Player : Entity2d, IHasGame<SpaceInvaderGame>, IHasDrawable<DrawableTexture>, IIsUpdatable
    {
        public IKeyboardInputService KeyboardInputService { get; set; }

        public SpaceInvaderGame Game { get; set; }
        public DrawableTexture Drawable { get; set; }

        public double Speed { get; set; } = 5;
        public Vec2 Size { get; set; } = new Vec2(46, 29);

        public override void Initialize()
        {
            Drawable = new DrawableTexture(this, Game.Window.CurrentScene.ContentLoader.LoadContent<SDLTexture>("content/ship.png"));
            Position = new Vec2(100, 400);
        }

        public void Update(double dt)
        {
            if (KeyboardInputService.IsDown("Left"))
            {
                Position.X -= Speed;
            }
            if (KeyboardInputService.IsDown("Right"))
            {
                Position.X += Speed;
            }
        }
    }
}
