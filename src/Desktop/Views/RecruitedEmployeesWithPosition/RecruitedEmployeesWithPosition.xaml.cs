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

namespace ProConstructionsManagment.Desktop.Views.RecruitedEmployeesWithPosition
{
    /// <summary>
    /// Interaction logic for RecruitedEmployeesWithPosition.xaml
    /// </summary>
    public partial class RecruitedEmployeesWithPosition : UserControl
    {
        public RecruitedEmployeesWithPosition()
        {
            InitializeComponent();

            Loaded += async (sender, args) => await (DataContext as RecruitedEmployeesWithPositionViewModel).Initialize();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}
