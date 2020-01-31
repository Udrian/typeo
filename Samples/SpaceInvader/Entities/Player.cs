using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Entities.Drawables.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.SDL.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;

namespace SpaceInvader.Entities
{
    public class PlayerData : EntityData
    {
        public double Speed { get; set; }

        public override void Initialize() {}
    }

    public class Player : Entity2d, IHasGame<SpaceInvaderGame>, IHasDrawable<DrawableTexture>, IIsUpdatable, IHasData<PlayerData>
    {
        public IKeyboardInputService KeyboardInputService { get; set; }

        public SpaceInvaderGame Game { get; set; }
        public DrawableTexture Drawable { get; set; }

        public PlayerData EntityData { get; set; }

        public bool Pause { get; set; }
        public bool Hidden { get; set; }
        
        public Vec2 Size { get; set; } = new Vec2(46, 29);

        public override void Initialize()
        {
            EntityData = new PlayerData()
            {
                Speed = 5
            };

            Drawable = new DrawableTexture(this, Game.Window.CurrentScene.ContentLoader.LoadContent<SDLTexture>("content/ship.png"));
            Position = new Vec2(100, 400);
        }

        public void Update(double dt)
        {
            if (KeyboardInputService.IsDown("Left"))
            {
                Position.X -= EntityData.Speed;
            }
            if (KeyboardInputService.IsDown("Right"))
            {
                Position.X += EntityData.Speed;
            }
        }
    }
}
