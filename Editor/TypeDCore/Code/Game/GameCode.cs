using System.Collections.Generic;
using TypeD.Code;

namespace TypeDCore.Code.Game
{
    partial class GameCode : TypeDCodalyzer
    {
        // Constructors
        public GameCode() : base()
        {
            BaseClass = "Game";
        }

        public override void Init()
        {
            ClassName = $"{Project.ProjectName}Game";
            Namespace = Project.ProjectName;

            base.Init();
        }

        protected override void InitClass()
        {
            AddUsings(new List<string>()
            {
                "TypeOEngine.Typedeaf.Core"
            });

            AddFunction(new Function("protected void InternalInitialize()", () => { }));
            AddFunction(new Function("protected void InternalCleanup()", () => { }));
        }
    }
}
