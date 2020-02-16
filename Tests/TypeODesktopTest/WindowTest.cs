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

            public override void DrawImage(Texture texture, Vec2 pos, Entity2d entity = null)
            {
            }

            public override void DrawImage(Texture texture, Vec2 pos, Vec2 scale = null, double rotation = 0, Vec2 origin = null, Color color = null, Flipped flipped = Flipped.None, Rectangle source = null, Entity2d entity = null)
            {
            }

            public override void DrawLine(Vec2 from, Vec2 size, Color color, Entity2d entity = null)
            {
            }

            public override void DrawLineE(Vec2 from, Vec2 to, Color color, Entity2d entity = null)
            {
            }

            public override void DrawLines(List<Vec2> points, Color color, Entity2d entity = null)
            {
            }

            public override void DrawPixel(Vec2 point, Color color, Entity2d entity = null)
            {
            }

            public override void DrawPixels(List<Vec2> points, Color color, Entity2d entity = null)
            {
            }

            public override void DrawRectangle(Rectangle rectangle, bool filled, Color color, Entity2d entity = null)
            {
            }

            public override void DrawRectangle(Vec2 from, Vec2 size, bool filled, Color color, Entity2d entity = null)
            {
            }

            public override void DrawRectangleE(Vec2 from, Vec2 to, bool filled, Color color, Entity2d entity = null)
            {
            }

            public override void DrawText(Font font, string text, Vec2 pos, Entity2d entity = null)
            {
            }

            public override void DrawText(Font font, string text, Vec2 pos, Vec2 scale = null, double rotate = 0, Vec2 origin = null, Color color = null, Flipped flipped = Flipped.None, Rectangle source = null, Entity2d entity = null)
            {
            }

            public override void Present()
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
            public override void Initialize(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false)
            {
            }

            public override Canvas CreateCanvas()
            {
                return new TestCanvas();
            }

            public override Canvas CreateCanvas(Rectangle viewport)
            {
                return new TestCanvas();
            }

            public override ContentLoader CreateContentLoader(Canvas canvas)
            {
                return new TestContentLoader(canvas, null);
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

            Assert.NotNull(testGame.WindowService.WindowHardware);
            Assert.IsType<TestWindowHardware>(testGame.WindowService.WindowHardware);

            var window = testGame.WindowService.CreateWindow();
            Assert.NotNull(window);
            Assert.IsType<TestWindow>(window);

            var canvas = window.CreateCanvas();
            Assert.NotNull(canvas);
            Assert.IsType<TestCanvas>(canvas);

            var contentLoader = window.CreateContentLoader(canvas);
            Assert.NotNull(contentLoader);
            Assert.IsType<TestContentLoader>(contentLoader);
        }
    }
}
