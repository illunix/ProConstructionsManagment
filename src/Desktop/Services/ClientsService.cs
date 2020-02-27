using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Desktop.Configuration;
using ProConstructionsManagment.Desktop.Models;

namespace ProConstructionsManagment.Desktop.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IRequestProvider _requestProvider;

        public ClientsService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<Client>> GetAllClients()
        {
            var uri = $"{Config.ApiUrlBase}/clients";

            var json = await _requestProvider.GetAsync<RootMultiple<Client>>(uri);

            return json.Data;
        }

        public async Task<ObservableCollection<Client>> GetClientById(Guid clientId)
        {
            var uri = $"{Config.ApiUrlBase}/clients/{clientId}";

            var json = await _requestProvider.GetAsync<RootMultiple<Client>>(uri);

            return json.Data;
        }
    }
}