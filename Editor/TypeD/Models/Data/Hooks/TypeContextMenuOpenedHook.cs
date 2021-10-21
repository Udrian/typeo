using TypeD.TreeNodes;
using TypeD.View;

namespace TypeD.Models.Data.Hooks
{
    public class TypeContextMenuOpenedHook
    {
        public Menu Menu { get; set; }
        public Node Node { get; private set; }

        public TypeContextMenuOpenedHook(Node node)
        {
            Menu = new Menu();
            Node = node;
        }
    }
}
