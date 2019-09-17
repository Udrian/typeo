using Typedeaf.TypeO.Engine.Core;
using Typedeaf.TypeO.Engine.Graphics;

namespace Typedeaf.TypeO.Engine
{
    namespace Core
    {
        public abstract class Scene<T> where T : Game
        {
            public bool IsInitialized { get; set; } = false;
            public bool Pause         { get; set; } = false;
            public bool Hide          { get; set; } = false;

            protected T      Game   { get; private set; }
            public    Canvas Canvas { get; private set; }

            public Scene(T game, Canvas canvas) {
                Game   = game;
                Canvas = canvas;
            }

            public abstract void Initialize();
            public abstract void Update(float dt);
            public abstract void Draw();

            public abstract void OnExit(Scene<T> to);
            public abstract void OnEnter(Scene<T> from);
        }
    }

    /*namespace Graphics
    {
        public abstract partial class Canvas
        {
            private Scene currentScene;
            public Scene CurrentScene {
                get {
                    return currentScene;
                }
                set {
                    if (currentScene != null)
                    {
                        currentScene.OnExit(value);
                    }
                    var oldScene = currentScene;
                    currentScene = value;
                    currentScene.Canvas = this;
                    if (!currentScene.IsInitialized)
                    {
                        currentScene.Initialize();
                        currentScene.IsInitialized = true;
                    }
                    currentScene.OnEnter(oldScene);
                }
            }
        }
    }*/
}
