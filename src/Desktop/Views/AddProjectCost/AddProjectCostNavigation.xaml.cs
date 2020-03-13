using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProConstructionsManagment.Desktop.Views.Base;
using ProConstructionsManagment.Desktop.Views.HiredEmployees;

namespace ProConstructionsManagment.Desktop.Views.AddProjectCost
{
    /// <summary>
    /// Interaction logic for AddProjectCostNavigation.xaml
    /// </summary>
    public partial class AddProjectCostNavigation : UserControl
    {
        public AddProjectCostNavigation()
        {
            InitializeComponent();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}
