namespace TypeDCore.Commands.Project.Data
{
    class CreateTypeCommandData
    {
        public TypeD.Models.Data.Project Project { get; private set; }
        public string Namespace { get; private set; }

        public CreateTypeCommandData(TypeD.Models.Data.Project project, string @namespace)
        {
            Project = project;
            Namespace = @namespace;
        }
    }
}
