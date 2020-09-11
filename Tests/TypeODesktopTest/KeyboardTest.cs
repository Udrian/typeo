using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services;
using Xunit;

namespace TypeODesktopTest
{
    public class KeyboardTest
    {
        public string GameName { get; set; } = "test";

        public class TestKeyboardGame : Game
        {
            public TestKeyboardInputService KeyboardInputService { get; set; }

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

        public class TestKeyboardInputService : KeyboardInputService
        {
            public new IKeyboardHardware KeyboardHardware { get { return base.KeyboardHardware; } set { base.KeyboardHardware = value; } }
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

        [Fact]
        public void CreateKeyboardInputService()
        {
            var typeO = TypeO.Create<TestKeyboardGame>(GameName)
                .AddHardware<IKeyboardHardware, TestKeyboardHardware>()
                .AddService<TestKeyboardInputService>() as TypeO;
            typeO.Start();

            var testGame = typeO.Context.Game as TestKeyboardGame;
            Assert.NotNull(testGame.KeyboardInputService);
            Assert.IsType<TestKeyboardInputService>(testGame.KeyboardInputService);

            Assert.NotNull(testGame.KeyboardInputService.KeyboardHardware);
            Assert.IsType<TestKeyboardHardware>(testGame.KeyboardInputService.KeyboardHardware);
        }
    }
}
