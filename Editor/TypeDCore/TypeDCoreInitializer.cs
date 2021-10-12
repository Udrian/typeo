using System.Collections.Generic;
using System.Threading.Tasks;
using TypeD;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.Types;
using TypeD.View;
using TypeDCore.Code.Drawable2d;
using TypeDCore.Code.Entity;
using TypeDCore.Code.Game;
using TypeDCore.Code.Scene;
using TypeDCore.Models;
using TypeDCore.Models.Interfaces;

namespace TypeDCore
{
    public class TypeDCoreInitializer : TypeDModuleInitializer
    {
        // Models
        private IProjectModel ProjectModel { get; set; }
        private ISaveModel SaveModel { get; set; }

        // Internal Models
        private ITypeDCoreProjectModel TypeDCoreProjectModel { get; set; }

        public override void Initializer()
        {
            // Models
            ProjectModel = Resources.Get<IProjectModel>("ProjectModel");
            SaveModel = Resources.Get<ISaveModel>("SaveModel");

            // Internal Models
            TypeDCoreProjectModel = new TypeDCoreProjectModel(ProjectModel);

            // Hooks
            Hooks.AddHook("ProjectCreate", ProjectCreate);
            Hooks.AddHook("InitUI", InitUI);

            // Init
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

        void InitUI(object param)
        {
            var hookParam = param as InitUIHook;

            hookParam.Menu.Items.Add(
                new MenuItem() {
                    Name = "_Project",
                    Items = new List<MenuItem>()
                    {
                        new MenuItem()
                        {
                            Name = "_Create",
                            Items = new List<MenuItem>()
                            {
                                new MenuItem() {
                                    Name = "_Entity",
                                    ClickBinding = "LoadedProject",
                                    Click = (param) => {
                                        var project = param as Project;

                                        //TypeDCoreProjectModel.CreateEntity()
                                        SaveModel.AddSave("Kalle Anka", () => { return Task.Delay(10); });
                                    }
                                }
                            }
                        }
                    }
                }
            );
        }
    }
}
