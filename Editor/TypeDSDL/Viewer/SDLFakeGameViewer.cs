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
            }

            public void CreateWindowAndCanvas(Vec2 size)
            {
                Window = WindowService.CreateWindow("Test", new Vec2(0, 0), size, false, true);
                Canvas = WindowService.CreateCanvas(Window);
            }

            public override void Cleanup()
            {
            }

            public override void Draw()
            {
                if (Canvas == null) return;
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

        public SDLFakeGameViewer(Project project, List<Tuple<TypeOEngine.Typedeaf.Core.Engine.Module, TypeOEngine.Typedeaf.Core.Engine.ModuleOption>> modules = null)
        {
            FakeTypeO = TypeO.Create<FakeGame>("Drawable Viewer") as TypeO;
            if (modules != null)
            {
                foreach (var module in modules)
                {
                    FakeTypeO.LoadModule(module.Item1, module.Item2);
                }
            }
        }

        public void Start()
        {
            Task.Run(() =>
            {
                FakeTypeO.Start();
            });

            while (!(FakeTypeO?.Context?.Game?.Initialized == true))
            {
                Task.Delay(100);
            }
            Game = FakeTypeO.Context.Game as FakeGame;
        }

        public void AddComponent(Project project, Component component)
        {
            var typeInfo = project.Assembly.GetType(component.FullName);
            if (typeInfo == null) return;
            if (component.TypeOBaseType == "Drawable2d")
            {
                Game.Drawables.Create(typeInfo);
            }
        }

        public void SetWindowSize(Vec2 size)
        {
            if(Game.Window == null)
            {
                Game.CreateWindowAndCanvas(size);
            }
            else
            {
                Game.Window.Size = size;
                Game.Canvas.Viewport = new Rectangle(Game.Canvas.Viewport.Pos, size);
            }
        }

        public void Close()
        {
            Game.Exit();
        }
    }
}
