using System.Collections.Generic;
using TypeD.Code;
using TypeD.Helpers;

namespace TypeDCore.Code.Scene
{
    public partial class SceneCode : ComponentTypeCode
    {
        // Properties
        public List<string> Entities { get; set; }

        // Constructors
        protected override void InitTypeDClass()
        {
            var entityStartBlock = "//Entity start block";
            var entityEndBlock = "//Entity end block";

            AddUsings(new List<string>()
            {
                "TypeOEngine.Typedeaf.Core.Common"
            });

            AddFunction(new Function("public override void Initialize()", () => {
                if (!IsBaseComponentType)
                {
                    Writer.AddLine("base.Initialize();");
                }
                if (Entities.Count > 0)
                {
                    Writer.AddLine(entityStartBlock);
                    foreach (var entity in Entities)
                    {
                        Writer.AddLine($"Entities.Create<{entity}>();");
                    }
                    Writer.AddLine(entityEndBlock);
                }
                if (IsBaseComponentType)
                {
                    Writer.AddLine("InternalInitialize();");
                }
            }));

            AddFunction(new Function("public override void Update(double dt)", () => {
                if (IsBaseComponentType)
                {
                    Writer.AddLine("Entities.Update(dt);");
                    Writer.AddLine("UpdateLoop.Update(dt);");
                }
                else
                {
                    Writer.AddLine("base.Update(dt);");
                }
            }));
            AddFunction(new Function("public override void Draw()", () => {
                if (IsBaseComponentType)
                {
                    Writer.AddLine("Canvas.Clear(Color.Black);");
                    Writer.AddLine("DrawStack.Draw(Canvas);");
                    Writer.AddLine("Canvas.Present();");
                }
                else
                {
                    Writer.AddLine("base.Draw();");
                }
            }));

            Entities = FileHelper.FetchStringList(FilePath(), entityStartBlock, entityEndBlock);
        }
    }
}
