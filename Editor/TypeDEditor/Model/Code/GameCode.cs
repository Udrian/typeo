using System.Collections.Generic;

namespace TypeD.Model.Code
{
    public class GameCode : Codalyzer
    {
        public GameCode(Project project) : base(project, $"{project.Name}Game", $"{project.Name}")
        {
            Base = "Game";
            Usings = new List<string>()
            {
                "TypeOEngine.Typedeaf.Core"
            };
        }

        protected override void GenerateBody()
        {
            AddLine("public override void Initialize()");
            AddLeftCurlyBracket();
            AddRightCurlyBrackets();
            AddLine("public override void Draw()");
            AddLeftCurlyBracket();
            AddRightCurlyBrackets();
            AddLine("public override void Update(double dt)");
            AddLeftCurlyBracket();
            AddRightCurlyBrackets();
            AddLine("public override void Cleanup()");
            AddLeftCurlyBracket();
            AddRightCurlyBrackets();
        }
    }
}
