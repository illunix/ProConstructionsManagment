using System;
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
        private readonly IMessengerService _messengerService;
        private readonly IProjectsService _projectsService;
        private readonly IShellManager _shellManager;

        private ObservableCollection<Models.Project> _projectsToStart;

        private string _projectToStartCount;

        public ProjectsToStartViewModel(IProjectsService projectsService, IMessengerService messengerService,
            IShellManager shellManager)
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

        public ObservableCollection<Models.Project> ProjectsToStart
        {
            get => _projectsToStart;
            set => Set(ref _projectsToStart, value);
        }

        public async Task Initialize()
        {
            try
            {
                _shellManager.SetLoadingData(true);
            }
            catch (Exception e)
            {
                ProjectsToStart = await _projectsService.GetProjectsForStart();

                ProjectsToStartCount = $"Łącznie {ProjectsToStart.Count} rekordów";

                if (ProjectsToStart.Count == 0) _messengerService.Send(new NoDataMessage(true));
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}