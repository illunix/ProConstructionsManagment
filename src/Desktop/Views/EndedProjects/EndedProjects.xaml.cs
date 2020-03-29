using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

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