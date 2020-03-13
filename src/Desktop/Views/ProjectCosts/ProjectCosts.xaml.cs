using ProConstructionsManagment.Desktop.Views.Base;
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

namespace ProConstructionsManagment.Desktop.Views.ProjectCosts
{
    /// <summary>
    /// Interaction logic for ProjectCosts.xaml
    /// </summary>
    public partial class ProjectCosts : UserControl
    {
        public ProjectCosts()
        {
            InitializeComponent();

            Loaded += async (sender, args) => await (DataContext as ProjectCostsViewModel).Initialize();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}
