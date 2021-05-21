using System;
using System.Collections.Generic;

namespace TypeD.TreeNodes
{
    public class Tree : TreeNode
    {
        public static event Action<Node> NodeAddedEvent;
        public static event Action<TreeNode> ClearTreeEvent;

        public List<Node> Nodes { get; set; }

        public Tree()
        {
            Nodes = new List<Node>();
        }

        public void AddNode(string name, Item item)
        {
            var node = new Node(name, item, this, null);
            Nodes.Add(node);
            NodeAddedEvent?.Invoke(node);
        }

        internal void AddNode(Node parent, string name, Item item)
        {
            var node = new Node(name, item, this, parent);
            parent.Nodes.Add(node);
            NodeAddedEvent?.Invoke(node);
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
            ClearTreeEvent?.Invoke(this);
        }

        internal void Clear(Node node)
        {
            node.InternalClear();
            ClearTreeEvent?.Invoke(node);
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
