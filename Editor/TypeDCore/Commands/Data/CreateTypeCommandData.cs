using TypeD.Models.Data;

namespace TypeDCore.Commands.Data
{
    class CreateTypeCommandData
    {
        public Project Project { get; private set; }
        public string Namespace { get; private set; }

        public CreateTypeCommandData(Project project, string @namespace)
        {
            Project = project;
            Namespace = @namespace;
        }
    }
}
