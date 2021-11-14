using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeDitor.Helpers;

namespace TypeDitor.ViewModel.Panels
{
    class TypeBrowserViewModel
    {
        public class Node
        {
            public string IconPath { get { return $"/Icons/{Type}.png"; } }
            public string Name { get { return Context.Name; } }
            public string Type { get { return Context.Type; } }
            public TypeD.TreeNodes.Node Context { get; set; }
            public ObservableCollection<Node> Nodes { get; set; }
        }

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
            Nodes = TreeToNodeList(LoadedProject.TypeOTypeTree.Nodes);
            TreeView.ItemsSource = Nodes;
        }

        private ObservableCollection<Node> TreeToNodeList(IList<TypeD.TreeNodes.Node> treeNodes)
        {
            var nodes = new ObservableCollection<Node>();

            foreach(var treeNode in treeNodes)
            {
                nodes.Add(new Node()
                {
                    Context = treeNode,
                    Nodes = TreeToNodeList(treeNode.Nodes)
                });
            }

            return nodes;
        }

        public void ContextMenuOpened(ContextMenu contextMenu, Node node)
        {
            var typeContextMenuOpenedHook = new TypeContextMenuOpenedHook(node.Context);
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
            TreeView.Dispatcher.Invoke(() =>
            {
                var treeNodes = TreeToNodeList(LoadedProject.TypeOTypeTree.Nodes);
                Buildtree(treeNodes, Nodes);
            });
        }

        private void Buildtree(IList<Node> treeNodes, IList<Node> nodes)
        {
            var shouldBeDeleted = new List<Node>();
            var i = 0;
            foreach (var treeNode in treeNodes)
            {
                Node foundNode = null;
                foreach (var node in nodes)
                {
                    if (node.Name == treeNode.Name)
                    {
                        foundNode = node;
                        break;
                    }

                    var delFound = false;
                    foreach(var delTreeNode in treeNodes)
                    {
                        if(delTreeNode.Name == node.Name)
                        {
                            delFound = true;
                            break;
                        }
                    }
                    if(!delFound)
                    {
                        shouldBeDeleted.Add(node);
                    }
                }
                if (foundNode == null)
                {
                    nodes.Insert(i, treeNode);
                }
                else
                {
                    Buildtree(treeNode.Nodes, foundNode.Nodes);
                }
                i++;
            }
            foreach(var delNode in shouldBeDeleted)
            {
                nodes.Remove(delNode);
            }
        }
    }
}
