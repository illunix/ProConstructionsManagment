using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.Projects
{
    public class ProjectsNavigationViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;

        public ProjectsNavigationViewModel(IMessengerService messengerService)
        {
            _messengerService = messengerService;
        }

        public ICommand NavigateToMainViewCommand => new AsyncRelayCommand(NavigateToMainView);

        private async Task NavigateToMainView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Main));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.MainNavigation));
        }

        public ICommand NavigateToProjectsViewCommand => new AsyncRelayCommand(NavigateToProjectsView);

        private async Task NavigateToProjectsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Projects));
        }

        public ICommand NavigateToOpenedProjectsViewCommand => new AsyncRelayCommand(NavigateToOpenedProjectsView);

        private async Task NavigateToOpenedProjectsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.OpenedProjects));
        }

        public ICommand NavigateToProjectsToStartViewCommand => new AsyncRelayCommand(NavigateToProjectsToStartView);

        private async Task NavigateToProjectsToStartView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectsToStart));
        }

        public ICommand NavigateToProjectSettlementsViewCommand => new AsyncRelayCommand(NavigateToProjectSettlementsView);

        private async Task NavigateToProjectSettlementsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectSettlements));
        }

        public ICommand NavigateToEndedProjectsViewCommand => new AsyncRelayCommand(NavigateToEndedProjectsView);

        private async Task NavigateToEndedProjectsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.EndedProjects));
        }

        public ICommand NavigateToAddProjectViewCommand => new AsyncRelayCommand(NavigateToAddProject);

        private async Task NavigateToAddProject()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.AddProject));
        }
    }
}