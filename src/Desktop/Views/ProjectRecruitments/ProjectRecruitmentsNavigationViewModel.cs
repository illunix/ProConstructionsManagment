using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.ProjectRecruitments
{
    public class ProjectRecruitmentsNavigationViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;

        private string _projectId;

        public ProjectRecruitmentsNavigationViewModel(IMessengerService messengerService)
        {
            _messengerService = messengerService;

            messengerService.Register<ProjectIdMessage>(this, msg => ProjectId = msg.ProjectId);
        }

        public string ProjectId
        {
            get => _projectId;
            set => Set(ref _projectId, value);
        }

        public ICommand NavigateToProjectViewCommand => new AsyncRelayCommand(NavigateToProjectView);

        private async Task NavigateToProjectView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Project));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectNavigation));

            _messengerService.Send(new ProjectIdMessage(ProjectId));
        }

        public ICommand NavigateToAddProjectRecruitmentViewCommand => new AsyncRelayCommand(NavigateToAddProjectRecruitmentView);

        private async Task NavigateToAddProjectRecruitmentView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.AddProjectRecruitment));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.AddProjectRecruitmentNavigation));

            _messengerService.Send(new ProjectIdMessage(ProjectId));
        }
    }
}
