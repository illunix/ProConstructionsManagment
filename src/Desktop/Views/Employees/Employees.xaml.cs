using System.Windows.Controls;
using ProConstructionsManagment.Desktop.Views.Base;

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
        }
    }
}