using System.Collections.Generic;
using TypeD.Code;
using TypeD.Helpers;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeDCore.Code.Entity
{
    partial class EntityCode : ComponentTypeCode
    {
        // Properties
        public List<string> Drawables { get; set; }

        // Constructors
        protected override void InitTypeDClass()
        {
            var drawablesStartBlock = "//Drawables start block";
            var drawablesEndBlock = "//Drawables end block";

            AddFunction(new Function("public override void Initialize()", () => {
                if (!IsBaseComponentType)
                {
                    Writer.AddLine("base.Initialize();");
                }
                if (Drawables.Count > 0)
                {
                    Writer.AddLine(drawablesStartBlock);
                    foreach (var drawable in Drawables)
                    {
                        Writer.AddLine($"Drawables.Create<{drawable}>();");
                    }
                    Writer.AddLine(drawablesEndBlock);
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

            Drawables = FileHelper.FetchStringList(FilePath(), drawablesStartBlock, drawablesEndBlock);
        }
    }
}
