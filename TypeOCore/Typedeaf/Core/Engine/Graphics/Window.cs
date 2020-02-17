using System;
using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Graphics
    {
        public abstract partial class Window : IHasContext, IHasGame
        {
            Context IHasContext.Context { get; set; }
            protected Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }
            public Game Game { get; set; }
            public ILogger Logger { get; set; }

            private Dictionary<Type, Scene> Scenes { get; set; }
            public Scene CurrentScene { get; private set; }

            public virtual string Title { get; set; }
            public virtual Vec2 Size { get; set; }

            protected Window()
            {
                Scenes = new Dictionary<Type, Scene>();
            }

            public abstract void Initialize();

            public virtual void Update(double dt)
            {
                CurrentScene?.Update(dt);
            }
            public virtual void Draw()
            {
                CurrentScene?.Draw();
            }

            public abstract Canvas CreateCanvas();
            public abstract Canvas CreateCanvas(Rectangle viewport);
            public abstract ContentLoader CreateContentLoader(Canvas canvas);

            public void CreateScene<S>() where S : Scene, new()
            {
                if (!Scenes.ContainsKey(typeof(S)))
                {
                    Logger.Log($"Creating Scene '{typeof(S).FullName}'");
                    var scene = new S();
                    Scenes.Add(scene.GetType(), scene);
                    if (scene is IHasGame)
                    {
                        (scene as IHasGame).Game = Game;
                    }
                    Context.SetLogger(scene);

                    scene.Window = this;
                    if (scene.Window is IHasContext)
                    {
                        (scene.Window as IHasContext).Context = Context;
                    }
                    Context.SetLogger(scene.Window);

                    scene.Canvas = CreateCanvas();
                    if (scene.Canvas is IHasContext)
                    {
                        (scene.Canvas as IHasContext).Context = Context;
                    }
                    Context.SetLogger(scene.Canvas);
                    scene.Canvas.Initialize();

                    scene.ContentLoader = CreateContentLoader(scene.Canvas);
                    (scene.ContentLoader as IHasContext).Context = Context;
                    Context.SetLogger(scene.ContentLoader);

                    scene.Entities = new EntityList()
                    {
                        Game = Game,
                        Scene = scene
                    };
                    (scene.Entities as IHasContext).Context = Context;
                    Context.SetLogger(scene.Entities);
                    Context.SetServices(scene);
                }
            }

            public S SetScene<S>() where S : Scene, new()
            {
                var init = false;
                if (!Scenes.ContainsKey(typeof(S)))
                {
                    CreateScene<S>();
                    init = true;
                }
                Logger.Log($"Switching to Scene '{typeof(S).FullName}'");
                var fromScene = CurrentScene;
                var toScene = Scenes[typeof(S)];
                CurrentScene?.OnExit(toScene);
                CurrentScene = toScene;
                CurrentScene?.OnEnter(fromScene);
                if (init)
                    CurrentScene.Initialize();
                return Scenes[typeof(S)] as S;
            }
        }
    }
}