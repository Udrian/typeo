using System;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Content;

namespace Typedeaf.TypeOCore
{
    namespace Content
    {
        public abstract partial class ContentLoader : IHasTypeO
        {
            TypeO IHasTypeO.TypeO { get; set; }
            private TypeO TypeO { get { return (this as IHasTypeO).GetTypeO(); } }

            public ContentLoader(string basePath)
            {
                BasePath = basePath;
            }

            public string BasePath { get; protected set; }
        }
    }

    partial class Game
    {
        public T CreateContentLoader<T>(string basePath, params object[] args) where T : ContentLoader
        {
            var constructorArgs = new List<object>() { basePath };
            constructorArgs.AddRange(args);
            var contentLoader = (T)Activator.CreateInstance(typeof(T), constructorArgs.ToArray());
            (contentLoader as IHasTypeO).SetTypeO(TypeO);
            return contentLoader;
        }
    }
}