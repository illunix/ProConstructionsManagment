using System.Windows.Controls;
using ProConstructionsManagment.Desktop.Views.Base;

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
        }
    }
}