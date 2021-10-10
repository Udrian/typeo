using System.Collections.Generic;

namespace TypeD.View
{
    public class MenuItem
    {
        public string Name { get; set; }
        public List<MenuItem> Items;

        public MenuItem()
        {
            Items = new List<MenuItem>();
        }
    }
}
