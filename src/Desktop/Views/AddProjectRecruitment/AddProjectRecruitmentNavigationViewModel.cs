using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.AddProjectRecruitment
{
    public class AddProjectRecruitmentNavigationViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;

        private string _projectId;

        public AddProjectRecruitmentNavigationViewModel(IMessengerService messengerService)
        {
            _messengerService = messengerService;

            messengerService.Register<ProjectIdMessage>(this, msg => ProjectId = msg.ProjectId);
        }

        public string ProjectId
        {
            get => _projectId;
            set => Set(ref _projectId, value);
        }

        public ICommand NavigateToProjectRecruitmentViewCommand => new AsyncRelayCommand(NavigateToProjectRecruitmentView);

        private async Task NavigateToProjectRecruitmentView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectRecruitments));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectRecruitmentsNavigation));

            _messengerService.Send(new ProjectIdMessage(ProjectId));
        }

        public ICommand NavigateToMainViewCommand => new AsyncRelayCommand(NavigateToMainView);

        private async Task NavigateToMainView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Main));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.MainNavigation));
        }
    }
}