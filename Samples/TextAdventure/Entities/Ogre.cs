using System;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TextAdventure.Entities
{
    public class Ogre : Entity, IUpdatable, IDrawable
    {
        public bool Pause { get; set; }
        public bool Hidden { get; set; }
        public int DrawOrder { get; set; }

        public override void Initialize()
        {
        }

        public override void Cleanup()
        {
        }

        public void Update(double dt)
        {
        }

        public void Draw(Canvas canvas)
        {
            Console.WriteLine("OGRE!");
        }
    }
}
