using TypeD.Code;
using TypeD.Models;

namespace TypeD.Commands.Project
{
    public static partial class ProjectCommand
    {
        public static void AddEntity(ProjectModel project, string className, string @namespace, bool updatable, bool drawable)
        {
            var entityCode = new EntityCode(project, className, $"{project.ProjectName}.{@namespace}");

            if (updatable)
            {
                entityCode.Usings.Add("TypeOEngine.Typedeaf.Core.Interfaces");
                entityCode.AddInterface("IUpdatable");
                entityCode.AddProperty(new Codalyzer.Property("public bool Pause"));
                entityCode.AddFunction(new Codalyzer.Function("public void Update(double dt)", () => { }));
            }
            if(drawable)
            {
                entityCode.Usings.Add("TypeOEngine.Typedeaf.Core.Entities.Interfaces");
                entityCode.Usings.Add("TypeOEngine.Typedeaf.Core.Engine.Graphics");
                entityCode.AddInterface("IDrawable");
                entityCode.AddProperty(new Codalyzer.Property("public bool Hidden"));
                entityCode.AddProperty(new Codalyzer.Property("public int DrawOrder"));
                entityCode.AddFunction(new Codalyzer.Function("public void Draw(Canvas canvas)", () => { }));
            }

            project.AddCode(entityCode);
            project.AddCode(new EntityTypeDCode(project, className, $"{project.ProjectName}.{@namespace}"));
        }
    }
}
