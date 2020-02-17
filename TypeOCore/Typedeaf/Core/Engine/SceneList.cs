using System;
using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core.Engine
{
    public class SceneList : IHasContext, IHasGame
    {
        Context IHasContext.Context { get; set; }
        protected Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }
        public ILogger Logger { get; set; }
        public Game Game { get; set; }

        private Dictionary<Type, Scene> Scenes { get; set; }
        public Scene CurrentScene { get; private set; }
        public Window Window { get; set; }
        public Canvas Canvas { get; set; }
        public ContentLoader ContentLoader { get; set; }

        public SceneList()
        {
            Scenes = new Dictionary<Type, Scene>();
        }

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

                scene.Window = Window;
                scene.Canvas = Canvas;
                scene.ContentLoader = ContentLoader;

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

        public virtual void Update(double dt)
        {
            CurrentScene?.Update(dt);
        }
        public virtual void Draw()
        {
            CurrentScene?.Draw();
        }
    }
}
