using System.Windows.Controls;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.AddProject
{
    /// <summary>
    ///     Interaction logic for AddProject.xaml
    /// </summary>
    public partial class AddProject : UserControl
    {
        public AddProject()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<AddProjectViewModel>();

            DataContext = viewModel;

            Loaded += async (sender, args) => await viewModel.Initialize();

            Unloaded += (sender, args) => viewModel.Cleanup();
        }
    }
}