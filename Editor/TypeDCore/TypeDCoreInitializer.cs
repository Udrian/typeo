using TypeD;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeDCore.Code.Game;

namespace TypeDCore
{
    public class TypeDCoreInitializer : TypeDModuleInitializer
    {
        // Models
        public IProjectModel ProjectModel { get; set; }

        public override void Initializer()
        {
            ProjectModel = Resources.Get<IProjectModel>("ProjectModel");

            Hooks.AddHook("ProjectCreate", ProjectCreate);
        }

        void ProjectCreate(object param)
        {
            var hookParam = param as ProjectCreateHook;

            ProjectModel.AddCode(hookParam.Project, new GameCode(hookParam.Project), "Game");
            ProjectModel.AddCode(hookParam.Project, new GameTypeDCode(hookParam.Project), "Game");
        }
    }
}
