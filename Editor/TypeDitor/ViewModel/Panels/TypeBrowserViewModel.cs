using System.Windows.Controls;
using TypeD.Models.Interfaces;
using TypeD.TreeNodes;

namespace TypeDitor.ViewModel.Panels
{
    class TypeBrowserViewModel
    {
        // Models
        IHookModel HookModel { get; set; }

        TreeView TreeView;

        // Constructors
        public TypeBrowserViewModel(IHookModel hookModel, TreeView treeView)
        {
            HookModel = hookModel;

            TreeView = treeView;

            HookModel.AddHook("TypeTreeBuilt", (param) => { BuildTree(param as Tree); });
        }

        private void BuildTree(Tree tree)
        {
            TreeView.Items.Clear();
            foreach(var node in tree.Nodes)
            {
                AddNode(TreeView.Items, node);
            }
        }

        private void AddNode(ItemCollection itemCollection, Node node)
        {
            var tvi = new TreeViewItem();
            tvi.Header = node.Name;

            itemCollection.Add(tvi);
            foreach(var innerNode in node.Nodes)
            {
                AddNode(tvi.Items, innerNode);
            }
        }
    }
}
