using TypeD.Components;
using TypeD.Helpers;
using TypeDCore.Code.Scene;
using TypeOEngine.Typedeaf.Core.Entities;

namespace TypeDCore.ComponentTemplates
{
    public class SceneComponent : ComponentTemplate<SceneCode>
    {
        // Constructors
        public override void Init()
        {
        }

        // Functions
        public override void ChildrenFilter(FilterHelper filter)
        {
            filter.Filters += $"{typeof(Entity).FullName};";
        }
    }
}
