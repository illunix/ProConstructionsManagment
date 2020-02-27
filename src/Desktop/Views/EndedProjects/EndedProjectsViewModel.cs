using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.EndedProjects
{
    public class EndedProjectsViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;
        private readonly IProjectsService _projectsService;
        private readonly IShellManager _shellManager;

        private string _endedProjectCount;

        private ObservableCollection<Models.Project> _endedProjects;

        public EndedProjectsViewModel(IProjectsService projectsService, IMessengerService messengerService,
            IShellManager shellManager)
        {
            _projectsService = projectsService;
            _messengerService = messengerService;
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
            try
            {
                _shellManager.SetLoadingData(true);

                EndedProjects = await _projectsService.GetEndedProjects();

                EndedProjectCount = $"Łącznie {EndedProjects.Count} rekordów";

                if (EndedProjects.Count == 0) _messengerService.Send(new NoDataMessage(true));

            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading ended projects view");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}