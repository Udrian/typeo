using TypeD.TreeNodes;

namespace TypeD.Models.Data.Hooks
{
    public class TypeTreeBuiltHook
    {
        public Tree Tree { get; private set; }

        public TypeTreeBuiltHook(Tree tree)
        {
            Tree = tree;
        }
    }
}
