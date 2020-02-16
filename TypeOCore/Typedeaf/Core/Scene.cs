using System;
using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    public abstract partial class Scene
    {
        public Window Window { get; set; }
        public Canvas Canvas { get; set; }
        public ContentLoader ContentLoader { get; set; }
        public EntityList Entities { get; set; }

        public bool IsInitialized { get; set; } = false;
        public bool Pause         { get; set; } = false;
        public bool Hide          { get; set; } = false;

        protected Scene() {}

        public abstract void Initialize();
        public abstract void Update(double dt);
        public abstract void Draw();

        public abstract void OnExit(Scene to);
        public abstract void OnEnter(Scene from);
    }

    namespace Engine.Graphics
    {
        partial class Window
        {
            private Dictionary<Type, Scene> Scenes { get; set; }
            public Scene CurrentScene { get; private set; }

            public void CreateScene<S>() where S : Scene, new()
            {
                if (!Scenes.ContainsKey(typeof(S)))
                {
                    Logger.Log($"Creating Scene '{typeof(S).FullName}'");
                    var scene = new S();
                    Scenes.Add(scene.GetType(), scene);
                    if(scene is IHasGame)
                    {
                        (scene as IHasGame).Game = Game;
                    }
                    Context.SetLogger(scene);

                    scene.Window = this;
                    if(scene.Window is IHasContext)
                    {
                        (scene.Window as IHasContext).Context = Context;
                    }
                    Context.SetLogger(scene.Window);

                    scene.Canvas = CreateCanvas();
                    if(scene.Canvas is IHasContext)
                    {
                        (scene.Canvas as IHasContext).Context = Context;
                    }
                    Context.SetLogger(scene.Canvas);

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
                CurrentScene = Scenes[typeof(S)];
                if(init)
                    CurrentScene.Initialize();
                return Scenes[typeof(S)] as S;
            }
        }
    }
}
