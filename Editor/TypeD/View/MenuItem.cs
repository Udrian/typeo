using System;
using System.Collections.Generic;

namespace TypeD.View
{
    public class MenuItem
    {
        public string Name { get; set; }
        public Action<object> Click { get; set; }
        public string ClickParameter { get; set; }
        public List<MenuItem> Items;

        public MenuItem()
        {
            Items = new List<MenuItem>();
        }
    }
}
