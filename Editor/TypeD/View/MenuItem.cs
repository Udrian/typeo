using System;
using System.Collections.Generic;

namespace TypeD.View
{
    public class MenuItem
    {
        // Properties
        public string Name { get; set; }
        public Action<object> Click { get; set; }
        public string ClickParameter { get; set; }
        public List<MenuItem> Items;

        // Constructors
        public MenuItem()
        {
            Items = new List<MenuItem>();
        }
    }
}
