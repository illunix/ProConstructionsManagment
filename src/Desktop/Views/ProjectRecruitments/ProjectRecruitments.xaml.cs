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
using ProConstructionsManagment.Desktop.Views.ProjectCosts;

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
