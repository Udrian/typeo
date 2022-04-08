using TypeD.Models.Data;

namespace TypeDCore.Commands.Data
{
    public class AddComponentCommandData
    {
        public Project Project { get; set; }
        public Component ToComponent { get; set; }
    }
}
