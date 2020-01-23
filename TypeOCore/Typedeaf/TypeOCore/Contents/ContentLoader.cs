using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Contents
    {
        public class ContentLoader
        {
            public string BasePath { get; set; }
            public Canvas Canvas { get; private set; }

            public ContentLoader(Canvas canvas)
            {
                Canvas = canvas;
            }

            public C LoadContent<C>(string path) where C : Content, new()
            {
                var content = new C();
                content.Load(path, this);
                return content;
            }
        }
    }
}