using TypeD.TreeNodes;
using TypeD.View;

namespace TypeD.Models.Data.Hooks
{
    public class ComponentContextMenuOpenedHook
    {
        public Menu Menu { get; set; }
        public Node Node { get; private set; }

        public ComponentContextMenuOpenedHook(Node node)
        {
            Menu = new Menu();
            Node = node;
        }
    }
}
