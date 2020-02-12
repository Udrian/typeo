using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;
using Xunit;

namespace TypeOCoreTest
{
    public class TypeOCoreTest
    {
        public class TestGame : Game
        {
            public override void Initialize()
            {
            }

            public override void Update(double dt)
            {
            }

            public override void Draw()
            {
            }
        }

        public interface ITestService : IService
        {

        }
        public class TestService : Service, ITestService
        {
            public override void Initialize()
            {
            }
        }

        [Fact]
        public void CreateGame()
        {
            TypeO typeO = TypeO.Create<TestGame>() as TypeO;
            Assert.NotNull(typeO);
            Assert.IsType<TestGame>(typeO.Game);
        }

        [Fact]
        public void AddService()
        {
            TypeO typeO = TypeO.Create<TestGame>()
                .AddService<ITestService, TestService>() as TypeO;

            
        }
    }
}
