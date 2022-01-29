namespace TypeDCore.ViewModel.Dialogs.Project
{
    public class CreateEntityTypeViewModel : CreateComponentTypeBaseViewModel
    {
        // Constructors
        public CreateEntityTypeViewModel(TypeD.Models.Data.Project project) : base(project)
        {
        }

        // Properties
        public bool ComponentUpdatable { get; set; }
        public bool ComponentDrawable { get; set; }
    }
}
