using TypeD;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.Types;
using TypeDCore.Code.Drawable2d;
using TypeDCore.Code.Entity;
using TypeDCore.Code.Game;
using TypeDCore.Code.Scene;

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

            TypeOType.AddTypeOType(typeof(TypeOEngine.Typedeaf.Core.Game), typeof(GameTypeOType));
            TypeOType.AddTypeOType(typeof(TypeOEngine.Typedeaf.Core.Scene), typeof(SceneTypeOType));
            TypeOType.AddTypeOType(typeof(TypeOEngine.Typedeaf.Core.Entities.Entity), typeof(EntityTypeOType));
            TypeOType.AddTypeOType(typeof(TypeOEngine.Typedeaf.Core.Entities.Drawables.Drawable), typeof(Drawable2dTypeOType));
            //TypeOType.AddTypeOType(typeof(Stub), typeof());
            //TypeOType.AddTypeOType(typeof(Logic), typeof());
            //TypeOType.AddTypeOType(typeof(EntityData), typeof());
        }

        void ProjectCreate(object param)
        {
            var hookParam = param as ProjectCreateHook;

            ProjectModel.AddCode(hookParam.Project, new GameCode(hookParam.Project), "Game");
            ProjectModel.AddCode(hookParam.Project, new GameTypeDCode(hookParam.Project), "Game");
        }
    }
}
