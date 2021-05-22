using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TypeD.Types;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeDCore.Viewer
{
    public class ConsoleWriterEventArgs : EventArgs
    {
        public string Value { get; private set; }
        public ConsoleWriterEventArgs(string value)
        {
            Value = value;
        }
    }

    public class ConsoleWriter : TextWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }

        public override void Write(string value)
        {
            WriteEvent?.Invoke(this, new ConsoleWriterEventArgs(value));
            base.Write(value);
        }

        public override void WriteLine(string value)
        {
            WriteLineEvent?.Invoke(this, new ConsoleWriterEventArgs(value));
            base.WriteLine(value);
        }

        public event EventHandler<ConsoleWriterEventArgs> WriteEvent;
        public event EventHandler<ConsoleWriterEventArgs> WriteLineEvent;
    }

    public class DrawableViewer
    {
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

        public DrawableViewer(TypeOType drawable)
        {
            if (drawable.TypeOBaseType != "Drawable") return;
            if (drawable.TypeInfo == null) return;

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
            Game.Drawables.Create(drawable.TypeInfo.AsType());
        }

        public void Close()
        {
            Game.Exit();
        }
    }
}
