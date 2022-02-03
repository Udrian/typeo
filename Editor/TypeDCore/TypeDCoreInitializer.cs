using System;
using System.Collections.Generic;
using TypeD;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeD.TreeNodes;
using TypeD.View;
using TypeDCore.Code.Game;
using TypeDCore.Commands;
using TypeDCore.Commands.Data;
using TypeDCore.Models;
using TypeDCore.Models.Interfaces;
using TypeOEngine.Typedeaf.Core;

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
        CreateSceneTypeCommand CreateSceneTypeCommand { get; set; }
        CreateDrawable2dTypeCommand CreateDrawable2dTypeCommand { get; set; }

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
            CreateSceneTypeCommand = new CreateSceneTypeCommand(TypeDCoreProjectModel);
            CreateDrawable2dTypeCommand = new CreateDrawable2dTypeCommand(TypeDCoreProjectModel);

            // Hooks
            Hooks.AddHook<ProjectCreateHook>(ProjectCreate);
            Hooks.AddHook<InitUIHook>(InitUI);
            Hooks.AddHook<ComponentContextMenuOpenedHook>(ComponentContextMenuOpened);
        }

        void ProjectCreate(ProjectCreateHook hook)
        {
            var gameCode = new GameCode();
            ProjectModel.InitAndSaveCode(hook.Project, gameCode);
            ComponentProvider.Save(hook.Project, new Component()
            {
                ClassName = gameCode.ClassName,
                Namespace = gameCode.Namespace,
                TemplateClass = gameCode.GetType(),
                TypeOBaseType = typeof(Game)
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
                                        CreateSceneTypeCommand.Execute(new CreateComponentCommandData(param as Project, $"Scenes"));
                                    }
                                },
                                new MenuItem() {
                                    Name = "_Drawable2d",
                                    ClickParameter = "LoadedProject",
                                    Click = (param) => {
                                        CreateDrawable2dTypeCommand.Execute(new CreateComponentCommandData(param as Project, $"Drawables"));
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
                                var @namespace = "Entities";
                                if(hook.Node != null)
                                {
                                    Func<Node, string> getParentName = null;
                                    getParentName = (node) => {
                                        var retVal = "";
                                        if(node.Parent != null)
                                        {
                                            retVal = getParentName(node.Parent);
                                        }

                                        if(node.Nodes.Count != 0)
                                            return retVal == "" ? node.Name : $"{retVal}.{node.Name}";
                                        return retVal;
                                    };
                                    @namespace = getParentName(hook.Node);
                                }
                                CreateEntityTypeCommand.Execute(new CreateComponentCommandData(param as Project, @namespace));
                            }
                        },
                        new MenuItem() {
                            Name = "_Scene",
                            ClickParameter = "LoadedProject",
                            Click = (param) => {
                                var @namespace = "Scenes";
                                if(hook.Node != null)
                                {
                                    Func<Node, string> getParentName = null;
                                    getParentName = (node) => {
                                        var retVal = "";
                                        if(node.Parent != null)
                                        {
                                            retVal = getParentName(node.Parent);
                                        }

                                        if(node.Nodes.Count != 0)
                                            return retVal == "" ? node.Name : $"{retVal}.{node.Name}";
                                        return retVal;
                                    };
                                    @namespace = getParentName(hook.Node);
                                }
                                CreateSceneTypeCommand.Execute(new CreateComponentCommandData(param as Project, @namespace));
                            }
                        },
                        new MenuItem() {
                            Name = "_Drawable2d",
                            ClickParameter = "LoadedProject",
                            Click = (param) => {
                                var @namespace = "Drawables";
                                if(hook.Node != null)
                                {
                                    Func<Node, string> getParentName = null;
                                    getParentName = (node) => {
                                        var retVal = "";
                                        if(node.Parent != null)
                                        {
                                            retVal = getParentName(node.Parent);
                                        }

                                        if(node.Nodes.Count != 0)
                                            return retVal == "" ? node.Name : $"{retVal}.{node.Name}";
                                        return retVal;
                                    };
                                    @namespace = getParentName(hook.Node);
                                }
                                CreateDrawable2dTypeCommand.Execute(new CreateComponentCommandData(param as Project, @namespace));
                            }
                        }
                    }
                }
            );
        }
    }
}
