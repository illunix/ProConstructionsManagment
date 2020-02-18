using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.Projects
{
    public class ProjectsViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;
        
        private string _projectCount;

        private ObservableCollection<Models.Project> _projects;
        
        public ProjectsViewModel(IProjectsService projectsService, IMessengerService messengerService, IShellManager shellManager)
        {
            _projectsService = projectsService;
            _messengerService = messengerService;
            _shellManager = shellManager;
        }

        public string ProjectCount
        {
            get => _projectCount;
            set => Set(ref _projectCount, value);
        }

        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set => Set(ref _projects, value);
        }

        public async Task Initialize()
        {
            _shellManager.SetLoadingData(true);

            Projects = await _projectsService.GetAllProjects();

            ProjectCount = $"Łącznie {Projects.Count} rekordów";
            
            _shellManager.SetLoadingData(false);
        }
    }
}