using System.Windows.Controls;
using ProConstructionsManagment.Desktop.Views.Base;
using ProConstructionsManagment.Desktop.Views.OpenedProjects;

namespace ProConstructionsManagment.Desktop.Views.ProjectsToStart
{
    public partial class ProjectsToStart : UserControl
    {
        public ProjectsToStart()
        {
            InitializeComponent();
            
            var viewModel = ViewModelLocator.Get<ProjectsToStartViewModel>();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.Initialize();
        }
    }
}