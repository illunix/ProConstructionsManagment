using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.Project
{
    /// <summary>
    /// Interaction logic for ProjectNavigation.xaml
    /// </summary>
    public partial class ProjectNavigation : UserControl
    {
        public ProjectNavigation()
        {
            InitializeComponent();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}