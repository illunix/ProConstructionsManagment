using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.Clients
{
    public partial class Clients : UserControl
    {
        public Clients()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<ClientsViewModel>();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.Initialize();

            Unloaded += (sender, args) => viewModel.Cleanup();
        }
    }
}