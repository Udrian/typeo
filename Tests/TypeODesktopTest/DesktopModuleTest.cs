using System.Linq;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Desktop;
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

        [Fact]
        public void LoadDesktopModule()
        {
            var typeO = TypeO.Create<TestGame>(GameName) 
                             .LoadModule<DesktopModule>() as TypeO;
            var module = typeO.Context.Modules.FirstOrDefault(m => m.GetType() == typeof(DesktopModule)) as DesktopModule;
            Assert.NotNull(module);
            Assert.IsType<DesktopModule>(module);
            Assert.NotEmpty(typeO.Context.Modules);
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
