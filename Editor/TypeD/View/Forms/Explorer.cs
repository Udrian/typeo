using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using TypeD.Model;

namespace TypeD.View.Forms
{
    public partial class Explorer : UserControl
    {
        public Explorer()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            foreach(TreeNode tree in treeView.Nodes)
            {
                tree.Nodes.Clear();
            }
        }

        public void PopulateTree(Project project)
        {
            foreach(var type in project.Types)
            {
                AddTree(type);
            }

            /*if(project.Game != null)
                AddTree("Game", new List<TypeInfo>() { project.Game });
            AddTree("Scenes", project.Scenes);
            AddTree("Entities", project.Entities);
            AddTree("Stubs", project.Stubs);
            AddTree("Logics", project.Logics);
            AddTree("Drawables", project.Drawables);
            AddTree("EntityDatas", project.EntityDatas);*/
        }
        private void AddTree(TypeInfo type)
        {
            var namespaces = type.Namespace.Split('.');

            var node = treeView.Nodes;
            foreach(var ns in namespaces)
            {
                if (!node.ContainsKey(ns))
                    node.Add(ns);
                node = node[ns].Nodes;
            }

            node.Add(type.FullName, type.Name);
        }
    }
}
