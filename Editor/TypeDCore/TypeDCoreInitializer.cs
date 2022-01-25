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

        // Providers
        IComponentProvider ComponentProvider { get; set; }

        // Internal Models
        ITypeDCoreProjectModel TypeDCoreProjectModel { get; set; }
        ITypeDCoreRestoreModel TypeDCoreRestoreModel { get; set; }

        // Commands
        CreateEntityTypeCommand CreateEntityTypeCommand { get; set; }
        CreateSceneCommand CreateSceneCommand { get; set; }
        CreateDrawable2dCommand CreateDrawable2dCommand { get; set; }

        public override void Initializer()
        {
            // Models
            ProjectModel = Resources.Get<IProjectModel>();

            // Providers
            ComponentProvider = Resources.Get<IComponentProvider>();

            // Internal Models
            TypeDCoreProjectModel = new TypeDCoreProjectModel();
            TypeDCoreRestoreModel = new TypeDCoreRestoreModel();

            Resources.Add("TypeDCoreProjectModel", TypeDCoreProjectModel);
            Resources.Add("TypeDCoreRestoreModel", TypeDCoreRestoreModel);

            // Commands
            CreateEntityTypeCommand = new CreateEntityTypeCommand(TypeDCoreProjectModel);
            CreateSceneCommand = new CreateSceneCommand(TypeDCoreProjectModel);
            CreateDrawable2dCommand = new CreateDrawable2dCommand(TypeDCoreProjectModel);

            // Hooks
            Hooks.AddHook<ProjectCreateHook>(ProjectCreate);
            Hooks.AddHook<InitUIHook>(InitUI);
            Hooks.AddHook<ComponentContextMenuOpenedHook>(ComponentContextMenuOpened);
        }

        void ProjectCreate(ProjectCreateHook hook)
        {
            var gameCode = new GameCode();
            ProjectModel.AddCode(hook.Project, gameCode);
            ProjectModel.AddCode(hook.Project, new GameTypeDCode());
            ComponentProvider.Save(hook.Project, new Component()
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

        void InitUI(InitUIHook hook)
        {
            hook.Menu.Items.Add(
                new MenuItem() {
                    Name = "_Project",
                    Items = new List<MenuItem>()
                    {
                        new MenuItem()
                        {
                            Name = "_Create Component",
                            Items = new List<MenuItem>()
                            {
                                new MenuItem() {
                                    Name = "_Entity",
                                    ClickParameter = "LoadedProject",
                                    Click = (param) => {

                                        CreateEntityTypeCommand.Execute(new CreateComponentCommandData(param as Project, $"Entities"));
                                    }
                                },
                                new MenuItem() {
                                    Name = "_Scene",
                                    ClickParameter = "LoadedProject",
                                    Click = (param) => {
                                        CreateSceneCommand.Execute(new CreateComponentCommandData(param as Project, $"Scenes"));
                                    }
                                },
                                new MenuItem() {
                                    Name = "_Drawable2d",
                                    ClickParameter = "LoadedProject",
                                    Click = (param) => {
                                        CreateDrawable2dCommand.Execute(new CreateComponentCommandData(param as Project, $"Drawables"));
                                    }
                                }
                            }
                        }
                    }
                }
            );
        }

        void ComponentContextMenuOpened(ComponentContextMenuOpenedHook hook)
        {
            hook.Menu.Items.Add(
                new MenuItem()
                {
                    Name = "_Create Component",
                    Items = new List<MenuItem>()
                    {
                        new MenuItem() {
                            Name = "_Entity",
                            ClickParameter = "LoadedProject",
                            Click = (param) => {
                                CreateEntityTypeCommand.Execute(new CreateComponentCommandData(param as Project, hook.Node?.Name ?? $"Entities"));
                            }
                        },
                        new MenuItem() {
                            Name = "_Scene",
                            ClickParameter = "LoadedProject",
                            Click = (param) => {
                                CreateSceneCommand.Execute(new CreateComponentCommandData(param as Project, hook.Node?.Name ?? $"Scenes"));
                            }
                        },
                        new MenuItem() {
                            Name = "_Drawable2d",
                            ClickParameter = "LoadedProject",
                            Click = (param) => {
                                CreateDrawable2dCommand.Execute(new CreateComponentCommandData(param as Project, hook.Node?.Name ?? $"Drawables"));
                            }
                        }
                    }
                }
            );
        }
    }
}
