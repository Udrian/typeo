using System.Windows.Controls;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.TreeNodes;

namespace TypeDitor.ViewModel.Panels
{
    class TypeBrowserViewModel
    {
        // Models
        IHookModel HookModel { get; set; }

        TreeView TreeView { get; set; }

        // Constructors
        public TypeBrowserViewModel(IHookModel hookModel, TreeView treeView)
        {
            HookModel = hookModel;

            TreeView = treeView;

            HookModel.AddHook("TypeTreeBuilt", BuildTree);
        }

        private void BuildTree(object param)
        {
            if (param is not TypeTreeBuiltHook hookParam) return;

            TreeView.Items.Clear();
            foreach(var node in hookParam.Tree.Nodes)
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
