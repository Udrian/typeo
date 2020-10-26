using System.Collections.Generic;
using TypeD.Models;

namespace TypeD.Code
{
    public class GameCode : Codalyzer
    {
        public GameCode(ProjectModel project) : base(project, $"{project.ProjectName}Game", $"{project.ProjectName}")
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
