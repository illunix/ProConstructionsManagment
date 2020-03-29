using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.Projects
{
    public partial class ProjectsNavigation : UserControl
    {
        public ProjectsNavigation()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<ProjectsNavigationViewModel>();

            Unloaded += (sender, args) => viewModel.Cleanup();
        }
    }
}