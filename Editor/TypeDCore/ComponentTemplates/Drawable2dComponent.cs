using TypeD.Components;
using TypeD.Helpers;
using TypeDCore.Code.Drawable;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeDCore.ComponentTemplates
{
    public class Drawable2dComponent : ComponentTemplate<Drawable2dCode>
    {
        // Constructors
        public override void Init()
        {
        }

        // Functions
        public override void ChildrenFilter(FilterHelper filter)
        {
            filter.Filters += $"{typeof(Drawable2d).FullName};";
        }
    }
}
