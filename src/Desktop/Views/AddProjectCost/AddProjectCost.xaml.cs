using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.AddProjectCost
{
    /// <summary>
    /// Interaction logic for AddProjectCost.xaml
    /// </summary>
    public partial class AddProjectCost : UserControl
    {
        public AddProjectCost()
        {
            InitializeComponent();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}