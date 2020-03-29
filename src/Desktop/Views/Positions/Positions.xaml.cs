using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProConstructionsManagment.Desktop.Views.Base;
using ProConstructionsManagment.Desktop.Views.Projects;

namespace ProConstructionsManagment.Desktop.Views.Positions
{
    /// <summary>
    /// Interaction logic for Positions.xaml
    /// </summary>
    public partial class Positions : UserControl
    {
        public Positions()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<PositionsViewModel>();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.Initialize();

            Unloaded += (sender, args) => viewModel.Cleanup();
        }
    }
}
