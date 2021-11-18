using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TypeD.Models.Data;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeD.Viewer
{
    public class ConsoleWriter : TextWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }

        public override void Write(string value)
        {
            WriteEvent?.Invoke(this, value);
            base.Write(value);
        }

        public override void WriteLine(string value)
        {
            WriteEvent?.Invoke(this, value);
            base.WriteLine(value);
        }

        public event EventHandler<string> WriteEvent;
    }

    public class DrawableViewer
    {
        public ConsoleWriter ConsoleWriter { get; set; }
        readonly TypeO FakeTypeO;

        private class FakeGame : Game
        {
            public override void Initialize()
            {
            }

            public override void Cleanup()
            {
            }

            public override void Draw()
            {
                foreach(var drawable in Drawables.Get<Drawable>())
                {
                    drawable.Draw(null);
                }
            }

            public override void Update(double dt)
            {
            }
        }

        private FakeGame Game { get; set; }

        public DrawableViewer(Project project, TypeOType drawable)
        {
            if (drawable.TypeOBaseType != "Drawable2d") return;
            var typeInfo = project.Assembly.GetType(drawable.FullName);
            if (typeInfo == null) return;

            ConsoleWriter = new ConsoleWriter();
            Console.SetOut(ConsoleWriter);

            FakeTypeO = TypeO.Create<FakeGame>("Drawable Viewer") as TypeO;
            var task = new Task(() =>
            {
                FakeTypeO.Start();
            });
            task.Start();

            while(!(FakeTypeO?.Context?.Game?.Initialized == true))
            {
                Task.Delay(100);
            }
            Game = FakeTypeO.Context.Game as FakeGame;
            Game.Drawables.Create(typeInfo);
        }

        public void Close()
        {
            Game.Exit();
        }
    }
}
