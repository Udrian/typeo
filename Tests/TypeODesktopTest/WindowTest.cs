using System;
using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Desktop.Engine.Graphics;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;
using Xunit;

namespace TypeODesktopTest
{
    public class WindowTest
    {
        public string GameName { get; set; } = "test";

        public class TestGame : Game
        {
            public IWindowService WindowService { get; set; }

            public override void Initialize()
            {
            }

            public override void Update(double dt)
            {
                Exit();
            }

            public override void Draw()
            {
            }

            public override void Cleanup()
            {
            }
        }

        public class TestCanvas : Canvas
        {
            public override Rectangle Viewport { get; set; }

            public override void Initialize()
            {
            }

            public override void Clear(Color clearColor)
            {
            }

            public override void DrawImage(Texture texture, Vec2 pos, IAnchor2d anchor = null)
            {
            }

            public override void DrawImage(Texture texture, Vec2 pos, Vec2? scale = null, double rotation = 0, Vec2? origin = null, Color? color = null, Flipped flipped = Flipped.None, Rectangle? source = null, IAnchor2d anchor = null)
            {
            }

            public override void DrawLine(Vec2 from, Vec2 size, Color color, IAnchor2d anchor = null)
            {
            }

            public override void DrawLineE(Vec2 from, Vec2 to, Color color, IAnchor2d anchor = null)
            {
            }

            public override void DrawLines(List<Vec2> points, Color color, IAnchor2d anchor = null)
            {
            }

            public override void DrawPixel(Vec2 point, Color color, IAnchor2d anchor = null)
            {
            }

            public override void DrawPixels(List<Vec2> points, Color color, IAnchor2d anchor = null)
            {
            }

            public override void DrawRectangle(Rectangle rectangle, bool filled, Color color, IAnchor2d anchor = null)
            {
            }

            public override void DrawRectangle(Vec2 from, Vec2 size, bool filled, Color color, IAnchor2d anchor = null)
            {
            }

            public override void DrawRectangleE(Vec2 from, Vec2 to, bool filled, Color color, IAnchor2d anchor = null)
            {
            }

            public override void DrawText(Font font, string text, Vec2 pos, IAnchor2d anchor = null)
            {
            }

            public override void DrawText(Font font, string text, Vec2 pos, Vec2? scale = null, double rotate = 0, Vec2? origin = null, Color? color = null, Flipped flipped = Flipped.None, Rectangle? source = null, IAnchor2d anchor = null)
            {
            }

            public override void Present()
            {
            }

            public override void Cleanup()
            {
            }
        }

        public class TestContentLoader : ContentLoader
        {
            public TestContentLoader(Canvas canvas, Dictionary<Type, Type> contentBinding) : base(canvas, contentBinding)
            {
            }
        }

        public class TestWindow : DesktopWindow
        {
            public override void Cleanup()
            {
            }

            public override void Initialize(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false)
            {
            }

            public override void Initialize()
            {
            }
        }

        public class TestWindowHardware : Hardware, IWindowHardware
        {
            public override void Initialize()
            {
            }

            public DesktopWindow CreateWindow()
            {
                return new TestWindow();
            }

            public Canvas CreateCanvas(Window desktopWindow)
            {
                return new TestCanvas();
            }

            public ContentLoader CreateContentLoader(Canvas canvas)
            {
                return new TestContentLoader(canvas, null);
            }

            public override void Cleanup()
            {
            }
        }

        public class TestWindowService : Service, IWindowService
        {
            public IWindowHardware WindowHardware { get; set; }

            public override void Initialize()
            {
            }

            public DesktopWindow CreateWindow()
            {
                return WindowHardware.CreateWindow();
            }

            public DesktopWindow CreateWindow(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false)
            {
                return WindowHardware.CreateWindow();
            }

            public Canvas CreateCanvas(Window window)
            {
                return WindowHardware.CreateCanvas(window);
            }

            public Canvas CreateCanvas(Window window, Rectangle viewport)
            {
                return WindowHardware.CreateCanvas(window);
            }

            public ContentLoader CreateContentLoader(Canvas canvas)
            {
                return WindowHardware.CreateContentLoader(canvas);
            }

            public override void Cleanup()
            {
            }
        }

        [Fact]
        public void CreateWindowService()
        {
            var typeO = TypeO.Create<TestGame>(GameName)
                .AddHardware<IWindowHardware, TestWindowHardware>()
                .AddService<IWindowService, TestWindowService>() as TypeO;
            typeO.Start();

            var testGame = typeO.Context.Game as TestGame;
            Assert.NotNull(testGame.WindowService);
            Assert.IsType<TestWindowService>(testGame.WindowService);

            Assert.NotNull((testGame.WindowService as TestWindowService)?.WindowHardware);
            Assert.IsType<TestWindowHardware>((testGame.WindowService as TestWindowService)?.WindowHardware);

            var window = testGame.WindowService.CreateWindow();
            Assert.NotNull(window);
            Assert.IsType<TestWindow>(window);

            var canvas = testGame.WindowService.CreateCanvas(window);
            Assert.NotNull(canvas);
            Assert.IsType<TestCanvas>(canvas);

            var contentLoader = testGame.WindowService.CreateContentLoader(canvas);
            Assert.NotNull(contentLoader);
            Assert.IsType<TestContentLoader>(contentLoader);
        }
    }
}
