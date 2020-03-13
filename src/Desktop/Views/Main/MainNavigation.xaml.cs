using System.Windows.Controls;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.Main
{
    public partial class MainNavigation : UserControl
    {
        public MainNavigation()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<MainNavigationViewModel>();

            Unloaded += (sender, args) => viewModel.Cleanup();
        }
    }
}