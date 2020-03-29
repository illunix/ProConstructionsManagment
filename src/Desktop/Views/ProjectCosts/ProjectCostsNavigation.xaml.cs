using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.ProjectCosts
{
    /// <summary>
    /// Interaction logic for ProjectCostsNavigation.xaml
    /// </summary>
    public partial class ProjectCostsNavigation : UserControl
    {
        public ProjectCostsNavigation()
        {
            InitializeComponent();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}