using System.Collections.Generic;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Entities;
using Typedeaf.TypeOCore.Entities.Drawables;
using Typedeaf.TypeOCore.Graphics;

namespace SampleGame.Entities
{
    public class Space : Entity2d, IHasGame<SpaceInvaderGame>, IIsUpdatable, IIsDrawable
    {
        public SpaceInvaderGame Game  { get; set; }
        private List<Vec2>      Stars { get; set; }
        
        private int   NumberOfStars { get; set; } = 100;
        private float Speed         { get; set; } = 250;

        public override void Initialize() {

            Stars = new List<Vec2>();
            for (int i = 0; i < NumberOfStars; i++)
            {
                Stars.Add(new Vec2(Game.Random.Next((int)Game.Window.Size.X), Game.Random.Next(-(int)Game.Window.Size.Y, (int)Game.Window.Size.Y)));
            }
        }

        public void Draw(Canvas canvas)
        {
            foreach(var star in Stars)
            {
                var pulse = (Game.Random.NextDouble() <= 0.0001);
                if (pulse)
                {
                    canvas.DrawRectangle(star - new Vec2(2, 2), new Vec2(4, 4), true, Color.White);
                }
                else
                {
                    canvas.DrawPixel(star, Color.LightYellow);
                }
            }
        }

        public void Update(float dt)
        {
            for(int i = 0; i < Stars.Count; i++)
            {
                var star = Stars[i];
                star = new Vec2(star.X, star.Y + Speed * dt);
                if (star.Y > Game.Window.Size.Y)
                    star = new Vec2(Game.Random.Next((int)Game.Window.Size.X), Game.Random.Next(-(int)Game.Window.Size.Y, 0));
                
                Stars[i] = star;
            }
        }
    }
}
