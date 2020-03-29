using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.ProjectRecruitment
{
    public class ProjectRecruitmentNavigationViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IMessengerService _messengerService;

        private string _projectId;
        private string _projectRecruitmentId;

        public ProjectRecruitmentNavigationViewModel(IProjectsService projectsService, IMessengerService messengerService)
        {
            _projectsService = projectsService;
            _messengerService = messengerService;

            messengerService.Register<ProjectIdMessage>(this, msg => ProjectId = msg.ProjectId);
            messengerService.Register<ProjectRecruitmentIdMessage>(this, msg => ProjectRecruitmentId = msg.ProjectRecruitmentId);
        }

        public string ProjectId
        {
            get => _projectId;
            set => Set(ref _projectId, value);
        }

        public string ProjectRecruitmentId
        {
            get => _projectRecruitmentId;
            set => Set(ref _projectRecruitmentId, value);
        }

        public ICommand NavigateToProjectRecruitmentsViewCommand => new AsyncRelayCommand(NavigateToProjectRecruitmentsView);

        private async Task NavigateToProjectRecruitmentsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectRecruitments));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectRecruitmentsNavigation));
            _messengerService.Send(new ProjectIdMessage(ProjectId));
            _messengerService.Send(new ProjectRecruitmentIdMessage(ProjectRecruitmentId));
        }

        public ICommand NavigateToMainViewCommand => new AsyncRelayCommand(NavigateToMainView);

        private async Task NavigateToMainView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Main));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.MainNavigation));
        }

        public ICommand NavigateToRecruitedEmployeesViewCommand => new AsyncRelayCommand(NavigateToRecruitedEmployeesView);

        private async Task NavigateToRecruitedEmployeesView()
        {
            var projectRecruitment = await _projectsService.GetProjectRecruitmentById(ProjectRecruitmentId);

            _messengerService.Send(new ChangeViewMessage(ViewTypes.RecruitedEmployeesWithPosition));
            _messengerService.Send(new ProjectIdMessage(ProjectId));
            _messengerService.Send(new ProjectRecruitmentIdMessage(ProjectRecruitmentId));
            _messengerService.Send(new PositionIdMessage(projectRecruitment.PositionId));
        }

        public ICommand NavigateToAddEmployeesToProjectRecruitmentViewCommand => new AsyncRelayCommand(NavigateToAddEmployeesToProjectRecruitmentView);

        private async Task NavigateToAddEmployeesToProjectRecruitmentView()
        {
            var projectRecruitment = await _projectsService.GetProjectRecruitmentById(ProjectRecruitmentId);

            _messengerService.Send(new ChangeViewMessage(ViewTypes.AddEmployeesToProjectRecruitment));
            // _messengerService.Send(new ChangeViewMessage(ViewTypes.AddEmployeesToProjectRecruitmentNavigation));
            _messengerService.Send(new ProjectIdMessage(ProjectId));
            _messengerService.Send(new ProjectRecruitmentIdMessage(ProjectRecruitmentId));
            _messengerService.Send(new PositionIdMessage(projectRecruitment.PositionId));
        }
    }
}