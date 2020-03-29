using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.Employees
{
    public partial class Employees : UserControl
    {
        public Employees()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<EmployeesViewModel>();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.Initialize();

            Unloaded += (sender, args) => viewModel.Cleanup();
        }
    }
}