using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;

namespace ProConstructionsManagment.Desktop.Views.Clients
{
    public class ClientsViewModel : ViewModelBase
    {
        private readonly IClientsService _clientsService;
        private readonly IMessengerService _messengerService;

        private string _clientCount;
        
        private ObservableCollection<Models.Client> _clients;
        
        public ClientsViewModel(IClientsService clientsService, IMessengerService messengerService)
        {
            _clientsService = clientsService;
            _messengerService = messengerService;
        }

        public string ClientCount
        {
            get => _clientCount;
            set => Set(ref _clientCount, value);
        }

        public ObservableCollection<Models.Client> Clients
        {
            get => _clients;
            set => Set(ref _clients, value);
        }

        public async Task Initialize()
        {
            Clients = await _clientsService.GetAllClients();

            ClientCount = $"Łącznie {Clients.Count} rekordów";

            if (Clients.Count == 0)
            {
                _messengerService.Send(new NoDataMessage(true));
            }
        }
    }
}