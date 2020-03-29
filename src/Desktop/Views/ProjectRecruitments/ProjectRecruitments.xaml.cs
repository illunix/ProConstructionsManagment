using ProConstructionsManagment.Desktop.Views.Base;
using System.Windows.Controls;

namespace ProConstructionsManagment.Desktop.Views.ProjectRecruitments
{
    /// <summary>
    /// Interaction logic for ProjectRecruitments.xaml
    /// </summary>
    public partial class ProjectRecruitments : UserControl
    {
        public ProjectRecruitments()
        {
            InitializeComponent();

            Loaded += async (sender, args) => await (DataContext as ProjectRecruitmentsViewModel).Initialize();

            Unloaded += (sender, args) => (DataContext as ViewModelBase)?.Cleanup();
        }
    }
}