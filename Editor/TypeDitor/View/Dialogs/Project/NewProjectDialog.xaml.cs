using Ookii.Dialogs.Wpf;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace TypeDitor.View.Dialogs.Project
{
    /// <summary>
    /// Interaction logic for NewProjectDialog.xaml
    /// </summary>
    public partial class NewProjectDialog : Window, INotifyPropertyChanged
    {
        public NewProjectDialog()
        {
            InitializeComponent();
            this.DataContext = this;

            ProjectLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TypeD");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void btnOpenLocation_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new VistaFolderBrowserDialog();
            folderBrowserDialog.SelectedPath = ProjectLocation;
            if (folderBrowserDialog.ShowDialog() == true)
            {
                ProjectLocation = folderBrowserDialog.SelectedPath;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private string projectName;
        public string ProjectName
        {
            get => projectName;
            set
            {
                if (ProjectName == ProjectCSSolutionName)
                {
                    ProjectCSSolutionName = value;
                }
                if (ProjectName == ProjectCSProjectName)
                {
                    ProjectCSProjectName = value;
                }
                projectName = value;
                NotifyPropertyChanged("ProjectName");
            }
        }

        private string projectLocation;
        public string ProjectLocation {
            get => projectLocation;
            set
            {
                projectLocation = value;
                NotifyPropertyChanged("ProjectLocation");
            }
        }

        private string projectCSSolutionName;
        public string ProjectCSSolutionName {
            get => projectCSSolutionName;
            set
            {
                projectCSSolutionName = value;
                NotifyPropertyChanged("ProjectCSSolutionName");
            }
        }

        private string projectCSProjectName;
        public string ProjectCSProjectName {
            get => projectCSProjectName;
            set
            {
                projectCSProjectName = value;
                NotifyPropertyChanged("ProjectCSProjectName");
            }
        }
    }
}
