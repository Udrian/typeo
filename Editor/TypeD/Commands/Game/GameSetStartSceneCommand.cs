using TypeD.Types;

namespace TypeD.Commands.Game
{
    public partial class GameCommand
    {
        public void SetStartScene(Models.Data.Project project, TypeOType scene)
        {
            if (scene.TypeOBaseType != "Scene") return;
            project.StartScene = scene.FullName;
        }
    }
}
