using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using Microsoft.Win32.SafeHandles;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.Project
{
    /// <summary>
    ///     Interaction logic for Employee.xaml
    /// </summary>
    public partial class Project : UserControl
    {
        public Project()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<ProjectViewModel>();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.Initialize();
        }
    }
}