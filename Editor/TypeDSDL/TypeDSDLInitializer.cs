using TypeD;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeDSDL.View.Documents;

namespace TypeDSDL
{
    public class TypeDSDLInitializer : TypeDModuleInitializer
    {
        // Models
        IPanelModel PanelModel { get; set; }

        // Functions
        public override void Initializer(Project project)
        {
            // Models
            PanelModel = Resources.Get<IPanelModel>();

            // Viewers
            PanelModel.AddViewer<SDLViewerDocument>();
        }

        public override void Uninitializer()
        {
        }
    }
}
