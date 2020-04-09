using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;
using Xunit;

namespace TypeODesktopTest
{
    public class KeyboardTest
    {
        public string GameName { get; set; } = "test";

        public class TestGame : Game
        {
            public IKeyboardInputService KeyboardInputService { get; set; }

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

        public class TestKeyboardHardware : Hardware, IKeyboardHardware
        {
            public override void Initialize()
            {
            }

            public bool CurrentKeyDownEvent(object key)
            {
                return false;
            }

            public bool CurrentKeyUpEvent(object key)
            {
                return false;
            }

            public bool OldKeyDownEvent(object key)
            {
                return false;
            }

            public bool OldKeyUpEvent(object key)
            {
                return false;
            }

            public override void Cleanup()
            {
            }
        }

        public class TestKeyboardInputService : Service, IKeyboardInputService
        {
            public IKeyboardHardware KeyboardHardware { get; set; }

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
                .AddHardware<IKeyboardHardware, TestKeyboardHardware>()
                .AddService<IKeyboardInputService, TestKeyboardInputService>() as TypeO;
            typeO.Start();

            var testGame = typeO.Context.Game as TestGame;
            Assert.NotNull(testGame.KeyboardInputService);
            Assert.IsType<TestKeyboardInputService>(testGame.KeyboardInputService);

            Assert.NotNull((testGame.KeyboardInputService as TestKeyboardInputService)?.KeyboardHardware);
            Assert.IsType<TestKeyboardHardware>((testGame.KeyboardInputService as TestKeyboardInputService)?.KeyboardHardware);
        }
    }
}
