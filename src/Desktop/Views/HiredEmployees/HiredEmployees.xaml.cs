using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.HiredEmployees
{
    /// <summary>
    /// Interaction logic for HiredEmployees.xaml
    /// </summary>
    public partial class HiredEmployees : UserControl
    {
        public HiredEmployees()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<HiredEmployeesViewModel>();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.Initialize();
        }
    }
}