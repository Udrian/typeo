namespace TypeDCore.ViewModel.Dialogs.Project
{
    public class CreateEntityTypeViewModel : CreateComponentTypeBaseViewModel
    {
        // Constructors
        public CreateEntityTypeViewModel(TypeD.Models.Data.Project project, string @namespace, string inherits) : base(project, @namespace, inherits)
        {
            ComponentBaseType = "Entity2d";
        }

        // Properties
        public bool ComponentUpdatable { get; set; }
        public bool ComponentDrawable { get; set; }
    }
}
