using System.Linq;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Desktop;
using TypeOEngine.Typedeaf.Desktop.Engine.Graphics;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services;
using Xunit;

namespace TypeODesktopTest
{
    public class DesktopModuleTest
    {
        public string GameName { get; set; } = "test";

        public class TestGame : Game
        {
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

        public class TestWindowHardware : Hardware, IWindowHardware
        {
            public override void Cleanup()
            {
            }

            public Canvas CreateCanvas(Window window)
            {
                throw new System.NotImplementedException();
            }

            public ContentLoader CreateContentLoader(Canvas canvas)
            {
                throw new System.NotImplementedException();
            }

            public DesktopWindow CreateWindow()
            {
                throw new System.NotImplementedException();
            }

            public override void Initialize()
            {
            }
        }

        public class TestKeyboardHardware : Hardware, IKeyboardHardware
        {
            public override void Cleanup()
            {
            }

            public bool CurrentKeyDownEvent(object key)
            {
                throw new System.NotImplementedException();
            }

            public bool CurrentKeyUpEvent(object key)
            {
                throw new System.NotImplementedException();
            }

            public override void Initialize()
            {
            }

            public bool OldKeyDownEvent(object key)
            {
                throw new System.NotImplementedException();
            }

            public bool OldKeyUpEvent(object key)
            {
                throw new System.NotImplementedException();
            }
        }

        public class TestMouseHardware : Hardware, IMouseHardware
        {
            public Vec2 CurrentMousePosition { get; set; }
            public Vec2 OldMousePosition { get; set; }
            public Vec2 CurrentWheelPosition { get; set; }
            public Vec2 OldWheelPosition { get; set; }

            public override void Cleanup()
            {
            }

            public bool CurrentButtonDownEvent(object key)
            {
                throw new System.NotImplementedException();
            }

            public bool CurrentButtonUpEvent(object key)
            {
                throw new System.NotImplementedException();
            }

            public override void Initialize()
            {
            }

            public bool OldButtonDownEvent(object key)
            {
                throw new System.NotImplementedException();
            }

            public bool OldButtonUpEvent(object key)
            {
                throw new System.NotImplementedException();
            }
        }

        [Fact]
        public void LoadDesktopModule()
        {
            var typeO = TypeO.Create<TestGame>(GameName)
                             .AddHardware<IWindowHardware, TestWindowHardware>()
                             .AddHardware<IKeyboardHardware, TestKeyboardHardware>()
                             .AddHardware<IMouseHardware, TestMouseHardware>()
                             .LoadModule<DesktopModule>() as TypeO;
            typeO.Start();
            var module = typeO.Context.Modules.FirstOrDefault(m => m.GetType() == typeof(DesktopModule)) as DesktopModule;
            Assert.NotNull(module);
            Assert.IsType<DesktopModule>(module);
            Assert.NotEmpty(typeO.Context.Modules);

            Assert.NotEmpty(typeO.Context.Services);

            Assert.NotNull(typeO.Context.Services[typeof(WindowService)]);
            Assert.NotNull(typeO.Context.Services[typeof(KeyboardInputService)]);
            Assert.NotNull(typeO.Context.Services[typeof(MouseInputService)]);
        }

        [Fact]
        public void TestDefaultLogger()
        {
            var typeO = TypeO.Create<TestGame>(GameName)
                             .LoadModule<DesktopModule>(new DesktopModuleOption() { LogPath = "test" }, false) as TypeO;
            var module = typeO.Context.Modules.FirstOrDefault(m => m.GetType() == typeof(DesktopModule)) as DesktopModule;
            typeO.Start();

            Assert.NotNull(typeO.Context.Logger);
            Assert.IsType<DefaultLogger>(typeO.Context.Logger);
            Assert.Equal("test", (typeO.Context.Logger as DefaultLogger).LogPath);
        }
    }
}
