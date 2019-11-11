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

    namespace Graphics
    {
        partial class Canvas
        {
            private Dictionary<Type, Scene> Scenes { get; set; }
            public Scene CurrentScene { get; private set; }

            public S SetScene<S>() where S : Scene
            {
                if (!Scenes.ContainsKey(typeof(S)))
                {
                    var constructorArgs = new List<object>() { this };
                    var scene = (S)Activator.CreateInstance(typeof(S), constructorArgs.ToArray());
                    Scenes.Add(scene.GetType(), scene);
                    if (scene is IHasGame)
                    {
                        (scene as IHasGame).SetGame(Game);
                    }

                    scene.Initialize();
                }
                
                CurrentScene = Scenes[typeof(S)];
                return Scenes[typeof(S)] as S;
            }
        }
    }
}
