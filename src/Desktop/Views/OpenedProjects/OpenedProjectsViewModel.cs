using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.OpenedProjects
{
    public class OpenedProjectsViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;
        private readonly IProjectsService _projectsService;
        private readonly IShellManager _shellManager;

        private string _openedProjectCount;

        private ObservableCollection<Models.Project> _openedProjects;

        public OpenedProjectsViewModel(IProjectsService projectsService, IMessengerService messengerService,
            IShellManager shellManager)
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

        public ObservableCollection<Models.Project> OpenedProjects
        {
            get => _openedProjects;
            set => Set(ref _openedProjects, value);
        }

        public async Task Initialize()
        {
            try
            {
                _shellManager.SetLoadingData(true);

                OpenedProjects = await _projectsService.GetStartedProjects();

                OpenedProjectCount = $"Łącznie {OpenedProjects.Count} rekordów";

                if (OpenedProjects.Count == 0) _messengerService.Send(new NoDataMessage(true));
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading opened projects view");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}