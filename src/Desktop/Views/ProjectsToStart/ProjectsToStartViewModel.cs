using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

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

        public ICommand NavigateToProjectViewCommand => new AsyncRelayCommand<object>(NavigateToProjectView);

        private async Task NavigateToProjectView(object obj)
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Project));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectNavigation));

            if (obj is string projectId)
            {
                _messengerService.Send(new ProjectIdMessage(projectId));
            }
        }

        public async Task Initialize()
        {
            try
            {
                _shellManager.SetLoadingData(true);

                ProjectsToStart = await _projectsService.GetProjectsForStart();

                ProjectsToStartCount = $"Łącznie {ProjectsToStart.Count} rekordów";

                if (ProjectsToStart.Count == 0)
                {
                    _messengerService.Send(new NoDataMessage(true));
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading projects to start view");

                MessageBox.Show("Coś poszło nie tak podczas pobierania danych");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}