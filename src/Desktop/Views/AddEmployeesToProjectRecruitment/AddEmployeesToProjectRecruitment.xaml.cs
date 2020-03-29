using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.AddEmployeesToProjectRecruitment
{
    /// <summary>
    /// Interaction logic for AddEmployeesToProjectRecruitment.xaml
    /// </summary>
    public partial class AddEmployeesToProjectRecruitment : UserControl
    {
        public AddEmployeesToProjectRecruitment()
        {
            InitializeComponent();

            Loaded += async (sender, args) => await (DataContext as AddEmployeesToProjectRecruitmentViewModel).Initialize();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}