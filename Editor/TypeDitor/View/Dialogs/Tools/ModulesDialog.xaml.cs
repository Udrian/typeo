using System;
using System.Windows;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeDitor.ViewModel.Dialogs.Tools;

namespace TypeDitor.View.Dialogs.Tools
{
    /// <summary>
    /// Interaction logic for ModulesDialog.xaml
    /// </summary>
    public partial class ModulesDialog : Window
    {
        // ViewModel
        ModulesDialogViewModel ModulesDialogViewModel { get; set; }

        // Constructors
        public ModulesDialog(IModuleModel moduleModel, IProjectModel projectModel, ISaveModel saveModel, IModuleProvider moduleProvider, TypeD.Models.Data.Project loadedProject)
        {
            ModulesDialogViewModel = new ModulesDialogViewModel(moduleModel, projectModel, saveModel, moduleProvider, loadedProject);

            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ModulesDialogViewModel.Save();
            Close();
        }

        private async void Window_Initialized(object sender, EventArgs e)
        {
            ModuleList.ItemsSource = await ModulesDialogViewModel.ListModules();
        }
    }
}
