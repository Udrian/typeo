using TypeD.View;

namespace TypeD.Models.Data.Hooks
{
    public class InitUIHook
    {
        public Menu Menu { get; set; }

        public InitUIHook()
        {
            Menu = new Menu();
        }
    }
}
