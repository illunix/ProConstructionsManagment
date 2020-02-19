using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.ProjectsToStart
{
    public class ProjectsToStartViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;
        
        private string _projectToStartCount;

        private ObservableCollection<Models.Project> _projectsToStart;
        
        public ProjectsToStartViewModel(IProjectsService projectsService, IMessengerService messengerService, IShellManager shellManager)
        {
            _projectsService = projectsService;
            _messengerService = messengerService;
            _shellManager = shellManager;
        }

        public string ProjectsToStartCount
        {
            get => _projectToStartCount;
            set => Set(ref _projectToStartCount, value);
        }

        public ObservableCollection<Project> ProjectsToStart
        {
            get => _projectsToStart;
            set => Set(ref _projectsToStart, value);
        }

        public async Task Initialize()
        {
            _shellManager.SetLoadingData(true);

            ProjectsToStart = await _projectsService.GetProjectsForStart();

            ProjectsToStartCount = $"Łącznie {ProjectsToStart.Count} rekordów";
            
            if (ProjectsToStart.Count == 0)
            {
                _messengerService.Send(new NoDataMessage(true));
            }

            _shellManager.SetLoadingData(false);
        }
    }
}