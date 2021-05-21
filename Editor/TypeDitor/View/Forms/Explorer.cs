using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TypeD.Data;
using TypeDEditor.Controller;
using TypeDEditor.Helper;

namespace TypeDEditor.View.Forms
{
    public partial class Explorer : UserControl
    {
        public event Action<TypeDType> NodeSelect;

        public Explorer()
        {
            InitializeComponent();

            treeView.DrawNode += TreeView_DrawNode;
            treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;

            TypeD.TreeNodes.Tree.NodeAddedEvent += TreeNode_NodeAddedEvent;
            TypeD.TreeNodes.Tree.ClearTreeEvent += TreeNode_ClearEvent;
        }

        private void TreeNode_ClearEvent(TypeD.TreeNodes.TreeNode obj)
        {
            Clear();
        }

        private void Clear()
        {
            ThreadHelper.InvokeMainThread(this, () => {
                treeView.Nodes.Clear();
            });
        }

        private void TreeNode_NodeAddedEvent(TypeD.TreeNodes.Node node)
        {
            ThreadHelper.InvokeMainThread(this, () =>
            {
                if (node.Parent == null)
                {
                    treeView.Nodes.Add(node.Name);
                }
                else
                {
                    var parents = new Stack<TypeD.TreeNodes.Node>();
                    {
                        var parent = node.Parent;
                        while(parent != null)
                        {
                            parents.Push(parent);
                            parent = parent.Parent;
                        }
                    }

                    var nodes = treeView.Nodes;
                    {
                        while(parents.Count > 0)
                        {
                            var parent = parents.Pop();
                            foreach(TreeNode node in nodes)
                            {
                                if(node.Name == parent.Name)
                                {
                                    nodes = node.Nodes;
                                    break;
                                }
                            }
                        }
                    }
                    var addedNode = nodes.Add(node.Name);
                    addedNode.Tag = node;
                }
            });
        }

        private void TreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.Graphics.DrawString(e.Node.Text, treeView.Font, Brushes.Black, Rectangle.Inflate(e.Bounds, 0, 0));
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var type = e.Node.Tag as TypeDType;
            if (type == null) return;

            NodeSelect?.Invoke(type);
        }
    }
}
