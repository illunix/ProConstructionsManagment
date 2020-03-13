using System.Windows.Controls;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.EndedProjects
{
    public partial class EndedProjects : UserControl
    {
        public EndedProjects()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<EndedProjectsViewModel>();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.Initialize();

            Unloaded += (sender, args) => viewModel.Cleanup();
        }
    }
}