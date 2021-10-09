﻿using System.Windows;
using TypeD.Models;
using TypeD.Models.Providers.Interfaces;
using TypeDitor.ViewModel;

namespace TypeDitor.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel MainWindowViewModel { get; set; }

        public MainWindow(ProjectModel loadedProject)
        {
            InitializeComponent();

            MainWindowViewModel = new MainWindowViewModel(FindResource("RecentProvider") as IRecentProvider);
            MainWindowViewModel.LoadedProject = loadedProject;
            DataContext = MainWindowViewModel;
        }
    }
}
