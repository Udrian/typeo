using TypeD.Data;
using TypeD.Models;

namespace TypeD.Commands.Game
{
    public partial class GameCommand
    {
        public static void SetStartScene(ProjectModel project, TypeDType scene)
        {
            if (scene.TypeType != TypeDTypeType.Scene) return;
            project.StartScene = scene.FullName;
        }
    }
}
