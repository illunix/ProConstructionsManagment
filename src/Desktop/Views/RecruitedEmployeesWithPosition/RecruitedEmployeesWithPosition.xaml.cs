using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

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