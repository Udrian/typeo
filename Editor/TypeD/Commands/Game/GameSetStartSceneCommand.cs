using TypeD.Models;
using TypeD.Types;

namespace TypeD.Commands.Game
{
    public partial class GameCommand
    {
        public void SetStartScene(ProjectModel project, TypeOType scene)
        {
            if (scene.TypeOBaseType != "Scene") return;
            project.StartScene = scene.FullName;
        }
    }
}
