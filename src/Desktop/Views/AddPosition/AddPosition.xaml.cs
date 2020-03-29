using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.AddPosition
{
    /// <summary>
    /// Interaction logic for AddPosition.xaml
    /// </summary>
    public partial class AddPosition : UserControl
    {
        public AddPosition()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Get<AddPositionViewModel>();

            Unloaded += (sender, args) => viewModel.Cleanup();
        }
    }
}