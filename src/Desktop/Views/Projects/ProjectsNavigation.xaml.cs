using System.Windows.Controls;
using ProConstructionsManagment.Desktop.Views.Base;

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