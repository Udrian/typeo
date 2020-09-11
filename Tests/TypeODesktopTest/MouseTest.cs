using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services;
using Xunit;

namespace TypeODesktopTest
{
    public class MouseTest
    {
        public string GameName { get; set; } = "test";

        public class TestGame : Game
        {
            public MouseInputService MouseInputService { get; set; }

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
        public void CreateKeyboardInputService()
        {
            var typeO = TypeO.Create<TestGame>(GameName)
                .AddHardware<IMouseHardware, TestMouseHardware>()
                .AddService<MouseInputService>() as TypeO;
            typeO.Start();

            var testGame = typeO.Context.Game as TestGame;
            Assert.NotNull(testGame.MouseInputService);
            Assert.IsType<MouseInputService>(testGame.MouseInputService);

            Assert.NotNull(testGame.MouseInputService.MouseHardware);
            Assert.IsType<TestMouseHardware>(testGame.MouseInputService.MouseHardware);
        }
    }
}
