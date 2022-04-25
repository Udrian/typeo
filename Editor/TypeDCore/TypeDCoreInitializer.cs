using System;
using System.Collections.Generic;
using TypeD;
using TypeDCore.Commands.Data;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Providers.Interfaces;
using TypeD.View.TreeNodes;
using TypeD.View;
using TypeDCore.Commands;
using TypeDCore.Models;
using TypeDCore.Models.Interfaces;
using TypeOEngine.Typedeaf.Core;
using TypeDCore.Components;
using TypeD.Models.Interfaces;
using TypeD.Models.Data.SettingContexts;
using TypeDCore.View.Panels;

namespace TypeDCore
{
    public class TypeDCoreInitializer : TypeDModuleInitializer
    {
        // Providers
        IComponentProvider ComponentProvider { get; set; }

        // Models
        ITypeDCoreProjectModel TypeDCoreProjectModel { get; set; }
        ITypeDCoreRestoreModel TypeDCoreRestoreModel { get; set; }
        ISettingModel SettingModel { get; set; }
        IPanelModel PanelModel { get; set; }

        // Commands
        CreateEntityTypeCommand CreateEntityTypeCommand { get; set; }
        CreateSceneTypeCommand CreateSceneTypeCommand { get; set; }
        CreateDrawable2dTypeCommand CreateDrawable2dTypeCommand { get; set; }
        DeleteComponentTypeCommand DeleteComponentTypeCommand { get; set; }
        RenameComponentTypeCommand RenameComponentTypeCommand { get; set; }
        SetStartSceneCommand SetStartSceneCommand { get; set; }
        AddComponentCommand AddComponentCommand { get; set; }
        OpenComponentCommand OpenComponentCommand { get; set; }
        CloseComponentCommand CloseComponentCommand { get; set; }

        public override void Initializer(Project project)
        {
            // Internal Models
            TypeDCoreProjectModel = new TypeDCoreProjectModel();
            TypeDCoreRestoreModel = new TypeDCoreRestoreModel();

            Resources.Add(new List<object>() {
                 TypeDCoreProjectModel,
                 TypeDCoreRestoreModel,
            });

            // Providers
            ComponentProvider = Resources.Get<IComponentProvider>();

            // Models
            SettingModel = Resources.Get<ISettingModel>();
            PanelModel = Resources.Get<IPanelModel>();

            // Commands
            CreateEntityTypeCommand = new CreateEntityTypeCommand(Resources);
            CreateSceneTypeCommand = new CreateSceneTypeCommand(Resources);
            CreateDrawable2dTypeCommand = new CreateDrawable2dTypeCommand(Resources);
            DeleteComponentTypeCommand = new DeleteComponentTypeCommand(Resources);
            RenameComponentTypeCommand = new RenameComponentTypeCommand(Resources);
            SetStartSceneCommand = new SetStartSceneCommand(Resources);
            AddComponentCommand = new AddComponentCommand(Resources);
            OpenComponentCommand = new OpenComponentCommand(Resources);
            CloseComponentCommand = new CloseComponentCommand(Resources);

            // Hooks
            Hooks.AddHook<ProjectCreateHook>(ProjectCreate);
            Hooks.AddHook<InitUIHook>(InitUI);
            Hooks.AddHook<ComponentTypeBrowserContextMenuOpenedHook>(ComponentTypeBrowserContextMenuOpened);
            Hooks.AddHook<ComponentContextMenuHook>(ComponentContextMenuOpened);
            Hooks.AddHook<OptionsHook>(OptionsWindowOpened);

            // Settings
            SettingModel.InitContext<MainWindowSettingContext>();

            // Panels
            PanelModel.AttachPanel("typed_tabs", "Tabs", new System.Windows.Controls.TabControl());
            PanelModel.AttachPanel("typed_component", "Component", new ComponentPanel(project));
            PanelModel.AttachPanel("typed_output", "Output", new OutputPanel());
            PanelModel.AttachPanel("typed_componenttypebrowser", "Component Type Browser", new ComponentBrowserPanel(project));
        }

        public override void Uninitializer()
        {
            // Panels
            PanelModel.DetachPanel("typed_tabs");
            PanelModel.DetachPanel("typed_component");
            PanelModel.DetachPanel("typed_output");
            PanelModel.DetachPanel("typed_componenttypebrowser");

            // Internal Models
            Resources.Remove("TypeDCoreProjectModel");
            Resources.Remove("TypeDCoreRestoreModel");

            // Hooks
            Hooks.RemoveHook<ProjectCreateHook>();
            Hooks.RemoveHook<InitUIHook>();
            Hooks.RemoveHook<ComponentTypeBrowserContextMenuOpenedHook>();
            Hooks.RemoveHook<ComponentContextMenuHook>();

            // Settings
            SettingModel.RemoveContext<MainWindowSettingContext>();
        }

        void ProjectCreate(ProjectCreateHook hook)
        {
            ComponentProvider.Create<GameComponent>(
                hook.Project,
                $"{hook.Project.ProjectName}Game",
                hook.Project.ProjectName
            );
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
                            Name = "_Open Component",
                            ClickParameter = "LoadedProject",
                            Click = (param) =>
                            {
                                OpenComponentCommand.Execute(new OpenComponentCommandData() { Project = param as Project});
                            }
                        },
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

        void ComponentTypeBrowserContextMenuOpened(ComponentTypeBrowserContextMenuOpenedHook hook)
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
            if(hook.Node != null && hook.Node.Item is Component)
            {
                var component = hook.Node.Item as Component;

                if(component.TypeOBaseType != typeof(Game))
                {
                    hook.Menu.Items.Add(
                        new MenuItem()
                        {
                            Name = "_Delete Component",
                            ClickParameter = "LoadedProject",
                            Click = (param) =>
                            {
                                DeleteComponentTypeCommand.Execute(new ComponentCommandData() { Component = component, Project = param as Project });
                            }
                        }
                    );
                    hook.Menu.Items.Add(
                        new MenuItem()
                        {
                            Name = "_Rename Component",
                            ClickParameter = "LoadedProject",
                            Click = (param) =>
                            {
                                RenameComponentTypeCommand.Execute(new ComponentCommandData() { Component = component, Project = param as Project });
                            }
                        }
                    );
                }
                if (component.TypeOBaseType == typeof(Scene))
                {
                    hook.Menu.Items.Add(
                       new MenuItem()
                       {
                           Name = "_Set Start Scene",
                           ClickParameter = "LoadedProject",
                           Click = (param) =>
                           {
                               SetStartSceneCommand.Execute(new ComponentCommandData() { Component = component, Project = param as Project });
                           }
                       }
                   );
                }
            }
        }

        void ComponentContextMenuOpened(ComponentContextMenuHook hook)
        {
            if(hook.OpenedComponent == null)
            {
                hook.Menu.Items.Add(
                    new MenuItem()
                    {
                        Name = "_Open Component",
                        ClickParameter = "LoadedProject",
                        Click = (param) => {
                            OpenComponentCommand.Execute(new OpenComponentCommandData() { Project = param as Project });
                        }
                    }
                );

                return;
            }

            hook.Menu.Items.Add(
                new MenuItem()
                {
                    Name = "_Add Component",
                    ClickParameter = "LoadedProject",
                    Click = (param) =>
                    {
                        AddComponentCommand.Execute(new AddComponentCommandData() { ToComponent = hook.SelectedComponent ?? hook.OpenedComponent, Project = param as Project});
                    }
                }
            );

            hook.Menu.Items.Add(
                new MenuItem()
                {
                    Name = "Close",
                    ClickParameter = "LoadedProject",
                    Click = (param) =>
                    {
                        CloseComponentCommand.Execute(new CloseComponentCommandData() { Project = param as Project, Component = hook.OpenedComponent });
                    }
                }
            );
        }

        void OptionsWindowOpened(OptionsHook hook)
        {
            var mainWindowSettingContext = SettingModel.GetContext<MainWindowSettingContext>(hook.Level);

            hook.Items.Add(new SettingItem("Hello World", "Hello World", mainWindowSettingContext.HelloWorld, (value) => {
                mainWindowSettingContext.HelloWorld.Value = value as string;
                SettingModel.SetContext(mainWindowSettingContext);
            }));
        }
    }
}
