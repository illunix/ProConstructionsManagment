using System.Windows.Controls;
using ProConstructionsManagment.Desktop.Views.Base;

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
        }
    }
}