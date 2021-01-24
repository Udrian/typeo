using TextAdventure.Scenes;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine;

namespace TextAdventure
{
    class TextAdventureGame : Game
    {
        public SceneList Scenes { get; set; }

        public override void Initialize()
        {
            Scenes = CreateSceneHandler();
            Scenes.SetScene<PlayScene>();
        }

        public override void Draw()
        {
            Scenes.Draw();
        }

        public override void Update(double dt)
        {
            Scenes.Update(dt);
        }

        public override void Cleanup()
        {
            Scenes.Cleanup();
        }
    }
}
