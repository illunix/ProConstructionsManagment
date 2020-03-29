using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.Employees
{
    public partial class EmployeesNavigation : UserControl
    {
        public EmployeesNavigation()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<EmployeesNavigationViewModel>();

            Unloaded += (sender, args) => viewModel.Cleanup();
        }
    }
}