using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using TypeD.Models;

namespace TypeDEditor.View.Forms
{
    public partial class Explorer : UserControl
    {
        public Explorer()
        {
            InitializeComponent();

            treeView.DrawNode += TreeView_DrawNode;
            treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;

            TypeD.Models.TreeNodes.TreeNode.NodeAddedEvent += TreeNode_NodeAddedEvent;
            TypeD.Models.TreeNodes.TreeNode.ClearEvent += TreeNode_ClearEvent;
        }

        private void TreeNode_ClearEvent(TypeD.Models.TreeNodes.TreeNode obj)
        {
            Clear();
        }

        private void Clear()
        {
            treeView.Nodes.Clear();
        }

        private void TreeNode_NodeAddedEvent(TypeD.Models.TreeNodes.TreeNode obj)
        {
            if(obj.Parent == null)
            {
                treeView.Nodes.Add(obj.Key, obj.Name);
            }
            else
            {
                var parents = new Stack<TypeD.Models.TreeNodes.TreeNode>();
                {
                    var parent = obj.Parent;
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
                            if(node.Name == parent.Key)
                            {
                                nodes = node.Nodes;
                                break;
                            }
                        }
                    }
                }
                nodes.Add(obj.Key, obj.Name);
            }
        }

        private void TreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.Graphics.DrawString(e.Node.Text, treeView.Font, Brushes.Black, Rectangle.Inflate(e.Bounds, 0, 0));
        }
    }
}
