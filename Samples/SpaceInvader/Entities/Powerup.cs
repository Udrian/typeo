using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;

namespace SpaceInvader.Entities
{
    public class Powerup : Entity2d, IIsDrawable
    {
        public bool Hidden { get; set; }

        public void Draw(Canvas canvas)
        {
            canvas.DrawLines(new List<Vec2> { new Vec2(10, 10), new Vec2(5, 5), new Vec2(25, 25) }, Color.Blue);
        }

        public override void Initialize() { }
    }
}
