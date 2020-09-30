using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables
    {
        public class DrawableAnimation<D> : Drawable2d where D : Drawable2d
        {
            public class Animation : Drawable2d
            {
                protected List<D> Drawables { get; set; }
                public override Vec2 Size { get => CurrentDrawable.Size; protected set { } }

                protected D CurrentDrawable;

                public Animation() { }

                public void AddFrame(D drawable)
                {
                    Drawables.Add(drawable);
                    if (CurrentDrawable == null)
                        CurrentDrawable = drawable;
                }

                public override void Initialize()
                {
                    Drawables = new List<D>();
                }

                public override void Cleanup()
                {
                    foreach(var drawable in Drawables)
                    {
                        drawable.Cleanup();
                    }
                }

                public override void Draw(Canvas canvas)
                {
                    CurrentDrawable.Draw(canvas);
                }
            }

            protected Dictionary<string, Animation> Animations { get; set; }
            protected Animation CurrentAnimation { get; set; }
            public override Vec2 Size { get => CurrentAnimation.Size; protected set { } }

            public DrawableAnimation() { }

            public Animation AddAnimation(string name)
            {
                var animation = new Animation();
                animation.Initialize();
                Animations.Add(name, animation);
                if (CurrentAnimation == null)
                    CurrentAnimation = animation;
                return animation;
            }

            public void SetAnimation(string name)
            {
                CurrentAnimation = Animations[name];
            }

            public override void Initialize()
            {
                Animations = new Dictionary<string, Animation>();
            }

            public override void Cleanup()
            {
                foreach(var animation in Animations)
                {
                    animation.Value.Cleanup();
                }
            }

            public override void Draw(Canvas canvas)
            {
                CurrentAnimation.Draw(canvas);
            }
        }
    }
}
