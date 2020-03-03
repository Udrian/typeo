﻿using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using SpaceInvader.Entities.Drawable;
using TypeOEngine.Typedeaf.Core.Common;
using System;
using SpaceInvader.Data.Entities;

namespace SpaceInvader.Entities
{
    public class Bullet : Entity2d, IHasDrawable<DrawableBullet>, IIsUpdatable, IHasData<BulletData>
    {
        public bool Pause { get; set; }
        public bool Hidden { get; set; }
        public DrawableBullet Drawable { get; set; }
        public BulletData EntityData { get; set; }

        public override Vec2 Size { get => Drawable.Size; set { } }

        public Bullet() : base() { }

        public override void Initialize()
        {
            EntityData.Speed = 500;
        }

        public void Update(double dt)
        {
            Position = new Vec2(Position.X, Position.Y - EntityData.Speed * dt);

            if (Position.Y <= -Drawable.Size.Y)
                Remove();
        }

        public override void Cleanup()
        {
        }
    }
}
