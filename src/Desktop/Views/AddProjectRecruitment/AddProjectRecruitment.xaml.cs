using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.AddProjectRecruitment
{
    /// <summary>
    /// Interaction logic for AddProjectRecruitment.xaml
    /// </summary>
    public partial class AddProjectRecruitment : UserControl
    {
        public AddProjectRecruitment()
        {
            InitializeComponent();

            Loaded += async (sender, args) => await (DataContext as AddProjectRecruitmentViewModel).Initialize();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}