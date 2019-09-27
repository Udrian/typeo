using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Content
    {
        public abstract partial class ContentLoader
        {
            protected TypeO TypeO { get; set; }
            public ContentLoader(TypeO typeO, string basePath)
            {
                TypeO = typeO;
                BasePath = basePath;
            }

            public string BasePath { get; protected set; }
        }
    }

    public abstract partial class Game
    {
        public T CreateContentLoader<T>(string basePath, params object[] args) where T : ContentLoader
        {
            var constructorArgs = new List<object>() { TypeO, basePath };
            constructorArgs.AddRange(args);
            var contentLoader = (T)Activator.CreateInstance(typeof(T), constructorArgs.ToArray());
            return contentLoader;
        }
    }
}