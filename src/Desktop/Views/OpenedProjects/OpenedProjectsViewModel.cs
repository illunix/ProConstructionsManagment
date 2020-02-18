using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.OpenedProjects
{
    public class OpenedProjectsViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;
        
        private string _openedProjectCount;

        private ObservableCollection<Models.Project> _openedProjects;
        
        public OpenedProjectsViewModel(IProjectsService projectsService, IMessengerService messengerService, IShellManager shellManager)
        {
            _projectsService = projectsService;
            _messengerService = messengerService;
            _shellManager = shellManager;
        }

        public string OpenedProjectCount
        {
            get => _openedProjectCount;
            set => Set(ref _openedProjectCount, value);
        }

        public ObservableCollection<Project> OpenedProjects
        {
            get => _openedProjects;
            set => Set(ref _openedProjects, value);
        }

        public async Task Initialize()
        {
            _shellManager.SetLoadingData(true);

            OpenedProjects = await _projectsService.GetStartedProjects();

            OpenedProjectCount = $"Łącznie {OpenedProjects.Count} rekordów";
            
            _shellManager.SetLoadingData(false);
        }
    }
}