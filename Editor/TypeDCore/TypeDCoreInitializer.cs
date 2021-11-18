using System.Collections.Generic;
using TypeD;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeD.View;
using TypeDCore.Code.Game;
using TypeDCore.Commands;
using TypeDCore.Commands.Data;
using TypeDCore.Models;
using TypeDCore.Models.Interfaces;

namespace TypeDCore
{
    public class TypeDCoreInitializer : TypeDModuleInitializer
    {
        // Models
        IProjectModel ProjectModel { get; set; }
        ISaveModel SaveModel { get; set; }

        // Providers
        IProjectProvider ProjectProvider { get; set; }
        ITypeOTypeProvider TypeOTypeProvider { get; set; }

        // Internal Models
        ITypeDCoreProjectModel TypeDCoreProjectModel { get; set; }

        // Commands
        CreateEntityTypeCommand CreateEntityTypeCommand { get; set; }
        CreateSceneCommand CreateSceneCommand { get; set; }
        CreateDrawable2dCommand CreateDrawable2dCommand { get; set; }

        public override void Initializer()
        {
            // Models
            ProjectModel = Resources.Get<IProjectModel>();
            SaveModel = Resources.Get<ISaveModel>();

            // Providers
            ProjectProvider = Resources.Get<IProjectProvider>();
            TypeOTypeProvider = Resources.Get<ITypeOTypeProvider>();

            // Internal Models
            TypeDCoreProjectModel = new TypeDCoreProjectModel(ProjectModel, SaveModel, ProjectProvider, TypeOTypeProvider);

            // Commands
            CreateEntityTypeCommand = new CreateEntityTypeCommand(TypeDCoreProjectModel);
            CreateSceneCommand = new CreateSceneCommand(TypeDCoreProjectModel);
            CreateDrawable2dCommand = new CreateDrawable2dCommand(TypeDCoreProjectModel);

            // Hooks
            Hooks.AddHook("ProjectCreate", ProjectCreate);
            Hooks.AddHook("InitUI", InitUI);
            Hooks.AddHook("TypeContextMenuOpened", TypeContextMenuOpened);
        }

        void ProjectCreate(object param)
        {
            if (param is not ProjectCreateHook hookParam) return;

            var gameCode = new GameCode();
            ProjectModel.AddCode(hookParam.Project, gameCode);
            ProjectModel.AddCode(hookParam.Project, new GameTypeDCode());
            TypeOTypeProvider.Save(hookParam.Project, new TypeOType()
            {
                ClassName = gameCode.ClassName,
                Namespace = gameCode.Namespace,
                TemplateClass = new List<string>()
                {
                    typeof(GameCode).FullName,
                    typeof(GameTypeDCode).FullName
                },
                TypeOBaseType = "Game"
            });
        }

        void InitUI(object param)
        {
            if (param is not InitUIHook hookParam) return;

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
                                    ClickParameter = "LoadedProject",
                                    Click = (param) => {

                                        CreateEntityTypeCommand.Execute(new CreateTypeCommandData(param as Project, $"Entities"));
                                    }
                                },
                                new MenuItem() {
                                    Name = "_Scene",
                                    ClickParameter = "LoadedProject",
                                    Click = (param) => {
                                        CreateSceneCommand.Execute(new CreateTypeCommandData(param as Project, $"Scenes"));
                                    }
                                },
                                new MenuItem() {
                                    Name = "_Drawable2d",
                                    ClickParameter = "LoadedProject",
                                    Click = (param) => {
                                        CreateDrawable2dCommand.Execute(new CreateTypeCommandData(param as Project, $"Drawables"));
                                    }
                                }
                            }
                        }
                    }
                }
            );
        }

        void TypeContextMenuOpened(object param)
        {
            if (param is not TypeContextMenuOpenedHook hookParam) return;

            hookParam.Menu.Items.Add(
                new MenuItem()
                {
                    Name = "_Create",
                    Items = new List<MenuItem>()
                    {
                        new MenuItem() {
                            Name = "_Entity",
                            ClickParameter = "LoadedProject",
                            Click = (param) => {
                                CreateEntityTypeCommand.Execute(new CreateTypeCommandData(param as Project, hookParam.Node.Name ?? $"Entities"));
                            }
                        },
                        new MenuItem() {
                            Name = "_Scene",
                            ClickParameter = "LoadedProject",
                            Click = (param) => {
                                CreateSceneCommand.Execute(new CreateTypeCommandData(param as Project, hookParam.Node.Name ?? $"Scenes"));
                            }
                        },
                        new MenuItem() {
                            Name = "_Drawable2d",
                            ClickParameter = "LoadedProject",
                            Click = (param) => {
                                CreateDrawable2dCommand.Execute(new CreateTypeCommandData(param as Project, hookParam.Node.Name ?? $"Drawables"));
                            }
                        }
                    }
                }
            );
        }
    }
}
