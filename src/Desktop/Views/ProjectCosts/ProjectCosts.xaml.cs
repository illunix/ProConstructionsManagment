using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.ProjectCosts
{
    /// <summary>
    /// Interaction logic for ProjectCosts.xaml
    /// </summary>
    public partial class ProjectCosts : UserControl
    {
        public ProjectCosts()
        {
            InitializeComponent();

            Loaded += async (sender, args) => await (DataContext as ProjectCostsViewModel).Initialize();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}