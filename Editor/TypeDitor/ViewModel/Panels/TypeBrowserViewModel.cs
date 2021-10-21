using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.TreeNodes;
using TypeDitor.Helpers;

namespace TypeDitor.ViewModel.Panels
{
    class TypeBrowserViewModel
    {
        // Models
        IHookModel HookModel { get; set; }

        // View
        TreeView TreeView { get; set; }

        // Data
        public Project LoadedProject { get; private set; }
        ObservableCollection<Node> Nodes { get; set; }

        // Constructors
        public TypeBrowserViewModel(IHookModel hookModel, Project loadedProject, TreeView treeView)
        {
            HookModel = hookModel;
            LoadedProject = loadedProject;
            TreeView = treeView;

            HookModel.AddHook("TypeTreeBuilt", BuildTree);
            Nodes = new ObservableCollection<Node>(LoadedProject.Tree.Nodes);
            TreeView.ItemsSource = Nodes;
        }

        public void ContextMenuOpened(ContextMenu contextMenu, Node node)
        {
            var typeContextMenuOpenedHook = new TypeContextMenuOpenedHook(node);
            HookModel.Shoot("TypeContextMenuOpened", typeContextMenuOpenedHook);

            contextMenu.Items.Clear();
            foreach (var menu in typeContextMenuOpenedHook.Menu.Items)
            {
                ViewHelper.InitMenu(contextMenu, menu, this);
            }
        }

        private void BuildTree(object param)
        {
            if (param is not TypeTreeBuiltHook hookParam) return;

            Nodes = new ObservableCollection<Node>(hookParam.Tree.Nodes);
            TreeView.ItemsSource = Nodes;
        }
    }
}
