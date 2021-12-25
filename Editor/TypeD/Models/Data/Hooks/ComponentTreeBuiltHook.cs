using TypeD.TreeNodes;

namespace TypeD.Models.Data.Hooks
{
    public class ComponentTreeBuiltHook
    {
        public Tree Tree { get; private set; }

        public ComponentTreeBuiltHook(Tree tree)
        {
            Tree = tree;
        }
    }
}
