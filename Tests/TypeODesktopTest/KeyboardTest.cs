using System;
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
        }

        public class TestKeyboardHardware : Hardware, IKeyboardHardware
        {
            public override void Initialize()
            {
            }

            public bool CurrentKeyDownEvent(object key)
            {
                throw new NotImplementedException();
            }

            public bool CurrentKeyUpEvent(object key)
            {
                throw new NotImplementedException();
            }

            public bool OldKeyDownEvent(object key)
            {
                throw new NotImplementedException();
            }

            public bool OldKeyUpEvent(object key)
            {
                throw new NotImplementedException();
            }
        }

        public class TestKeyboardInputService : Service, IKeyboardInputService
        {
            public IKeyboardHardware KeyboardHardware { get; set; }

            public override void Initialize()
            {
            }

            public bool IsDown(object input)
            {
                throw new NotImplementedException();
            }

            public bool IsPressed(object input)
            {
                throw new NotImplementedException();
            }

            public bool IsReleased(object input)
            {
                throw new NotImplementedException();
            }

            public void SetKeyAlias(object input, object key)
            {
                throw new NotImplementedException();
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

            Assert.NotNull(testGame.KeyboardInputService.KeyboardHardware);
            Assert.IsType<TestKeyboardHardware>(testGame.KeyboardInputService.KeyboardHardware);
        }
    }
}
