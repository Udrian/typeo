using System.Collections.Generic;
using TypeD.Code;
using TypeD.Helpers;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeDCore.Code.Entity
{
    public partial class EntityCode : ComponentTypeCode
    {
        // Constructors
        protected override void InitTypeDClass()
        {
            AddFunction(new Function("public override void Initialize()", () => {
                if (!IsBaseComponentType)
                {
                    Writer.AddLine("base.Initialize();");
                }
                foreach (var child in Component.Children)
                {
                    if (child.TypeOBaseType == typeof(Entity2d))
                        Writer.AddLine($"Entities.Create<{child.FullName}>();");
                    else if (child.TypeOBaseType == typeof(Drawable2d))
                        Writer.AddLine($"Drawables.Create<{child.FullName}>();");
                }
                if (IsBaseComponentType)
                {
                    Writer.AddLine("InternalInitialize();");
                }
            }));

            if (Updatable && (ParentComponent == null || !ParentComponent.Interfaces.Contains(typeof(IUpdatable))))
            {
                AddUsing("TypeOEngine.Typedeaf.Core.Interfaces");
                AddInterface(typeof(IUpdatable));
                AddProperty(new Property("public bool Pause"));
            }

            if (Drawable && (ParentComponent == null || !ParentComponent.Interfaces.Contains(typeof(IDrawable))))
            {
                AddUsing("TypeOEngine.Typedeaf.Core.Entities.Interfaces");
                AddInterface(typeof(IDrawable));
                AddProperty(new Property("public bool Hidden"));
                AddProperty(new Property("public int DrawOrder"));
            }
        }
    }
}
