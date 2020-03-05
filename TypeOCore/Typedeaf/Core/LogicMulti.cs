﻿using System.Collections.Generic;
using System.Linq;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    public class LogicMulti : Logic, IHasContext, IHasScene, IHasEntity, IHasData<EntityData>
    {
        Context IHasContext.Context { get; set; }
        private Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }

        protected List<Logic> Logics { get; set; }
        public Scene Scene { get; set; }
        public Entity Entity { get; set; }
        public EntityData EntityData { get; set; }

        public LogicMulti() { }

        public override void Initialize()
        {
            Logics = new List<Logic>();
        }

        public override void Update(double dt)
        {
            foreach(var logic in Logics)
            {
                logic.Update(dt);
            }
        }

        public L CreateLogic<L>() where L : Logic, new()
        {
            var logic = new L();

            Context.InitializeObject(logic, (Entity as object) ?? Scene);
            logic.Initialize();

            Logics.Add(logic);

            return logic;
        }

        public bool RemoveLogic<L>() where L : Logic
        {
            return Logics.Remove(Logics.FirstOrDefault(l => l.GetType() == typeof(L)));
        }

        public L GetLogic<L>() where L : Logic
        {
            return Logics.FirstOrDefault(l => l is L) as L;
        }
    }
}