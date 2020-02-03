using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using SpaceInvader.Entities.Data;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;

namespace SpaceInvader.Entities
{
    public class Player : Entity2d, IHasGame<SpaceInvaderGame>, IHasDrawable<DrawableTexture>, IIsUpdatable, IHasData<PlayerData>, IHasScene
    {
        public IKeyboardInputService KeyboardInputService { get; set; }

        public SpaceInvaderGame Game { get; set; }
        public DrawableTexture Drawable { get; set; }

        public PlayerData EntityData { get; set; }

        public bool Pause { get; set; }
        public bool Hidden { get; set; }
        
        public Vec2 Size { get; set; } = new Vec2(46, 29);
        public Scene Scene { get; set; }

        public override void Initialize()
        {
            EntityData = new PlayerData()
            {
                Speed = 5
            };

            Drawable.Texture = Scene.ContentLoader.LoadContent<Texture>("content/ship.png");
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
