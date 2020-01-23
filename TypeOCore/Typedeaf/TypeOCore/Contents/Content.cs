namespace Typedeaf.TypeOCore
{
    namespace Contents
    {
        public abstract class Content
        {
            public string FilePath { get; protected set; }
            public abstract void Load(string path, ContentLoader contentLoader);
        }
    }
}
