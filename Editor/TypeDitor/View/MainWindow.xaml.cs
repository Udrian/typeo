﻿using System.Windows;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeDitor.View.Panels;
using TypeDitor.ViewModel;

namespace TypeDitor.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel MainWindowViewModel { get; set; }

        public MainWindow(Project loadedProject)
        {
            InitializeComponent();

            MainWindowViewModel = new MainWindowViewModel(
                FindResource("ProjectModel") as IProjectModel,
                FindResource("RecentProvider") as IRecentProvider, FindResource("ProjectProvider") as IProjectProvider
            );
            MainWindowViewModel.LoadedProject = loadedProject;
            DataContext = MainWindowViewModel;

            DockRoot.AddPanel(new OutputPanel(), System.Windows.Controls.Dock.Bottom);
        }
    }
}
