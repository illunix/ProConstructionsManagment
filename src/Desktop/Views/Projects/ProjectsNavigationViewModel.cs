using System.Threading.Tasks;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

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

        public ICommand NavigateToProjectsViewCommand => new AsyncRelayCommand(NavigateToProjectsView);

        public ICommand NavigateToOpenedProjectsViewCommand => new AsyncRelayCommand(NavigateToOpenedProjectsView);

        public ICommand NavigateToProjectsToStartViewCommand => new AsyncRelayCommand(NavigateToProjectsToStartView);

        public ICommand NavigateToProjectSettlementsViewCommand =>
            new AsyncRelayCommand(NavigateToProjectSettlementsView);

        public ICommand NavigateToEndedProjectsViewCommand => new AsyncRelayCommand(NavigateToEndedProjectsView);

        private async Task NavigateToMainView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Main));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.MainNavigation));
        }

        private async Task NavigateToProjectsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Projects));
        }

        private async Task NavigateToOpenedProjectsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.OpenedProjects));
        }

        private async Task NavigateToProjectsToStartView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectsToStart));
        }

        private async Task NavigateToProjectSettlementsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectSettlements));
        }

        private async Task NavigateToEndedProjectsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.EndedProjects));
        }
    }
}