using System.Collections.Generic;
using System.Linq;
using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine
    {
        public class EntityList : IHasTypeO, IHasGame, IHasScene
        {
            TypeO IHasTypeO.TypeO { get; set; }
            private TypeO TypeO { get => (this as IHasTypeO).TypeO; set => (this as IHasTypeO).TypeO = value; }
            public Game Game { get; set; }
            public Scene Scene { get; set; }

            protected List<Entity> Entities;
            protected List<IIsUpdatable> Updatables;
            protected List<IHasDrawable> HasDrawables;
            protected List<IIsDrawable> IsDrawables;

            public EntityList()
            {
                Entities = new List<Entity>();
                Updatables = new List<IIsUpdatable>();
                HasDrawables = new List<IHasDrawable>();
                IsDrawables = new List<IIsDrawable>();
            }

            public E Create<E>(Vec2 position = null, Vec2 scale = null, double rotation = 0, Vec2 origin = null, Color color = null, Flipped flipped = Flipped.None) where E : Entity2d, new()
            {
                var entity = CreateEntity<E>() as Entity2d;

                entity.Position = position ?? Vec2.Zero;
                entity.Scale    = scale    ?? Vec2.One;
                entity.Rotation = rotation;
                entity.Origin   = origin   ?? Vec2.Zero;
                entity.Color    = color    ?? Color.White;
                entity.Flipped = flipped;

                return entity as E;
            }

            public E Create<E>() where E : Entity, new()
            {
                return CreateEntity<E>();
            }

            private E CreateEntity<E>() where E : Entity, new()
            {
                var entity = new E();

                (entity as IHasTypeO).TypeO = TypeO;
                if(entity is IHasGame)
                {
                    (entity as IHasGame).Game = Game;
                }
                if(entity is IHasScene)
                {
                    (entity as IHasScene).Scene = Scene;
                }

                (TypeO as TypeO)?.SetServices(entity);

                if (entity is IIsUpdatable)
                {
                    Updatables.Add(entity as IIsUpdatable);
                }

                if (entity is IHasDrawable)
                {
                    (entity as IHasDrawable).CreateDrawable(entity);
                    HasDrawables.Add(entity as IHasDrawable);
                }

                if (entity is IIsDrawable)
                {
                    IsDrawables.Add(entity as IIsDrawable);
                }

                entity.Initialize();
                (entity as IHasData)?.EntityData?.Initialize();
                Entities.Add(entity);

                return entity;
            }

            public void Update(double dt)
            {
                foreach(var entity in Updatables)
                {
                    if ((entity as Entity)?.WillBeDeleted == true) continue;
                    if (!entity.Pause)
                    {
                        entity.Update(dt);
                    }
                }

                //Update all entities logics
                //TODO: theese should really be put in the Updatables
                foreach (var entity in Entities)
                {
                    if (entity.WillBeDeleted == true) continue;
                    if ((entity as IIsUpdatable)?.Pause == false)
                    {
                        foreach (var logic in entity.GetLogics())
                        {
                            logic.Update(dt);
                        }
                    }
                }

                for (int i = Entities.Count - 1; i >= 0; i--)
                {
                    if (Entities[i].WillBeDeleted)
                    {
                        for(int j = 0; j < Updatables.Count; j++)
                        {
                            if(Updatables[j] == Entities[i])
                            {
                                Updatables.RemoveAt(j);
                                break;
                            }
                        }

                        for (int j = 0; j < HasDrawables.Count; j++)
                        {
                            if (HasDrawables[j] == Entities[i])
                            {
                                HasDrawables.RemoveAt(j);
                                break;
                            }
                        }

                        for (int j = 0; j < IsDrawables.Count; j++)
                        {
                            if (IsDrawables[j] == Entities[i])
                            {
                                IsDrawables.RemoveAt(j);
                                break;
                            }
                        }

                        Entities.RemoveAt(i);
                    }
                }
            }

            public void Draw(Canvas canvas)
            {
                foreach (var entity in HasDrawables)
                {
                    if (!entity.Hidden)
                    {
                        entity.DrawDrawable(entity as Entity, canvas);
                    }
                }

                foreach (var entity in IsDrawables)
                {
                    if (!entity.Hidden)
                    {
                        entity.Draw(canvas);
                    }
                }
            }

            public IEnumerable<E> List<E>() where E : Entity
            {
                //TODO: but to dictonary with type 

                return Entities.Where(e => e is E).Cast<E>();
            }
        }
    }
}