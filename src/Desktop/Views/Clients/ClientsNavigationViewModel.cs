using System.Threading.Tasks;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.Clients
{
    public class ClientsNavigationViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;

        public ClientsNavigationViewModel(IMessengerService messengerService)
        {
            _messengerService = messengerService;
        }

        public ICommand NavigateToMainViewCommand => new AsyncRelayCommand(NavigateToMainView);

        private async Task NavigateToMainView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Main));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.MainNavigation));
        }

        public ICommand NavigateToClientsViewCommand => new AsyncRelayCommand(NavigateToClientsView);

        private async Task NavigateToClientsView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Clients));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ClientsNavigation));
        }

        public ICommand NavigateToAddClientViewCommand => new AsyncRelayCommand(NavigateToAddClientView);

        private async Task NavigateToAddClientView()
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.AddClient));
        }
    }
}