using System;
using TextAdventure.Entities;
using TypeOEngine.Typedeaf.Core;

namespace TextAdventure.Scenes
{
    class PlayScene : Scene
    {
        public override void Initialize()
        {
        }

        public override void Update(double dt)
        {
            Entities.Update(dt);
            UpdateLoop.Update(dt);

            Entities.Create<Ogre>();
        }

        public override void Draw()
        {
            DrawStack.Draw(null);

            var input = Console.ReadLine();
            Console.WriteLine($"You wrote: {input}");
        }

        public override void OnEnter(Scene from)
        {
        }

        public override void OnExit(Scene to)
        {
        }
    }
}
