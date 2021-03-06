﻿using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace SpaceInvader.Entities.Drawable
{
    class DrawableBullet : Drawable2d
    {
        public override Vec2 Size { get; protected set; }

        public override void Initialize()
        {
            Size = new Vec2(10, 10);
        }

        public override void Draw(Canvas canvas)
        {
            canvas.DrawRectangle(Position, Size, true, Color.CapeHoney, Entity);
        }

        public override void Cleanup()
        {
        }
    }
}
