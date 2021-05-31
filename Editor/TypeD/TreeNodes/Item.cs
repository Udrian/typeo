namespace TypeD.TreeNodes
{
    public abstract class Item
    {
        public string Type { get; set; }
        public string Name { get; set; }

        public abstract void Save();
    }
}
