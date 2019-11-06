using System;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    public abstract class Scene
    {
        public bool IsInitialized { get; set; } = false;
        public bool Pause         { get; set; } = false;
        public bool Hide          { get; set; } = false;

        public Scene() { }

        public abstract void Initialize();
        public abstract void Update(float dt);
        public abstract void Draw();

        public abstract void OnExit(Scene to);
        public abstract void OnEnter(Scene from);
    }

    public abstract class Scene<C> : Scene where C : Canvas
    {
        public C Canvas { get; private set; }

        public Scene(C canvas) {
            Canvas = canvas;
        }
    }

    partial class Game
    {
        private Dictionary<Type, Scene> Scenes { get; set; }
        private Scene _currentScene;
        public Scene CurrentScene { get { return _currentScene; } set { SetScene(value); } }

        public void SetScene<S>(Canvas canvas, params object[] args) where S : Scene
        {
            var constructorArgs = new List<object>() { canvas };
            constructorArgs.AddRange(args);
            SetScene((S)Activator.CreateInstance(typeof(S), constructorArgs.ToArray()));
        }

        public void SetScene(Scene scene)
        {
            if(scene is IHasGame)
            {
                (scene as IHasGame).SetGame(this);
            }
            if (!Scenes.ContainsKey(scene.GetType()))
            {
                Scenes.Add(scene.GetType(), scene);
                scene.Initialize();
            }
            _currentScene = scene;
        }
    }
}
