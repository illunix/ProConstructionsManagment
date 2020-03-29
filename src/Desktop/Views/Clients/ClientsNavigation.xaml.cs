using ProConstructionsManagment.Desktop.Views.AddClient;
using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.Clients
{
    public partial class ClientsNavigation : UserControl
    {
        public ClientsNavigation()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<AddClientViewModel>();

            Unloaded += (sender, args) => viewModel.Cleanup();
        }
    }
}