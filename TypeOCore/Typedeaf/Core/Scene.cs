﻿using System.Collections.Generic;
using System.Linq;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    public abstract class Scene : IHasEntities, IHasContext //TODO: have a Scene Cleanup
    {
        Context IHasContext.Context { get; set; }
        private Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }

        public SceneList Scenes { get; set; }
        public Window Window { get; set; }
        public Canvas Canvas { get; set; }
        public ContentLoader ContentLoader { get; set; }
        public EntityList Entities { get; set; } //TODO: Look over this
        public DrawStack DrawStack { get; private set; } //TODO: Should be able to create draw stack from Game maybe?
        public UpdateLoop UpdateLoop { get; private set; } //TODO: Should be able to create Update loop from Game maybe?
        private List<Logic> Logics { get; set; } //TODO: Do we want internally saved Logic list?
        public DrawableManager<Drawable> Drawables { get; private set; }

        public bool IsInitialized { get; set; } = false;
        public bool Pause         { get; set; } = false;
        public bool Hide          { get; set; } = false;

        protected Scene()
        {
            DrawStack = new DrawStack();
            UpdateLoop = new UpdateLoop();
            Logics = new List<Logic>();
        }

        internal void InternalInitialize()
        {
            Drawables = new DrawableManager<Drawable>(DrawStack, this);
            Context.InitializeObject(Drawables, this);
        }

        public abstract void Initialize();
        public abstract void Update(double dt);
        public abstract void Draw();

        public abstract void OnExit(Scene to);
        public abstract void OnEnter(Scene from);

        public L CreateLogic<L>(bool pushToUpdateLoop = true) where L : Logic, new() //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
        {
            var logic = Context.CreateLogic<L>(this, pushToUpdateLoop ? UpdateLoop : null);
            Logics.Add(logic);
            return logic;
        }

        public int DestroyLogic<L>() where L : Logic //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
        {
            var destroyCount = 0;
            foreach(var logic in Logics)
            {
                if(logic is L)
                {
                    Context.DestroyLogic(logic, UpdateLoop);
                    destroyCount++;
                }
            }

            Logics.RemoveAll(logic => logic is L);

            return destroyCount;
        }

        public void DestroyLogic(Logic logic) //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
        {
            Context.DestroyLogic(logic, UpdateLoop);
            Logics.Remove(logic);
        }

        public IEnumerable<L> GetLogics<L>() where L : Logic //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
        {
            return Logics.FindAll(logic => logic is L).Cast<L>();
        }
    }
}
