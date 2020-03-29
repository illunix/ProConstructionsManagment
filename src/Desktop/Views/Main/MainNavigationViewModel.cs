using System.Threading.Tasks;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.Main
{
    public class MainNavigationViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;

        public MainNavigationViewModel(IMessengerService messengerService)
        {
            _messengerService = messengerService;
        }

        public ICommand NavigateToProjectsViewCommand => new AsyncRelayCommand(NavigateToProjectsView);

        private async Task NavigateToProjectsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Projects));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectsNavigation));
        }

        public ICommand NavigateToClientsViewCommand => new AsyncRelayCommand(NavigateToClientsView);

        private async Task NavigateToClientsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ClientsNavigation));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Clients));
        }

        public ICommand NavigateToEmployeesViewCommand => new AsyncRelayCommand(NavigateToEmployeesView);

        private async Task NavigateToEmployeesView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Employees));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.EmployeesNavigation));
        }

        public ICommand NavigateToPositionsViewCommand => new AsyncRelayCommand(NavigateToPositionsView);

        private async Task NavigateToPositionsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Positions));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.PositionsNavigation));
        }
    }
}