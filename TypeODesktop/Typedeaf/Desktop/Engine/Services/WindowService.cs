﻿using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Desktop.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Contents;

namespace TypeOEngine.Typedeaf.Desktop
{
    namespace Engine.Services
    {
        public class WindowService : Service, IHasGame, IWindowService
        {
            public ILogger Logger { get; set; }
            public IWindowHardware WindowHardware { get; set; }

            public Game Game { get; set; }

            public override void Initialize() { }

            public DesktopWindow CreateWindow()
            {
                var window = WindowHardware.CreateWindow();
                Logger.Log($"Creating Window of type '{window.GetType().FullName}'");
                Context.InitializeObject(window);

                return window;
            }

            public DesktopWindow CreateWindow(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false)
            {
                var window = CreateWindow();

                window.Initialize(title, position, size, fullscreen, borderless);

                return window;
            }

            public Canvas CreateCanvas(Window window)
            {
                var canvas = WindowHardware.CreateCanvas(window);
                Logger.Log($"Creating Canvas of type '{canvas.GetType().FullName}'");
                Context.InitializeObject(canvas);
                canvas.Initialize();

                return canvas;
            }

            public Canvas CreateCanvas(Window window, Rectangle viewport)
            {
                var canvas = CreateCanvas(window);
                canvas.Viewport = viewport;
                return canvas;
            }

            public ContentLoader CreateContentLoader(Canvas canvas)
            {
                var contentLoader = WindowHardware.CreateContentLoader(canvas);
                Logger.Log($"Creating ContentLoader of type '{canvas.GetType().FullName}'");
                Context.InitializeObject(contentLoader);

                return contentLoader;
            }
        }
    }
}
