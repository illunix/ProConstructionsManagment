using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

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