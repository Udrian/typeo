using TypeOEngine.Typedeaf.Core.Engine.Graphics;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Interfaces
    {
        public interface IDrawable
        {
            public bool Hidden { get; set; }
            public void Draw(Canvas canvas);
        }
    }
}
