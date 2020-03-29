using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.Positions
{
    /// <summary>
    /// Interaction logic for PositionsNavigation.xaml
    /// </summary>
    public partial class PositionsNavigation : UserControl
    {
        public PositionsNavigation()
        {
            InitializeComponent();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}