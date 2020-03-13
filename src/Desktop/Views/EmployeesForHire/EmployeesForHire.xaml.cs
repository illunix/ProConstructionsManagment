using System.Windows.Controls;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.EmployeesForHire
{
    /// <summary>
    ///     Interaction logic for EmployeesForHire.xaml
    /// </summary>
    public partial class EmployeesForHire : UserControl
    {
        public EmployeesForHire()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<EmployeesForHireViewModel>();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.Initialize();

            Unloaded += (sender, args) => viewModel.Cleanup();
        }
    }
}