using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.ProjectSettlements
{
    public partial class ProjectSettlements : UserControl
    {
        public ProjectSettlements()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<ProjectSettlementsViewModel>();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.Initialize();

            Unloaded += (sender, args) => viewModel.Cleanup();
        }
    }
}