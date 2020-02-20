using System;
using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;

namespace TypeOEngine.Typedeaf.Core.Engine
{
    public class SceneList : IHasContext
    {
        Context IHasContext.Context { get; set; }
        protected Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }
        public ILogger Logger { get; set; }

        private Dictionary<Type, Scene> Scenes { get; set; }
        public Scene CurrentScene { get; private set; }
        public Window Window { get; set; }
        public Canvas Canvas { get; set; }
        public ContentLoader ContentLoader { get; set; }

        internal SceneList()
        {
            Scenes = new Dictionary<Type, Scene>();
        }

        public void Cleanup()
        {
            Window?.Cleanup();
            Canvas?.Cleanup();
        }

        public void CreateScene<S>() where S : Scene, new()
        {
            if (!Scenes.ContainsKey(typeof(S)))
            {
                Logger.Log(LogLevel.Debug, $"Creating Scene '{typeof(S).FullName}'");
                var scene = new S();
                Context.InitializeObject(scene);
                Scenes.Add(scene.GetType(), scene);

                if (Window == null)
                    Logger.Log(LogLevel.Warning, $"Window have not been instantiated to SceneList on '{Context.Game.GetType().FullName}'");
                if (Canvas == null)
                    Logger.Log(LogLevel.Warning, $"Canvas have not been instantiated to SceneList on '{Context.Game.GetType().FullName}'");
                if (ContentLoader == null)
                    Logger.Log(LogLevel.Warning, $"ContentLoader have not been instantiated to SceneList on '{Context.Game.GetType().FullName}'");

                scene.Scenes = this;
                scene.Window = Window;
                scene.Canvas = Canvas;
                scene.ContentLoader = ContentLoader;
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
            Logger.Log(LogLevel.Debug, $"Switching to Scene '{typeof(S).FullName}'");
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
