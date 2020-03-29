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
using ProConstructionsManagment.Desktop.Views.AddEmployee;
using ProConstructionsManagment.Desktop.Views.Base;

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
