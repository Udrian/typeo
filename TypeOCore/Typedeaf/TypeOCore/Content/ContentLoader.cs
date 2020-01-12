using Typedeaf.TypeOCore.Content;

namespace Typedeaf.TypeOCore
{
    namespace Content
    {
        public abstract partial class ContentLoader
        {
            protected ContentLoader() { }

            public string BasePath { get; set; }
        }
    }

    partial class Scene
    {
        public void CreateContentLoader<C>(string basePath) where C : ContentLoader, new()
        {
            ContentLoader = new C();
            ContentLoader.BasePath = basePath;
        }
    }
}