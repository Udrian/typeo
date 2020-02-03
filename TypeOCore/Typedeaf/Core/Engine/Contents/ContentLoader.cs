using System;
using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Contents
    {
        public class ContentLoader
        {
            public string BasePath { get; set; }
            public Canvas Canvas { get; private set; }
            protected Dictionary<Type, Type> ContentBinding { get; private set; }

            protected ContentLoader(Canvas canvas, Dictionary<Type, Type> contentBinding)
            {
                Canvas = canvas;
                ContentBinding = contentBinding;
            }

            public C LoadContent<C>(string path) where C : Content
            {
                Content content;
                if (ContentBinding.ContainsKey(typeof(C)))
                {
                    content = Activator.CreateInstance(ContentBinding[typeof(C)]) as Content;
                }
                else
                {
                    if (typeof(C).IsAbstract)
                        throw new Exception($"Base content type '{typeof(C).Name}' is missing a sub class Content Binding");
                    content = Activator.CreateInstance(typeof(C)) as Content;
                }

                content.Load(path, this);
                return content as C;
            }
        }
    }
}