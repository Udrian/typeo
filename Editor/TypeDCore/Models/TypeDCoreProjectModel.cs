using TypeD;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeDCore.Code.Drawable2d;
using TypeDCore.Code.Entity;
using TypeDCore.Code.Scene;
using TypeDCore.Models.Interfaces;

namespace TypeDCore.Models
{
    class TypeDCoreProjectModel : ITypeDCoreProjectModel
    {
        // Models
        IProjectModel ProjectModel { get; set; }
        ISaveModel SaveModel { get; set; }

        // Providers
        IProjectProvider ProjectProvider { get; set; }

        // Constructors
        public TypeDCoreProjectModel(
            IProjectModel projectModel, ISaveModel saveModel,
            IProjectProvider projectProvider
        )
        {
            ProjectModel = projectModel;
            SaveModel = saveModel;
            ProjectProvider = projectProvider;
        }

        // Functions
        public void CreateEntity(Project project, string className, string @namespace, bool updatable, bool drawable)
        {
            var entityCode = new EntityCode(project, className, $"{project.ProjectName}.{@namespace}");

            if (updatable)
            {
                entityCode.Usings.Add("TypeOEngine.Typedeaf.Core.Interfaces");
                entityCode.AddInterface("IUpdatable");
                entityCode.AddProperty(new Codalyzer.Property("public bool Pause"));
                entityCode.AddFunction(new Codalyzer.Function("public void Update(double dt)", () => { }));
            }
            if (drawable)
            {
                entityCode.Usings.Add("TypeOEngine.Typedeaf.Core.Entities.Interfaces");
                entityCode.Usings.Add("TypeOEngine.Typedeaf.Core.Engine.Graphics");
                entityCode.AddInterface("IDrawable");
                entityCode.AddProperty(new Codalyzer.Property("public bool Hidden"));
                entityCode.AddProperty(new Codalyzer.Property("public int DrawOrder"));
                entityCode.AddFunction(new Codalyzer.Function("public void Draw(Canvas canvas)", () => { }));
            }

            ProjectModel.AddCode(project, entityCode, "Entity");
            ProjectModel.AddCode(project, new EntityTypeDCode(project, className, $"{project.ProjectName}.{@namespace}"), "Entity");

            ProjectModel.BuildTree(project);

            SaveModel.AddSave("Project", () => { return ProjectProvider.Save(project); });
        }

        public void CreateScene(Project project, string className, string @namespace)
        {
            ProjectModel.AddCode(project, new SceneCode(project, className, $"{project.ProjectName}.{@namespace}"), "Scene");
            ProjectModel.AddCode(project, new SceneTypeDCode(project, className, $"{project.ProjectName}.{@namespace}"), "Scene");

            ProjectModel.BuildTree(project);

            SaveModel.AddSave("Project", () => { return ProjectProvider.Save(project); });
        }

        public void CreateDrawable2d(Project project, string className, string @namespace)
        {
            ProjectModel.AddCode(project, new Drawable2dCode(project, className, $"{project.ProjectName}.{@namespace}"), "Drawable");

            ProjectModel.BuildTree(project);
        }
    }
}
