using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeD.Models.Data;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Desktop.Engine.Graphics;
using TypeOEngine.Typedeaf.Desktop.Engine.Services;
using TypeOEngine.Typedeaf.SDL.Engine.Graphics;

namespace TypeDSDL.Viewer
{
    class SDLFakeGameViewer
    {
        readonly TypeO FakeTypeO;

        private class FakeGame : Game
        {
            private WindowService WindowService { get; set; }
            public Window Window { get; set; }
            public Canvas Canvas { get; set; }

            public override void Initialize()
            {
                Window = WindowService.CreateWindow("Test", new Vec2(0, 0), new Vec2(640, 480), false, true);
                Canvas = WindowService.CreateCanvas(Window);
            }

            public override void Cleanup()
            {
            }

            public override void Draw()
            {
                Canvas.Clear(Color.Black);
                foreach (var drawable in Drawables.Get<Drawable>())
                {
                    drawable.Draw(Canvas);
                }
                Canvas.Present();
            }

            public override void Update(double dt)
            {
            }
        }

        private FakeGame Game { get; set; }

        public IntPtr WindowHandler { get { return ((SDLWindow)Game.Window).SDL_Window; } }
        public IntPtr CanvasHandler { get { return ((SDLCanvas)Game.Canvas).SDLRenderer; } }

        public SDLFakeGameViewer(Project project, TypeOType drawable, List<TypeOEngine.Typedeaf.Core.Engine.Module> modules = null)
        {
            if (drawable.TypeOBaseType != "Drawable2d") return;
            var typeInfo = project.Assembly.GetType(drawable.FullName);
            if (typeInfo == null) return;

            FakeTypeO = TypeO.Create<FakeGame>("Drawable Viewer") as TypeO;
            if (modules != null)
            {
                foreach (var module in modules)
                {
                    FakeTypeO.LoadModule(module);
                }
            }
            var task = new Task(() =>
            {
                FakeTypeO.Start();
            });
            task.Start();

            while (!(FakeTypeO?.Context?.Game?.Initialized == true))
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
