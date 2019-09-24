using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
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
