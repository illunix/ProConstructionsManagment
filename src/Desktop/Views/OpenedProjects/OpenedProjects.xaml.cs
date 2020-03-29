using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.OpenedProjects
{
    public partial class OpenedProjects : UserControl
    {
        public OpenedProjects()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<OpenedProjectsViewModel>();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.Initialize();

            Unloaded += (sender, args) => viewModel.Cleanup();
        }
    }
}