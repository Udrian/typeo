using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.SDL;
using TypeOEngine.Typedeaf.SDL.Engine.Graphics;
using TypeOEngine.Typedeaf.SDL.Engine.Hardwares;
using Xunit;

namespace Test
{
    public class SDLModuleTest
    {
        public string GameName { get; set; } = "test";

        public class TestGame : Game
        {
            public ILogger Logger { get; set; }
            public IWindowService WindowService { get; set; }
            public Window Window { get; set; }

            public override void Initialize()
            {
                Window = WindowService.CreateWindow("test", new Vec2(100, 100), new Vec2(100, 100));
            }

            public override void Update(double dt)
            {
                Exit();
            }

            public override void Draw()
            {
            }
        }

        public class TestScene1 : Scene
        {
            public override void Draw()
            {
            }

            public override void Initialize()
            {
            }

            public override void OnEnter(Scene from)
            {
            }

            public override void OnExit(Scene to)
            {
            }

            public override void Update(double dt)
            {
            }
        }

        [Fact]
        public void LoadSDLModule()
        {
            var typeO = TypeO.Create<TestGame>(GameName) as TypeO;
            var module = typeO.LoadModule<SDLModule>();
            Assert.NotNull(module);
            Assert.IsType<SDLModule>(module);
            Assert.NotEmpty(typeO.Context.Modules);
        }

        
        /*[Fact]
        public void LoadDefaults()
        {
            var typeO = TypeO.Create<TestGame>(GameName) as TypeO;
            typeO.LoadModule<DesktopModule>()
                .LoadModule<SDLModule>()
                .AddDefaultSDLContentBinding()
                .AddDefaultSDLHardware()
                .AddDefaultSDLServices()
                .Start();

            Assert.NotEmpty(typeO.Context.ContentBinding);
            Assert.NotEmpty(typeO.Context.Hardwares);
            Assert.NotEmpty(typeO.Context.Services);

            var testGame = (typeO.Context.Game as TestGame);

            Assert.NotNull(testGame.Logger);
            Assert.IsType<DefaultLogger>(testGame.Logger);

            Assert.NotNull(testGame.WindowService);
            Assert.NotNull(testGame.WindowService.WindowHardware);
            Assert.IsType<SDLWindowHardware>(testGame.WindowService.WindowHardware);

            Assert.NotNull(testGame.Window);
            Assert.IsType<SDLWindow>(testGame.Window);
        }*/

        /*[Fact]
        public void SwitchScene()
        {
            var typeO = TypeO.Create<TestGame>(GameName) as TypeO;
            typeO.LoadModule<DesktopModule>()
                .LoadModule<SDLModule>()
                .AddDefaultSDLContentBinding()
                .AddDefaultSDLHardware()
                .AddDefaultSDLServices()
                .Start();

            var testGame = (typeO.Context.Game as TestGame);
            testGame.Window.SetScene<TestScene1>();

            Assert.NotNull(testGame.Window.CurrentScene);
            Assert.IsType<TestScene1>(testGame.Window.CurrentScene);
        }*/
    }
}
