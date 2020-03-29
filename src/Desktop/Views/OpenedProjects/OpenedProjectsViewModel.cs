using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        public ICommand NavigateToProjectViewCommand => new AsyncRelayCommand<object>(NavigateToProjectView);

        private async Task NavigateToProjectView(object obj)
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Project));

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

                OpenedProjects = await _projectsService.GetStartedProjects();

                OpenedProjectCount = $"Łącznie {OpenedProjects.Count} rekordów";

                if (OpenedProjects.Count == 0) _messengerService.Send(new NoDataMessage(true));
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading opened projects view");

                MessageBox.Show("Coś poszło nie tak podczas pobierania danych");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}