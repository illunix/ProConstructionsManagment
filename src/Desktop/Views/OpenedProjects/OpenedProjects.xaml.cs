using System.Windows.Controls;
using ProConstructionsManagment.Desktop.Views.Base;

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
        }
    }
}