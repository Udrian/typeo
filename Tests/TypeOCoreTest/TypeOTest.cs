using System;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;
using Xunit;

namespace TypeOCoreTest
{
    public class TypeOTest
    {
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
        }

        public class TestGameWithService : Game
        {
            public ITestService TestService { get; set; }

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

        public interface ITestService : IService
        {

        }
        public class TestService : Service, ITestService
        {
            public override void Initialize()
            {
            }
        }

        public class TestServiceWithHardware : Service, ITestService
        {
            public ITestHardware TestHardware { get; set; }

            public override void Initialize()
            {
            }
        }

        public interface ITestHardware : IHardware
        {

        }
        public class TestHardware : Hardware, ITestHardware
        {
            public override void Initialize()
            {
            }
        }

        public abstract class BaseContent : Content
        {

        }

        public class SubContent : BaseContent
        {
            public override void Load(string path, ContentLoader contentLoader)
            {
            }
        }

        public class TestModule : Module
        {
            public ITestService TestService { get; set; }
            public ITestHardware TestHardware { get; set; }

            public TestModule(TypeO typeO) : base(typeO)
            {
            }

            public override void Initialize()
            {
            }

            public override void Cleanup()
            {
            }
        }

        [Fact]
        public void CreateGame()
        {
            var typeO = TypeO.Create<TestGame>() as TypeO;
            Assert.NotNull(typeO);
            Assert.NotNull(typeO.Context);
            Assert.NotNull(typeO.Context.Game);
            Assert.IsType<TestGame>(typeO.Context.Game);
        }

        [Fact]
        public void AddService()
        {
            var typeO = TypeO.Create<TestGameWithService>()
                .AddService<ITestService, TestService>() as TypeO;
            typeO.Start();

            var game = typeO.Context.Game as TestGameWithService;
            Assert.NotNull(game.TestService);
            Assert.IsType<TestService>(game.TestService);
            Assert.NotEmpty(typeO.Context.Services);

            typeO = TypeO.Create<TestGameWithService>()
                .AddService<ITestService, TestServiceWithHardware>() as TypeO;
            Assert.Throws<InvalidOperationException>(() => typeO.Start());
        }

        [Fact]
        public void AddHardware()
        {
            var typeO = TypeO.Create<TestGameWithService>()
                .AddHardware<ITestHardware, TestHardware>()
                .AddService<ITestService, TestServiceWithHardware>() as TypeO;
            typeO.Start();

            var game = typeO.Context.Game as TestGameWithService;
            Assert.NotNull((game.TestService as TestServiceWithHardware)?.TestHardware);
            Assert.IsType<TestHardware>((game.TestService as TestServiceWithHardware)?.TestHardware);
            Assert.NotEmpty(typeO.Context.Hardwares);
        }

        [Fact]
        public void AddModule()
        {
            var typeO = TypeO.Create<TestGameWithService>() as TypeO;
            var module = typeO.LoadModule<TestModule>();
            Assert.NotNull(module);
            Assert.IsType<TestModule>(module);
            Assert.NotEmpty(typeO.Context.Modules);
            Assert.Null(module.TestService);
            Assert.Null(module.TestHardware);

            module
                .AddHardware<ITestHardware, TestHardware>()
                .AddService<ITestService, TestServiceWithHardware>();
            
            typeO.Start();

            Assert.NotNull(module.TestService);
            Assert.NotNull(module.TestHardware);

            typeO = TypeO.Create<TestGameWithService>() as TypeO;
            typeO.ReferenceModule<TestModule>();
            Assert.Throws<InvalidOperationException>(() => typeO.Start());
        }

        [Fact]
        public void BindContent()
        {
            var typeO = TypeO.Create<TestGame>()
                .BindContent<BaseContent, SubContent>() as TypeO;
            typeO.Start();

            Assert.NotEmpty(typeO.Context.ContentBinding);
            Assert.NotNull(typeO.Context.ContentBinding[typeof(BaseContent)]);
        }
    }
}
