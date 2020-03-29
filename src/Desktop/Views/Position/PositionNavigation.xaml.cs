using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.Position
{
    /// <summary>
    /// Interaction logic for PositionNavigation.xaml
    /// </summary>
    public partial class PositionNavigation : UserControl
    {
        public PositionNavigation()
        {
            InitializeComponent();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}