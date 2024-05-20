using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using Task2;

namespace Task3
{
    public partial class MainWindow : Window
    {
        private ViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel();
            DataContext = _viewModel;
        }

        private void LoadAssemblyButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "DLL files (*.dll)|*.dll";
            if (openFileDialog.ShowDialog() == true)
            {
                string assemblyPath = openFileDialog.FileName;
                _viewModel.LoadAssembly(assemblyPath);
            }
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ExecuteMethod();
        }

        private void ClassComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ClassComboBox.SelectedItem != null)
            {
                _viewModel.LoadMethods(ClassComboBox.SelectedItem.ToString());
            }
        }

        private void MethodListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MethodListBox.SelectedItem != null)
            {
                _viewModel.SelectedMethodName = MethodListBox.SelectedItem.ToString();
            }
        }

        private void ResultTextBox_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.ExecuteMethod();
        }
    }
}