﻿using System.Collections.Generic;

namespace TypeD.TreeNodes
{
    public class Node : TreeNode
    {
        public string Name { get; set; }
        public object Item { get; set; }
        public Tree Tree { get; private set; }
        public Node Parent { get; private set; }
        public IList<Node> Nodes { get; set; }

        internal Node(string name, object item, Tree tree, Node parent)
        {
            Name = name;
            Item = item;
            Tree = tree;
            Parent = parent;
            Nodes = new List<Node>();
        }

        public void AddNode(string name, object item)
        {
            Tree.AddNode(this, name, item);
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
            Tree.Clear(this);
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
