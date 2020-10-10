﻿using System.Reflection;
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
                AddTree(type, project);
            }
        }
        private void AddTree(TypeInfo type, Project project)
        {
            var namespaces = (type.Namespace.StartsWith(project.Name)?type.Namespace.Remove(0, project.Name.Length):type.Namespace).Split('.');

            var node = treeView.Nodes;
            foreach(var ns in namespaces)
            {
                if (string.IsNullOrEmpty(ns)) continue;
                if (!node.ContainsKey(ns))
                    node.Add(ns, ns);
                node = node[ns].Nodes;
            }

            node.Add(type.FullName, type.Name);
        }
    }
}
