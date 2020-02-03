using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;

namespace TypeOEngine.Typedeaf.Desktop
{
    namespace Engine.Graphics
    {
        public abstract class DesktopWindow : Window
        {
            public virtual Vec2 Position { get; set; }
            public virtual bool Fullscreen { get; set; }
            public virtual bool Borderless { get; set; }

            protected DesktopWindow() : base()
            {
            }

            public override void Initialize()
            {
                Initialize("", Vec2.Zero, Vec2.Zero);
            }

            public abstract void Initialize(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false);
        }
    }
}
