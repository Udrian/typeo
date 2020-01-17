using System;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    public abstract partial class Scene
    {
        public Window Window { get; set; }
        public Canvas Canvas { get; set; }
        public ContentLoader ContentLoader { get; set; }

        public bool IsInitialized { get; set; } = false;
        public bool Pause         { get; set; } = false;
        public bool Hide          { get; set; } = false;

        protected Scene() {}

        public abstract void Initialize();
        public abstract void Update(float dt);
        public abstract void Draw();

        public abstract void OnExit(Scene to);
        public abstract void OnEnter(Scene from);
    }

    namespace Graphics
    {
        partial class Window
        {
            private Dictionary<Type, Scene> Scenes { get; set; }
            public Scene CurrentScene { get; private set; }

            public S CreateScene<S>() where S : Scene, new()
            {
                if (!Scenes.ContainsKey(typeof(S)))
                {
                    var scene = new S();
                    Scenes.Add(scene.GetType(), scene);
                    if (scene is IHasGame)
                    {
                        (scene as IHasGame).SetGame(Game);
                    }

                    scene.Window = this;
                    scene.Canvas = CreateCanvas();

                    return scene;
                }
                return Scenes[typeof(S)] as S;
            }

            public S SetScene<S>() where S : Scene, new()
            {
                var init = false;
                if (!Scenes.ContainsKey(typeof(S)))
                {
                    var scene = CreateScene<S>();
                    init = true;
                }
                
                CurrentScene = Scenes[typeof(S)];
                if(init)
                    CurrentScene.Initialize();
                return Scenes[typeof(S)] as S;
            }
        }
    }
}
