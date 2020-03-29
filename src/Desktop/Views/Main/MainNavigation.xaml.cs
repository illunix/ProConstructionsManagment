using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

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