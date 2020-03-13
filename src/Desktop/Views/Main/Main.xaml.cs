﻿using System.Windows.Controls;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.Main
{
    public partial class Main : UserControl
    {
        public Main()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<MainViewModel>();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.InitializeAsync();

            Unloaded += (sender, args) => viewModel.Cleanup();
        }
    }
}