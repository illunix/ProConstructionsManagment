using System;
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

namespace ProConstructionsManagment.Desktop.Views.Project
{
    public class ProjectNavigationViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IShellManager _shellManager;
        private readonly IMessengerService _messengerService;

        private string _projectId;

        public ProjectNavigationViewModel(IProjectsService projectsService, IShellManager shellManager, IMessengerService messengerService)
        {
            _projectsService = projectsService;
            _shellManager = shellManager;
            _messengerService = messengerService;

            messengerService.Register<ProjectIdMessage>(this, msg => ProjectId = msg.ProjectId);
        }

        public string ProjectId
        {
            get => _projectId;
            set => Set(ref _projectId, value);
        }

        public ICommand NavigateToProjectsViewCommand => new AsyncRelayCommand(NavigateToProjectsView);

        private async Task NavigateToProjectsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Projects));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectsNavigation));
        }

        public ICommand NavigateToProjectCostsViewCommand => new AsyncRelayCommand(NavigateToProjectCostsView);

        private async Task NavigateToProjectCostsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectCosts));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectCostsNavigation));

            _messengerService.Send(new ProjectIdMessage(ProjectId));
        }

        public ICommand NavigateToProjectRecruitmentCommand => new AsyncRelayCommand(NavigateToProjectRecruitmentView);

        private async Task NavigateToProjectRecruitmentView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectRecruitments));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectRecruitmentsNavigation));

            _messengerService.Send(new ProjectIdMessage(ProjectId));
        }

        public ICommand NavigateToAddProjectRecruitmentCommand => new AsyncRelayCommand(NavigateToAddProjectRecruitment);

        private async Task NavigateToAddProjectRecruitment()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.AddProjectRecruitment));

            _messengerService.Send(new ProjectIdMessage(ProjectId));
        }

        public ICommand EndProjectCommand => new AsyncRelayCommand(EndProject);

        private async Task EndProject()
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Czy jesteś pewien/pewna że chcesz zakończyć ten projekt?", "", MessageBoxButton.YesNo);

                switch (messageBoxResult)
                {
                    case MessageBoxResult.Yes:
                        _shellManager.SetLoadingData(true);

                        var employee = await _projectsService.GetProjectById(ProjectId);

                        if (employee.Status == 4)
                        {
                            MessageBox.Show("Projekt jest już zakończony");
                        }
                        else if (employee.Status != 4)
                        {
                            MessageBox.Show("Nie możesz zakończyć projektu");
                        }
                        else
                        {
                            var data = new Models.Project
                            {
                                Id = ProjectId,
                                Name = employee.Name,
                                StartDate = employee.StartDate,
                                EndDate = employee.EndDate,
                                PlaceOfPerformance = employee.PlaceOfPerformance,
                                RequiredNumberOfEmployees = employee.RequiredNumberOfEmployees,
                                Agreement = employee.Agreement,
                                Status = 4
                            };

                            var result = _projectsService.UpdateProject(data, ProjectId);
                            if (result.IsSuccessful)
                            {
                                Log.Information($"Successfully changed project status to ended ({data.Id})");

                                MessageBox.Show("Pomyślnie zakończono projekt");
                            }
                        }
                        break;
                    case MessageBoxResult.No:
                        return;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed updating project");

                MessageBox.Show(
                    "Coś poszło nie tak podczas zapisywania zmian, proszę spróbować jeszcze raz. Jeśli problem nadal występuje, skontakuj się z administratorem oprogramowania");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }
    }
}