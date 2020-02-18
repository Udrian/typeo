using TypeOEngine.Typedeaf.Core.Common;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Contents
    {
        public abstract class Font : Content
        {
            protected Font() { }

            public virtual int FontSize { get; set; }
            public abstract Vec2 MeasureString(string text);
        }
    }
}