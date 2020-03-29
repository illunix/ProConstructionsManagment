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
using ProConstructionsManagment.Desktop.Views.Project;

namespace ProConstructionsManagment.Desktop.Views.Position
{
    /// <summary>
    /// Interaction logic for Position.xaml
    /// </summary>
    public partial class Position : UserControl
    {
        public Position()
        {
            InitializeComponent();

            Loaded += async (sender, args) => await (DataContext as PositionViewModel).Initialize();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}
