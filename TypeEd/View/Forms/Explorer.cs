using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using TypeOEditor.Model;

namespace TypeOEditor.View.Forms
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
            AddTree("Game", new List<TypeInfo>() { project.Game });
            AddTree("Scenes", project.Scenes);
            AddTree("Entities", project.Entities);
            AddTree("Stubs", project.Stubs);
            AddTree("Logics", project.Logics);
            AddTree("Drawables", project.Drawables);
            AddTree("EntityDatas", project.EntityDatas);
        }

        private void AddTree(string node, List<TypeInfo> types)
        {
            foreach (var type in types)
            {
                treeView.Nodes[node].Nodes.Add(type.FullName, type.Name);
            }
        }
    }
}
