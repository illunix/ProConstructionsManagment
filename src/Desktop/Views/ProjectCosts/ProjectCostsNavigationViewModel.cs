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

namespace ProConstructionsManagment.Desktop.Views.ProjectCosts
{
    public class ProjectCostsNavigationViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;

        private string _projectId;

        public ProjectCostsNavigationViewModel(IMessengerService messengerService)
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
        public ICommand NavigateToMainViewCommand => new AsyncRelayCommand(NavigateToMainView);

        private async Task NavigateToMainView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Main));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.MainNavigation));
        }

        public ICommand NavigateToAddProjectCostViewCommand => new AsyncRelayCommand(NavigateToAddProjectCostView);

        private async Task NavigateToAddProjectCostView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.AddProjectCost));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.AddProjectCostNavigation));

            _messengerService.Send(new ProjectIdMessage(ProjectId));
        }
    }
}
