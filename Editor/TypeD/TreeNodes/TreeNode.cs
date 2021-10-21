﻿using System.Collections.Generic;

namespace TypeD.TreeNodes
{
    public interface TreeNode
    {
        IList<Node> Nodes { get; set; }
        void AddNode(string name, Item item);
        bool Contains(string name);
        Node Get(string name);
        void Clear();
    }
}
