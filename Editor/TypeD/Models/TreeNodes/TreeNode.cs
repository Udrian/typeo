using System;
using System.Collections.Generic;

namespace TypeD.Models.TreeNodes
{
    public class TreeNode
    {
        public static event Action<TreeNode> NodeAddedEvent;
        public static event Action<TreeNode> ClearEvent;

        public string Name { get; set; }
        public string Key { get; private set; }
        public object Object { get; set; }
        public TreeNode Parent { get; private set; }
        public Dictionary<string, TreeNode> Nodes { get; private set; }

        public static TreeNode Create(string name, object obj, string key = null)
        {
            var node = new TreeNode(name, obj, key);
            NodeAddedEvent?.Invoke(node);
            return node;
        }

        protected TreeNode(string name, object obj, string key = null)
        {
            Key = key ?? Guid.NewGuid().ToString();
            Name = name;
            Nodes = new Dictionary<string, TreeNode>();
            Object = obj;
        }

        public void AddNode(string name, object obj, string key = null)
        {
            var node = new TreeNode(name, obj, key);
            node.Parent = this;
            Nodes.Add(node.Key, node);
            NodeAddedEvent?.Invoke(node);
        }

        public void AddSibling(string name, object obj, string key = null)
        {
            var node = new TreeNode(name, obj, key);
            node.Parent = Parent;
            Nodes.Add(node.Key, node);
            NodeAddedEvent?.Invoke(node);
        }

        public void Clear()
        {
            InternalClear();
            ClearEvent?.Invoke(this);
        }

        protected void InternalClear()
        {
            foreach (var node in Nodes.Values)
            {
                node.InternalClear();
            }
            Nodes.Clear();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
