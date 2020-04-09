using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;
using Xunit;

namespace TypeODesktopTest
{
    public class MouseTest
    {
        public string GameName { get; set; } = "test";

        public class TestGame : Game
        {
            public IMouseInputService MouseInputService { get; set; }

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

        public class TestMouseInputService : Service, IMouseInputService
        {
            public IMouseHardware MouseHardware { get; set; }
            public Vec2 MousePosition { get; }
            public Vec2 MousePositionRelative { get; }
            public Vec2 WheelPosition { get; }
            public Vec2 WheelPositionRelative { get; }

            public override void Cleanup()
            {
            }

            public override void Initialize()
            {
            }

            public bool IsDown(object input)
            {
                return false;
            }

            public bool IsPressed(object input)
            {
                return false;
            }

            public bool IsReleased(object input)
            {
                return false;
            }

            public void SetKeyAlias(object input, object key)
            {
            }
        }

        [Fact]
        public void CreateKeyboardInputService()
        {
            var typeO = TypeO.Create<TestGame>(GameName)
                .AddHardware<IMouseHardware, TestMouseHardware>()
                .AddService<IMouseInputService, TestMouseInputService>() as TypeO;
            typeO.Start();

            var testGame = typeO.Context.Game as TestGame;
            Assert.NotNull(testGame.MouseInputService);
            Assert.IsType<TestMouseInputService>(testGame.MouseInputService);

            Assert.NotNull((testGame.MouseInputService as TestMouseInputService)?.MouseHardware);
            Assert.IsType<TestMouseHardware>((testGame.MouseInputService as TestMouseInputService)?.MouseHardware);
        }
    }
}
