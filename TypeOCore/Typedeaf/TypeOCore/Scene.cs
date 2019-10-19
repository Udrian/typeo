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

    public abstract class Scene<G, C> : Scene where G : Game where C : Canvas
    {
        protected G Game   { get; private set; }
        public    C Canvas { get; private set; }

        public Scene(G game, C canvas) {
            Game   = game;
            Canvas = canvas;
        }
    }
}
