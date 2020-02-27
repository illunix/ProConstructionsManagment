using System.Windows.Controls;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.Projects
{
    public partial class Projects : UserControl
    {
        public Projects()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<ProjectsViewModel>();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.Initialize();
        }
    }
}