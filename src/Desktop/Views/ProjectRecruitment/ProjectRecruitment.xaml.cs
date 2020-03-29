using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.ProjectRecruitment
{
    /// <summary>
    /// Interaction logic for ProjectRecruitments.xaml
    /// </summary>
    public partial class ProjectRecruitment : UserControl
    {
        public ProjectRecruitment()
        {
            InitializeComponent();

            Loaded += async (sender, args) => await (DataContext as ProjectRecruitmentViewModel).Initialize();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}