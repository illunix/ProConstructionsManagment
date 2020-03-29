using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.Client
{
    public class ClientNavigationViewModel : ViewModelBase
    {
        private readonly IMessengerService _messengerService;

        public ClientNavigationViewModel(IMessengerService messengerService)
        {
            _messengerService = messengerService;
        }

        public ICommand NavigateToClientsViewCommand => new AsyncRelayCommand<object>(NavigateToClientsView);

        private async Task NavigateToClientsView(object obj)
        {
            _messengerService.Send(new ChangeViewMessage(ViewTypes.Clients));
            _messengerService.Send(new ChangeViewMessage(ViewTypes.ClientsNavigation));
        }
    }
}