using System.Collections.Generic;

namespace TypeD.TreeNodes
{
    public class Tree : TreeNode
    {
        public IList<Node> Nodes { get; set; }

        public Tree()
        {
            Nodes = new List<Node>();
        }

        public void AddNode(string name, string type)
        {
            var node = new Node(name, type, this, null);
            Nodes.Add(node);
        }

        internal void AddNode(Node parent, string name, string type)
        {
            var node = new Node(name, type, this, parent);
            parent.Nodes.Add(node);
        }

        public bool Contains(string name)
        {
            return Get(name) != null;
        }

        public Node Get(string name)
        {
            foreach(var node in Nodes)
            {
                if(node.Name == name) return node;
            }
            return null;
        }

        public void Clear()
        {
            InternalClear();
        }

        internal void Clear(Node node)
        {
            node.InternalClear();
        }

        internal void InternalClear()
        {
            foreach(var node in Nodes)
            {
                node.InternalClear();
            }
            Nodes.Clear();
        }
    }
}
