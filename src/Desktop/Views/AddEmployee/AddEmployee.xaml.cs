﻿using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.AddEmployee
{
    /// <summary>
    ///     Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : UserControl
    {
        public AddEmployee()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<AddEmployeeViewModel>();

            DataContext = viewModel;

            Unloaded += (sender, args) => viewModel.Cleanup();

            Loaded += async (sender, args) => await viewModel.Initialize();
        }
    }
}