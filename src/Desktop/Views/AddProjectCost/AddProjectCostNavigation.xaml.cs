using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.AddProjectCost
{
    /// <summary>
    /// Interaction logic for AddProjectCostNavigation.xaml
    /// </summary>
    public partial class AddProjectCostNavigation : UserControl
    {
        public AddProjectCostNavigation()
        {
            InitializeComponent();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}