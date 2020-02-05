using System.Collections.Generic;
using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using SpaceInvader.Entities.Data;

namespace SpaceInvader.Entities
{
    public class Space : Entity2d, IHasGame<SpaceInvaderGame>, IIsUpdatable, IIsDrawable, IHasData<SpaceData>
    {
        public SpaceInvaderGame Game { get; set; }
        private List<Vec2> Stars { get; set; }
        public bool Pause { get; set; }
        public bool Hidden { get; set; }

        public SpaceData EntityData { get; set; }

        public override void Initialize()
        {
            EntityData.NumberOfStars = 100;
            EntityData.Speed = 250;

            Stars = new List<Vec2>();
            for (int i = 0; i < EntityData.NumberOfStars; i++)
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

        public void Update(double dt)
        {
            for(int i = 0; i < Stars.Count; i++)
            {
                var star = Stars[i];
                star.Y += EntityData.Speed * dt;
                if (star.Y > Game.Window.Size.Y)
                    star = new Vec2(Game.Random.Next((int)Game.Window.Size.X), Game.Random.Next(-(int)Game.Window.Size.Y, 0));
                
                Stars[i] = star;
            }
        }
    }
}
