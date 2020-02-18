using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.EndedProjects
{
    public class EndedProjectsViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IShellManager _shellManager;

        private string _endedProjectCount;

        private ObservableCollection<Models.Project> _endedProjects;
        
        public EndedProjectsViewModel(IProjectsService projectsService, IShellManager shellManager)
        {
            _projectsService = projectsService;
            _shellManager = shellManager;
        }

        public string EndedProjectCount
        {
            get => _endedProjectCount;
            set => Set(ref _endedProjectCount, value);
        }

        public ObservableCollection<Models.Project> EndedProjects
        {
            get => _endedProjects;
            set => Set(ref _endedProjects, value);
        }

        public async Task Initialize()
        {
            _shellManager.SetLoadingData(true);
            
            EndedProjects = await _projectsService.GetEndedProjects();

            EndedProjectCount = $"Łącznie {EndedProjects.Count} rekordów";
            
            _shellManager.SetLoadingData(false);
        }
    }
}