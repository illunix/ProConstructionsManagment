﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using Microsoft.Win32.SafeHandles;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.Employee
{
    /// <summary>
    ///     Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : UserControl
    {
        public Employee()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<EmployeeViewModel>();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.Initialize();
        }
    }
}