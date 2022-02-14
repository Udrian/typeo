using TypeD.Models.Data;

namespace TypeDCore.Commands.Data
{
    public class DeleteComponentTypeCommandData
    {
        public Project Project { get; set; }
        public Component Component { get; set; }
    }
}
